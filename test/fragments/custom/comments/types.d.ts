/**
 * Summary: SomeFunction
 * 
 * @remarks Remarks:
 * SomeFunction 
 * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
 */
export function SomeFunction(): void

/**
 * Summary: SomeInterface
 * 
 * @remarks Remarks:
 * SomeInterface 
 * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
 */
export interface SomeInterface {
    /**
     * Summary: SomeValue
     * 
     * @remarks Remarks:
     * SomeValue 
     * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
     */
    SomeValue: number
    /**
     * Summary: SomeFunction
     * 
     * @remarks Remarks:
     * SomeFunction 
     * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
     */
    SomeFunction(): void
}
/**
 * Summary: SomeClass
 * 
 * @remarks Remarks:
 * SomeClass 
 * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
 */
export class SomeClass {
    /**
     * Summary: SomeValue
     * 
     * @remarks Remarks:
     * SomeValue 
     * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
     */
    SomeValue: number;
    /**
     * Summary: constructor
     * 
     * @remarks Remarks:
     * constructor 
     * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
     */
    constructor(value: string);

    /**
     * Summary: SomeFunction
     * 
     * @remarks Remarks:
     * SomeFunction 
     * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
     */
    SomeFunction();
}

/**
 * Summary: SomeType
 * 
 * @remarks Remarks:
 * SomeType 
 * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
 */
export type SomeType = {
    /**
     * Summary: SomeValue
     * 
     * @remarks Remarks:
     * SomeValue 
     * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
     */
    SomeValue: number
    /**
     * Summary: SomeFunction
     * 
     * @remarks Remarks:
     * SomeFunction 
     * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
     */
    SomeFunction(): void
}

/**
 * Summary: SomeFunctionType
 * 
 * @remarks Remarks:
 * SomeFunctionType 
 * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
 */
export type SomeFunctionType = (a: number, b: string) => string

/**
 * Summary: SomeAlias
 * 
 * @remarks Remarks:
 * SomeAlias 
 * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
 */
export type SomeAlias = string

/**
 * Summary: SomeUnion
 * 
 * @remarks Remarks:
 * SomeUnion 
 * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
 */
export type SomeUnion = 
    /** 
     * Summary: string
     * 
     * @remarks Remarks:
     * string 
     * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
    */
		string 
    /** 
     * Summary: number
     * 
     * @remarks Remarks:
     * number 
     * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
    */
		| number


// Comments of Literal Case aren't stored in Node

/**
 * Summary: SomeLiteral
 * 
 * @remarks Remarks:
 * SomeLiteral 
 * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
 */
export type SomeLiteral = 
    /** 
     * Summary: A
     * 
     * @remarks Remarks:
     * A 
     * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
    */
    "A" 
    /** 
     * Summary: B
     * 
     * @remarks Remarks:
     * B 
     * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
    */
    | "B"

/**
 * Summary: SomeIntersectionType
 * 
 * @remarks Remarks:
 * SomeIntersectionType 
 * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
 */
export type SomeIntersectionType = number & string

/**
 * Summary: SomeEnum
 * 
 * @remarks Remarks:
 * SomeEnum 
 * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
 */
export enum SomeEnum {
    /**
     * Summary: `A = 0`
     * 
     * @remarks Remarks:
     * `A 
     * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable} = 0`
     */
    A = 0,
    /**
     * Summary: `B = 1`
     * 
     * @remarks `B = 1`
     */
    B = 1,
}

/**
 * Summary: SomeStringEnum
 * 
 * @remarks Remarks:
 * SomeStringEnum 
 * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
 */
export enum SomeStringEnum {
    /**
     * Summary: `A = "A"`
     * 
     * @remarks Remarks:
     * `A 
     * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable} = "A"`
     */
    A = "A",
    /**
     * Summary: `B = "B"`
     * 
     * @remarks Remarks:
     * `B 
     * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable} = "B"`
     */
    B = "B", 
}

/**
 * Summary: SomeConst
 * 
 * @remarks Remarks:
 * SomeConst 
 * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
 */
export const SomeConst: number;

/**
 * Summary: SomeVariable
 *
 * @remarks Remarks:
 * SomeVariable 
 * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
 */
export declare var SomeVariable: number;

/**
 * Summary: SomeNamespace
 * 
 * @remarks Remarks:
 * SomeNamespace 
 * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
 */
export namespace SomeNamespace {
    /**
     * Summary: SomeFunction
     * 
     * @remarks Remarks:
     * SomeFunction 
     * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
     */
    export function SomeFunction();
}

/**
 * Summary: SomeModule
 * 
 * @remarks Remarks:
 * SomeModule 
 * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
 */
export module SomeModule {
    /**
     * Summary: SomeFunction
     * 
     * @remarks Remarks:
     * SomeFunction 
     * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
     */
    export function SomeFunction();
}

/**
 * Summary: SomeGenericType
 * 
 * @remarks Remarks:
 * SomeGenericType 
 * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
 */
export type SomeGenericType<A> = {}

/**
 * Summary: SomeClassWithStaticFunction
 * 
 * @remarks Remarks:
 * SomeClassWithStaticFunction 
 * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
 */

declare class SomeClassWithStaticFunction {
    /**
     * Summary: SomeValue
     *
     * @remarks Remarks:
     * SomeValue 
     * and a link: {@link https://github.com/fable-compiler/ts2fable ts2fable}
     */
    static SomeStaticFunction(): void;
}

