// ts2fable 0.0.0
module rec ``#403-xml-comment-escape-chars``
open System
open Fable.Core
open Fable.Core.JS


/// <summary>
/// Chars to escape: &amp; &lt;
/// Chars that might be reasonable to escape, but not needed: > ' "
/// 
/// In code environment: &lt;c> &amp; &lt; > ' " &lt;/c>
/// 
/// In link: <see href="https://duckduckgo.com/?q=fsharp+ts2fable&amp;ia=web">search fsharp &amp; ts2fable</see>
/// </summary>
type [<AllowNullLiteral>] I =
    interface end

/// No escape here: just summary, no xml tags
/// 
/// Chars: & < > ' "
type [<AllowNullLiteral>] I2 =
    interface end

/// <summary>Escape chars here: some extra tag</summary>
/// <remarks>Remarks: Stuff &amp; Stuff: &amp; &lt; > ' "</remarks>
type [<AllowNullLiteral>] I3 =
    interface end

/// No escape in deprecated: gets extracted into obsolete attribute
/// 
/// With one Exception: double-quotation marks must be escaped!
[<Obsolete("Ok: &; not \"ok\"!")>]
type [<AllowNullLiteral>] I4 =
    interface end
