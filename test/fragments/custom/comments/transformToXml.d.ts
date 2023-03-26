/**
 * Testing all text transformations (links & code)
 * 
 * Simple link: https://github.com/fable-compiler/ts2fable
 * 
 * Markdown link: [ts2fable](https://github.com/fable-compiler/ts2fable)
 * 
 * JsDoc Link: {@link https://github.com} and {@link https://github.com GitHub (previously separated by BAR)} and {@link https://github.com GitHub}
 * Note: TS parser currently doesn't support `@link` with `|` separator: `@{link target|description}` 
 *       -> Bar gets removed, link target becomes `targetdescription` and no description
 *       -> use space as separator again
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
 * [ts2fable](https://github.com/fable-compiler/ts2fable) and `code` and {@link https://github.com GitHub} ....
 * 
 * Href and Cref:
 * * href: [ts2fable](https://github.com/fable-compiler/ts2fable) and {@link https://github.com GitHub} 
 * * cref: [Thingy B](B) and {@link B Thingy B}
 * 
 * CRef with `#` (instance member) and `~` (inner member) -> convert all into `.`:
 * `Namespace.Class.method` = {@link Namespace.Class.method}
 * `Namespace.Class#method` = {@link Namespace.Class#method}
 * `Namespace.Class~method` = {@link Namespace.Class~method} (Note: currently not supported by TS -> not handled by ts2fable)
 * remove leading:
 * `#method` = {@link method}
 * `~method` = {@link method}
 * 
 * Code in link:
 * [code `1+1` fsharp](https://fsharp.org)
 * 
 * Link in code aren't transformed:
 * `[ts2fable](https://github.com/fable-compiler/ts2fable)` and `{@link https://github.com GitHub}`
 * and multiline:
 * ```
 * [ts2fable](https://github.com/fable-compiler/ts2fable)`
 * `{@link https://github.com GitHub}` 
 * ```
 */
export interface AllTextTransformations {}

/**
 * Testing transformation in all tags
 * 
 * Root: [ts2fable](https://github.com/fable-compiler/ts2fable)
 * 
 * @summary Summary: [ts2fable](https://github.com/fable-compiler/ts2fable)
 * @description Description: [ts2fable](https://github.com/fable-compiler/ts2fable)
 * @remarks Remarks: [ts2fable](https://github.com/fable-compiler/ts2fable)
 * @example Example: [ts2fable](https://github.com/fable-compiler/ts2fable)
 * @default Default: [ts2fable](https://github.com/fable-compiler/ts2fable)
 * @version Version: [ts2fable](https://github.com/fable-compiler/ts2fable)
 * @param p Param: [ts2fable](https://github.com/fable-compiler/ts2fable)
 * @returns Returns: [ts2fable](https://github.com/fable-compiler/ts2fable)
 * @see SomeType See: [ts2fable](https://github.com/fable-compiler/ts2fable)
 * @typeparam T TypeParam: [ts2fable](https://github.com/fable-compiler/ts2fable)
 * @throws {Exception} Throws: [ts2fable](https://github.com/fable-compiler/ts2fable)
 */
export interface AllTags {}

/**
 * Testing `@see` tag (-> `<seealso...`)
 * 
 * @see SomeType1 should be cref
 * @see Module.SomeType2 should be cref
 * @see https://github.com/fable-compiler/ts2fable should be href
 * @see {@link SomeType3} should be cref
 * @see {@link SomeType4 should be cref}
 * @see {@link SomeType5 should be} cref
 * @see {@link SomeType6 should be} cref
 * @see [should be cref](SomeType7)
 * @see [should be](SomeType8) cref
 * @see SomeType9 should be
 *      cref on
 *      multiple lines
 * @see SomeType10
 *      should be cref
 *      on multiple lines
 * @see SomeType11
 *
 *      should be cref
 *      on multiple lines
 *      with leading empty line
 */
export interface SeeTag {}

/**
 * In practice (and sometimes in specs too): Separator between Name/Type and Description.
 * Should be removed during transformation.
 * 
 * @param p1 some description
 * @param p2: some description
 * @param p3 - some description
 * 
 * @typeparam T1 some description
 * @typeparam T2: some description
 * @typeparam T3 - some description
 */
export interface Separator {}

/**
 * Exception Type in `@throws` is optional, but required in `<exception>`
 * 
 * @throws {SomeException} exception with type
 * @throws {Module.SomeException} exception with type in module
 * @throws exception without type
 */
export interface Throws {}

/**
 * Tags not handled by ts2fable
 * -> No tags in F#, and no exceptions in ts2fable
 * 
 * @author Ian Awesome <i.am.awesome@example.com>
 * @extends {Set<T>}
 * @something foo bar baz
 */
export interface Unknown {}
