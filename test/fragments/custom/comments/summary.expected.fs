// ts2fable 0.0.0
module rec summary

#nowarn "3390" // disable warnings for invalid XML comments

open System
open Fable.Core
open Fable.Core.JS


/// Single line main summary
type [<AllowNullLiteral>] A =
    interface end

/// Multi line
/// main summary
type [<AllowNullLiteral>] B =
    interface end

/// jsDoc single line summary tag
type [<AllowNullLiteral>] C =
    interface end

/// jsDoc multi line
/// summary tag
type [<AllowNullLiteral>] D =
    interface end

/// jsDoc single line description tag
type [<AllowNullLiteral>] E =
    interface end

/// jsDoc multi line
/// description tag
type [<AllowNullLiteral>] F =
    interface end

/// Main summary
/// 
/// jsDoc summary tag
/// 
/// jsDoc description tag
type [<AllowNullLiteral>] G =
    interface end

/// <summary>Single line main summary</summary>
/// <remarks>jsDoc remarks tag</remarks>
type [<AllowNullLiteral>] H =
    interface end

/// <summary>
/// Multi line 
/// main summary
/// </summary>
/// <remarks>jsDoc remarks tag</remarks>
type [<AllowNullLiteral>] J =
    interface end

/// <summary>
/// Main summary
/// 
/// jsDoc description tag
/// </summary>
/// <remarks>jsDoc remarks tag</remarks>
type [<AllowNullLiteral>] K =
    interface end

/// <summary>jsDoc description tag</summary>
/// <remarks>jsDoc remarks tag</remarks>
type [<AllowNullLiteral>] L =
    interface end
