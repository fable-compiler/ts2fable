module ts2fable.fsFileTests

open Fable.Core
open Fable.Core.JsInterop

open ts2fable.Naming
open TypeScript
open TypeScript.Ts
open Node
open ts2fable.Read
open ts2fable.Write
open ts2fable.Transform
open ts2fable.Print

let [<Emit("this.timeout($0)")>] private timeout (duration: int): unit = jsNative

let [<Global>] describe (msg: string) (f: unit->unit): unit = jsNative
let [<Global>] it (msg: string) (f: unit->unit): unit = jsNative
let inline equal (expected: 'T) (actual: 'T): unit =
    Testing.Assert.AreEqual(expected, actual)

let testFsFiles tsPath fsPath (f: FsFile list -> unit) =
    let getFsFiles tsPath = 
        let workSpaceRoot = ``process``.cwd()
        let tsPaths = [path.join(ResizeArray [workSpaceRoot; tsPath])]
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

        tsFiles |> List.map (fun tsFile ->
            {
                FileName = tsFile.fileName
                ModuleName = moduleNameMap.[tsFile.fileName]
                Modules = []
            }
            |> readSourceFile checker tsFiles
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

    let fsFiles = getFsFiles tsPath 
    emitFsFiles fsPath fsFiles
    f fsFiles


describe "transform tests" <| fun _ ->
    timeout 10000
    
    let getTopTypes fsFiles = 
        fsFiles
        |> List.head
        |> fun f ->  f.Modules
        |> List.head
        |> fun md -> md.Types
    
    let getTopVarialbles fsFiles = 
        fsFiles
        |> getTopTypes
        |> List.choose FsType.asVariable 

    //https://github.com/fable-compiler/ts2fable/issues/154
    it "duplicated variable exports" <| fun _ ->
        let tsPath = "node_modules/reactxp/dist/web/ReactXP.d.ts"
        let fsPath = "test-compile/ReactXP.fs"
        testFsFiles tsPath fsPath  <| fun fsFiles ->
                fsFiles
                |> getTopVarialbles 
                |> List.countBy(fun vb -> vb.Name)
                |> List.forall(fun (_,l) -> l = 1)
                |> equal true