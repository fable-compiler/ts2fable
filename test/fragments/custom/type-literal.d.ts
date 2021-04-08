/** Type Alias -> interface, NOT anonymous record */
export type NoUnion = { v1: string }
/** Anonymous Record */
export type Union1 = string | { v1: string }
/** less than 5 members -> Anonymous Record */
export type Union4 = string | { v1: string, v2: number, v3: number, v4: string }
/** more than 4 members -> Interface */
export type Union5 = string | { v1: string, v2: number, v3: number, v4: string, v5: string }

/** 
 * Source: (React)[https://github.com/DefinitelyTyped/DefinitelyTyped/blob/master/types/react/index.d.ts#L87]
 * 
 * Similar to `fragments/react/f1` (but not generic) 
 */
export type UnionBivarianceHack = string | { bivarianceHack(instance: string | null): void }["bivarianceHack"]
/**
 * Source: (React)[https://github.com/DefinitelyTyped/DefinitelyTyped/blob/master/types/react/index.d.ts#L87]
 */
export type RefCallback<T> = { bivarianceHack(instance: T | null): void }["bivarianceHack"]; 


/** neither interface nor anon record */
export function f0_0( v: string ): number
/** Input: Anon record; Return: Anon record */
export function f1_1( v: { v1: string } ): { v1: number }
/** Input: Anon record; Return: Anon record */
export function f2_2( v: { v1: string, v2: number } ): { v1: number, v2: string }
/** Input: Anon record; Return: Anon record */
export function f3_3( v: { v1: string, v2: number, v3: string } ): { v1: number, v2: string, v3: string }
/** Input: Anon record; Return: Anon record */
export function f4_4( v: { v1: string, v2: number, v3: string, v4: string } ): { v1: number, v2: string, v3: string, v4: number }
/** Input: interface; Return: interface */
export function f5_5( v: { v1: string, v2: number, v3: string, v4: string, v5: string } ): { v1: number, v2: string, v3: string, v4: number, v5: string }
/** Input: anon record, interface; Return: anon record */
export function f2_5_3( v1: { v1: string, v2: number }, v2: { v1: number, v2: string, v3: string, v4: number, v5: string } ): { v1: number, v2: string, v3: string }


/** Input: anon record; Return: anon record */
export function fOptional2_Optional3( v1: { name: string, birthday?: number } ): { name: string, age?: number, id: number }

/** Input: anon record; Return: anon record */
export function ff2_2( v1: { value: string, f: ( value: string ) => string } ): { result: string, f: ( value: string, id: number ) => string }
/** Input: interface; Return: anon interface */
export function ff2_2( v1: { value: string, f: ( value: string ) => string, v2: number, v3: number, v4: string, v5: string } ): { result: string, f: ( value: string, id: number ) => string, v2: number, v3: number, v4: string, v5: string }

/** Anon record */
export const c1: { v1: string }
/** Anon record */
export const c4: { v1: string, v2: number, v3: number, v4: string }
/** interface */
export const c5: { v1: string, v2: number, v3: number, v4: string, v5: string }

/** Anon record */
export let l1: { v1: string }
/** Anon record */
export let l4: { v1: string, v2: number, v3: number, v4: string }
/** interface */
export let l5: { v1: string, v2: number, v3: number, v4: string, v5: string }

/** Nested Anon records */
export const cn1: {
  v1: string,
  v2: {
    v2_1: string,
    v2_2: {
      v2_2_1: string,
      v2_2_2: number,
      v2_2_3: string,
    },
    v2_3: {
      v2_3_1: string,
      v2_3_2: string,
    },
    v2_4: string,
  },
  v3: number,
}
/** Nested Anon records & Interfaces */
export const cn2: {
  v1: string,
  v2: {   // anon record
    v2_1: string,
    v2_2: { // interface
      v2_2_1: string,
      v2_2_2: number,
      v2_2_3: string,
      v2_2_4: string,
      v2_2_5: number,
    },
    v2_3: { // anon record
      v2_3_1: string,
      v2_3_2: string
    },
    v2_4: string
  },
  v3: { // interface
    v3_1: string,
    v3_2: number,
    v3_3: { // anon record
      v3_3_1: string,
      v3_3_2: { // interface
        v3_3_2_1: string,
        v3_3_2_2: number,
        v3_3_2_3: string,
        v3_3_2_4: number,
        v3_3_2_5: string,
        v3_3_2_6: string,
      },
      v3_3_3: string,
    },
    v3_4: string,
    v3_5: number,
  }
}

export interface I1 {
  /** Anon Record */
  l1: { v1: string }
  /** Interface */
  l5: { v1: string, v2: number, v3: number, v4: string, v5: string }
  /** Anon Record */
  readonly c1: { v1: string }
  /** Interface */
  readonly c5: { v1: string, v2: number, v3: number, v4: string, v5: string }
  /** Anon Record */
  f1( v1: { v1: string } ): { v1: string }
  /** Interface */
  f5( v1: { v1: string, v2: number, v3: number, v4: string, v5: string }): { v1: string, v2: number, v3: number, v4: string, v5: string }
}

export type T1 = {
  /** Anon Record */
  l1: { v1: string }
  /** Interface */
  l5: { v1: string, v2: number, v3: number, v4: string, v5: string }
  /** Anon Record */
  readonly c1: { v1: string }
  /** Interface */
  readonly c5: { v1: string, v2: number, v3: number, v4: string, v5: string }
  /** Anon Record */
  f1( v1: { v1: string } ): { v1: string }
  /** Interface */
  f5( v1: { v1: string, v2: number, v3: number, v4: string, v5: string }): { v1: string, v2: number, v3: number, v4: string, v5: string }
}

export namespace N1 {
  /** Anon Record */
  let l1: { v1: string }
  /** Interface */
  let l5: { v1: string, v2: number, v3: number, v4: string, v5: string }
  /** Anon Record */
  const c1: { v1: string }
  /** Interface */
  const c5: { v1: string, v2: number, v3: number, v4: string, v5: string }
  /** Anon Record */
  function f1( v1: { v1: string } ): { v1: string }
  /** Interface */
  function f5( v1: { v1: string, v2: number, v3: number, v4: string, v5: string }): { v1: string, v2: number, v3: number, v4: string, v5: string }
}

export class C1 {
  /** Anon Record */
  l1: { v1: string }
  /** Interface */
  l5: { v1: string, v2: number, v3: number, v4: string, v5: string }
  /** Anon Record */
  readonly c1: { v1: string }
  /** Interface */
  readonly c5: { v1: string, v2: number, v3: number, v4: string, v5: string }
  /** Anon Record */
  f1( v1: { v1: string } ): { v1: string }
  /** Interface */
  f5( v1: { v1: string, v2: number, v3: number, v4: string, v5: string }): { v1: string, v2: number, v3: number, v4: string, v5: string }
}
