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
open Node
open Node.Fs

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

let testFsFileLines tsPaths fsPath (f: string list -> unit) =

    let fsFileOut = getFsFileOut fsPath tsPaths 
    emitFsFileOutAsLines fsPath fsFileOut
    |> f



describe "transform tests" <| fun _ ->
    timeout 10000
    
    let getAllTypesFromFile fsFile =
        let tps = List []
        fsFile
        |> fixFile (fun tp -> 
            tp |> tps.Add
            tp
        ) |> ignore
        tps |> List.ofSeq

    let getAllTypes fsFiles =
        fsFiles |> List.collect getAllTypesFromFile

    
    let getTypeByName name fsFiles =
        getAllTypes fsFiles
        |> List.filter(fun tp -> getName tp = name)

    let existOnlyOne name (isType:FsType -> bool) fsFiles= 
        fsFiles
        |> getTypeByName name
        |> List.filter isType
        |> fun tp -> tp.Length = 1

    let existMany i name (isType:FsType -> bool) fsFiles= 
        fsFiles
        |> getTypeByName name
        |> List.filter isType 
        |> fun tp -> tp.Length = i   

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
        let tsPaths = ["node_modules/reactxp/dist/web/ReactXP.d.ts"]
        let fsPath = "test-compile/ReactXP.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles
            |> getTopVarialbles 
            |> List.countBy(fun vb -> vb.Name)
            |> List.forall(fun (_,l) -> l = 1)
            |> equal true

    it "multiple ts inputs should export one time" <| fun _ ->
        let tsPaths =         
            [   
                "node_modules/@types/google-protobuf/index.d.ts"
                "node_modules/@types/google-protobuf/google/protobuf/empty_pb.d.ts"
            ]
        let fsPath = "test-compile/Protobuf.fs"
        testFsFileLines tsPaths fsPath  <| fun lines ->
            lines.Length < 700
            |> equal true

    // https://github.com/fable-compiler/ts2fable/pull/167
    it "extract type literal from union" <| fun _ ->
        let tsPaths = ["test/fragments/react/f1.d.ts"]
        let fsPath = "test/fragments/react/f1.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existOnlyOne "bivarianceHack" FsType.isFunction
            |> equal true

    
    it "compile type alias has only function to interface" <| fun _ ->
        let tsPaths = ["test/fragments/react/f2.d.ts"]
        let fsPath = "test/fragments/react/f2.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existOnlyOne "DOMFactory" FsType.isInterface
            |> equal true