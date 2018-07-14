module ts2fable.node.Version

open Node
open Fable.Core.JsInterop

type PackageJson =
    {
        version: string
    }
let version =
    let packageJsonPath = path.join(ResizeArray([__dirname; "../package.json"]))
    if fs.existsSync !^packageJsonPath then
        let packageJson = fs.readFileSync(!^(!^packageJsonPath), !^"utf8") |> ofJson<PackageJson>
        packageJson.version
    else
        "0.0.0"