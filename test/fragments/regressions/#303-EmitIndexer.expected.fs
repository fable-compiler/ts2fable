// ts2fable 0.8.0
module rec #303-EmitIndexer
open System
open Fable.Core
open Fable.Core.JS


type [<AllowNullLiteral>] A =
    [<EmitIndexer>] abstract Item: i: float -> float with get, set

type [<AllowNullLiteral>] B =
    [<EmitIndexer>] abstract Item: i: float -> string with get, set
    [<EmitIndexer>] abstract Item: v: string -> string with get, set