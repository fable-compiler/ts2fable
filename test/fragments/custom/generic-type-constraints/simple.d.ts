// interface WITH fields:
//   type checking is based on shape of interface or type ("duck typing" or "structural subtyping")
//   -> `interface A {}` and `interface B {}` are equal
//   that would allow things like:
//   ```
//   interface T1<T extends A> {}
//   interface T2<T extends T1<B>> {}
//   ```
//   -> correct in typescript, but not in F#
export interface A { readonly alpha: number }
export interface B { readonly beta: string }
export interface C<T> { readonly value: T }

export interface Simple1<T extends A> {}
export interface Simple2<T extends C<A>> {}
export interface Simple3<T extends A, U extends B> {}
export interface Simple4<T extends A, U extends C<T>> {}
