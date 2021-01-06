/**
 * Simple link: https://github.com/fable-compiler/ts2fable
 * 
 * Markdown link: [ts2fable](https://github.com/fable-compiler/ts2fable)
 * 
 * JsDoc Link: {@link https://github.com} and {@link https://github.com|GitHub} and {@link https://github.com GitHub}
 * 
 * Markdown inline code: `console.log("hello World")`
 * 
 * Multi line code without language:
 * ```
 * let text = "Hello World"
 * printfn "%s" text
 * ```
 * 
 * Multi line code with language:
 * ```fsharp
 * let text = "Hello fsharp"
 * printfn "%s" text
 * ```
 * 
 * Multiple things in one line: 
 * [ts2fable](https://github.com/fable-compiler/ts2fable) and `code` and {@link https://github.com|GitHub} ....
 * 
 * Href and Cref:
 * * href: [ts2fable](https://github.com/fable-compiler/ts2fable) and {@link https://github.com|GitHub} 
 * * cref: [Thingy B](B) and {@link B|Thingy B}
 * 
 * Code in link:
 * [code `1+1` fsharp](https://fsharp.org)
 * 
 * Link in code aren't transformed:
 * `[ts2fable](https://github.com/fable-compiler/ts2fable)` and `{@link https://github.com|GitHub}`
 * and multiline:
 * ```
 * [ts2fable](https://github.com/fable-compiler/ts2fable)`
 * `{@link https://github.com|GitHub}` 
 * ```
 */
export interface A {}

//todo: add other tags -- they aren't fully transformed yet (like `typeparam` -> leading name isn't extracted yet)
/**
 * Testing transformation in all tags
 * 
 * Root: [ts2fable](https://github.com/fable-compiler/ts2fable)` 
 * 
 * @summary Summary: [ts2fable](https://github.com/fable-compiler/ts2fable)` 
 * @description Description: [ts2fable](https://github.com/fable-compiler/ts2fable)` 
 * @remarks Remarks: [ts2fable](https://github.com/fable-compiler/ts2fable)` 
 * @example Example: [ts2fable](https://github.com/fable-compiler/ts2fable)` 
 * @default Default: [ts2fable](https://github.com/fable-compiler/ts2fable)` 
 * @version Version: [ts2fable](https://github.com/fable-compiler/ts2fable)` 
 * @param p Param: [ts2fable](https://github.com/fable-compiler/ts2fable)` 
 * @returns Returns: [ts2fable](https://github.com/fable-compiler/ts2fable)` 
 */
export interface B {}