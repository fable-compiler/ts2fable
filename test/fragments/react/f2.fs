// ts2fable 0.0.0
module rec f2
open System
open Fable.Core
open Fable.Import.JS

type ComponentLifecycle = React.ComponentLifecycle

type [<AllowNullLiteral>] Component<'P, 'S> =
    inherit ComponentLifecycle<'P, 'S>

type Component<'S> =
    Component<obj, 'S>

type Component =
    Component<obj, obj>
