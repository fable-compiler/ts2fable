module rec ts2fable.Print
open ts2fable.Naming

let printType (tp: FsType): string =
    match tp with
    | FsType.Mapped mp ->
        mp.Name
    | FsType.Array at ->
        sprintf (if Config.EmitResizeArray then "ResizeArray<%s>" else "%s[]") (printType at)
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
        printFunctionType ft
        |> sprintf "(%s)"
    | FsType.Tuple tp ->
        let line = ResizeArray()
        tp.Types |> List.map printType |> String.concat " * " |> line.Add
        line |> String.concat ""
    | FsType.Variable vb ->
        printfn "Variable in printType that should have been converted into property: %s" (vb.Name)
        let vtp = vb.Type |> printType
        sprintf "abstract %s: %s%s" vb.Name vtp (if vb.IsConst then "" else " with get, set")
    | FsType.Literal l ->
        match l with
        | FsLiteral.String _ -> "string"
        | FsLiteral.Int _ -> "int"
        | FsLiteral.Float _ -> "float"
        | FsLiteral.Bool _ -> "bool"
    | FsType.Property p -> printType p.Type
    | FsType.Enum en ->
        printfn "unextracted printType %s: %s" (getTypeName tp) (getName tp)
        printEnumType en
    | FsType.GenericTypeParameter gtp ->
        gtp.Name
    | FsType.KeyOf k ->
        printType k.Type
        |> sprintf "KeyOf<%s>"
    | FsType.TypeLiteral tl -> printTypeLiteral tl

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

    | _ ->
        printfn "unsupported printType %s: %s" (getTypeName tp) (getName tp)
        "obj"

/// unlike `printType`, don't surround Function with brackets:
/// * `printType`: `(string -> string)`
/// * `printRootType`: `string -> string`
/// Necessary for generic properties `'T -> 'T`: not valid in brackets
let printRootType (tp: FsType): string =
    match tp with
    | FsType.Function f -> printFunctionType f
    | _ -> printType tp

let printFunctionType (f: FsFunction) =
    match f.Params with
    | [] -> sprintf "unit -> %s" (printType f.ReturnType)
    | ps ->
        let ps =
            ps
            |> List.map (fun p ->
                let t = printType p.Type
                if p.Optional then
                    // this bracket isn't always necessary for example:
                    // `const f: (value?: string) => void`
                    // in F#: `abstract f: (string) option -> unit`
                    // but it's easier to always put into brackets, than to discrimate
                    // when it's needed (tuple, function) and when not (simple type).
                    sprintf "(%s) option" t
                else
                    t
            )
        ps @ [ printType f.ReturnType ]
        |> String.concat " -> "

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

let printFunctionName (fn: FsFunction) (name:string): string =
    let line = ResizeArray()

    match fn.Kind with
    | FsFunctionKind.Regular -> ()
    | FsFunctionKind.Constructor ->
        "[<EmitConstructor>] " |> line.Add
    | FsFunctionKind.Call ->
        "[<Emit(\"$0($1...)\")>] " |> line.Add
    | FsFunctionKind.StringParam emit ->
        sprintf  "[<Emit(\"%s\")>] " emit |> line.Add

    sprintf "abstract %s" name |> line.Add

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

let printFunction (fn: FsFunction): string =
    printFunctionName fn (fn.Name.Value)

let printProperty (pr: FsProperty): string =
    match Config.ConvertPropertyFunctions, pr.Type with
    | true, FsType.Function fn ->
        printFunctionName fn pr.Name
    | _ ->
        printPropertyDefault pr

let printPropertyDefault (pr: FsProperty): string =
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
        (printOptionalType pr.Option pr.Accessor pr.Type)
        (
            // generic type parameter
            match pr.Type with
            | FsType.Function f when f.TypeParameters.Length > 0 ->
                printGenericTypeConstraints f.TypeParameters
            | _ -> ""
        )
        (
            match pr.Accessor with
            | ReadOnly -> ""
            | WriteOnly -> " with set"
            | ReadWrite -> " with get, set"
        )

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

let printTypeLiteral (tl: FsTypeLiteral): string =
    let members =
        tl.Members
        |> List.choose (
            function
            | FsType.Property p ->
                Some (p.Name, printOptionalType p.Option ReadOnly p.Type)
            | FsType.Function ({ Name = Some name } as f) ->
                let prms =
                    f.Params
                    |> List.map (fun p ->
                        printOptionalType p.Optional ReadOnly p.Type
                    )
                let prms =
                    match prms with
                    | [] -> "unit"
                    | _ -> prms |> String.concat " -> "
                let t =
                    sprintf "%s -> %s" prms (printType f.ReturnType)

                Some (name, t)
            | _ -> None
        )

    members
    |> List.map (fun (name, t) -> sprintf "%s: %s" name t)
    |> String.concat "; "
    |> sprintf "{| %s |}"

