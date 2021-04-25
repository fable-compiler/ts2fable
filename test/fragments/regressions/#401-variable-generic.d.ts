//todo: root variable (-> `let`)

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

    /** get */
    const f13: (v1: string, v2: string) => string;
    /** get, set */
    let f14: (v1: string, v2: string) => string;

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

export interface I {
    /** get */
    readonly v1: string;

    /** get */
    readonly f1: (value: string) => number;

    /** get */
    readonly f2: <T>(value: T) => number;
    /** get */
    readonly f3: <T>(value: string) => T;
    /** get */
    readonly f4: <T>(value: T) => T;

    /** get, set */
    f5: <T>(value: T) => T;
    /** get, set */
    f6: <T>(value: T) => T;

    /** get */
    readonly f7: <T>(v1: T, v2: T) => T;
    /** get */
    readonly f8: <T>(v1: T | string, v2: string | number | T) => T;

    /** get */
    readonly f9: <T>(f1: ((v1: T, v2: T) => T), f2: ((v1: string | T, v2: T | number) => string | T)) => T | string
    /** get, set */
    f10: <T>(f1: ((v1: T, v2: T) => T), f2: ((v1: string | T, v2: T | number) => string | T)) => T | string

    /** get */
    readonly f11: <T>(v1: T, v2: T) => T;
    /** get, set */
    f12: <T>(v1: T, v2: T) => T;

    /** get */
    readonly f13: (v1: string, v2: string) => string;
    /** get, set */
    f14: (v1: string, v2: string) => string;


    /** 
     * get 
     * 
     * T extends A
     */
    readonly gf1: <T extends A>(value: T) => T
    /** 
     * get, set
     * 
     * T extends A
     */
    gf2: <T extends A>(value: T) => T
    /** 
     * get 
     * 
     * T extends A
     */
    readonly gf3: <T extends A>(v1: T, v2: T) => T
    /** 
     * get, set
     * 
     * T extends A
     */
    gf4: <T extends A>(v1: T, v2: T) => T
    /**
     * get
     * 
     * T extends A
     * U extends A & B
     */
    readonly gf5: <T extends A, U extends A & B>(v1: T, v2: U) => U
    /**
     * get, set
     * 
     * T extends A
     * U extends A & B
     */
    gf6: <T extends A, U extends A & B>(v1: T, v2: U) => U

    /** get */
    readonly arrayify: <T>(obj: T | T[]) => T[];
    
    /** get */
    readonly izi1: (v1: string, v2: string) => void;
    /** get, set */
    izi3: (v1: string, v2: string) => void;
    /** get */
    readonly izi4: <T>(v1: T, v2: T) => T;
    /** get, set */
    izi5: <T>(v1: T, v2: T) => T;

    /** get */
    readonly t1: [string, string];
    /** get */
    readonly ft1: <T>(vs: [T, T]) => T;
    /** get, set */
    ft2: <T>(vs: [T, T]) => T;

    /** 
     * get
     * 
     * v2 optional
     */
    readonly o1: <T>(v1: T, v2?: T) => T;
    /** 
     * get, set
     * 
     * v2 optional
     */
    o2: <T>(v1: T, v2?: T) => T;
    /** 
     * get
     * 
     * v2 optional
     */
    readonly o3: <T>(v1: T, v2?: [T, T]) => T;
    /** 
     * get, set
     * 
     * v2 optional
     */
    o4: <T>(v1: T, v2?: [T, T]) => T;

