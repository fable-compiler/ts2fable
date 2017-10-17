module FableApp

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Node
open Fable.Import.ts

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
    let nodes = ResizeArray<ModuleDeclaration>()
    ts.forEachChildNode root (fun node -> 
        if node.kind = SyntaxKind.ModuleDeclaration then
            nodes.Add (node :?> ModuleDeclaration) )
    nodes

let getModuleName(mn: ModuleName): string =
    match mn with
    | U2.Case1 id -> id.getText()
    | U2.Case2 sl -> sl.getText()

let visitModule(md: ModuleDeclaration): FsModule =
    {
        Name = md.name |> getModuleName
    }

let visitFile(sf: SourceFile): FsFile =
    {
        Modules = getModules sf |> List.ofSeq |> List.map visitModule
    }

let printCodeFile (file: FsFile) =
    // TODO namespace
    // TODO open statements
    for md in file.Modules do
        printfn "module %s =" md.Name

let filePath = @"c:\Users\camer\fs\ts2fable\node_modules\typescript\lib\typescript.d.ts"
let code = Fs.readFileSync(filePath).toString()
let tsFile = ts.createSourceFile(filePath, code, ScriptTarget.ES2015, true)

let fsFile = visitFile tsFile
printCodeFile fsFile
