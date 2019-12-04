// ts2fable 0.0.0
module rec ReactXP
open System
open Fable.Core
open Fable.Core.JS

module ReactXP = __web_ReactXP

module __common_Accessibility =

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
    type CommonAccessibility = __common_Accessibility.Accessibility

    type [<AllowNullLiteral>] IExports =
        abstract Accessibility: AccessibilityStatic
        abstract _default: Accessibility

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

module __windows_App =
    type ComponentProvider = React_native.ComponentProvider
