export interface A { readonly alpha: number }
export interface B { readonly beta: string }
export interface C<T> { readonly value: T }

// `'T :> A`
export interface Comb1<T extends A & (B | C<B>)> {}
// ``
export interface Comb2<T extends A | (B & C<B>)> {}
// ``
export interface Comb3<T extends (A | C<A>) & (B | C<B>)> {}
