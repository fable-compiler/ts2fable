module ts2fable.node.Version

open Node.Api
open Fable.Core
open Fable.Core.DynamicExtensions


/// Cannot use `__dirname` with esm
module private Path =
    open Fable.Core.JsInterop
    // // source: https://github.com/fable-compiler/Fable/blob/1527ca6922a7900b83284429a8c13656da208229/src/fable-compiler-js/src/app.fs#L9-L15
    [<Emit("import.meta.url")>]
    let importMetaUrl: string = jsNative
    let fileURLToPath (path: string): string = importMember "url"
    let currentFilePath = fileURLToPath(importMetaUrl)
    let currentDirPath = currentFilePath |> path.dirname

let tryGetVersion (packageJsonPath: string) =
    if fs.existsSync (packageJsonPath |> U2.Case1) then
        let packageJson = fs.readFileSync(packageJsonPath, "utf8") |> JS.JSON.parse
        let version = packageJson.["version"]
        if version = JS.undefined then
            None
        else
            Some (string version)
    else
        None

/// Unlike `tryGetVersion`, this return version `0.0.0` as fallback
let getVersion (packageJsonPath: string) =
    tryGetVersion packageJsonPath
    |> Option.defaultValue "0.0.0"

let version =
    let packageJsonPath = path.join [| Path.currentDirPath ; "../package.json" |]
    getVersion packageJsonPath