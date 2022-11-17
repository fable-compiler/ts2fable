[<AutoOpen>]
module rec ts2fable.Syntax
open System.Net.Cache
// our simplified syntax tree
// some names inspired by the actual F# AST:
// https://github.com/fsharp/FSharp.Compiler.Service/blob/master/src/fsharp/ast.fs

[<RequireQualifiedAccess>]
type FsAccessibility =
| Public
| Protected
| Private

type FsInterface =
    {
        Attributes: FsAttributeSet list
        Comments: FsComment list
        IsStatic: bool // contains only static functions
        IsClass: bool
        Name: string
        FullName: string
        TypeParameters: FsType list
        Inherits: FsType list
        Members: FsType list
        Accessibility : FsAccessibility option
    }
with
    member x.HasStaticMembers = x.Members |> List.exists isStatic
    member x.StaticMembers = x.Members |> List.filter isStatic
    member x.NonStaticMembers = x.Members |> List.filter (not << isStatic)
    member x.HasConstructor = x.Members |> List.exists isConstructor
    member x.Constructors = x.Members |> List.filter isConstructor

type FsTypeLiteral =
    {
        Members: FsType list
    }

type FsGenericTypeParameter =
    {
        Name: string
        Constraint: FsType option
        Default: FsType option
    }

[<RequireQualifiedAccess>]
type FsLiteral =
    | String of string
    | Int of int
    | Float of float
    | Bool of bool
with
    member x.Value =
        match x with
        | String s -> s
        | Int i -> string i
        | Float f -> string f
        | Bool b -> string b

[<RequireQualifiedAccess>]
type FsEnumCaseType =
    | Numeric
    | String
    | Unknown

type FsEnumCase =
    {
        Attributes: FsAttributeSet list
        Comments: FsComment list
        Name: string
        Value: FsLiteral option
    }

type FsEnum =
    {
        Attributes: FsAttributeSet list
        Comments: FsComment list
        Name: string
        FullName: string
        Cases: FsEnumCase list
    }
with
    member x.Type =
        if x.Cases |> List.forall (fun x ->
            match x.Value with
            | Some (FsLiteral.Float _ | FsLiteral.Int _) -> true
            | Some _ -> false
            | None -> true) then FsEnumCaseType.Numeric
        else if x.Cases |> List.forall (fun x ->
            match x.Value with
            | Some (FsLiteral.String _) -> true
            | Some _ -> false
            | None -> true) then FsEnumCaseType.String
        else FsEnumCaseType.Unknown

type FsCommentLine = string
type FsCommentContent = FsCommentLine list

type FsCommentTag =
    {
        Name: string
        Content: FsCommentContent
    }

type FsCommentLinkType =
    | HRef
    | CRef
type FsCommentLink =
    {
        Type: FsCommentLinkType
        Target: string
        Content: FsCommentContent
    }

type FsCommentException =
    {
        Type: string option
        Content: FsCommentContent
    }

[<RequireQualifiedAccess>]
type FsComment =
    | Summary of FsCommentContent
    | Param of FsCommentTag
    | TypeParam of FsCommentTag
    | Returns of FsCommentContent
    | Remarks of FsCommentContent
    | SeeAlso of FsCommentLink
    | Example of FsCommentContent
    | Exception of FsCommentException
      // non-standard tags
    | Version of FsCommentContent
    | Default of FsCommentContent
      /// Used for non-standard tags
    | Tag of FsCommentTag
    | UnknownTag of FsCommentTag
    | Unknown of FsCommentContent

[<RequireQualifiedAccess>]
module FsComment =
    let isSummary v = match v with | FsComment.Summary _ -> true | _ -> false
    let asSummary v = match v with | FsComment.Summary o -> Some o | _ -> None

    let justSummary comments =
        match comments with
        | [ FsComment.Summary s ] -> Some s
        | _ -> None

    /// Checks if passed xml comment text contains xml comment tags
    let containsXml (text: string) =
        //cannot test for just < or > -> might not be xml tags like `the return value should be Option<string>`
        // -> look for valid_ish xml doc tags
        [
            "<para>"
            "<code>"
            "<c>"
            "<paramref name="
            "<typeparamref name="
            "<see href="
            "<see cref="
        ]
        |> List.exists text.Contains

