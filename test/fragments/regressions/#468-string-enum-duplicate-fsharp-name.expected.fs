// ts2fable 0.0.0
module rec ``#468-string-enum-duplicate-fsharp-name``
open System
open Fable.Core
open Fable.Core.JS


/// Keep 1st case
type [<StringEnum>] [<RequireQualifiedAccess>] A =
    | Utf8
    | [<CompiledName("utf-8")>] ``utf-8``

/// Keep 2nd case
type [<StringEnum>] [<RequireQualifiedAccess>] B =
    | [<CompiledName("utf-8")>] ``utf-8``
    | Utf8

type [<StringEnum>] [<RequireQualifiedAccess>] C =
    | [<CompiledName("utf8")>] ``utf8``
    | [<CompiledName("Utf8")>] Utf8
    | [<CompiledName("UTF8")>] UTF8
    | [<CompiledName("utf-8")>] ``utf-8``
    | [<CompiledName("UTF-8")>] ``UTF-8``

type [<StringEnum>] [<RequireQualifiedAccess>] BufferEncoding =
    | Ascii
    | Utf8
    | [<CompiledName("utf-8")>] ``utf-8``
    | Utf16le
    | Ucs2
    | [<CompiledName("ucs-2")>] ``ucs-2``
    | Base64
    | Latin1
    | Binary
    | Hex
