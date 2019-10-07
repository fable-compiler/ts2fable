module ts2fable.node.Write

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
open ts2fable.node.Transform
let getFsFileOut (fsPath: string) (tsPaths: string list) (exports: string list) = 
    {
        FixNamespace = fixNamespace
        GetFsFileKind =
            fun (nb,tsPath) ->
                if nb.Exports.Length = 0 then FsFileKind.Index
                else
                    NodeBridge.useExport nb (fun index ->
                        if nb.TsPaths |> List.contains tsPath then 
                            FsFileKind.Index
                        else 
                            let dir = path.dirname index
                            let relativePath = path.relative (dir,tsPath)                                               
                            let relativePathWithOutExtension = 
                                relativePath.Substring(0,relativePath.LastIndexOf(".d.ts"))      
                            FsFileKind.Extra relativePathWithOutExtension
                    )        
        EnumerateFilesInSameDir = 
            fun indexFile -> 
                let dir = path.dirname indexFile
                enumerateFiles [dir]
        NameSpace = path.basename(fsPath, path.extname(fsPath))
        TsPaths = tsPaths
        Exports = exports
        ReadText = readText

    } |> Bridge.Node |> Bridge.getFsFileOut



let emitFsFileOutAsLines (fsPath: string) (fsFileOut: FsFileOut) =
    let lines = printFsFile Version.version fsFileOut
    let fullText = lines |> String.concat "\n"
    fs.writeFileSync(!! fsPath, !! (fullText + "\n"))
    lines |> List.ofSeq
    
let emitFsFileOut fsPath (fsFileOut: FsFileOut) = 
    emitFsFileOutAsLines fsPath fsFileOut
    |> ignore
let writeFile (tsPaths: string list) (fsPath: string) exports: unit =
    // printfn "writeFile %A %s" tsPaths fsPath
    let fsFileOut = getFsFileOut fsPath tsPaths exports
    emitFsFileOut fsPath fsFileOut