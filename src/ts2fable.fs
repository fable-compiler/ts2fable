module rec ts2fable.App

open Fable.Core
open Fable.Import
open Fable.Import.Node
open Fable.Import.ts
open System.Collections.Generic
open System

// our simplified F# AST
// some names inspired by the actual F# AST:
// https://github.com/fsharp/FSharp.Compiler.Service/blob/master/src/fsharp/ast.fs

type FsInterface =
    {
        Name: string
        TypeParameters: FsType list
        Inherits: FsType list
        Members: FsType list
    }

type FsClass =
    {
        Name: string
        TypeParameters: FsType list
        Members: FsType list
    }

[<RequireQualifiedAccess>]
type FsEnumCaseType =
    | Numeric
    | String
    | Unknown

type FsEnumCase =
    {
        Name: string
        Type: FsEnumCaseType
        Value: string option
    }

type FsEnum =
    {
        Name: string
        Cases: FsEnumCase list
    }
    with
        member x.Type
            with get() =
                if x.Cases |> List.exists (fun c -> c.Type = FsEnumCaseType.Unknown) then
                    FsEnumCaseType.Unknown
                else if x.Cases |> List.exists (fun c -> c.Type = FsEnumCaseType.String) then
                    FsEnumCaseType.String
                else
                    FsEnumCaseType.Numeric

type FsParam =
    {
        Name: string
        Optional: bool
        Type: FsType
    }

type FsFunction =
    {
        Name: string
        TypeParameters: FsType list
        Params: FsParam list
        ReturnType: FsType
    }

type FsFunctionType =
    {
        Name: string option
        TypeParameters: FsType list
        Params: FsParam list
        ReturnType: FsType
    }

type FsProperty =
    {
        Name: string
        Option: bool
        Type: FsType
    }

type FsGenericType =
    {
        Type: FsType
        TypeParameters: FsType list
    }

type FsUnion =
    {
        Option: bool
        Types: FsType list
    }

type FsAlias =
    {
        Name: string
        Type: FsType
        TypeParameters: FsType list
    }

type FsTuple =
    {
        Types: FsType list
    }

[<RequireQualifiedAccess>]
type FsType =
    | Interface of FsInterface
    | Class of FsClass
    | Enum of FsEnum
    | Property of FsProperty
    | Param of FsParam
    | Array of FsType
    | TODO
    | None // when it is not set
    | Mapped of string
    | Function of FsFunction
    | FunctionType of FsFunctionType
    | Union of FsUnion
    | Alias of FsAlias
    | Generic of FsGenericType
    | Tuple of FsTuple
    | Module of FsModule
    | File of FsFile

type FsModule =
    {
        Name: string
        Types: FsType list
    }

type FsFile =
    {
        Modules: FsModule list
    }


type Globals with
    member x.forEachChildNode (node: Node) cbNode =
        let func = System.Func<_,_>(fun (node:Node) -> cbNode node; None)
        x.forEachChild<unit>(node, func) |> ignore

    member x.forEachChildNodeRec (root: Node) cbNode =
        cbNode root
        x.forEachChildNode root (fun node -> x.forEachChildNodeRec node cbNode)

    member x.isLiteralExpressionAs(node: Node): LiteralExpression option =
        if x.isLiteralExpression node then Some(node :?> LiteralExpression) else None

let getModules(root: Node) =
    let nodes = ResizeArray<ModuleBlock>()
    ts.forEachChildNodeRec root (fun node -> 
        if node.kind = SyntaxKind.ModuleBlock then
            nodes.Add (node :?> ModuleBlock) )
    nodes |> List.ofSeq

let getModuleName(mb: ModuleBlock): string =
    match mb.parent with
    | Some md ->
        match md.name with
        | U2.Case1 id -> id.getText()
        | U2.Case2 sl -> sl.text
    | None -> "NoModuleName"

let getPropertyName(pn: PropertyName): string =
    match pn with
    | U4.Case1 id -> id.text
    | U4.Case2 sl -> sl.text
    | U4.Case3 nl -> nl.text
    | U4.Case4 cpn -> cpn.getText()

