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
  //                     ^^^^^^
  //                     The type 'B' is not compatible with the type 'A'
}
