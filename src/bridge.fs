
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

let scriptTarget = ScriptTarget.ES2015
type internal NodeBridge =
    {
        TsPaths: string list
        Exports: string list
        ReadText: string -> string
        NameSpace: string
        EnumerateFilesInSameDir: string -> seq<string>
        GetFsFileKind:  (NodeBridge * string) -> FsFileKind
        FixNamespace: FsFile -> FsFile
    }

[<RequireQualifiedAccess>]
module internal NodeBridge =
    let useExport nb f =
        match nb.TsPaths with
        | [tsPath] -> f tsPath
        | _ -> failwith "tspaths 's length must be 1 when with --exports option"

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

    let private fsFileKind tsPath =
        function
        | Bridge.Node nb ->
            nb.GetFsFileKind (nb,tsPath)

        | Bridge.Web w -> FsFileKind.Index
    let private getTsPaths =
        function
        | Bridge.Node nb -> nb.TsPaths
        | Bridge.Web w -> [w.FileName]

    let private getExportFiles =
        function
        | Bridge.Node nb ->
            if nb.Exports.Length = 0 then nb.TsPaths
            else
                NodeBridge.useExport nb (fun index ->
                    let tsPathsInExports (exports:string list) =
                        nb.EnumerateFilesInSameDir index |> List.ofSeq |> List.filter (fun f ->
                            f.EndsWith(".d.ts") && stringContainsAny f exports
                        )
                    tsPathsInExports nb.Exports
                )
        | Bridge.Web w -> [w.FileName]

    let private getNamespace =
        function
        | Bridge.Node nb -> nb.NameSpace
        | Bridge.Web _ -> "moduleName"

    let private createProgram bridge =
        let createDummy tsPaths (sourceFiles: SourceFile list) =
            let options = jsOptions<CompilerOptions>(fun o ->
                o.target <- Some scriptTarget
                o.``module`` <- Some ModuleKind.CommonJS
            )
            let host = jsOptions<CompilerHost>(fun o ->
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
        match bridge with
        | Bridge.Node nb ->
            let exports,readText = getExportFiles bridge,nb.ReadText
            let sourceFiles =
                exports |> List.map (fun tsPath ->
                    let text = tsPath |> readText
                    ts.createSourceFile (tsPath,text, scriptTarget, true)
                )
            createDummy exports sourceFiles
        | Bridge.Web w ->
            createDummy [w.FileName] [w.SourceFile]

    let private fixNameSpaceWithBridge bridge file =
        match bridge with
        | Bridge.Node nb -> nb.FixNamespace file
        | Bridge.Web _ -> fixNamespace file


    // This app has 3 main functions.
    // 1. Read a TypeScript file into a syntax tree.
    // 2. Fix the syntax tree.
    // 3. Print the syntax tree to a F# file.
    let private transform bridge (file: FsFile): FsFile =
        file
        // |> wrapperModuleForExtralFile
        |> removeInternalModules
        |> mergeModulesInFile
        // |> aliasToInterfacePartly
        |> removeKeyOfConstraint
        |> extractGenericParameterDefaults
        |> removeInvalidGenericConstraints
        |> fixTypesHasESKeywords
        |> extractTypesInGlobalModules
        |> addConstructors
        |> removePrivatesFromClasses
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
        |> fixNameSpaceWithBridge bridge
        |> addTicForGenericFunctions // must be after fixEscapeWords
        |> addTicForGenericTypes
        // |> removeTodoMembers
        |> removeTypeParamsFromStatic
        |> removeDuplicateFunctions
        |> removeDuplicateOptions
        |> extractTypeLiterals // after fixEscapeWords
        // |> addAliasUnionHelpers // disabled for Fable.Core 3.x
        |> removeDuplicateOptionsFromParameters
        |> fixFloatAlias

    let getFsFileOut bridge =
        let program = createProgram bridge
        let nameSpace = getNamespace bridge
        let exportFiles = getExportFiles bridge

        for export in exportFiles do
            printfn "export %s" export

        let tsFiles = exportFiles |> List.map program.getSourceFile |> List.choose id
        let checker = program.getTypeChecker()

        let moduleNameMap =
            program.getSourceFiles()
            |> Seq.map (fun sf -> sf.fileName, getJsModuleName sf.fileName)
            |> dict

        let fsFiles = tsFiles |> List.map (fun tsFile ->
            {
                Kind = fsFileKind tsFile.fileName bridge
                FileName = tsFile.fileName
                ModuleName = moduleNameMap.[tsFile.fileName]
                Modules = []
            }
            |> readSourceFile checker tsFile
            |> transform bridge
        )

        {
            // use the F# file name as the module namespace
            // TODO ensure valid name
            Namespace = nameSpace
            Opens =
                [
                    "System"
                    "Fable.Core"
                    "Fable.Core.JS"
                ]
            Files = fsFiles
            AbbrevTypes = []
        }
        |> fixFsFileOut

