// ts2fable 0.0.0
module rec f6
open System
open Fable.Core
open Fable.Import.JS

type ComponentLifecycle = React.ComponentLifecycle
type ReactNode = React.ReactNode
type ReactInstance = React.ReactInstance

type [<AllowNullLiteral>] IExports =
    abstract Component: ComponentStatic

type [<AllowNullLiteral>] Component<'P, 'S> =
    abstract state: obj with get, set
    abstract defaultProps: obj option with get, set

type [<AllowNullLiteral>] ComponentStatic =
    [<Emit "new $0($1...)">] abstract Create: props: 'P * ?context: obj option -> Component<'P, 'S>
