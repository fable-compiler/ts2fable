// ts2fable 0.8.0
module rec ``default``
open System
open Fable.Core
open Fable.Core.JS


type [<AllowNullLiteral>] A =
    abstract alpha: float

type [<AllowNullLiteral>] B =
    abstract beta: string

type [<AllowNullLiteral>] AB =
    inherit A
    inherit B

type [<AllowNullLiteral>] C<'T> =
    abstract value: 'T

type D1 =
    D1<AB>

type [<AllowNullLiteral>] D1<'T when 'T :> A and 'T :> B> =
    interface end

type D2 =
    D2<C<A>>

type [<AllowNullLiteral>] D2<'T when 'T :> C<A>> =
    interface end

type D3 =
    D3<obj>

type [<AllowNullLiteral>] D3<'T> =
    interface end