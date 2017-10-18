module FableApp

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
    }

type FsType =
    | Interface of FsInterface

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

let getModules(root: Node) =
    let nodes = ResizeArray<ModuleBlock>()
    ts.forEachChildNodeRec root (fun node -> 
        if node.kind = SyntaxKind.ModuleBlock then
            nodes.Add (node :?> ModuleBlock) )
    nodes |> List.ofSeq

let getTypes(root: Node) =
    let nodes = ResizeArray<DeclarationStatement>()
    let kinds = [ SyntaxKind.InterfaceDeclaration ] |> Set.ofList
    ts.forEachChildNode root (fun node ->
        // printfn "kind: %A" node.kind
        if kinds.Contains node.kind then
            nodes.Add (node :?> DeclarationStatement) )
    nodes |> List.ofSeq

let getModuleName(mb: ModuleBlock): string =
    match mb.parent with
    | Some md ->
        match md.name with
        | U2.Case1 id -> id.getText()
        | U2.Case2 sl -> sl.getText()
    | None -> "NoModuleName"

let visitType(sd: DeclarationStatement): FsType =
    match sd.kind with
    | SyntaxKind.InterfaceDeclaration ->
        let id = sd :?> InterfaceDeclaration
        {
            Name =
                match id.name with
                | Some v ->
                    match v with
                    | U3.Case1 id -> id.getText()
                    | U3.Case2 sl -> sl.getText()
                    | U3.Case3 nl -> nl.getText()
                | None -> "NoInterfaceName"
            Inherit = None
        }
        |> FsType.Interface
    | _ -> failwithf "unsupported type declaration kind: %A" sd.kind

let visitModule(mb: ModuleBlock): FsModule =
    {
        Name = mb |> getModuleName
        Types = getTypes mb |> List.map visitType
    }

let visitFile(sf: SourceFile): FsFile =
    {
        Modules = getModules sf |> List.map visitModule
    }

let printCodeFile (file: FsFile) =
    // TODO namespace
    // TODO open statements
    for md in file.Modules do
        printfn "module %s =" md.Name

        for tp in md.Types do
            match tp with
            | Interface inf ->
                printfn "    interface %s =" inf.Name

let filePath = @"c:\Users\camer\fs\ts2fable\node_modules\typescript\lib\typescript.d.ts"
let code = Fs.readFileSync(filePath).toString()
let tsFile = ts.createSourceFile(filePath, code, ScriptTarget.ES2015, true)

let fsFile = visitFile tsFile
printCodeFile fsFile