let getBindingyName(bn: BindingName): string =
    match bn with
    | U2.Case1 id -> id.text
    | U2.Case2 bp -> // BindingPattern
        match bp with
        | U2.Case1 obp -> obp.getText()
        | U2.Case2 abp -> abp.getText()

let visitEnumCase(em: EnumMember): FsEnumCase =
    let name = em.name |> getPropertyName
    let tp, value =
        match em.initializer with
        | None -> FsEnumCaseType.Unknown, None
        | Some ep ->
            match ep.kind with
            | SyntaxKind.NumericLiteral ->
                let nl = ep :?> NumericLiteral
                FsEnumCaseType.Numeric, Some nl.text
            // | _ -> None // TODO TypeScript string based enums #17
            // https://github.com/fable-compiler/ts2fable/issues/17
            | SyntaxKind.StringLiteral ->
                let sl = ep :?> StringLiteral
                FsEnumCaseType.String, Some sl.text
            | SyntaxKind.PrefixUnaryExpression -> // TODO TypeScript Ternary
                FsEnumCaseType.Unknown, None
            | _ -> failwithf "EnumCase type not supported %A %A" ep.kind name
    {
        Name = name
        Type = tp
        Value = value
    }

let visitTypeParameters(tps: TypeParameterDeclaration array option): FsType list =
    match tps with
    | None -> []
    | Some tps ->
        tps |> List.ofArray |> List.map (fun tp ->
            tp.name.text |> FsType.Mapped
        )

let visitInterface(id: InterfaceDeclaration): FsInterface =
    {
        Name = id.name.text 
        Inherits =
            match id.heritageClauses with
            | None -> []
            | Some hcs ->
                hcs |> List.ofArray |> List.map (fun hc ->
                    hc.types |> List.ofArray |> List.map (fun eta ->
                        {
                            Type = visitTypeNode eta
                            TypeParameters =
                                match eta.typeArguments with
                                | None -> []
                                | Some tps ->
                                    tps |> List.ofArray |> List.map visitTypeNode
                        }
                        |> FsType.Generic
                    )
                )
                |> List.concat
        Members = id.members |> List.ofArray |> List.map visitTypeElement
        TypeParameters = visitTypeParameters id.typeParameters
    }

let visitEnum(ed: EnumDeclaration): FsEnum =
    {
        Name = ed.name.text
        Cases = ed.members |> List.ofArray |> List.map visitEnumCase
    }

let visitTypeReference(tr: TypeReferenceNode): FsType =
    match tr.typeArguments with
    | None ->
        let txt = tr.getText()
        if txt.Contains "." then
            txt.Substring(0, txt.IndexOf ".") |> FsType.Mapped
        else
            txt |> FsType.Mapped
    | Some tas ->
        {
            Type =
                match tr.typeName with
                | U2.Case1 id ->
                    id.text
                | U2.Case2 qn ->
                    qn.getText()
                |> FsType.Mapped
            TypeParameters =
                tas |> List.ofArray |> List.map visitTypeNode
        }
        |>
        FsType.Generic
let visitFunctionType(ft: FunctionTypeNode): FsFunctionType =
    {
        Name = ft.name |> Option.map getPropertyName
        TypeParameters = visitTypeParameters ft.typeParameters
        Params =  ft.parameters |> List.ofArray |> List.map visitParameterDeclaration
        ReturnType =
            match ft.``type`` with
            | Some t -> visitTypeNode t
            | None -> FsType.Mapped "unit"
    }
