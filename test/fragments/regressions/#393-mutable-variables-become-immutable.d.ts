export module M {
    /** `var` -> mutable */
    var v2: number;
    /** `let` -> mutable */
    let l1: number;
    /** `const` -> immutable */
    const c1: number;
}

export namespace N {
    /** `var` -> mutable */
    var v2: number;
    /** `let` -> mutable */
    let l1: number;
    /** `const` -> immutable */
    const c1: number;
}

/** 
 * `var` -> mutable 
 * 
 * BUT: 
 * > error FS0874: Mutable 'let' bindings can't be recursive or defined in recursive modules or namespaces
 * -> keep immutable
 * */
export var v2: number;
/** 
 * `let` -> mutable 
 * 
 * BUT: 
 * > error FS0874: Mutable 'let' bindings can't be recursive or defined in recursive modules or namespaces
 * -> keep immutable
 * */
export let l1: number;
/** `const` -> immutable */
export const c1: number;
