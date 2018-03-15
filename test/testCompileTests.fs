module ts2fable.testCompileTests

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
open ts2fable.Keywords
open System.ComponentModel
let [<Global>] describe (msg: string) (f: unit->unit): unit = jsNative
let [<Global>] it (msg: string) (f: unit->unit): unit = jsNative

// use only to debug single test
let [<Emit("it.only($0,$1)")>] only (msg: string) (f: unit->unit): unit = jsNative

// use onlys to debug single group tests
let [<Emit("describe.only($0,$1)")>] onlys (msg: string) (f: unit->unit): unit = jsNative
let [<Emit("this.timeout($0)")>] timeout (duration: int): unit = jsNative
let inline equal (expected: 'T) (actual: 'T): unit =
    Testing.Assert.AreEqual(expected, actual)

describe "generate test compile files" <| fun _ ->

    timeout 50000
    printfn "ts.version: %s" ts.version
    printfn "Node O_RDWR %A" Node.Fs.constants.O_RDWR // read/write should be 2
    printfn "NGHTTP2_STREAM_CLOSED %A" Node.Http2.constants.NGHTTP2_STREAM_CLOSED    
     
    it "node" <| fun _ ->
        writeFile ["node_modules/@types/node/index.d.ts"] "test-compile/Node.fs"
        equal true true     

    it "yargs" <| fun _ ->
       // used by ts2fable
        writeFile ["node_modules/@types/yargs/index.d.ts"] "test-compile/Yargs.fs"
        equal true true 

    it "vscode" <| fun _ ->
        writeFile ["node_modules/vscode/vscode.d.ts"] "test-compile/VSCode.fs"
        equal true true 

    it "izitoast" <| fun _ ->
        writeFile ["node_modules/izitoast/types/index.d.ts"] "test-compile/IziToast.fs"
        equal true true 
  
    it "electron" <| fun _ ->
        writeFile ["node_modules/electron/electron.d.ts"] "test-compile/Electron.fs"
        equal true true 

    it "react" <| fun _ ->
        writeFile ["node_modules/@types/react/index.d.ts"] "test-compile/React.fs"
        equal true true 

    it "mocha" <| fun _ ->
        writeFile ["node_modules/@types/mocha/index.d.ts"] "test-compile/Mocha.fs"
        equal true true 

    it "chai" <| fun _ ->
        writeFile ["node_modules/@types/chai/index.d.ts"] "test-compile/Chai.fs"
        equal true true 
        
    it "chalk" <| fun _ ->
        writeFile ["node_modules/chalk/types/index.d.ts"] "test-compile/Chalk.fs"
        equal true true 
        
    it "monaco" <| fun _ ->
        writeFile ["node_modules/monaco-editor/monaco.d.ts"] "test-compile/Monaco.fs"
        equal true true 
        
    it "google-protobuf" <| fun _ ->
        writeFile
            [   "node_modules/@types/google-protobuf/index.d.ts"
                "node_modules/@types/google-protobuf/google/protobuf/empty_pb.d.ts"
            ]
            "test-compile/Protobuf.fs"
        equal true true

    printfn "done writing test-compile files"    
        