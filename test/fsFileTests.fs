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
open ts2fable.Keywords

let [<Global>] describe (msg: string) (f: unit->unit): unit = jsNative
let [<Global>] it (msg: string) (f: unit->unit): unit = jsNative

// use only to debug single test
let [<Emit("it.only($0,$1)")>] only (msg: string) (f: unit->unit): unit = jsNative
let [<Emit("this.timeout($0)")>] timeout (duration: int): unit = jsNative
let inline equal (expected: 'T) (actual: 'T): unit =
    Testing.Assert.AreEqual(expected, actual)

let testFsFiles tsPaths fsPath (f: FsFile list -> unit) =

    let fsFileOut = getFsFileOut fsPath tsPaths []
    emitFsFileOut fsPath fsFileOut 
    f fsFileOut.Files

let testFsFileLines tsPaths fsPath (f: string list -> unit) =

    let fsFileOut = getFsFileOut fsPath tsPaths []
    emitFsFileOutAsLines fsPath fsFileOut 
    |> f


// make sure tests are strict
describe "transform tests" <| fun _ ->
    timeout 10000
    
    let getTypeByName name fsFiles =
        getAllTypes fsFiles
        |> List.filter(fun tp -> getName tp = name)

    let existOnlyOneByName name (predicate: FsType -> bool) fsFiles= 
        fsFiles
        |> getTypeByName name
        |> List.filter predicate
        |> fun tp -> tp.Length = 1

    let existLeastOneByName name (predicate: FsType -> bool) fsFiles= 
        fsFiles
        |> getTypeByName name
        |> List.filter predicate
        |> fun tp -> tp.Length > 0     

    let existNone (predicate: FsType -> bool) fsFiles= 
        fsFiles
        |> getAllTypes
        |> List.filter predicate
        |> fun tp -> tp.Length = 0        

    let existNoneByName name (predicate: FsType -> bool) fsFiles= 
        fsFiles
        |> getTypeByName name
        |> List.filter predicate
        |> fun tp -> tp.Length = 0                  

    let existManyByName i name (predicate: FsType -> bool) fsFiles= 
        fsFiles
        |> getTypeByName name
        |> List.filter predicate
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

    let inline (<&&>) (t1:FsFile list -> bool) (t2:FsFile list -> bool) fsFiles =
        fsFiles |> t1 && fsFiles |> t2        

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

    // https://github.com/fable-compiler/ts2fable/pull/164
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
    
    // https://github.com/fable-compiler/ts2fable/issues/175
    it "fix some Option.map to Microsoft.FSharp.Core.Option.map" <| fun _ ->
        let tsPaths = ["node_modules/@types/react/index.d.ts"]
        let fsPath = "test-compile/React.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existLeastOneByName "ReactNode" (fun tp ->
                match tp with 
                | FsType.Module md -> 
                    md.HelperLines  |> List.exists(fun l -> l.Contains("Microsoft.FSharp.Core.Option.map"))
                | _ -> false
            )
            |> equal true

    // https://github.com/fable-compiler/ts2fable/pull/170
    it "compile type alias has only function to interface" <| fun _ ->
        let tsPaths = ["test/fragments/react/f2.d.ts"]
        let fsPath = "test/fragments/react/f2.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existOnlyOneByName "DOMFactory" FsType.isInterface
            |> equal true

    // https://github.com/fable-compiler/ts2fable/pull/167        
    it "extract type literal from union" <| fun _ ->
        let tsPaths = ["test/fragments/react/f1.d.ts"]
        let fsPath = "test/fragments/react/f1.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existOnlyOneByName "bivarianceHack" FsType.isFunction
            |> equal true

    // https://github.com/fable-compiler/ts2fable/issues/177        
    it "generic parameter defaults" <| fun _ ->
        let tsPaths = ["test/fragments/react/f3.d.ts"]
        let fsPath = "test/fragments/react/f3.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existManyByName 2 "Component" FsType.isAlias
            |> equal true            

    // https://github.com/fable-compiler/ts2fable/issues/179  
    it "fix types has ESKeywords" <| fun _ ->
        let tsPaths = ["test/fragments/react/f4.d.ts"]
        let fsPath = "test/fragments/react/f4.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existNone (fun tp -> 
                let name = getName tp
                esKeywords.Contains name
            )
            |> equal true

    // https://github.com/fable-compiler/ts2fable/issues/181 
    it "extract types in global module" <| fun _ ->
        let tsPaths = ["test/fragments/react/f5.d.ts"]
        let fsPath = "test/fragments/react/f5.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existNoneByName "global" FsType.isModule
            |> equal true            

    // https://github.com/fable-compiler/ts2fable/issues/182               
    it "compile intersection to interface end" <| fun _ ->
        let tsPaths = ["test/fragments/react/f6.d.ts"]
        let fsPath = "test/fragments/react/f6.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existOnlyOneByName "ClassType" FsType.isInterface
            |> equal true                                    

    // https://github.com/fable-compiler/ts2fable/issues/185               
    it "extract type literal from type alias" <| fun _ ->
        let tsPaths = ["test/fragments/react/f7.d.ts"]
        let fsPath = "test/fragments/react/f7.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |>  (
                    existOnlyOneByName "EventHandler" FsType.isInterface 
                    <&&> existOnlyOneByName "bivarianceHack" FsType.isFunction
                )
            |> equal true                                                

    // https://github.com/fable-compiler/ts2fable/issues/44               
    it "map mapped types" <| fun _ ->
        let tsPaths = ["test/fragments/react/f8.d.ts"]
        let fsPath = "test/fragments/react/f8.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existLeastOneByName "ValidationMap" FsType.isInterface
            |> equal true                

    // https://github.com/fable-compiler/ts2fable/issues/208
    it "remove duplicate options" <| fun _ ->
        let tsPaths = ["test/fragments/yargs/duplicateOption.d.ts"]
        let fsPath = "test/fragments/yargs/duplicateOption.fs"
        testFsFiles tsPaths fsPath  <| fun fsFiles ->
            fsFiles 
            |> existNone (fun tp ->
                match tp with 
                | FsType.Union un when un.Option -> true
                | _ -> false
            )
            |> equal true                            