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
let [<Global>] describe (msg: string) (f: unit->unit): unit = jsNative
let [<Global>] it (msg: string) (f: unit->unit): unit = jsNative
let inline equal (expected: 'T) (actual: 'T): unit =
    Testing.Assert.AreEqual(expected, actual)

let testFsFiles tsPaths fsPath (f: FsFile list -> unit) =

    let fsFiles = getFsFiles tsPaths 
    emitFsFiles fsPath fsFiles
    f fsFiles

describe "transform tests" <| fun _ ->
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

    //https://github.com/fable-compiler/ts2fable/issues/154
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
        let getTsPaths tsPath= 
            tsPath 
            |> readAllResolvedModuleNames 
            |> List.filter(fun s -> getJsModuleName s =  getJsModuleName tsPath)
        
        let tsPaths = getTsPaths "node_modules/reactxp/dist/web/ReactXP.d.ts"
        let fsPath = "test-compile/ReactXP.fs"
        testFsFiles tsPaths fsPath  <| fun _ ->
            equal true true   

    // it "multiple linked files protobuf" <| fun _ ->
    //     let tsPaths = 
    //         [   
    //             "node_modules/@types/google-protobuf/index.d.ts"
    //             "node_modules/@types/google-protobuf/google/protobuf/empty_pb.d.ts"
    //         ]
    //     let fsPath = "test-compile/Protobuf.fs"
    //     testFsFiles tsPaths fsPath  <| fun _ ->
    //         equal true true               