module rec ts2fable.Transform

open Fable.Core
open Fable.Core.JsInterop
open TypeScript
open TypeScript.Ts
open System.Collections.Generic
open System
open ts2fable.Naming
open System.Collections
open System.Collections.Generic
open ts2fable.Syntax
open ts2fable.Keywords
open Fable

let getAllTypesFromFile fsFile =
    let tps = List []
    fsFile
    |> fixFile (fun ns tp ->
        tp |> tps.Add
        tp
    ) |> ignore
    tps |> List.ofSeq

let getAllTypes fsFiles =
    fsFiles |> List.collect getAllTypesFromFile

/// recursively fix all the FsType childen and allow the caller to decide how deep to recurse.
let rec fixTypeEx (ns: string) (doFix:FsType->bool) (fix: string->FsType->FsType) (tp: FsType): FsType =

    let ns =
        match tp with
        | FsType.Module _ -> ns
        | _ -> ns + (getName tp |> makePartName)

    let fixType = fixTypeEx ns doFix fix

    let fixModule (a: FsModule): FsModule =
        { a with
            Types = a.Types |> List.map fixType
        }

    let fixFile (f: FsFile): FsFile =
        { f with
            Modules = f.Modules |> List.map fixModule
        }

    let fixParam (a: FsParam): FsParam =
        let b =
            { a with
                Type = fixType a.Type
            }
            |> FsType.Param
        match fix ns b with
        | FsType.Param c -> c
        | _ -> failwithf "param must be mapped to param"

    // fix children first, then current type
    match tp with
    | tp when (not (doFix tp)) -> tp
    | FsType.Interface it ->
        { it with
            TypeParameters = it.TypeParameters |> List.map fixType
            Inherits = it.Inherits |> List.map fixType
            Members = it.Members |> List.map fixType
        }
        |> FsType.Interface
    | FsType.TypeLiteral tl ->
        { tl with
            Members = tl.Members |> List.map fixType
        }
        |> FsType.TypeLiteral
    | FsType.Property pr ->
        { pr with
            Index = Option.map fixParam pr.Index
            Type = fixType pr.Type
        }
        |> FsType.Property
    | FsType.Param pr ->
        { pr with
            Type = fixType pr.Type
        }
        |> FsType.Param
    | FsType.Array ar ->
        fixType ar |> FsType.Array
    | FsType.Function fn ->
        { fn with
            TypeParameters = fn.TypeParameters |> List.map fixType
            Params = fn.Params |> List.map fixParam
            ReturnType = fixType fn.ReturnType
        }
        |> FsType.Function
    | FsType.Union un ->
        { un with
            Types = un.Types |> List.map fixType
        }
        |> FsType.Union
    | FsType.Alias al ->
        { al with
            Type = fixType al.Type
            TypeParameters = al.TypeParameters |> List.map fixType
        }
        |> FsType.Alias
    | FsType.Generic gn ->
        { gn with
            Type = fixType gn.Type
            TypeParameters = gn.TypeParameters |> List.map fixType
        }
        |> FsType.Generic
    | FsType.Tuple tp ->
        { tp with
            Types = tp.Types |> List.map fixType
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
            Type = fixType vb.Type
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
    | FsType.GenericTypeParameter gtp ->
        { gtp with
            Constraint = gtp.Constraint |> Option.map fixType
            Default = gtp.Default |> Option.map fixType
        }
        |> FsType.GenericTypeParameter
    | FsType.KeyOf k ->
        { k with
            Type = fixType k.Type
        }
        |> FsType.KeyOf
    |> fun t -> if doFix(t) then fix ns t else t // current type

/// recursively fix all the FsType childen
let fixType ns (fix: string->FsType->FsType) (tp: FsType): FsType =
    fixTypeEx ns (fun _ -> true) fix tp

/// recursively fix all the FsType childen for the given FsFile and allow the caller to decide how deep to recurse.
let fixFileEx (doFix:FsType->bool) (fix: string->FsType->FsType) (f: FsFile): FsFile =
    { f with
        Modules =
            f.Modules
            |> List.map FsType.Module
            |> List.map (fixTypeEx "" doFix fix)
            |> List.choose FsType.asModule
    }

/// recursively fix all the FsType childen for the given FsFile
let fixFile (fix: string->FsType->FsType) (f: FsFile): FsFile =
    fixFileEx (fun _ -> true) fix f

let mergeTypes (tps: FsType list): FsType list =
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

let mergeModules (tps: FsType list): FsType list =
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
                let a = (list.[i] |> FsType.asModule).Value
                list.[i] <-
                    { a with
                        Types = a.Types @ md2.Types |> mergeTypes |> mergeModules
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
            |> List.choose FsType.asModule
    }

let engines = ["node"; "vscode"] |> Set.ofList

let rec createIExportsModule (ns: string list) (md: FsModule): FsModule * FsVariable list =
    printfn "createIExportsModule %A, %s" ns md.Name
    let typesInIExports = ResizeArray<FsType>()
    let typesGlobal = ResizeArray<FsType>()
    let typesChild = ResizeArray<FsType>()
    let typesChildExport = ResizeArray<FsType>()
    let typesOther = ResizeArray<FsType>()
    let variablesForParent = ResizeArray<FsVariable>()

    let variables = HashSet<FsVariable>()
    let exportAssignments = HashSet<string>()
    md.Types |> List.iter(fun tp ->
        match tp with
        | FsType.ExportAssignment ea ->
            exportAssignments.Add ea |> ignore
        | _ -> ()
    )

    // convert variables in interfaces (after transformation) into properties
    // -> no extra handling of Variables in print
    let variableToProperty (v: FsVariable): FsProperty =
        {
            Attributes = v.Attributes
            Comments = v.Comments
            Kind = FsPropertyKind.Regular
            Index = None
            Name = v.Name
            Option = false
            Type = v.Type
            Accessor = if v.IsConst then ReadOnly else ReadWrite
            IsStatic = v.IsStatic
            Accessibility = v.Accessibility
        }

    md.Types |> List.iter(fun tp ->
        match tp with
        | FsType.Module smd ->
            let ns =
                if md.Name = "" then ns
                else
                    let parts =
                        let name = md.Name.Replace("'","")
                        match name with
                        | ModuleName.Normal -> [name]
                        | ModuleName.Parts parts -> parts |> List.filter((<>) ".")
                    ns @ parts

            let smd, vars = createIExportsModule ns smd
            for v in vars do
                if v.Export.IsSome then v |> variables.Add |> ignore
                else v |> FsType.Variable |> typesChild.Add
            smd |> FsType.Module |> typesOther.Add
        | FsType.Variable vb ->
            if vb.HasDeclare then
                if md.Name = "" then
                    { vb with
                        Export =
                            { IsGlobal = engines.Contains ns.[0]
                              Selector =
                                if String.Compare(vb.Name,ns.[0],true) = 0 then "*"
                                else vb.Name
                              Path = ns.[0] } |> Some
                    }
                    |> FsType.Variable
                    |> typesGlobal.Add
                else
                    if vb.IsGlobal then
                        typesGlobal.Add tp
                    else
                        vb
                        |> variableToProperty
                        |> FsType.Property
                        |> typesInIExports.Add
            else
                vb
                |> variableToProperty
                |> FsType.Property
                |> typesInIExports.Add
        | FsType.Function _ -> typesInIExports.Add tp
        | FsType.Interface it ->
            if it.IsStatic then
                // add a property for accessing the static class
                {
                    Attributes = []
                    Comments = it.Comments
                    Kind = FsPropertyKind.Regular
                    Index = None
                    Name = it.Name.Replace("Static","")
                    Option = false
                    Type = it.Name |> simpleType
                    Accessor = ReadOnly
                    IsStatic = false
                    Accessibility = None
                }
                |> FsType.Property
                |> typesInIExports.Add
            typesOther.Add tp
        | _ -> typesOther.Add tp
    )

    let ns = if engines.Contains ns.[0] then ns.[1..] else ns
    let selector =
        if ns.Length = 0 then "*"
        else md.Name.Replace("'","")
    let path =
        if ns.Length = 0 then
            md.Name.Replace("'","")
        else ns |> String.concat "/"

    if typesInIExports.Count > 0 then

        // Some JS names are all uppercase which looks really odd if we just lowercase the first letter
        let name =
            if md.Name |> Seq.forall Char.IsUpper then
                md.Name.ToLower()
            else
                md.Name |> lowerFirst

        if md.HasDeclare then
            if not <| md.IsNamespace then
                {
                    Attributes = []
                    Comments = md.Comments
                    Export = { IsGlobal = false; Selector = "*"; Path = path } |> Some
                    HasDeclare = true
                    Name = name
                    Type = sprintf "%s.IExports" (fixModuleName md.Name) |> simpleType
                    IsConst = true
                    IsStatic = false
                    Accessibility = None
                }
                |> variablesForParent.Add
        else
            {
                Attributes = []
                Comments = md.Comments
                Export = { IsGlobal = false; Selector = selector; Path = path } |> Some
                HasDeclare = true
                Name = name
                Type = sprintf "%s.IExports" (fixModuleName md.Name) |> simpleType
                IsConst = true
                IsStatic = false
                Accessibility = None
            }
            |> variablesForParent.Add

    let iexports =
        if typesInIExports.Count = 0 then []
        else
            [
                {
                    Attributes = []
                    Comments = []
                    IsStatic = false
                    IsClass = false
                    Name = "IExports"
                    FullName = "IExports"
                    Inherits = []
                    TypeParameters = []
                    Members =
                        (typesInIExports |> List.ofSeq)
                    Accessibility = None
                }
                |> FsType.Interface
            ]

    // add exports assignments
    // make sure there are no conflicting globals already
    let globalNames = typesGlobal |> Seq.map getName |> Set.ofSeq

    md.Types |> List.iter(fun tp ->
        match tp with
        | FsType.Module smd ->
            if not <| globalNames.Contains smd.Name && exportAssignments.Contains smd.Name then
                {
                    Attributes = []
                    Comments = smd.Comments
                    Export = { IsGlobal = false; Selector = "*"; Path = path } |> Some
                    HasDeclare = true
                    Name = smd.Name |> lowerFirst
                    Type = sprintf "%s.IExports" (fixModuleName smd.Name) |> simpleType
                    IsConst = true
                    IsStatic = false
                    Accessibility = None
                }
                |> variables.Add |> ignore
        | _ -> ()
    )

    let newMd =
        { md with
            Types =
                (variables |> List.ofSeq |> List.map FsType.Variable)
                @ (typesGlobal |> List.ofSeq)
                @ (typesChildExport |> List.ofSeq)
                @ (typesChild |> List.ofSeq)
                @ iexports
                @ (typesOther |> List.ofSeq)
        }

    newMd, variablesForParent |> List.ofSeq

let createIExports (f: FsFile): FsFile =

    { f with
        Modules =
            f.Modules
            |> List.ofSeq
            |> List.map (fun md ->
                let md, _ = createIExportsModule [f.ModuleName] md
                md
            )
    }

let fixTic ns (typeParameters: FsType list) (tp: FsType) =
    if typeParameters.Length = 0 then
        tp
    else
        let set =
            typeParameters
            |> List.choose FsType.asGenericTypeParameter
            |> List.map (fun gtp -> gtp.Name)
            |> Set.ofList
        let formatTic = sprintf "'%s"
        let fix ns (t: FsType): FsType =
            match t with
            | FsType.Mapped mp when set.Contains mp.Name ->
                { mp with Name = formatTic mp.Name }
                |> FsType.Mapped
            | FsType.GenericTypeParameter p when set.Contains p.Name ->
                { p with Name = formatTic p.Name }
                |> FsType.GenericTypeParameter
            | _ -> t
        fixType ns fix tp

let addTicForGenericFunctions(f: FsFile): FsFile =
    f |> fixFile (fun ns tp ->
        match tp with
        | FsType.Function fn ->
            fixTic ns fn.TypeParameters tp
        | _ -> tp
    )

// https://github.com/Microsoft/TypeScript/blob/master/doc/spec.md#18-overloading-on-string-parameters
let fixOverloadingOnStringParameters(f: FsFile): FsFile =
    f |> fixFile (fun ns tp ->
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
                    match FsType.asStringLiteral prm.Type with
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
                let name = 
                    let name = String.concat "" name
                    // replace whitespaces with `_`
                    let name = name.Replace(' ', '_').Replace('\t', '_')
                    // if still invalid identifier: put into double backticks
                    if name |> isIdentifier then
                        name
                    else
                        sprintf "``%s``" name
                { fn with
                    Kind = String.concat "" kind |> FsFunctionKind.StringParam
                    Name = name |> Some
                    Params = List.ofSeq prms
                }
                |> FsType.Function
            else tp
        | _ -> tp
    )

let fixNodeArray(f: FsFile): FsFile =
    f |> fixFile (fun ns tp ->
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
    let fix ns (tp: FsType): FsType =
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
    f |> fixFile (fun ns tp ->
        match tp with
        | FsType.Function _ -> fixType ns fix tp
        | _ -> tp
    )

let fixEscapeWords(f: FsFile): FsFile =
    f |> fixFile (fun ns tp ->
        match tp with
        | FsType.Mapped mp ->
            { mp with Name = escapeWord mp.Name } |> FsType.Mapped
        | FsType.Param pm ->
            { pm with Name = escapeWord pm.Name } |> FsType.Param
        | FsType.Function fn ->
            { fn with Name = fn.Name |> Option.map escapeWord } |> FsType.Function
        | FsType.Property pr ->
            { pr with Name = escapeProperty pr.Name } |> FsType.Property
        | FsType.Interface it ->
            { it with Name = escapeWord it.Name } |> FsType.Interface
        | FsType.Module md ->
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

    f |> fixFile (fun ns tp ->
        match tp with
        | FsType.Mapped mp ->
            { mp with Name = replaceName mp.Name } |> FsType.Mapped
        | _ -> tp
    )

let fixEnumReferences (f: FsFile): FsFile =
    // get a list of enum names
    let list = List<string>()
    f |> fixFile (fun ns tp ->
        match tp with
        | FsType.Enum en ->
            list.Add en.Name |> ignore
            tp
        | _ -> tp
    ) |> ignore

    // use those as the references
    let set = Set.ofSeq list
    f |> fixFile (fun ns tp ->
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
    f |> fixFile (fun ns tp ->
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
    f |> fixFile (fun ns tp ->
        match tp with
        | FsType.Interface it -> fixTic ns it.TypeParameters tp
        | FsType.Alias al -> fixTic ns al.TypeParameters tp
        | _ -> tp
    )

/// replaces `this` with a reference to the interface type
let fixThis(f: FsFile): FsFile =
    f |> fixFile (fun ns tp ->
        match tp with
        | FsType.Interface it ->

            let replaceThis ns tp =
                match tp with
                | FsType.This ->
                    {
                        Type = simpleType it.Name
                        TypeParameters = it.TypeParameters
                    }
                    |> FsType.Generic
                | _ -> tp

            { it with
                Members = it.Members |> List.map (fixType ns replaceThis)
            }
            |> FsType.Interface
        | _ -> tp
    )

let fixStatic(f: FsFile): FsFile =
    f |> fixFile (fun ns tp ->
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

let hasTodo ns (tp: FsType) =
    let mutable has = false
    tp |> fixType ns  (fun ns t ->
        match t with
        | FsType.TODO ->
            has <- true
            t
        | _ -> t
    ) |> ignore
    has

let removeTodoMembers(f: FsFile): FsFile =
    f |> fixFile (fun ns tp ->
        match tp with
        | FsType.Interface it ->
            { it with
                // Members = it.Members |> List.filter (not << hasTodo)
                Members = it.Members |> List.filter (fun mb ->
                    if hasTodo ns mb then
                        printfn "removing member with TODO: %s.%s" (getName tp) (getName mb)
                        false
                    else true
                )
            }
            |> FsType.Interface
        | _ -> tp
    )

let removeTypeParamsFromStatic(f: FsFile): FsFile =
    f |> fixFile (fun ns tp ->
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
    f |> fixFile (fun ns tp ->
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
    f |> fixFile (fun ns tp ->
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
                                Attributes = []
                                Comments = []
                                Kind = FsFunctionKind.Constructor
                                IsStatic = true
                                Name = Some "Create"
                                TypeParameters = it.TypeParameters
                                Params = []
                                ReturnType = FsType.This
                                Accessibility = None
                            }
                            |> FsType.Function

                        { it with Members = [defaultCtr] @ it.Members } |> FsType.Interface

            else tp
        | _ -> tp
    )

let removeInternalModules(f: FsFile): FsFile =
    f |> fixFile (fun ns tp ->
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

let removePrivatesFromClasses(f: FsFile): FsFile =
    f |> fixFile (fun ns tp ->
        match tp with
        | FsType.Interface c when c.IsClass ->
            { c with
                Members = c.Members |> List.filter (fun m -> getAccessibility m <> Some FsAccessibility.Private)
            }
            |> FsType.Interface
        | _ -> tp
    )

let removeDuplicateFunctions(f: FsFile): FsFile =
    f |> fixFile (fun ns tp ->
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

let removeDuplicateOptions(f: FsFile): FsFile =
    f |> fixFile (fun ns tp ->
        match tp with
        | FsType.Property pr when pr.Option ->
            match pr.Type with
            | FsType.Union un when un.Option ->
                { pr with
                    Type = { un  with Option = false } |> FsType.Union
                }
                |> FsType.Property
            | _ -> tp

        | _ -> tp
    )

/// this converts `(?x : int option)` to `(?x : int)`
let removeDuplicateOptionsFromParameters(f: FsFile): FsFile =

    // consider two TS cases:
    //   1) (?p : int | null)
    //   2) type Nullable<T> = T | null
    //      (?p : Nullable<int>)

    let allTypes = lazy (getAllTypesFromFile f |> List.toArray)

    let rec isAliasToOption (name: string) =
        let typFromName = allTypes.Value |> Seq.tryFind (fun t ->
            match t with
            | FsType.Alias { Name = aliasName }
                when (aliasName = name) -> true
            | _ -> false
        )
        match typFromName with
        // simple: the alias is itself a union with option
        | Some (FsType.Alias { Type = FsType.Union { Option = true; Types = [ FsType.Mapped mp ] }; TypeParameters = [ FsType.GenericTypeParameter gtp ] })
           when mp.Name = gtp.Name -> true
        // here we could check if the alias is another alias to option, but that seems like an unlikely pattern
        | _ -> false


    f |> fixFile (fun ns tp ->

        match tp with
        | FsType.Param pr when pr.Optional ->

            match pr.Type with
            // case 1: simple
            | FsType.Union { Option = true; Types = [ t ] } -> { pr with Type = t } |> FsType.Param
            // not tested: I assume this is hit with (?p : int | string | null)
            | FsType.Union un when un.Option ->
                { pr with Type = { un  with Option = false } |> FsType.Union } |> FsType.Param
            // case 2: alias
            | FsType.Generic { Type = FsType.Mapped { Name = name; FullName = "" }; TypeParameters = [ t ] }
                when (isAliasToOption name) ->
                    { pr with Type = t } |> FsType.Param

            | _ -> tp

        | _ -> tp
    )

let extractTypeLiterals(f: FsFile): FsFile =

    /// the goal is to create interface types with 'pretty' names like '$(Class)$(Method)Return'.
    let extractTypeLiterals_pass1 (f: FsFile): FsFile =
      f |> fixFile (fun ns tp ->
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
                        let materializeInterfaceType name members =
                            let materialized = {
                                Attributes = []
                                Comments = []
                                IsStatic = false
                                IsClass = false
                                Name = name
                                FullName = name
                                Inherits = []
                                Members = members
                                TypeParameters = []
                                Accessibility = None
                            }
                            newTypes.Add (FsType.Interface materialized)


                        let it2 =
                            { it with
                                Members = it.Members |> List.map (fun mb ->
                                    match mb with
                                    | FsType.Function fn ->
                                        let mapParam (prm:FsParam) : FsParam =
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

                                                materializeInterfaceType name tl.Members
                                                { prm with Type = simpleType name }
                                            | _ -> prm

                                        let mapReturnType (tp:FsType) =
                                            match tp with
                                            | FsType.TypeLiteral tl ->
                                                let name =
                                                    let itName = if it.Name = "IExports" then "" else it.Name.Replace("`","")
                                                    let fnName = fn.Name.Value.Replace("`","")
                                                    sprintf "%s%sReturn" itName (capitalize fnName) |> newTypeName

                                                materializeInterfaceType name tl.Members
                                                simpleType name
                                            | _ -> tp
                                        { fn with
                                            Params = fn.Params |> List.map mapParam
                                            ReturnType = fn.ReturnType |> mapReturnType
                                        }
                                        |> FsType.Function
                                    | _ -> mb
                                )
                            }
                            |> FsType.Interface

                        [it2] @ (List.ofSeq newTypes) // append new types
                    | FsType.Alias al ->
                        match al.Type with
                        | FsType.Union un ->
                            let un2 =
                                { un with
                                    Types =
                                        let tps = List<FsType>()
                                        un.Types |> List.iter(fun tp ->
                                            match tp with
                                            | FsType.TypeLiteral tl -> tl.Members |> tps.AddRange
                                            | _ -> tp |> tps.Add
                                        )
                                        tps |> List.ofSeq
                                }
                            {al with Type = un2 |> FsType.Union} |> FsType.Alias |> List.singleton
                        | FsType.TypeLiteral tl ->
                            {
                                Attributes = []
                                Comments = al.Comments
                                IsStatic = false
                                IsClass = false
                                Name = al.Name
                                FullName = al.Name
                                Inherits = []
                                Members = tl.Members
                                TypeParameters = al.TypeParameters
                                Accessibility = None
                            } |> FsType.Interface |> List.singleton
                        | _ -> [tp]
                    | _ -> [tp]
                )
            }
            |> FsType.Module
        | _ -> tp
    )

    /// type literals can occur in many places, and it is kinda hard to account for all of them in the first pass.
    /// so do a second pass, and just replace them with interfaces with a not quite so pretty name.
    /// Note: in an ideal world with enough time, this pass would not find anything, and all TLs would be accounted for in the first pass with pretty names.
    let extractTypeLiterals_pass2 (f: FsFile): FsFile =
        // let mutable i = 1 // the name of the type literal ("TypeLiteral_%02i"): use one counter over all modules.
        let extractFromModule (m:FsModule) : FsModule =

            let fixModuleEx doFix fix (m:FsModule) : FsModule =
                match fixTypeEx "" doFix fix (FsType.Module m) with
                | FsType.Module m2 -> m2
                | x -> failwithf "Impossible: %A" x

            /// fixup the contents of one module, but do not go into sub-modules
            let fixOneModule fix m =
                 m |> fixModuleEx (function FsType.Module m2 when (m2 <> m) -> false | _ -> true) fix

            let replacedTypeLiterals = Dictionary<FsTypeLiteral, FsInterface>()
            let uniqueNames = Dictionary<string, int>()
            let replaceLiteral ns (tl:FsTypeLiteral) : FsInterface =
                let build() =
                    // let name = sprintf "TypeLiteral_%02i" i
                    // i <- i + 1
                    let name =
                        if uniqueNames.ContainsKey(ns) then
                            uniqueNames.[ns] <- uniqueNames.[ns] + 1
                            sprintf "%s%i" ns uniqueNames.[ns]
                        else
                            uniqueNames.[ns] <- 1
                            ns
                    let generics = HashSet<FsType>()
                    FsType.TypeLiteral tl |> fixType ns  (fun ns t ->
                        match t with
                        // REVIEW: better detection for generics?
                        | FsType.Mapped({Name = name}) when (name.StartsWith "'") ->
                            generics.Add t |> ignore
                            t
                        | _ -> t
                    ) |> ignore

                    let extractedInterface = {
                        Attributes = []
                        Comments = []
                        IsStatic = false
                        IsClass = false
                        Name = name
                        FullName = name
                        Inherits = []
                        Members = tl.Members
                        TypeParameters = generics |> Seq.toList
                        Accessibility = None
                    }
                    extractedInterface
                match replacedTypeLiterals.TryGetValue(tl) with
                | true, i -> i
                | false, _ ->
                    let i = build()
                    replacedTypeLiterals.[tl] <- i
                    i

            m
            // 1: replace occurences of TypeLiterals with references to the generated types
            |> fixOneModule (fun ns tp ->
                match tp with
                | FsType.TypeLiteral tl ->

                    let extractedInterface = replaceLiteral ns tl
                    match extractedInterface.TypeParameters with
                    | [ ] ->
                        simpleType (extractedInterface.Name)
                    | tp -> FsType.Generic({ Type = simpleType (extractedInterface.Name); TypeParameters = tp })
                | _ -> tp
            )
            // 2: append the generated types to the module
            |> (fun m ->
                let generatedTypes = replacedTypeLiterals |> Seq.map (fun kv -> FsType.Interface kv.Value) |> Seq.toList
                { m with Types = m.Types @ generatedTypes }
            )

        f |> fixFile  (fun ns t -> match t with FsType.Module m -> FsType.Module (extractFromModule m) | _ -> t)


    // run both passes
    f
    |> extractTypeLiterals_pass1
    |> extractTypeLiterals_pass2

let addAliasUnionHelpers(f: FsFile): FsFile =
    f |> fixFile (fun ns tp ->
        match tp with
        | FsType.Module md ->
            { md with
                Types =
                    (md.Types |> List.collect(fun tp2 ->
                        match tp2 with
                        | FsType.Alias al ->
                            match al.Type with
                            | FsType.Union un ->
                                if un.Types.Length > 1 then
                                    [tp2] @
                                    [
                                        {
                                            Comments = []
                                            Attributes = [
                                                [
                                                    FsAttribute.fromName "RequireQualifiedAccess"
                                                    { Namespace = None; Name = "CompilationRepresentation"; Arguments = [ FsArgument.justValue "CompilationRepresentationFlags.ModuleSuffix" ] }
                                                ]
                                            ]
                                            HasDeclare = false
                                            IsNamespace = false
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
                                    ]
                                else [tp2]
                            | _ -> [tp2]
                        | _ -> [tp2]
                    ))
            }
            |> FsType.Module
        | _ -> tp
    )

let fixNamespace (f: FsFile): FsFile =
    f |> fixFile (fun ns tp ->
        match tp with
        | FsType.Mapped mp ->
            { mp with Name = fixNamespaceString mp.Name } |> FsType.Mapped
        | FsType.Import im ->
            match im with
            | FsImport.Module immd ->
                { immd with
                    Module = fixModuleName immd.Module
                    SpecifiedModule = fixModuleName immd.SpecifiedModule
                }
                |> FsImport.Module
            | FsImport.Type imtp ->
                { imtp with
                    SpecifiedModule =
                        match f.Kind with
                        | FsFileKind.Index ->
                            fixModuleName imtp.SpecifiedModule
                        | FsFileKind.Extra _ -> imtp.SpecifiedModule
                }
                |> FsImport.Type
            |> FsType.Import
        | _ -> tp
    )

let aliasToInterfacePartly (f: FsFile): FsFile =
    let compileAliasHasOnlyFunctionToInterface f =
        f |> fixFile (fun ns tp ->
            match tp with
            | FsType.Alias al ->
                match al.Type with
                | FsType.Function f ->
                    {
                        Attributes = []
                        Comments = al.Comments
                        IsStatic = false
                        IsClass = false
                        Name = al.Name
                        FullName = al.Name
                        Inherits = []
                        Members = 
                            { f with 
                                Comments = al.Comments
                                Name = Some "Invoke"
                                Kind = FsFunctionKind.Call
                            } 
                            |> FsType.Function 
                            |> List.singleton
                        TypeParameters = al.TypeParameters
                        Accessibility = None
                    } |> FsType.Interface
                | _ -> tp
            | _ -> tp
    )

    let compileAliasHasIntersectionToInterface f =
        f |> fixFile (fun ns tp ->
            match tp with
            | FsType.Alias al ->
                match al.Type with
                | FsType.Tuple tu ->
                    match tu.Kind with
                    | FsTupleKind.Intersection ->
                        {
                            Attributes = []
                            Comments = al.Comments
                            IsStatic = false
                            IsClass = false
                            Name = al.Name
                            FullName = al.Name
                            Inherits = []
                            Members = []
                            TypeParameters = al.TypeParameters
                            Accessibility = None
                        } |> FsType.Interface
                    | _ -> tp
                | _ -> tp
            | _ -> tp
        )

    let compileAliasHasMappedToInterface f =
        f |> fixFile (fun ns tp ->
            match tp with
            | FsType.Alias al ->
                match al.Type with
                | FsType.Tuple tu when tu.Kind = FsTupleKind.Mapped ->
                    {
                        Attributes = []
                        Comments = al.Comments
                        IsStatic = false
                        IsClass = false
                        Name = al.Name
                        FullName = al.Name
                        Inherits = []
                        Members = []
                        TypeParameters = al.TypeParameters
                        Accessibility = None
                    } |> FsType.Interface
                | _ -> tp
            | _ -> tp
        )

    //we don't want to print intersection and mapped types, so compile them to simpleType "obj"
    let flatten f =
        // FsTupleKind.Intersection { Kind = Intersection } -> `A & B` 
        // -> valid for generic type parameter constraints
        //    (but only as direct constraint)
        //    * `extends A & B` -> keep tuple
        //    * `extends MyType<A & B>` -> remove tuple

        let flattenTuple ns tp =
            match tp with
            | FsType.Tuple tu ->
                match tu.Kind with
                | FsTupleKind.Intersection | FsTupleKind.Mapped -> simpleType "obj"
                | _ -> tp
            | _ -> tp

        let rec flattenNestedTuple ns tp =
            match tp with
            | FsType.Tuple tu when tu.Kind = FsTupleKind.Intersection ->
                { tu with
                    Types = tu.Types |> List.map (flattenNestedTuple ns)
                }
                |> FsType.Tuple
            | _ -> fixType ns flattenTuple tp

        f
        |> fixFileEx
            (function FsType.GenericTypeParameter { Constraint = Some _ } -> false | _ -> true)
            flattenTuple
        |> fixFile
           (fun ns tp ->
                match tp with
                | FsType.GenericTypeParameter ({ Constraint = Some c; Default = d } as gtp) ->
                    { gtp with
                        Constraint = c |> flattenNestedTuple ns |> Some
                        Default = d |> Option.map (flattenTuple ns)
                    }
                    |> FsType.GenericTypeParameter
                | _ -> tp
           )

    f
    |> compileAliasHasOnlyFunctionToInterface
    |> compileAliasHasIntersectionToInterface
    |> compileAliasHasMappedToInterface
    |> flatten

/// babylonjs contains 'type float = number;', which creates invalid f# output (type float = float)
let fixFloatAlias (f: FsFile): FsFile =
    f |> fixFile (fun ns tp ->
        match tp with
        | FsType.Module m ->
            let floatNumberAlias =
                m.Types
                |> List.tryFind (function
                    | FsType.Alias { Name = "float"; Type = FsType.Mapped { Name = "float"; FullName = "float" } } -> true
                    | _ -> false)
            match floatNumberAlias with
            | Some a -> FsType.Module { m with Types = m.Types |> List.except [ a ] }
            | None -> tp
        | _ -> tp
    )

let emitNowarn (fo: FsFileOut): FsFileOut =

    // suppress warnings about XML Comments (for example for missing comments for parameter)
    let fo =
        let mutable hasXmlComments = false
        let doFix _ = not hasXmlComments
        let fix _ ty =
            match ty |> FsType.comments with
            | [] -> ()
            | [ FsComment.Summary lines ] when lines |> List.forall (not << FsComment.containsXml) -> ()
            | _ -> hasXmlComments <- true
            ty

        fo.Files
        |> List.iter (fixFileEx doFix fix >> ignore)

        if hasXmlComments then
            let noWarn: AdditionalData = (BetweenModuleAndOpen, [ "#nowarn \"3390\" // disable warnings for invalid XML comments"])
            { fo with 
                AdditionalData = fo.AdditionalData @ [ noWarn ]
            }
        else
            fo

    // suppress warnings about usage of Obsolete things
    // for simplicity: always emit when something is obsolete; its usage isn't as easily detectable
    let fo =
        let mutable hasObsolete = false
        let doFix _ = 
            not hasObsolete
        let fix _ ty =
            let obs =
                ty
                |> FsType.attributes
                |> List.exists (
                    List.exists (
                            function
                            | { Namespace = None; Name = "Obsolete" }
                            | { Namespace = Some "System"; Name = "Obsolete" }
                            | { Namespace = None; Name = "System.Obsolete" }
                                -> true
                            | _ -> false
                    )
                )
            if obs then
                hasObsolete <- true
            ty

        fo.Files
        |> List.iter (fixFileEx doFix fix >> ignore)

        if hasObsolete then
            let noWarn: AdditionalData = (BetweenModuleAndOpen, [ "#nowarn \"0044\" // disable warnings for `Obsolete` usage"])
            { fo with 
                AdditionalData = fo.AdditionalData @ [ noWarn ]
            }
        else
            fo

    fo

let private emitKeyOfType (fo: FsFileOut): FsFileOut =
    if fo.Files |> getAllTypes |> List.exists FsType.isKeyOf then
        let keyofType = "[<Erase>] type KeyOf<'T> = Key of string"
        { fo with AbbrevTypes = keyofType :: fo.AbbrevTypes}
    else
        fo

let abbrevTypes =
    [
    "Array",         "type Array<'T> = System.Collections.Generic.IList<'T>"
    "ReadonlyArray", "type ReadonlyArray<'T> = System.Collections.Generic.IReadOnlyList<'T>"
    "ReadonlySet",   "type ReadonlySet<'T> = Set<'T>"
    "ReadonlyMap",   "type ReadonlyMap<'K, 'V> = Map<'K, 'V>"
    "RegExp",        "type RegExp = System.Text.RegularExpressions.Regex"
    "Error",         "type Error = System.Exception"
    "Function",      "type Function = System.Action"
    "Symbol",        "type Symbol = obj"
    "TemplateStringsArray", "type TemplateStringsArray = System.Collections.Generic.IReadOnlyList<string>"
    "ArrayLike",     "type ArrayLike<'T> = System.Collections.Generic.IList<'T>"
    "PromiseLike",   "type PromiseLike<'T> = Fable.Core.JS.Promise<'T>"
    ] |> Map.ofList

let fixFsFileOut fo =

    let mappedTypes =
        fo.Files
        |> getAllTypes
        |> List.choose FsType.asMapped
        |> List.distinct

    let isBrowser =
        mappedTypes
        |> List.exists(fun mp -> mp.Name.StartsWith "HTML")

    let abbrevTypes =
        mappedTypes
        |> List.filter (fun mp -> abbrevTypes |> Map.containsKey mp.Name)
        |> List.map (fun mp -> abbrevTypes |> Map.find mp.Name)
        |> List.distinct
        |> List.sort

    let fixHelperLines (f: FsFile) =
        f |> fixFile (fun ns tp ->
            match tp with
            | FsType.Module md ->
                { md with
                    HelperLines =
                        md.HelperLines |> List.map (fun l ->
                            l.Replace("Option.map","Microsoft.FSharp.Core.Option.map")
                ) } |> FsType.Module
            | _ -> tp )

    let fo = 
        { fo with AbbrevTypes = abbrevTypes @ fo.AbbrevTypes }
        |> emitKeyOfType
    let fo =
        if isBrowser then
            { fo with
                Opens = fo.Opens @ ["Browser.Types"]
                Files = fo.Files |> List.map fixHelperLines }
        else fo

    let fo = emitNowarn fo
    
    fo

let extractGenericParameterDefaults (f: FsFile): FsFile =
    let fix f =
        let extractAliasesFromGenericParameterDefaults attributes comments name tps =
            let aliases = List<FsAlias>()

            tps
            |> List.choose FsType.asGenericTypeParameter
            |> List.iteri (fun i gtp ->
                match gtp.Default with
                | None -> ()
                | Some _ ->
                    {
                        Attributes = attributes
                        //todo: enhancement: remove defaulted (=removed) typeparams from comments
                        Comments = comments
                        Name = name
                        Type =
                            {
                                Type = simpleType name
                                TypeParameters =
                                    tps.[0..(i-1)]
                                    @
                                    (
                                        tps.[i..]
                                        |> List.choose FsType.asGenericTypeParameter
                                        |> List.map (fun p ->
                                            match p.Default with
                                            | Some d ->
                                                match d with
                                                  // `A & B` gets compiled as tuple `A * B` -> prevent
                                                | FsType.Tuple tp when tp.Kind = FsTupleKind.Intersection ->
                                                    None
                                                  // `{}`
                                                | FsType.TypeLiteral _ ->
                                                    None
                                                | t -> Some t
                                            | _ -> None
                                            |> Option.defaultWith (fun _ -> simpleType "obj")
                                        )
                                    )
                            }
                            |> FsType.Generic
                        TypeParameters = tps.[0..(i-1)]
                    }
                    |> aliases.Add
            )

            aliases |> List.ofSeq |> List.map FsType.Alias

        f |> fixFile (fun ns tp ->
            match tp with
            | FsType.Module md ->
                { md with
                    Types =
                        let tps = List<FsType>()
                        md.Types |> List.iter(fun tp ->
                            match tp with
                            | FsType.Interface it ->
                                it.TypeParameters
                                |> extractAliasesFromGenericParameterDefaults it.Attributes it.Comments it.Name
                                |> tps.AddRange

                                tp |> tps.Add
                            | FsType.Alias al ->
                                al.TypeParameters
                                |> extractAliasesFromGenericParameterDefaults al.Attributes al.Comments al.Name
                                |> tps.AddRange

                                tp |> tps.Add
                            | _ -> tp |> tps.Add
                        )

                        tps |> List.ofSeq

                } |> FsType.Module
            | _ -> tp
        )

    // ~= mark default as handled
    let removeDefaults =
        fixFile (fun ns ->
            function
            | FsType.GenericTypeParameter gtp when Option.isSome gtp.Default ->
                { gtp with Default = None }
                |> FsType.GenericTypeParameter
            | t -> t
        )

    f
    |> fix
    |> removeDefaults

let private sealedTypes = 
    [
        "string"
        "float"
        "bool"
        "ReadonlySet"
        "ReadonlyMap"
        "Function"
    ] 
    |> Set.ofList
let removeInvalidGenericConstraints (f: FsFile): FsFile =
    // remove unsupported constraints like `A | B`
    let rec removeUnsupportedType (c: FsType) =
        match c with
          // actual tupe or generic type parameter name
        | FsType.Mapped _ -> Some c
          // generic type 
        | FsType.Generic _ -> Some c
          // `A & B & C` -> only remove unsupported, keep others
        | FsType.Tuple tp when tp.Kind = FsTupleKind.Intersection ->
            match tp.Types |> List.choose removeUnsupportedType with
            | [] -> None
            | tys ->
                { tp with Types = tys }
                |> FsType.Tuple
                |> Some
          // `A | B`
        | FsType.Tuple tp when tp.Kind = FsTupleKind.Intersection -> None
          // inline interface `extends { f(args: ...): void }`
        | FsType.TypeLiteral _ -> None
        | _ -> None

    // cannot use sealed type as constraint
    let rec removeSealed (c: FsType) =
        match c with
        | FsType.Mapped mp when sealedTypes.Contains mp.Name ->
            None
        | FsType.Generic g when removeSealed g.Type |> Option.isNone ->
            None
        | FsType.Tuple tp when tp.Kind = FsTupleKind.Intersection ->
            match tp.Types |> List.choose removeSealed with
            | [] -> None
            | tys ->
                { tp with Types = tys }
                |> FsType.Tuple
                |> Some
        | _ -> Some c

    f |> fixFile (fun _ ->
        function
        | FsType.GenericTypeParameter ({Constraint = Some c} as gp) ->
            { gp with
                Constraint =
                    c
                    |> removeUnsupportedType
                    |> Option.bind removeSealed
            }
            |> FsType.GenericTypeParameter
        | t -> t
    )

let fixTypesHasESKeywords  (f: FsFile): FsFile =
    f |> fixFile (fun ns tp ->
        match tp with
        | FsType.Generic gn ->
            esKeywords
            |> Set.contains (getName tp)
            |> function
                | true -> { gn with Type = simpleType "obj"; TypeParameters = []} |> FsType.Generic
                | _ ->
                    { gn with
                        TypeParameters = gn.TypeParameters |> List.map(fun tp2 ->
                        match tp2 with
                        | FsType.Mapped mp ->
                            if esKeywords.Contains mp.Name then simpleType "obj"
                            else tp2
                        | _ -> tp2
                    )
                    } |> FsType.Generic
        | _ -> tp
    )

let extractTypesInGlobalModules  (f: FsFile): FsFile =
    { f with
        Modules = f.Modules |> List.map(fun md ->
            let tps = List []
            md.Types |> List.iter(fun tp ->
                match tp with
                | FsType.Module md2 ->
                    if md2.Name = "global" then md2.Types |> tps.AddRange
                    else tp |> tps.Add
                | _ -> tp |> tps.Add
            )
            { md with Types = tps |> List.ofSeq }
        )
    }

let removeKeyOfConstraint (f: FsFile): FsFile =
    // remove type parameter `K extends keyof XXX`
    // and replace all occurrences of `K` with `keyof XXX`

    let extractKeyOfTypeParameters (tps: FsType list) =
        let (tps, keyofs) =
            tps
            |> List.choose FsType.asGenericTypeParameter
            |> List.fold (fun (tps, keyofs) tp ->
                match tp.Constraint with
                | Some (FsType.KeyOf { Type = t }) ->
                    (tps, keyofs |> Map.add tp.Name t)
                | _ -> 
                    (tp::tps, keyofs)
            ) ([], Map.empty)
        
        if keyofs |> Map.isEmpty then
            None
        else
            let tps =
                tps
                |> List.rev
                |> List.map FsType.GenericTypeParameter

            Some (tps, keyofs)
    
    let replaceTypeParameters ns (keyofs: Map<string, FsType>) =
        fixType ns (fun _ ->
            function
            | FsType.Mapped mp when keyofs |> Map.containsKey mp.Name ->
                {
                    Type = keyofs |> Map.find mp.Name
                }
                |> FsType.KeyOf
            | t -> t
        )
    
    let fix ns c =
        match c with
        | FsType.Function f ->
            match extractKeyOfTypeParameters f.TypeParameters with
            | None -> c
            | Some (tps, keyofs) ->
                { f with TypeParameters = tps }
                |> FsType.Function
                |> replaceTypeParameters ns keyofs
        | FsType.Interface i -> 
            match extractKeyOfTypeParameters i.TypeParameters with
            | None -> c
            | Some (tps, keyofs) ->
                { i with TypeParameters = tps }
                |> FsType.Interface
                |> replaceTypeParameters ns keyofs
        | FsType.Alias a ->
            match extractKeyOfTypeParameters a.TypeParameters with
            | None -> c
            | Some (tps, keyofs) ->
                { a with TypeParameters = tps }
                |> FsType.Alias
                |> replaceTypeParameters ns keyofs
        | _ -> c

    f
    |> fixFile fix

/// In TS: getter and setter are two distinct functions:
/// ```typescript
/// get length(): number;
/// set length(value: number);  
/// ```
/// -> are read as two properties, one with `ReadOnly`, one `WriteOnly`
/// -> merge into one `ReadWrite` property
/// 
/// Note: it's legal F# to split getter and setter, 
/// but it's probably more common and clearer to merge get and set into a single property.
let mergeReadAndWriteProperties (f: FsFile): FsFile =
    let fix _ =
        function
        | FsType.Interface it ->
            // iff valid TS declaration file:
            // then no need to check:
            // * Number of properties: no more than one getter and one setter allowed
            // * Accessor of properties: no more than one getter and one setter allowed
            // * Types: must match, no overloads possible
            // * Visibility: must match

            let convertToReadWrite =
                it.Members
                |> List.choose (FsType.asProperty)
                |> List.filter (fun p -> p.Accessor <> ReadWrite)
                |> List.groupBy (fun p -> p.Name)
                |> List.filter (fun (_, ps) -> (ps |> List.length) > 1)
                |> List.map fst
                |> Set.ofList

            if convertToReadWrite |> Set.isEmpty then
                it |> FsType.Interface
            else
                // convert read properties to read-write, and remove write-properties
                let members =
                    it.Members
                    |> List.choose (
                        function
                        | FsType.Property p ->
                            if convertToReadWrite |> Set.contains p.Name then
                                match p.Accessor with
                                | ReadOnly -> 
                                    { p with Accessor = ReadWrite }
                                    |> FsType.Property
                                    |> Some
                                | WriteOnly -> None
                                | _ -> None
                            else
                                p 
                                |> FsType.Property
                                |> Some
                        | m -> Some m
                    )

                { it with Members = members }
                |> FsType.Interface
        | t -> t

    f |> fixFile fix