let rec visitTypeNode(t: TypeNode): FsType =
    match t.kind with
    | SyntaxKind.StringKeyword -> FsType.Mapped "string"
    | SyntaxKind.FunctionType ->
        visitFunctionType (t :?> FunctionTypeNode) |> FsType.FunctionType
    | SyntaxKind.TypeReference ->
        visitTypeReference (t :?> TypeReferenceNode)
    | SyntaxKind.ArrayType ->
        // return "ResizeArray<" + getType(type.elementType) + ">";
        let at = t :?> ArrayTypeNode
        FsType.Array (visitTypeNode at.elementType)
    | SyntaxKind.NumberKeyword -> FsType.Mapped "float"
    | SyntaxKind.BooleanKeyword -> FsType.Mapped "bool"
    | SyntaxKind.UnionType ->
        let un = t :?> UnionTypeNode
        let typs = un.types |> List.ofArray
        let isUndefined (t:TypeNode) = t.kind = SyntaxKind.UndefinedKeyword
        {
            Option = typs |> List.exists isUndefined
            Types = typs |> List.filter (isUndefined >> not) |> List.map visitTypeNode
        }
        |> FsType.Union
    | SyntaxKind.AnyKeyword -> FsType.Mapped "obj"
    | SyntaxKind.VoidKeyword -> FsType.Mapped "unit"
    | SyntaxKind.TupleType ->
        let tp = t :?> TupleTypeNode
        {
            Types = tp.elementTypes |> List.ofArray |> List.map visitTypeNode
        }
        |> FsType.Tuple
    | SyntaxKind.SymbolKeyword -> FsType.Mapped "Symbol"
    | SyntaxKind.ThisType ->
        // TODO map to the actual type of this
        FsType.Mapped "obj"
    | SyntaxKind.TypePredicate -> FsType.Mapped "bool"
    | SyntaxKind.TypeLiteral -> FsType.Mapped "obj"
    | SyntaxKind.IntersectionType -> FsType.Mapped "obj"
    | SyntaxKind.IndexedAccessType ->
        // function createKeywordTypeNode(kind: KeywordTypeNode["kind"]): KeywordTypeNode;
        FsType.Mapped "obj" // TODO
    | SyntaxKind.TypeQuery ->
        // let tq = t :?> TypeQueryNode
        FsType.Mapped "obj"
    | SyntaxKind.LiteralType -> FsType.Mapped "obj"
    | SyntaxKind.ExpressionWithTypeArguments ->
        let eta = t :?> ExpressionWithTypeArguments
        match eta.expression.kind with
        | SyntaxKind.Identifier ->
            let id = eta.expression :?> Identifier
            FsType.Mapped id.text
        | _ -> failwithf "unsupported TypeNode ExpressionWithTypeArguments kind: %A" eta.expression.kind
    | SyntaxKind.ParenthesizedType -> FsType.Mapped "obj"
    | _ -> failwithf "unsupported TypeNode kind: %A" t.kind

let visitParameterDeclaration(pd: ParameterDeclaration): FsParam =
    {
        Name = pd.name |> getBindingyName
        Optional = pd.questionToken.IsSome
        Type = 
            match pd.``type`` with
            | Some t -> visitTypeNode t
            | None -> FsType.Mapped "obj"
    }

let visitMethodSignature(ms: MethodSignature): FsFunction =
    {
        Name = ms.name |> getPropertyName
        TypeParameters = visitTypeParameters ms.typeParameters
        Params = ms.parameters |> List.ofArray |> List.map visitParameterDeclaration
        ReturnType =
            match ms.``type`` with
            | Some t -> visitTypeNode t
            | None -> FsType.Mapped "unit"
    }

let visitPropertySignature(ps: PropertySignature): FsProperty =
    {
        Name = ps.name |> getPropertyName
        Option = ps.questionToken.IsSome
        Type = 
            match ps.``type`` with
            | None -> FsType.None 
            | Some tp -> visitTypeNode tp
    }

let visitFunctionDeclaration(fd: FunctionDeclaration): FsFunction =
    {
        Name =
            match fd.name with
            | None -> "TodoNamelessFunction"
            | Some nm -> nm.text
        TypeParameters = visitTypeParameters fd.typeParameters
        Params = fd.parameters |> List.ofArray |> List.map visitParameterDeclaration
        ReturnType =
            match fd.``type`` with
            | Some t -> visitTypeNode t
            | None -> FsType.Mapped "unit"
    }

let visitTypeElement(te: TypeElement): FsType =
    match te.kind with
    | SyntaxKind.IndexSignature -> FsType.TODO
    | SyntaxKind.MethodSignature ->
        visitMethodSignature (te :?> MethodSignature) |> FsType.Function
    | SyntaxKind.PropertySignature ->
        visitPropertySignature (te :?> PropertySignature) |> FsType.Property
    | SyntaxKind.CallSignature -> FsType.TODO
    | _ -> failwithf "unsupported TypeElement kind: %A" te.kind

