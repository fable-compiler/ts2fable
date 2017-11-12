module rec ts2fable.Transform

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import.Node
open Fable.Import.TypeScript
open Fable.Import.TypeScript.ts
open System.Collections.Generic
open System
open ts2fable.Naming

/// recursively fix all the FsType childen
let rec fixType (fix: FsType -> FsType) (tp: FsType): FsType =

    let fixModule (a: FsModule): FsModule =
        { a with
            Types = a.Types |> List.map (fixType fix)
        }

    let fixParam (a: FsParam): FsParam =
        let b =
            { a with
                Type = fixType fix a.Type
            }
            |> FsType.Param |> fix
        match b with
        | FsType.Param c -> c
        | _ -> failwithf "param must be mapped to param"

    // fix children first, then curren type
    match tp with
    | FsType.Interface it ->
        { it with
            TypeParameters = it.TypeParameters |> List.map (fixType fix)
            Inherits = it.Inherits |> List.map (fixType fix)
            Members = it.Members |> List.map (fixType fix)
        }
        |> FsType.Interface
    | FsType.Property pr ->
        { pr with
            Index = Option.map fixParam pr.Index
            Type = fixType fix pr.Type
        }
        |> FsType.Property 
    | FsType.Param pr ->
        { pr with
            Type = fixType fix pr.Type
        }
        |> FsType.Param
    | FsType.Array ar ->
        fixType fix ar |> FsType.Array
    | FsType.Function fn ->
        { fn with
            TypeParameters = fn.TypeParameters |> List.map (fixType fix)
            Params = fn.Params |> List.map fixParam
            ReturnType = fixType fix fn.ReturnType
        }
        |> FsType.Function
    | FsType.Union un ->
        { un with
            Types = un.Types |> List.map (fixType fix)
        }
        |> FsType.Union
    | FsType.Alias al ->
        { al with
            Type = fixType fix al.Type
            TypeParameters = al.TypeParameters |> List.map (fixType fix)
        }
        |> FsType.Alias
    | FsType.Generic gn ->
        { gn with
            Type = fixType fix gn.Type
            TypeParameters = gn.TypeParameters |> List.map (fixType fix)
        }
        |> FsType.Generic
    | FsType.Tuple tp ->
        { tp with
            Types = tp.Types |> List.map (fixType fix)
        }
        |> FsType.Tuple
    | FsType.Module md ->
        fixModule md |> FsType.Module
     | FsType.File f ->
        { f with
            Modules = f.Modules |> List.map fixModule
        }
        |> FsType.File
    | FsType.Variable vb ->
        { vb with
            Type = fixType fix vb.Type
        }
        |> FsType.Variable
    | FsType.Import im ->
        { im with
            Type = fixType fix im.Type
        }
        |> FsType.Import

    | FsType.Enum _ -> tp
    | FsType.Mapped _ -> tp
    | FsType.None _ -> tp
    | FsType.TODO _ -> tp
    | FsType.StringLiteral _ -> tp
    | FsType.This -> tp

    |> fix // current type

/// recursively fix all the FsType childen for the given FsFile
let fixFile (fix: FsType -> FsType) (f: FsFile): FsFile =
    { f with
        Modules = 
            f.Modules 
            |> List.map FsType.Module 
            |> List.map (fixType fix) 
            |> List.choose asModule
    }


let mergeTypes(tps: FsType list): FsType list =
    let index = Dictionary<string,int>()
    let list = List<FsType>()
    for b in tps do
        match b with
        | FsType.Interface bi ->
            if index.ContainsKey bi.Name then
                let i = index.[bi.Name]
                let a = list.[i]
                match a with
                | FsType.Interface ai ->
                    list.[i] <-
                        { ai with
                            Inherits = List.append ai.Inherits bi.Inherits |> List.distinct
                            Members = List.append ai.Members bi.Members
                        }
                        |> FsType.Interface
                | _ -> ()

            else
                list.Add b
                index.Add(bi.Name, list.Count-1)
        | _ -> 
            list.Add b
    list |> List.ofSeq

let mergeModules(tps: FsType list): FsType list =
    let index = Dictionary<string,int>()
    let list = List<FsType>()

    for tp in tps do
        match tp with
        | FsType.Module md ->
            let md2 =
                { md with
                    Types = md.Types |> mergeTypes |> mergeModules // submodules
                }
            
            if index.ContainsKey md.Name then
                let i = index.[md.Name]
                let a = (list.[i] |> asModule).Value
                list.[i] <-
                    { a with
                        Types = a.Types @ md2.Types |> mergeTypes
                    }
                    |> FsType.Module
            else
                md2 |> FsType.Module |> list.Add |> ignore
                index.Add(md2.Name, list.Count-1)
        | _ -> list.Add tp |> ignore
    
    list |> List.ofSeq

