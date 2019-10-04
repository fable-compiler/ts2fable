// ts2fable 0.0.0
module rec f2
open System
open Fable.Core
open Fable.Core.JS

type ClassAttributes = React.ClassAttributes
type ReactNode = React.ReactNode
type DOMElement = React.DOMElement
type HTMLAttributes = React.HTMLAttributes
type DOMAttributes = React.DOMAttributes
type DetailedReactHTMLElement = React.DetailedReactHTMLElement

type [<AllowNullLiteral>] DOMFactory<'P, 'T> =
    [<Emit "$0($1...)">] abstract Invoke: ?props: obj * [<ParamArray>] children: ResizeArray<ReactNode> -> DOMElement<'P, 'T>

type [<AllowNullLiteral>] DetailedHTMLFactory<'P, 'T> =
    inherit DOMFactory<'P, 'T>
    [<Emit "$0($1...)">] abstract Invoke: ?props: obj * [<ParamArray>] children: ResizeArray<ReactNode> -> DetailedReactHTMLElement<'P, 'T>