type FsParam =
    {
        Name: string
        Optional: bool
        ParamArray: bool
        Type: FsType
    }

[<RequireQualifiedAccess>]
module FsParam =
    let isStringLiteral (p: FsParam): bool = FsType.isStringLiteral p.Type

[<RequireQualifiedAccess>]
type FsFunctionKind =
    | Regular
    | Constructor
    | Call
    | StringParam of string

type FsFunction =
    {
        Attributes: FsAttributeSet list
        Comments: FsComment list
        Kind: FsFunctionKind
        IsStatic: bool
        Name: string option // declarations have them, signatures do not
        TypeParameters: FsType list
        Params: FsParam list
        ReturnType: FsType
        Accessibility : FsAccessibility option
    }
with
    member x.HasStringLiteralParams = x.Params |> List.exists FsParam.isStringLiteral
    member x.StringLiteralParams = x.Params |> List.filter FsParam.isStringLiteral
    member x.NonStringLiteralParams = x.Params |> List.filter (not << FsParam.isStringLiteral)

[<RequireQualifiedAccess>]
type FsPropertyKind =
    | Regular
    | Index

type FsAccessor =
    | ReadOnly
    | WriteOnly
    | ReadWrite
module FsAccessor =
    let fromReadonly (isReadonly: bool) =
        if isReadonly then
            ReadOnly
        else
            ReadWrite

type FsProperty =
    {
        Attributes: FsAttributeSet list
        Comments: FsComment list
        Kind: FsPropertyKind
        Index: FsParam option
        Name: string
        Option: bool
        Type: FsType
        Accessor: FsAccessor
        IsStatic: bool
        Accessibility : FsAccessibility option
    }

type FsGenericType =
    {
        Type: FsType
        TypeParameters: FsType list
    }

type FsUnion =
    {
        Option: bool
        Types: FsType list
    }

type FsAlias =
    {
        Attributes: FsAttributeSet list
        Comments: FsComment list
        Name: string
        FullName: string
        Type: FsType
        TypeParameters: FsType list
    }

type FsTag =
    {
        Name: string option
        Value: FsLiteral
    }

type FsTaggedUnionAlias =
    {
        Attributes: FsAttributeSet list
        Comments: FsComment list
        Name: string
        Discriminator: string
        Cases: Map<FsTag, FsType>
        TypeParameters: FsType list
    }

[<RequireQualifiedAccess>]
type FsTupleKind =
    | Intersection
    | Tuple
    | Mapped

type FsTuple =
    {
        Types: FsType list
        Kind: FsTupleKind
    }
type FsImportSpecifier =
    {
        PropertyName: string option
        Name: string
    }

type FsTypeImport =
    {
        ImportSpecifier: FsImportSpecifier
        SpecifiedModule: string
        ResolvedModule: string option
    }

type ModuleImport =
    {
        Module: string
        SpecifiedModule: string
        ResolvedModule: string option
    }

[<RequireQualifiedAccess>]
type FsImport =
    | Type of FsTypeImport
    | Module of ModuleImport

type FsExport =
    {
        IsGlobal: bool
        Selector: string
        Path: string
    }
[<CustomEquality;CustomComparison>]
type FsVariable =
    {
        Attributes: FsAttributeSet list
        Comments: FsComment list
        Export: FsExport option
        HasDeclare: bool
        Name: string
        Type: FsType
        IsConst: bool
        IsStatic: bool
        Accessibility : FsAccessibility option
    }

