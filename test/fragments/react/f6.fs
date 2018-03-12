// ts2fable 0.0.0
module rec f6
open System
open Fable.Core
open Fable.Import.JS

type Component = React.Component
type ComponentState = React.ComponentState
type ComponentClass = React.ComponentClass

type [<AllowNullLiteral>] ClassType<'P, 'T, 'C> =
    interface end
