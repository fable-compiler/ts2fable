// ts2fable 0.0.0
module rec #277-unwrap-options
open System
open Fable.Core
open Fable.Core.JS

let [<Import("UnwrapOptions","#277-unwrap-options")>] unwrapOptions: UnwrapOptions.IExports = jsNative
let [<Import("UnwrapOptionsAlias","#277-unwrap-options")>] unwrapOptionsAlias: UnwrapOptionsAlias.IExports = jsNative

module UnwrapOptions =

    type [<AllowNullLiteral>] IExports =
        abstract Class: ClassStatic

    type [<AllowNullLiteral>] Class =
        /// The parameter should be "?p : string", NOT "?p : Option<string>"
        abstract Foo: ?p: string -> unit
        /// The parameter should be "?p : string", NOT "?p : Option<string>"
        abstract Foo2: ?p: string -> unit
        /// the return value should be Option<string> 
        abstract Bar: unit -> string option
        /// the return value should be Option<string> 
        abstract Bar2: unit -> string option

    type [<AllowNullLiteral>] ClassStatic =
        [<EmitConstructor>] abstract Create: unit -> Class

module UnwrapOptionsAlias =

    type [<AllowNullLiteral>] IExports =
        abstract Class: ClassStatic

    /// Alias type for value that can be null
    type Nullable<'T> =
        'T option

    type [<AllowNullLiteral>] Class =
        /// The parameter should be "?p : string", NOT "?p : Option<string>"
        abstract Foo: ?p: string -> unit
        /// (DOES NOT YET WORK) The parameter should be "?p : string", NOT "?p : Option<string>"
        abstract Foo2: ?p: Nullable<string> -> unit
        /// (DOES NOT YET WORK) The parameter should be "?p : string", NOT "?p : Option<string>"
        abstract Foo3: ?p: Nullable<string> -> unit
        /// the return value should be Option<string> 
        abstract Bar: unit -> Nullable<string>
        /// (DOES NOT YET WORK) the return value should be Option<string> 
        abstract Bar2: unit -> Nullable<string> option
        /// (DOES NOT YET WORK) the return value should be Option<string> 
        abstract Bar3: unit -> Nullable<string> option

    type [<AllowNullLiteral>] ClassStatic =
        [<EmitConstructor>] abstract Create: unit -> Class
