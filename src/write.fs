module rec ts2fable.Write

open Fable.Core
open Fable.Core.JsInterop
open TypeScript
open TypeScript.Ts

open ts2fable.Naming
open ts2fable.Print
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
    |> extractGenericParameterDefaults
    |> fixTypesHasESKeywords
    |> extractTypesInGlobalModules
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
    |> removeDuplicateOptions
    |> extractTypeLiterals // after fixEscapeWords
    |> addAliasUnionHelpers
    
let stringContainsAny (b: string list) (a: string): bool =
    b |> List.exists a.Contains

let getFsFileOutWithText (text: string) = 
    let scriptTarget = ScriptTarget.ES2015
    let inputFileName = "module.d.ts"
    let sourceFile = ts.createSourceFile (inputFileName,text, scriptTarget, true)
    let tsFiles = [sourceFile]
    let options = jsOptions<Ts.CompilerOptions>(fun o ->
        o.target <- Some scriptTarget
        o.``module`` <- Some ModuleKind.CommonJS
    )

    let host =  jsOptions<CompilerHost>(fun o ->
        o.getSourceFile <- fun fileName -> if fileName.StartsWith "module" then Some sourceFile else None
        o.writeFile <- fun (_,_) -> ()
        o.getDefaultLibFileName <- fun _ -> "lib.d.ts"
        o.useCaseSensitiveFileNames <- fun _ -> false
        o.getCanonicalFileName <- id
        o.getCurrentDirectory <- fun _ -> ""
        o.getNewLine <- fun _ -> "\r\n"
        o.fileExists <- fun fileName -> inputFileName = fileName
        o.readFile <- fun _ -> Some ""
        o.directoryExists <- fun _ -> true
        o.getDirectories <- fun _ -> ResizeArray [] 
    )

    let program = ts.createProgram(ResizeArray [inputFileName], options, host)
    printfn "export %s" inputFileName

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
        Namespace = "ModuleName"
        Opens =
            [
                "System"
                "Fable.Core"
                "Fable.Import.JS"
            ]
        Files = fsFiles
    }
    |> fixFsFileOut
let emitFsFileOutAsText (fsFileOut: FsFileOut) = 
    let lines = List []
    for line in printFsFile "0.6.1" fsFileOut do
        lines.Add(line)
    lines |> String.concat "\n"


        