let mergeModulesInFile (f: FsFile): FsFile =
    { f with
        Modules = 
            f.Modules
            |> List.ofSeq
            |> List.map FsType.Module
            |> mergeModules
            |> List.choose asModule
    }

let createIExports (f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Module md ->

            let tps = md.Types |> List.collect(fun t ->
                match t with
                | FsType.Variable vb ->
                    if vb.HasDeclare then []
                    else [t]
                | FsType.Function _ -> [t]
                | FsType.Interface it ->
                    if it.IsStatic then
                        [
                            // add a property for accessing the static class
                            {
                                Comments = []
                                Emit = None
                                Index = None
                                Name = it.Name.Replace("Static","")
                                Option = false
                                Type = it.Name |> FsType.Mapped
                            }
                            |> FsType.Property
                        ]
                    else []
                | _ -> []
            )
            if tps.Length = 0 then
                tp
            else
                let cl: FsInterface =
                    {
                        Comments = []
                        IsStatic = false
                        Name = "IExports"
                        Inherits = []
                        TypeParameters = []
                        Members = tps
                    }
                { md with
                    Types = [ FsType.Interface cl ] @ md.Types
                }
                |> FsType.Module
        | _ -> tp
    )

let fixTic (typeParameters: FsType list) (tp: FsType) =
    if typeParameters.Length = 0 then
        tp
    else
        let set = typeParameters |> Set.ofList
        let fix (t: FsType): FsType =
            match t with
            | FsType.Mapped s ->
                if set.Contains t then
                    sprintf "'%s" s |> FsType.Mapped
                else t
            | _ -> t
        fixType fix tp

let addTicForGenericFunctions(f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Interface it ->
            { it with
                Members = it.Members |> List.map (fun mbr ->
                    match asFunction mbr with
                    | None -> mbr
                    | Some fn -> fixTic fn.TypeParameters mbr
                )
            }
            |> FsType.Interface
        | _ -> tp
    )

// https://github.com/Microsoft/TypeScript/blob/master/doc/spec.md#18-overloading-on-string-parameters
let fixOverloadingOnStringParameters(f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Function fn ->
            if fn.Params.Length > 0 && isStringLiteralParam fn.Params.[0] then
                let p0 = fn.Params.[0]
                let p0sl = (asStringLiteral p0.Type).Value
                { fn with
                    Emit = sprintf "$0.%s('%s',$1...)" fn.Name.Value p0sl |> Some
                    Name = sprintf "%s_%s" fn.Name.Value p0sl |> Some
                    Params = fn.Params.[1..]
                }
                |> FsType.Function
            else tp
        | _ -> tp
    )

let fixNodeArray(f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Generic gn ->
            match gn.Type with
            | FsType.Mapped s ->
                if s.Equals "NodeArray" && gn.TypeParameters.Length = 1 then
                    gn.TypeParameters.[0] |> FsType.Array
                else tp
            | _ -> tp
        | _ -> tp
    )

let escapeWord (s: string) =
    if String.IsNullOrEmpty s then ""
    else
        let s = s.Replace("'","") // remove single quotes
        if Keywords.reserved.Contains s 
            || Keywords.keywords.Contains s
            || s.IndexOfAny [|'-';'/';'$'|] <> -1 // invalid chars
            // || Char.IsNumber s.[0] then // TODO Fable 1.3
            || isDigit (s.Substring(0,1)) then
            sprintf "``%s``" s
        else
            s

// TODO
let fixModuleName (s: string) =
    let s = s.Replace("'","") // remove single quotes
    let parts = s |> createModuleNameParts
    // [parts.Head] @ (parts.Tail |> List.map Enum.capitalize) |> String.concat ""
    parts |> String.concat "_"

let fixEscapeWords(f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Mapped s ->
            escapeWord s |> FsType.Mapped
        | FsType.Param pm ->
            { pm with Name = escapeWord pm.Name } |> FsType.Param
        | FsType.Function fn ->
            { fn with Name = fn.Name |> Option.map escapeWord } |> FsType.Function
        | FsType.Property pr ->
            { pr with Name = escapeWord pr.Name } |> FsType.Property
        | FsType.Interface it ->
            { it with Name = escapeWord it.Name } |> FsType.Interface
        | FsType.Module md ->
            // can't just escape module names
            { md with Name = fixModuleName md.Name } |> FsType.Module
        | FsType.Variable vb ->
            { vb with Name = escapeWord vb.Name } |> FsType.Variable
        | FsType.Alias al ->
            { al with Name = escapeWord al.Name } |> FsType.Alias
        | _ -> tp
    )

