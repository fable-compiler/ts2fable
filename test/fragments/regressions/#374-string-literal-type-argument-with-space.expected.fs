// ts2fable 0.8.0
module rec ``#374-string-literal-type-argument-with-space``
open System
open Fable.Core
open Fable.Core.JS


type [<AllowNullLiteral>] A =
    [<Emit "$0.on('Hello Space')">] abstract on_Hello_Space: unit -> unit
    [<Emit "$0.on('Hello	Tab')">] abstract on_Hello_Tab: unit -> unit
    [<Emit "$0.on('Hello Multiple Words')">] abstract on_Hello_Multiple_Words: unit -> unit
    [<Emit "$0.on('Hello   Multiple Spaces')">] abstract on_Hello___Multiple_Spaces: unit -> unit
    // [<Emit "$0.on('Hello☕Invalid')">] abstract ``on_Hello☕Invalid``: unit -> unit
    // [<Emit "$0.on('Hello ☕ Invalid With Spaces')">] abstract ``on_Hello_☕_Invalid_With_Spaces``: unit -> unit
    [<Emit "$0.on('(╯°□°）╯︵ ┻━┻')">] abstract ``on_(╯°□°）╯︵_┻━┻``: unit -> unit
    [<Emit "$0.on('post-require')">] abstract ``on_post-require``: unit -> unit

type [<StringEnum>] [<RequireQualifiedAccess>] S =
    | [<CompiledName "Foo">] Foo
    | [<CompiledName "Hello World">] ``Hello World``
    // | [<CompiledName "Hello☕">] ``Hello☕``
