// ts2fable 0.0.0
module rec #289-recursive-merge-modules
open System
open Fable.Core
open Fable.Core.JS


module Outer =
    /// It should generate one module Outer.Inner with two interfaces C1 and C2
    let [<Import("Inner","#289-recursive-merge-modules/Outer")>] inner: Inner.IExports = jsNative

    /// It should generate one module Outer.Inner with two interfaces C1 and C2
    module Inner =

        type [<AllowNullLiteral>] IExports =
            abstract C1: C1Static
            abstract C2: C2Static

        type [<AllowNullLiteral>] C1 =
            interface end

        type [<AllowNullLiteral>] C1Static =
            [<EmitConstructor>] abstract Create: unit -> C1

        type [<AllowNullLiteral>] C2 =
            inherit C1

        type [<AllowNullLiteral>] C2Static =
            [<EmitConstructor>] abstract Create: unit -> C2
