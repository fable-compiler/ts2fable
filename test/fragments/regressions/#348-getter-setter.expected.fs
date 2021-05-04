// ts2fable 0.0.0
module rec ``#348-getter-setter``

#nowarn "3390" // disable warnings for invalid XML comments

open System
open Fable.Core
open Fable.Core.JS


type [<AllowNullLiteral>] IExports =
    abstract I: IStatic
    abstract SKey: SKeyStatic

type [<AllowNullLiteral>] I =
    /// get,set
    abstract length1: float with get, set
    /// get
    abstract length2: float
    /// get
    abstract length3: float
    /// set
    abstract length4: float with set
    /// get,set
    abstract length5: float with get, set

type [<AllowNullLiteral>] IStatic =
    [<EmitConstructor>] abstract Create: unit -> I

type [<AllowNullLiteral>] SKey =
    /// get
    abstract name: string
    /// get
    abstract sName: string
    /// get
    /// 
    /// Returns the name in the format, {schemaName}.{name}.
    abstract fullName: string
    /// <summary>Checks whether this SKey matches the one provided.</summary>
    /// <param name="rhs">The SKey to compare to this.</param>
    abstract matches: rhs: SKey -> bool
    abstract matchesFullName: name: string -> bool

type [<AllowNullLiteral>] SKeyStatic =
    [<EmitConstructor>] abstract Create: name: string -> SKey
