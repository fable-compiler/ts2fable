export interface A { readonly alpha: number }

/** This does something */
export type Delegate1 = (arg1: number, optArg1?: string, optArg2?: string) => void;

export type Delegate2<T extends A> = (arg1: T, arg2: number | string, ...paramArgs: number[]) => string;