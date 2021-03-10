// ts2fable 0.0.0
module rec ``#386-TS-types``
open System
open Fable.Core
open Fable.Core.JS

type ArrayLike<'T> = System.Collections.Generic.IList<'T>
type PromiseLike<'T> = Fable.Core.JS.Promise<'T>


type [<AllowNullLiteral>] I =
    abstract a: v: PromiseLike<string> -> PromiseLike<float>
    abstract b: v: ArrayLike<string> -> ArrayLike<float>
