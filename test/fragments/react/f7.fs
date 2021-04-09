// ts2fable 0.0.0
module rec f7
open System
open Fable.Core
open Fable.Core.JS

type SyntheticEvent = React.SyntheticEvent

type [<AllowNullLiteral>] EventHandler<'E when 'E :> SyntheticEvent<obj option>> =
    [<Emit "$0($1...)">] abstract Invoke: ``event``: 'E -> unit
