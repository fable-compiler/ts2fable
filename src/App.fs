module rec FableApp

open Fable.Core
open Fable.Core.JsInterop
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

type FsType =
    | Interface of FsInterface
    | Enum of FsEnum
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
        Name = ed.name.getText()
        Cases = ed.members |> List.ofArray |> List.map visitEnumCase
    }

let visitTypeElement(te: TypeElement): FsType =
    match te.kind with
    | SyntaxKind.IndexSignature -> FsType.TODO
    | SyntaxKind.MethodSignature -> FsType.TODO
    | SyntaxKind.PropertySignature -> FsType.TODO
    | SyntaxKind.CallSignature -> FsType.TODO
    | _ -> failwithf "unsupported TypeElement kind: %A" te.kind

let visitStatement(sd: Statement): FsType =
    match sd.kind with
    | SyntaxKind.InterfaceDeclaration ->
        let id = sd :?> InterfaceDeclaration
        visitInterface id |> FsType.Interface
    | SyntaxKind.EnumDeclaration ->
        let ed = sd :?> EnumDeclaration
        visitEnum ed |> FsType.Enum
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
            | Enum en ->
                printfn "    enum %s =" en.Name
                for cs in en.Cases do
                    match cs.Value with
                    | None -> printfn "        | %s" cs.Name
                    | Some v -> printfn "        | %s = %s" cs.Name v
            | TODO -> ()


let filePath = @"c:\Users\camer\fs\ts2fable\node_modules\typescript\lib\typescript.d.ts"
let code = Fs.readFileSync(filePath).toString()
let tsFile = ts.createSourceFile(filePath, code, ScriptTarget.ES2015, true)

let fsFile = visitSourceFile tsFile
printCodeFile fsFile
()