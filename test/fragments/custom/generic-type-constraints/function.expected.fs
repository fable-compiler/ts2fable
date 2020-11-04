// ts2fable 0.8.0
module rec function
open System
open Fable.Core
open Fable.Core.JS


type [<AllowNullLiteral>] IExports =
    abstract f1: unit -> unit when 'T :> A
    abstract f2: unit -> unit when 'T :> A and 'T :> B

type [<AllowNullLiteral>] A =
    abstract alpha: float

type [<AllowNullLiteral>] B =
    abstract beta: string

type [<AllowNullLiteral>] C<'T> =
    abstract value: 'T

type [<AllowNullLiteral>] I1 =
    abstract f1: value: 'T -> unit when 'T :> A
    abstract f2: value1: 'T * value2: 'U -> unit when 'T :> A and 'U :> B