module ts2fable.App

open Fable.Core
open Fable.Import.JS
open Fable.Core.JsInterop
open Fable.Import.Node
open Fable.Import.TypeScript
open Fable.Import.TypeScript.ts
open System.Collections.Generic

open ts2fable.Read
open ts2fable.Transform
open ts2fable.Write

// This app has 3 main functions.
// 1. Read a TypeScript file into a syntax tree.
// 2. Fix the syntax tree.
// 3. Print the syntax tree to a F# file.

let readSourceFile (checker: TypeChecker) (ns: string) (sf: SourceFile): FsFile =
    let modules = List()

    let gbl: FsModule =
        {
            Name = ""
            Types = sf.statements |> List.ofSeq |> List.map (readStatement checker)
        }
    modules.Add gbl

    let opens = 
        [
            "System"
            // "System.Text.RegularExpressions"
            "Fable.Core"
            "Fable.Import.JS"
        ]
    {
        Name = ns
        Opens = opens
        Modules = modules |> List.ofSeq
    }
    |> mergeModulesInFile
    // |> fixImport [ns]
    |> fixThis
    |> fixNodeArray
    |> fixDateTime
    |> fixOpens
    |> fixStatic
    |> createIExports
    |> fixOverloadingOnStringParameters // fixEscapeWords must be after
    |> fixEnumReferences
    |> fixDuplicatesInUnion
    |> fixEscapeWords
    |> addTicForGenericFunctions // must be after fixEscapeWords
    |> addTicForGenericTypes
    |> removeTodoMembers
    |> removeTypeParamsFromStatic

let ts: ts.IExports = importAll "typescript"

let writeFile tsPath (fsPath: string): unit =

    let options = jsOptions<ts.CompilerOptions>(fun o ->
        o.target <- Some ScriptTarget.ES2015
        o.``module`` <- Some ModuleKind.CommonJS
    )
    let setParentNodes = true
    let host = ts.createCompilerHost(options, setParentNodes)
    let program = ts.createProgram(List [tsPath], options, host)
    let tsFile = program.getSourceFile tsPath
    let checker = program.getTypeChecker()

    // use the F# file name as the module namespace
    let path = Fable.Import.Node.Exports.Path
    let ns = path.basename(fsPath, path.extname(fsPath)) // TODO ensure valid name

    let fsFile = readSourceFile checker ns tsFile
    let file = Fs.createWriteStream fsPath
    for line in printFsFile fsFile do
        file.write(sprintf "%s%c" line '\n') |> ignore
    file.``end``()

let p = Fable.Import.Node.Globals.``process``
let argv = p.argv |> List.ofSeq

// if run via `dotnet fable npm-build` or `dotnet fable npm-start`
// TODO `dotnet fable npm-build` doesn't wait for the test files to finish writing
if argv |> List.exists (fun s -> s = "splitter.config.js") then // run from build
    printfn "ts.version: %s" ts.version
    writeFile "node_modules/izitoast/dist/izitoast/izitoast.d.ts" "test-compile/Fable.Import.IziToast.fs"
    writeFile "node_modules/typescript/lib/typescript.d.ts" "test-compile/Fable.Import.TypeScript.fs"
    writeFile "node_modules/electron/electron.d.ts" "test-compile/Fable.Import.Electron.fs"
    writeFile "node_modules/@types/react/index.d.ts" "test-compile/Fable.Import.React.fs"
    writeFile "node_modules/@types/node/index.d.ts" "test-compile/Fable.Import.Node.fs"
    writeFile "node_modules/@types/mocha/index.d.ts" "test-compile/Fable.Import.Mocha.fs"
    writeFile "node_modules/@types/chai/index.d.ts" "test-compile/Fable.Import.Chai.fs"
    writeFile "node_modules/chalk/types/index.d.ts" "test-compile/Fable.Import.Chalk.fs"
    writeFile "node_modules/@types/google-protobuf/index.d.ts" "test-compile/Fable.Import.Protobuf.fs"

    // files that have TODOs
    // writeFile "node_modules/@types/jquery/index.d.ts" "test-compile/Fable.Import.JQuery.fs"
    // writeFile "node_modules/typescript/lib/lib.es2015.promise.d.ts" "test-compile/Fable.Import.Promise.fs"

    printfn "done writing test-compile files"

else
    let tsfile = argv |> List.tryFind (fun s -> s.EndsWith ".ts")
    let fsfile = argv |> List.tryFind (fun s -> s.EndsWith ".fs")
    
    match tsfile, fsfile with
    | None, _ -> failwithf "Please provide the path to a TypeScript definition file"
    | _, None -> failwithf "Please provide the path to the F# file to be written "
    | Some tsf, Some fsf -> writeFile tsf fsf