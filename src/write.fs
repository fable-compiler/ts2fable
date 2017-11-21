module rec ts2fable.Write
open System.Collections.Generic
open ts2fable.Naming

let printType (tp: FsType): string =
    match tp with
    | FsType.Mapped mp -> mp.Name
    | FsType.TODO -> "TODO"
    | FsType.Array at ->
        sprintf "ResizeArray<%s>" (printType at)
    | FsType.Union un ->
        if un.Types.Length = 1 then
            sprintf "%s%s" (printType un.Types.[0]) (if un.Option then " option" else "")
        else
            let line = List()
            sprintf "U%d<" un.Types.Length |> line.Add
            un.Types |> List.map printType |> String.concat ", " |> line.Add
            sprintf ">%s" (if un.Option then " option" else "") |> line.Add
            line |> String.concat ""
    | FsType.Generic g ->
        let line = List()
        sprintf "%s" (printType g.Type) |> line.Add
        if g.TypeParameters.Length > 0 then
            "<" |> line.Add
            g.TypeParameters |> List.map printType |> String.concat ", " |> line.Add
            ">" |> line.Add
        line |> String.concat ""
    | FsType.Function ft ->
        let line = List()
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
        let line = List()
        tp.Types |> List.map printType |> String.concat " * " |> line.Add
        line |> String.concat ""
    | FsType.Variable vb ->
        let vtp = vb.Type |> printType
        sprintf "abstract %s: %s with get, set" vb.Name vtp
    | FsType.StringLiteral _ -> "string"
    | _ -> printfn "unsupported printType %A" tp; "TODO"

let printFunction (f: FsFunction): string =
    let line = List()

    match f.Kind with
    | FsFunctionKind.Regular -> ()
    | FsFunctionKind.Constructor ->
        "[<Emit \"new $0($1...)\">] " |> line.Add
    | FsFunctionKind.Call ->
        "[<Emit \"$0($1...)\">] " |> line.Add
    | FsFunctionKind.StringParam emit ->
        sprintf  "[<Emit \"%s\">] " emit |> line.Add

    sprintf "abstract %s" f.Name.Value |> line.Add
    let prms = 
        f.Params |> List.map(fun p ->
            if p.ParamArray then
                sprintf "[<ParamArray>] %s%s: %s" (if p.Optional then "?" else "") p.Name
                    (   match p.Type with
                        | FsType.Array t -> printType t // inner type
                        | _ -> 
                            // failwithf "function with unsupported param array type: %s" f.Name.Value
                            printfn "ParamArray function is not an array type: %s" (getName(FsType.Function f))
                            printType p.Type
                    )
            else
                sprintf "%s%s: %s" (if p.Optional then "?" else "") p.Name (printType p.Type)
        )
    if prms.Length = 0 then
        sprintf ": unit" |> line.Add
    else
        sprintf ": %s" (prms |> String.concat " * ") |> line.Add
    sprintf " -> %s" (printType f.ReturnType) |> line.Add
    line |> String.concat ""
let printProperty (pr: FsProperty): string =
    sprintf "%sabstract %s: %s%s%s%s"
        (   match pr.Kind with
            | FsPropertyKind.Regular -> ""
            | FsPropertyKind.Index -> "[<Emit \"$0[$1]{{=$2}}\">] "
        )
        pr.Name
        (   match pr.Index with
            | None -> ""
            | Some idx -> sprintf "%s: %s -> " idx.Name (printType idx.Type)
        )
        (printType pr.Type)
        (if pr.Option then " option" else "")
        (if pr.IsReadonly then "" else " with get, set")

let printTypeParameters (tps: FsType list): string =
    if tps.Length = 0 then ""
    else
        let line = List()
        line.Add "<"
        tps |> List.map printType |> String.concat ", " |> line.Add
        line.Add ">"
        line |> String.concat ""

let printComments (lines: List<string>) (indent: string) (comments: string list): unit =
    for comment in comments do
        sprintf "%s/// %s" indent comment |> lines.Add

