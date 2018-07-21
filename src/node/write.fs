module rec ts2fable.node.Write

open Fable.Core
open Fable.Core.JsInterop
open TypeScript
open TypeScript.Ts
open Yargs
open Node

open ts2fable.Naming
open ts2fable.Print
open ts2fable.Read
open ts2fable.Transform
open ts2fable.Write
open System.Collections.Generic
open ts2fable.Syntax
open ts2fable.node.FileSystem
open ts2fable.Bridges
let getFsFileOut (fsPath: string) (tsPaths: string list) (exports: string list) = 
    {
        NameSpace = path.basename(fsPath, path.extname(fsPath))
        TsPaths = tsPaths
        Exports = exports
        ReadText = readText
    } |> Bridge.Node |> Bridge.getFsFileOut

let emitFsFileOut fsPath (fsFileOut: FsFileOut) = 
    emitFsFileOutAsLines fsPath fsFileOut
    |> ignore

let emitFsFileOutAsLines (fsPath: string) (fsFileOut: FsFileOut) = 
    let file = fs.createWriteStream (!^fsPath)
    let lines = List []
    for line in printFsFile Version.version fsFileOut do
        lines.Add(line)
        file.write(sprintf "%s%c" line '\n') |> ignore
    file.``end``() 
    lines |> List.ofSeq
        
let writeFile (tsPaths: string list) (fsPath: string) exports: unit =
    // printfn "writeFile %A %s" tsPaths fsPath
    let fsFileOut = getFsFileOut fsPath tsPaths exports
    emitFsFileOut fsPath fsFileOut