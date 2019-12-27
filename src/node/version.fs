module ts2fable.node.Version

open Node.Api
open Fable.Core

[<Emit "require($0).version">]
let getVersion (path: string): string = jsNative

let version =
    let packageJsonPath = path.join [| __dirname; "../package.json" |]
    if fs.existsSync (packageJsonPath |> U2.Case1) then
        getVersion packageJsonPath
        // let packageJson = fs.readFileSync(packageJsonPath, "utf8") |> ofJson<PackageJson>
        // packageJson.version
    else
        "0.0.0"