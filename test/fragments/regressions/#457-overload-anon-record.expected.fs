// ts2fable 0.0.0
module rec ``#457-overload-anon-record``
open System
open Fable.Core
open Fable.Core.JS

let [<Import("createStringLiteral","#457-overload-anon-record")>] createStringLiteral: CreateStringLiteral = jsNative

type [<AllowNullLiteral>] StringLiteral =
    abstract kind: string

type [<AllowNullLiteral>] CreateStringLiteral =
    [<Emit("$0($1...)")>] abstract Invoke: text: string * ?isSingleQuote: bool -> StringLiteral
    [<Emit("$0($1...)")>] abstract Invoke: text: string * ?isSingleQuote: bool * ?hasExtendedUnicodeEscape: bool -> StringLiteral
