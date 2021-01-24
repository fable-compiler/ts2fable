module ts2fable.node.Write

open Fable.Core
open Fable.Core.JsInterop
open TypeScript
open TypeScript.Ts
open Node.Api

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
    }
    |> Bridge.Node
    |> Bridge.getFsFileOut

let emitFsFileOutAsLines (fsPath: string) (fsFileOut: FsFileOut) =
    let file = fs.createWriteStream (fsPath)
    let lines = List []
    for line in printFsFile Version.version fsFileOut do
        // line might contain new line:
        // ```
        // [<Obsolete("Foo Bar
        // baz")>]
        // ```
        // -> returned in single line
        // -> comparing with expected file fails:
        // ```
        // - "[<Obsolete(\"Foo Bar"
        // - "baz\")>]"
        // + "[<Obsolete(\"Foo Bar\nbaz\")>]"
        // ```
        // (`+`: actual; `-`: expected)

        if line.Contains "\n" then
            lines.AddRange(line.Split [|'\n'|])
        else
            lines.Add(line)
        file.write (sprintf "%s%c" line '\n') |> ignore
    file.``end``()
    lines |> List.ofSeq

let emitFsFileOut fsPath (fsFileOut: FsFileOut) =
    emitFsFileOutAsLines fsPath fsFileOut
    |> ignore

let writeFile (tsPaths: string list) (fsPath: string) exports: unit =
    // printfn "writeFile %A %s" tsPaths fsPath
    let fsFileOut = getFsFileOut fsPath tsPaths exports
    emitFsFileOut fsPath fsFileOut
