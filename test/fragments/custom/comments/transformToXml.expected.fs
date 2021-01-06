// ts2fable 0.0.0
module rec transformToXml
open System
open Fable.Core
open Fable.Core.JS


/// <summary>
/// Simple link: <see href="https://github.com/fable-compiler/ts2fable" />
/// 
/// Markdown link: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>
/// 
/// JsDoc Link: <see href="https://github.com" /> and <see href="https://github.com">GitHub</see> and <see href="https://github.com">GitHub</see>
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
/// <code language="fsharp">
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
/// Code in link:
/// <see href="https://fsharp.org">code <c>1+1</c> fsharp</see>
/// 
/// Link in code aren't transformed:
/// <c>[ts2fable](https://github.com/fable-compiler/ts2fable)</c> and <c>{@link https://github.com|GitHub}</c>
/// and multiline:
/// <code>
/// [ts2fable](https://github.com/fable-compiler/ts2fable)`
/// `{@link https://github.com|GitHub}` 
/// </code>
/// </summary>
type [<AllowNullLiteral>] A =
    interface end

/// <summary>
/// Testing transformation in all tags
/// 
/// Root: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>`
/// 
/// Summary: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>`
/// 
/// Description: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>`
/// </summary>
/// <remarks>Remarks: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>`</remarks>
/// <example>Example: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>`</example>
/// <default>Default: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>`</default>
/// <version>Version: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>`</version>
/// <param name="p">Param: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>`</param>
/// <returns>Returns: <see href="https://github.com/fable-compiler/ts2fable">ts2fable</see>`</returns>
type [<AllowNullLiteral>] B =
    interface end
