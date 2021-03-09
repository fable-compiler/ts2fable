// ts2fable 0.0.0
module rec ``#275-private-members``
open System
open Fable.Core
open Fable.Core.JS

let [<Import("PrivateMembersTests","#275-private-members")>] privateMembersTests: PrivateMembersTests.IExports = jsNative

module PrivateMembersTests =

    type [<AllowNullLiteral>] IExports =
        /// No explicit ctor, so an implicit one should be generated
        abstract ImplicitCtor: ImplicitCtorStatic
        /// explicit paramless ctor, no implicit one should be generated
        abstract ExplicitCtor: ExplicitCtorStatic
        /// explicit ctor, no implicit one should be generated
        abstract ExplicitCtor2: ExplicitCtor2Static
        /// public property named i should be emitted
        abstract PublicField: PublicFieldStatic
        /// no field should be emitted
        abstract PrivateField: PrivateFieldStatic

    /// No explicit ctor, so an implicit one should be generated 
    type [<AllowNullLiteral>] ImplicitCtor =
        interface end

    /// No explicit ctor, so an implicit one should be generated 
    type [<AllowNullLiteral>] ImplicitCtorStatic =
        [<EmitConstructor>] abstract Create: unit -> ImplicitCtor

    /// explicit paramless ctor, no implicit one should be generated 
    type [<AllowNullLiteral>] ExplicitCtor =
        interface end

    /// explicit paramless ctor, no implicit one should be generated 
    type [<AllowNullLiteral>] ExplicitCtorStatic =
        [<EmitConstructor>] abstract Create: unit -> ExplicitCtor

    /// explicit ctor, no implicit one should be generated 
    type [<AllowNullLiteral>] ExplicitCtor2 =
        interface end

    /// explicit ctor, no implicit one should be generated 
    type [<AllowNullLiteral>] ExplicitCtor2Static =
        [<EmitConstructor>] abstract Create: i: float -> ExplicitCtor2

    /// explicit private ctor, no ctor should be emitted at all 
    type [<AllowNullLiteral>] PrivateCtor =
        interface end

    /// public property named i should be emitted 
    type [<AllowNullLiteral>] PublicField =
        abstract i: float with get, set

    /// public property named i should be emitted 
    type [<AllowNullLiteral>] PublicFieldStatic =
        [<EmitConstructor>] abstract Create: unit -> PublicField

    /// no field should be emitted 
    type [<AllowNullLiteral>] PrivateField =
        interface end

    /// no field should be emitted 
    type [<AllowNullLiteral>] PrivateFieldStatic =
        [<EmitConstructor>] abstract Create: unit -> PrivateField
