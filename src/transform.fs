module rec ts2fable.Transform

open Fable.Core
open Fable.Core.JsInterop
open Node
open TypeScript
open TypeScript.ts
open System.Collections.Generic
open System
open ts2fable.Naming

/// recursively fix all the FsType childen
let rec fixType (fix: FsType -> FsType) (tp: FsType): FsType =

    let fixModule (a: FsModule): FsModule =
        { a with
            Types = a.Types |> List.map (fixType fix)
        }

    let fixFile (f: FsFile): FsFile =
        { f with
            Modules = f.Modules |> List.map fixModule
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
    | FsType.TypeLiteral tl ->
        { tl with
            Members = tl.Members |> List.map (fixType fix)
        }
        |> FsType.TypeLiteral
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
        fixModule md
        |> FsType.Module
     | FsType.File f ->
        fixFile f
        |> FsType.File
    | FsType.FileOut fo ->
        { fo with
            Files = fo.Files |> List.map fixFile
        }
        |> FsType.FileOut
    | FsType.Variable vb ->
        { vb with
            Type = fixType fix vb.Type
        }
        |> FsType.Variable

    | FsType.ExportAssignment _ -> tp
    | FsType.Enum _ -> tp
    | FsType.Mapped _ -> tp
    | FsType.None _ -> tp
    | FsType.TODO _ -> tp
    | FsType.StringLiteral _ -> tp
    | FsType.This -> tp
    | FsType.Import _ -> tp

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
                                Kind = FsPropertyKind.Regular
                                Index = None
                                Name = it.Name.Replace("Static","")
                                Option = false
                                Type = it.Name |> simpleType
                                IsReadonly = true
                            }
                            |> FsType.Property
                        ]
                    else []
                | _ -> []
            )
            if tps.Length = 0 then
                tp
            else
                let newTypes =
                    [
                        {
                            Comments = []
                            IsStatic = false
                            IsClass = false
                            Name = "IExports"
                            FullName = "IExports"
                            Inherits = []
                            TypeParameters = []
                            Members = tps
                        }
                        |> FsType.Interface |> Some

                        (
                            if md.HasDeclare then
                                let path = if f.ModuleName = "node" then md.Name else f.ModuleName
                                {
                                    Export = { IsGlobal = false; Selector = "*"; Path = path } |> Some
                                    HasDeclare = true
                                    Name = md.Name
                                    Type = sprintf "%s.IExports" (fixModuleName md.Name) |> simpleType
                                }
                                |> FsType.Variable |> Some
                            else
                                None
                        )
                    ]
                    |> List.choose id
                { md with
                    Types = newTypes @ md.Types
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
            | FsType.Mapped mp ->
                if set.Contains t then
                    { mp with Name = sprintf "'%s" mp.Name } |> FsType.Mapped
                else t
            | _ -> t
        fixType fix tp

let addTicForGenericFunctions(f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Function fn ->
            fixTic fn.TypeParameters tp
        | _ -> tp
    )

// https://github.com/Microsoft/TypeScript/blob/master/doc/spec.md#18-overloading-on-string-parameters
let fixOverloadingOnStringParameters(f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Function fn ->
            if fn.HasStringLiteralParams then
                let kind = ResizeArray()
                let name = ResizeArray()
                let prms = ResizeArray()
                sprintf "$0.%s(" fn.Name.Value |> kind.Add
                sprintf "%s" fn.Name.Value |> name.Add
                let slCount = ref 0
                fn.Params |> List.iteri (fun i prm ->
                    match asStringLiteral prm.Type with
                    | None ->
                        sprintf "$%d" (i + 1 - !slCount) |> kind.Add
                        prms.Add prm
                    | Some sl ->
                        incr slCount
                        sprintf "'%s'" sl |> kind.Add
                        sprintf "_%s" sl |> name.Add
                    if i < fn.Params.Length - 1 then
                        "," |> kind.Add
                )
                ")" |> kind.Add
                { fn with
                    Kind = String.concat "" kind |> FsFunctionKind.StringParam
                    Name = String.concat "" name |> Some
                    Params = List.ofSeq prms
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
            | FsType.Mapped mp ->
                if mp.Name.Equals "NodeArray" && gn.TypeParameters.Length = 1 then
                    gn.TypeParameters.[0] |> FsType.Array
                else tp
            | _ -> tp
        | _ -> tp
    )

let fixReadonlyArray(f: FsFile): FsFile =
    let fix (tp: FsType): FsType =
        match tp with
        | FsType.Generic gn ->
            match gn.Type with
            | FsType.Mapped mp ->
                if mp.Name.Equals "ReadonlyArray" && gn.TypeParameters.Length = 1 then
                    gn.TypeParameters.[0] |> FsType.Array
                else tp
            | _ -> tp
        | _ -> tp

    // only replace in functions
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Function _ -> fixType fix tp
        | _ -> tp
    )

let fixEscapeWords(f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Mapped mp ->
            { mp with Name = escapeWord mp.Name } |> FsType.Mapped
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
        | FsType.Mapped mp ->
            { mp with Name = replaceName mp.Name } |> FsType.Mapped
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
        | FsType.Mapped mp ->
            if mp.Name.Contains "." then
                let nm = mp.Name.Substring(0, mp.Name.IndexOf ".")
                if set.Contains nm then
                    // { mp with Name = nm } |> FsType.Mapped
                    simpleType nm
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
            if tps.Length > 8 then
                // printfn "union has %d types, > 8, so setting as obj %A" tps.Length un
                simpleType "obj"
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

/// replaces `this` with a reference to the interface type
let fixThis(f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Interface it ->

            let replaceThis tp = 
                match tp with
                | FsType.This ->
                    {
                        Type = simpleType it.Name
                        TypeParameters = it.TypeParameters
                    }
                    |> FsType.Generic
                | _ -> tp

            { it with
                Members = it.Members |> List.map (fixType replaceThis)
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
                                    // TypeParameters = [] // remove them after the tic is added
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

let fixOpens(fo: FsFileOut): FsFileOut =

    let isBrowser (name: string): bool =
        if isNull name then false
        else name.StartsWith "HTML"

    let mutable hasBrowser = false

    let fix(tp: FsType): FsType =
        match tp with
        | FsType.Mapped mp ->
            if isBrowser mp.Name then
                hasBrowser <- true
            tp
        | _ -> tp

    fo |> FsType.FileOut |> fixType fix |> ignore

    { fo with
        Opens =
            if hasBrowser then fo.Opens @ ["Fable.Import.Browser"]
            else fo.Opens
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

let removeTypeParamsFromStatic(f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Interface it ->
            { it with
                TypeParameters =
                    if it.IsStatic then [] else it.TypeParameters
            }
            |> FsType.Interface
        | _ -> tp
    )

let addConstructors  (f: FsFile): FsFile =
    // we are importing classes as interfaces
    // we need of list of classes with constructors
    let list = List<_>()
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Interface it ->
            if it.IsClass && it.HasConstructor then
                list.Add it |> ignore
                tp
            else tp
        | _ -> tp
    ) |> ignore

    let map = list |> Seq.map(fun it -> it.FullName, it) |> dict

    // use those as the references
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Interface it ->
            if it.IsClass then
                if it.HasConstructor then
                    tp
                else
                    // see if base type has constructors
                    let parent =
                        it.Inherits |> List.tryPick (fun inh ->
                            let fn = getFullName inh
                            if map.ContainsKey fn then
                                Some map.[fn]
                                else None
                        )

                    match parent with
                    | Some pt -> 
                        // copy the constructors from the parent
                        { it with Members = pt.Constructors @ it.Members } |> FsType.Interface

                    | None ->
                        let defaultCtr =
                            {
                                Comments = []
                                Kind = FsFunctionKind.Constructor
                                IsStatic = true
                                Name = Some "Create"
                                TypeParameters = it.TypeParameters
                                Params = []
                                ReturnType = FsType.This
                            }
                            |> FsType.Function

                        { it with Members = [defaultCtr] @ it.Members } |> FsType.Interface

            else tp
        | _ -> tp
    )

let rec addExportAssigments (f: FsFile): FsFile =

    let globalModule = f.Modules |> List.find (fun md -> md.Name = "")

    // get a list of modules
    let modules =
        globalModule.Types
        |> List.choose asModule
        |> List.map (fun md -> md.Name)
        |> Set.ofList

    // get a list of variables in global
    let variables =
        globalModule.Types
        |> List.choose asVariable
        |> List.map (fun vb -> vb.Name)
        |> Set.ofList

    // let exports =
    //     globalModule.Types
    //     |> List.choose asExport
    //     |> Set.ofList

    // printfn "modules %A" modules
    // printfn "variables %A" variables
    // printfn "exports %A" exports

    { f with
        Modules = f.Modules |> List.map (fun md ->
            if md.Name = "" then // global
                { md with
                    Types = md.Types |> List.map (fun tp ->
                        match tp with
                        // TODO
                        // | FsType.Export ep ->
                        //     if modules.Contains ep && variables.Contains ep = false then
                        //         {
                        //             Export = { IsGlobal = false; Selector = "*"; Path = f.ModuleName } |> Some
                        //             HasDeclare = true // so it is not in IExports
                        //             Name = ep
                        //             Type = sprintf "%s.IExports" ep |> simpleType
                        //         }
                        //         |> FsType.Variable
                        //     else tp
                        | FsType.Variable vb ->
                            if vb.HasDeclare then
                                { vb with
                                    Export = { IsGlobal = f.ModuleName = "node"; Selector = "*"; Path = f.ModuleName } |> Some
                                }
                                |> FsType.Variable
                            else tp
                        | _ -> tp
                    )
                }
            else md
        )
    }

let removeInternalModules(f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Module md ->
            { md with
                Types = md.Types |> List.collect (fun tp ->
                    match tp with
                    | FsType.Module smd when smd.Name = "internal" ->
                        smd.Types
                    | _ -> [tp]
                )
            }
            |> FsType.Module
        | _ -> tp
    )

let removeDuplicateFunctions(f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Interface it ->
            let set = HashSet<_>()
            { it with
                Members = it.Members |> List.collect (fun mb ->
                    match mb with
                    | FsType.Function fn ->
                        // compare without comments or a return type
                        let fn2 = { fn with Comments = []; ReturnType = FsType.None }
                        if set.Add fn2 then
                            [mb]
                        else
                            // printfn "mb is duplicate %A" mb
                            []
                    | _ -> [mb]
                )
            }
            |> FsType.Interface
        | _ -> tp
    )

let rec moveDeclaredVariables (f: FsFile): FsFile =
    let globalDeclares = List<_>()
    let otherDeclares = List<_>()
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Module md ->
            for tp in md.Types do
                match tp with
                | FsType.Variable vb ->
                    if vb.HasDeclare then
                        if vb.IsGlobal then
                            globalDeclares.Add tp |> ignore
                        else
                            otherDeclares.Add tp |> ignore
                | _ -> ()
            tp
        | _ -> tp
    ) |> ignore

    let filterDeclares (tps: FsType list) =
        tps |> List.collect (fun tp ->
            match tp with
            | FsType.Variable vb ->
                if vb.HasDeclare then []
                else [tp]
            | _ -> [tp]
        )

    f |> fixFile (fun tp ->
        match tp with
        | FsType.Module md ->
            if md.Name = "" then // global
                { md with
                    Types =
                        (globalDeclares |> List.ofSeq)
                        @ (otherDeclares |> List.ofSeq)
                        @ (md.Types |> filterDeclares)
                }
                |> FsType.Module
            else
                // filter out the declares that were just moved
                { md with
                    Types = md.Types |> filterDeclares
                }
                |> FsType.Module
        | _ -> tp
    )

let extractTypeLiterals(f: FsFile): FsFile =
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Module md ->

            let typeNames =
                md.Types
                |> List.map getName
                |> HashSet<_>

            // append an underscores until a unique name is created
            let rec newTypeName (name: string): string =
                let name = capitalize name
                if typeNames.Contains name then
                    let name = sprintf "%s_" name
                    if typeNames.Contains name then
                        newTypeName name
                    else
                        typeNames.Add name |> ignore
                        name
                else
                    typeNames.Add name |> ignore
                    name

            { md with
                Types = md.Types |> List.collect (fun tp ->
                    match tp with
                    | FsType.Interface it ->

                        let newTypes = List<FsType>()
                        let it2 =
                            { it with
                                Members = it.Members |> List.map (fun mb ->
                                    match mb with
                                    | FsType.Function fn ->
                                        { fn with
                                            Params = fn.Params |> List.map (fun prm ->
                                                match prm.Type with
                                                | FsType.TypeLiteral tl ->
                                                    let name =
                                                        let itName = if it.Name = "IExports" then "" else it.Name.Replace("`","")
                                                        let fnName = fn.Name.Value.Replace("`","")
                                                        let pmName = prm.Name.Replace("`","")
                                                        if fnName = "Create" then
                                                            sprintf "%s%s" itName (capitalize pmName) |> newTypeName
                                                        else if fnName = pmName then
                                                            sprintf "%s%s" itName (capitalize pmName) |> newTypeName
                                                        else
                                                            sprintf "%s%s%s" itName (capitalize fnName) (capitalize pmName) |> newTypeName
                                                    {
                                                        Comments = []
                                                        IsStatic = false
                                                        IsClass = false
                                                        Name = name
                                                        FullName = name
                                                        Inherits = []
                                                        Members = tl.Members
                                                        TypeParameters = []
                                                    }
                                                    |> FsType.Interface
                                                    |> newTypes.Add |> ignore

                                                    { prm with Type = simpleType name }
                                                | _ -> prm
                                            )
                                        }
                                        |> FsType.Function
                                    | _ -> mb
                                )
                            }
                            |> FsType.Interface

                        [it2] @ (List.ofSeq newTypes) // append new types

                    | _ -> [tp]
                )
            }
            |> FsType.Module
        | _ -> tp
    )

let addAliasUnionHelpers(f: FsFile): FsFile =
    
    f |> fixFile (fun tp ->
        match tp with
        | FsType.Module md ->
            let helpers = ResizeArray<FsType>()

            { md with
                Types = 
                    (md.Types |> List.collect(fun tp2 ->
                        match tp2 with
                        | FsType.Alias al ->
                            match al.Type with
                            | FsType.Union un ->
                                if un.Types.Length > 1 then
                                    // [tp2] @
                                    // [
                                        {
                                            Attributes = ["RequireQualifiedAccess"; "CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix"]
                                            HasDeclare = false
                                            Name = al.Name
                                            Types = []
                                            HelperLines =
                                                let mutable i = 0
                                                un.Types |> List.collect (fun tp3 ->
                                                    let n = un.Types.Length
                                                    i <- i + 1
                                                    let name = getName tp3
                                                    let name = if name = "" then sprintf "Case%d" i else name
                                                    let name = name.Replace("'","") // strip generics
                                                    let name = capitalize name
                                                    let aliasNameWithTypes = sprintf "%s%s" al.Name (Print.printTypeParameters al.TypeParameters)
                                                    if un.Option then
                                                        [
                                                            sprintf "let of%sOption v: %s = v |> Option.map U%d.Case%d" name aliasNameWithTypes n i
                                                            sprintf "let of%s v: %s = v |> U%d.Case%d |> Some" name aliasNameWithTypes n i
                                                            sprintf "let is%s (v: %s) = match v with None -> false | Some o -> match o with U%d.Case%d _ -> true | _ -> false" name aliasNameWithTypes n i
                                                            sprintf "let as%s (v: %s) = match v with None -> None | Some o -> match o with U%d.Case%d o -> Some o | _ -> None" name aliasNameWithTypes n i
                                                        ]
                                                    else
                                                        [
                                                            sprintf "let of%s v: %s = v |> U%d.Case%d" name aliasNameWithTypes n i
                                                            sprintf "let is%s (v: %s) = match v with U%d.Case%d _ -> true | _ -> false" name aliasNameWithTypes n i
                                                            sprintf "let as%s (v: %s) = match v with U%d.Case%d o -> Some o | _ -> None" name aliasNameWithTypes n i
                                                        ]
                                                )
                                        }
                                        |> FsType.Module
                                        |> helpers.Add
                                    // ]
                                        [tp2]
                                else [tp2]
                            | _ -> [tp2]
                        | _ -> [tp2]
                    ))
                    @
                    [
                        {
                            Attributes = [] // ["AutoOpen"] TODO fails too
                            HasDeclare = false
                            Name = "AliasUnionHelpers"
                            Types = helpers |> List.ofSeq
                            HelperLines = []
                        }
                        |> FsType.Module
                    ]
            }
            |> FsType.Module

        | _ -> tp
    )