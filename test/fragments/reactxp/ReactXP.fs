// ts2fable 0.0.0
module rec ReactXP
open System
open Fable.Core
open Fable.Import.JS

module ReactXP = __web_ReactXP

module __web_Accessibility =
    let [<Import("","test/web/Accessibility")>] : ``.IExports`` = jsNative
    type Accessibility as CommonAccessibility = ___common_Accessibility.Accessibility as CommonAccessibility
    let [<Import("_default","test")>] _default: Accessibility = jsNative

    type [<AllowNullLiteral>] IExports =
        abstract Accessibility: AccessibilityStatic

    type [<AllowNullLiteral>] Accessibility =
        inherit CommonAccessibility
        abstract isScreenReaderEnabled: unit -> bool

    type [<AllowNullLiteral>] AccessibilityStatic =
        [<Emit "new $0($1...)">] abstract Create: unit -> Accessibility

module __web_ReactXP =
    let [<Import("*","test/web/ReactXP")>] reactXP: ReactXP.IExports = jsNative

    module ReactXP =

        type [<AllowNullLiteral>] IExports =
            abstract __spread: obj option