let fixDateTime(f: FsFile): FsFile =
    let replaceName name =
        if String.Equals("Date", name) then "DateTime" else name

    f |> fixFile (fun tp ->
        match tp with
        | FsType.Mapped s ->
            replaceName s |> FsType.Mapped
        | _ -> tp
    )

let fixEnumReferences (f: FsFile): FsFile =
    // get a list of enum names
    let list = List<string>()
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Enum en ->
            list.Add en.Name |> ignore
            tp
        | _ -> tp
    ) |> ignore

    // use those as the references
    let set = Set.ofSeq list
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Mapped s ->
            if s.Contains "." then
                let nm = s.Substring(0, s.IndexOf ".")
                if set.Contains nm then
                    FsType.Mapped nm
                else tp
            else tp
        | _ -> tp
    )

let fixDuplicatesInUnion (f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Union un ->
            let set = HashSet<_>()
            let tps = un.Types |> List.choose (fun tp -> 
                    if set.Contains tp then
                        None
                    else
                        set.Add tp |> ignore
                        Some tp
                )
            if tps.Length > 6 then
                // add U7 and U8 union types https://github.com/fable-compiler/Fable/issues/1211
                FsType.Mapped "obj"
            else 
                { un with Types = tps } |> FsType.Union
        | _ -> tp
    )

let addTicForGenericTypes(f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Interface it -> fixTic it.TypeParameters tp
        | FsType.Alias al -> fixTic al.TypeParameters tp
        | _ -> tp
    )

/// add a namespace to import
/// and convert `declare` variables to imports
let rec fixImport (ns: string list) (md: FsModule): FsModule =
    let newNs = if String.IsNullOrEmpty md.Name then ns else ns @ [md.Name]
    { md with
        Types = md.Types |> List.map (fun tp ->
            match tp with
            | FsType.Module submd ->
                fixImport newNs submd |> FsType.Module
            | FsType.Import ep ->
                { ep with Namespace = newNs } |> FsType.Import
            | FsType.Variable vb ->
                if vb.HasDeclare then
                    {
                        Namespace = newNs
                        Variable = vb.Name
                        Type = vb.Type
                    }
                    |> FsType.Import
                else
                    vb  |> FsType.Variable
            | _ -> tp
        )
    }

/// replaces `this` with a reference to the interface type
let fixThis(f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Interface it ->
            { it with
                Members = it.Members |> List.map (fun mbr -> 
                    match mbr with
                    | FsType.Function f ->
                        { f with
                            ReturnType =
                                match f.ReturnType with
                                | FsType.This ->
                                    {
                                        Type = FsType.Mapped it.Name
                                        TypeParameters = it.TypeParameters
                                    }
                                    |> FsType.Generic
                                | _ -> f.ReturnType
                        }
                        |> FsType.Function
                    | _ -> mbr
                )
            }
            |> FsType.Interface
        | _ -> tp
    )

let fixStatic(f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Module md ->
            { md with
                Types = md.Types |> List.collect (fun tp2 ->
                    match tp2 with
                    | FsType.Interface it ->
                        if it.HasStaticMembers then
                            [
                                { it with
                                    Members = it.NonStaticMembers
                                }
                                |> FsType.Interface

                                { it with
                                    IsStatic = true
                                    Name = sprintf "%sStatic" it.Name
                                    Inherits = []
                                    Members = it.StaticMembers
                                }
                                |> FsType.Interface
                            ]
                        else
                            [tp2]
                    | _ -> [tp2]
                )
            }
            |> FsType.Module
        | _ -> tp
    )

let fixOpens(f: FsFile): FsFile =

    let isBrowser (name: string): bool =
        if isNull name then false
        else name.StartsWith "HTML"

    let mutable hasBrowser = false

    let fix(tp: FsType): FsType =
        match tp with
        | FsType.Mapped s ->
            if isBrowser s then
                hasBrowser <- true
            tp
        | _ -> tp

    f |> FsType.File |> fixType fix |> ignore

    { f with
        Opens =
            if hasBrowser then f.Opens @ ["Fable.Import.Browser"]
            else f.Opens
    }

let hasTodo (tp: FsType) =
    let mutable has = false
    tp |> fixType (fun t ->
        match t with
        | FsType.TODO ->
            has <- true
            t
        | _ -> t
    ) |> ignore
    has

let removeTodoMembers(f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Interface it ->
            { it with
                // Members = it.Members |> List.filter (not << hasTodo)
                Members = it.Members |> List.filter (fun mb ->
                    if hasTodo mb then
                        printfn "removing member with TODO: %s.%s" (getName tp) (getName mb)
                        false
                    else true
                )
            }
            |> FsType.Interface
        | _ -> tp
    )