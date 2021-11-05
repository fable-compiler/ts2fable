module ts2fable.node.App

open Fable.Core
open Fable.Core.JsInterop
open Node.Api

let getArgs pred (args: string seq) =
    args
    |> Seq.skipWhile (pred >> not)
    |> Seq.skipWhile pred
    |> Seq.takeWhile (fun s -> not (s.StartsWith "-"))
    |> Seq.toList

let parseArgs (args: string[]) =
    let exports = args |> getArgs (fun s -> s = "-e" || s = "--exports")
    let fsPaths = args |> Array.filter (fun s -> s.EndsWith ".fs") |> Array.toList
    let tsPaths = args |> Array.filter (fun s -> s.EndsWith ".ts") |> Array.toList
    Config.EmitResizeArray <- not (args |> Array.contains (Config.OptionNames.NoEmitResizeArray))
    Config.ConvertPropertyFunctions <- args |> Array.contains (Config.OptionNames.ConvertPropertyFunctions)
    if List.isEmpty fsPaths then failwithf "Please provide the path to the F# file to be written."
    if List.isEmpty tsPaths then failwithf "Please provide the path to a TypeScript file."
    // print ts2fable version
    printfn "ts2fable %s" Version.version
    // validate ts files exist
    for tsPath in tsPaths do
        if not <| fs.existsSync(tsPath |> U2.Case1) then
            failwithf "TypeScript file not found: %s" tsPath
    Write.writeFile tsPaths fsPaths.Head exports

#if !FABLE_COMPILER || TS2FABLE_STANDALONE
[<EntryPoint>]
#endif
let main (argv: string[]): int =
    try
        if Array.contains "-v" argv || Array.contains "--version" argv
        then printfn "%s" Version.version
        if Array.length argv < 2 || Array.contains "-h" argv || Array.contains "--help" argv
        then
            printfn "Usage: ts2fable some.d.ts src/Some.fs"
            printfn "Options:\n%s" (Config.Usage())
        else parseArgs argv
        ``process``.exitCode <- 0.0
        0
    with ex ->
        eprintfn "%A" ex.StackTrace
        ``process``.exitCode <- 1.0
        1
