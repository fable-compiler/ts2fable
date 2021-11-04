// ts2fable 0.0.0
module rec variableInModule
open System
open Fable.Core
open Fable.Core.JS

let [<ImportAll("variableInModule")>] monaco: Monaco.IExports = jsNative

module Monaco =

    type [<AllowNullLiteral>] IExports =
        abstract EditorType: IExportsEditorType with get, set

    type [<AllowNullLiteral>] IDisposable =
        abstract dispose: unit -> unit

    type [<AllowNullLiteral>] IEvent<'T> =
        [<Emit "$0($1...)">] abstract Invoke: listener: ('T -> obj option) * ?thisArg: obj -> IDisposable

    type [<AllowNullLiteral>] IExportsEditorType =
        abstract ICodeEditor: string with get, set
        abstract IDiffEditor: string with get, set
