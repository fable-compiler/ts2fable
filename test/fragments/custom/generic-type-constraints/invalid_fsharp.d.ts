// all examples here are valid TypeScript
// and can be converted by ts2fable
// but produce invalid F#

export module UnusedTypeParameter {
  interface A { }

  type T1<T extends A> = {}
  type T2<T> = T1<T>
  //           ^^^^^
  //           A type parameter is missing a constraint 'when 'T :> A'

  // Note: Only valid in TS because `A` is empty
  //       `interface A { alpha: number }`
  //       -> TS compiler complains about missing constraint
}

export module ExtendOtherTypeParameter {
  interface A { alpha: number }
  
  interface T1<T extends A, U extends T> {}
  //                        ^
  //                        This type parameter has been used in a way that constrains it to always be '#A'
}

export module StructuralSubtyping {
  // `A` and `B` have same shape -> are equal
  // But are different in F#

  interface A {}
  interface B {}

  interface T1<T extends A> {}
  interface T2<T extends T1<B>> {}
  //                     ^^^^^
  //                     The type 'B' is not compatible with the type 'A'
}

export module StructuralSubtypingWithOptionalField {
  // Special case of the example above:
  // `A` and `B` are different, but can have the same shape:
  // All fields in `A` are optional -> no fields specified -> `A` is `{}`

  interface A {
    value?: number
  }
  interface B {}

  interface T1<T extends A> {}
  interface T2<T extends T1<B>> {}
  //                     ^^^^^
  //                     The type 'B' is not compatible with the type 'A'
}

export module MakeGenericMoreSpecific {
  interface A { alpha: number }
  interface B { beta: string }
  interface C<T> { value: T }

  interface T1<T extends C<any>> {}
  interface S1<T extends T1<C<T>>> {}
  //                     ^^^^^^^^
  //                     The type 'obj option' does not match the type ''T'

  interface T2<T extends C<A | B>> {}
  interface S2<T extends T2<C<A>>> {}
  //                     ^^^^^^^^
  //                     The type 'U2<A,B>' does not match the type 'A'
}

export module ReduceToInvalidGenericItem {
  // Type might be valid inside a generic, but invalid when extracted
  // like sealed types (`Function`->`Action`) or Tuples (`A | B`):
  // both are ok inside a generic:
  //   * `T extends C<Function>` -> `'T when 'T :> C<Function>`
  //   * `T extends C<A|B>` -> `T when 'T :> C<U2<A,B>>`
  // but are removed as direct constraints: 
  //   * `T extends Function` -> `'T`
  //   * `T extends A | B` -> `'T`

  interface A { alpha: number }
  interface B { beta: string }
  interface C<T> { value: T }

  interface T1<T extends C<Function>> {}
  type S1<T extends Function> = T1<C<T>>
  //                            ^^^^^^^^
  //                            The type 'Function' does not match the type ''T'

  interface T2<T extends C<A | B>> {}
  type S2<T extends A | B> = T2<C<T>>
  //                         ^^^^^^^^
  //                         The type 'U2<A,B>' does not match the type ''T'
}

export module TypeAndExtendedTypeForSameParameter {
  interface A { alpha: number }
  interface Aext extends A {}
  interface C<T> { value: T }
  
  interface T1<T extends A, K extends C<T>> {}
  interface S1 {
    v: T1<Aext, C<A>>
    // ^^^^^^^^^^^^^^
    // The type 'Aext' does not match the type 'A'
  }
}

export module TypeParamterDefault {
  interface C<T> { value: T }

  interface T1<T extends C<any> = C<string>> {} 
  // generates 
  // ```fsharp
  // type [<AllowNullLiteral>] T1<'T when 'T :> C<obj option>> = interface end
  // type T1 = T1<C<string>>
  //           ^^^^^^^^^^^^^
  //           The type 'obj option' does not match the type 'string'
  // ```
}