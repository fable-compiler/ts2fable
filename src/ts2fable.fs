module rec ts2fable.App

open Fable.Core
open Fable.Import
open Fable.Import.Node
open Fable.Import.ts
open System.Collections.Generic

// our simplified F# AST
// some names inspired by the actual F# AST:
// https://github.com/fsharp/FSharp.Compiler.Service/blob/master/src/fsharp/ast.fs

type FsInterface =
    {
        Name: string
        TypeParameters: FsType list
        Inherits: FsGenericType list
        Members: FsType list
    }

type FsEnumCase =
    {
        Name: string
        Value: string option
    }

type FsEnum =
    {
        Name: string
        Cases: FsEnumCase list
    }

type FsParam =
    {
        Name: string
        Optional: bool
        Type: FsType
    }

type FsFunction =
    {
        Name: string
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
    }

[<RequireQualifiedAccess>]
type FsType =
    | Interface of FsInterface
    | Enum of FsEnum
    | Method of FsFunction
    | Property of FsProperty
    | Param of FsParam
    | Array of FsType
    | TODO
    | None // when it is not set
    | Mapped of string
    | Function of FsFunction
    | Union of FsUnion
    | Alias of FsAlias
    | Generic of FsGenericType

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
    {
        Name = em.name |> getPropertyName
        Value = 
            match em.initializer with
            | None -> None
            | Some ep ->
                match ep.kind with
                | SyntaxKind.NumericLiteral ->
                    let nl = ep :?> NumericLiteral
                    Some nl.text
                | _ -> None // TODO TypeScript string based enums #17
                // https://github.com/fable-compiler/ts2fable/issues/17
    }

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
                                    tps |> List.ofArray |> List.map (fun tp ->
                                        tp.getText() |> FsType.Mapped // TODO
                                    )
                        }
                    )
                )
                |> List.concat
        Members = id.members |> List.ofArray |> List.map visitTypeElement
        TypeParameters = 
            match id.typeParameters with
            | None -> []
            | Some tps ->
                tps |> List.ofArray |> List.map (fun tp ->
                    tp.name.text |> FsType.Mapped
                )
    }

let visitEnum(ed: EnumDeclaration): FsEnum =
    {
        Name = ed.name.text
        Cases = ed.members |> List.ofArray |> List.map visitEnumCase
    }

let rec visitTypeNode(t: TypeNode): FsType =
    match t.kind with
    | SyntaxKind.StringKeyword -> FsType.Mapped "string"
    | SyntaxKind.FunctionType ->
        // var cbParams = type.parameters.map(function (x) {
        //     return x.dotDotDotToken ? "obj" : getType(x.type);
        // }).join(", ");
        // return "Func<" + (cbParams || "unit") + ", " + getType(type.type) + ">";
        FsType.Mapped "fun" // TODO
    | SyntaxKind.TypeReference ->
        let tr = t :?> TypeReferenceNode
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
        // return type.elementTypes.map(getType).join(" * ");
        FsType.Mapped "tupple" // TODO
    | SyntaxKind.SymbolKeyword -> FsType.Mapped "Symbol"
    | SyntaxKind.ThisType -> FsType.Mapped "TODO_ThisType"
    | SyntaxKind.TypePredicate -> FsType.Mapped "TODO_TypePredicate"
    | SyntaxKind.TypeLiteral -> FsType.Mapped "TODO_TypeLiteral"
    | SyntaxKind.IntersectionType ->
        // FsType.Mapped "TODO_IntersectionType"
        FsType.Mapped "obj"
    | SyntaxKind.IndexedAccessType -> FsType.Mapped "TODO_IndexedAccessType"
    | SyntaxKind.TypeQuery -> FsType.Mapped "TODO_TypeQuery"
    | SyntaxKind.LiteralType -> FsType.Mapped "TODO_LiteralType"
    | SyntaxKind.ExpressionWithTypeArguments ->
        let eta = t :?> ExpressionWithTypeArguments
        match eta.expression.kind with
        | SyntaxKind.Identifier ->
            let id = eta.expression :?> Identifier
            FsType.Mapped id.text
        | _ -> failwithf "unsupported TypeNode ExpressionWithTypeArguments kind: %A" eta.expression.kind
    | SyntaxKind.ParenthesizedType -> FsType.Mapped "TODO_ParenthesizedType"
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
        visitMethodSignature (te :?> MethodSignature) |> FsType.Method
    | SyntaxKind.PropertySignature ->
        visitPropertySignature (te :?> PropertySignature) |> FsType.Property
    | SyntaxKind.CallSignature -> FsType.TODO
    | _ -> failwithf "unsupported TypeElement kind: %A" te.kind

