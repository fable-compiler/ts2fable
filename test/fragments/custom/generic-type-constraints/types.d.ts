export interface A { readonly alpha: number }
export interface B { readonly beta: string }
export interface C<T> { readonly value: T }

export interface T1<T extends A> {}
export type T2<T extends A> = {}
export class T3<T extends A> {}