let visitAliasDeclaration(d: TypeAliasDeclaration): FsAlias =
    {
        Name = d.name.text
        Type = d.``type`` |> visitTypeNode
        TypeParameters = visitTypeParameters d.typeParameters
    }

let visitStatement(sd: Statement): FsType =
    match sd.kind with
    | SyntaxKind.InterfaceDeclaration ->
        visitInterface (sd :?> InterfaceDeclaration) |> FsType.Interface
    | SyntaxKind.EnumDeclaration ->
        visitEnum (sd :?> EnumDeclaration) |> FsType.Enum
    | SyntaxKind.TypeAliasDeclaration ->
        visitAliasDeclaration (sd :?> TypeAliasDeclaration) |> FsType.Alias
    | SyntaxKind.ClassDeclaration -> FsType.TODO
    | SyntaxKind.VariableStatement -> FsType.TODO
    | SyntaxKind.FunctionDeclaration ->
        visitFunctionDeclaration (sd :?> FunctionDeclaration) |> FsType.Function
    | SyntaxKind.ModuleDeclaration -> FsType.TODO
    | _ -> failwithf "unsupported Statement kind: %A" sd.kind

let visitModuleBlock(mb: ModuleBlock): FsModule =
    {
        Name = mb |> getModuleName
        Types = mb.statements |> List.ofArray |> List.map visitStatement
    }

let mergeTypes(tps: FsType list): FsType list =
    let index = Dictionary<string,int>()
    let list = List<FsType>()
    for b in tps do
        match b with
        | FsType.Interface bi ->
            if index.ContainsKey bi.Name then
                let i = index.[bi.Name]
                let a = list.[i]
                match a with
                | FsType.Interface ai ->
                    list.[i] <-
                        {
                            Name = ai.Name
                            TypeParameters = ai.TypeParameters
                            Inherits = List.append ai.Inherits bi.Inherits
                            Members = List.append ai.Members bi.Members
                        }
                        |> FsType.Interface
                | _ ->
                    list.Add b
                    index.Add(bi.Name, list.Count-1)

            else
                list.Add b
                index.Add(bi.Name, list.Count-1)
        | _ -> 
            list.Add b
    list |> List.ofSeq

let mergeModules(mds: FsModule list): FsModule list =
    let index = Dictionary<string,int>()
    let list = List<FsModule>()
    for b in mds do
        if index.ContainsKey b.Name then
            let i = index.[b.Name]
            let a = list.[i]
            list.[i] <-
                {
                    Name = a.Name
                    Types = List.append a.Types b.Types |> mergeTypes
                }
        else
            list.Add b
            index.Add(b.Name, list.Count-1)
    list |> List.ofSeq

let asFunction (tp: FsType) = match tp with | FsType.Function v -> Some v | _ -> None
let asMapped (tp: FsType) = match tp with | FsType.Mapped v -> Some v | _ -> None
let asInterface (tp: FsType) = match tp with | FsType.Interface v -> Some v | _ -> None
let asClass (tp: FsType) = match tp with | FsType.Class v -> Some v | _ -> None
let asGeneric (tp: FsType) = match tp with | FsType.Generic v -> Some v | _ -> None

let isFunction tp = asFunction tp |> Option.isSome

let createGlobals(md: FsModule): FsModule =
    let fns = md.Types |> List.filter isFunction
    let cl: FsClass =
        {
            Name = "Globals"
            TypeParameters = []
            Members = fns
        }
    { md with
        Types = [ FsType.Class cl ] @ md.Types
    }

