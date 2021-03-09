// ts2fable 0.8.0
module rec ``#368-compare-xml-comments.pass``
open System
open Fable.Core
open Fable.Core.JS

// code comments are ignored

/// XML comments are compared
type [<AllowNullLiteral>] SomeInterface =
    // indented comments are ignored

    /// Indented XML comments are compared
    abstract a: unit with get, set
