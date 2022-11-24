export type CompletionsTriggerCharacter = 
  | "@"
  | "."
  | "+"
  | "$"
  | "&"
  | "["
  | "]"
  | "/"
  | "\""
  | "\\"
  | "*"
  | "`"

  | "'" 
  | "<" 
  | "#" 
  | " " 
  | "-"
  | "_"

  | "\t"
  | "\n"
  | "\r\n"

  | "////" 
  | "\\\\\\" 
  | "@\t\\\n"
  | "<@\t\\\n>"

  | "A\"B\\C&D+E@F"
  ; 

export type Unescaped =
    // Note: above in double-quotations is escaped, while here it isn't escaped!
  | '"'

/**
 * Unlike unions, enums can have special chars...
 */
declare enum Chars {
    "@",
    ".",
    "+",
    "$",
    "&",
    "[",
    "]",
    "/",
    "\"",
    "\\",
    "*",
    "`",
    "'",
    "<",
    "#",
    " ",
    "-",
    "_",
    "\t",
    "\n",
    "\r\n",
    "////",
    "\\\\\\",
    "@\t\\\n",
    "<@\t\\\n>",
    "A\"B\\C&D+E@F"
}