let rec printModule (lines: List<string>) (indent: string) (md: FsModule): unit =
    let lineCountStart = lines.Count
    let indent =
        if md.Name <> "" then
            "" |> lines.Add
            sprintf "%smodule %s =" indent md.Name |> lines.Add
            sprintf "%s    " indent
        else indent

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
                sprintf "%stype %s = %s.%s" indent imptp.Type imptp.SpecifiedModule imptp.Type |> lines.Add
            | _ -> ()
        | _ -> ()

    // print all other types
    for tp in md.Types do
        match tp with
        | FsType.Interface inf ->
            sprintf "" |> lines.Add
            printComments lines indent inf.Comments
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
                    sprintf "%s%s" indent (printFunction f) |> lines.Add
                    incr nLines
                | FsType.Property p ->
                    let indent = sprintf "%s    " indent
                    printComments lines indent p.Comments
                    sprintf "%s%s" indent (printProperty p) |> lines.Add
                    incr nLines
                | _ ->
                    sprintf "%s    %s" indent (printType mbr) |> lines.Add
                    incr nLines
            if !nLines = 0 then
                sprintf "%s    interface end" indent |> lines.Add
        | FsType.Enum en ->
            sprintf "" |> lines.Add
            match en.Type with
            | FsEnumCaseType.Numeric ->
                sprintf "%stype [<RequireQualifiedAccess>] %s =" indent en.Name |> lines.Add
                for cs in en.Cases do
                    let nm = cs.Name
                    let unm = createEnumName nm
                    let line = List()
                    sprintf "    | %s" unm |> line.Add
                    if cs.Value.IsSome then
                        sprintf " = %s" cs.Value.Value |> line.Add
                    sprintf "%s%s" indent (line |> String.concat "") |> lines.Add
            | FsEnumCaseType.String ->
                sprintf "%stype [<StringEnum>] [<RequireQualifiedAccess>] %s =" indent en.Name |> lines.Add
                for cs in en.Cases do
                    let nm = cs.Name
                    let v = cs.Value |> Option.defaultValue nm
                    let unm = createEnumName nm
                    let line = List()
                    if nameEqualsDefaultFableValue unm v then
                        sprintf "    | %s" unm |> line.Add
                    else
                        sprintf "    | [<CompiledName \"%s\">] %s" v unm |> line.Add
                    sprintf "%s%s" indent (line |> String.concat "") |> lines.Add
            | FsEnumCaseType.Unknown ->
                sprintf "%stype %s =" indent en.Name |> lines.Add
                sprintf "%s    obj" indent |> lines.Add
        | FsType.Alias al ->
            sprintf "" |> lines.Add
            sprintf "%stype %s%s =" indent al.Name (printTypeParameters al.TypeParameters) |> lines.Add
            sprintf "%s    %s" indent (printType al.Type) |> lines.Add
        | FsType.Module smd ->
            printModule lines indent smd
        | FsType.Variable vb ->
            if vb.HasDeclare then
                sprintf "" |> lines.Add
                sprintf "%slet %s%s: %s = jsNative" indent 
                    (   match vb.Import with
                        | None -> ""
                        | Some im ->
                            // special case Node
                            if im.Path = "node" then
                                "[<Global>] "
                            else
                                sprintf "[<Import(\"%s\",\"%s\")>] " im.Selector im.Path
                    )
                    vb.Name (printType vb.Type)
                |> lines.Add
        | _ -> ()
    let addedLines = lines.Count - lineCountStart
    // remove empty modules
    if addedLines = 2 then
        // lines.RemoveRange (lines.Count-3,2) // https://github.com/fable-compiler/Fable/issues/1242
        for _ in 0..1 do
            lines.RemoveAt (lines.Count-1)

let printFsFile (file: FsFileOut): List<string> =
    let lines = List<string>()

    sprintf "module rec %s" file.Namespace |> lines.Add

    for opn in file.Opens do
        sprintf "open %s" opn |> lines.Add

    for f in file.Files do
        f.Modules
            |> List.filter (fun md -> md.Types.Length > 0)
            |> List.iter (printModule lines "")
    lines