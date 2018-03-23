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
open Yargs.Yargs

let argv =
    yargs
        .usage("Usage: ts2fable some.d.ts src/Some.fs  
        ts2fable --multiple some.d.ts src/Some.fs")
        .command(U2.Case1 "$0 [files..]", "")
        .demandOption(U2.Case1 "files", "")
        .help()
        .option("multiple",jsOptions<Options>(fun o ->
            o.description <- Some "Import from multiple linked tsfiles"
            o.boolean <- Some true
        ))
        .argv


let files = argv.["files"].Value :?> string array |> List.ofArray
let tsfiles = files |> List.filter (fun s -> s.EndsWith ".ts")
let fsfile = files |> List.tryFind (fun s -> s.EndsWith ".fs")
let isMultiple = argv.["multiple"].Value :?> bool

// validate ts files exist
for ts in tsfiles do
    if not <| fs.existsSync(!^ts) then
        failwithf "TypeScript file not found: %s" ts

match tsfiles.Length, fsfile,isMultiple with
| 0, _, _ -> failwithf "Please provide the path to a TypeScript file"
| _, None, _ -> failwithf "Please provide the path to the F# file to be written "
| _, Some fsf, false ->
    printfn "ts2fable %s" Version.version
    writeFile tsfiles fsf
| 1, Some _, true ->
    printf "Not implemented"
| _, Some _, true ->
    failwithf "--multiple only accept one input tsfile"
