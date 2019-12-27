module ts2fable.Write

open TypeScript
open ts2fable.Print
open System.Collections.Generic
open ts2fable.Bridges

let getFsFileOutWithText (text: string) =
    let inputFileName = "module.d.ts"
    let sourceFile = ts.createSourceFile (inputFileName, text, scriptTarget, true)
    {
        FileName = inputFileName
        SourceFile = sourceFile
    }
    |> Bridge.Web |> Bridge.getFsFileOut

let emitFsFileOutAsText (fsFileOut: FsFileOut) =
    let lines = List []
    for line in printFsFile "0.7.0" fsFileOut do
        lines.Add(line)
    lines |> String.concat "\n"
