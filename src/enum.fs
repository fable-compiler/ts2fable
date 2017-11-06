module ts2fable.Enum

open System

let (|Capital|_|) (letter: string)= 
    let capitals = [ 'A' .. 'Z' ] |> Seq.map string
    match Seq.contains letter capitals with
    | true -> Some letter
    | false -> None

let (|Digit|_|) (digit: string) = 
    let capitals = [ '0' .. '9' ] |> Seq.map string
    match Seq.contains digit capitals with
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

// TODO make into unit tests
// createEnumNameParts "simpleCase" // ["simple"; "Case"]
// createEnumNameParts "simple-kebab-case" // ["simple"; "kebab"; "case"]
// createEnumNameParts "Other-Cases" // ["Other"; "Cases"]
// createEnumNameParts "helloThereItsMe" // ["hello"; "There"; "Its"; "Me"]
// createEnumNameParts "OtherSimpleCase" // ["Other";"Simple"; "Case"]
// createEnumNameParts "2d-shadow" // ["N2d"; "shadow"]
// createEnumNameParts "shadow-2d" // ["shadow"; "2d"]
// createEnumNameParts "shadow-2d-more" // ["shadow"; "2d"; "more"]

let capitalize (input: string): string =
    if String.IsNullOrWhiteSpace input then ""
    // else sprintf "%c%s" (Char.ToUpper input.[0]) (input.Substring 1) // Fable 1.2.3 bug Char.ToUpper not supported
    else sprintf "%s%s" (input.Substring(0,1).ToUpper()) (input.Substring 1)

let createEnumName s =
    if String.IsNullOrWhiteSpace s then "Empty"
    else s |> createEnumNameParts |> List.map capitalize |> String.concat ""