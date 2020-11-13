// ts2fable 0.0.0
module rec #288-type-alias-float-number
open System
open Fable.Core
open Fable.Core.JS

let [<Import("TypeAloasFloatNumber","#288-type-alias-float-number")>] typeAloasFloatNumber: TypeAloasFloatNumber.IExports = jsNative

module TypeAloasFloatNumber =

    type [<AllowNullLiteral>] IExports =
        abstract Class: ClassStatic

    type [<AllowNullLiteral>] Class =
        /// param f and return value should be "float" 
        abstract Foo: f: float -> float

    type [<AllowNullLiteral>] ClassStatic =
        [<EmitConstructor>] abstract Create: unit -> Class