let rec fixType (fix: FsType -> FsType) (tp: FsType): FsType =

    let fixModule (md: FsModule) =    
        { md with
            Types = md.Types |> List.map (fixType fix)
        }

    let fixParam (pm: FsParam) =
        { pm with
            Type = fixType fix pm.Type
        }

    match tp with
    | FsType.Interface it ->
        { it with
            TypeParameters = it.TypeParameters |> List.map (fixType fix)
            Inherits = it.Inherits |> List.map (fixType fix)
            Members = it.Members |> List.map (fixType fix)
        }
        |> FsType.Interface
    | FsType.Class cl ->
        { cl with
            TypeParameters = cl.TypeParameters |> List.map (fixType fix)
            Members = cl.Members |> List.map (fixType fix)
        }
        |> FsType.Class
    | FsType.Property pr ->
        { pr with
            Type = fixType fix pr.Type
        }
        |> FsType.Property 
    | FsType.Param pr ->
        { pr with
            Type = fixType fix pr.Type
        }
        |> FsType.Param
    | FsType.Array ar ->
        fixType fix ar |> FsType.Array
    | FsType.Function fn ->
        { fn with
            TypeParameters = fn.TypeParameters |> List.map (fixType fix)
            Params = fn.Params |> List.map fixParam
            ReturnType = fixType fix fn.ReturnType
        }
        |> FsType.Function
    | FsType.FunctionType fn ->
        { fn with
            TypeParameters = fn.TypeParameters |> List.map (fixType fix)
            Params = fn.Params |> List.map fixParam
            ReturnType = fixType fix fn.ReturnType
        }
        |> FsType.FunctionType
    | FsType.Union un ->
        { un with
            Types = un.Types |> List.map (fixType fix)
        }
        |> FsType.Union
    | FsType.Alias al ->
        { al with
            Type = fixType fix al.Type
            TypeParameters = al.TypeParameters |> List.map (fixType fix)
        }
        |> FsType.Alias
    | FsType.Generic gn ->
        { gn with
            Type = fixType fix gn.Type
            TypeParameters = gn.TypeParameters |> List.map (fixType fix)
        }
        |> FsType.Generic
    | FsType.Tuple tp ->
        { tp with
            Types = tp.Types |> List.map (fixType fix)
        }
        |> FsType.Tuple
    | FsType.Module md ->
        fixModule md |> FsType.Module
     | FsType.File f ->
        { f with
            Modules = f.Modules |> List.map fixModule
        }
        |> FsType.File
    | _ -> tp
    |> fix

let fixTic (typeParameters: FsType list) (tp: FsType) =
    if typeParameters.Length = 0 then
        tp
    else
        let list = typeParameters |> List.map printType
        let set = list |> Set.ofList
        let fix (t: FsType): FsType =
            match asMapped t with
            | None -> t
            | Some s -> 
                if set.Contains s then
                    sprintf "'%s" s |> FsType.Mapped
                else t
        fixType fix tp

let addTicForGenericFunctions(md: FsModule): FsModule =
    { md with
        Types =
            md.Types |> List.map (fun tp ->
                match tp with
                | FsType.Interface it ->
                    { it with
                        Members = it.Members |> List.map (fun mbr ->
                            match asFunction mbr with
                            | None -> mbr
                            | Some fn -> fixTic fn.TypeParameters mbr
                        )
                    }
                    |> FsType.Interface
                | FsType.Class cl ->
                    { cl with
                        Members = cl.Members |> List.map (fun mbr ->
                            match asFunction mbr with
                            | None -> mbr
                            | Some fn -> fixTic fn.TypeParameters mbr
                        )
                    }
                    |> FsType.Class
                | _ -> tp
            )
    }

let fixNodeArray(md: FsModule): FsModule =

    let fix(tp: FsType): FsType =
        match asGeneric tp with
        | None -> tp
        | Some gn ->
            // printfn "gn: %A" gn 
            match asMapped gn.Type with
            | None -> tp
            | Some s ->
                if s.Equals "NodeArray" && gn.TypeParameters.Length = 1 then
                    gn.TypeParameters.[0] |> FsType.Array
                else tp

    { md with Types = md.Types |> List.map (fixType fix) }


let addTicForGenericTypes(md: FsModule): FsModule =
    { md with
        Types =
            md.Types |> List.map (fun tp ->
                match tp with
                | FsType.Interface it -> fixTic it.TypeParameters tp
                | FsType.Class cl -> fixTic cl.TypeParameters tp
                | FsType.Alias al -> fixTic al.TypeParameters tp
                | _ -> tp
            )
    }

let visitSourceFile(sf: SourceFile): FsFile =
    {
        Modules =
            getModules sf
            |> List.map visitModuleBlock
            |> mergeModules
            |> List.map createGlobals
            |> List.map addTicForGenericFunctions
            |> List.map addTicForGenericTypes
            |> List.map fixNodeArray
    }

