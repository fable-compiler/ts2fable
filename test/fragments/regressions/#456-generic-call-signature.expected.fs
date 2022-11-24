// ts2fable 0.0.0
module rec ``#456-generic-call-signature``
open System
open Fable.Core
open Fable.Core.JS


type [<AllowNullLiteral>] Node =
    interface end

type [<AllowNullLiteral>] NodeVisitor =
    [<Emit("$0($1...)")>] abstract Invoke: nodes: 'T -> unit when 'T :> Node
    [<Emit("$0($1...)")>] abstract Invoke: nodes: 'T * test: ('T -> bool) -> unit when 'T :> Node
