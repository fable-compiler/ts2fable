module ts2fable.Version

open Node
open Node.fs
open Fable.Core
open Fable.Core.JsInterop
open System.Collections.Generic

type PackageJson =
    {
        version: string
    }
let version =
    let packageJsonPath = path.join(List([__dirname; "../package.json"]))
    let packageJson = fs.readFileSync(PathLike.ofString packageJsonPath |> U2.Case1, "utf8" |> U2.Case2) |> ofJson<PackageJson>
    packageJson.version