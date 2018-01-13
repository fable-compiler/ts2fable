module ts2fable.Version

open Node
open Fable.Core.JsInterop

type PackageJson =
    {
        version: string
    }
let version =
    let packageJsonPath = path.join(ResizeArray([``process``.cwd(); "package.json"]))
    let packageJson = fs.readFileSync(!^(!^packageJsonPath), !^"utf8") |> ofJson<PackageJson>
    packageJson.version