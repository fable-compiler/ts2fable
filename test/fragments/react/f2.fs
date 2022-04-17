// ts2fable 0.0.0
module rec f2
open System
open Fable.Core
open Fable.Core.JS
open Browser.Types

type ClassAttributes = React.ClassAttributes
type ReactNode = React.ReactNode
type DOMElement = React.DOMElement
type HTMLAttributes = React.HTMLAttributes
type DOMAttributes = React.DOMAttributes
type DetailedReactHTMLElement = React.DetailedReactHTMLElement

type [<AllowNullLiteral>] DOMFactory<'P, 'T when 'P :> DOMAttributes<'T> and 'T :> Element> =
    [<Emit("$0($1...)")>] abstract Invoke: ?props: obj * [<ParamArray>] children: ReactNode[] -> DOMElement<'P, 'T>

type [<AllowNullLiteral>] DetailedHTMLFactory<'P, 'T when 'P :> HTMLAttributes<'T> and 'T :> HTMLElement> =
    inherit DOMFactory<'P, 'T>
    [<Emit("$0($1...)")>] abstract Invoke: ?props: obj * [<ParamArray>] children: ReactNode[] -> DetailedReactHTMLElement<'P, 'T>
