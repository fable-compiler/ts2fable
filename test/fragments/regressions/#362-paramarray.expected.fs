// ts2fable 0.8.0
module rec ``#362-paramarray``
open System
open Fable.Core
open Fable.Core.JS


type [<AllowNullLiteral>] IExports =
    abstract a1: [<ParamArray>] args: string[] -> unit
    abstract a2: a: string * [<ParamArray>] args: float[] -> unit
    abstract a3: a: string * b: float * [<ParamArray>] args: ResizeArray<string>[] -> unit
    abstract a4: vs: ResizeArray<string> * [<ParamArray>] args: string[] -> unit
    abstract r1: args: ResizeArray<string> -> unit
    abstract r2: a: string * args: ResizeArray<float> -> unit
    abstract r3: a: string * b: float * args: ResizeArray<ResizeArray<string>> -> unit
    abstract r4: vs: ResizeArray<string> * args: ResizeArray<string> -> unit