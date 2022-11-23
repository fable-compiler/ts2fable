// ts2fable 0.0.0
module rec ``#423-invalid-du-case-name``
open System
open Fable.Core
open Fable.Core.JS


type [<StringEnum>] [<RequireQualifiedAccess>] CompletionsTriggerCharacter =
    | [<CompiledName("@")>] AT
    | [<CompiledName(".")>] DOT
    | [<CompiledName("+")>] PLUS
    | [<CompiledName("$")>] DOLLAR
    | [<CompiledName("&")>] AMPERSAND
    | [<CompiledName("[")>] LEFT_SQUARE_BRACKET
    | [<CompiledName("]")>] RIGHT_SQUARE_BRACKET
    | [<CompiledName("/")>] SLASH
    | [<CompiledName("\"")>] QUOTATION
    | [<CompiledName("\\")>] BACKSLASH
    | [<CompiledName("*")>] ASTERISK
    | [<CompiledName("`")>] BACKTICK
    | [<CompiledName("'")>] ``'``
    | [<CompiledName("<")>] ``<``
    | [<CompiledName("#")>] ``#``
    | [<CompiledName(" ")>] Empty
    | [<CompiledName("-")>] ``-``
    | [<CompiledName("_")>] ``_``
    | [<CompiledName("\t")>] TAB
    | [<CompiledName("\n")>] LF
    | [<CompiledName("\r\n")>] CRLF
    | [<CompiledName("////")>] SLASH_SLASH_SLASH_SLASH
    | [<CompiledName("\\\\\\")>] BACKSLASH_BACKSLASH_BACKSLASH
    | [<CompiledName("@\t\\\n")>] AT_TAB_BACKSLASH_LF
    | [<CompiledName("<@\t\\\n>")>] ``<_AT_TAB_BACKSLASH_LF_>``
    | [<CompiledName("A\"B\\C&D+E@F")>] A_QUOTATION_B_BACKSLASH_C_AMPERSAND_D_PLUS_E_AT_F

type [<StringEnum>] [<RequireQualifiedAccess>] Unescaped =
    | [<CompiledName("\"")>] QUOTATION

/// Unlike unions, enums can have special chars...
type [<RequireQualifiedAccess>] Chars =
    | AT = 0
    | ``.`` = 1
    | ``+`` = 2
    | ``$`` = 3
    | ``&`` = 4
    | ``[`` = 5
    | ``]`` = 6
    | ``/`` = 7
    | ``\"`` = 8
    | ``\\`` = 9
    | ``*`` = 10
    | BACKTICK = 11
    | ``'`` = 12
    | ``<`` = 13
    | ``#`` = 14
    | `` `` = 15
    | ``-`` = 16
    | ``_`` = 17
    | ``\t`` = 18
    | ``\n`` = 19
    | ``\r\n`` = 20
    | ``////`` = 21
    | ``\\\\\\`` = 22
    | ``AT_\t\\\n`` = 23
    | ``<_AT_\t\\\n>`` = 24
    | ``A\"B\\C&D+E_AT_F`` = 25


