module rec ts2fable.Print
open ts2fable.Naming

let printType (tp: FsType): string =
    match tp with
    | FsType.Mapped mp ->
        mp.Name
    | FsType.Array at ->
        sprintf "ResizeArray<%s>" (printType at)
    | FsType.Union un ->
        if un.Types.Length = 1 then
            sprintf "%s%s" (printType un.Types.[0]) (if un.Option then " option" else "")
        else
            let line = ResizeArray()
            sprintf "U%d<" un.Types.Length |> line.Add
            un.Types |> List.map printType |> String.concat ", " |> line.Add
            sprintf ">%s" (if un.Option then " option" else "") |> line.Add
            line |> String.concat ""
    | FsType.Generic g ->
        printGeneric g
    | FsType.Function ft ->
        let line = ResizeArray()
        let typs =
            if ft.Params.Length = 0 then
                [ simpleType "unit"; ft.ReturnType ]
            else
                (ft.Params |> List.map (fun p -> p.Type)) @ [ ft.ReturnType ]
        "(" |> line.Add
        typs |> List.map printType |> String.concat " -> " |> line.Add
        ")"|> line.Add
        line |> String.concat ""
    | FsType.Tuple tp ->
        let line = ResizeArray()
        tp.Types |> List.map printType |> String.concat " * " |> line.Add
        line |> String.concat ""
    | FsType.Variable vb ->
        let vtp = vb.Type |> printType
        sprintf "abstract %s: %s%s" vb.Name vtp (if vb.IsConst then "" else " with get, set")
    | FsType.StringLiteral _ -> "string"
    | FsType.Property p -> printType p.Type
    | FsType.Enum en ->
        printfn "unextracted printType %s: %s" (getTypeName tp) (getName tp)
        printEnumType en
    | FsType.GenericTypeParameter gtp ->
        gtp.Name
    | FsType.KeyOf k ->
        printType k.Type
        |> sprintf "KeyOf<%s>"

    // | FsType.Alias _ -> "obj"
    // | FsType.ExportAssignment _ -> "obj"
    // | FsType.File _ -> "obj"
    // | FsType.FileOut _ -> "obj"
    // | FsType.GenericParameterDefaults _ -> "obj"
    // | FsType.Import _ -> "obj"
    // | FsType.Interface _ -> "obj"
    // | FsType.Module _ -> "obj"
    // | FsType.None -> "obj"
    // | FsType.Param _ -> "obj"
    // | FsType.This -> "obj"
    // | FsType.TODO -> "obj"
    // | FsType.TypeLiteral tp -> "obj"

    | _ ->
        printfn "unsupported printType %s: %s" (getTypeName tp) (getName tp)
        "obj"

let printGeneric (g: FsGenericType): string =
    let line = ResizeArray()
    sprintf "%s" (printType g.Type) |> line.Add
    if g.TypeParameters.Length > 0 then
        "<" |> line.Add
        g.TypeParameters |> List.map printType |> String.concat ", " |> line.Add
        ">" |> line.Add
    line |> String.concat ""

let printEnumType (en: FsEnum): string =
    match en.Type with
    | FsEnumCaseType.Numeric -> "float"
    | FsEnumCaseType.String -> "string"
    | FsEnumCaseType.Unknown -> "obj"

let printFunction (fn: FsFunction): string =
    let line = ResizeArray()

    match fn.Kind with
    | FsFunctionKind.Regular -> ()
    | FsFunctionKind.Constructor ->
        "[<EmitConstructor>] " |> line.Add
    | FsFunctionKind.Call ->
        "[<Emit \"$0($1...)\">] " |> line.Add
    | FsFunctionKind.StringParam emit ->
        sprintf  "[<Emit \"%s\">] " emit |> line.Add

    sprintf "abstract %s" fn.Name.Value |> line.Add

    // parameters
    let prms =
        fn.Params |> List.map(fun p ->
            if p.ParamArray then
                sprintf "[<ParamArray>] %s%s: %s" (if p.Optional then "?" else "") p.Name
                    (   match p.Type with
                        | FsType.Array at ->
                            // array instead of ResizeArray with ParamArray
                            sprintf "%s[]" (printType at)
                        | _ ->
                            // failwithf "function with unsupported param array type: %s" f.Name.Value
                            printfn "ParamArray function is not an array type: %s" (getName(FsType.Function fn))
                            printType p.Type
                    )
            else
                sprintf "%s%s: %s" (if p.Optional then "?" else "") p.Name (printType p.Type)
        )
    if prms.Length = 0 then
        sprintf ": unit" |> line.Add
    else
        sprintf ": %s" (prms |> String.concat " * ") |> line.Add
    sprintf " -> %s" (printType fn.ReturnType) |> line.Add

    // generic type parameter constraints
    printGenericTypeConstraints fn.TypeParameters
    |> line.Add

    line |> String.concat ""

