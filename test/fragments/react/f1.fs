// ts2fable 0.0.0
module rec f1
open System
open Fable.Core
open Fable.Import.JS

type ClassAttributes = React.ClassAttributes
type ReactNode = React.ReactNode
type DOMElement = React.DOMElement
type HTMLAttributes = React.HTMLAttributes
type DOMAttributes = React.DOMAttributes
type DetailedReactHTMLElement = React.DetailedReactHTMLElement

type [<AllowNullLiteral>] DOMFactory<'P, 'T> =
    abstract invoke: ?props: obj option * [<ParamArray>] children: ResizeArray<ReactNode> -> DOMElement<'P, 'T>

type [<AllowNullLiteral>] DetailedHTMLFactory<'P, 'T> =
    inherit DOMFactory<'P, 'T>
    [<Emit "$0($1...)">] abstract Invoke: ?props: obj option * [<ParamArray>] children: ResizeArray<ReactNode> -> DetailedReactHTMLElement<'P, 'T>
