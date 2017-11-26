module rec ts2fable.Write
open ts2fable.Naming
open ts2fable.Print

let printComments (lines: ResizeArray<string>) (indent: string) (comments: string list): unit =
    for comment in comments do
        sprintf "%s/// %s" indent comment |> lines.Add

let rec printModule (lines: ResizeArray<string>) (indent: string) (md: FsModule): unit =
    let lineCountStart = lines.Count
    let indent =
        if md.Name <> "" then
            "" |> lines.Add
            if md.HasAttributes then
                sprintf "%s[<%s)>]" indent (md.Attributes |> String.concat "; " ) |> lines.Add
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
                    let line = ResizeArray()
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
                    let line = ResizeArray()
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
                // sprintf "" |> lines.Add
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

let printFsFile (file: FsFileOut): ResizeArray<string> =
    let lines = ResizeArray<string>()

    sprintf "// ts2fable %s" Version.version |> lines.Add
    sprintf "module rec %s" file.Namespace |> lines.Add

    for opn in file.Opens do
        sprintf "open %s" opn |> lines.Add

    sprintf "" |> lines.Add
    for f in file.Files do
        f.Modules
            |> List.filter (fun md -> md.Types.Length > 0)
            |> List.iter (printModule lines "")
    lines