let printType (tp: FsType): string =
    match tp with
    | FsType.Mapped s -> s
    | FsType.TODO -> "TODO"
    | FsType.Array at -> // TODO which is better
        sprintf "ResizeArray<%s>" (printType at)
        // sprintf "%s array" (printType at)
    | FsType.Union un ->
        if un.Types.Length > 6 then
            "obj"
        else if un.Types.Length = 1 then
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
            g.TypeParameters |> List.map printType |> String.concat " * " |> line.Add
            ">" |> line.Add
        line |> String.concat ""
    | FsType.FunctionType ft ->
        let line = ResizeArray()
        let typs = (ft.Params |> List.map (fun p -> p.Type)) @ [ft.ReturnType]
        "Func<" |> line.Add
        typs |> List.map printType |> String.concat ", " |> line.Add
        ">"|> line.Add
        line |> String.concat ""
    | FsType.Tuple tp ->
        let line = ResizeArray()
        tp.Types |> List.map printType |> String.concat " * " |> line.Add
        line |> String.concat ""
    | _ -> failwithf "unsupported printType %A" tp

let printFunction (f: FsFunction): string =
    let line = ResizeArray()
    // TODO can interface methods not have generics?
    // sprintf "abstract %s%s" (escapeWord f.Name) (printTypeParameters f.TypeParameters) |> line.Add
    sprintf "abstract %s" (escapeWord f.Name) |> line.Add
    let prms = 
        f.Params |> List.map(fun p ->
            sprintf "%s%s: %s" (if p.Optional then "?" else "") (escapeWord p.Name) (printType p.Type)
        )
    if prms.Length = 0 then
        sprintf ": unit" |> line.Add
    else
        sprintf ": %s" (prms |> String.concat " * ") |> line.Add
    sprintf " -> %s" (printType f.ReturnType) |> line.Add
    line |> String.concat ""

let printClassFunction (f: FsFunction): string =
    let line = ResizeArray()
    sprintf "member __.%s%s" (escapeWord f.Name) (printTypeParameters f.TypeParameters) |> line.Add
    let prms = 
        f.Params |> List.map(fun p ->
            sprintf "%s%s: %s" (if p.Optional then "?" else "") (escapeWord p.Name) (printType p.Type)
        )
    if prms.Length = 0 then
        sprintf "(" |> line.Add
    else
        sprintf "(%s" (prms |> String.concat ", ") |> line.Add
    sprintf "): %s = jsNative" (printType f.ReturnType) |> line.Add
    line |> String.concat ""

let printProperty (pr: FsProperty): string =
    sprintf "abstract %s: %s%s with get, set" (escapeWord pr.Name) (printType pr.Type) (if pr.Option then " option" else "")

let printTypeParameters (tps: FsType list): string =
    if tps.Length = 0 then ""
    else
        let line = ResizeArray()
        line.Add "<"
        tps |> List.map printType |> String.concat ", " |> line.Add
        line.Add ">"
        line |> String.concat ""

let upperFirstLetter (s: string): string =
    // sprintf "%c%s" (Char.ToUpper s.[0]) (s.Substring 1)
    sprintf "%s%s" (s.Substring(0,1).ToUpper()) (s.Substring 1)

