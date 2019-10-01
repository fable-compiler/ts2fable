// ts2fable 0.0.0
module rec ``#289-recursive-merge-modules``
open System
open Fable.Core
open Fable.Import.JS


module Outer =
    let [<Import("Inner","test/Outer")>] inner: Inner.IExports = jsNative

    module Inner =

        type [<AllowNullLiteral>] IExports =
            abstract C1: C1Static
            abstract C2: C2Static

        type [<AllowNullLiteral>] C1 =
            interface end

        type [<AllowNullLiteral>] C1Static =
            [<Emit "new $0($1...)">] abstract Create: unit -> C1

        type [<AllowNullLiteral>] C2 =
            inherit C1

        type [<AllowNullLiteral>] C2Static =
            [<Emit "new $0($1...)">] abstract Create: unit -> C2