with
    member x.IsGlobal = x.Export.IsSome && x.Export.Value.IsGlobal
    override x.Equals(y) =
        match y with
        | :? FsVariable as other -> x.Name = other.Name
        | _ -> false
    override x.GetHashCode() = hash x.Name
    interface System.IComparable with
        member x.CompareTo y =
            match y with
            | :? FsVariable as vb -> compare x.Name vb.Name
            | _ -> invalidArg "y" "cannot compare values of different types"

type FsKeyOf = {
    Type: FsType
}

[<RequireQualifiedAccess>]
type FsMappedDeclaration =
    | Type of FsType
    | EnumCase of (FsEnum * FsEnumCase)

type [<CustomEquality; CustomComparison>] FsMapped =
    {
        // Namespace: string list // TODO
        Name: string
        FullName: string
        /// The declarations of this type. Reader would run on creating the lazy value for the first time.
        ///
        /// This must be lazy because mutually-recurisive types would contain each other...
        Declarations: Lazy<FsMappedDeclaration list>
    }
with
    member x.AsComparable =
        (x.Name, x.FullName)
    override x.Equals(yo) =
        match yo with
        | :? FsMapped as y -> x.AsComparable = y.AsComparable
        | _ -> false
    override x.GetHashCode() = x.AsComparable.GetHashCode()
    interface System.IComparable with
        member x.CompareTo(yo) =
            match yo with
            | :? FsMapped as y -> compare x.AsComparable y.AsComparable
            | _ -> invalidArg "yo" "cannot compare values"

type FsArgument = {
    /// Named argument
    Name: string option
    /// stringyfied value
    /// -> includes quotation marks for string argument: `"\"MyVal\""`
    Value: string
}
module FsArgument =
    let justValue value =
        {
            Name = None
            Value = value
        }
type FsAttribute = {
    /// might possible be `open`ed.
    ///
    /// NOT included in `Name`
    /// -> `System.Obsolete`: `Namespace`=`System`; `Name`=`Obsolete`
    ///
    /// Place Full Name in `Name` to emit full name.
    Namespace: string option
    Name: string
    Arguments: FsArgument list
}
module FsAttribute =
    let fromName name =
        {
            Namespace = None
            Name = name
            Arguments = []
        }
/// Attributes in a single set will be placed in common brackets:
/// `[<A(...); B(...)>]`
type FsAttributeSet = FsAttribute list

let simpleType name: FsType =
    {
        // Namespace = []
        Name = name
        FullName = name
        Declarations = Lazy.CreateFromValue []
    }
    |> FsType.Mapped

[<RequireQualifiedAccess; StructuralEquality; StructuralComparison>]
type FsType =
    | Interface of FsInterface
    | Enum of FsEnum
    | Property of FsProperty
    | Param of FsParam
    | Array of FsType
    | TODO
    | None // when it is not set
    | Mapped of FsMapped
    | Function of FsFunction
    | Union of FsUnion
    | Alias of FsAlias
    | TaggedUnionAlias of FsTaggedUnionAlias
    | Generic of FsGenericType
    | Tuple of FsTuple
    | Module of FsModule
    | File of FsFile
    | FileOut of FsFileOut
    | Variable of FsVariable
    | Literal of FsLiteral
    | ExportAssignment of string
    | This
    | Import of FsImport
    | TypeLiteral of FsTypeLiteral
    | GenericTypeParameter of FsGenericTypeParameter
    | KeyOf of FsKeyOf

