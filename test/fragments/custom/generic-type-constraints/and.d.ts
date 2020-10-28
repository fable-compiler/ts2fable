export interface A { readonly alpha: number }
export interface B { readonly beta: string }
export interface C<T> { readonly value: T }

export interface And1<T extends A & B> {}
export interface And2<T extends A & B & C<A> & C<B>> {}
export interface And3<T extends A, U extends B & C<T>> {}
