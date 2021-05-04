// ts2fable 0.0.0
module rec ``#301-union-with-parens``

#nowarn "3390" // disable warnings for invalid XML comments

open System
open Fable.Core
open Fable.Core.JS


/// <summary>everything must be <c>string option</c></summary>
type [<AllowNullLiteral>] I =
    abstract fReturn1: unit -> string option
    abstract fReturn2: unit -> string option
    abstract fReturn3: unit -> string option
    abstract fReturn4: unit -> string option
    abstract fParam1: v: string option -> unit
    abstract fParam2: v: string option -> unit
    abstract fParam3: v: string option -> unit
    abstract fParam4: v: string option -> unit
