// ts2fable 0.8.0
module rec sealed
open System
open Fable.Core
open Fable.Core.JS

type Function = System.Action


type [<AllowNullLiteral>] A =
    abstract alpha: float

type [<AllowNullLiteral>] B =
    abstract beta: string

type [<AllowNullLiteral>] C<'T> =
    abstract value: 'T

type [<AllowNullLiteral>] Sealed1<'T> =
    interface end

type [<AllowNullLiteral>] Sealed2<'T> =
    interface end

type [<AllowNullLiteral>] Sealed3<'T> =
    interface end

type [<AllowNullLiteral>] Sealed4<'T when 'T :> C<Function>> =
    interface end

type [<AllowNullLiteral>] Sealed5<'T> =
    interface end

type [<AllowNullLiteral>] Sealed6<'T when 'T :> A> =
    interface end

type [<AllowNullLiteral>] Sealed7<'T when 'T :> A> =
    interface end

type [<AllowNullLiteral>] Sealed8<'T when 'T :> A and 'T :> B> =
    interface end