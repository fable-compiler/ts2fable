export interface A { readonly alpha: number }
export interface B { readonly beta: string }
interface AB extends A, B {}
export interface C<T> { readonly value: T }

export interface D1<T extends A & B = AB> {}
// doesn't work in F#: `= A & B` -> `obj`
//  -> "The type 'obj' is not compatible with the type 'A'"
// export interface D1no<T extends A & B = A & B> {}

export interface D2<T extends C<A> = C<A>> {}
// doesn't work in F#: `C<AB>`
// -> The type 'A' does not match the type 'AB'
// export interface D2no<T extends C<A> = C<AB>> {}

export interface D3<T extends {} = {}> {}