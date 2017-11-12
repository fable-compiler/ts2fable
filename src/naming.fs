module ts2fable.Naming

open System

let (|Capital|_|) (letter: string)= 
    let capitals = [ 'A' .. 'Z' ] |> Seq.map string
    match Seq.contains letter capitals with
    | true -> Some letter
    | false -> None

let private digits = [ '0' .. '9' ] |> Seq.map string

// TODO Char.IsDigit will be in Fable 1.3
let isDigit digit = Seq.contains digit digits

let (|Digit|_|) (digit: string) = 
    match isDigit digit with
    | true -> Some digit
    | false -> None

let createEnumNameParts (name: string) = 
    let tokens = Seq.map string name
    let rec splitParts part parts  = 
        function
        | [] -> part :: parts
        | Digit n :: rest when 
            // Append N to beginning only if enum starts with digit
            List.isEmpty parts
            && part = "" -> splitParts ("N" + n) parts rest 
        | Capital letter :: rest when part = "" -> splitParts letter parts rest
        | Capital letter :: rest -> splitParts letter (part :: parts) rest
        | "-" :: rest -> splitParts "" (part :: parts) rest
        | token :: rest ->  splitParts (part + token) parts rest
    tokens 
    |> List.ofSeq
    |> splitParts "" []  
    |> List.rev

let capitalize (input: string): string =
    if String.IsNullOrWhiteSpace input then ""
    // else sprintf "%c%s" (Char.ToUpper input.[0]) (input.Substring 1) // Fable 1.2.3 bug Char.ToUpper not supported
    else sprintf "%s%s" (input.Substring(0,1).ToUpper()) (input.Substring 1)

let createEnumName s =
    if String.IsNullOrWhiteSpace s then "Empty"
    else s |> createEnumNameParts |> List.map capitalize |> String.concat ""

// by default Fable lowercases the first letter of the name for the value
let nameEqualsDefaultFableValue (name: string) (value: string): bool =
    let defaultFableValue =
        sprintf "%s%s" 
            (name.Substring(0,1).ToLower())
            (name.Substring(1))
    defaultFableValue.Equals value

let createModuleNameParts (name: string) = 

    let tokens = Seq.map string name
    let rec splitParts part parts  = 
        function
        | [] -> part :: parts
        | "-" :: rest -> splitParts "" (part :: parts) rest
        | "/" :: rest -> splitParts "" (part :: parts) rest
        | "." :: rest -> splitParts "" (part :: parts) rest
        | token :: rest ->  splitParts (part + token) parts rest
    tokens 
    |> List.ofSeq
    |> splitParts "" []  
    |> List.rev

let escapeWord (s: string) =
    if String.IsNullOrEmpty s then ""
    else
        let s = s.Replace("'","") // remove single quotes
        if Keywords.reserved.Contains s 
            || Keywords.keywords.Contains s
            || s.IndexOfAny [|'-';'/';'$'|] <> -1 // invalid chars
            // || Char.IsNumber s.[0] then // TODO Fable 1.3
            || isDigit (s.Substring(0,1))
            || s.Substring(0,1).Equals "."
        then
            sprintf "``%s``" s
        else
            s

// TODO
let fixModuleName (s: string) =
    let s = s.Replace("'","") // remove single quotes
    let parts = s |> createModuleNameParts
    // [parts.Head] @ (parts.Tail |> List.map Enum.capitalize) |> String.concat ""
    parts |> String.concat "_"

let removeQuotes (s:string): string =
    if isNull s then ""
    else s.Replace("\"","").Replace("'","")