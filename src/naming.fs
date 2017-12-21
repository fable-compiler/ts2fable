module ts2fable.Naming

open System
open Node
open System.Text.RegularExpressions
let (|Capital|_|) (letter: string)= 
    let capitals = [ 'A' .. 'Z' ] |> Seq.map string
    match Seq.contains letter capitals with
    | true -> Some letter
    | false -> None
    // Char.IsUpper https://github.com/fable-compiler/Fable/issues/1236
    // if letter.Length = 1 && Char.IsUpper letter.[0] then Some letter else None

let private digits = [ '0' .. '9' ] |> Seq.map string

let isDigit digit = Seq.contains digit digits

let (|Digit|_|) (digit: string) = 
    match isDigit digit with
    | true -> Some digit
    | false -> None
    // Char.IsNumber https://github.com/fable-compiler/Fable/issues/1237
    // if digit.Length = 1 && Char.IsNumber digit.[0] then Some digit else None

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
    else sprintf "%c%s" (Char.ToUpper input.[0]) (input.Substring 1)

let lowerFirst (input: string): string =
    if String.IsNullOrWhiteSpace input then ""
    else sprintf "%c%s" (Char.ToLower input.[0]) (input.Substring 1)

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
            // || Char.IsNumber s.[0] then // TODO https://github.com/fable-compiler/Fable/issues/1237
            || isDigit (s.Substring(0,1))
            || s.Substring(0,1).IndexOfAny [|'.';'['|] <> -1 // starts with
            || s.Equals "_"
        then
            sprintf "``%s``" s
        else
            s
let fixImportEqualModuleName (s:string) = 
    s.Replace("'","").Replace("require(","").Replace(")","")

let fixModuleName (s: string) =
    let s =  s |> fixImportEqualModuleName // remove single quotes
    let s = capitalize s
    let s =
        // let parts = s |> createModuleNameParts
        let parts = s |> createModuleNameParts |> Seq.map capitalize |> Seq.skipWhile (fun s -> s ="")
        parts |> String.concat "."
    let s =
        if Keywords.reserved.Contains s || Keywords.keywords.Contains s then
            sprintf "%s_" s
        else s
    s

let fixImportTypeName (s:string) =
    let (|Regex|_|) pattern input =
        let m = Regex.Match(input, pattern)
        if m.Success then Some(m.Index)
        else None
    match s with 
    | Regex "\\bas\\b" ias-> s.Substring(0,ias-1),s.Substring(ias+3)
    | _ -> s,s
let removeQuotes (s:string): string =
    if isNull s then ""
    else s.Replace("\"","").Replace("'","")

// gets the JavaScript module name
// intended for use by SourceFile.fileName which has slashes normalized
// TODO implement https://github.com/ajafff/tsutils/issues/14#issuecomment-345544684
let getJsModuleName (fileName: string): string =
    let inm = fileName.LastIndexOf "node_modules"
    
    let path =
        if inm = -1 then fileName
        else fileName.Substring(inm+13)

    // TODO scoped & submodules
    let last =
        path.Split '/'
        |> List.ofArray
        |> List.filter (fun s -> s <> "index.d.ts")
        |> List.last

    last.Replace(".ts","").Replace(".d","")

let primatives = ["string"; "obj"; "unit"; "float"; "bool"] |> Set.ofList

// espects a type where the namespace is simply dot separated
let fixNamespaceString (name: string): string =
    if primatives.Contains name then
        name
    else
        let parts = name.Split [|'.'|]
        if parts.Length = 1 then
            name
        else
            let parts = parts |> List.ofArray |> List.rev
            let parts = [parts.Head] @ parts.Tail |> List.map fixModuleName
            parts |> List.rev |> String.concat "."
let automaticNamespace (fileName: string) (subPath: string)   : string =
    let subPath = subPath.Trim()

    let directoryName = path.dirname fileName
    
    let inm = directoryName.LastIndexOf "node_modules"

    let path =
        if inm = -1 then directoryName
        else directoryName.Substring(inm+13)
    
    let parts = path.Split '/' |> List.ofArray |> List.filter (fun s -> s <> subPath)

    let parts = parts |> List.map (capitalize >> fixModuleName) 
    
    parts |> String.concat "."            