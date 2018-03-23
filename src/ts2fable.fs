module ts2fable.App

open Fable.Core
open Fable.Core.JsInterop
open Node
open Yargs

open ts2fable.Write

let argv =
    yargs
        .usage("Usage: ts2fable some.d.ts src/Some.fs")
        .command(U2.Case1 "$0 [files..]", "")
        .option("files", jsOptions<Yargs.Options>(fun o ->
            o.alias <- Some !^"f"
            o.description <- Some "input ts files and output fs file"
            o.``type`` <- U5.Case1 "array" |> Some
        ))
        .option("exports", jsOptions<Yargs.Options>(fun o ->
            o.alias <- Some !^"e"
            o.description <- Some "export types from files that contain the string"
            o.``type`` <- U5.Case1 "array" |> Some
        ))
        .help()
        .argv

//printfn "argv %A" argv
let files = argv.["files"].Value :?> string array |> List.ofArray
let tsfiles = files |> List.filter (fun s -> s.EndsWith ".ts")
let fsfile = files |> List.tryFind (fun s -> s.EndsWith ".fs")
let exports = match argv.["exports"] with None -> [] | Some v -> v :?> string array |> List.ofArray

match tsfiles.Length, fsfile with
| 0, _ -> failwithf "Please provide the path to a TypeScript file"
| _, None -> failwithf "Please provide the path to the F# file to be written "
| _, Some fsf ->
    printfn "ts2fable %s" Version.version

    // validate ts files exist
    for ts in tsfiles do
        if not <| fs.existsSync(!^ts) then
            failwithf "TypeScript file not found: %s" ts

    writeFile tsfiles fsf exports
