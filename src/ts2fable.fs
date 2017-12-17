module ts2fable.App

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
open Yargs.Yargs
open Node.Fs
open Mkdirp
open Fable.PowerPack
open Node.Fs
open Node.Fs
// This app has 3 main functions.
// 1. Read a TypeScript file into a syntax tree.
// 2. Fix the syntax tree.
// 3. Print the syntax tree to a F# file.

let transform (file: FsFile): FsFile =
    file
    |> removeInternalModules
    |> mergeModulesInFile
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
    
let inline writeFileInternal (tsPaths: string list) (fsPath: string) (dependent:string Option): unit =    // printfn "writeFile %A %s" tsPaths fsPath 

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
        |> readSourceFile checker tsFiles
        |> transform
    )

    let fsFileOut: FsFileOut =
        let nameSpace =
            match dependent with 
            | Some subPath -> 
                let fileName = fsFiles.[0].FileName
                let moduleName = fsFiles.[0].ModuleName
                automaticNamespace fileName subPath moduleName
            | None -> path.basename(fsPath, path.extname(fsPath))
        {
            // use the F# file name as the module namespace
            // TODO ensure valid name
            // Namespace = path.basename(fsPath, path.extname(fsPath))
            Namespace = nameSpace
            Opens =
                [
                    "System"
                    "Fable.Core"
                    "Fable.Import.JS"
                ]
            Files = fsFiles
        }
        |> fixOpens

    let file = fs.createWriteStream (!^fsPath)
    for line in printFsFile fsFileOut do
        file.write(sprintf "%s%c" line '\n') |> ignore
    file.``end``()

let writeFile (tsPaths: string list) (fsPath: string) :unit=
    writeFileInternal tsPaths fsPath None
let writeFile2 (tsPaths: string list) (fsPath: string) (subPath:string):unit=
    writeFileInternal tsPaths fsPath  (Some subPath)
let (|TsFile2FsFile|TsDirectory2FsDirectory|) (files:string list)=
    let isfile (s:string) = s.EndsWith ".ts" ||  s.EndsWith ".fs"
    let isDirectory (s:string) = path.extname s = ""
    let dir2Dir (files:string list)=
        if (files |> Seq.forall isDirectory) && files.Length = 2 
            then Some (files.Head,files.[1])
        else None
    files 
    |> Seq.forall isfile
    |> function 
       | true -> TsFile2FsFile
       | false -> files 
                |> dir2Dir
                |> function
                   | Some dirs -> TsDirectory2FsDirectory dirs
                   | None -> failwith "incorrent input files"
let argv = ``process``.argv |> List.ofSeq

// if run via `dotnet fable npm-build` or `dotnet fable npm-start`
// TODO `dotnet fable npm-build` doesn't wait for the test files to finish writing
if argv |> List.exists (fun s -> s = "splitter.config.js") then // run from build
    printfn "ts.version: %s" ts.version
    printfn "Node O_RDWR %A" Node.Fs.constants.O_RDWR // read/write should be 2
    // printfn "NGHTTP2_STREAM_CLOSED %A" Node.Http2.constants.NGHTTP2_STREAM_CLOSED

    // used by ts2fable
    writeFile ["node_modules/typescript/lib/typescript.d.ts"] "test-compile/TypeScript.fs"
    writeFile ["node_modules/@types/node/index.d.ts"] "test-compile/Node.fs"
    writeFile ["node_modules/@types/yargs/index.d.ts"] "test-compile/Yargs.fs"

    // for test-compile
    writeFile ["node_modules/vscode/vscode.d.ts"] "test-compile/VSCode.fs"
    // writeFile ["node_modules/izitoast/dist/izitoast/izitoast.d.ts"] "test-compile/IziToast.fs"
    writeFile ["node_modules/izitoast/types/index.d.ts"] "test-compile/IziToast.fs"
    writeFile ["node_modules/electron/electron.d.ts"] "test-compile/Electron.fs"
    writeFile ["node_modules/@types/react/index.d.ts"] "test-compile/React.fs"
    writeFile ["node_modules/@types/mocha/index.d.ts"] "test-compile/Mocha.fs"
    writeFile ["node_modules/@types/chai/index.d.ts"] "test-compile/Chai.fs"
    writeFile ["node_modules/chalk/types/index.d.ts"] "test-compile/Chalk.fs"
    writeFile ["node_modules/@types/mkdirp/index.d.ts"] "test-compile/Mkdirp.fs"
    writeFile
        [   "node_modules/@types/google-protobuf/index.d.ts"
            "node_modules/@types/google-protobuf/google/protobuf/empty_pb.d.ts"
        ]
        "test-compile/Protobuf.fs"

    // files that have too many TODOs
    // writeFile ["node_modules/@types/jquery/index.d.ts"] "test-compile/JQuery.fs"
    // writeFile ["node_modules/typescript/lib/lib.es2015.promise.d.ts"] "test-compile/Promise.fs"

    printfn "done writing test-compile files"