[<RequireQualifiedAccess>]
module FsType =
    open Fable.Core.JsInterop

    let isMapped tp = match tp with | FsType.Mapped _ -> true | _ -> false
    let isFunction tp = match tp with | FsType.Function _ -> true | _ -> false
    let isInterface tp = match tp with | FsType.Interface _ -> true | _ -> false
    let isStringLiteral tp = match tp with | FsType.Literal (FsLiteral.String _) -> true | _ -> false
    let isModule tp = match tp with | FsType.Module _ -> true | _ -> false
    let isImport tp = match tp with | FsType.Import _ -> true | _ -> false
    let isVariable tp = match tp with | FsType.Variable _ -> true | _ -> false
    let isAlias tp = match tp with | FsType.Alias _ -> true | _ -> false
    let isGeneric tp = match tp with | FsType.Generic _ -> true | _ -> false
    let isKeyOf tp = match tp with | FsType.KeyOf _ -> true | _ -> false
    let isTuple tp = match tp with | FsType.Tuple _ -> true | _ -> false
    let isProperty tp = match tp with | FsType.Property _ -> true | _ -> false

    let asMapped (tp: FsType) = match tp with | FsType.Mapped v -> Some v | _ -> None
    let asFunction (tp: FsType) = match tp with | FsType.Function v -> Some v | _ -> None
    let asInterface (tp: FsType) = match tp with | FsType.Interface v -> Some v | _ -> None
    let asGeneric (tp: FsType) = match tp with | FsType.Generic v -> Some v | _ -> None
    let asStringLiteral (tp: FsType): string option = match tp with | FsType.Literal (FsLiteral.String v) -> Some v | _ -> None
    let asModule (tp: FsType) = match tp with | FsType.Module v -> Some v | _ -> None
    let asVariable (tp: FsType) = match tp with | FsType.Variable v -> Some v | _ -> None
    let asExportAssignment (tp: FsType) = match tp with | FsType.ExportAssignment v -> Some v | _ -> None
    let asGenericTypeParameter (tp: FsType) = match tp with | FsType.GenericTypeParameter v -> Some v | _ -> None
    let asTuple (tp: FsType) = match tp with | FsType.Tuple v -> Some v | _ -> None
    let asProperty (tp: FsType) = match tp with | FsType.Property v -> Some v | _ -> None

    let private unsafeTryGetContainingField (name: string) (tp: FsType): 'a option =
        match Reflection.FSharpValue.GetUnionFields (tp, typeof<FsType>) with
        | (_, [|o|]) ->
            !!o?(name)
        | _ -> None

    let comments (tp: FsType): FsComment list =
        tp
        |> unsafeTryGetContainingField "Comments"
        |> Option.defaultValue []

    let attributes (tp: FsType): FsAttributeSet list =
        tp
        |> unsafeTryGetContainingField "Attributes"
        |> Option.defaultValue []


type FsModule =
    {
        Comments: FsComment list
        HasDeclare: bool
        IsNamespace: bool
        Name: string
        Types: FsType list
        HelperLines: string list
        Attributes: FsAttributeSet list
    }
with
    member x.IsHelper = x.HelperLines.Length > 0

[<RequireQualifiedAccess>]
type FsFileKind =
    | Index
    | Extra of string(*relative path to index file*)


type FsFile =
    {
        Kind: FsFileKind
        FileName: string
        ModuleName: string
        Modules: FsModule list
    }

type Line = string
type FsAdditionalData = {
    Top: Line list
    BetweenModuleAndOpen: Line list
    BetweenOpenAndTypes: Line list
    Bottom: Line list
}

type AdditionalDataLocation =
    | Top
    | Bottom
    | BetweenModuleAndOpen
    | BetweenOpenAndTypes
type AdditionalData = AdditionalDataLocation * (Line list)

type FsFileOut =
    {
        Namespace: string
        Opens: string list
        Files: FsFile list
        AbbrevTypes: string list
        /// Can be used to output additional text not covered by any `FsXXX` type
        ///
        /// Used to print `#nowarn` or might be useful to output debug logs.
        AdditionalData: AdditionalData list
    }

let isStatic (tp: FsType) =
    match tp with
    | FsType.Function fn -> fn.IsStatic
    | FsType.Interface it -> it.IsStatic
    | FsType.Property p -> p.IsStatic
    | FsType.Variable v -> v.IsStatic
    | _ -> false

let isConstructor (tp: FsType) =
    match tp with
    | FsType.Function fn -> fn.Kind = FsFunctionKind.Constructor
    | _ -> false