let printOptionalType (optional: bool) (accessor: FsAccessor) (ty: FsType) =
    let t = printRootType ty

    let surround = sprintf "(%s)"
    let optionInBrackets ty =
        if optional then
            match ty with
            // if `Option<A * B>`, surround tuple with brackets (`(A * B) option`), otherwise it's emitted as `A * Option<B>` (`A * B option`)
            | FsType.Tuple { Kind = FsTupleKind.Tuple; Types = tys } when (tys |> List.length) > 1 ->
                surround t
            // brackets required for function type like `(string -> string) option`
            | FsType.Function _ ->
                surround t
            | _ -> t
        else
            t

    let t =
        match accessor with
        | ReadOnly ->
            optionInBrackets ty
        | WriteOnly | ReadWrite ->
            match ty with
            | FsType.Function _ ->
                surround t
            | _ ->
                optionInBrackets ty

    if optional then
        sprintf "%s option" t
    else
        t

    // if optional then
    //     // when optional type: might need brackets arround formatted `t`:
    //     // Ensure `(A * B) option` instead of `A * B option`
    //     let surround = sprintf "(%s)"

    //     let ts = printType t
    //     match t with
    //     // if `Option<A * B>`, surround tuple with brackets (`(A * B) option`), otherwise it's emitted as `A * Option<B>` (`A * B option`)
    //     | FsType.Tuple { Kind = FsTupleKind.Tuple; Types = tys } when tys.Length > 1 ->
    //         surround ts
    //     // brackets required for function type like `(string -> string) option`
    //     | FsType.Function _ ->
    //         surround ts
    //     | _ -> ts
    //     |> sprintf "%s option"
    // else
    //     printType t

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

    match comments with
    | [] -> ()
    | [ FsComment.Summary lines ] when lines |> List.forall (not << FsComment.containsXml) ->
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
                sprintf " = %s" cs.Value.Value.Value |> line.Add
            sprintf "%s%s" indent (line |> String.concat "") |> lines.Add
    | FsEnumCaseType.String ->
        sprintf "%stype [<StringEnum>] [<RequireQualifiedAccess>] %s =" indent en.Name |> lines.Add
        for cs in en.Cases do
            printComments lines (indent + "    ") cs.Comments
            printAttributes lines (indent + "    ") cs.Attributes
            let nm = cs.Name
            let v = (cs.Value |> Option.map (fun v -> v.Value) |> Option.defaultValue nm).Replace("\"", "\\\"")
            let unm = createEnumName nm
            let line = ResizeArray()
            if nameEqualsDefaultFableValue unm v then
                sprintf "    | %s" unm |> line.Add
            else
                sprintf "    | [<CompiledName(\"%s\")>] %s" v unm |> line.Add
            sprintf "%s%s" indent (line |> String.concat "") |> lines.Add
    | FsEnumCaseType.Unknown ->
        sprintf "%stype %s =" indent en.Name |> lines.Add
        sprintf "%s    obj" indent |> lines.Add