let printProperty (pr: FsProperty): string =
    sprintf "%sabstract %s: %s%s%s%s"
        (   match pr.Kind with
            | FsPropertyKind.Regular -> ""
            | FsPropertyKind.Index -> "[<EmitIndexer>] "
        )
        pr.Name
        (   match pr.Index with
            | None -> ""
            | Some idx -> sprintf "%s: %s -> " idx.Name (printType idx.Type)
        )
        (printType pr.Type)
        (if pr.Option then " option" else "")
        (if pr.IsReadonly then "" else " with get, set")

let printGenericTypeConstraint (p: FsGenericTypeParameter): string option =
    match p.Constraint with
    | None -> None
    | Some c ->
        let formatConstraint = sprintf "%s :> %s" p.Name >> Some
        let rec printConstraint =
            function
              // actual type or generic parameter name: `MyType`, `T`
            | FsType.Mapped mp ->
                formatConstraint mp.Name
              // generic type: `MyType<...>
            | FsType.Generic g ->
                printGeneric g
                |> formatConstraint
              // and: `MyType1 & MyType2` (ts), `'T :> MyType1 and 'T :> MyType2` (F#)
            | FsType.Tuple tp when tp.Kind = FsTupleKind.Intersection ->
                tp.Types
                |> List.choose printConstraint
                |> function
                   | [] -> None
                   | cs ->
                        cs
                        |> String.concat " and "
                        |> Some
            | _ -> None
        printConstraint c
let printGenericTypeConstraints (tps: FsType list): string =
    tps
    |> List.choose FsType.asGenericTypeParameter
    |> List.choose printGenericTypeConstraint
    |> function
       | [] -> ""
       | cs ->
           sprintf " when %s" (cs |> String.concat " and ")


let printTypeParameters (tps: FsType list): string =
    if tps.Length = 0 then ""
    else
        let line = ResizeArray()
        line.Add "<"

        // first: handle type parameter names
        tps |> List.map printType |> String.concat ", " |> line.Add

        // then: handle constraints
        printGenericTypeConstraints tps
        |> line.Add

        line.Add ">"
        line |> String.concat ""

let printComments (lines: ResizeArray<string>) (indent: string) (comments: FsComment list): unit =
    let printLine comment =
        sprintf "%s/// %s" indent comment |> lines.Add
    let printLines = List.iter printLine

    let printTag name attributes (content: FsCommentContent) =
        // 0 lines  : empty tag
        // 1 line   : tag & comment on same line
        // otherwise: tag & comment on different lines

        let nameWithAttributes =
            match attributes with
            | [] -> name
            | _ ->
                let attrs = 
                    attributes 
                    |> List.map (fun (name, value) -> sprintf "%s=\"%s\"" name value)
                    |> String.concat " "
                sprintf "%s %s" name attrs
            
        match content with
        | [] -> 
            sprintf "<%s />" nameWithAttributes
            |> printLine
        | [ line ] ->
            sprintf "<%s>%s</%s>" nameWithAttributes line name
            |> printLine
        | _ ->
            sprintf "<%s>" nameWithAttributes |> printLine
            content |> printLines
            sprintf "</%s>" name |> printLine
    
    let containsXml (text: string) =
        //cannot test for just < or > -> might not be xml tags like `the return value should be Option<string>`
        // -> look for valid_ish xml doc tags
        [
            "<para>"
            "<code>"
            "<c>"
            "<paramref name="
            "<typeparamref name="
            "<see href="
            "<see cref="
        ]
        |> List.exists text.Contains

    match comments with
    | [] -> ()
    | [ FsComment.Summary lines ] when lines |> List.forall (not << containsXml) ->
        // only summary
        // -> no `<summary>` tag necessary
        // BUT: without `<summary>` `<` and `>` are automatically escaped  (https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/xml-documentation#comments-without-xml-tags)
        // -> only emit no summary iff it contains no other tags
        lines
        |> printLines
    | _ ->
        for c in comments do
            match c with
            | FsComment.Summary s -> printTag "summary" [] s
            | FsComment.Param p -> printTag "param" [("name", p.Name)] p.Content
            | FsComment.Returns r -> printTag "returns" [] r
            | FsComment.Remarks r -> printTag "remarks" [] r
            | FsComment.SeeAlso link -> printTag "seealso" [((match link.Type with | HRef -> "href" | CRef -> "cref"), link.Target)] link.Content
            | FsComment.TypeParam tp -> printTag "typeparam" [("name", tp.Name)] tp.Content
            | FsComment.Example e -> printTag "example" [] e
            | FsComment.Exception e -> 
                // exception REQUIRES a type -- otherwise it isn't shown (in VS) or produces a doc parsing error in Ionide
                // BUT: it doesn't matter what's in the attribute...
                // -> use empty type entry when no type specified
                let ty = e.Type |> Option.defaultValue ""
                printTag "exception" [("cref", ty)] e.Content
            | FsComment.Version v -> printTag "version" [] v
            | FsComment.Default d -> printTag "default" [] d
              // Unknown tag, but was explicitly kept (vs. `UnknownTag`) -> print too
            | FsComment.Tag t -> printTag t.Name [] t.Content
            | FsComment.UnknownTag _ -> ()
            | FsComment.Unknown _ -> ()

