module rec delegate

open System
open Fable.Core
open Fable.Core.JS

type [<AllowNullLiteral>] A =
    abstract alpha: float

type Delegate1 =
    delegate of arg1: float * ?optArg1: string * ?optArg2: string -> unit

type Delegate2<'T when 'T :> A> =
    delegate of arg1: 'T * arg2: U2<float, string> * [<ParamArray>] paramArgs: float[] -> string