let printDU (lines: ResizeArray<string>) (indent: string) (du: FsTaggedUnionAlias) =
    sprintf "" |> lines.Add
    printComments lines indent du.Comments
    printAttributes lines indent du.Attributes
    let types =
        du.Cases
        |> Map.toList |> List.map snd
        |> List.map printType
        |> List.distinct
    match types with
    | [] ->   // this should never happen
        sprintf "%stype %s =" indent du.Name |> lines.Add
        sprintf "%s    obj" indent |> lines.Add
    | [ty] -> // just emit alias if everything is converted to the same type by `fixEnumReferences`
        sprintf "%stype %s =" indent du.Name |> lines.Add
        sprintf "%s    %s" indent ty |> lines.Add
    | _ ->
        let memberLines = new ResizeArray<string>()
        sprintf "%stype [<TypeScriptTaggedUnion(\"%s\")>] [<RequireQualifiedAccess>] %s%s ="
            indent du.Discriminator du.Name (printTypeParameters du.TypeParameters) |> lines.Add
        let mutable usedNames = Set.empty
        for index, (tag, ty) in du.Cases |> Map.toArray |> Array.indexed do
            let caseName =
                let name =
                    match getName ty with
                    | "" -> // not helpful. see the tag instead
                        match tag.Name with
                        | Some name -> name // if tag is an enum case, use its name
                        | None ->
                            match tag.Value with
                            | FsLiteral.String s -> createEnumName s // use value as name (like in StringEnum)
                            | _ ->
                                // use index. would be confusing if it became something like `Case2147483647`
                                sprintf "Case%d" (index+1)
                    | name -> name
                if usedNames |> Set.contains name then name + "'" else name
            usedNames <- usedNames |> Set.add caseName
            let tyStr = printType ty
            let attr =
                match tag.Value with
                | FsLiteral.String s ->
                    let s = s.Replace("\"", "\\\"")
                    if nameEqualsDefaultFableValue caseName s then ""
                    else sprintf "[<CompiledName(\"%s\")>] " s
                | FsLiteral.Int i -> sprintf "[<CompiledValue(%d)>] " i
                | FsLiteral.Float f -> sprintf "[<CompiledValue(%s)>] " (string f)
                | FsLiteral.Bool b -> sprintf "[<CompiledValue(%b)>] " b
            sprintf "%s    | %s%s of %s" indent attr caseName tyStr |> lines.Add
            sprintf "%s    static member inline op_ErasedCast(x: %s) = %s x" indent tyStr caseName |> memberLines.Add
        lines.AddRange(memberLines) // add members

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
                let mutable nLines = 0
                for ih in inf.Inherits do
                    sprintf "%s    inherit %s" indent (printType ih) |> lines.Add
                    nLines <- nLines + 1
                for mbr in inf.Members do
                    let indent = sprintf "%s    " indent
                    match mbr with
                    | FsType.Function f ->
                        printComments lines indent f.Comments
                        printAttributes lines indent f.Attributes
                        sprintf "%s%s" indent (printFunction f) |> lines.Add
                        nLines <- nLines + 1
                    | FsType.Property p ->
                        printComments lines indent p.Comments
                        printAttributes lines indent p.Attributes
                        sprintf "%s%s" indent (printProperty p) |> lines.Add
                        nLines <- nLines + 1
                    | FsType.Variable v ->
                        printComments lines indent v.Comments
                        printAttributes lines indent v.Attributes
                        sprintf "%s%s" indent (printType mbr) |> lines.Add
                        nLines <- nLines + 1
                    | _ ->
                        sprintf "%s    %s" indent (printType mbr) |> lines.Add
                        nLines <- nLines + 1
                if nLines = 0 then
                    sprintf "%s    interface end" indent |> lines.Add
        | FsType.Enum en ->
            printEnum lines indent en
        | FsType.Alias al ->
            sprintf "" |> lines.Add
            printComments lines indent al.Comments
            printAttributes lines indent al.Attributes
            sprintf "%stype %s%s =" indent al.Name (printTypeParameters al.TypeParameters) |> lines.Add
            sprintf "%s    %s" indent (printType al.Type) |> lines.Add
        | FsType.TaggedUnionAlias du ->
            printDU lines indent du
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
                        | Some ep when ep.IsGlobal -> "[<Global>] "
                        | Some ep ->
                            match ep.Selector with
                            | "*" -> sprintf "[<ImportAll(\"%s\")>] " ep.Path
                            | "default" -> sprintf "[<ImportDefault(\"%s\")>] " ep.Path
                            | _ -> sprintf "[<Import(\"%s\",\"%s\")>] " ep.Selector ep.Path
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

[<RequireQualifiedAccess>]
type private Space =
    | None
    | Before
    | After
    | Both
let private printAdditionalData (lines: ResizeArray<string>) (space: Space) (loc: AdditionalDataLocation) (data: AdditionalData list) =
    match data |> List.filter (fst >> (=) loc) |> List.map snd with
    | [] -> ()
    | data ->
        match space with
        | Space.Before | Space.Both -> lines.Add("")
        | _ -> ()

        data |> List.iter lines.AddRange

        match space with
        | Space.After | Space.Both -> lines.Add("")
        | _ -> ()

let printFsFile version (file: FsFileOut): ResizeArray<string> =
    let lines = ResizeArray<string>()

    sprintf "// ts2fable %s" version |> lines.Add
    printAdditionalData lines Space.None Top file.AdditionalData
    sprintf "module rec %s" file.Namespace |> lines.Add

    printAdditionalData lines Space.Both BetweenModuleAndOpen file.AdditionalData

    for opn in file.Opens do
        sprintf "open %s" opn |> lines.Add

    printAdditionalData lines Space.Both BetweenOpenAndTypes file.AdditionalData

    if not (List.isEmpty file.AbbrevTypes) then
        lines.Add ""
        file.AbbrevTypes |> List.iter lines.Add

    lines.Add ""
    for f in file.Files do
        f.Modules
            |> List.filter (fun md -> md.Types.Length > 0)
            |> List.iter (printModule lines "")

    printAdditionalData lines Space.Before Bottom file.AdditionalData

    lines