export interface A { readonly alpha: number }
export interface B { readonly beta: string }
export interface C<T> { readonly value: T }

export function f1<T extends A>(): void
export function f2<T extends A & B>(): void

export interface I1 {
  f1<T extends A>(value: T): void
  f2<T extends A, U extends B>(value1: T, value2: U): void
}
