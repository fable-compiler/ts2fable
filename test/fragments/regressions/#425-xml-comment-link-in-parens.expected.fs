// ts2fable 0.0.0
module rec ``#425-xml-comment-link-in-parens``

#nowarn "3390" // disable warnings for invalid XML comments

open System
open Fable.Core
open Fable.Core.JS


/// <summary>
/// Markdown-Link in parens: (<see href="http://fable.io/ts2fable/">ts2fable</see>)
/// Markdown-Link in two parens: ((<see href="http://fable.io/ts2fable/">ts2fable</see>))
/// Markdown-Link in brackets: [<see href="http://fable.io/ts2fable/">ts2fable</see>]
/// Markdown-Link in two brackets: [[<see href="http://fable.io/ts2fable/">ts2fable</see>]]
/// Markdown-Link in braces: {<see href="http://fable.io/ts2fable/">ts2fable</see>}
/// Markdown-Link in two braces: {{<see href="http://fable.io/ts2fable/">ts2fable</see>}}
/// 
/// JSDoc-Link in parens: (<see href="http://fable.io/ts2fable/">ts2fable</see>)
/// JSDoc-Link in two parens: ((<see href="http://fable.io/ts2fable/">ts2fable</see>))
/// JSDoc-Link in brackets: [<see href="http://fable.io/ts2fable/">ts2fable</see>]
/// JSDoc-Link in two brackets: [[<see href="http://fable.io/ts2fable/">ts2fable</see>]]
/// JSDoc-Link in braces: {<see href="http://fable.io/ts2fable/">ts2fable</see>}
/// JSDoc-Link in two braces: {{<see href="http://fable.io/ts2fable/">ts2fable</see>}}
/// </summary>
type [<AllowNullLiteral>] I1 =
    interface end
