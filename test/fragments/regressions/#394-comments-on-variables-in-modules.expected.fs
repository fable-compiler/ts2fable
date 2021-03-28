// ts2fable 0.0.0
module rec ``#394-comments-on-variables-in-modules``
open System
open Fable.Core
open Fable.Core.JS

/// Comment on module
let [<Import("M","#394-comments-on-variables-in-modules")>] m: M.IExports = jsNative
/// Comment on namespace
let [<Import("N","#394-comments-on-variables-in-modules")>] n: N.IExports = jsNative

/// Comment on module
module M =

    type [<AllowNullLiteral>] IExports =
        /// var
        abstract v1: float with get, set
        /// let
        abstract l1: float with get, set
        /// const
        abstract c1: float

/// Comment on namespace
module N =

    type [<AllowNullLiteral>] IExports =
        /// var
        abstract v1: float with get, set
        /// let
        abstract l1: float with get, set
        /// const
        abstract c1: float
