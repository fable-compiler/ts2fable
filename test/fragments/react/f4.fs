// ts2fable 0.0.0
module rec f4
open System
open Fable.Core
open Fable.Core.JS

type DetailedHTMLFactory = React.DetailedHTMLFactory
type DialogHTMLAttributes = React.DialogHTMLAttributes

module React =

    type [<AllowNullLiteral>] IExports =
        abstract Component: ComponentStatic

    type [<AllowNullLiteral>] Component<'P, 'S> =
        abstract setState: state: U2<(obj -> 'P -> U2<obj, 'S>), U2<obj, 'S>> * ?callback: (unit -> obj option) -> unit

    type [<AllowNullLiteral>] ComponentStatic =
        [<Emit "new $0($1...)">] abstract Create: unit -> Component<'P, 'S>

type StatelessComponent =
    StatelessComponent<obj>

type [<AllowNullLiteral>] StatelessComponent<'P> =
    abstract defaultProps: obj option with get, set

type [<AllowNullLiteral>] ReactHTML =
    abstract dialog: DetailedHTMLFactory<DialogHTMLAttributes<obj>, obj> with get, set
