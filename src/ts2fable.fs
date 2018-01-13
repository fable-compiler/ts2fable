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



let argv = ``process``.argv |> List.ofSeq

// if run via `dotnet fable npm-build` or `dotnet fable npm-start`
// TODO `dotnet fable npm-build` doesn't wait for the test files to finish writing
if argv |> List.exists (fun s -> s = "splitter.config.js") then // run from build
    failwith "Save Debug Time"
    printfn "ts.version: %s" ts.version
    printfn "Node O_RDWR %A" Node.Fs.constants.O_RDWR // read/write should be 2
    printfn "NGHTTP2_STREAM_CLOSED %A" Node.Http2.constants.NGHTTP2_STREAM_CLOSED

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
    let argv =
        yargs
            .usage("Usage: ts2fable some.d.ts src/Some.fs")
            .command(U2.Case1 "$0 [files..]", "")
            .demandOption(U2.Case1 "files", "")
            .help()
            .argv

    let files = argv.["files"].Value :?> string array |> List.ofArray
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

        writeFile tsfiles fsf