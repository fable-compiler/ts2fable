// ts2fable 0.8.0
module rec #368-compare-xml-comments.pass
open System
open Fable.Core
open Fable.Core.JS

// code comments are ignored

/// XML comments are compared
type [<AllowNullLiteral>] SomeInterface =
    interface end