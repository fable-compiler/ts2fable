// ts2fable 0.0.0
module rec ``#288-type-alias-float-number``
open System
open Fable.Core
open Fable.Import.JS

let [<Import("TypeAloasFloatNumber","test")>] typeAloasFloatNumber: TypeAloasFloatNumber.IExports = jsNative

module TypeAloasFloatNumber =

    type [<AllowNullLiteral>] IExports =
        abstract Class: ClassStatic

    type [<AllowNullLiteral>] Class =
        /// param f and return value should be "float" 
        abstract Foo: f: float -> float

    type [<AllowNullLiteral>] ClassStatic =
        [<Emit "new $0($1...)">] abstract Create: unit -> Class
