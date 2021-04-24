//todo: root variable (-> `let`)
//todo: interface
    //todo: optional field (`value?: ...`)
//todo: class:
    //todo: static

export interface A { readonly name: string }
export interface B { readonly id: number }
export namespace N {
    /** get */
    const v1: string;

    /** get */
    const f1: (value: string) => number;

    /** get */
    const f2: <T>(value: T) => number;
    /** get */
    const f3: <T>(value: string) => T;
    /** get */
    const f4: <T>(value: T) => T;

    /** get, set */
    let f5: <T>(value: T) => T;
    /** get, set */
    var f6: <T>(value: T) => T;

    /** get */
    const f7: <T>(v1: T, v2: T) => T;
    /** get */
    const f8: <T>(v1: T | string, v2: string | number | T) => T;

    /** get */
    const f9: <T>(f1: ((v1: T, v2: T) => T), f2: ((v1: string | T, v2: T | number) => string | T)) => T | string
    /** get, set */
    let f10: <T>(f1: ((v1: T, v2: T) => T), f2: ((v1: string | T, v2: T | number) => string | T)) => T | string

    /** get */
    const f11: <T>(v1: T, v2: T) => T;
    /** get, set */
    let f12: <T>(v1: T, v2: T) => T;

    /** 
     * get 
     * 
     * T extends A
     */
    const gf1: <T extends A>(value: T) => T
    /** 
     * get, set
     * 
     * T extends A
     */
    let gf2: <T extends A>(value: T) => T
    /** 
     * get 
     * 
     * T extends A
     */
    const gf3: <T extends A>(v1: T, v2: T) => T
    /** 
     * get, set
     * 
     * T extends A
     */
    let gf4: <T extends A>(v1: T, v2: T) => T
    /**
     * get
     * 
     * T extends A
     * U extends A & B
     */
    const gf5: <T extends A, U extends A & B>(v1: T, v2: U) => U
    /**
     * get, set
     * 
     * T extends A
     * U extends A & B
     */
    let gf6: <T extends A, U extends A & B>(v1: T, v2: U) => U

    /** get */
    const arrayify: <T>(obj: T | T[]) => T[];
    
    /** get */
    const izi1: (v1: string, v2: string) => void;
    /** get, set */
    let izi3: (v1: string, v2: string) => void;
    /** get */
    const izi4: <T>(v1: T, v2: T) => T;
    /** get, set */
    let izi5: <T>(v1: T, v2: T) => T;

    /** get */
    const t1: [string, string];
    /** get */
    const ft1: <T>(vs: [T, T]) => T;
    /** get, set */
    let ft2: <T>(vs: [T, T]) => T;

    /** 
     * get
     * 
     * v2 optional
     */
    const o1: <T>(v1: T, v2?: T) => T;
    /** 
     * get, set
     * 
     * v2 optional
     */
    let o2: <T>(v1: T, v2?: T) => T;
    /** 
     * get
     * 
     * v2 optional
     */
    const o3: <T>(v1: T, v2?: [T, T]) => T;
    /** 
     * get, set
     * 
     * v2 optional
     */
    let o4: <T>(v1: T, v2?: [T, T]) => T;

    /** function */
    function ff1<T>(f: ((value: T) => T)): ((value: T) => T);
    /** function */
    function ff2<T>(f: ((value: T) => T), v: T): string;
    /** function */
    function ff3<T>(v1: T, v2: T | string): T | number
}