let printAttributes (lines: ResizeArray<string>) (indent: string) (attrs: FsAttributeSet list) =
    match attrs with
    | [] -> ()
    | _ ->
        let printInBrackets =
            sprintf "%s[<%s>]" indent
            >> lines.Add

        let formatArgument (arg: FsArgument) =
            match arg.Name with
            | Some name -> sprintf "%s = %s" name arg.Value
            | None -> arg.Value
        let formatAttribute (attr: FsAttribute) =
            let name =
                match attr.Namespace with
                | None -> attr.Name
                | Some ns -> sprintf "%s.%s" ns attr.Name
            match attr.Arguments with
            | [] -> name
            | args ->
                let args = 
                    args
                    |> List.map formatArgument
                    |> String.concat ", "
                sprintf "%s(%s)" name args

        for attrSet in attrs do
            match attrs with
            | [] -> ()
            | _ ->
                attrSet
                |> List.map formatAttribute
                |> String.concat "; "
                |> printInBrackets

let printEnum (lines: ResizeArray<string>) (indent: string) (en: FsEnum) =
    sprintf "" |> lines.Add
    printComments lines indent en.Comments
    printAttributes lines indent en.Attributes
    match en.Type with
    | FsEnumCaseType.Numeric ->
        sprintf "%stype [<RequireQualifiedAccess>] %s =" indent en.Name |> lines.Add
        for cs in en.Cases do
            printComments lines (indent + "    ") cs.Comments
            printAttributes lines (indent + "    ") cs.Attributes
            let nm = cs.Name
            let unm = createEnumName nm
            let line = ResizeArray()
            sprintf "    | %s" unm |> line.Add
            if cs.Value.IsSome then
                sprintf " = %s" cs.Value.Value |> line.Add
            sprintf "%s%s" indent (line |> String.concat "") |> lines.Add
    | FsEnumCaseType.String ->
        sprintf "%stype [<StringEnum>] [<RequireQualifiedAccess>] %s =" indent en.Name |> lines.Add
        for cs in en.Cases do
            printComments lines (indent + "    ") cs.Comments
            printAttributes lines (indent + "    ") cs.Attributes
            let nm = cs.Name
            let v = (cs.Value |> Option.defaultValue nm).Replace("\"", "\\\"")
            let unm = createEnumName nm
            let line = ResizeArray()
            if nameEqualsDefaultFableValue unm v then
                sprintf "    | %s" unm |> line.Add
            else
                sprintf "    | [<CompiledName \"%s\">] %s" v unm |> line.Add
            sprintf "%s%s" indent (line |> String.concat "") |> lines.Add
    | FsEnumCaseType.Unknown ->
        sprintf "%stype %s =" indent en.Name |> lines.Add
        sprintf "%s    obj" indent |> lines.Add

