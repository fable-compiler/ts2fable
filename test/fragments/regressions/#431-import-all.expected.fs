// ts2fable 0.0.0
module rec ``#431-import-all``
open System
open Fable.Core
open Fable.Core.JS

/// Import *
let [<ImportAll("#431-import-all")>] myModule: MyModule.IExports = jsNative
/// Import named
let [<Import("numberOfStickers","#431-import-all")>] numberOfStickers: float = jsNative

/// Import *
module MyModule =

    type [<AllowNullLiteral>] IExports =
        abstract DoStuff: value: string -> unit
