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
let [<Emit("it.only($0,$1)")>] only (msg: string) (f: unit->unit): unit = jsNative
let [<Emit("this.timeout($0)")>] timeout (duration: int): unit = jsNative
let inline equal (expected: 'T) (actual: 'T): unit =
    Testing.Assert.AreEqual(expected, actual)

let testFsFiles tsPaths fsPath (f: FsFile list -> unit) =

    let fsFiles = getFsFiles tsPaths 
    emitFsFiles fsPath fsFiles
    f fsFiles

describe "react tests" <| fun _ ->
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

    let existone name (isType:FsType -> bool) fsFiles= 
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
        timeout 10000
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
        timeout 10000
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

    it "compile interface has optional generic type to multiple interfaces" <| fun _ ->
        let tsPaths = ["test/fragments/react/f2.d.ts"]
        let fsPath = "test/fragments/react/f2.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> getTopTypes
            |> List.filter (fun tp -> getName tp = "Component")
            |> List.length
            |> equal 3

    it "compile intersection type to interface" <| fun _ ->
        let tsPaths = ["test/fragments/react/f3.d.ts"]
        let fsPath = "test/fragments/react/f3.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> getTopTypes
            |> List.exists (fun tp -> getName tp = "DetailedHTMLProps" && FsType.isInterface tp)
            |> equal true                   
    
    // simplely fixed https://github.com/fable-compiler/ts2fable/issues/44 
    it "compile mapped types to interface" <| fun _ ->
        let tsPaths = ["test/fragments/react/f4.d.ts"]
        let fsPath = "test/fragments/react/f4.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> getTopTypes
            |> List.exists (fun tp -> getName tp = "ValidationMap" && FsType.isInterface tp)
            |> equal true      

    it "compile type literal to interface" <| fun _ ->
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
            |> existone "Ref" FsType.isInterface
            |> equal true        

    it "extract type literal from variable" <| fun _ ->
        let tsPaths = ["test/fragments/SyncTasks/f1.d.ts"]
        let fsPath = "test/fragments/SyncTasks/f1.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            (
                (existone "config" FsType.isVariable fsFiles) 
                && 
                (existone "Config" FsType.isInterface fsFiles)
            )
            |> equal true

    it "extract type literal from export declare type alias" <| fun _ ->
        let tsPaths = ["test/fragments/SyncTasks/f2.d.ts"]
        let fsPath = "test/fragments/SyncTasks/f2.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existone "RaceTimerResponse" FsType.isInterface
            |> equal true

