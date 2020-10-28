export interface A { readonly alpha: number }
export interface B { readonly beta: string }
export interface C<T> { readonly value: T }

// remove sealed types from constraints
export interface Sealed1<T extends Function> {}
export interface Sealed2<T extends ReadonlySet<A>> {}
export interface Sealed3<T extends ReadonlyMap<A, B>> {}

// allowed inside generics
export interface Sealed4<T extends C<Function>> {}

// remove when used in `&`
// ``
export interface Sealed5<T extends Function & ReadonlySet<A>> {}
// `'T :> A`
export interface Sealed6<T extends Function & A> {}
// `'T :> A`
export interface Sealed7<T extends A & Function> {}
// `'T :> A and 'T :> B`
export interface Sealed8<T extends A & Function & B> {}

