// ts2fable 0.8.0
module rec or
open System
open Fable.Core
open Fable.Core.JS


type [<AllowNullLiteral>] A =
    abstract alpha: float

type [<AllowNullLiteral>] B =
    abstract beta: string

type [<AllowNullLiteral>] C<'T> =
    abstract value: 'T

type [<AllowNullLiteral>] Or1<'T> =
    interface end

type [<AllowNullLiteral>] Or2<'T when 'T :> C<U2<A, B>>> =
    interface end