let rec printModule (lines: ResizeArray<string>) (indent: string) (md: FsModule): unit =
    let lineCountStart = lines.Count
    let indent =
        if md.Name <> "" then
            "" |> lines.Add
            printComments lines indent md.Comments
            printAttributes lines indent md.Attributes
            sprintf "%smodule %s =" indent md.Name |> lines.Add
            sprintf "%s    " indent
        else indent

    for line in md.HelperLines do
        sprintf "%s%s" indent line |> lines.Add

    // print module aliases first
    for tp in md.Types do
        match tp with
        | FsType.Import imp ->
            match imp with
            | FsImport.Module impmd ->
                if impmd.Module <> impmd.SpecifiedModule then
                    // TODO used ResolvedModule rather than SpecifiedModule
                    sprintf "%smodule %s = %s" indent impmd.Module impmd.SpecifiedModule |> lines.Add
            | _ -> ()
        | _ -> ()

    // print type aliases
    for tp in md.Types do
        match tp with
        | FsType.Import imp ->
            match imp with
            | FsImport.Type imptp ->
                let imsp = imptp.ImportSpecifier
                match imsp.PropertyName with
                | Some pn ->
                    sprintf "%stype %s = %s.%s" indent imsp.Name imptp.SpecifiedModule pn |> lines.Add
                | None ->
                    sprintf "%stype %s = %s.%s" indent imsp.Name imptp.SpecifiedModule imsp.Name |> lines.Add
            | _ -> ()
        | _ -> ()

    // print all other types
    for tp in md.Types do
        match tp with
        | FsType.Interface inf ->
            match inf.Members with
            | [FsType.Enum en] ->
                // type literal -> comments are in parent Alias which was transformed into Interface
                let en = 
                    { en with 
                        Name = inf.Name 
                        Comments = inf.Comments @ en.Comments
                    }
                printEnum lines indent en
            | _ ->
                sprintf "" |> lines.Add
                printComments lines indent inf.Comments
                printAttributes lines indent inf.Attributes
                sprintf "%stype [<AllowNullLiteral>] %s%s =" indent inf.Name (printTypeParameters inf.TypeParameters) |> lines.Add
                let nLines = ref 0
                for ih in inf.Inherits do
                    sprintf "%s    inherit %s" indent (printType ih) |> lines.Add
                    incr nLines
                for mbr in inf.Members do
                    match mbr with
                    | FsType.Function f ->
                        let indent = sprintf "%s    " indent
                        printComments lines indent f.Comments
                        printAttributes lines indent f.Attributes
                        sprintf "%s%s" indent (printFunction f) |> lines.Add
                        incr nLines
                    | FsType.Property p ->
                        let indent = sprintf "%s    " indent
                        printComments lines indent p.Comments
                        printAttributes lines indent p.Attributes
                        sprintf "%s%s" indent (printProperty p) |> lines.Add
                        incr nLines
                    | _ ->
                        sprintf "%s    %s" indent (printType mbr) |> lines.Add
                        incr nLines
                if !nLines = 0 then
                    sprintf "%s    interface end" indent |> lines.Add
        | FsType.Enum en ->
            printEnum lines indent en
        | FsType.Alias al ->
            sprintf "" |> lines.Add
            printComments lines indent al.Comments
            printAttributes lines indent al.Attributes
            sprintf "%stype %s%s =" indent al.Name (printTypeParameters al.TypeParameters) |> lines.Add
            sprintf "%s    %s" indent (printType al.Type) |> lines.Add
        | FsType.Module smd ->
            printModule lines indent smd
        | FsType.Variable vb ->
            if vb.HasDeclare then
                // sprintf "" |> lines.Add
                printComments lines indent vb.Comments
                printAttributes lines indent vb.Attributes
                sprintf "%slet %s%s: %s = jsNative" indent
                    (   match vb.Export with
                        | None -> ""
                        | Some ep ->
                            if ep.IsGlobal then
                                "[<Global>] "
                            else
                                sprintf "[<Import(\"%s\",\"%s\")>] " ep.Selector ep.Path
                    )
                    vb.Name (printType vb.Type)
                |> lines.Add
            // else
            //     printfn "Variable no declare: %A" vb
                // sprintf "%s// let %s = " indent vb.Name |> lines.Add
        | _ -> ()
    let addedLines = lines.Count - lineCountStart
    // remove empty modules
    if addedLines = 2 then
        // lines.RemoveRange (lines.Count-3,2) // https://github.com/fable-compiler/Fable/issues/1242
        for _ in 0..1 do
            lines.RemoveAt (lines.Count-1)

let printFsFile version (file: FsFileOut): ResizeArray<string> =
    let lines = ResizeArray<string>()

    sprintf "// ts2fable %s" version |> lines.Add
    sprintf "module rec %s" file.Namespace |> lines.Add

    for opn in file.Opens do
        sprintf "open %s" opn |> lines.Add

    if not (List.isEmpty file.AbbrevTypes) then
        lines.Add ""
        file.AbbrevTypes |> List.iter lines.Add

    lines.Add ""
    for f in file.Files do
        f.Modules
            |> List.filter (fun md -> md.Types.Length > 0)
            |> List.iter (printModule lines "")
    lines