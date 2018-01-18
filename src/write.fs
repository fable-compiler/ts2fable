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
let getFsFiles tsPaths = 
    let workSpaceRoot = ``process``.cwd()
    let tsPaths = tsPaths |> List.map (fun tsPath -> path.join(ResizeArray [workSpaceRoot; tsPath]))
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

    tsFiles |> List.mapi (fun i tsFile ->
        {
            FileName = tsFile.fileName
            ModuleName = moduleNameMap.[tsFile.fileName]
            Modules = []
            IsMaster = i = 0
        }
        |> readSourceFile checker tsFile
        |> transform
    )


let emitFsFiles fsPath (fsFiles: FsFile list) = 

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

    let file = fs.createWriteStream (!^fsPath)
    for line in printFsFile fsFileOut do
        file.write(sprintf "%s%c" line '\n') |> ignore
    file.``end``()     
let writeFile (tsPaths: string list) (fsPath: string): unit =
    // printfn "writeFile %A %s" tsPaths fsPath

    let fsFiles = getFsFiles tsPaths 
    emitFsFiles fsPath fsFiles