let printFile (file: FsFile) =
    // TODO specify namespace
    // TODO customize open statements
    printfn "namespace rec Fable.Import"
    printfn "open System"
    printfn "open System.Text.RegularExpressions" // TODO why
    printfn "open Fable.Core"
    printfn "open Fable.Import.JS"

    for md in file.Modules do
        printfn ""
        printfn "module %s =" md.Name
        for i, tp in md.Types |> Seq.indexed do
            match tp with
            | FsType.Interface inf ->
                printfn ""
                printfn "    and [<AllowNullLiteral>] %s%s =" inf.Name (printTypeParameters inf.TypeParameters)
                let nLines = ref 0
                for ih in inf.Inherits do
                    printfn "        inherit %s" (printType ih)
                    incr nLines
                for mbr in inf.Members do
                    match mbr with
                    | FsType.Function f ->
                        printfn "        %s" (printFunction f)
                        incr nLines
                    | FsType.Property p ->
                        printfn "        %s" (printProperty p)
                        incr nLines
                    | _ -> ()
                if !nLines = 0 then
                    printfn "        interface end"
            | FsType.Class cl ->
                printfn ""
                printfn "    %s %s%s =" (if i = 0 then "type" else "and") cl.Name (printTypeParameters cl.TypeParameters)
                let nLines = ref 0
                for mbr in cl.Members do
                    match mbr with
                    | FsType.Function f ->
                        printfn "        %s" (printClassFunction f)
                        incr nLines
                    | _ -> ()
                if !nLines = 0 then
                    printfn "        class end"
            | FsType.Enum en ->
                printfn ""
                match en.Type with
                | FsEnumCaseType.Numeric ->
                    printfn "    and [<RequireQualifiedAccess>] %s =" en.Name
                    for cs in en.Cases do
                        let nm = cs.Name
                        let unm = upperFirstLetter nm
                        let line = ResizeArray()
                        if nm.Equals unm then
                            sprintf "        | %s" nm |> line.Add
                        else
                            sprintf "        | [<CompiledName \"%s\">] %s" nm unm |> line.Add
                        if cs.Value.IsSome then
                            sprintf " = %s" cs.Value.Value |> line.Add
                        printfn "%s" (line |> String.concat "")
                | FsEnumCaseType.String ->
                    printfn "    and [<StringEnum>] [<RequireQualifiedAccess>] %s =" en.Name
                    for cs in en.Cases do
                        let nm = cs.Name
                        let unm = upperFirstLetter nm
                        let line = ResizeArray()
                        if nm.Equals unm then
                            sprintf "        | %s" nm |> line.Add
                        else
                            sprintf "        | [<CompiledName \"%s\">] %s" nm unm |> line.Add
                        printfn "%s" (line |> String.concat "")
                | FsEnumCaseType.Unknown ->
                    printfn "        obj"
            | FsType.Alias al ->
                printfn ""
                printfn "    and %s%s =" al.Name (printTypeParameters al.TypeParameters)
                printfn "        %s" (printType al.Type)
            | _ -> ()

let reserved =
    [
        "atomic"
        "break"
        "checked"
        "component"
        "const"
        "constraint"
        "constructor"
        "continue"
        "eager"
        "event"
        "external"
        "fixed"
        "functor"
        "include"
        "measure"
        "method"
        "mixin"
        "object"
        "parallel"
        "params"
        "process"
        "protected"
        "pure"
        "sealed"
        "tailcall"
        "trait"
        "virtual"
        "volatile"
        "asr"
        "land"
        "lor"
        "lsl"
        "lsr"
        "lxor"
        "mod"
        "sig"
    ]
    |> Set.ofList

let keywords = 
    [
        "abstract"
        "and"
        "as"
        "assert"
        "base"
        "begin"
        "class"
        "default"
        "delegate"
        "do"
        "done"
        "downcast"
        "downto"
        "elif"
        "else"
        "end"
        "exception"
        "extern"
        "false"
        "finally"
        "for"
        "fun"
        "function"
        "global"
        "if"
        "in"
        "inherit"
        "inline"
        "interface"
        "internal"
        "lazy"
        "let"
        "match"
        "member"
        "module"
        "mutable"
        "namespace"
        "new"
        "null"
        "of"
        "open"
        "or"
        "override"
        "private"
        "public"
        "rec"
        "return"
        "sig"
        "static"
        "struct"
        "then"
        "to"
        "true"
        "try"
        "type"
        "upcast"
        "use"
        "val"
        "void"
        "when"
        "while"
        "with"
        "yield"
    ]
    |> Set.ofList

let escapeWord s =
    if reserved.Contains s || keywords.Contains s then
        sprintf "``%s``" s
    else
        s

let printTypeScriptFile() =
    let filePath = @"c:\Users\camer\fs\ts2fable\node_modules\typescript\lib\typescript.d.ts"
    let code = Fs.readFileSync(filePath).toString()
    let tsFile = ts.createSourceFile(filePath, code, ScriptTarget.ES2015, true)
    let fsFile = visitSourceFile tsFile
    printFile fsFile
    ()

printTypeScriptFile()