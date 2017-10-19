module rec ts2fable.App

open Fable.Core
open Fable.Import
open Fable.Import.Node
open Fable.Import.ts

// our simplified F# AST
// some names inspired by the actual F# AST:
// https://github.com/fsharp/FSharp.Compiler.Service/blob/master/src/fsharp/ast.fs

type FsInterface =
    {
        Name: string
        Inherit: string option
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
        Type: string option
        IsArray: bool
    }

type FsMethod =
    {
        Name: string
        Params: FsParam list
    }

type FsProperty =
    {
        Name: string
    }

type FsType =
    | Interface of FsInterface
    | Enum of FsEnum
    | Method of FsMethod
    | Property of FsProperty
    | TODO

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
        Inherit = None
        Members = id.members |> List.ofArray |> List.map visitTypeElement
    }

let visitEnum(ed: EnumDeclaration): FsEnum =
    {
        Name = ed.name.text
        Cases = ed.members |> List.ofArray |> List.map visitEnumCase
    }

let visitParameterDeclaration(pd: ParameterDeclaration): FsParam =
    {
        Name = pd.name |> getBindingyName
        Type =
            match pd.``type`` with
            | None -> None
            | Some t ->
                match t.kind with
                | SyntaxKind.StringKeyword -> Some "string"
                | SyntaxKind.FunctionType -> Some "fun" // TODO
                | SyntaxKind.TypeReference -> Some (t.getText())
                | SyntaxKind.ArrayType -> Some "array" // TODO
                | SyntaxKind.NumberKeyword -> Some "int"
                | SyntaxKind.BooleanKeyword -> Some "bool"
                | SyntaxKind.UnionType -> Some "union" // TODO
                | SyntaxKind.AnyKeyword -> Some "obj"
                | _ -> failwithf "unsupported ParameterDeclaration TypeNode kind: %A" t.kind
        IsArray = false // TODO
    }

let visitMethodSignature(ms: MethodSignature): FsMethod =
    {
        Name = ms.name |> getPropertyName
        Params = ms.parameters |> List.ofArray |> List.map visitParameterDeclaration
    }

let visitPropertySignature(ps: PropertySignature): FsProperty=
    {
        Name = ps.name |> getPropertyName
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

let visitStatement(sd: Statement): FsType =
    match sd.kind with
    | SyntaxKind.InterfaceDeclaration ->
        visitInterface (sd :?> InterfaceDeclaration) |> FsType.Interface
    | SyntaxKind.EnumDeclaration ->
        visitEnum (sd :?> EnumDeclaration) |> FsType.Enum
    | SyntaxKind.TypeAliasDeclaration -> FsType.TODO
    | SyntaxKind.ClassDeclaration -> FsType.TODO
    | SyntaxKind.VariableStatement -> FsType.TODO
    | SyntaxKind.FunctionDeclaration -> FsType.TODO
    | SyntaxKind.ModuleDeclaration -> FsType.TODO
    | _ -> failwithf "unsupported Statement kind: %A" sd.kind

let visitModuleBlock(mb: ModuleBlock): FsModule =
    {
        Name = mb |> getModuleName
        Types = mb.statements |> List.ofArray |> List.map visitStatement
    }

let visitSourceFile(sf: SourceFile): FsFile =
    {
        Modules = getModules sf |> List.map visitModuleBlock
    }

let printCodeFile (file: FsFile) =
    // TODO namespace
    // TODO open statements
    for md in file.Modules do
        printfn ""
        printfn "module %s =" md.Name
        for tp in md.Types do
            printfn ""
            match tp with
            | Interface inf ->
                printfn "    interface %s =" inf.Name
                for mbr in inf.Members do
                    match mbr with
                    | Method m ->
                        let line = ResizeArray()
                        sprintf "        abstract %s" m.Name |> line.Add
                        let prms = 
                            m.Params |> List.map(fun p ->
                                match p.Type with
                                | None -> sprintf "%s" p.Name
                                | Some t -> sprintf "%s: %s" p.Name t
                            )
                        sprintf ": %s" (prms |> String.concat " * ") |> line.Add
                        printfn "%s" (line |> String.concat "")
                    | Property p ->
                        printfn "        prop %s" p.Name 
                    | _ -> ()
            | Enum en ->
                printfn "    enum %s =" en.Name
                for cs in en.Cases do
                    match cs.Value with
                    | None -> printfn "        | %s" cs.Name
                    | Some v -> printfn "        | %s = %s" cs.Name v
            | _ -> ()


let filePath = @"c:\Users\camer\fs\ts2fable\node_modules\typescript\lib\typescript.d.ts"
let code = Fs.readFileSync(filePath).toString()
let tsFile = ts.createSourceFile(filePath, code, ScriptTarget.ES2015, true)

let fsFile = visitSourceFile tsFile
printCodeFile fsFile
()