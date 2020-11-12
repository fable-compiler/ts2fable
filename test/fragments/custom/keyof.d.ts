export function f1(k: keyof number): keyof (number [])
export function f2<K extends keyof number>(k: K): K
export function f3<T, K extends keyof T>(k: K): K
export function f4(): [keyof number, keyof string]
export function f5<T, K extends keyof T>(v: [T, K]): [T, K]

export type T1<K extends keyof number> = {}
export type T2<T, K extends keyof T> = {}
export interface I1<K extends keyof number> {}
export interface I2<T, K extends keyof T> {}
export class C1<K extends keyof number> {}
export class C2<T, K extends keyof T> {}

export interface I3<T, K extends keyof T> {
  p1: K

  f1(t: T, k: K): [T, K]
  f2(t: T, k: keyof T): [T, keyof T]
  f3<L extends keyof T>(l: L): L
  f4<L extends keyof number>(l: L): L
}

export declare type T3<T, K extends keyof T> = {
  p1: K

  f1(t: T, k: K): [T, K]
  f2(t: T, k: keyof T): [T, keyof T]
  f3<L extends keyof T>(l: L): L
  f4<L extends keyof number>(l: L): L
}

export declare class C3<T, K extends keyof T> {

  constructor(k: K);

  p1: K

  f1(t: T, k: K): [T, K]
  f2(t: T, k: keyof T): [T, keyof T]
  f3<L extends keyof T>(l: L): L
  f4<L extends keyof number>(l: L): L
}