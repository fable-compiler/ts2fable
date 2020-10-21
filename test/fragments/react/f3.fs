// ts2fable 0.0.0
module rec f3
open System
open Fable.Core
open Fable.Core.JS

type ComponentLifecycle = React.ComponentLifecycle
type ComponentClass = React.ComponentClass
type StatelessComponent = React.StatelessComponent

type Component =
    Component<obj, obj>

type Component<'P> =
    Component<'P, obj>

type [<AllowNullLiteral>] Component<'P, 'S> =
    inherit ComponentLifecycle<'P, 'S>
