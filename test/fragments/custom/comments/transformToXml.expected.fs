// ts2fable 0.0.0
module rec transformToXml

#nowarn "3390" // disable warnings for invalid XML comments

open System
open Fable.Core
open Fable.Core.JS


/// <summary>
/// Testing all text transformations (links &amp; code)
/// 
/// Simple link: <see href="https://github.com/fable-compiler/ts2fable" />
/// 
/// Markdown link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// 
/// JsDoc Link: <see href="https://github.com" /> and <see href="https://github.com">GitHub (previously separated by BAR)</see> and <see href="https://github.com">GitHub</see>
/// Note: TS parser currently doesn't support <c>@link</c> with <c>|</c> separator: <c>@{link target|description}</c> 
///       -&gt; Bar gets removed, link target becomes <c>targetdescription</c> and no description
///       -&gt; use space as separator again
/// 
/// Markdown inline code: <c>console.log("hello World")</c>
/// 
/// Multi line code without language:
/// <code>
/// let text = "Hello World"
/// printfn "%s" text
/// </code>
/// 
/// Multi line code with language:
/// <code lang="fsharp">
/// let text = "Hello fsharp"
/// printfn "%s" text
/// </code>
/// 
/// Multiple things in one line: 
/// <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see> and <c>code</c> and <see href="https://github.com">GitHub</see> ....
/// 
/// Href and Cref:
/// * href: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see> and <see href="https://github.com">GitHub</see> 
/// * cref: <see cref="B">Thingy B</see> and <see cref="B">Thingy B</see>
/// 
/// CRef with <c>#</c> (instance member) and <c>~</c> (inner member) -&gt; convert all into <c>.</c>:
/// <c>Namespace.Class.method</c> = <see cref="Namespace.Class.method" />
/// <c>Namespace.Class#method</c> = <see cref="Namespace.Class.method" />
/// <c>Namespace.Class~method</c> = <see cref="Namespace.Class">~method</see> (Note: currently not supported by TS -&gt; not handled by ts2fable)
/// remove leading:
/// <c>#method</c> = <see cref="method" />
/// <c>~method</c> = <see cref="method" />
/// 
/// Code in link:
/// <see href="https://fsharp.org">code <c>1+1</c> fsharp</see>
/// 
/// Link in code aren't transformed:
/// <c>[ts2fable](https://github.com/fable-compiler/ts2fable)</c> and <c>{@link https://github.com GitHub}</c>
/// and multiline:
/// <code>
/// [ts2fable](https://github.com/fable-compiler/ts2fable)`
/// `{@link https://github.com GitHub}` 
/// </code>
/// </summary>
type [<AllowNullLiteral>] AllTextTransformations =
    interface end

/// <summary>
/// Testing transformation in all tags
/// 
/// Root: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// 
/// Summary: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// 
/// Description: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// </summary>
/// <remarks>Remarks: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see></remarks>
/// <example>Example: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see></example>
/// <default>Default: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see></default>
/// <version>Version: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see></version>
/// <param name="p">Param: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see></param>
/// <returns>Returns: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see></returns>
/// <seealso cref="SomeType">See: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see></seealso>
/// <typeparam name="T">TypeParam: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see></typeparam>
/// <exception cref="Exception">Throws: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see></exception>
type [<AllowNullLiteral>] AllTags =
    interface end

/// <summary>Testing <c>@see</c> tag (-&gt; <c>&lt;seealso...</c>)</summary>
/// <seealso cref="SomeType1">should be cref</seealso>
/// <seealso cref="Module.SomeType2">should be cref</seealso>
/// <seealso href="https://github.com/fable-compiler/ts2fable">should be href</seealso>
/// <seealso cref="SomeType3">should be cref</seealso>
/// <seealso cref="SomeType4">should be cref</seealso>
/// <seealso cref="SomeType5">should be cref</seealso>
/// <seealso cref="SomeType6">should be cref</seealso>
/// <seealso cref="SomeType7">should be cref</seealso>
/// <seealso cref="SomeType8">should be cref</seealso>
/// <seealso cref="SomeType9">
/// should be
/// cref on
/// multiple lines
/// </seealso>
/// <seealso cref="SomeType10">
/// should be cref
/// on multiple lines
/// </seealso>
/// <seealso cref="SomeType11">
/// 
/// should be cref
/// on multiple lines
/// with leading empty line
/// </seealso>
type [<AllowNullLiteral>] SeeTag =
    interface end

/// <summary>
/// In practice (and sometimes in specs too): Separator between Name/Type and Description.
/// Should be removed during transformation.
/// </summary>
/// <param name="p1">some description</param>
/// <param name="p2">some description</param>
/// <param name="p3">some description</param>
/// <typeparam name="T1">some description</typeparam>
/// <typeparam name="T2">some description</typeparam>
/// <typeparam name="T3">some description</typeparam>
type [<AllowNullLiteral>] Separator =
    interface end

/// <summary>Exception Type in <c>@throws</c> is optional, but required in <c>&lt;exception&gt;</c></summary>
/// <exception cref="SomeException">exception with type</exception>
/// <exception cref="Module.SomeException">exception with type in module</exception>
/// <exception cref="">exception without type</exception>
type [<AllowNullLiteral>] Throws =
    interface end

/// <summary>
/// Tags not handled by ts2fable
/// -&gt; No tags in F#, and no exceptions in ts2fable
/// </summary>
type [<AllowNullLiteral>] Unknown =
    interface end
