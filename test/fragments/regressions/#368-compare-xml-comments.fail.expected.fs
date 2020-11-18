// ts2fable 0.8.0
module rec #368-compare-xml-comments.fail
open System
open Fable.Core
open Fable.Core.JS

// code comments are ignored

/// XML comments are compared -- additional text to fail
type [<AllowNullLiteral>] SomeInterface =
    interface end