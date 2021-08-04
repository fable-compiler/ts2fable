/**
 * @typeparam T - Some Type
 */
export interface GT1<T> {}
/**
 * Some Description
 * 
 * @param <T> - Some Type
 */
export interface A1<T> {}
/**
 * Some Description
 * 
 * @param <T> Some Type
 */
export interface A2<T> {}
/**
 * Some Description
 * 
 * @param T Some Type
 */
export interface A3<T> {}
/**
 * Some Description
 * 
 * @param T - Some Type
 */
export interface A4<T> {}
/**
 * Some Description
 * 
 * @param T: Some Type
 */
export interface A5<T> {}

/**
 * Some Description
 * 
 * @typeparam T1 1st Type
 * @param T2 2nd Type
 * @param T3 - 3rd Type
 * @param T4: 4th Type
 * @param <T5> 5th Type
 * @param <T6> - 6th Type
 * @param <T7>: 7th Type
 * @typeparam T8 - 8th Type
 * @typeparam T9: 9th Type
 */
export interface A6<T1, T2, T3, T4, T5, T6, T7, T8, T9> {}

export interface I {
  /**
   * Description
   * 
   * @param value Some parameter
   * @param T Some Type
   */
  f1<T>(value: T): void;
  /**
   * Description
   * 
   * Probably best to refer to parameter `T`, not type parameter `T`
   * 
   * @param T Some parameter
   */
  f2<T>(T: T): void;
  /**
   * Description
   * 
   * First `T` is parameter, 2nd is type parameter
   * 
   * @param T Some parameter
   * @param <T> Some Type
   */
  f3<T>(T: T): void;

  /**
   * Description
   * 
   * `T` is param name, NOT generic type param
   * 
   * @param T Some parameter
   */
  p1(T: string): void;
}
