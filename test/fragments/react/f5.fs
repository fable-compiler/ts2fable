// ts2fable 0.0.0
module rec f5
open System
open Fable.Core
open Fable.Core.JS

let [<Import("*","test")>] react: React.IExports = jsNative

module React =

    type a =
        JSX.ElementAttributesProperty

module JSX =

    type [<AllowNullLiteral>] ElementAttributesProperty =
        abstract props: TypeLiteral_01 with get, set

    type [<AllowNullLiteral>] TypeLiteral_01 =
        interface end
