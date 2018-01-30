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
open System.Collections.Generic
open System
open ts2fable.Keywords
open TypeScript.Ts


let [<Global>] describe (msg: string) (f: unit->unit): unit = jsNative
let [<Global>] it (msg: string) (f: unit->unit): unit = jsNative

// use only to debug single test
let [<Emit("it.only($0,$1)")>] only (msg: string) (f: unit->unit): unit = jsNative
let [<Emit("this.timeout($0)")>] timeout (duration: int): unit = jsNative
let inline equal (expected: 'T) (actual: 'T): unit =
    Testing.Assert.AreEqual(expected, actual)

let testFsFiles tsPaths fsPath (f: FsFile list -> unit) =

    let fsFileOut = getFsFileOut fsPath tsPaths 
    emitFsFileOut fsPath fsFileOut
    f fsFileOut.Files

describe "tests" <| fun _ ->
    timeout 10000

    let getAllTypes fsFiles =
        let tps = List<FsType>()
        fsFiles
        |> List.iter(fun fsFile ->
            fsFile
            |> fixFile (fun tp -> 
                tp |> tps.Add
                tp
            ) |> ignore
        )
        tps |> List.ofSeq
    
    let getTypeByName name fsFiles =
        getAllTypes fsFiles
        |> List.filter(fun tp -> getName tp = name)

    let existOnlyOne name (isType:FsType -> bool) fsFiles= 
        fsFiles
        |> getTypeByName name
        |> List.filter isType
        |> fun tp -> tp.Length = 1
        
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

    it "sample" <| fun _ ->
        let tsPaths = ["node_modules/reactxp/dist/web/ReactXP.d.ts"]
        let fsPath = "test-compile/ReactXP.fs"
        testFsFiles tsPaths fsPath  <| fun _ ->
            equal true true     

    // https://github.com/fable-compiler/ts2fable/issues/154
    it "duplicated variable exports" <| fun _ ->
        let tsPaths = ["node_modules/reactxp/dist/web/ReactXP.d.ts"]
        let fsPath = "test-compile/ReactXP.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
                fsFiles
                |> getTopVarialbles 
                |> List.countBy(fun vb -> vb.Name)
                |> List.forall(fun (_,l) -> l = 1)
                |> equal true

    it "multiple linked files reactxp" <| fun _ ->
        let rec loop tsPath fsDir = 

            let nodePaths,tsPaths= 
                tsPath
                |> readAllResolvedModuleNames 
                
            let fsBasename = tsPath |> getJsModuleName |> capitalize |> sprintf "%s.fs" 
            let fsPath = path.join(ResizeArray [fsDir; fsBasename])
            testFsFiles tsPaths fsPath  <| fun _ ->
                equal true true   

            for nodePath in nodePaths do
                loop nodePath fsDir    
        
        let tsPath = "node_modules/reactxp/dist/web/ReactXP.d.ts"
        let fsDir = "test-compile"        
        loop tsPath fsDir


    it "multiple linked files react" <| fun _ ->
        let rec loop tsPath fsDir = 

            let nodePaths,tsPaths= 
                tsPath
                |> readAllResolvedModuleNames 
            let fsBasename = tsPath |> getJsModuleName |> capitalize |> sprintf "%s.fs" 
            let fsPath = path.join(ResizeArray [fsDir; fsBasename])
            testFsFiles tsPaths fsPath  <| fun _ ->
                equal true true   

            for nodePath in nodePaths do
                loop nodePath fsDir    
        
        let tsPath = "node_modules/@types/react/index.d.ts"
        let fsDir = "test-compile"        
        loop tsPath fsDir

    it "compile type alias has only function to interface" <| fun _ ->
        let tsPaths = ["test/fragments/react/f1.d.ts"]
        let fsPath = "test/fragments/react/f1.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> getTopTypes
            |> List.exists (fun tp -> getName tp = "DOMFactory" && FsType.isInterface tp)
            |> equal true

    it "fix generic default parameters" <| fun _ ->
        let tsPaths = ["test/fragments/react/f2.d.ts"]
        let fsPath = "test/fragments/react/f2.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            (
            (fsFiles 
            |> getAllTypes
            |> List.filter (fun tp -> getName tp = "Component")
            |> List.filter FsType.isAlias
            |> fun tps -> tps.Length = 2)
            &&
                (fsFiles 
                |> getAllTypes
                |> List.filter (fun tp -> getName tp = "ComponentType")
                |> List.filter FsType.isAlias
                |> fun tps -> tps.Length = 2)
            )            
            |> equal true
            
    it "compile alias with intersection type to interface" <| fun _ ->
        let tsPaths = ["test/fragments/react/f3.d.ts"]
        let fsPath = "test/fragments/react/f3.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> getTopTypes
            |> List.exists (fun tp -> getName tp = "DetailedHTMLProps" && FsType.isInterface tp)
            |> equal true                   
    
    // simplely fixed https://github.com/fable-compiler/ts2fable/issues/44 
    it "compile alias with mapped types to interface" <| fun _ ->
        let tsPaths = ["test/fragments/react/f4.d.ts"]
        let fsPath = "test/fragments/react/f4.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> getTopTypes
            |> List.exists (fun tp -> getName tp = "ValidationMap" && FsType.isInterface tp)
            |> equal true      

    it "compile alias with type literal to interface" <| fun _ ->
        let tsPaths = ["test/fragments/react/f5.d.ts"]
        let fsPath = "test/fragments/react/f5.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> getTopTypes
            |> List.exists (fun tp -> getName tp = "Validator" && FsType.isInterface tp)
            |> equal true                  

    it "fix es6 type" <| fun _ ->
        let tsPaths = ["test/fragments/react/f6.d.ts"]
        let fsPath = "test/fragments/react/f6.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> getAllTypes
            |> List.filter FsType.isGeneric
            |> List.exists(fun f ->
                esKeyWords 
                |> List.ofSeq
                |> List.contains (getName f)
            )
            |> equal false

    it "extract type literal from union" <| fun _ ->
        let tsPaths = ["test/fragments/react/f7.d.ts"]
        let fsPath = "test/fragments/react/f7.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existOnlyOne "Ref" FsType.isAlias
            |> equal true        

    it "extract type literal from variable" <| fun _ ->
        let tsPaths = ["test/fragments/SyncTasks/f1.d.ts"]
        let fsPath = "test/fragments/SyncTasks/f1.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            (
                (existOnlyOne "config" FsType.isVariable fsFiles) 
                && 
                (existOnlyOne "Config'" FsType.isInterface fsFiles)
            )
            |> equal true

    it "extract type literal from alias" <| fun _ ->
        let tsPaths = ["test/fragments/SyncTasks/f2.d.ts"]
        let fsPath = "test/fragments/SyncTasks/f2.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existOnlyOne "RaceTimerResponse" FsType.isInterface
            |> equal true

    it "anonymous type should has single quotation mark" <| fun _ ->
        let tsPaths = ["test/fragments/Node/f1.d.ts"]
        let fsPath = "test/fragments/Node/f1.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existOnlyOne "Buffer'" FsType.isInterface
            |> equal true

    it "fix interSection to simple obj" <| fun _ ->
        let tsPaths = ["test/fragments/Node/f2.d.ts"]
        let fsPath = "test/fragments/Node/f2.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> getAllTypes
            |> List.choose FsType.asTuple
            |> List.forall(fun tu -> tu.Types.Length = 2)      
            |> equal true

    it "remove external module alias" <| fun _ ->
        let tsPaths = ["test/fragments/reactxp/f1.d.ts"]
        let fsPath = "test/fragments/reactxp/f1.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            existOnlyOne "CommonStyledProps" FsType.isImport fsFiles
            |> not 
            |> fun b -> b && existOnlyOne "Animated" FsType.isImport fsFiles
            |> equal true

    it "fix pointing to remote sub module alias" <| fun _ ->
        let tsPaths = 
            [
                "test/fragments/reactxp/f4Master.d.ts"
                "test/fragments/reactxp/f4Interface.d.ts"
                "test/fragments/reactxp/f4Types.d.ts"
            ]
        let fsPath = "test/fragments/reactxp/f4.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existOnlyOne "__f4Types.SyntheticEvent" FsType.isMapped
            |> equal true
            
    it "fix pointing to remote sub module alias 2" <| fun _ ->
        let tsPaths = 
            [
                "test/fragments/reactxp/f5Master.d.ts"
                "test/fragments/reactxp/f5Animated.d.ts"
                "test/fragments/reactxp/f5Types.d.ts"
            ]
        let fsPath = "test/fragments/reactxp/f5.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existOnlyOne "Types.Animated.EndCallback" FsType.isMapped
            |> equal true
            