let visitAliasDeclaration(d: TypeAliasDeclaration): FsAlias =
    {
        Name = d.name.text
        Type = d.``type`` |> visitTypeNode
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

let mergeModules(modules: FsModule list): FsModule list =
    let index = Dictionary<string,int>()
    let list = List<FsModule>()
    for b in modules do
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

let visitSourceFile(sf: SourceFile): FsFile =
    {
        Modules = getModules sf |> List.map visitModuleBlock |> mergeModules
    }

// TODO may need to pass in a list of generic types
let printType (tp: FsType): string =
    match tp with
    | FsType.Mapped s -> s
    | FsType.TODO -> "TODO"
    | FsType.Array at ->
        match at with
        | FsType.Mapped s -> sprintf "ResizeArray<%s>" s
        | FsType.Generic g -> sprintf "TODO_ArrayGeneric"
        | _ -> failwithf "unsupported Array ReturnType %A" tp
    | FsType.Union un ->
        if un.Types.Length > 6 then
            "obj"
        else if un.Types.Length = 1 then
            sprintf "%s%s" (printType un.Types.[0]) (if un.Option then " option" else "")
        else
            let line = ResizeArray()
            sprintf "U%d<" un.Types.Length |> line.Add
            un.Types |> List.map printType |> List.map (sprintf "%s") |> String.concat ", " |> line.Add
            sprintf ">%s" (if un.Option then " option" else "") |> line.Add
            line |> String.concat ""
    | FsType.Generic g ->
        let line = ResizeArray()
        sprintf "%s<" (printType g.Type) |> line.Add
        g.TypeParameters |> List.map printType |> List.map (sprintf "%s") |> String.concat " * " |> line.Add
        ">" |> line.Add
        line |> String.concat ""
    | _ -> failwithf "unsupported printType %A" tp

let printFunction (m: FsFunction): string =
    let line = ResizeArray()
    sprintf "abstract %s" (escapeWord m.Name) |> line.Add
    let prms = 
        m.Params |> List.map(fun p ->
            match p.Type with
            | FsType.Mapped t ->
                sprintf "%s%s: %s" (if p.Optional then "?" else "") (escapeWord p.Name) t
            | _ -> sprintf "TODO %A" p.Type
        )
    if prms.Length = 0 then
        sprintf ": unit" |> line.Add
    else
        sprintf ": %s" (prms |> String.concat " * ") |> line.Add
    sprintf " -> %s" (printType m.ReturnType) |> line.Add
    line |> String.concat ""

let printProperty (pr: FsProperty): string =
    sprintf "abstract %s: %s%s with get, set" (escapeWord pr.Name) (printType pr.Type) (if pr.Option then " option" else "")

let printTypeParameters (tps: FsType list): string =
    if tps.Length = 0 then ""
    else
        let line = ResizeArray()
        line.Add "<"
        tps |> List.map printType |> List.map (sprintf "'%s") |> String.concat ", " |> line.Add
        line.Add ">"
        line |> String.concat ""

let printCodeFile (file: FsFile) =
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
        for tp in md.Types do
            match tp with
            | FsType.Interface inf ->
                printfn ""
                printfn "    and [<AllowNullLiteral>] %s%s =" inf.Name (printTypeParameters inf.TypeParameters)
                let nLines = ref 0
                for ih in inf.Inherits do
                    printfn "        inherit %s%s" (printType ih.Type) (printTypeParameters ih.TypeParameters)
                    incr nLines
                for mbr in inf.Members do
                    match mbr with
                    | FsType.Method m ->
                        printfn "        %s" (printFunction m)
                        incr nLines
                    | FsType.Property p ->
                        printfn "        %s" (printProperty p)
                        incr nLines
                    | _ -> ()
                if !nLines = 0 then
                    printfn "        interface end"
            | FsType.Enum en ->
                printfn ""
                printfn "    and %s =" en.Name
                for cs in en.Cases do
                    match cs.Value with
                    | None -> printfn "        | %s" cs.Name
                    | Some v -> printfn "        | %s = %s" cs.Name v
            | FsType.Alias al ->
                printfn ""
                printfn "    and %s =" al.Name
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

let printFile() =
    let filePath = @"c:\Users\camer\fs\ts2fable\node_modules\typescript\lib\typescript.d.ts"
    let code = Fs.readFileSync(filePath).toString()
    let tsFile = ts.createSourceFile(filePath, code, ScriptTarget.ES2015, true)
    let fsFile = visitSourceFile tsFile
    printCodeFile fsFile
    ()

printFile()