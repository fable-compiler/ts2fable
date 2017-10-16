module FableApp

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Import.Node
open Fable.Import.ts

type Fable.Import.ts.Globals with
    member x.forEachChildNode (node: Node) cbNode =
        let func = System.Func<_,_>(fun (node:Node) -> cbNode node; None)
        x.forEachChild<unit>(node, func) |> ignore

    member x.forEachChildNodeRec (root: Node) cbNode =
        cbNode root
        x.forEachChildNode root (fun node -> x.forEachChildNodeRec node cbNode)

let getAllInterfaces(root: Node) =
    let nodes = ResizeArray<Node>()

    ts.forEachChildNodeRec root (fun node -> 
        if node.kind = SyntaxKind.InterfaceDeclaration then
            nodes.Add node )

    for n in nodes do
        let ifd = n :?> InterfaceDeclaration
        let name =
            match ifd.name with
            | Some name ->
                match name with
                | U3.Case1 id -> id.getText()
                | U3.Case2 sl -> sl.getText()
                | U3.Case3 nl -> nl.getText()
            | None -> ""
        printfn "interface: %s" (name)
    
let filePath = @"c:\Users\camer\fs\ts2fable\node_modules\typescript\lib\typescript.d.ts"
let code = Fs.readFileSync(filePath).toString()
let sourceFile = ts.createSourceFile(filePath, code, ScriptTarget.ES2015, true)

getAllInterfaces sourceFile