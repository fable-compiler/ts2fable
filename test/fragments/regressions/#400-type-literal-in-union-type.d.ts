/** simple Union without anything special */
export type Union = string | number
/** Type Alias -> interface, NOT anonymous record */
export type NoUnion = { v1: string }
/** Anonymous Record */
export type Union1 = string | { v1: string }
/** Anonymous Record */
export type Union2 = string | { v1: string, v2: number }
/** Anonymous Record */
export type Union3 = string | { v1: string, v2: number, v3: number}
/** Anonymous Record */
export type Union4 = string | { v1: string, v2: number, v3: number, v4: string }
/** Interface */
export type Union5 = string | { v1: string, v2: number, v3: number, v4: string, v5: string }

/** 2 Interfaces */
export type Union5_6 = { v1: string, v2: number, v3: number, v4: string, v5: string } | { v1: number, v2: number, v3: number, v4: number, v5: number, v6: string }

/** 3 Anonymous Records */
export type Union1_2_4 = {v1: string} | { v1: number, v2: string } | { v1: string, v2: number, v3: number, v4: string }

/** 2 Anonymous Records, 1 Interface */
export type Union1_5_3 = {v1: string} | { v1: string, v2: number, v3: number, v4: string, v5: string } | { v1: string, v2: number, v3: number}

/** Anonymous Records, v2 & v3 optional */
export type UnionOptional3 = string | { v1: string, v2?: number, v3?: number}
/** Interface, v2 & v3 optional */
export type UnionOptional5 = string | { v1: string, v2?: number, v3?: number, v4: string, v5: string }

/** Anonymous Records in Anonymous Record */
export type Union1Union1 = string | { v1: { nested1: string }}
/** Anonymous Record in Interface */
export type Union5Union1 = string | { v1: string, v2: number, v3: { nested1: string, nested2: number }, v4: string, v5: string }
/** Interface in Anonymous Record */
export type Union2Union5 = string | { v1: string, v2: { v1: string, v2: number, v3: number, v4: string, v5: string }}

/** Empty interface */
export type Union0 = string | {}

/** 
 * Source: [React](https://github.com/DefinitelyTyped/DefinitelyTyped/blob/master/types/react/index.d.ts#L87)
 * 
 * Similar to `fragments/react/f1` (but not generic) 
 */
export type UnionBivarianceHack = string | { bivarianceHack(instance: string | null): void }["bivarianceHack"]

/**
 * Anon Record (-> everything immutable) 
 * name: readonly, age: mutable 
 */
export type UnionReadonly2 = string | { readonly name: string, age: number }
/**
 * Interface
 * name: readonly, age: mutable 
 * (v3 mutable, v4 readonly, v5 mutable)
 */
export type UnionReadonly5 = string | { readonly name: string, age: number, v3: string, v4: string, v5: number }