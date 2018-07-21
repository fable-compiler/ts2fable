
module internal ts2fable.Bridges

open Fable.Core
open Fable.Core.JsInterop
open TypeScript
open TypeScript.Ts

open ts2fable.Naming
open ts2fable.Print
open ts2fable.Read
open ts2fable.Transform
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
let scriptTarget = ScriptTarget.ES2015
type internal NodeBridge =
    {
        TsPaths: string list
        Exports: string list
        ReadText: string -> string
        NameSpace: string
    }
type internal WebBridge =
    {
        FileName: string
        SourceFile: SourceFile
    }

[<RequireQualifiedAccess>]
type internal Bridge =
    | Node of NodeBridge
    | Web of WebBridge

[<RequireQualifiedAccess>]
module internal Bridge =
    let private getTsPaths = 
        function
        | Bridge.Node nm -> nm.TsPaths
        | Bridge.Web w -> [w.FileName]

    let private getExports =
        function
        | Bridge.Node nm -> nm.Exports
        | Bridge.Web _ -> []
    let private getNamespace =      
        function
        | Bridge.Node nm -> nm.NameSpace
        | Bridge.Web _ -> "moduleName"  
    let private createProgram =
        let createDummy tsPaths (sourceFiles: SourceFile list) =
            let options = jsOptions<CompilerOptions>(fun o ->
                o.target <- Some scriptTarget
                o.``module`` <- Some ModuleKind.CommonJS
            )
            let host =  jsOptions<CompilerHost>(fun o ->
                o.getSourceFile <- fun fileName -> sourceFiles |> List.tryFind (fun sf -> sf.fileName = fileName)
                o.writeFile <- fun (_,_) -> ()
                o.getDefaultLibFileName <- fun _ -> "lib.d.ts"
                o.useCaseSensitiveFileNames <- fun _ -> false
                o.getCanonicalFileName <- id
                o.getCurrentDirectory <- fun _ -> ""
                o.getNewLine <- fun _ -> "\r\n"
                o.fileExists <- fun fileName -> List.contains fileName tsPaths
                o.readFile <- fun _ -> Some ""
                o.directoryExists <- fun _ -> true
                o.getDirectories <- fun _ -> ResizeArray [] 
            )
            ts.createProgram(ResizeArray tsPaths, options, host)    
        function
        | Bridge.Node nm -> 
            let exports,tsPaths,readText = nm.Exports,nm.TsPaths,nm.ReadText
            if List.isEmpty exports then 
                let sourceFiles = 
                    tsPaths |> List.map (fun tsPath ->
                        let text = tsPath |> readText
                        ts.createSourceFile (tsPath,text, scriptTarget, true)
                    )
                createDummy tsPaths sourceFiles
            else
                let options = jsOptions<Ts.CompilerOptions>(fun o ->
                    o.target <- Some scriptTarget
                    o.``module`` <- Some ModuleKind.CommonJS
                )
                let setParentNodes = true
                let host = ts.createCompilerHost(options, setParentNodes)
                ts.createProgram(ResizeArray tsPaths, options, host)    
        | Bridge.Web w -> 
            createDummy [w.FileName] [w.SourceFile]

    let getFsFileOut bridge = 
        let program = createProgram bridge
        let exports = getExports bridge
        let tsPaths = getTsPaths bridge
        let nameSpace = getNamespace bridge

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
            Namespace = nameSpace
            Opens =
                [
                    "System"
                    "Fable.Core"
                    "Fable.Import.JS"
                ]
            Files = fsFiles
        }
        |> fixFsFileOut
