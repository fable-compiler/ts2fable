// ts2fable 0.0.0
module rec f8
open System
open Fable.Core
open Fable.Import.JS


type Key =
    U2<string, float>

[<RequireQualifiedAccess; CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Key =
    let ofString v: Key = v |> U2.Case1
    let isString (v: Key) = match v with U2.Case1 _ -> true | _ -> false
    let asString (v: Key) = match v with U2.Case1 o -> Some o | _ -> None
    let ofFloat v: Key = v |> U2.Case2
    let isFloat (v: Key) = match v with U2.Case2 _ -> true | _ -> false
    let asFloat (v: Key) = match v with U2.Case2 o -> Some o | _ -> None
