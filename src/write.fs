module rec ts2fable.Write
open ts2fable.Naming
open ts2fable.Print


open Fable.Core
open Fable.Core.JsInterop
open TypeScript
open TypeScript.Ts
open Node
open Yargs

open ts2fable.Naming
open ts2fable.Read
open ts2fable.Transform
open ts2fable.Write
open System.Collections.Generic
// This app has 3 main functions.
// 1. Read a TypeScript file into a syntax tree.
// 2. Fix the syntax tree.
// 3. Print the syntax tree to a F# file.

let transform (file: FsFile): FsFile =
    file
    |> removeInternalModules
    |> mergeModulesInFile
    |> aliasToInterfacePartly
    |> addConstructors
    |> fixThis
    |> fixNodeArray
    |> fixReadonlyArray
    |> fixDateTime
    |> fixStatic
    |> createIExports
    |> fixOverloadingOnStringParameters // fixEscapeWords must be after
    |> fixEnumReferences
    |> fixDuplicatesInUnion
    |> fixEscapeWords
    |> fixNamespace
    |> addTicForGenericFunctions // must be after fixEscapeWords
    |> addTicForGenericTypes
    // |> removeTodoMembers
    |> removeTypeParamsFromStatic
    |> removeDuplicateFunctions
    |> extractTypeLiterals // after fixEscapeWords
    |> addAliasUnionHelpers
    
let getFsFileOut (fsPath: string) (tsPaths: string list) = 
    let options = jsOptions<Ts.CompilerOptions>(fun o ->
        o.target <- Some ScriptTarget.ES2015
        o.``module`` <- Some ModuleKind.CommonJS
    )
    let setParentNodes = true
    let host = ts.createCompilerHost(options, setParentNodes)
    let program = ts.createProgram(ResizeArray tsPaths, options, host)
    let tsFiles = tsPaths |> List.map program.getSourceFile
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
    |> fixOpens
let emitFsFileOut fsPath (fsFileOut: FsFileOut) = 
    emitFsFileOutAsLines fsPath fsFileOut
    |> ignore

let emitFsFileOutAsLines (fsPath: string) (fsFileOut: FsFileOut) = 
    let file = fs.createWriteStream (!^fsPath)
    let lines = List []
    for line in printFsFile fsFileOut do
        lines.Add(line)
        file.write(sprintf "%s%c" line '\n') |> ignore
    file.``end``() 
    lines |> List.ofSeq
        
let writeFile (tsPaths: string list) (fsPath: string): unit =
    // printfn "writeFile %A %s" tsPaths fsPath

    let fsFileOut = getFsFileOut fsPath tsPaths 
    emitFsFileOut fsPath fsFileOut