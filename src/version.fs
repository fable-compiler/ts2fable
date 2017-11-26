module ts2fable.Version

open Node
open Node.fs
open Fable.Core.JsInterop

type PackageJson =
    {
        version: string
    }
let version =
    let packageJsonPath = path.join(ResizeArray([__dirname; "../package.json"]))
    let packageJson = fs.readFileSync(!^(!^packageJsonPath), !^"utf8") |> ofJson<PackageJson>
    packageJson.version