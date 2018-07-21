// ts2fable 0.0.0
module rec variableInModule
open System
open Fable.Core
open Fable.Import.JS

let [<Import("*","test")>] monaco: Monaco.IExports = jsNative

module Monaco =

    type [<AllowNullLiteral>] IExports =
        abstract EditorType: obj

    type [<AllowNullLiteral>] IDisposable =
        abstract dispose: unit -> unit

    type [<AllowNullLiteral>] IEvent<'T> =
        [<Emit "$0($1...)">] abstract Invoke: listener: ('T -> obj option) * ?thisArg: obj option -> IDisposable
