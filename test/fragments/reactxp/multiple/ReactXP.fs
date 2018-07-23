// ts2fable 0.0.0
module rec ReactXP
open System
open Fable.Core
open Fable.Import.JS

module ReactXP = __web_ReactXP

module __common_Accessibility =
    let [<Import("","test/common/Accessibility")>] : ``.IExports`` = jsNative

    type [<AllowNullLiteral>] IExports =
        abstract Accessibility: AccessibilityStatic

    type [<AllowNullLiteral>] Accessibility =
        abstract isScreenReaderEnabled: unit -> bool
        abstract screenReaderChangedEvent: SubscribableEvent<(bool -> unit)> with get, set
        abstract isHighContrastEnabled: unit -> bool
        abstract newAnnouncementReadyEvent: SubscribableEvent<(string -> unit)> with get, set
        abstract announceForAccessibility: announcement: string -> unit

    type [<AllowNullLiteral>] AccessibilityStatic =
        [<Emit "new $0($1...)">] abstract Create: unit -> Accessibility

module __web_Accessibility =
    let [<Import("","test/web/Accessibility")>] : ``.IExports`` = jsNative
    type CommonAccessibility = __common_Accessibility.Accessibility
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