else
    let dOption=createEmpty<Options>
    dOption.alias <- Some (U2.Case1 "dependent")
    dOption.description <- Some "Fix module name when TypeScript files interdepent"
    dOption.``default`` <- None
    let argv =
        yargs
            .usage("Usage: ts2fable some.d.ts src/Some.fs")
            .command(U2.Case1 "$0 [files..]", "")
            .demandOption(U2.Case1 "files", "")
            .help()
            .option("d",dOption)
            .argv
    let files = argv.["files"].Value :?> string array |> List.ofArray
    match files with
    | TsFile2FsFile -> 
        let tsfiles = files |> List.filter (fun s -> s.EndsWith ".ts")
        let fsfile = files |> List.tryFind (fun s -> s.EndsWith ".fs")
        match tsfiles.Length, fsfile with
        | 0, _ -> failwithf "Please provide the path to a TypeScript file"
        | _, None -> failwithf "Please provide the path to the F# file to be written "
        | _, Some fsf ->
            printfn "ts2fable %s" Version.version
            // validate ts files exist
            for ts in tsfiles do
                if not <| fs.existsSync(!^ts) then
                    failwithf "TypeScript file not found: %s" ts

            match argv.["d"] with
            |Some s ->writeFile2 tsfiles fsf (string s)
            |None-> writeFile tsfiles fsf     
    | TsDirectory2FsDirectory (tsdir,fsdir)->
        let enumerateFiles dir=
            promise {
                let rec loop (array:ResizeArray<string>) dir = 
                    let enumerateFileSystemEntries dir =
                        let encoding = BufferEncoding.Utf8 |> U2.Case2 |> Some
                        let t= dir |> PathLike.ofString
                        fs.readdirSync (t,encoding) |> Seq.map(fun str -> path.join(ResizeArray<string> [dir;str]))       
                    let isFile path=  
                        let stats=
                            path |> PathLike.ofString |> fs.lstatSync
                        stats.isFile()
                    let isDirectory path=  
                        let stats=
                            path |> PathLike.ofString |> fs.lstatSync
                        stats.isDirectory()            

                    promise {
                        let paths = enumerateFileSystemEntries dir
                        let files = paths |> Seq.filter isFile
                        array.AddRange files
                        let dirs = paths |> Seq.filter isDirectory 
                        printfn "%A" dirs
                        for dir in dirs do
                            return! loop array dir
                    }
                let resizeArray = ResizeArray<string>()    
                do! loop resizeArray dir 
                return resizeArray |> Seq.toList
            }
        
        let handle (files:string list)= 
            let maxSpawnNumber = 8
          
            let tsFiles = files |>List.filter (fun s -> s.EndsWith ".ts")
            let fsFiles = tsFiles |> List.map (fun s ->
                let tsdir = path.normalize tsdir
                let fsdir = path.normalize fsdir
                s.Replace(tsdir,fsdir).Replace(".d.ts",".fs").Replace(".ts",".fs"))
            
            let run (files: (string * string) list) = 
                let rec loop (files: (string * string) list) (spawnNum:int ref) = 
                    promise {
                        while !spawnNum = maxSpawnNumber do do! Promise.sleep(500)
                        
                        let tsFile,fsFile = files.Head
                        let fsDir = fsFile |> path.dirname
                        mkdirp.sync(dir = fsDir) |> ignore
                        
                        let spawn = child_process.spawn ("node", ResizeArray<string> [__filename ;tsFile;fsFile])
                        
                        spawn.addListener_close(fun _ _ -> 
                            decr spawnNum
                            printfn "decr spawn number,current spawn number is %d" !spawnNum) |> ignore
                        incr spawnNum
                        printfn "incr spawn number,current spawn number is %d" !spawnNum
                        
                        return! loop files.Tail spawnNum 
                    }
                loop files (ref 0)

            List.zip tsFiles fsFiles
            |> run
            |> Promise.map (fun _ -> printfn "done writing test-compile files")

        enumerateFiles tsdir
        |> Promise.map handle
        |> ignore