    /**
     * get
     * 
     * optional
     */
    readonly opt1?: string;
    /**
     * get, set
     * 
     * optional
     */
    opt2?: string;
    // // F#: `abstract opt2: ('T -> 'T) option`
    // // -> The type parameter 'T is not defined.
    // // Note: same `with get, set` is allowed (but neither `with get` nor `with set`)
    // /**
    //  * get
    //  * 
    //  * optional
    //  */
    // readonly opt3?: <T>(v1: T) => T;
    /**
     * get, set
     * 
     * optional
     */
    opt4?: <T>(v1: T) => T;
    /**
     * get
     * 
     * optional
     */
    readonly opt5?: (v1: string, v2: string) => string;
    // /**
    //  * get
    //  * 
    //  * optional
    //  */
    // readonly opt6?: <T>(v1: T, v2: T) => T;
    /**
     * get, set
     * 
     * optional
     */
    opt7?: (v1: string, v2: string) => string;
    /**
     * get, set
     * 
     * optional
     */
    opt8?: <T>(v1: T, v2: T) => T;

    /** function */
    ff1<T>(f: ((value: T) => T)): ((value: T) => T);
    /** function */
    ff2<T>(f: ((value: T) => T), v: T): string;
    /** function */
    ff3<T>(v1: T, v2: T | string): T | number
}

export class C {
    /** get */
    readonly v1: string;

    /** get */
    readonly f1: (value: string) => number;

    /** get */
    readonly f2: <T>(value: T) => number;
    /** get */
    readonly f3: <T>(value: string) => T;
    /** get */
    readonly f4: <T>(value: T) => T;

    /** get, set */
    f5: <T>(value: T) => T;
    /** get, set */
    f6: <T>(value: T) => T;

    /** get */
    readonly f7: <T>(v1: T, v2: T) => T;
    /** get */
    readonly f8: <T>(v1: T | string, v2: string | number | T) => T;

    /** get */
    readonly f9: <T>(f1: ((v1: T, v2: T) => T), f2: ((v1: string | T, v2: T | number) => string | T)) => T | string
    /** get, set */
    f10: <T>(f1: ((v1: T, v2: T) => T), f2: ((v1: string | T, v2: T | number) => string | T)) => T | string

    /** get */
    readonly f11: <T>(v1: T, v2: T) => T;
    /** get, set */
    f12: <T>(v1: T, v2: T) => T;

    /** get */
    readonly f13: (v1: string, v2: string) => string;
    /** get, set */
    f14: (v1: string, v2: string) => string;

    /** 
     * get 
     * 
     * T extends A
     */
    readonly gf1: <T extends A>(value: T) => T
    /** 
     * get, set
     * 
     * T extends A
     */
    gf2: <T extends A>(value: T) => T
    /** 
     * get 
     * 
     * T extends A
     */
    readonly gf3: <T extends A>(v1: T, v2: T) => T
    /** 
     * get, set
     * 
     * T extends A
     */
    gf4: <T extends A>(v1: T, v2: T) => T
    /**
     * get
     * 
     * T extends A
     * U extends A & B
     */
    readonly gf5: <T extends A, U extends A & B>(v1: T, v2: U) => U
    /**
     * get, set
     * 
     * T extends A
     * U extends A & B
     */
    gf6: <T extends A, U extends A & B>(v1: T, v2: U) => U

    /** get */
    readonly arrayify: <T>(obj: T | T[]) => T[];
    
    /** get */
    readonly izi1: (v1: string, v2: string) => void;
    /** get, set */
    izi3: (v1: string, v2: string) => void;
    /** get */
    readonly izi4: <T>(v1: T, v2: T) => T;
    /** get, set */
    izi5: <T>(v1: T, v2: T) => T;

    /** get */
    readonly t1: [string, string];
    /** get */
    readonly ft1: <T>(vs: [T, T]) => T;
    /** get, set */
    ft2: <T>(vs: [T, T]) => T;

    /** 
     * get
     * 
     * v2 optional
     */
    readonly o1: <T>(v1: T, v2?: T) => T;
    /** 
     * get, set
     * 
     * v2 optional
     */
    o2: <T>(v1: T, v2?: T) => T;
    /** 
     * get
     * 
     * v2 optional
     */
    readonly o3: <T>(v1: T, v2?: [T, T]) => T;
    /** 
     * get, set
     * 
     * v2 optional
     */
    o4: <T>(v1: T, v2?: [T, T]) => T;

