module ts2fable.Keywords

let reserved =
    [
        "break"
        "checked"
        "component"
        "const"
        "constraint"
        "continue"
        "event"
        "external"
        "fixed"
        "include"
        "mixin"
        "parallel"
        "params"
        "process"
        "protected"
        "pure"
        "sealed"
        "tailcall"
        "trait"
        "virtual"
        "asr"
        "land"
        "lor"
        "lsl"
        "lsr"
        "lxor"
        "mod"
        "sig"
    ]
    |> Set.ofList

let keywords =
    [
        "abstract"
        "and"
        "as"
        "assert"
        "base"
        "begin"
        "class"
        "default"
        "delegate"
        "do"
        "done"
        "downcast"
        "downto"
        "elif"
        "else"
        "end"
        "exception"
        "extern"
        "false"
        "finally"
        "for"
        "fun"
        "function"
        "global"
        "if"
        "in"
        "inherit"
        "inline"
        "interface"
        "internal"
        "lazy"
        "let"
        "match"
        "member"
        "module"
        "mutable"
        "namespace"
        "new"
        "null"
        "of"
        "open"
        "or"
        "override"
        "private"
        "public"
        "rec"
        "return"
        "sig"
        "static"
        "struct"
        "then"
        "to"
        "true"
        "try"
        "type"
        "upcast"
        "use"
        "val"
        "void"
        "when"
        "while"
        "with"
        "yield"
    ]
    |> Set.ofList

let esKeywords =
    [
        "Readonly"
        "Partial"
        "Pick"
        "HTMLDialogElement"
        "HTMLWebViewElement"
    ]
    |> Set.ofList