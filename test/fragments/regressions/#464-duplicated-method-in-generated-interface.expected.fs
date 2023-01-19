// ts2fable 0.0.0
module rec ``#464-duplicated-method-in-generated-interface``
open System
open Fable.Core
open Fable.Core.JS


module Ns =

    type [<AllowNullLiteral>] IExports =
        abstract node: IExportsNode

    type [<AllowNullLiteral>] IExportsNode =
        [<Emit("$0($1...)")>] abstract Invoke: arg: string -> bool
