// ts2fable 0.0.0
module rec ``#304-reserved-keywords``
open System
open Fable.Core
open Fable.Core.JS


/// <summary>
/// Escape all reserved keywords.
/// 
/// Some previously reserved keywords, aren't reserved any more.
/// See <see href="https://github.com/fsharp/fslang-design/blob/master/FSharp-4.1/FS-1016-unreserve-keywords.md">RFC</see>, <see href="https://github.com/dotnet/fsharp/pull/1279">PR</see>
/// </summary>
type [<AllowNullLiteral>] I =
    abstract ``asr``: string with get, set
    abstract ``land``: string with get, set
    abstract ``lor``: string with get, set
    abstract ``lsl``: string with get, set
    abstract ``lsr``: string with get, set
    abstract ``lxor``: string with get, set
    abstract ``mod``: string with get, set
    abstract ``sig``: string with get, set
    abstract ``break``: string with get, set
    abstract ``checked``: string with get, set
    abstract ``component``: string with get, set
    abstract ``const``: string with get, set
    abstract ``constraint``: string with get, set
    abstract ``continue``: string with get, set
    abstract ``event``: string with get, set
    abstract ``external``: string with get, set
    abstract ``include``: string with get, set
    abstract ``mixin``: string with get, set
    abstract ``parallel``: string with get, set
    abstract ``process``: string with get, set
    abstract ``protected``: string with get, set
    abstract ``pure``: string with get, set
    abstract ``sealed``: string with get, set
    abstract ``tailcall``: string with get, set
    abstract ``trait``: string with get, set
    abstract ``virtual``: string with get, set
    /// Not reserved any more
    abstract atomic: string with get, set
    /// Not reserved any more
    abstract constructor: string with get, set
    /// Not reserved any more
    abstract eager: string with get, set
    /// Not reserved any more
    abstract functor: string with get, set
    /// Not reserved any more
    abstract measure: string with get, set
    /// Not reserved any more
    abstract method: string with get, set
    /// Not reserved any more
    abstract object: string with get, set
    /// Not reserved any more
    abstract recursive: string with get, set
    /// Not reserved any more
    abstract volatile: string with get, set

