// ts2fable 0.8.0
module rec simple
open System
open Fable.Core
open Fable.Core.JS


type [<AllowNullLiteral>] A =
    abstract alpha: float

type [<AllowNullLiteral>] B =
    abstract beta: string

type [<AllowNullLiteral>] C<'T> =
    abstract value: 'T

type [<AllowNullLiteral>] Simple1<'T when 'T :> A> =
    interface end

type [<AllowNullLiteral>] Simple2<'T when 'T :> C<A>> =
    interface end

type [<AllowNullLiteral>] Simple3<'T, 'U when 'T :> A and 'U :> B> =
    interface end

type [<AllowNullLiteral>] Simple4<'T, 'U when 'T :> A and 'U :> C<'T>> =
    interface end