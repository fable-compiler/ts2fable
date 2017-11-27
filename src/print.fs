module rec ts2fable.Print
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
            let line = ResizeArray()
            sprintf "U%d<" un.Types.Length |> line.Add
            un.Types |> List.map printType |> String.concat ", " |> line.Add
            sprintf ">%s" (if un.Option then " option" else "") |> line.Add
            line |> String.concat ""
    | FsType.Generic g ->
        let line = ResizeArray()
        sprintf "%s" (printType g.Type) |> line.Add
        if g.TypeParameters.Length > 0 then
            "<" |> line.Add
            g.TypeParameters |> List.map printType |> String.concat ", " |> line.Add
            ">" |> line.Add
        line |> String.concat ""
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
    | FsType.TypeLiteral _ ->
        // printfn "TypeLiteral %A" tp
        "obj"
    | _ ->
        printfn "unsupported printType %A" tp
        "obj"

let printFunction (fn: FsFunction): string =
    let line = ResizeArray()

    match fn.Kind with
    | FsFunctionKind.Regular -> ()
    | FsFunctionKind.Constructor ->
        "[<Emit \"new $0($1...)\">] " |> line.Add
    | FsFunctionKind.Call ->
        "[<Emit \"$0($1...)\">] " |> line.Add
    | FsFunctionKind.StringParam emit ->
        sprintf  "[<Emit \"%s\">] " emit |> line.Add

    sprintf "abstract %s" fn.Name.Value |> line.Add
    let prms = 
        fn.Params |> List.map(fun p ->
            if p.ParamArray then
                sprintf "[<ParamArray>] %s%s: %s" (if p.Optional then "?" else "") p.Name
                    (   match p.Type with
                        | FsType.Array _ -> printType p.Type
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
        let line = ResizeArray()
        line.Add "<"
        tps |> List.map printType |> String.concat ", " |> line.Add
        line.Add ">"
        line |> String.concat ""