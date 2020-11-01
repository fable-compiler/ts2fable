// ts2fable 0.0.0
module rec f5
open System
open Fable.Core
open Fable.Core.JS

let [<Import("*","f5")>] react: React.IExports = jsNative

module React =

    type a =
        JSX.ElementAttributesProperty

module JSX =

    type [<AllowNullLiteral>] ElementAttributesProperty =
        abstract props: ElementAttributesPropertyProps with get, set

    type [<AllowNullLiteral>] ElementAttributesPropertyProps =
        interface end
