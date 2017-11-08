[<AutoOpen>]
module rec ts2fable.App

// This app has 3 main functions.
// 1. Read a TypeScript file into a syntax tree.
// 2. Fix the syntax tree.
// 3. Print the syntax tree to a F# file.

let p = Fable.Import.Node.Globals.``process``
let argv = p.argv |> List.ofSeq
// printfn "%A" argv

// if run via `dotnet fable npm-build` or `dotnet fable npm-start`
// TODO `dotnet fable npm-build` doesn't wait for the test files to finish writing
if argv |> List.exists (fun s -> s = "splitter.config.js") then // run from build
    printfn "ts.version: %s" ts.version
    writeFile "node_modules/izitoast/dist/izitoast/izitoast.d.ts" "src/bin/Fable.Import.IziToast.fs"
    writeFile "node_modules/typescript/lib/typescript.d.ts" "src/bin/Fable.Import.TypeScript.fs"
    writeFile "node_modules/@types/electron/index.d.ts" "src/bin/Fable.Import.Electron.fs"
    writeFile "node_modules/@types/react/index.d.ts" "src/bin/Fable.Import.React.fs"
    writeFile "node_modules/@types/node/index.d.ts" "src/bin/Fable.Import.Node.fs"

else
    let tsfile = argv |> List.tryFind (fun s -> s.EndsWith ".ts")
    let fsfile = argv |> List.tryFind (fun s -> s.EndsWith ".fs")
    
    match tsfile, fsfile with
    | None, _ -> failwithf "Please provide the path to a TypeScript definition file"
    | _, None -> failwithf "Please provide the path to the F# file to be written "
    | Some tsf, Some fsf -> writeFile tsf fsf