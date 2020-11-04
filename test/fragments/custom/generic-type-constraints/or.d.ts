export interface A { readonly alpha: number }
export interface B { readonly beta: string }
export interface C<T> { readonly value: T }

// ignore -> just `Or1<'T>`
export interface Or1<T extends A | B> {}

// `A | B` is allowed inside generic
// -> `C<U2<A,B>>`
export interface Or2<T extends C<A | B>> {}