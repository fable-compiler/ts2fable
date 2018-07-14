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

let getFsFileOut (fsPath: string) (tsPaths: string list) (exports: string list) = 
    let options = jsOptions<Ts.CompilerOptions>(fun o ->
        o.target <- Some ScriptTarget.ES2015
        o.``module`` <- Some ModuleKind.CommonJS
    )
    let setParentNodes = true
    let host = ts.createCompilerHost(options, setParentNodes)
    let program = ts.createProgram(ResizeArray tsPaths, options, host)

    let exportFiles =
        if exports.Length = 0 then tsPaths
        else
            program.getSourceFiles()
            |> List.ofSeq
            |> List.map (fun sf -> sf.fileName)
            |> List.filter (stringContainsAny exports)

    for export in exportFiles do
        printfn "export %s" export

    let tsFiles = exportFiles |> List.map program.getSourceFile
    let checker = program.getTypeChecker()
    
    let moduleNameMap =
        program.getSourceFiles()
        |> Seq.map (fun sf -> sf.fileName, getJsModuleName sf.fileName)
        |> dict

    let fsFiles = tsFiles |> List.map (fun tsFile ->
        {
            FileName = tsFile.fileName
            ModuleName = moduleNameMap.[tsFile.fileName]
            Modules = []
        }
        |> readSourceFile checker tsFile
        |> transform
    )

    {
        // use the F# file name as the module namespace
        // TODO ensure valid name
        Namespace = path.basename(fsPath, path.extname(fsPath))
        Opens =
            [
                "System"
                "Fable.Core"
                "Fable.Import.JS"
            ]
        Files = fsFiles
    }
    |> fixFsFileOut

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