let rec getName (tp: FsType) =
    match tp with
    | FsType.Interface it -> it.Name
    | FsType.Enum en -> en.Name
    | FsType.Param pm -> pm.Name
    | FsType.Function fn -> fn.Name |> Option.defaultValue ""
    | FsType.Property pr -> pr.Name
    | FsType.Alias al -> al.Name
    | FsType.Variable vb -> vb.Name
    | FsType.Module md -> md.Name
    | FsType.File fl -> fl.ModuleName
    | FsType.Generic gn -> getName gn.Type
    | FsType.Mapped mp -> mp.Name
    | FsType.Import im ->
        match im with
        | FsImport.Type imtp -> imtp.ImportSpecifier.Name
        | _ -> ""
    | FsType.Array ar ->
        let name = getName ar
        if name = "" then "" else sprintf "%sArray" name
    | _ -> ""

let rec getFullName (tp: FsType) =
    match tp with
    | FsType.Interface it -> it.FullName
    | FsType.Mapped en -> en.FullName
    | FsType.Generic gn -> getFullName gn.Type
    | FsType.File fl -> fl.FileName
    | _ -> getName tp

let getTypeName (tp: FsType) =
    match tp with
    | FsType.Interface t -> t.GetType().ToString()
    | FsType.Function t -> t.GetType().ToString()
    | FsType.Property t -> t.GetType().ToString()
    | FsType.Variable t -> t.GetType().ToString()
    | FsType.Module t -> t.GetType().ToString()
    | FsType.Enum t -> t.GetType().ToString()
    | FsType.Param t -> t.GetType().ToString()
    | FsType.Alias t -> t.GetType().ToString()
    | FsType.TaggedUnionAlias t -> t.GetType().ToString()
    | FsType.File t -> t.GetType().ToString()
    | FsType.Generic t -> t.GetType().ToString()
    | FsType.Mapped t -> t.GetType().ToString()
    | FsType.Import t -> t.GetType().ToString()
    | FsType.Array t -> t.GetType().ToString()
    | FsType.ExportAssignment t -> t.GetType().ToString()
    | FsType.GenericTypeParameter t -> t.GetType().ToString()
    | FsType.KeyOf t -> t.GetType().ToString()
    | FsType.None as t -> t.GetType().ToString() + ".None"
    | FsType.TODO as t -> t.GetType().ToString() + ".TODO"
    | FsType.Literal l ->
        match l with
        | FsLiteral.String t -> t.GetType().ToString()
        | FsLiteral.Int t -> t.GetType().ToString()
        | FsLiteral.Float t -> t.GetType().ToString()
        | FsLiteral.Bool t -> t.GetType().ToString()
    | FsType.This as t -> t.GetType().ToString() + ".This"
    | FsType.Tuple t -> t.GetType().ToString()
    | FsType.TypeLiteral t -> t.GetType().ToString()
    | FsType.Union t -> t.GetType().ToString()
    | FsType.FileOut t -> t.GetType().ToString()

let getAccessibility (tp: FsType) : FsAccessibility option =
    match tp with
    | FsType.Interface it -> it.Accessibility
    | FsType.Function fn -> fn.Accessibility
    | FsType.Property pr -> pr.Accessibility
    | FsType.Variable vb -> vb.Accessibility
    | FsType.Module _
    | FsType.Enum _
    | FsType.Param _
    | FsType.Alias _
    | FsType.TaggedUnionAlias _
    | FsType.File _
    | FsType.Generic _
    | FsType.Mapped _
    | FsType.Import _
    | FsType.Array _
    | FsType.ExportAssignment _
    | FsType.GenericTypeParameter _
    | FsType.KeyOf _
    | FsType.None
    | FsType.TODO
    | FsType.Literal _
    | FsType.This
    | FsType.Tuple _
    | FsType.TypeLiteral _
    | FsType.Union _
    | FsType.FileOut _ -> None
