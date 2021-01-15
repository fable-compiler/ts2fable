/**
 * Escape quotation marks
 * 
 * @deprecated Hello "World"!
 */
export interface A {}

/**
 * Multiline
 * 
 * @deprecated
 * Deprecated because
 * of stuff
 */
export interface B {}

/**
 * Multiple deprecated tags
 * 
 * @deprecated Message 1
 * @deprecated Reason 2
 */
export interface C {}

/**
 * Deprecated interface with default type parameter
 * 
 * Both Interfaces with generic and with default should be deprecated
 * 
 * @deprecated Interface with default is deprecated
 */
export interface D<T = string> {}
/**
 * Summary: SomeFunction
 * 
 * @deprecated SomeFunction is deprecated
 */
export function SomeFunction(): void

/**
 * Summary: SomeInterface
 * 
 * @deprecated SomeInterface is deprecated
 */
export interface SomeInterface {
    /**
     * Summary: SomeValue
     * 
     * @deprecated SomeValue is deprecated
     */
    SomeValue: number
    /**
     * Summary: SomeFunction
     * 
     * @deprecated SomeFunction is deprecated
     */
    SomeFunction(): void
}
/**
 * Summary: SomeClass
 * 
 * @deprecated SomeClass is deprecated
 */
export class SomeClass {
    /**
     * Summary: SomeValue
     * 
     * @deprecated SomeValue is deprecated
     */
    SomeValue: number;
    /**
     * Summary: constructor
     * 
     * @deprecated ctor is deprecated
     */
    constructor(value: string);

    /**
     * Summary: SomeFunction
     * 
     * @deprecated SomeFunction is deprecated
     */
    SomeFunction();
}

/**
 * Summary: SomeType
 * 
 * @deprecated SomeType is deprecated
 */
export type SomeType = {
    /**
     * Summary: SomeValue
     * 
     * @deprecated SomeValue is deprecated
     */
    SomeValue: number
    /**
     * Summary: SomeFunction
     * 
     * @deprecated SomeFunction is deprecated
     */
    SomeFunction(): void
}

/**
 * Summary: SomeFunctionType
 * 
 * @deprecated SomeFunctionType is deprecated
 */
export type SomeFunctionType = (a: number, b: string) => string

/**
 * Summary: SomeAlias
 * 
 * @deprecated SomeAlias is deprecated
 */
export type SomeAlias = string

/**
 * Summary: SomeUnion
 * 
 * @deprecated SomeUnion is deprecated
 */
export type SomeUnion = 
    /** 
     * Summary: string
     * 
     * @deprecated SomeUnion.string is deprecated
    */
		string 
    /** 
     * Summary: number
     * 
     * @deprecated SomeUnion.number is deprecated
    */
		| number


// Comments of Literal Case aren't stored in Node

/**
 * Summary: SomeLiteral
 * 
 * @deprecated SomeLiteral is deprecated
 */
export type SomeLiteral = 
    /** 
     * Summary: A
     * 
     * @deprecated "A" is deprecated
    */
    "A" 
    /** 
     * Summary: B
     * 
     * @deprecated "B" is deprecated
    */
    | "B"

/**
 * Summary: SomeIntersectionType
 * 
 * @deprecated SomeIntersectionType is deprecated
 */
export type SomeIntersectionType = number & string

/**
 * Summary: SomeEnum
 * 
 * @deprecated SomeEnum is deprecated
 */
export enum SomeEnum {
    /**
     * Summary: `A = 0`
     * 
     * @deprecated A is deprecated
     */
    A = 0,
    /**
     * Summary: `B = 1`
     * 
     * @deprecated B is deprecated
     */
    B = 1,
}

/**
 * Summary: SomeStringEnum
 * 
 * @deprecated SomeStringEnum is deprecated
 */
export enum SomeStringEnum {
    /**
     * Summary: `A = "A"`
     * 
     * @deprecated A is deprecated
     */
    A = "A",
    /**
     * Summary: `B = "B"`
     * 
     * @deprecated B is deprecated
     */
    B = "B", 
}

/**
 * Summary: SomeConst
 * 
 * @deprecated SomeConst is deprecated
 */
export const SomeConst: number;

/**
 * Summary: SomeVariable
 *
 * @deprecated SomeVariable is deprecated
 */
export declare var SomeVariable: number;

/**
 * Summary: SomeNamespace
 * 
 * @deprecated SomeNamespace is deprecated
 */
export namespace SomeNamespace {
    /**
     * Summary: SomeFunction
     * 
     * @deprecated SomeFunction is deprecated
     */
    export function SomeFunction();
}

/**
 * Summary: SomeModule
 * 
 * @deprecated SomeModule is deprecated
 */
export module SomeModule {
    /**
     * Summary: SomeFunction
     * 
     * @deprecated SomeFunction is deprecated
     */
    export function SomeFunction();
}

/**
 * Summary: SomeGenericType
 * 
 * @deprecated SomeGenericType is deprecated
 */
export type SomeGenericType<A> = {}

/**
 * Summary: SomeClassWithStaticFunction
 * 
 * @deprecated SomeClassWithStaticFunction is deprecated
 */

declare class SomeClassWithStaticFunction {
    /**
     * Summary: SomeStaticFunction
     *
     * @deprecated SomeStaticFunction is deprecated
     */
    static SomeStaticFunction(): void;
}

