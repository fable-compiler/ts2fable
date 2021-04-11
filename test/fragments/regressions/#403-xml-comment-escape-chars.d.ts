/**
 * Chars to escape: & <
 * Chars that might be reasonable to escape, but not needed: > ' "
 * 
 * In code environment: ` & < > ' " `
 * 
 * In link: [search fsharp & ts2fable](https://duckduckgo.com/?q=fsharp+ts2fable&ia=web)
 */
export interface I { }
/**
 * No escape here: just summary, no xml tags
 * 
 * Chars: & < > ' "
 */
export interface I2 { }
/**
 * Escape chars here: some extra tag
 * 
 * @remarks Remarks: Stuff & Stuff: & < > ' "
 */
export interface I3 { }
/**
 * No escape in deprecated: gets extracted into obsolete attribute
 * 
 * With one Exception: double-quotation marks must be escaped!
 * 
 * @deprecated Ok: &; not "ok"!
 */
export interface I4 { }

/**
 * A & and link: [link](target)
 */
export interface I5 { }

/**
 * `And` in code: `&`
 */
export interface I6 { }