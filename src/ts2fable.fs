module ts2fable.App

open Fable.Core
open Fable.Import.JS
open Fable.Core.JsInterop
open TypeScript
open TypeScript.ts
open System.Collections.Generic
open Node

open ts2fable.Naming
open ts2fable.Read
open ts2fable.Transform
open ts2fable.Write

// This app has 3 main functions.
// 1. Read a TypeScript file into a syntax tree.
// 2. Fix the syntax tree.
// 3. Print the syntax tree to a F# file.

let transform (file: FsFile): FsFile =
    file
    |> removeInternalModules
    |> mergeModulesInFile
    |> addExportAssigments
    |> addConstructors
    |> fixThis
    |> fixNodeArray
    |> fixReadonlyArray
    |> fixDateTime
    |> fixStatic
    |> createIExports
    |> moveDeclaredVariables
    |> fixOverloadingOnStringParameters // fixEscapeWords must be after
    |> fixEnumReferences
    |> fixDuplicatesInUnion
    |> fixEscapeWords
    |> addTicForGenericFunctions // must be after fixEscapeWords
    |> addTicForGenericTypes
    |> removeTodoMembers
    |> removeTypeParamsFromStatic
    |> removeDuplicateFunctions

let writeFile (tsPaths: string list) (fsPath: string): unit =

    // TODO ensure the files exist
    // for tsPath in tsPaths do
    //     path.existsSync(tsPath)

    let options = jsOptions<ts.CompilerOptions>(fun o ->
        o.target <- Some ScriptTarget.ES2015
        o.``module`` <- Some ModuleKind.CommonJS
    )
    let setParentNodes = true
    let host = ts.createCompilerHost(options, setParentNodes)
    let program = ts.createProgram(List tsPaths, options, host)
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
        |> readSourceFile checker tsFiles
        |> transform
    )

    let fsFileOut: FsFileOut =
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

    let file = fs.createWriteStream (PathLike.ofString fsPath)
    for line in printFsFile fsFileOut do
        file.write(sprintf "%s%c" line '\n') |> ignore
    file.``end``()

let argv = ``process``.argv |> List.ofSeq

type PackageJson =
    {
        version: string
    }

// if run via `dotnet fable npm-build` or `dotnet fable npm-start`
// TODO `dotnet fable npm-build` doesn't wait for the test files to finish writing
if argv |> List.exists (fun s -> s = "splitter.config.js") then // run from build
    printfn "ts.version: %s" ts.version
    writeFile ["node_modules/izitoast/dist/izitoast/izitoast.d.ts"] "test-compile/IziToast.fs"
    writeFile ["node_modules/typescript/lib/typescript.d.ts"] "test-compile/TypeScript.fs"
    writeFile ["node_modules/electron/electron.d.ts"] "test-compile/Electron.fs"
    writeFile ["node_modules/@types/react/index.d.ts"] "test-compile/React.fs"
    writeFile ["node_modules/@types/node/index.d.ts"] "test-compile/Node.fs"
    writeFile ["node_modules/@types/mocha/index.d.ts"] "test-compile/Mocha.fs"
    writeFile ["node_modules/@types/chai/index.d.ts"] "test-compile/Chai.fs"
    writeFile ["node_modules/chalk/types/index.d.ts"] "test-compile/Chalk.fs"
    writeFile
        [   "node_modules/@types/google-protobuf/index.d.ts"
            "node_modules/@types/google-protobuf/google/protobuf/empty_pb.d.ts"
        ]
        "test-compile/Protobuf.fs"

    // files that have TODOs
    // writeFile ["node_modules/@types/jquery/index.d.ts"] "test-compile/JQuery.fs"
    // writeFile ["node_modules/typescript/lib/lib.es2015.promise.d.ts"] "test-compile/Promise.fs"

    printfn "done writing test-compile files"

else
    let packageJson = fs.readFileSync(PathLike.ofString "package.json" |> U2.Case1, "utf8" |> U2.Case2) |> ofJson<PackageJson>
    printfn "ts2fable %s" packageJson.version

    let tsfiles = argv |> List.filter (fun s -> s.EndsWith ".ts")
    let fsfile = argv |> List.tryFind (fun s -> s.EndsWith ".fs")
    
    match tsfiles.Length, fsfile with
    | 0, _ -> failwithf "Please provide the path to a TypeScript definition file"
    | _, None -> failwithf "Please provide the path to the F# file to be written "
    | _, Some fsf -> writeFile tsfiles fsf