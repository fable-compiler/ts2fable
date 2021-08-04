// ts2fable 0.0.0
module rec ``#420-xml-comment-generic-type-param-in-param``

#nowarn "3390" // disable warnings for invalid XML comments

open System
open Fable.Core
open Fable.Core.JS


/// <typeparam name="T">Some Type</typeparam>
type [<AllowNullLiteral>] GT1<'T> =
    interface end

/// <summary>Some Description</summary>
/// <typeparam name="T">Some Type</typeparam>
type [<AllowNullLiteral>] A1<'T> =
    interface end

/// <summary>Some Description</summary>
/// <typeparam name="T">Some Type</typeparam>
type [<AllowNullLiteral>] A2<'T> =
    interface end

/// <summary>Some Description</summary>
/// <typeparam name="T">Some Type</typeparam>
type [<AllowNullLiteral>] A3<'T> =
    interface end

/// <summary>Some Description</summary>
/// <typeparam name="T">Some Type</typeparam>
type [<AllowNullLiteral>] A4<'T> =
    interface end

/// <summary>Some Description</summary>
/// <typeparam name="T">Some Type</typeparam>
type [<AllowNullLiteral>] A5<'T> =
    interface end

/// <summary>Some Description</summary>
/// <typeparam name="T1">1st Type</typeparam>
/// <typeparam name="T2">2nd Type</typeparam>
/// <typeparam name="T3">3rd Type</typeparam>
/// <typeparam name="T4">4th Type</typeparam>
/// <typeparam name="T5">5th Type</typeparam>
/// <typeparam name="T6">6th Type</typeparam>
/// <typeparam name="T7">7th Type</typeparam>
/// <typeparam name="T8">8th Type</typeparam>
/// <typeparam name="T9">9th Type</typeparam>
type [<AllowNullLiteral>] A6<'T1, 'T2, 'T3, 'T4, 'T5, 'T6, 'T7, 'T8, 'T9> =
    interface end

type [<AllowNullLiteral>] I =
    /// <summary>Description</summary>
    /// <param name="value">Some parameter</param>
    /// <typeparam name="T">Some Type</typeparam>
    abstract f1: value: 'T -> unit
    /// <summary>
    /// Description
    /// 
    /// Probably best to refer to parameter <c>T</c>, not type parameter <c>T</c>
    /// </summary>
    /// <param name="T">Some parameter</param>
    abstract f2: T: 'T -> unit
    /// <summary>
    /// Description
    /// 
    /// First <c>T</c> is parameter, 2nd is type parameter
    /// </summary>
    /// <param name="T">Some parameter</param>
    /// <typeparam name="T">Some Type</typeparam>
    abstract f3: T: 'T -> unit
    /// <summary>
    /// Description
    /// 
    /// <c>T</c> is param name, NOT generic type param
    /// </summary>
    /// <param name="T">Some parameter</param>
    abstract p1: T: string -> unit