    /** function */
    ff1<T>(f: ((value: T) => T)): ((value: T) => T);
    /** function */
    ff2<T>(f: ((value: T) => T), v: T): string;
    /** function */
    ff3<T>(v1: T, v2: T | string): T | number 
}

/** static */
export class CS {
    /** get */
    static readonly v1: string;

    /** get */
    static readonly f1: (value: string) => number;

    /** get */
    static readonly f2: <T>(value: T) => number;
    /** get */
    static readonly f3: <T>(value: string) => T;
    /** get */
    static readonly f4: <T>(value: T) => T;

    /** get, set */
    static f5: <T>(value: T) => T;
    /** get, set */
    static f6: <T>(value: T) => T;

    /** get */
    static readonly f7: <T>(v1: T, v2: T) => T;
    /** get */
    static readonly f8: <T>(v1: T | string, v2: string | number | T) => T;

    /** get */
    static readonly f9: <T>(f1: ((v1: T, v2: T) => T), f2: ((v1: string | T, v2: T | number) => string | T)) => T | string
    /** get, set */
    static f10: <T>(f1: ((v1: T, v2: T) => T), f2: ((v1: string | T, v2: T | number) => string | T)) => T | string

    /** get */
    static readonly f11: <T>(v1: T, v2: T) => T;
    /** get, set */
    static f12: <T>(v1: T, v2: T) => T;

    /** get */
    static readonly f13: (v1: string, v2: string) => string;
    /** get, set */
    static f14: (v1: string, v2: string) => string;

    /** 
     * get 
     * 
     * T extends A
     */
    static readonly gf1: <T extends A>(value: T) => T
    /** 
     * get, set
     * 
     * T extends A
     */
    static gf2: <T extends A>(value: T) => T
    /** 
     * get 
     * 
     * T extends A
     */
    static readonly gf3: <T extends A>(v1: T, v2: T) => T
    /** 
     * get, set
     * 
     * T extends A
     */
    static gf4: <T extends A>(v1: T, v2: T) => T
    /**
     * get
     * 
     * T extends A
     * U extends A & B
     */
    static readonly gf5: <T extends A, U extends A & B>(v1: T, v2: U) => U
    /**
     * get, set
     * 
     * T extends A
     * U extends A & B
     */
    static gf6: <T extends A, U extends A & B>(v1: T, v2: U) => U

    /** get */
    static readonly arrayify: <T>(obj: T | T[]) => T[];
    
    /** get */
    static readonly izi1: (v1: string, v2: string) => void;
    /** get, set */
    static izi3: (v1: string, v2: string) => void;
    /** get */
    static readonly izi4: <T>(v1: T, v2: T) => T;
    /** get, set */
    static izi5: <T>(v1: T, v2: T) => T;

    /** get */
    static readonly t1: [string, string];
    /** get */
    static readonly ft1: <T>(vs: [T, T]) => T;
    /** get, set */
    static ft2: <T>(vs: [T, T]) => T;

    /** 
     * get
     * 
     * v2 optional
     */
    static readonly o1: <T>(v1: T, v2?: T) => T;
    /** 
     * get, set
     * 
     * v2 optional
     */
    static o2: <T>(v1: T, v2?: T) => T;
    /** 
     * get
     * 
     * v2 optional
     */
    static readonly o3: <T>(v1: T, v2?: [T, T]) => T;
    /** 
     * get, set
     * 
     * v2 optional
     */
    static o4: <T>(v1: T, v2?: [T, T]) => T;

    /** function */
    static ff1<T>(f: ((value: T) => T)): ((value: T) => T);
    /** function */
    static ff2<T>(f: ((value: T) => T), v: T): string;
    /** function */
    static ff3<T>(v1: T, v2: T | string): T | number 
}