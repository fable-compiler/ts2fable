type W<T> = { readonly value: T }

/**
 * https://www.typescriptlang.org/docs/handbook/utility-types.html#nonnullabletype
 */
export namespace NsNonNullable {
  /**
   * string | number
   */
  type T0 = NonNullable<string | number | undefined>;
  /**
   * string[]
   */
  type T1 = NonNullable<string[] | null | undefined>;

  /**
   * W<T>
   */
  type T2<T> = W<NonNullable<T>>

  /**
   * W<T> -> W<T>
   */
  function f0<T>(v: W<NonNullable<T>>): W<NonNullable<T>>
  /**
   * W<T> -> W<T>
   */
  function f1<T>(v: NonNullable<W<T>>): NonNullable<W<T>>

  interface I0 {
    /**
     * W<T> -> W<T>
     */
    <T>(v: W<NonNullable<T>>): W<NonNullable<T>>
  }
}