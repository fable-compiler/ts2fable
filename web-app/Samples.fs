module Samples

let [<Literal>] chai =
    """// Type definitions for chai 4.1
// Project: http://chaijs.com/
// Definitions by: Jed Mao <https://github.com/jedmao>,
//                 Bart van der Schoor <https://github.com/Bartvds>,
//                 Andrew Brown <https://github.com/AGBrown>,
//                 Olivier Chevet <https://github.com/olivr70>,
//                 Matt Wistrand <https://github.com/mwistrand>,
//                 Josh Goldberg <https://github.com/joshuakgoldberg>
//                 Shaun Luttin <https://github.com/shaunluttin>
//                 Gintautas Miselis <https://github.com/Naktibalda>
//                 Satana Charuwichitratana <https://github.com/micksatana>
//                 Erik Schierboom <https://github.com/ErikSchierboom>
// Definitions: https://github.com/DefinitelyTyped/DefinitelyTyped

declare namespace Chai {
    interface ChaiStatic {
        expect: ExpectStatic;
        should(): Should;
        /**
         * Provides a way to extend the internals of Chai
         */
        use(fn: (chai: any, utils: any) => void): ChaiStatic;
        assert: AssertStatic;
        config: Config;
        AssertionError: typeof AssertionError;
        version: string;
    }

    export interface ExpectStatic extends AssertionStatic {
        fail(actual?: any, expected?: any, message?: string, operator?: Operator): void;
    }

    export interface AssertStatic extends Assert {
    }

    export interface AssertionStatic {
        (target: any, message?: string): Assertion;
    }

    export type Operator = string; // "==" | "===" | ">" | ">=" | "<" | "<=" | "!=" | "!==";

    export type OperatorComparable = boolean | null | number | string | undefined | Date;

    interface ShouldAssertion {
        equal(value1: any, value2: any, message?: string): void;
        Throw: ShouldThrow;
        throw: ShouldThrow;
        exist(value: any, message?: string): void;
    }

    interface Should extends ShouldAssertion {
        not: ShouldAssertion;
        fail(actual: any, expected: any, message?: string, operator?: Operator): void;
    }

    interface ShouldThrow {
        (actual: Function, expected?: string|RegExp, message?: string): void;
        (actual: Function, constructor: Error|Function, expected?: string|RegExp, message?: string): void;
    }

    interface Assertion extends LanguageChains, NumericComparison, TypeComparison {
        not: Assertion;
        deep: Deep;
        ordered: Ordered;
        nested: Nested;
        any: KeyFilter;
        all: KeyFilter;
        a: TypeComparison;
        an: TypeComparison;
        include: Include;
        includes: Include;
        contain: Include;
        contains: Include;
        ok: Assertion;
        true: Assertion;
        false: Assertion;
        null: Assertion;
        undefined: Assertion;
        NaN: Assertion;
        exist: Assertion;
        empty: Assertion;
        arguments: Assertion;
        Arguments: Assertion;
        equal: Equal;
        equals: Equal;
        eq: Equal;
        eql: Equal;
        eqls: Equal;
        property: Property;
        ownProperty: OwnProperty;
        haveOwnProperty: OwnProperty;
        ownPropertyDescriptor: OwnPropertyDescriptor;
        haveOwnPropertyDescriptor: OwnPropertyDescriptor;
        length: Length;
        lengthOf: Length;
        match: Match;
        matches: Match;
        string(string: string, message?: string): Assertion;
        keys: Keys;
        key(string: string): Assertion;
        throw: Throw;
        throws: Throw;
        Throw: Throw;
        respondTo: RespondTo;
        respondsTo: RespondTo;
        itself: Assertion;
        satisfy: Satisfy;
        satisfies: Satisfy;
        closeTo: CloseTo;
        approximately: CloseTo;
        members: Members;
        increase: PropertyChange;
        increases: PropertyChange;
        decrease: PropertyChange;
        decreases: PropertyChange;
        change: PropertyChange;
        changes: PropertyChange;
        extensible: Assertion;
        sealed: Assertion;
        frozen: Assertion;
        oneOf(list: any[], message?: string): Assertion;
    }

    interface LanguageChains {
        to: Assertion;
        be: Assertion;
        been: Assertion;
        is: Assertion;
        that: Assertion;
        which: Assertion;
        and: Assertion;
        has: Assertion;
        have: Assertion;
        with: Assertion;
        at: Assertion;
        of: Assertion;
        same: Assertion;
        but: Assertion;
        does: Assertion;
    }

    interface NumericComparison {
        above: NumberComparer;
        gt: NumberComparer;
        greaterThan: NumberComparer;
        least: NumberComparer;
        gte: NumberComparer;
        below: NumberComparer;
        lt: NumberComparer;
        lessThan: NumberComparer;
        most: NumberComparer;
        lte: NumberComparer;
        within(start: number, finish: number, message?: string): Assertion;
        within(start: Date, finish: Date, message?: string): Assertion;
    }

    interface NumberComparer {
        (value: number | Date, message?: string): Assertion;
    }

    interface TypeComparison {
        (type: string, message?: string): Assertion;
        instanceof: InstanceOf;
        instanceOf: InstanceOf;
    }

    interface InstanceOf {
        (constructor: Object, message?: string): Assertion;
    }

    interface CloseTo {
        (expected: number, delta: number, message?: string): Assertion;
    }

    interface Nested {
      include: Include;
      property: Property;
      members: Members;
    }

    interface Deep {
        equal: Equal;
        equals: Equal;
        eq: Equal;
        include: Include;
        property: Property;
        members: Members;
        ordered: Ordered;
    }

    interface Ordered {
        members: Members;
    }

    interface KeyFilter {
        keys: Keys;
    }

    interface Equal {
        (value: any, message?: string): Assertion;
    }

    interface Property {
        (name: string, value?: any, message?: string): Assertion;
    }

    interface OwnProperty {
        (name: string, message?: string): Assertion;
    }

    interface OwnPropertyDescriptor {
        (name: string, descriptor: PropertyDescriptor, message?: string): Assertion;
        (name: string, message?: string): Assertion;
    }

    interface Length extends LanguageChains, NumericComparison {
        (length: number, message?: string): Assertion;
    }

    interface Include {
        (value: Object | string | number, message?: string): Assertion;
        keys: Keys;
        deep: Deep;
        ordered: Ordered;
        members: Members;
        any: KeyFilter;
        all: KeyFilter;
    }

    interface Match {
        (regexp: RegExp, message?: string): Assertion;
    }

    interface Keys {
        (...keys: string[]): Assertion;
        (keys: any[]|Object): Assertion;
    }

    interface Throw {
        (expected?: string|RegExp, message?: string): Assertion;
        (constructor: Error|Function, expected?: string|RegExp, message?: string): Assertion;
    }

    interface RespondTo {
        (method: string, message?: string): Assertion;
    }

    interface Satisfy {
        (matcher: Function, message?: string): Assertion;
    }

    interface Members {
        (set: any[], message?: string): Assertion;
    }

    interface PropertyChange {
        (object: Object, property: string, message?: string): Assertion;
    }

    export interface Assert {
        /**
         * @param expression    Expression to test for truthiness.
         * @param message    Message to display on error.
         */
        (expression: any, message?: string): void;

        /**
         * Throws a failure.
         *
         * @type T   Type of the objects.
         * @param actual   Actual value.
         * @param expected   Potential expected value.
         * @param message    Message to display on error.
         * @param operator   Comparison operator, if not strict equality.
         * @remarks Node.js assert module-compatible.
         */
        fail<T>(actual?: T, expected?: T, message?: string, operator?: Operator): void;

        /**
         * Asserts that object is truthy.
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param message    Message to display on error.
         */
        isOk<T>(value: T, message?: string): void;

        /**
         * Asserts that object is truthy.
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param message    Message to display on error.
         */
        ok<T>(value: T, message?: string): void;

        /**
         * Asserts that object is falsy.
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param message    Message to display on error.
         */
        isNotOk<T>(value: T, message?: string): void;

        /**
         * Asserts that object is falsy.
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param message    Message to display on error.
         */
        notOk<T>(value: T, message?: string): void;

        /**
         * Asserts non-strict equality (==) of actual and expected.
         *
         * @type T   Type of the objects.
         * @param actual   Actual value.
         * @param expected   Potential expected value.
         * @param message   Message to display on error.
         */
        equal<T>(actual: T, expected: T, message?: string): void;

        /**
         * Asserts non-strict inequality (==) of actual and expected.
         *
         * @type T   Type of the objects.
         * @param actual   Actual value.
         * @param expected   Potential expected value.
         * @param message   Message to display on error.
         */
        notEqual<T>(actual: T, expected: T, message?: string): void;

        /**
         * Asserts strict equality (===) of actual and expected.
         *
         * @type T   Type of the objects.
         * @param actual   Actual value.
         * @param expected   Potential expected value.
         * @param message   Message to display on error.
         */
        strictEqual<T>(actual: T, expected: T, message?: string): void;

        /**
         * Asserts strict inequality (==) of actual and expected.
         *
         * @type T   Type of the objects.
         * @param actual   Actual value.
         * @param expected   Potential expected value.
         * @param message   Message to display on error.
         */
        notStrictEqual<T>(actual: T, expected: T, message?: string): void;

        /**
         * Asserts that actual is deeply equal (==) to expected.
         *
         * @type T   Type of the objects.
         * @param actual   Actual value.
         * @param expected   Potential expected value.
         * @param message   Message to display on error.
         */
        deepEqual<T>(actual: T, expected: T, message?: string): void;

        /**
         * Asserts that actual is not deeply equal (==) to expected.
         *
         * @type T   Type of the objects.
         * @param actual   Actual value.
         * @param expected   Potential expected value.
         * @param message   Message to display on error.
         */
        notDeepEqual<T>(actual: T, expected: T, message?: string): void;

        /**
         * Asserts that actual is deeply strict equal (===) to expected.
         *
         * @type T   Type of the objects.
         * @param actual   Actual value.
         * @param expected   Potential expected value.
         * @param message   Message to display on error.
         */
        deepStrictEqual<T>(actual: T, expected: T, message?: string): void;

        /**
         * Asserts valueToCheck is strictly greater than (>) valueToBeAbove.
         *
         * @param valueToCheck   Actual value.
         * @param valueToBeAbove   Minimum Potential expected value.
         * @param message   Message to display on error.
         */
        isAbove(valueToCheck: number, valueToBeAbove: number, message?: string): void;

        /**
         * Asserts valueToCheck is greater than or equal to (>=) valueToBeAtLeast.
         *
         * @param valueToCheck   Actual value.
         * @param valueToBeAtLeast   Minimum Potential expected value.
         * @param message   Message to display on error.
         */
        isAtLeast(valueToCheck: number, valueToBeAtLeast: number, message?: string): void;

        /**
         * Asserts valueToCheck is strictly less than (<) valueToBeBelow.
         *
         * @param valueToCheck   Actual value.
         * @param valueToBeBelow   Minimum Potential expected value.
         * @param message   Message to display on error.
         */
        isBelow(valueToCheck: number, valueToBeBelow: number, message?: string): void;

        /**
         * Asserts valueToCheck is greater than or equal to (>=) valueToBeAtMost.
         *
         * @param valueToCheck   Actual value.
         * @param valueToBeAtMost   Minimum Potential expected value.
         * @param message   Message to display on error.
         */
        isAtMost(valueToCheck: number, valueToBeAtMost: number, message?: string): void;

        /**
         * Asserts that value is true.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isTrue<T>(value: T, message?: string): void;

        /**
         * Asserts that value is false.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isFalse<T>(value: T, message?: string): void;

        /**
         * Asserts that value is not true.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isNotTrue<T>(value: T, message?: string): void;

        /**
         * Asserts that value is not false.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isNotFalse<T>(value: T, message?: string): void;

        /**
         * Asserts that value is null.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isNull<T>(value: T, message?: string): void;

        /**
         * Asserts that value is not null.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isNotNull<T>(value: T, message?: string): void;

        /**
         * Asserts that value is not null.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isNaN<T>(value: T, message?: string): void;

        /**
         * Asserts that value is not null.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isNotNaN<T>(value: T, message?: string): void;

        /**
         * Asserts that the target is neither null nor undefined.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message    Message to display on error.
         */
        exists<T>(value: T, message?: string): void;

        /**
         * Asserts that the target is either null or undefined.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message    Message to display on error.
         */
        notExists<T>(value: T, message?: string): void;

        /**
         * Asserts that value is undefined.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isUndefined<T>(value: T, message?: string): void;

        /**
         * Asserts that value is not undefined.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isDefined<T>(value: T, message?: string): void;

        /**
         * Asserts that value is a function.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isFunction<T>(value: T, message?: string): void;

        /**
         * Asserts that value is not a function.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isNotFunction<T>(value: T, message?: string): void;

        /**
         * Asserts that value is an object of type 'Object'
         * (as revealed by Object.prototype.toString).
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         * @remarks The assertion does not match subclassed objects.
         */
        isObject<T>(value: T, message?: string): void;

        /**
         * Asserts that value is not an object of type 'Object'
         * (as revealed by Object.prototype.toString).
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isNotObject<T>(value: T, message?: string): void;

        /**
         * Asserts that value is an array.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isArray<T>(value: T, message?: string): void;

        /**
         * Asserts that value is not an array.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isNotArray<T>(value: T, message?: string): void;

        /**
         * Asserts that value is a string.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isString<T>(value: T, message?: string): void;

        /**
         * Asserts that value is not a string.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isNotString<T>(value: T, message?: string): void;

        /**
         * Asserts that value is a number.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isNumber<T>(value: T, message?: string): void;

        /**
         * Asserts that value is not a number.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isNotNumber<T>(value: T, message?: string): void;

        /**
         * Asserts that value is a boolean.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isBoolean<T>(value: T, message?: string): void;

        /**
         * Asserts that value is not a boolean.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param message   Message to display on error.
         */
        isNotBoolean<T>(value: T, message?: string): void;

        /**
         * Asserts that value's type is name, as determined by Object.prototype.toString.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param name   Potential expected type name of value.
         * @param message   Message to display on error.
         */
        typeOf<T>(value: T, name: string, message?: string): void;

        /**
         * Asserts that value's type is not name, as determined by Object.prototype.toString.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param name   Potential expected type name of value.
         * @param message   Message to display on error.
         */
        notTypeOf<T>(value: T, name: string, message?: string): void;

        /**
         * Asserts that value is an instance of constructor.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param constructor   Potential expected contructor of value.
         * @param message   Message to display on error.
         */
        instanceOf<T>(value: T, constructor: Function, message?: string): void;

        /**
         * Asserts that value is not an instance of constructor.
         *
         * @type T   Type of value.
         * @param value   Actual value.
         * @param constructor   Potential expected contructor of value.
         * @param message   Message to display on error.
         */
        notInstanceOf<T>(value: T, type: Function, message?: string): void;

        /**
         * Asserts that haystack includes needle.
         *
         * @param haystack   Container string.
         * @param needle   Potential expected substring of haystack.
         * @param message   Message to display on error.
         */
        include(haystack: string, needle: string, message?: string): void;

        /**
         * Asserts that haystack includes needle.
         *
         * @type T   Type of values in haystack.
         * @param haystack   Container array.
         * @param needle   Potential value contained in haystack.
         * @param message   Message to display on error.
         */
        include<T>(haystack: T[], needle: T, message?: string): void;

        /**
         * Asserts that haystack does not include needle.
         *
         * @param haystack   Container string or array.
         * @param needle   Potential expected substring of haystack.
         * @param message   Message to display on error.
         */
        notInclude(haystack: string | any[], needle: any, message?: string): void;

        /**
         * Asserts that haystack includes needle. Can be used to assert the inclusion of a value in an array or a subset of properties in an object. Deep equality is used.
         *
         * @param haystack   Container string.
         * @param needle   Potential expected substring of haystack.
         * @param message   Message to display on error.
         */
        deepInclude(haystack: string, needle: string, message?: string): void;

        /**
         * Asserts that haystack includes needle. Can be used to assert the inclusion of a value in an array or a subset of properties in an object. Deep equality is used.
         *
         * @param haystack
         * @param needle
         * @param message   Message to display on error.
         */
        deepInclude<T>(haystack: any, needle: any, message?: string): void;

        /**
         * Asserts that haystack does not include needle. Can be used to assert the absence of a value in an array or a subset of properties in an object. Deep equality is used.
         *
         * @param haystack   Container string or array.
         * @param needle   Potential expected substring of haystack.
         * @param message   Message to display on error.
         */
        notDeepInclude(haystack: string | any[], needle: any, message?: string): void;

        /**
         * Asserts that ‘haystack’ includes ‘needle’. Can be used to assert the inclusion of a subset of properties in an object.
         *
         * Enables the use of dot- and bracket-notation for referencing nested properties.
         * ‘[]’ and ‘.’ in property names can be escaped using double backslashes.Asserts that ‘haystack’ includes ‘needle’.
         * Can be used to assert the inclusion of a subset of properties in an object.
         * Enables the use of dot- and bracket-notation for referencing nested properties.
         * ‘[]’ and ‘.’ in property names can be escaped using double backslashes.
         *
         * @param haystack
         * @param needle
         * @param message   Message to display on error.
         */
        nestedInclude(haystack: any, needle: any, message?: string): void;

        /**
         * Asserts that ‘haystack’ does not include ‘needle’. Can be used to assert the absence of a subset of properties in an object.
         *
         * Enables the use of dot- and bracket-notation for referencing nested properties.
         * ‘[]’ and ‘.’ in property names can be escaped using double backslashes.Asserts that ‘haystack’ includes ‘needle’.
         * Can be used to assert the inclusion of a subset of properties in an object.
         * Enables the use of dot- and bracket-notation for referencing nested properties.
         * ‘[]’ and ‘.’ in property names can be escaped using double backslashes.
         *
         * @param haystack
         * @param needle
         * @param message   Message to display on error.
         */
        notNestedInclude(haystack: any, needle: any, message?: string): void;

        /**
         * Asserts that ‘haystack’ includes ‘needle’. Can be used to assert the inclusion of a subset of properties in an object while checking for deep equality
         *
         * Enables the use of dot- and bracket-notation for referencing nested properties.
         * ‘[]’ and ‘.’ in property names can be escaped using double backslashes.Asserts that ‘haystack’ includes ‘needle’.
         * Can be used to assert the inclusion of a subset of properties in an object.
         * Enables the use of dot- and bracket-notation for referencing nested properties.
         * ‘[]’ and ‘.’ in property names can be escaped using double backslashes.
         *
         * @param haystack
         * @param needle
         * @param message   Message to display on error.
         */
        deepNestedInclude(haystack: any, needle: any, message?: string): void;

        /**
         * Asserts that ‘haystack’ does not include ‘needle’. Can be used to assert the absence of a subset of properties in an object while checking for deep equality.
         *
         * Enables the use of dot- and bracket-notation for referencing nested properties.
         * ‘[]’ and ‘.’ in property names can be escaped using double backslashes.Asserts that ‘haystack’ includes ‘needle’.
         * Can be used to assert the inclusion of a subset of properties in an object.
         * Enables the use of dot- and bracket-notation for referencing nested properties.
         * ‘[]’ and ‘.’ in property names can be escaped using double backslashes.
         *
         * @param haystack
         * @param needle
         * @param message   Message to display on error.
         */
        notDeepNestedInclude(haystack: any, needle: any, message?: string): void;

        /**
         * Asserts that ‘haystack’ includes ‘needle’. Can be used to assert the inclusion of a subset of properties in an object while ignoring inherited properties.
         *
         * @param haystack
         * @param needle
         * @param message   Message to display on error.
         */
        ownInclude(haystack: any, needle: any, message?: string): void;

        /**
         * Asserts that ‘haystack’ includes ‘needle’. Can be used to assert the absence of a subset of properties in an object while ignoring inherited properties.
         *
         * @param haystack
         * @param needle
         * @param message   Message to display on error.
         */
        notOwnInclude(haystack: any, needle: any, message?: string): void;

        /**
         * Asserts that ‘haystack’ includes ‘needle’. Can be used to assert the inclusion of a subset of properties in an object while ignoring inherited properties and checking for deep
         *
         * @param haystack
         * @param needle
         * @param message   Message to display on error.
         */
        deepOwnInclude(haystack: any, needle: any, message?: string): void;

        /**
         * Asserts that ‘haystack’ includes ‘needle’. Can be used to assert the absence of a subset of properties in an object while ignoring inherited properties and checking for deep equality.
         *
         * @param haystack
         * @param needle
         * @param message   Message to display on error.
         */
        notDeepOwnInclude(haystack: any, needle: any, message?: string): void;

        /**
         * Asserts that value matches the regular expression regexp.
         *
         * @param value   Actual value.
         * @param regexp   Potential match of value.
         * @param message   Message to display on error.
         */
        match(value: string, regexp: RegExp, message?: string): void;

        /**
         * Asserts that value does not match the regular expression regexp.
         *
         * @param value   Actual value.
         * @param regexp   Potential match of value.
         * @param message   Message to display on error.
         */
        notMatch(expected: any, regexp: RegExp, message?: string): void;

        /**
         * Asserts that object has a property named by property.
         *
         * @type T   Type of object.
         * @param object   Container object.
         * @param property   Potential contained property of object.
         * @param message   Message to display on error.
         */
        property<T>(object: T, property: string /* keyof T */, message?: string): void;

        /**
         * Asserts that object has a property named by property.
         *
         * @type T   Type of object.
         * @param object   Container object.
         * @param property   Potential contained property of object.
         * @param message   Message to display on error.
         */
        notProperty<T>(object: T, property: string /* keyof T */, message?: string): void;

        /**
         * Asserts that object has a property named by property, which can be a string
         * using dot- and bracket-notation for deep reference.
         *
         * @type T   Type of object.
         * @param object   Container object.
         * @param property   Potential contained property of object.
         * @param message   Message to display on error.
         */
        deepProperty<T>(object: T, property: string, message?: string): void;

        /**
         * Asserts that object does not have a property named by property, which can be a
         * string using dot- and bracket-notation for deep reference.
         *
         * @type T   Type of object.
         * @param object   Container object.
         * @param property   Potential contained property of object.
         * @param message   Message to display on error.
         */
        notDeepProperty<T>(object: T, property: string, message?: string): void;

        /**
         * Asserts that object has a property named by property with value given by value.
         *
         * @type T   Type of object.
         * @type V   Type of value.
         * @param object   Container object.
         * @param property   Potential contained property of object.
         * @param value   Potential expected property value.
         * @param message   Message to display on error.
         */
        propertyVal<T, V>(object: T, property: string /* keyof T */, value: V, message?: string): void;

        /**
         * Asserts that object has a property named by property with value given by value.
         *
         * @type T   Type of object.
         * @type V   Type of value.
         * @param object   Container object.
         * @param property   Potential contained property of object.
         * @param value   Potential expected property value.
         * @param message   Message to display on error.
         */
        propertyNotVal<T, V>(object: T, property: string /* keyof T */, value: V, message?: string): void;

        /**
         * Asserts that object has a property named by property, which can be a string
         * using dot- and bracket-notation for deep reference.
         *
         * @type T   Type of object.
         * @type V   Type of value.
         * @param object   Container object.
         * @param property   Potential contained property of object.
         * @param value   Potential expected property value.
         * @param message   Message to display on error.
         */
        deepPropertyVal<T, V>(object: T, property: string, value: V, message?: string): void;

        /**
         * Asserts that object does not have a property named by property, which can be a
         * string using dot- and bracket-notation for deep reference.
         *
         * @type T   Type of object.
         * @type V   Type of value.
         * @param object   Container object.
         * @param property   Potential contained property of object.
         * @param value   Potential expected property value.
         * @param message   Message to display on error.
         */
        deepPropertyNotVal<T, V>(object: T, property: string, value: V, message?: string): void;

        /**
         * Asserts that object has a length property with the expected value.
         *
         * @type T   Type of object.
         * @param object   Container object.
         * @param length   Potential expected length of object.
         * @param message   Message to display on error.
         */
        lengthOf<T extends { readonly length?: number }>(object: T, length: number, message?: string): void;

        /**
         * Asserts that fn will throw an error.
         *
         * @param fn   Function that may throw.
         * @param message   Message to display on error.
         */
        throw(fn: Function, message?: string): void;

        /**
         * Asserts that function will throw an error with message matching regexp.
         *
         * @param fn   Function that may throw.
         * @param regExp   Potential expected message match.
         * @param message   Message to display on error.
         */
        throw(fn: Function, regExp: RegExp): void;

        /**
         * Asserts that function will throw an error that is an instance of constructor.
         *
         * @param fn   Function that may throw.
         * @param constructor   Potential expected error constructor.
         * @param message   Message to display on error.
         */
        throw(fn: Function, constructor: Function, message?: string): void;

        /**
         * Asserts that function will throw an error that is an instance of constructor
         * and an error with message matching regexp.
         *
         * @param fn   Function that may throw.
         * @param constructor   Potential expected error constructor.
         * @param message   Message to display on error.
         */
        throw(fn: Function, constructor: Function, regExp: RegExp): void;

        /**
         * Asserts that fn will throw an error.
         *
         * @param fn   Function that may throw.
         * @param message   Message to display on error.
         */
        throws(fn: Function, message?: string): void;

        /**
         * Asserts that function will throw an error with message matching regexp.
         *
         * @param fn   Function that may throw.
         * @param errType  Potential expected message match or error constructor.
         * @param message   Message to display on error.
         */
        throws(fn: Function, errType: RegExp|Function, message?: string): void;

        /**
         * Asserts that function will throw an error that is an instance of constructor
         * and an error with message matching regexp.
         *
         * @param fn   Function that may throw.
         * @param constructor   Potential expected error constructor.
         * @param message   Message to display on error.
         */
        throws(fn: Function, errType: Function, regExp: RegExp): void;

        /**
         * Asserts that fn will throw an error.
         *
         * @param fn   Function that may throw.
         * @param message   Message to display on error.
         */
        Throw(fn: Function, message?: string): void;

        /**
         * Asserts that function will throw an error with message matching regexp.
         *
         * @param fn   Function that may throw.
         * @param regExp   Potential expected message match.
         * @param message   Message to display on error.
         */
        Throw(fn: Function, regExp: RegExp): void;

        /**
         * Asserts that function will throw an error that is an instance of constructor.
         *
         * @param fn   Function that may throw.
         * @param constructor   Potential expected error constructor.
         * @param message   Message to display on error.
         */
        Throw(fn: Function, errType: Function, message?: string): void;

        /**
         * Asserts that function will throw an error that is an instance of constructor
         * and an error with message matching regexp.
         *
         * @param fn   Function that may throw.
         * @param constructor   Potential expected error constructor.
         * @param message   Message to display on error.
         */
        Throw(fn: Function, errType: Function, regExp: RegExp): void;

        /**
         * Asserts that fn will not throw an error.
         *
         * @param fn   Function that may throw.
         * @param message   Message to display on error.
         */
        doesNotThrow(fn: Function, message?: string): void;

        /**
         * Asserts that function will throw an error with message matching regexp.
         *
         * @param fn   Function that may throw.
         * @param regExp   Potential expected message match.
         * @param message   Message to display on error.
         */
        doesNotThrow(fn: Function, regExp: RegExp): void;

        /**
         * Asserts that function will throw an error that is an instance of constructor.
         *
         * @param fn   Function that may throw.
         * @param constructor   Potential expected error constructor.
         * @param message   Message to display on error.
         */
        doesNotThrow(fn: Function, errType: Function, message?: string): void;

        /**
         * Asserts that function will throw an error that is an instance of constructor
         * and an error with message matching regexp.
         *
         * @param fn   Function that may throw.
         * @param constructor   Potential expected error constructor.
         * @param message   Message to display on error.
         */
        doesNotThrow(fn: Function, errType: Function, regExp: RegExp): void;

        /**
         * Compares two values using operator.
         *
         * @param val1   Left value during comparison.
         * @param operator   Comparison operator.
         * @param val2   Right value during comparison.
         * @param message   Message to display on error.
         */
        operator(val1: OperatorComparable, operator: Operator, val2: OperatorComparable, message?: string): void;

        /**
         * Asserts that the target is equal to expected, to within a +/- delta range.
         *
         * @param actual   Actual value
         * @param expected   Potential expected value.
         * @param delta   Maximum differenced between values.
         * @param message   Message to display on error.
         */
        closeTo(actual: number, expected: number, delta: number, message?: string): void;

        /**
         * Asserts that the target is equal to expected, to within a +/- delta range.
         *
         * @param actual   Actual value
         * @param expected   Potential expected value.
         * @param delta   Maximum differenced between values.
         * @param message   Message to display on error.
         */
        approximately(act: number, exp: number, delta: number, message?: string): void;

        /**
         * Asserts that set1 and set2 have the same members. Order is not take into account.
         *
         * @type T   Type of set values.
         * @param set1   Actual set of values.
         * @param set2   Potential expected set of values.
         * @param message   Message to display on error.
         */
        sameMembers<T>(set1: T[], set2: T[], message?: string): void;

        /**
         * Asserts that set1 and set2 have the same members using deep equality checking.
         * Order is not take into account.
         *
         * @type T   Type of set values.
         * @param set1   Actual set of values.
         * @param set2   Potential expected set of values.
         * @param message   Message to display on error.
         */
        sameDeepMembers<T>(set1: T[], set2: T[], message?: string): void;

        /**
         * Asserts that set1 and set2 have the same members in the same order.
         * Uses a strict equality check (===).
         *
         * @type T   Type of set values.
         * @param set1   Actual set of values.
         * @param set2   Potential expected set of values.
         * @param message   Message to display on error.
         */
        sameOrderedMembers<T>(set1: T[], set2: T[], message?: string): void;

        /**
         * Asserts that set1 and set2 don’t have the same members in the same order.
         * Uses a strict equality check (===).
         *
         * @type T   Type of set values.
         * @param set1   Actual set of values.
         * @param set2   Potential expected set of values.
         * @param message   Message to display on error.
         */
        notSameOrderedMembers<T>(set1: T[], set2: T[], message?: string): void;

        /**
         * Asserts that set1 and set2 have the same members in the same order.
         * Uses a deep equality check.
         *
         * @type T   Type of set values.
         * @param set1   Actual set of values.
         * @param set2   Potential expected set of values.
         * @param message   Message to display on error.
         */
        sameDeepOrderedMembers<T>(set1: T[], set2: T[], message?: string): void;

        /**
         * Asserts that set1 and set2 don’t have the same members in the same order.
         * Uses a deep equality check.
         *
         * @type T   Type of set values.
         * @param set1   Actual set of values.
         * @param set2   Potential expected set of values.
         * @param message   Message to display on error.
         */
        notSameDeepOrderedMembers<T>(set1: T[], set2: T[], message?: string): void;

        /**
         * Asserts that subset is included in superset in the same order beginning with the first element in superset.
         * Uses a strict equality check (===).
         *
         * @type T   Type of set values.
         * @param superset   Actual set of values.
         * @param subset   Potential contained set of values.
         * @param message   Message to display on error.
         */
        includeOrderedMembers<T>(superset: T[], subset: T[], message?: string): void;

        /**
         * Asserts that subset isn’t included in superset in the same order beginning with the first element in superset.
         * Uses a strict equality check (===).
         *
         * @type T   Type of set values.
         * @param superset   Actual set of values.
         * @param subset   Potential contained set of values.
         * @param message   Message to display on error.
         */
        notIncludeOrderedMembers<T>(superset: T[], subset: T[], message?: string): void;

        /**
         * Asserts that subset is included in superset in the same order beginning with the first element in superset.
         * Uses a deep equality check.
         *
         * @type T   Type of set values.
         * @param superset   Actual set of values.
         * @param subset   Potential contained set of values.
         * @param message   Message to display on error.
         */
        includeDeepOrderedMembers<T>(superset: T[], subset: T[], message?: string): void;

        /**
         * Asserts that subset isn’t included in superset in the same order beginning with the first element in superset.
         * Uses a deep equality check.
         *
         * @type T   Type of set values.
         * @param superset   Actual set of values.
         * @param subset   Potential contained set of values.
         * @param message   Message to display on error.
         */
        notIncludeDeepOrderedMembers<T>(superset: T[], subset: T[], message?: string): void;

        /**
         * Asserts that subset is included in superset. Order is not take into account.
         *
         * @type T   Type of set values.
         * @param superset   Actual set of values.
         * @param subset   Potential contained set of values.
         * @param message   Message to display on error.
         */
        includeMembers<T>(superset: T[], subset: T[], message?: string): void;

        /**
         * Asserts that subset is included in superset using deep equality checking.
         * Order is not take into account.
         *
         * @type T   Type of set values.
         * @param superset   Actual set of values.
         * @param subset   Potential contained set of values.
         * @param message   Message to display on error.
         */
        includeDeepMembers<T>(superset: T[], subset: T[], message?: string): void;

        /**
         * Asserts that non-object, non-array value inList appears in the flat array list.
         *
         * @type T   Type of list values.
         * @param inList   Value expected to be in the list.
         * @param list   List of values.
         * @param message   Message to display on error.
         */
        oneOf<T>(inList: T, list: T[], message?: string): void;

        /**
         * Asserts that a function changes the value of a property.
         *
         * @type T   Type of object.
         * @param modifier   Function to run.
         * @param object   Container object.
         * @param property   Property of object expected to be modified.
         * @param message   Message to display on error.
         */
        changes<T>(modifier: Function, object: T, property: string /* keyof T */, message?: string): void;

        /**
         * Asserts that a function does not change the value of a property.
         *
         * @type T   Type of object.
         * @param modifier   Function to run.
         * @param object   Container object.
         * @param property   Property of object expected not to be modified.
         * @param message   Message to display on error.
         */
        doesNotChange<T>(modifier: Function, object: T, property: string /* keyof T */, message?: string): void;

        /**
         * Asserts that a function increases an object property.
         *
         * @type T   Type of object.
         * @param modifier   Function to run.
         * @param object   Container object.
         * @param property   Property of object expected to be increased.
         * @param message   Message to display on error.
         */
        increases<T>(modifier: Function, object: T, property: string /* keyof T */, message?: string): void;

        /**
         * Asserts that a function does not increase an object property.
         *
         * @type T   Type of object.
         * @param modifier   Function to run.
         * @param object   Container object.
         * @param property   Property of object expected not to be increased.
         * @param message   Message to display on error.
         */
        doesNotIncrease<T>(modifier: Function, object: T, property: string /* keyof T */, message?: string): void;

        /**
         * Asserts that a function decreases an object property.
         *
         * @type T   Type of object.
         * @param modifier   Function to run.
         * @param object   Container object.
         * @param property   Property of object expected to be decreased.
         * @param message   Message to display on error.
         */
        decreases<T>(modifier: Function, object: T, property: string /* keyof T */, message?: string): void;

        /**
         * Asserts that a function does not decrease an object property.
         *
         * @type T   Type of object.
         * @param modifier   Function to run.
         * @param object   Container object.
         * @param property   Property of object expected not to be decreased.
         * @param message   Message to display on error.
         */
        doesNotDecrease<T>(modifier: Function, object: T, property: string /* keyof T */, message?: string): void;

        /**
         * Asserts if value is not a false value, and throws if it is a true value.
         *
         * @type T   Type of object.
         * @param object   Actual value.
         * @param message   Message to display on error.
         * @remarks This is added to allow for chai to be a drop-in replacement for
         *          Node’s assert class.
         */
        ifError<T>(object: T, message?: string): void;

        /**
         * Asserts that object is extensible (can have new properties added to it).
         *
         * @type T   Type of object
         * @param object   Actual value.
         * @param message   Message to display on error.
         */
        isExtensible<T>(object: T, message?: string): void;

        /**
         * Asserts that object is extensible (can have new properties added to it).
         *
         * @type T   Type of object
         * @param object   Actual value.
         * @param message   Message to display on error.
         */
        extensible<T>(object: T, message?: string): void;

        /**
         * Asserts that object is not extensible.
         *
         * @type T   Type of object
         * @param object   Actual value.
         * @param message   Message to display on error.
         */
        isNotExtensible<T>(object: T, message?: string): void;

        /**
         * Asserts that object is not extensible.
         *
         * @type T   Type of object
         * @param object   Actual value.
         * @param message   Message to display on error.
         */
        notExtensible<T>(object: T, message?: string): void;

        /**
         * Asserts that object is sealed (can have new properties added to it
         * and its existing properties cannot be removed).
         *
         * @type T   Type of object
         * @param object   Actual value.
         * @param message   Message to display on error.
         */
        isSealed<T>(object: T, message?: string): void;

        /**
         * Asserts that object is sealed (can have new properties added to it
         * and its existing properties cannot be removed).
         *
         * @type T   Type of object
         * @param object   Actual value.
         * @param message   Message to display on error.
         */
        sealed<T>(object: T, message?: string): void;

        /**
         * Asserts that object is not sealed.
         *
         * @type T   Type of object
         * @param object   Actual value.
         * @param message   Message to display on error.
         */
        isNotSealed<T>(object: T, message?: string): void;

        /**
         * Asserts that object is not sealed.
         *
         * @type T   Type of object
         * @param object   Actual value.
         * @param message   Message to display on error.
         */
        notSealed<T>(object: T, message?: string): void;

        /**
         * Asserts that object is frozen (cannot have new properties added to it
         * and its existing properties cannot be removed).
         *
         * @type T   Type of object
         * @param object   Actual value.
         * @param message   Message to display on error.
         */
        isFrozen<T>(object: T, message?: string): void;

        /**
         * Asserts that object is frozen (cannot have new properties added to it
         * and its existing properties cannot be removed).
         *
         * @type T   Type of object
         * @param object   Actual value.
         * @param message   Message to display on error.
         */
        frozen<T>(object: T, message?: string): void;

        /**
         * Asserts that object is not frozen (cannot have new properties added to it
         * and its existing properties cannot be removed).
         *
         * @type T   Type of object
         * @param object   Actual value.
         * @param message   Message to display on error.
         */
        isNotFrozen<T>(object: T, message?: string): void;

        /**
         * Asserts that object is not frozen (cannot have new properties added to it
         * and its existing properties cannot be removed).
         *
         * @type T   Type of object
         * @param object   Actual value.
         * @param message   Message to display on error.
         */
        notFrozen<T>(object: T, message?: string): void;

        /**
         * Asserts that the target does not contain any values. For arrays and
         * strings, it checks the length property. For Map and Set instances, it
         * checks the size property. For non-function objects, it gets the count
         * of own enumerable string keys.
         *
         * @type T   Type of object
         * @param object   Actual value.
         * @param message   Message to display on error.
         */
        isEmpty<T>(object: T, message?: string): void;

        /**
         * Asserts that the target contains values. For arrays and strings, it checks
         * the length property. For Map and Set instances, it checks the size property.
         * For non-function objects, it gets the count of own enumerable string keys.
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param message    Message to display on error.
         */
        isNotEmpty<T>(object: T, message?: string): void;

        /**
         * Asserts that `object` has at least one of the `keys` provided.
         * You can also provide a single object instead of a `keys` array and its keys
         * will be used as the expected set of keys.
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param keys   Keys to check
         * @param message    Message to display on error.
         */
        hasAnyKeys<T>(object: T, keys: Array<Object | string> | { [key: string]: any }, message?: string): void;

        /**
         * Asserts that `object` has all and only all of the `keys` provided.
         * You can also provide a single object instead of a `keys` array and its keys
         * will be used as the expected set of keys.
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param keys   Keys to check
         * @param message    Message to display on error.
         */
        hasAllKeys<T>(object: T, keys: Array<Object | string> | { [key: string]: any }, message?: string): void;

        /**
         * Asserts that `object` has all of the `keys` provided but may have more keys not listed.
         * You can also provide a single object instead of a `keys` array and its keys
         * will be used as the expected set of keys.
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param keys   Keys to check
         * @param message    Message to display on error.
         */
        containsAllKeys<T>(object: T, keys: Array<Object | string> | { [key: string]: any }, message?: string): void;

        /**
         * Asserts that `object` has none of the `keys` provided.
         * You can also provide a single object instead of a `keys` array and its keys
         * will be used as the expected set of keys.
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param keys   Keys to check
         * @param message    Message to display on error.
         */
        doesNotHaveAnyKeys<T>(object: T, keys: Array<Object | string> | { [key: string]: any }, message?: string): void;

        /**
         * Asserts that `object` does not have at least one of the `keys` provided.
         * You can also provide a single object instead of a `keys` array and its keys
         * will be used as the expected set of keys.
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param keys   Keys to check
         * @param message    Message to display on error.
         */
        doesNotHaveAllKeys<T>(object: T, keys: Array<Object | string> | { [key: string]: any }, message?: string): void;

        /**
         * Asserts that `object` has at least one of the `keys` provided.
         * Since Sets and Maps can have objects as keys you can use this assertion to perform
         * a deep comparison.
         * You can also provide a single object instead of a `keys` array and its keys
         * will be used as the expected set of keys.
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param keys   Keys to check
         * @param message    Message to display on error.
         */
        hasAnyDeepKeys<T>(object: T, keys: Array<Object | string> | { [key: string]: any }, message?: string): void;

        /**
         * Asserts that `object` has all and only all of the `keys` provided.
         * Since Sets and Maps can have objects as keys you can use this assertion to perform
         * a deep comparison.
         * You can also provide a single object instead of a `keys` array and its keys
         * will be used as the expected set of keys.
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param keys   Keys to check
         * @param message    Message to display on error.
         */
        hasAllDeepKeys<T>(object: T, keys: Array<Object | string> | { [key: string]: any }, message?: string): void;

        /**
         * Asserts that `object` contains all of the `keys` provided.
         * Since Sets and Maps can have objects as keys you can use this assertion to perform
         * a deep comparison.
         * You can also provide a single object instead of a `keys` array and its keys
         * will be used as the expected set of keys.
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param keys   Keys to check
         * @param message    Message to display on error.
         */
        containsAllDeepKeys<T>(object: T, keys: Array<Object | string> | { [key: string]: any }, message?: string): void;

        /**
         * Asserts that `object` contains all of the `keys` provided.
         * Since Sets and Maps can have objects as keys you can use this assertion to perform
         * a deep comparison.
         * You can also provide a single object instead of a `keys` array and its keys
         * will be used as the expected set of keys.
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param keys   Keys to check
         * @param message    Message to display on error.
         */
        doesNotHaveAnyDeepKeys<T>(object: T, keys: Array<Object | string> | { [key: string]: any }, message?: string): void;

        /**
         * Asserts that `object` contains all of the `keys` provided.
         * Since Sets and Maps can have objects as keys you can use this assertion to perform
         * a deep comparison.
         * You can also provide a single object instead of a `keys` array and its keys
         * will be used as the expected set of keys.
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param keys   Keys to check
         * @param message    Message to display on error.
         */
        doesNotHaveAllDeepKeys<T>(object: T, keys: Array<Object | string> | { [key: string]: any }, message?: string): void;

        /**
         * Asserts that object has a direct or inherited property named by property,
         * which can be a string using dot- and bracket-notation for nested reference.
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param property    Property to test.
         * @param message    Message to display on error.
         */
        nestedProperty<T>(object: T, property: string, message?: string): void;

        /**
         * Asserts that object does not have a property named by property,
         * which can be a string using dot- and bracket-notation for nested reference.
         * The property cannot exist on the object nor anywhere in its prototype chain.
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param property    Property to test.
         * @param message    Message to display on error.
         */
        notNestedProperty<T>(object: T, property: string, message?: string): void;

        /**
         * Asserts that object has a property named by property with value given by value.
         * property can use dot- and bracket-notation for nested reference. Uses a strict equality check (===).
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param property    Property to test.
         * @param value    Value to test.
         * @param message    Message to display on error.
         */
        nestedPropertyVal<T>(object: T, property: string, value: any, message?: string): void;

        /**
         * Asserts that object does not have a property named by property with value given by value.
         * property can use dot- and bracket-notation for nested reference. Uses a strict equality check (===).
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param property    Property to test.
         * @param value    Value to test.
         * @param message    Message to display on error.
         */
        notNestedPropertyVal<T>(object: T, property: string, value: any, message?: string): void;

        /**
         * Asserts that object has a property named by property with a value given by value.
         * property can use dot- and bracket-notation for nested reference. Uses a deep equality check.
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param property    Property to test.
         * @param value    Value to test.
         * @param message    Message to display on error.
         */
        deepNestedPropertyVal<T>(object: T, property: string, value: any, message?: string): void;

        /**
         * Asserts that object does not have a property named by property with value given by value.
         * property can use dot- and bracket-notation for nested reference. Uses a deep equality check.
         *
         * @type T   Type of object.
         * @param object   Object to test.
         * @param property    Property to test.
         * @param value    Value to test.
         * @param message    Message to display on error.
         */
        notDeepNestedPropertyVal<T>(object: T, property: string, value: any, message?: string): void;
    }

    export interface Config {
        /**
         * Default: false
         */
        includeStack: boolean;

        /**
         * Default: true
         */
        showDiff: boolean;

        /**
         * Default: 40
         */
        truncateThreshold: number;
    }

    export class AssertionError {
        constructor(message: string, _props?: any, ssf?: Function);
        name: string;
        message: string;
        showDiff: boolean;
        stack: string;
    }
}

declare const chai: Chai.ChaiStatic;

declare module "chai" {
    export = chai;
}

interface Object {
    should: Chai.Assertion;
}
"""




let [<Literal>] mocha =
    """// Type definitions for mocha 2.2.5
// Project: http://mochajs.org/
// Definitions by: Kazi Manzur Rashid <https://github.com/kazimanzurrashid>, otiai10 <https://github.com/otiai10>, jt000 <https://github.com/jt000>, Vadim Macagon <https://github.com/enlight>
// Definitions: https://github.com/DefinitelyTyped/DefinitelyTyped

interface MochaSetupOptions {
    //milliseconds to wait before considering a test slow
    slow?: number;

    // timeout in milliseconds
    timeout?: number;

    // ui name "bdd", "tdd", "exports" etc
    ui?: string;

    //array of accepted globals
    globals?: any[];

    // reporter instance (function or string), defaults to `mocha.reporters.Spec`
    reporter?: string | ReporterConstructor;

    // bail on the first test failure
    bail?: boolean;

    // ignore global leaks
    ignoreLeaks?: boolean;

    // grep string or regexp to filter tests with
    grep?: any;

    // require modules before running tests
    require?: string[];
}

declare var mocha: Mocha;
declare var describe: Mocha.IContextDefinition;
declare var xdescribe: Mocha.IContextDefinition;
// alias for `describe`
declare var context: Mocha.IContextDefinition;
// alias for `describe`
declare var suite: Mocha.IContextDefinition;
declare var it: Mocha.ITestDefinition;
declare var xit: Mocha.ITestDefinition;
// alias for `it`
declare var test: Mocha.ITestDefinition;
declare var specify: Mocha.ITestDefinition;

// Used with the --delay flag; see https://mochajs.org/#hooks
declare function run(): void;

interface MochaDone {
    (error?: any): any;
}

declare function setup(callback: (this: Mocha.IBeforeAndAfterContext, done: MochaDone) => any): void;
declare function teardown(callback: (this: Mocha.IBeforeAndAfterContext, done: MochaDone) => any): void;
declare function suiteSetup(callback: (this: Mocha.IHookCallbackContext, done: MochaDone) => any): void;
declare function suiteTeardown(callback: (this: Mocha.IHookCallbackContext, done: MochaDone) => any): void;
declare function before(callback: (this: Mocha.IHookCallbackContext, done: MochaDone) => any): void;
declare function before(description: string, callback: (this: Mocha.IHookCallbackContext, done: MochaDone) => any): void;
declare function after(callback: (this: Mocha.IHookCallbackContext, done: MochaDone) => any): void;
declare function after(description: string, callback: (this: Mocha.IHookCallbackContext, done: MochaDone) => any): void;
declare function beforeEach(callback: (this: Mocha.IBeforeAndAfterContext, done: MochaDone) => any): void;
declare function beforeEach(description: string, callback: (this: Mocha.IBeforeAndAfterContext, done: MochaDone) => any): void;
declare function afterEach(callback: (this: Mocha.IBeforeAndAfterContext, done: MochaDone) => any): void;
declare function afterEach(description: string, callback: (this: Mocha.IBeforeAndAfterContext, done: MochaDone) => any): void;

interface ReporterConstructor {
    new(runner: Mocha.IRunner, options: any): any;
}

declare class Mocha {
    currentTest: Mocha.ITestDefinition;
    constructor(options?: {
        grep?: RegExp;
        ui?: string;
        reporter?: string | ReporterConstructor;
        timeout?: number;
        reporterOptions?: any;
        slow?: number;
        bail?: boolean;
    });

    /** Setup mocha with the given options. */
    setup(options: MochaSetupOptions): Mocha;
    bail(value?: boolean): Mocha;
    addFile(file: string): Mocha;
    /** Sets reporter by name, defaults to "spec". */
    reporter(name: string, reporterOptions?: any): Mocha;
    /** Sets reporter constructor, defaults to mocha.reporters.Spec. */
    reporter(reporter: ReporterConstructor, reporterOptions?: any): Mocha;
    ui(value: string): Mocha;
    grep(value: string): Mocha;
    grep(value: RegExp): Mocha;
    invert(): Mocha;
    ignoreLeaks(value: boolean): Mocha;
    checkLeaks(): Mocha;
    /**
     * Function to allow assertion libraries to throw errors directly into mocha.
     * This is useful when running tests in a browser because window.onerror will
     * only receive the 'message' attribute of the Error.
     */
    throwError(error: Error): void;
    /** Enables growl support. */
    growl(): Mocha;
    globals(value: string): Mocha;
    globals(values: string[]): Mocha;
    useColors(value: boolean): Mocha;
    useInlineDiffs(value: boolean): Mocha;
    timeout(value: number): Mocha;
    slow(value: number): Mocha;
    enableTimeouts(value: boolean): Mocha;
    asyncOnly(value: boolean): Mocha;
    noHighlighting(value: boolean): Mocha;
    /** Runs tests and invokes `onComplete()` when finished. */
    run(onComplete?: (failures: number) => void): Mocha.IRunner;
}

// merge the Mocha class declaration with a module
declare namespace Mocha {
    interface ISuiteCallbackContext {
        timeout(ms: number | string): this;
        retries(n: number): this;
        slow(ms: number): this;
    }

    interface IHookCallbackContext {
        skip(): this;
        timeout(ms: number | string): this;
        [index: string]: any;
    }


    interface ITestCallbackContext {
        skip(): this;
        timeout(ms: number | string): this;
        retries(n: number): this;
        slow(ms: number): this;
        [index: string]: any;
    }

    /** Partial interface for Mocha's `Runnable` class. */
    interface IRunnable {
        title: string;
        fn: Function;
        async: boolean;
        sync: boolean;
        timedOut: boolean;
        timeout(n: number | string): this;
        duration?: number;
    }

    /** Partial interface for Mocha's `Suite` class. */
    interface ISuite {
        parent: ISuite;
        title: string;

        fullTitle(): string;
    }

    /** Partial interface for Mocha's `Test` class. */
    interface ITest extends IRunnable {
        parent: ISuite;
        pending: boolean;
        state: 'failed' | 'passed' | undefined;

        fullTitle(): string;
    }

    interface IBeforeAndAfterContext extends IHookCallbackContext {
        currentTest: ITest;
    }

    interface IStats {
        suites: number;
        tests: number;
        passes: number;
        pending: number;
        failures: number;
        start?: Date;
        end?: Date;
        duration?: Date;
    }

    /** Partial interface for Mocha's `Runner` class. */
    interface IRunner {
        stats?: IStats;
        started: boolean;
        suite: ISuite;
        total: number;
        failures: number;
        grep: (re: string, invert: boolean) => this;
        grepTotal: (suite: ISuite) => number;
        globals: (arr: ReadonlyArray<string>) => this | string[];
        abort: () => this;
        run: (fn?: (failures: number) => void) => this;
    }

    interface IContextDefinition {
        (description: string, callback: (this: ISuiteCallbackContext) => void): ISuite;
        only(description: string, callback: (this: ISuiteCallbackContext) => void): ISuite;
        skip(description: string, callback: (this: ISuiteCallbackContext) => void): void;
        timeout(ms: number | string): void;
    }

    interface ITestDefinition {
        (expectation: string, callback?: (this: ITestCallbackContext, done: MochaDone) => any): ITest;
        only(expectation: string, callback?: (this: ITestCallbackContext, done: MochaDone) => any): ITest;
        skip(expectation: string, callback?: (this: ITestCallbackContext, done: MochaDone) => any): void;
        timeout(ms: number | string): void;
        state: "failed" | "passed";
    }

    export module reporters {
        export class Base {
            stats: IStats;

            constructor(runner: IRunner);
        }

        export class Doc extends Base { }
        export class Dot extends Base { }
        export class HTML extends Base { }
        export class HTMLCov extends Base { }
        export class JSON extends Base { }
        export class JSONCov extends Base { }
        export class JSONStream extends Base { }
        export class Landing extends Base { }
        export class List extends Base { }
        export class Markdown extends Base { }
        export class Min extends Base { }
        export class Nyan extends Base { }
        export class Progress extends Base {
            /**
             * @param options.open String used to indicate the start of the progress bar.
             * @param options.complete String used to indicate a complete test on the progress bar.
             * @param options.incomplete String used to indicate an incomplete test on the progress bar.
             * @param options.close String used to indicate the end of the progress bar.
             */
            constructor(runner: IRunner, options?: {
                open?: string;
                complete?: string;
                incomplete?: string;
                close?: string;
            });
        }
        export class Spec extends Base { }
        export class TAP extends Base { }
        export class XUnit extends Base {
            constructor(runner: IRunner, options?: any);
        }
    }
}

declare module "mocha" {
    export = Mocha;
}
"""

let [<Literal>] typescript =
    """// Type definitions for Node.js 8.10.x
// Project: http://nodejs.org/
// Definitions by: Microsoft TypeScript <http://typescriptlang.org>
//                 DefinitelyTyped <https://github.com/DefinitelyTyped/DefinitelyTyped>
//                 Parambir Singh <https://github.com/parambirs>
//                 Christian Vaagland Tellnes <https://github.com/tellnes>
//                 Wilco Bakker <https://github.com/WilcoBakker>
//                 Nicolas Voigt <https://github.com/octo-sniffle>
//                 Chigozirim C. <https://github.com/smac89>
//                 Flarna <https://github.com/Flarna>
//                 Mariusz Wiktorczyk <https://github.com/mwiktorczyk>
//                 wwwy3y3 <https://github.com/wwwy3y3>
//                 Deividas Bakanas <https://github.com/DeividasBakanas>
//                 Kelvin Jin <https://github.com/kjin>
//                 Alvis HT Tang <https://github.com/alvis>
//                 Sebastian Silbermann <https://github.com/eps1lon>
//                 Hannes Magnusson <https://github.com/Hannes-Magnusson-CK>
//                 Alberto Schiabel <https://github.com/jkomyno>
//                 Huw <https://github.com/hoo29>
//                 Nicolas Even <https://github.com/n-e>
//                 Bruno Scheufler <https://github.com/brunoscheufler>
//                 Hoàng Văn Khải <https://github.com/KSXGitHub>
//                 Lishude <https://github.com/islishude>
//                 Andrew Makarov <https://github.com/r3nya>
// Definitions: https://github.com/DefinitelyTyped/DefinitelyTyped
// TypeScript Version: 2.1

/** inspector module types */
/// <reference path="./inspector.d.ts" />

// This needs to be global to avoid TS2403 in case lib.dom.d.ts is present in the same build
interface Console {
    Console: NodeJS.ConsoleConstructor;
    assert(value: any, message?: string, ...optionalParams: any[]): void;
    dir(obj: any, options?: NodeJS.InspectOptions): void;
    debug(message?: any, ...optionalParams: any[]): void;
    error(message?: any, ...optionalParams: any[]): void;
    info(message?: any, ...optionalParams: any[]): void;
    log(message?: any, ...optionalParams: any[]): void;
    time(label: string): void;
    timeEnd(label: string): void;
    trace(message?: any, ...optionalParams: any[]): void;
    warn(message?: any, ...optionalParams: any[]): void;
}

interface Error {
    stack?: string;
}

// Declare "static" methods in Error
interface ErrorConstructor {
    /** Create .stack property on a target object */
    captureStackTrace(targetObject: Object, constructorOpt?: Function): void;

    /**
     * Optional override for formatting stack traces
     *
     * @see https://github.com/v8/v8/wiki/Stack%20Trace%20API#customizing-stack-traces
     */
    prepareStackTrace?: (err: Error, stackTraces: NodeJS.CallSite[]) => any;

    stackTraceLimit: number;
}

// compat for TypeScript 1.8
// if you use with --target es3 or --target es5 and use below definitions,
// use the lib.es6.d.ts that is bundled with TypeScript 1.8.
interface MapConstructor { }
interface WeakMapConstructor { }
interface SetConstructor { }
interface WeakSetConstructor { }

// Forward-declare needed types from lib.es2015.d.ts (in case users are using `--lib es5`)
interface Iterable<T> { }
interface Iterator<T> {
    next(value?: any): IteratorResult<T>;
}
interface IteratorResult<T> { }
interface SymbolConstructor {
    readonly iterator: symbol;
}
declare var Symbol: SymbolConstructor;

// Node.js ESNEXT support
interface String {
    /** Removes whitespace from the left end of a string. */
    trimLeft(): string;
    /** Removes whitespace from the right end of a string. */
    trimRight(): string;
}

/************************************************
*                                               *
*                   GLOBAL                      *
*                                               *
************************************************/
declare var process: NodeJS.Process;
declare var global: NodeJS.Global;
declare var console: Console;

declare var __filename: string;
declare var __dirname: string;

declare function setTimeout(callback: (...args: any[]) => void, ms: number, ...args: any[]): NodeJS.Timer;
declare namespace setTimeout {
    export function __promisify__(ms: number): Promise<void>;
    export function __promisify__<T>(ms: number, value: T): Promise<T>;
}
declare function clearTimeout(timeoutId: NodeJS.Timer): void;
declare function setInterval(callback: (...args: any[]) => void, ms: number, ...args: any[]): NodeJS.Timer;
declare function clearInterval(intervalId: NodeJS.Timer): void;
declare function setImmediate(callback: (...args: any[]) => void, ...args: any[]): any;
declare namespace setImmediate {
    export function __promisify__(): Promise<void>;
    export function __promisify__<T>(value: T): Promise<T>;
}
declare function clearImmediate(immediateId: any): void;

// TODO: change to `type NodeRequireFunction = (id: string) => any;` in next mayor version.
interface NodeRequireFunction {
    /* tslint:disable-next-line:callable-types */
    (id: string): any;
}

interface NodeRequire extends NodeRequireFunction {
    resolve: RequireResolve;
    cache: any;
    extensions: NodeExtensions;
    main: NodeModule | undefined;
}

interface RequireResolve {
    (id: string, options?: { paths?: string[]; }): string;
    paths(request: string): string[] | null;
}

interface NodeExtensions {
    '.js': (m: NodeModule, filename: string) => any;
    '.json': (m: NodeModule, filename: string) => any;
    '.node': (m: NodeModule, filename: string) => any;
    [ext: string]: (m: NodeModule, filename: string) => any;
}

declare var require: NodeRequire;

interface NodeModule {
    exports: any;
    require: NodeRequireFunction;
    id: string;
    filename: string;
    loaded: boolean;
    parent: NodeModule | null;
    children: NodeModule[];
    paths: string[];
}

declare var module: NodeModule;

// Same as module.exports
declare var exports: any;
declare var SlowBuffer: {
    new(str: string, encoding?: string): Buffer;
    new(size: number): Buffer;
    new(size: Uint8Array): Buffer;
    new(array: any[]): Buffer;
    prototype: Buffer;
    isBuffer(obj: any): boolean;
    byteLength(string: string, encoding?: string): number;
    concat(list: Buffer[], totalLength?: number): Buffer;
};

// Buffer class
type BufferEncoding = "ascii" | "utf8" | "utf16le" | "ucs2" | "base64" | "latin1" | "binary" | "hex";
interface Buffer extends NodeBuffer { }

/**
 * Raw data is stored in instances of the Buffer class.
 * A Buffer is similar to an array of integers but corresponds to a raw memory allocation outside the V8 heap.  A Buffer cannot be resized.
 * Valid string encodings: 'ascii'|'utf8'|'utf16le'|'ucs2'(alias of 'utf16le')|'base64'|'binary'(deprecated)|'hex'
 */
declare var Buffer: {
    /**
     * Allocates a new buffer containing the given {str}.
     *
     * @param str String to store in buffer.
     * @param encoding encoding to use, optional.  Default is 'utf8'
     */
    new(str: string, encoding?: string): Buffer;
    /**
     * Allocates a new buffer of {size} octets.
     *
     * @param size count of octets to allocate.
     */
    new(size: number): Buffer;
    /**
     * Allocates a new buffer containing the given {array} of octets.
     *
     * @param array The octets to store.
     */
    new(array: Uint8Array): Buffer;
    /**
     * Produces a Buffer backed by the same allocated memory as
     * the given {ArrayBuffer}.
     *
     *
     * @param arrayBuffer The ArrayBuffer with which to share memory.
     */
    new(arrayBuffer: ArrayBuffer): Buffer;
    /**
     * Allocates a new buffer containing the given {array} of octets.
     *
     * @param array The octets to store.
     */
    new(array: any[]): Buffer;
    /**
     * Copies the passed {buffer} data onto a new {Buffer} instance.
     *
     * @param buffer The buffer to copy.
     */
    new(buffer: Buffer): Buffer;
    prototype: Buffer;
    /**
     * When passed a reference to the .buffer property of a TypedArray instance,
     * the newly created Buffer will share the same allocated memory as the TypedArray.
     * The optional {byteOffset} and {length} arguments specify a memory range
     * within the {arrayBuffer} that will be shared by the Buffer.
     *
     * @param arrayBuffer The .buffer property of a TypedArray or a new ArrayBuffer()
     */
    from(arrayBuffer: ArrayBuffer, byteOffset?: number, length?: number): Buffer;
    /**
     * Creates a new Buffer using the passed {data}
     * @param data data to create a new Buffer
     */
    from(data: any[] | string | Buffer | ArrayBuffer /*| TypedArray*/): Buffer;
    /**
     * Creates a new Buffer containing the given JavaScript string {str}.
     * If provided, the {encoding} parameter identifies the character encoding.
     * If not provided, {encoding} defaults to 'utf8'.
     */
    from(str: string, encoding?: string): Buffer;
    /**
     * Returns true if {obj} is a Buffer
     *
     * @param obj object to test.
     */
    isBuffer(obj: any): obj is Buffer;
    /**
     * Returns true if {encoding} is a valid encoding argument.
     * Valid string encodings in Node 0.12: 'ascii'|'utf8'|'utf16le'|'ucs2'(alias of 'utf16le')|'base64'|'binary'(deprecated)|'hex'
     *
     * @param encoding string to test.
     */
    isEncoding(encoding: string): boolean;
    /**
     * Gives the actual byte length of a string. encoding defaults to 'utf8'.
     * This is not the same as String.prototype.length since that returns the number of characters in a string.
     *
     * @param string string to test. (TypedArray is also allowed, but it is only available starting ES2017)
     * @param encoding encoding used to evaluate (defaults to 'utf8')
     */
    byteLength(string: string | Buffer | DataView | ArrayBuffer, encoding?: string): number;
    /**
     * Returns a buffer which is the result of concatenating all the buffers in the list together.
     *
     * If the list has no items, or if the totalLength is 0, then it returns a zero-length buffer.
     * If the list has exactly one item, then the first item of the list is returned.
     * If the list has more than one item, then a new Buffer is created.
     *
     * @param list An array of Buffer objects to concatenate
     * @param totalLength Total length of the buffers when concatenated.
     *   If totalLength is not provided, it is read from the buffers in the list. However, this adds an additional loop to the function, so it is faster to provide the length explicitly.
     */
    concat(list: Buffer[], totalLength?: number): Buffer;
    /**
     * The same as buf1.compare(buf2).
     */
    compare(buf1: Buffer, buf2: Buffer): number;
    /**
     * Allocates a new buffer of {size} octets.
     *
     * @param size count of octets to allocate.
     * @param fill if specified, buffer will be initialized by calling buf.fill(fill).
     *    If parameter is omitted, buffer will be filled with zeros.
     * @param encoding encoding used for call to buf.fill while initalizing
     */
    alloc(size: number, fill?: string | Buffer | number, encoding?: string): Buffer;
    /**
     * Allocates a new buffer of {size} octets, leaving memory not initialized, so the contents
     * of the newly created Buffer are unknown and may contain sensitive data.
     *
     * @param size count of octets to allocate
     */
    allocUnsafe(size: number): Buffer;
    /**
     * Allocates a new non-pooled buffer of {size} octets, leaving memory not initialized, so the contents
     * of the newly created Buffer are unknown and may contain sensitive data.
     *
     * @param size count of octets to allocate
     */
    allocUnsafeSlow(size: number): Buffer;
    /**
     * This is the number of bytes used to determine the size of pre-allocated, internal Buffer instances used for pooling. This value may be modified.
     */
    poolSize: number;
};

/************************************************
*                                               *
*               GLOBAL INTERFACES               *
*                                               *
************************************************/
declare namespace NodeJS {
    export interface InspectOptions {
        showHidden?: boolean;
        depth?: number | null;
        colors?: boolean;
        customInspect?: boolean;
        showProxy?: boolean;
        maxArrayLength?: number | null;
        breakLength?: number;
    }

    export interface ConsoleConstructor {
        prototype: Console;
        new(stdout: WritableStream, stderr?: WritableStream): Console;
    }

    export interface CallSite {
        /**
         * Value of "this"
         */
        getThis(): any;

        /**
         * Type of "this" as a string.
         * This is the name of the function stored in the constructor field of
         * "this", if available.  Otherwise the object's [[Class]] internal
         * property.
         */
        getTypeName(): string | null;

        /**
         * Current function
         */
        getFunction(): Function | undefined;

        /**
         * Name of the current function, typically its name property.
         * If a name property is not available an attempt will be made to try
         * to infer a name from the function's context.
         */
        getFunctionName(): string | null;

        /**
         * Name of the property [of "this" or one of its prototypes] that holds
         * the current function
         */
        getMethodName(): string | null;

        /**
         * Name of the script [if this function was defined in a script]
         */
        getFileName(): string | null;

        /**
         * Current line number [if this function was defined in a script]
         */
        getLineNumber(): number | null;

        /**
         * Current column number [if this function was defined in a script]
         */
        getColumnNumber(): number | null;

        /**
         * A call site object representing the location where eval was called
         * [if this function was created using a call to eval]
         */
        getEvalOrigin(): string | undefined;

        /**
         * Is this a toplevel invocation, that is, is "this" the global object?
         */
        isToplevel(): boolean;

        /**
         * Does this call take place in code defined by a call to eval?
         */
        isEval(): boolean;

        /**
         * Is this call in native V8 code?
         */
        isNative(): boolean;

        /**
         * Is this a constructor call?
         */
        isConstructor(): boolean;
    }

    export interface ErrnoException extends Error {
        errno?: number;
        code?: string;
        path?: string;
        syscall?: string;
        stack?: string;
    }

    export class EventEmitter {
        addListener(event: string | symbol, listener: (...args: any[]) => void): this;
        on(event: string | symbol, listener: (...args: any[]) => void): this;
        once(event: string | symbol, listener: (...args: any[]) => void): this;
        removeListener(event: string | symbol, listener: (...args: any[]) => void): this;
        removeAllListeners(event?: string | symbol): this;
        setMaxListeners(n: number): this;
        getMaxListeners(): number;
        listeners(event: string | symbol): Function[];
        emit(event: string | symbol, ...args: any[]): boolean;
        listenerCount(type: string | symbol): number;
        // Added in Node 6...
        prependListener(event: string | symbol, listener: (...args: any[]) => void): this;
        prependOnceListener(event: string | symbol, listener: (...args: any[]) => void): this;
        eventNames(): Array<string | symbol>;
    }

    export interface ReadableStream extends EventEmitter {
        readable: boolean;
        read(size?: number): string | Buffer;
        setEncoding(encoding: string): this;
        pause(): this;
        resume(): this;
        isPaused(): boolean;
        pipe<T extends WritableStream>(destination: T, options?: { end?: boolean; }): T;
        unpipe<T extends WritableStream>(destination?: T): this;
        unshift(chunk: string): void;
        unshift(chunk: Buffer): void;
        wrap(oldStream: ReadableStream): this;
    }

    export interface WritableStream extends EventEmitter {
        writable: boolean;
        write(buffer: Buffer | string, cb?: Function): boolean;
        write(str: string, encoding?: string, cb?: Function): boolean;
        end(cb?: Function): void;
        end(buffer: Buffer, cb?: Function): void;
        end(str: string, cb?: Function): void;
        end(str: string, encoding?: string, cb?: Function): void;
    }

    export interface ReadWriteStream extends ReadableStream, WritableStream { }

    export interface Events extends EventEmitter { }

    export interface Domain extends Events {
        run(fn: Function): void;
        add(emitter: Events): void;
        remove(emitter: Events): void;
        bind(cb: (err: Error, data: any) => any): any;
        intercept(cb: (data: any) => any): any;
        dispose(): void;

        addListener(event: string, listener: (...args: any[]) => void): this;
        on(event: string, listener: (...args: any[]) => void): this;
        once(event: string, listener: (...args: any[]) => void): this;
        removeListener(event: string, listener: (...args: any[]) => void): this;
        removeAllListeners(event?: string): this;
    }

    export interface MemoryUsage {
        rss: number;
        heapTotal: number;
        heapUsed: number;
        external: number;
    }

    export interface CpuUsage {
        user: number;
        system: number;
    }

    export interface ProcessVersions {
        http_parser: string;
        node: string;
        v8: string;
        ares: string;
        uv: string;
        zlib: string;
        modules: string;
        openssl: string;
    }

    type Platform = 'aix'
        | 'android'
        | 'darwin'
        | 'freebsd'
        | 'linux'
        | 'openbsd'
        | 'sunos'
        | 'win32'
        | 'cygwin';

    type Signals =
        "SIGABRT" | "SIGALRM" | "SIGBUS" | "SIGCHLD" | "SIGCONT" | "SIGFPE" | "SIGHUP" | "SIGILL" | "SIGINT" | "SIGIO" |
        "SIGIOT" | "SIGKILL" | "SIGPIPE" | "SIGPOLL" | "SIGPROF" | "SIGPWR" | "SIGQUIT" | "SIGSEGV" | "SIGSTKFLT" |
        "SIGSTOP" | "SIGSYS" | "SIGTERM" | "SIGTRAP" | "SIGTSTP" | "SIGTTIN" | "SIGTTOU" | "SIGUNUSED" | "SIGURG" |
        "SIGUSR1" | "SIGUSR2" | "SIGVTALRM" | "SIGWINCH" | "SIGXCPU" | "SIGXFSZ" | "SIGBREAK" | "SIGLOST" | "SIGINFO";

    type BeforeExitListener = (code: number) => void;
    type DisconnectListener = () => void;
    type ExitListener = (code: number) => void;
    type RejectionHandledListener = (promise: Promise<any>) => void;
    type UncaughtExceptionListener = (error: Error) => void;
    type UnhandledRejectionListener = (reason: any, promise: Promise<any>) => void;
    type WarningListener = (warning: Error) => void;
    type MessageListener = (message: any, sendHandle: any) => void;
    type SignalsListener = () => void;
    type NewListenerListener = (type: string | symbol, listener: (...args: any[]) => void) => void;
    type RemoveListenerListener = (type: string | symbol, listener: (...args: any[]) => void) => void;

    export interface Socket extends ReadWriteStream {
        isTTY?: true;
    }

    export interface ProcessEnv {
        [key: string]: string | undefined;
    }

    export interface WriteStream extends Socket {
        readonly writableHighWaterMark: number;
        columns?: number;
        rows?: number;
        _write(chunk: any, encoding: string, callback: Function): void;
        _destroy(err: Error, callback: Function): void;
        _final(callback: Function): void;
        setDefaultEncoding(encoding: string): this;
        cork(): void;
        uncork(): void;
        destroy(error?: Error): void;
    }
    export interface ReadStream extends Socket {
        readonly readableHighWaterMark: number;
        isRaw?: boolean;
        setRawMode?(mode: boolean): void;
        _read(size: number): void;
        _destroy(err: Error, callback: Function): void;
        push(chunk: any, encoding?: string): boolean;
        destroy(error?: Error): void;
    }

    export interface Process extends EventEmitter {
        stdout: WriteStream;
        stderr: WriteStream;
        stdin: ReadStream;
        openStdin(): Socket;
        argv: string[];
        argv0: string;
        execArgv: string[];
        execPath: string;
        abort(): void;
        chdir(directory: string): void;
        cwd(): string;
        debugPort: number;
        emitWarning(warning: string | Error, name?: string, ctor?: Function): void;
        env: ProcessEnv;
        exit(code?: number): never;
        exitCode: number;
        getgid(): number;
        setgid(id: number | string): void;
        getuid(): number;
        setuid(id: number | string): void;
        geteuid(): number;
        seteuid(id: number | string): void;
        getegid(): number;
        setegid(id: number | string): void;
        getgroups(): number[];
        setgroups(groups: Array<string | number>): void;
        version: string;
        versions: ProcessVersions;
        config: {
            target_defaults: {
                cflags: any[];
                default_configuration: string;
                defines: string[];
                include_dirs: string[];
                libraries: string[];
            };
            variables: {
                clang: number;
                host_arch: string;
                node_install_npm: boolean;
                node_install_waf: boolean;
                node_prefix: string;
                node_shared_openssl: boolean;
                node_shared_v8: boolean;
                node_shared_zlib: boolean;
                node_use_dtrace: boolean;
                node_use_etw: boolean;
                node_use_openssl: boolean;
                target_arch: string;
                v8_no_strict_aliasing: number;
                v8_use_snapshot: boolean;
                visibility: string;
            };
        };
        kill(pid: number, signal?: string | number): void;
        pid: number;
        title: string;
        arch: string;
        platform: Platform;
        mainModule?: NodeModule;
        memoryUsage(): MemoryUsage;
        cpuUsage(previousValue?: CpuUsage): CpuUsage;
        nextTick(callback: Function, ...args: any[]): void;
        umask(mask?: number): number;
        uptime(): number;
        hrtime(time?: [number, number]): [number, number];
        domain: Domain;

        // Worker
        send?(message: any, sendHandle?: any): void;
        disconnect(): void;
        connected: boolean;

        /**
         * EventEmitter
         *   1. beforeExit
         *   2. disconnect
         *   3. exit
         *   4. message
         *   5. rejectionHandled
         *   6. uncaughtException
         *   7. unhandledRejection
         *   8. warning
         *   9. message
         *  10. <All OS Signals>
         *  11. newListener/removeListener inherited from EventEmitter
         */
        addListener(event: "beforeExit", listener: BeforeExitListener): this;
        addListener(event: "disconnect", listener: DisconnectListener): this;
        addListener(event: "exit", listener: ExitListener): this;
        addListener(event: "rejectionHandled", listener: RejectionHandledListener): this;
        addListener(event: "uncaughtException", listener: UncaughtExceptionListener): this;
        addListener(event: "unhandledRejection", listener: UnhandledRejectionListener): this;
        addListener(event: "warning", listener: WarningListener): this;
        addListener(event: "message", listener: MessageListener): this;
        addListener(event: Signals, listener: SignalsListener): this;
        addListener(event: "newListener", listener: NewListenerListener): this;
        addListener(event: "removeListener", listener: RemoveListenerListener): this;

        emit(event: "beforeExit", code: number): boolean;
        emit(event: "disconnect"): boolean;
        emit(event: "exit", code: number): boolean;
        emit(event: "rejectionHandled", promise: Promise<any>): boolean;
        emit(event: "uncaughtException", error: Error): boolean;
        emit(event: "unhandledRejection", reason: any, promise: Promise<any>): boolean;
        emit(event: "warning", warning: Error): boolean;
        emit(event: "message", message: any, sendHandle: any): this;
        emit(event: Signals): boolean;
        emit(event: "newListener", eventName: string | symbol, listener: (...args: any[]) => void): this;
        emit(event: "removeListener", eventName: string, listener: (...args: any[]) => void): this;

        on(event: "beforeExit", listener: BeforeExitListener): this;
        on(event: "disconnect", listener: DisconnectListener): this;
        on(event: "exit", listener: ExitListener): this;
        on(event: "rejectionHandled", listener: RejectionHandledListener): this;
        on(event: "uncaughtException", listener: UncaughtExceptionListener): this;
        on(event: "unhandledRejection", listener: UnhandledRejectionListener): this;
        on(event: "warning", listener: WarningListener): this;
        on(event: "message", listener: MessageListener): this;
        on(event: Signals, listener: SignalsListener): this;
        on(event: "newListener", listener: NewListenerListener): this;
        on(event: "removeListener", listener: RemoveListenerListener): this;

        once(event: "beforeExit", listener: BeforeExitListener): this;
        once(event: "disconnect", listener: DisconnectListener): this;
        once(event: "exit", listener: ExitListener): this;
        once(event: "rejectionHandled", listener: RejectionHandledListener): this;
        once(event: "uncaughtException", listener: UncaughtExceptionListener): this;
        once(event: "unhandledRejection", listener: UnhandledRejectionListener): this;
        once(event: "warning", listener: WarningListener): this;
        once(event: "message", listener: MessageListener): this;
        once(event: Signals, listener: SignalsListener): this;
        once(event: "newListener", listener: NewListenerListener): this;
        once(event: "removeListener", listener: RemoveListenerListener): this;

        prependListener(event: "beforeExit", listener: BeforeExitListener): this;
        prependListener(event: "disconnect", listener: DisconnectListener): this;
        prependListener(event: "exit", listener: ExitListener): this;
        prependListener(event: "rejectionHandled", listener: RejectionHandledListener): this;
        prependListener(event: "uncaughtException", listener: UncaughtExceptionListener): this;
        prependListener(event: "unhandledRejection", listener: UnhandledRejectionListener): this;
        prependListener(event: "warning", listener: WarningListener): this;
        prependListener(event: "message", listener: MessageListener): this;
        prependListener(event: Signals, listener: SignalsListener): this;
        prependListener(event: "newListener", listener: NewListenerListener): this;
        prependListener(event: "removeListener", listener: RemoveListenerListener): this;

        prependOnceListener(event: "beforeExit", listener: BeforeExitListener): this;
        prependOnceListener(event: "disconnect", listener: DisconnectListener): this;
        prependOnceListener(event: "exit", listener: ExitListener): this;
        prependOnceListener(event: "rejectionHandled", listener: RejectionHandledListener): this;
        prependOnceListener(event: "uncaughtException", listener: UncaughtExceptionListener): this;
        prependOnceListener(event: "unhandledRejection", listener: UnhandledRejectionListener): this;
        prependOnceListener(event: "warning", listener: WarningListener): this;
        prependOnceListener(event: "message", listener: MessageListener): this;
        prependOnceListener(event: Signals, listener: SignalsListener): this;
        prependOnceListener(event: "newListener", listener: NewListenerListener): this;
        prependOnceListener(event: "removeListener", listener: RemoveListenerListener): this;

        listeners(event: "beforeExit"): BeforeExitListener[];
        listeners(event: "disconnect"): DisconnectListener[];
        listeners(event: "exit"): ExitListener[];
        listeners(event: "rejectionHandled"): RejectionHandledListener[];
        listeners(event: "uncaughtException"): UncaughtExceptionListener[];
        listeners(event: "unhandledRejection"): UnhandledRejectionListener[];
        listeners(event: "warning"): WarningListener[];
        listeners(event: "message"): MessageListener[];
        listeners(event: Signals): SignalsListener[];
        listeners(event: "newListener"): NewListenerListener[];
        listeners(event: "removeListener"): RemoveListenerListener[];
    }

    export interface Global {
        Array: typeof Array;
        ArrayBuffer: typeof ArrayBuffer;
        Boolean: typeof Boolean;
        Buffer: typeof Buffer;
        DataView: typeof DataView;
        Date: typeof Date;
        Error: typeof Error;
        EvalError: typeof EvalError;
        Float32Array: typeof Float32Array;
        Float64Array: typeof Float64Array;
        Function: typeof Function;
        GLOBAL: Global;
        Infinity: typeof Infinity;
        Int16Array: typeof Int16Array;
        Int32Array: typeof Int32Array;
        Int8Array: typeof Int8Array;
        Intl: typeof Intl;
        JSON: typeof JSON;
        Map: MapConstructor;
        Math: typeof Math;
        NaN: typeof NaN;
        Number: typeof Number;
        Object: typeof Object;
        Promise: Function;
        RangeError: typeof RangeError;
        ReferenceError: typeof ReferenceError;
        RegExp: typeof RegExp;
        Set: SetConstructor;
        String: typeof String;
        Symbol: Function;
        SyntaxError: typeof SyntaxError;
        TypeError: typeof TypeError;
        URIError: typeof URIError;
        Uint16Array: typeof Uint16Array;
        Uint32Array: typeof Uint32Array;
        Uint8Array: typeof Uint8Array;
        Uint8ClampedArray: Function;
        WeakMap: WeakMapConstructor;
        WeakSet: WeakSetConstructor;
        clearImmediate: (immediateId: any) => void;
        clearInterval: (intervalId: NodeJS.Timer) => void;
        clearTimeout: (timeoutId: NodeJS.Timer) => void;
        console: typeof console;
        decodeURI: typeof decodeURI;
        decodeURIComponent: typeof decodeURIComponent;
        encodeURI: typeof encodeURI;
        encodeURIComponent: typeof encodeURIComponent;
        escape: (str: string) => string;
        eval: typeof eval;
        global: Global;
        isFinite: typeof isFinite;
        isNaN: typeof isNaN;
        parseFloat: typeof parseFloat;
        parseInt: typeof parseInt;
        process: Process;
        root: Global;
        setImmediate: (callback: (...args: any[]) => void, ...args: any[]) => any;
        setInterval: (callback: (...args: any[]) => void, ms: number, ...args: any[]) => NodeJS.Timer;
        setTimeout: (callback: (...args: any[]) => void, ms: number, ...args: any[]) => NodeJS.Timer;
        undefined: typeof undefined;
        unescape: (str: string) => string;
        gc: () => void;
        v8debug?: any;
    }

    export interface Timer {
        ref(): void;
        unref(): void;
    }

    class Module {
        static runMain(): void;
        static wrap(code: string): string;
        static builtinModules: string[];

        static Module: typeof Module;

        exports: any;
        require: NodeRequireFunction;
        id: string;
        filename: string;
        loaded: boolean;
        parent: Module | null;
        children: Module[];
        paths: string[];

        constructor(id: string, parent?: Module);
    }
}

interface IterableIterator<T> { }

/**
 * @deprecated
 */
interface NodeBuffer extends Uint8Array {
    write(string: string, offset?: number, length?: number, encoding?: string): number;
    toString(encoding?: string, start?: number, end?: number): string;
    toJSON(): { type: 'Buffer', data: any[] };
    equals(otherBuffer: Buffer): boolean;
    compare(otherBuffer: Buffer, targetStart?: number, targetEnd?: number, sourceStart?: number, sourceEnd?: number): number;
    copy(targetBuffer: Buffer, targetStart?: number, sourceStart?: number, sourceEnd?: number): number;
    slice(start?: number, end?: number): Buffer;
    writeUIntLE(value: number, offset: number, byteLength: number, noAssert?: boolean): number;
    writeUIntBE(value: number, offset: number, byteLength: number, noAssert?: boolean): number;
    writeIntLE(value: number, offset: number, byteLength: number, noAssert?: boolean): number;
    writeIntBE(value: number, offset: number, byteLength: number, noAssert?: boolean): number;
    readUIntLE(offset: number, byteLength: number, noAssert?: boolean): number;
    readUIntBE(offset: number, byteLength: number, noAssert?: boolean): number;
    readIntLE(offset: number, byteLength: number, noAssert?: boolean): number;
    readIntBE(offset: number, byteLength: number, noAssert?: boolean): number;
    readUInt8(offset: number, noAssert?: boolean): number;
    readUInt16LE(offset: number, noAssert?: boolean): number;
    readUInt16BE(offset: number, noAssert?: boolean): number;
    readUInt32LE(offset: number, noAssert?: boolean): number;
    readUInt32BE(offset: number, noAssert?: boolean): number;
    readInt8(offset: number, noAssert?: boolean): number;
    readInt16LE(offset: number, noAssert?: boolean): number;
    readInt16BE(offset: number, noAssert?: boolean): number;
    readInt32LE(offset: number, noAssert?: boolean): number;
    readInt32BE(offset: number, noAssert?: boolean): number;
    readFloatLE(offset: number, noAssert?: boolean): number;
    readFloatBE(offset: number, noAssert?: boolean): number;
    readDoubleLE(offset: number, noAssert?: boolean): number;
    readDoubleBE(offset: number, noAssert?: boolean): number;
    swap16(): Buffer;
    swap32(): Buffer;
    swap64(): Buffer;
    writeUInt8(value: number, offset: number, noAssert?: boolean): number;
    writeUInt16LE(value: number, offset: number, noAssert?: boolean): number;
    writeUInt16BE(value: number, offset: number, noAssert?: boolean): number;
    writeUInt32LE(value: number, offset: number, noAssert?: boolean): number;
    writeUInt32BE(value: number, offset: number, noAssert?: boolean): number;
    writeInt8(value: number, offset: number, noAssert?: boolean): number;
    writeInt16LE(value: number, offset: number, noAssert?: boolean): number;
    writeInt16BE(value: number, offset: number, noAssert?: boolean): number;
    writeInt32LE(value: number, offset: number, noAssert?: boolean): number;
    writeInt32BE(value: number, offset: number, noAssert?: boolean): number;
    writeFloatLE(value: number, offset: number, noAssert?: boolean): number;
    writeFloatBE(value: number, offset: number, noAssert?: boolean): number;
    writeDoubleLE(value: number, offset: number, noAssert?: boolean): number;
    writeDoubleBE(value: number, offset: number, noAssert?: boolean): number;
    fill(value: any, offset?: number, end?: number): this;
    indexOf(value: string | number | Buffer, byteOffset?: number, encoding?: string): number;
    lastIndexOf(value: string | number | Buffer, byteOffset?: number, encoding?: string): number;
    entries(): IterableIterator<[number, number]>;
    includes(value: string | number | Buffer, byteOffset?: number, encoding?: string): boolean;
    keys(): IterableIterator<number>;
    values(): IterableIterator<number>;
}

/************************************************
*                                               *
*                   MODULES                     *
*                                               *
************************************************/
declare module "buffer" {
    export var INSPECT_MAX_BYTES: number;
    var BuffType: typeof Buffer;
    var SlowBuffType: typeof SlowBuffer;
    export { BuffType as Buffer, SlowBuffType as SlowBuffer };
}

declare module "querystring" {
    export interface StringifyOptions {
        encodeURIComponent?: Function;
    }

    export interface ParseOptions {
        maxKeys?: number;
        decodeURIComponent?: Function;
    }

    interface ParsedUrlQuery { [key: string]: string | string[] | undefined; }

    export function stringify<T>(obj: T, sep?: string, eq?: string, options?: StringifyOptions): string;
    export function parse(str: string, sep?: string, eq?: string, options?: ParseOptions): ParsedUrlQuery;
    export function parse<T extends {}>(str: string, sep?: string, eq?: string, options?: ParseOptions): T;
    export function escape(str: string): string;
    export function unescape(str: string): string;
}

declare module "events" {
    class internal extends NodeJS.EventEmitter { }

    namespace internal {
        export class EventEmitter extends internal {
            static listenerCount(emitter: EventEmitter, event: string | symbol): number; // deprecated
            static defaultMaxListeners: number;

            addListener(event: string | symbol, listener: (...args: any[]) => void): this;
            on(event: string | symbol, listener: (...args: any[]) => void): this;
            once(event: string | symbol, listener: (...args: any[]) => void): this;
            prependListener(event: string | symbol, listener: (...args: any[]) => void): this;
            prependOnceListener(event: string | symbol, listener: (...args: any[]) => void): this;
            removeListener(event: string | symbol, listener: (...args: any[]) => void): this;
            removeAllListeners(event?: string | symbol): this;
            setMaxListeners(n: number): this;
            getMaxListeners(): number;
            listeners(event: string | symbol): Function[];
            emit(event: string | symbol, ...args: any[]): boolean;
            eventNames(): Array<string | symbol>;
            listenerCount(type: string | symbol): number;
        }
    }

    export = internal;
}

declare module "http" {
    import * as events from "events";
    import * as net from "net";
    import * as stream from "stream";
    import { URL } from "url";

    // incoming headers will never contain number
    export interface IncomingHttpHeaders {
        'accept'?: string;
        'access-control-allow-origin'?: string;
        'access-control-allow-credentials'?: string;
        'access-control-expose-headers'?: string;
        'access-control-max-age'?: string;
        'access-control-allow-methods'?: string;
        'access-control-allow-headers'?: string;
        'accept-patch'?: string;
        'accept-ranges'?: string;
        'authorization'?: string;
        'age'?: string;
        'allow'?: string;
        'alt-svc'?: string;
        'cache-control'?: string;
        'connection'?: string;
        'content-disposition'?: string;
        'content-encoding'?: string;
        'content-language'?: string;
        'content-length'?: string;
        'content-location'?: string;
        'content-range'?: string;
        'content-type'?: string;
        'date'?: string;
        'expires'?: string;
        'host'?: string;
        'last-modified'?: string;
        'location'?: string;
        'pragma'?: string;
        'proxy-authenticate'?: string;
        'public-key-pins'?: string;
        'retry-after'?: string;
        'set-cookie'?: string[];
        'strict-transport-security'?: string;
        'trailer'?: string;
        'transfer-encoding'?: string;
        'tk'?: string;
        'upgrade'?: string;
        'vary'?: string;
        'via'?: string;
        'warning'?: string;
        'www-authenticate'?: string;
        [header: string]: string | string[] | undefined;
    }

    // outgoing headers allows numbers (as they are converted internally to strings)
    export interface OutgoingHttpHeaders {
        [header: string]: number | string | string[] | undefined;
    }

    export interface ClientRequestArgs {
        protocol?: string;
        host?: string;
        hostname?: string;
        family?: number;
        port?: number | string;
        defaultPort?: number | string;
        localAddress?: string;
        socketPath?: string;
        method?: string;
        path?: string;
        headers?: OutgoingHttpHeaders;
        auth?: string;
        agent?: Agent | boolean;
        _defaultAgent?: Agent;
        timeout?: number;
        // https://github.com/nodejs/node/blob/master/lib/_http_client.js#L278
        createConnection?: (options: ClientRequestArgs, oncreate: (err: Error, socket: net.Socket) => void) => net.Socket;
    }

    export class Server extends net.Server {
        constructor(requestListener?: (req: IncomingMessage, res: ServerResponse) => void);

        setTimeout(msecs?: number, callback?: () => void): this;
        setTimeout(callback: () => void): this;
        maxHeadersCount: number;
        timeout: number;
        keepAliveTimeout: number;
    }
    /**
     * @deprecated Use IncomingMessage
     */
    export class ServerRequest extends IncomingMessage {
        connection: net.Socket;
    }

    // https://github.com/nodejs/node/blob/master/lib/_http_outgoing.js
    export class OutgoingMessage extends stream.Writable {
        upgrading: boolean;
        chunkedEncoding: boolean;
        shouldKeepAlive: boolean;
        useChunkedEncodingByDefault: boolean;
        sendDate: boolean;
        finished: boolean;
        headersSent: boolean;
        connection: net.Socket;

        constructor();

        setTimeout(msecs: number, callback?: () => void): this;
        destroy(error: Error): void;
        setHeader(name: string, value: number | string | string[]): void;
        getHeader(name: string): number | string | string[] | undefined;
        getHeaders(): OutgoingHttpHeaders;
        getHeaderNames(): string[];
        hasHeader(name: string): boolean;
        removeHeader(name: string): void;
        addTrailers(headers: OutgoingHttpHeaders | Array<[string, string]>): void;
        flushHeaders(): void;
    }

    // https://github.com/nodejs/node/blob/master/lib/_http_server.js#L108-L256
    export class ServerResponse extends OutgoingMessage {
        statusCode: number;
        statusMessage: string;

        constructor(req: IncomingMessage);

        assignSocket(socket: net.Socket): void;
        detachSocket(socket: net.Socket): void;
        // https://github.com/nodejs/node/blob/master/test/parallel/test-http-write-callbacks.js#L53
        // no args in writeContinue callback
        writeContinue(callback?: () => void): void;
        writeHead(statusCode: number, reasonPhrase?: string, headers?: OutgoingHttpHeaders): void;
        writeHead(statusCode: number, headers?: OutgoingHttpHeaders): void;
    }

    // https://github.com/nodejs/node/blob/master/lib/_http_client.js#L77
    export class ClientRequest extends OutgoingMessage {
        connection: net.Socket;
        socket: net.Socket;
        aborted: number;

        constructor(url: string | URL | ClientRequestArgs, cb?: (res: IncomingMessage) => void);

        abort(): void;
        onSocket(socket: net.Socket): void;
        setTimeout(timeout: number, callback?: () => void): this;
        setNoDelay(noDelay?: boolean): void;
        setSocketKeepAlive(enable?: boolean, initialDelay?: number): void;
    }

    export class IncomingMessage extends stream.Readable {
        constructor(socket: net.Socket);

        httpVersion: string;
        httpVersionMajor: number;
        httpVersionMinor: number;
        connection: net.Socket;
        headers: IncomingHttpHeaders;
        rawHeaders: string[];
        trailers: { [key: string]: string | undefined };
        rawTrailers: string[];
        setTimeout(msecs: number, callback: () => void): this;
        /**
         * Only valid for request obtained from http.Server.
         */
        method?: string;
        /**
         * Only valid for request obtained from http.Server.
         */
        url?: string;
        /**
         * Only valid for response obtained from http.ClientRequest.
         */
        statusCode?: number;
        /**
         * Only valid for response obtained from http.ClientRequest.
         */
        statusMessage?: string;
        socket: net.Socket;
        destroy(error?: Error): void;
    }

    /**
     * @deprecated Use IncomingMessage
     */
    export class ClientResponse extends IncomingMessage { }

    export interface AgentOptions {
        /**
         * Keep sockets around in a pool to be used by other requests in the future. Default = false
         */
        keepAlive?: boolean;
        /**
         * When using HTTP KeepAlive, how often to send TCP KeepAlive packets over sockets being kept alive. Default = 1000.
         * Only relevant if keepAlive is set to true.
         */
        keepAliveMsecs?: number;
        /**
         * Maximum number of sockets to allow per host. Default for Node 0.10 is 5, default for Node 0.12 is Infinity
         */
        maxSockets?: number;
        /**
         * Maximum number of sockets to leave open in a free state. Only relevant if keepAlive is set to true. Default = 256.
         */
        maxFreeSockets?: number;
    }

    export class Agent {
        maxFreeSockets: number;
        maxSockets: number;
        sockets: any;
        requests: any;

        constructor(opts?: AgentOptions);

        /**
         * Destroy any sockets that are currently in use by the agent.
         * It is usually not necessary to do this. However, if you are using an agent with KeepAlive enabled,
         * then it is best to explicitly shut down the agent when you know that it will no longer be used. Otherwise,
         * sockets may hang open for quite a long time before the server terminates them.
         */
        destroy(): void;
    }

    export var METHODS: string[];

    export var STATUS_CODES: {
        [errorCode: number]: string | undefined;
        [errorCode: string]: string | undefined;
    };

    export function createServer(requestListener?: (request: IncomingMessage, response: ServerResponse) => void): Server;
    export function createClient(port?: number, host?: string): any;

    // although RequestOptions are passed as ClientRequestArgs to ClientRequest directly,
    // create interface RequestOptions would make the naming more clear to developers
    export interface RequestOptions extends ClientRequestArgs { }
    export function request(options: RequestOptions | string | URL, callback?: (res: IncomingMessage) => void): ClientRequest;
    export function get(options: RequestOptions | string | URL, callback?: (res: IncomingMessage) => void): ClientRequest;
    export var globalAgent: Agent;
}

declare module "cluster" {
    import * as child from "child_process";
    import * as events from "events";
    import * as net from "net";

    // interfaces
    export interface ClusterSettings {
        execArgv?: string[]; // default: process.execArgv
        exec?: string;
        args?: string[];
        silent?: boolean;
        stdio?: any[];
        uid?: number;
        gid?: number;
        inspectPort?: number | (() => number);
    }

    export interface Address {
        address: string;
        port: number;
        addressType: number | "udp4" | "udp6";  // 4, 6, -1, "udp4", "udp6"
    }

    export class Worker extends events.EventEmitter {
        id: number;
        process: child.ChildProcess;
        suicide: boolean;
        send(message: any, sendHandle?: any, callback?: (error: Error) => void): boolean;
        kill(signal?: string): void;
        destroy(signal?: string): void;
        disconnect(): void;
        isConnected(): boolean;
        isDead(): boolean;
        exitedAfterDisconnect: boolean;

        /**
         * events.EventEmitter
         *   1. disconnect
         *   2. error
         *   3. exit
         *   4. listening
         *   5. message
         *   6. online
         */
        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "disconnect", listener: () => void): this;
        addListener(event: "error", listener: (error: Error) => void): this;
        addListener(event: "exit", listener: (code: number, signal: string) => void): this;
        addListener(event: "listening", listener: (address: Address) => void): this;
        addListener(event: "message", listener: (message: any, handle: net.Socket | net.Server) => void): this;  // the handle is a net.Socket or net.Server object, or undefined.
        addListener(event: "online", listener: () => void): this;

        emit(event: string | symbol, ...args: any[]): boolean;
        emit(event: "disconnect"): boolean;
        emit(event: "error", error: Error): boolean;
        emit(event: "exit", code: number, signal: string): boolean;
        emit(event: "listening", address: Address): boolean;
        emit(event: "message", message: any, handle: net.Socket | net.Server): boolean;
        emit(event: "online"): boolean;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "disconnect", listener: () => void): this;
        on(event: "error", listener: (error: Error) => void): this;
        on(event: "exit", listener: (code: number, signal: string) => void): this;
        on(event: "listening", listener: (address: Address) => void): this;
        on(event: "message", listener: (message: any, handle: net.Socket | net.Server) => void): this;  // the handle is a net.Socket or net.Server object, or undefined.
        on(event: "online", listener: () => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "disconnect", listener: () => void): this;
        once(event: "error", listener: (error: Error) => void): this;
        once(event: "exit", listener: (code: number, signal: string) => void): this;
        once(event: "listening", listener: (address: Address) => void): this;
        once(event: "message", listener: (message: any, handle: net.Socket | net.Server) => void): this;  // the handle is a net.Socket or net.Server object, or undefined.
        once(event: "online", listener: () => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "disconnect", listener: () => void): this;
        prependListener(event: "error", listener: (error: Error) => void): this;
        prependListener(event: "exit", listener: (code: number, signal: string) => void): this;
        prependListener(event: "listening", listener: (address: Address) => void): this;
        prependListener(event: "message", listener: (message: any, handle: net.Socket | net.Server) => void): this;  // the handle is a net.Socket or net.Server object, or undefined.
        prependListener(event: "online", listener: () => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "disconnect", listener: () => void): this;
        prependOnceListener(event: "error", listener: (error: Error) => void): this;
        prependOnceListener(event: "exit", listener: (code: number, signal: string) => void): this;
        prependOnceListener(event: "listening", listener: (address: Address) => void): this;
        prependOnceListener(event: "message", listener: (message: any, handle: net.Socket | net.Server) => void): this;  // the handle is a net.Socket or net.Server object, or undefined.
        prependOnceListener(event: "online", listener: () => void): this;
    }

    export interface Cluster extends events.EventEmitter {
        Worker: Worker;
        disconnect(callback?: Function): void;
        fork(env?: any): Worker;
        isMaster: boolean;
        isWorker: boolean;
        // TODO: cluster.schedulingPolicy
        settings: ClusterSettings;
        setupMaster(settings?: ClusterSettings): void;
        worker?: Worker;
        workers?: {
            [index: string]: Worker | undefined
        };

        /**
         * events.EventEmitter
         *   1. disconnect
         *   2. exit
         *   3. fork
         *   4. listening
         *   5. message
         *   6. online
         *   7. setup
         */
        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "disconnect", listener: (worker: Worker) => void): this;
        addListener(event: "exit", listener: (worker: Worker, code: number, signal: string) => void): this;
        addListener(event: "fork", listener: (worker: Worker) => void): this;
        addListener(event: "listening", listener: (worker: Worker, address: Address) => void): this;
        addListener(event: "message", listener: (worker: Worker, message: any, handle: net.Socket | net.Server) => void): this;  // the handle is a net.Socket or net.Server object, or undefined.
        addListener(event: "online", listener: (worker: Worker) => void): this;
        addListener(event: "setup", listener: (settings: any) => void): this;

        emit(event: string | symbol, ...args: any[]): boolean;
        emit(event: "disconnect", worker: Worker): boolean;
        emit(event: "exit", worker: Worker, code: number, signal: string): boolean;
        emit(event: "fork", worker: Worker): boolean;
        emit(event: "listening", worker: Worker, address: Address): boolean;
        emit(event: "message", worker: Worker, message: any, handle: net.Socket | net.Server): boolean;
        emit(event: "online", worker: Worker): boolean;
        emit(event: "setup", settings: any): boolean;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "disconnect", listener: (worker: Worker) => void): this;
        on(event: "exit", listener: (worker: Worker, code: number, signal: string) => void): this;
        on(event: "fork", listener: (worker: Worker) => void): this;
        on(event: "listening", listener: (worker: Worker, address: Address) => void): this;
        on(event: "message", listener: (worker: Worker, message: any, handle: net.Socket | net.Server) => void): this;  // the handle is a net.Socket or net.Server object, or undefined.
        on(event: "online", listener: (worker: Worker) => void): this;
        on(event: "setup", listener: (settings: any) => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "disconnect", listener: (worker: Worker) => void): this;
        once(event: "exit", listener: (worker: Worker, code: number, signal: string) => void): this;
        once(event: "fork", listener: (worker: Worker) => void): this;
        once(event: "listening", listener: (worker: Worker, address: Address) => void): this;
        once(event: "message", listener: (worker: Worker, message: any, handle: net.Socket | net.Server) => void): this;  // the handle is a net.Socket or net.Server object, or undefined.
        once(event: "online", listener: (worker: Worker) => void): this;
        once(event: "setup", listener: (settings: any) => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "disconnect", listener: (worker: Worker) => void): this;
        prependListener(event: "exit", listener: (worker: Worker, code: number, signal: string) => void): this;
        prependListener(event: "fork", listener: (worker: Worker) => void): this;
        prependListener(event: "listening", listener: (worker: Worker, address: Address) => void): this;
        prependListener(event: "message", listener: (worker: Worker, message: any, handle: net.Socket | net.Server) => void): this;  // the handle is a net.Socket or net.Server object, or undefined.
        prependListener(event: "online", listener: (worker: Worker) => void): this;
        prependListener(event: "setup", listener: (settings: any) => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "disconnect", listener: (worker: Worker) => void): this;
        prependOnceListener(event: "exit", listener: (worker: Worker, code: number, signal: string) => void): this;
        prependOnceListener(event: "fork", listener: (worker: Worker) => void): this;
        prependOnceListener(event: "listening", listener: (worker: Worker, address: Address) => void): this;
        prependOnceListener(event: "message", listener: (worker: Worker, message: any, handle: net.Socket | net.Server) => void): this;  // the handle is a net.Socket or net.Server object, or undefined.
        prependOnceListener(event: "online", listener: (worker: Worker) => void): this;
        prependOnceListener(event: "setup", listener: (settings: any) => void): this;
    }

    export function disconnect(callback?: Function): void;
    export function fork(env?: any): Worker;
    export var isMaster: boolean;
    export var isWorker: boolean;
    // TODO: cluster.schedulingPolicy
    export var settings: ClusterSettings;
    export function setupMaster(settings?: ClusterSettings): void;
    export var worker: Worker;
    export var workers: {
        [index: string]: Worker | undefined
    };

    /**
     * events.EventEmitter
     *   1. disconnect
     *   2. exit
     *   3. fork
     *   4. listening
     *   5. message
     *   6. online
     *   7. setup
     */
    export function addListener(event: string, listener: (...args: any[]) => void): Cluster;
    export function addListener(event: "disconnect", listener: (worker: Worker) => void): Cluster;
    export function addListener(event: "exit", listener: (worker: Worker, code: number, signal: string) => void): Cluster;
    export function addListener(event: "fork", listener: (worker: Worker) => void): Cluster;
    export function addListener(event: "listening", listener: (worker: Worker, address: Address) => void): Cluster;
    export function addListener(event: "message", listener: (worker: Worker, message: any, handle: net.Socket | net.Server) => void): Cluster;  // the handle is a net.Socket or net.Server object, or undefined.
    export function addListener(event: "online", listener: (worker: Worker) => void): Cluster;
    export function addListener(event: "setup", listener: (settings: any) => void): Cluster;

    export function emit(event: string | symbol, ...args: any[]): boolean;
    export function emit(event: "disconnect", worker: Worker): boolean;
    export function emit(event: "exit", worker: Worker, code: number, signal: string): boolean;
    export function emit(event: "fork", worker: Worker): boolean;
    export function emit(event: "listening", worker: Worker, address: Address): boolean;
    export function emit(event: "message", worker: Worker, message: any, handle: net.Socket | net.Server): boolean;
    export function emit(event: "online", worker: Worker): boolean;
    export function emit(event: "setup", settings: any): boolean;

    export function on(event: string, listener: (...args: any[]) => void): Cluster;
    export function on(event: "disconnect", listener: (worker: Worker) => void): Cluster;
    export function on(event: "exit", listener: (worker: Worker, code: number, signal: string) => void): Cluster;
    export function on(event: "fork", listener: (worker: Worker) => void): Cluster;
    export function on(event: "listening", listener: (worker: Worker, address: Address) => void): Cluster;
    export function on(event: "message", listener: (worker: Worker, message: any, handle: net.Socket | net.Server) => void): Cluster;  // the handle is a net.Socket or net.Server object, or undefined.
    export function on(event: "online", listener: (worker: Worker) => void): Cluster;
    export function on(event: "setup", listener: (settings: any) => void): Cluster;

    export function once(event: string, listener: (...args: any[]) => void): Cluster;
    export function once(event: "disconnect", listener: (worker: Worker) => void): Cluster;
    export function once(event: "exit", listener: (worker: Worker, code: number, signal: string) => void): Cluster;
    export function once(event: "fork", listener: (worker: Worker) => void): Cluster;
    export function once(event: "listening", listener: (worker: Worker, address: Address) => void): Cluster;
    export function once(event: "message", listener: (worker: Worker, message: any, handle: net.Socket | net.Server) => void): Cluster;  // the handle is a net.Socket or net.Server object, or undefined.
    export function once(event: "online", listener: (worker: Worker) => void): Cluster;
    export function once(event: "setup", listener: (settings: any) => void): Cluster;

    export function removeListener(event: string, listener: (...args: any[]) => void): Cluster;
    export function removeAllListeners(event?: string): Cluster;
    export function setMaxListeners(n: number): Cluster;
    export function getMaxListeners(): number;
    export function listeners(event: string): Function[];
    export function listenerCount(type: string): number;

    export function prependListener(event: string, listener: (...args: any[]) => void): Cluster;
    export function prependListener(event: "disconnect", listener: (worker: Worker) => void): Cluster;
    export function prependListener(event: "exit", listener: (worker: Worker, code: number, signal: string) => void): Cluster;
    export function prependListener(event: "fork", listener: (worker: Worker) => void): Cluster;
    export function prependListener(event: "listening", listener: (worker: Worker, address: Address) => void): Cluster;
    export function prependListener(event: "message", listener: (worker: Worker, message: any, handle: net.Socket | net.Server) => void): Cluster;  // the handle is a net.Socket or net.Server object, or undefined.
    export function prependListener(event: "online", listener: (worker: Worker) => void): Cluster;
    export function prependListener(event: "setup", listener: (settings: any) => void): Cluster;

    export function prependOnceListener(event: string, listener: (...args: any[]) => void): Cluster;
    export function prependOnceListener(event: "disconnect", listener: (worker: Worker) => void): Cluster;
    export function prependOnceListener(event: "exit", listener: (worker: Worker, code: number, signal: string) => void): Cluster;
    export function prependOnceListener(event: "fork", listener: (worker: Worker) => void): Cluster;
    export function prependOnceListener(event: "listening", listener: (worker: Worker, address: Address) => void): Cluster;
    export function prependOnceListener(event: "message", listener: (worker: Worker, message: any, handle: net.Socket | net.Server) => void): Cluster;  // the handle is a net.Socket or net.Server object, or undefined.
    export function prependOnceListener(event: "online", listener: (worker: Worker) => void): Cluster;
    export function prependOnceListener(event: "setup", listener: (settings: any) => void): Cluster;

    export function eventNames(): string[];
}

declare module "zlib" {
    import * as stream from "stream";

    export interface ZlibOptions {
        flush?: number; // default: zlib.constants.Z_NO_FLUSH
        finishFlush?: number; // default: zlib.constants.Z_FINISH
        chunkSize?: number; // default: 16*1024
        windowBits?: number;
        level?: number; // compression only
        memLevel?: number; // compression only
        strategy?: number; // compression only
        dictionary?: any; // deflate/inflate only, empty dictionary by default
    }

    export interface Zlib {
        readonly bytesRead: number;
        close(callback?: () => void): void;
        flush(kind?: number | (() => void), callback?: () => void): void;
    }

    export interface ZlibParams {
        params(level: number, strategy: number, callback: () => void): void;
    }

    export interface ZlibReset {
        reset(): void;
    }

    export interface Gzip extends stream.Transform, Zlib { }
    export interface Gunzip extends stream.Transform, Zlib { }
    export interface Deflate extends stream.Transform, Zlib, ZlibReset, ZlibParams { }
    export interface Inflate extends stream.Transform, Zlib, ZlibReset { }
    export interface DeflateRaw extends stream.Transform, Zlib, ZlibReset, ZlibParams { }
    export interface InflateRaw extends stream.Transform, Zlib, ZlibReset { }
    export interface Unzip extends stream.Transform, Zlib { }

    export function createGzip(options?: ZlibOptions): Gzip;
    export function createGunzip(options?: ZlibOptions): Gunzip;
    export function createDeflate(options?: ZlibOptions): Deflate;
    export function createInflate(options?: ZlibOptions): Inflate;
    export function createDeflateRaw(options?: ZlibOptions): DeflateRaw;
    export function createInflateRaw(options?: ZlibOptions): InflateRaw;
    export function createUnzip(options?: ZlibOptions): Unzip;

    type InputType = string | Buffer | DataView /* | TypedArray */;
    export function deflate(buf: InputType, callback: (error: Error | null, result: Buffer) => void): void;
    export function deflate(buf: InputType, options: ZlibOptions, callback: (error: Error | null, result: Buffer) => void): void;
    export function deflateSync(buf: InputType, options?: ZlibOptions): Buffer;
    export function deflateRaw(buf: InputType, callback: (error: Error | null, result: Buffer) => void): void;
    export function deflateRaw(buf: InputType, options: ZlibOptions, callback: (error: Error | null, result: Buffer) => void): void;
    export function deflateRawSync(buf: InputType, options?: ZlibOptions): Buffer;
    export function gzip(buf: InputType, callback: (error: Error | null, result: Buffer) => void): void;
    export function gzip(buf: InputType, options: ZlibOptions, callback: (error: Error | null, result: Buffer) => void): void;
    export function gzipSync(buf: InputType, options?: ZlibOptions): Buffer;
    export function gunzip(buf: InputType, callback: (error: Error | null, result: Buffer) => void): void;
    export function gunzip(buf: InputType, options: ZlibOptions, callback: (error: Error | null, result: Buffer) => void): void;
    export function gunzipSync(buf: InputType, options?: ZlibOptions): Buffer;
    export function inflate(buf: InputType, callback: (error: Error | null, result: Buffer) => void): void;
    export function inflate(buf: InputType, options: ZlibOptions, callback: (error: Error | null, result: Buffer) => void): void;
    export function inflateSync(buf: InputType, options?: ZlibOptions): Buffer;
    export function inflateRaw(buf: InputType, callback: (error: Error | null, result: Buffer) => void): void;
    export function inflateRaw(buf: InputType, options: ZlibOptions, callback: (error: Error | null, result: Buffer) => void): void;
    export function inflateRawSync(buf: InputType, options?: ZlibOptions): Buffer;
    export function unzip(buf: InputType, callback: (error: Error | null, result: Buffer) => void): void;
    export function unzip(buf: InputType, options: ZlibOptions, callback: (error: Error | null, result: Buffer) => void): void;
    export function unzipSync(buf: InputType, options?: ZlibOptions): Buffer;

    export namespace constants {
        // Allowed flush values.

        export const Z_NO_FLUSH: number;
        export const Z_PARTIAL_FLUSH: number;
        export const Z_SYNC_FLUSH: number;
        export const Z_FULL_FLUSH: number;
        export const Z_FINISH: number;
        export const Z_BLOCK: number;
        export const Z_TREES: number;

        // Return codes for the compression/decompression functions. Negative values are errors, positive values are used for special but normal events.

        export const Z_OK: number;
        export const Z_STREAM_END: number;
        export const Z_NEED_DICT: number;
        export const Z_ERRNO: number;
        export const Z_STREAM_ERROR: number;
        export const Z_DATA_ERROR: number;
        export const Z_MEM_ERROR: number;
        export const Z_BUF_ERROR: number;
        export const Z_VERSION_ERROR: number;

        // Compression levels.

        export const Z_NO_COMPRESSION: number;
        export const Z_BEST_SPEED: number;
        export const Z_BEST_COMPRESSION: number;
        export const Z_DEFAULT_COMPRESSION: number;

        // Compression strategy.

        export const Z_FILTERED: number;
        export const Z_HUFFMAN_ONLY: number;
        export const Z_RLE: number;
        export const Z_FIXED: number;
        export const Z_DEFAULT_STRATEGY: number;
    }

    // Constants
    export var Z_NO_FLUSH: number;
    export var Z_PARTIAL_FLUSH: number;
    export var Z_SYNC_FLUSH: number;
    export var Z_FULL_FLUSH: number;
    export var Z_FINISH: number;
    export var Z_BLOCK: number;
    export var Z_TREES: number;
    export var Z_OK: number;
    export var Z_STREAM_END: number;
    export var Z_NEED_DICT: number;
    export var Z_ERRNO: number;
    export var Z_STREAM_ERROR: number;
    export var Z_DATA_ERROR: number;
    export var Z_MEM_ERROR: number;
    export var Z_BUF_ERROR: number;
    export var Z_VERSION_ERROR: number;
    export var Z_NO_COMPRESSION: number;
    export var Z_BEST_SPEED: number;
    export var Z_BEST_COMPRESSION: number;
    export var Z_DEFAULT_COMPRESSION: number;
    export var Z_FILTERED: number;
    export var Z_HUFFMAN_ONLY: number;
    export var Z_RLE: number;
    export var Z_FIXED: number;
    export var Z_DEFAULT_STRATEGY: number;
    export var Z_BINARY: number;
    export var Z_TEXT: number;
    export var Z_ASCII: number;
    export var Z_UNKNOWN: number;
    export var Z_DEFLATED: number;
}

declare module "os" {
    export interface CpuInfo {
        model: string;
        speed: number;
        times: {
            user: number;
            nice: number;
            sys: number;
            idle: number;
            irq: number;
        };
    }

    export interface NetworkInterfaceBase {
        address: string;
        netmask: string;
        mac: string;
        internal: boolean;
    }

    export interface NetworkInterfaceInfoIPv4 extends NetworkInterfaceBase {
        family: "IPv4";
    }

    export interface NetworkInterfaceInfoIPv6 extends NetworkInterfaceBase {
        family: "IPv6";
        scopeid: number;
    }

    export type NetworkInterfaceInfo = NetworkInterfaceInfoIPv4 | NetworkInterfaceInfoIPv6;

    export function hostname(): string;
    export function loadavg(): number[];
    export function uptime(): number;
    export function freemem(): number;
    export function totalmem(): number;
    export function cpus(): CpuInfo[];
    export function type(): string;
    export function release(): string;
    export function networkInterfaces(): { [index: string]: NetworkInterfaceInfo[] };
    export function homedir(): string;
    export function userInfo(options?: { encoding: string }): { username: string, uid: number, gid: number, shell: any, homedir: string };
    export var constants: {
        UV_UDP_REUSEADDR: number,
        signals: {
            SIGHUP: number;
            SIGINT: number;
            SIGQUIT: number;
            SIGILL: number;
            SIGTRAP: number;
            SIGABRT: number;
            SIGIOT: number;
            SIGBUS: number;
            SIGFPE: number;
            SIGKILL: number;
            SIGUSR1: number;
            SIGSEGV: number;
            SIGUSR2: number;
            SIGPIPE: number;
            SIGALRM: number;
            SIGTERM: number;
            SIGCHLD: number;
            SIGSTKFLT: number;
            SIGCONT: number;
            SIGSTOP: number;
            SIGTSTP: number;
            SIGTTIN: number;
            SIGTTOU: number;
            SIGURG: number;
            SIGXCPU: number;
            SIGXFSZ: number;
            SIGVTALRM: number;
            SIGPROF: number;
            SIGWINCH: number;
            SIGIO: number;
            SIGPOLL: number;
            SIGPWR: number;
            SIGSYS: number;
            SIGUNUSED: number;
        },
        errno: {
            E2BIG: number;
            EACCES: number;
            EADDRINUSE: number;
            EADDRNOTAVAIL: number;
            EAFNOSUPPORT: number;
            EAGAIN: number;
            EALREADY: number;
            EBADF: number;
            EBADMSG: number;
            EBUSY: number;
            ECANCELED: number;
            ECHILD: number;
            ECONNABORTED: number;
            ECONNREFUSED: number;
            ECONNRESET: number;
            EDEADLK: number;
            EDESTADDRREQ: number;
            EDOM: number;
            EDQUOT: number;
            EEXIST: number;
            EFAULT: number;
            EFBIG: number;
            EHOSTUNREACH: number;
            EIDRM: number;
            EILSEQ: number;
            EINPROGRESS: number;
            EINTR: number;
            EINVAL: number;
            EIO: number;
            EISCONN: number;
            EISDIR: number;
            ELOOP: number;
            EMFILE: number;
            EMLINK: number;
            EMSGSIZE: number;
            EMULTIHOP: number;
            ENAMETOOLONG: number;
            ENETDOWN: number;
            ENETRESET: number;
            ENETUNREACH: number;
            ENFILE: number;
            ENOBUFS: number;
            ENODATA: number;
            ENODEV: number;
            ENOENT: number;
            ENOEXEC: number;
            ENOLCK: number;
            ENOLINK: number;
            ENOMEM: number;
            ENOMSG: number;
            ENOPROTOOPT: number;
            ENOSPC: number;
            ENOSR: number;
            ENOSTR: number;
            ENOSYS: number;
            ENOTCONN: number;
            ENOTDIR: number;
            ENOTEMPTY: number;
            ENOTSOCK: number;
            ENOTSUP: number;
            ENOTTY: number;
            ENXIO: number;
            EOPNOTSUPP: number;
            EOVERFLOW: number;
            EPERM: number;
            EPIPE: number;
            EPROTO: number;
            EPROTONOSUPPORT: number;
            EPROTOTYPE: number;
            ERANGE: number;
            EROFS: number;
            ESPIPE: number;
            ESRCH: number;
            ESTALE: number;
            ETIME: number;
            ETIMEDOUT: number;
            ETXTBSY: number;
            EWOULDBLOCK: number;
            EXDEV: number;
        },
    };
    export function arch(): string;
    export function platform(): NodeJS.Platform;
    export function tmpdir(): string;
    export const EOL: string;
    export function endianness(): "BE" | "LE";
}

declare module "https" {
    import * as tls from "tls";
    import * as events from "events";
    import * as http from "http";
    import { URL } from "url";

    export type ServerOptions = tls.SecureContextOptions & tls.TlsOptions;

    // see https://nodejs.org/docs/latest-v8.x/api/https.html#https_https_request_options_callback
    type extendedRequestKeys = "pfx" |
        "key" |
        "passphrase" |
        "cert" |
        "ca" |
        "ciphers" |
        "rejectUnauthorized" |
        "secureProtocol" |
        "servername";

    export type RequestOptions = http.RequestOptions & Pick<tls.ConnectionOptions, extendedRequestKeys>;

    export interface AgentOptions extends http.AgentOptions, tls.ConnectionOptions {
        rejectUnauthorized?: boolean;
        maxCachedSessions?: number;
    }

    export class Agent extends http.Agent {
        constructor(options?: AgentOptions);
        options: AgentOptions;
    }

    export class Server extends tls.Server {
        setTimeout(callback: () => void): this;
        setTimeout(msecs?: number, callback?: () => void): this;
        timeout: number;
        keepAliveTimeout: number;
    }

    export function createServer(options: ServerOptions, requestListener?: (req: http.IncomingMessage, res: http.ServerResponse) => void): Server;
    export function request(options: RequestOptions | string | URL, callback?: (res: http.IncomingMessage) => void): http.ClientRequest;
    export function get(options: RequestOptions | string | URL, callback?: (res: http.IncomingMessage) => void): http.ClientRequest;
    export var globalAgent: Agent;
}

declare module "punycode" {
    export function decode(string: string): string;
    export function encode(string: string): string;
    export function toUnicode(domain: string): string;
    export function toASCII(domain: string): string;
    export var ucs2: ucs2;
    interface ucs2 {
        decode(string: string): number[];
        encode(codePoints: number[]): string;
    }
    export var version: any;
}

declare module "repl" {
    import * as stream from "stream";
    import * as readline from "readline";

    export interface ReplOptions {
        prompt?: string;
        input?: NodeJS.ReadableStream;
        output?: NodeJS.WritableStream;
        terminal?: boolean;
        eval?: Function;
        useColors?: boolean;
        useGlobal?: boolean;
        ignoreUndefined?: boolean;
        writer?: Function;
        completer?: Function;
        replMode?: any;
        breakEvalOnSigint?: any;
    }

    export interface REPLServer extends readline.ReadLine {
        context: any;
        inputStream: NodeJS.ReadableStream;
        outputStream: NodeJS.WritableStream;

        defineCommand(keyword: string, cmd: Function | { help: string, action: Function }): void;
        displayPrompt(preserveCursor?: boolean): void;

        /**
         * events.EventEmitter
         * 1. exit
         * 2. reset
         */

        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "exit", listener: () => void): this;
        addListener(event: "reset", listener: (...args: any[]) => void): this;

        emit(event: string | symbol, ...args: any[]): boolean;
        emit(event: "exit"): boolean;
        emit(event: "reset", context: any): boolean;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "exit", listener: () => void): this;
        on(event: "reset", listener: (...args: any[]) => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "exit", listener: () => void): this;
        once(event: "reset", listener: (...args: any[]) => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "exit", listener: () => void): this;
        prependListener(event: "reset", listener: (...args: any[]) => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "exit", listener: () => void): this;
        prependOnceListener(event: "reset", listener: (...args: any[]) => void): this;
    }

    export function start(options?: string | ReplOptions): REPLServer;

    export class Recoverable extends SyntaxError {
        err: Error;

        constructor(err: Error);
    }
}

declare module "readline" {
    import * as events from "events";
    import * as stream from "stream";

    export interface Key {
        sequence?: string;
        name?: string;
        ctrl?: boolean;
        meta?: boolean;
        shift?: boolean;
    }

    export interface ReadLine extends events.EventEmitter {
        setPrompt(prompt: string): void;
        prompt(preserveCursor?: boolean): void;
        question(query: string, callback: (answer: string) => void): void;
        pause(): ReadLine;
        resume(): ReadLine;
        close(): void;
        write(data: string | Buffer, key?: Key): void;

        /**
         * events.EventEmitter
         * 1. close
         * 2. line
         * 3. pause
         * 4. resume
         * 5. SIGCONT
         * 6. SIGINT
         * 7. SIGTSTP
         */

        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "close", listener: () => void): this;
        addListener(event: "line", listener: (input: any) => void): this;
        addListener(event: "pause", listener: () => void): this;
        addListener(event: "resume", listener: () => void): this;
        addListener(event: "SIGCONT", listener: () => void): this;
        addListener(event: "SIGINT", listener: () => void): this;
        addListener(event: "SIGTSTP", listener: () => void): this;

        emit(event: string | symbol, ...args: any[]): boolean;
        emit(event: "close"): boolean;
        emit(event: "line", input: any): boolean;
        emit(event: "pause"): boolean;
        emit(event: "resume"): boolean;
        emit(event: "SIGCONT"): boolean;
        emit(event: "SIGINT"): boolean;
        emit(event: "SIGTSTP"): boolean;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "close", listener: () => void): this;
        on(event: "line", listener: (input: any) => void): this;
        on(event: "pause", listener: () => void): this;
        on(event: "resume", listener: () => void): this;
        on(event: "SIGCONT", listener: () => void): this;
        on(event: "SIGINT", listener: () => void): this;
        on(event: "SIGTSTP", listener: () => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "close", listener: () => void): this;
        once(event: "line", listener: (input: any) => void): this;
        once(event: "pause", listener: () => void): this;
        once(event: "resume", listener: () => void): this;
        once(event: "SIGCONT", listener: () => void): this;
        once(event: "SIGINT", listener: () => void): this;
        once(event: "SIGTSTP", listener: () => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "close", listener: () => void): this;
        prependListener(event: "line", listener: (input: any) => void): this;
        prependListener(event: "pause", listener: () => void): this;
        prependListener(event: "resume", listener: () => void): this;
        prependListener(event: "SIGCONT", listener: () => void): this;
        prependListener(event: "SIGINT", listener: () => void): this;
        prependListener(event: "SIGTSTP", listener: () => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "close", listener: () => void): this;
        prependOnceListener(event: "line", listener: (input: any) => void): this;
        prependOnceListener(event: "pause", listener: () => void): this;
        prependOnceListener(event: "resume", listener: () => void): this;
        prependOnceListener(event: "SIGCONT", listener: () => void): this;
        prependOnceListener(event: "SIGINT", listener: () => void): this;
        prependOnceListener(event: "SIGTSTP", listener: () => void): this;
    }

    type Completer = (line: string) => CompleterResult;
    type AsyncCompleter = (line: string, callback: (err: any, result: CompleterResult) => void) => any;

    export type CompleterResult = [string[], string];

    export interface ReadLineOptions {
        input: NodeJS.ReadableStream;
        output?: NodeJS.WritableStream;
        completer?: Completer | AsyncCompleter;
        terminal?: boolean;
        historySize?: number;
        prompt?: string;
        crlfDelay?: number;
        removeHistoryDuplicates?: boolean;
    }

    export function createInterface(input: NodeJS.ReadableStream, output?: NodeJS.WritableStream, completer?: Completer | AsyncCompleter, terminal?: boolean): ReadLine;
    export function createInterface(options: ReadLineOptions): ReadLine;

    export function cursorTo(stream: NodeJS.WritableStream, x: number, y?: number): void;
    export function emitKeypressEvents(stream: NodeJS.ReadableStream, interface?: ReadLine): void;
    export function moveCursor(stream: NodeJS.WritableStream, dx: number | string, dy: number | string): void;
    export function clearLine(stream: NodeJS.WritableStream, dir: number): void;
    export function clearScreenDown(stream: NodeJS.WritableStream): void;
}

declare module "vm" {
    export interface Context { }
    export interface ScriptOptions {
        filename?: string;
        lineOffset?: number;
        columnOffset?: number;
        displayErrors?: boolean;
        timeout?: number;
        cachedData?: Buffer;
        produceCachedData?: boolean;
    }
    export interface RunningScriptOptions {
        filename?: string;
        lineOffset?: number;
        columnOffset?: number;
        displayErrors?: boolean;
        timeout?: number;
    }
    export class Script {
        constructor(code: string, options?: ScriptOptions);
        runInContext(contextifiedSandbox: Context, options?: RunningScriptOptions): any;
        runInNewContext(sandbox?: Context, options?: RunningScriptOptions): any;
        runInThisContext(options?: RunningScriptOptions): any;
    }
    export function createContext(sandbox?: Context): Context;
    export function isContext(sandbox: Context): boolean;
    export function runInContext(code: string, contextifiedSandbox: Context, options?: RunningScriptOptions | string): any;
    export function runInDebugContext(code: string): any;
    export function runInNewContext(code: string, sandbox?: Context, options?: RunningScriptOptions | string): any;
    export function runInThisContext(code: string, options?: RunningScriptOptions | string): any;
}

declare module "child_process" {
    import * as events from "events";
    import * as stream from "stream";
    import * as net from "net";

    export interface ChildProcess extends events.EventEmitter {
        stdin: stream.Writable;
        stdout: stream.Readable;
        stderr: stream.Readable;
        stdio: [stream.Writable, stream.Readable, stream.Readable];
        killed: boolean;
        pid: number;
        kill(signal?: string): void;
        send(message: any, callback?: (error: Error) => void): boolean;
        send(message: any, sendHandle?: net.Socket | net.Server, callback?: (error: Error) => void): boolean;
        send(message: any, sendHandle?: net.Socket | net.Server, options?: MessageOptions, callback?: (error: Error) => void): boolean;
        connected: boolean;
        disconnect(): void;
        unref(): void;
        ref(): void;

        /**
         * events.EventEmitter
         * 1. close
         * 2. disconnect
         * 3. error
         * 4. exit
         * 5. message
         */

        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "close", listener: (code: number, signal: string) => void): this;
        addListener(event: "disconnect", listener: () => void): this;
        addListener(event: "error", listener: (err: Error) => void): this;
        addListener(event: "exit", listener: (code: number, signal: string) => void): this;
        addListener(event: "message", listener: (message: any, sendHandle: net.Socket | net.Server) => void): this;

        emit(event: string | symbol, ...args: any[]): boolean;
        emit(event: "close", code: number, signal: string): boolean;
        emit(event: "disconnect"): boolean;
        emit(event: "error", err: Error): boolean;
        emit(event: "exit", code: number, signal: string): boolean;
        emit(event: "message", message: any, sendHandle: net.Socket | net.Server): boolean;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "close", listener: (code: number, signal: string) => void): this;
        on(event: "disconnect", listener: () => void): this;
        on(event: "error", listener: (err: Error) => void): this;
        on(event: "exit", listener: (code: number, signal: string) => void): this;
        on(event: "message", listener: (message: any, sendHandle: net.Socket | net.Server) => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "close", listener: (code: number, signal: string) => void): this;
        once(event: "disconnect", listener: () => void): this;
        once(event: "error", listener: (err: Error) => void): this;
        once(event: "exit", listener: (code: number, signal: string) => void): this;
        once(event: "message", listener: (message: any, sendHandle: net.Socket | net.Server) => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "close", listener: (code: number, signal: string) => void): this;
        prependListener(event: "disconnect", listener: () => void): this;
        prependListener(event: "error", listener: (err: Error) => void): this;
        prependListener(event: "exit", listener: (code: number, signal: string) => void): this;
        prependListener(event: "message", listener: (message: any, sendHandle: net.Socket | net.Server) => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "close", listener: (code: number, signal: string) => void): this;
        prependOnceListener(event: "disconnect", listener: () => void): this;
        prependOnceListener(event: "error", listener: (err: Error) => void): this;
        prependOnceListener(event: "exit", listener: (code: number, signal: string) => void): this;
        prependOnceListener(event: "message", listener: (message: any, sendHandle: net.Socket | net.Server) => void): this;
    }

    export interface MessageOptions {
        keepOpen?: boolean;
    }

    export interface SpawnOptions {
        cwd?: string;
        env?: any;
        stdio?: any;
        detached?: boolean;
        uid?: number;
        gid?: number;
        shell?: boolean | string;
        windowsVerbatimArguments?: boolean;
        windowsHide?: boolean;
    }

    export function spawn(command: string, args?: ReadonlyArray<string>, options?: SpawnOptions): ChildProcess;

    export interface ExecOptions {
        cwd?: string;
        env?: any;
        shell?: string;
        timeout?: number;
        maxBuffer?: number;
        killSignal?: string;
        uid?: number;
        gid?: number;
        windowsHide?: boolean;
    }

    export interface ExecOptionsWithStringEncoding extends ExecOptions {
        encoding: BufferEncoding;
    }

    export interface ExecOptionsWithBufferEncoding extends ExecOptions {
        encoding: string | null; // specify `null`.
    }

    // no `options` definitely means stdout/stderr are `string`.
    export function exec(command: string, callback?: (error: Error | null, stdout: string, stderr: string) => void): ChildProcess;

    // `options` with `"buffer"` or `null` for `encoding` means stdout/stderr are definitely `Buffer`.
    export function exec(command: string, options: { encoding: "buffer" | null } & ExecOptions, callback?: (error: Error | null, stdout: Buffer, stderr: Buffer) => void): ChildProcess;

    // `options` with well known `encoding` means stdout/stderr are definitely `string`.
    export function exec(command: string, options: { encoding: BufferEncoding } & ExecOptions, callback?: (error: Error | null, stdout: string, stderr: string) => void): ChildProcess;

    // `options` with an `encoding` whose type is `string` means stdout/stderr could either be `Buffer` or `string`.
    // There is no guarantee the `encoding` is unknown as `string` is a superset of `BufferEncoding`.
    export function exec(command: string, options: { encoding: string } & ExecOptions, callback?: (error: Error | null, stdout: string | Buffer, stderr: string | Buffer) => void): ChildProcess;

    // `options` without an `encoding` means stdout/stderr are definitely `string`.
    export function exec(command: string, options: ExecOptions, callback?: (error: Error | null, stdout: string, stderr: string) => void): ChildProcess;

    // fallback if nothing else matches. Worst case is always `string | Buffer`.
    export function exec(command: string, options: ({ encoding?: string | null } & ExecOptions) | undefined | null, callback?: (error: Error | null, stdout: string | Buffer, stderr: string | Buffer) => void): ChildProcess;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace exec {
        export function __promisify__(command: string): Promise<{ stdout: string, stderr: string }>;
        export function __promisify__(command: string, options: { encoding: "buffer" | null } & ExecOptions): Promise<{ stdout: Buffer, stderr: Buffer }>;
        export function __promisify__(command: string, options: { encoding: BufferEncoding } & ExecOptions): Promise<{ stdout: string, stderr: string }>;
        export function __promisify__(command: string, options: ExecOptions): Promise<{ stdout: string, stderr: string }>;
        export function __promisify__(command: string, options?: ({ encoding?: string | null } & ExecOptions) | null): Promise<{ stdout: string | Buffer, stderr: string | Buffer }>;
    }

    export interface ExecFileOptions {
        cwd?: string;
        env?: any;
        timeout?: number;
        maxBuffer?: number;
        killSignal?: string;
        uid?: number;
        gid?: number;
        windowsHide?: boolean;
        windowsVerbatimArguments?: boolean;
    }
    export interface ExecFileOptionsWithStringEncoding extends ExecFileOptions {
        encoding: BufferEncoding;
    }
    export interface ExecFileOptionsWithBufferEncoding extends ExecFileOptions {
        encoding: 'buffer' | null;
    }
    export interface ExecFileOptionsWithOtherEncoding extends ExecFileOptions {
        encoding: string;
    }

    export function execFile(file: string): ChildProcess;
    export function execFile(file: string, options: ({ encoding?: string | null } & ExecFileOptions) | undefined | null): ChildProcess;
    export function execFile(file: string, args: string[] | undefined | null): ChildProcess;
    export function execFile(file: string, args: string[] | undefined | null, options: ({ encoding?: string | null } & ExecFileOptions) | undefined | null): ChildProcess;

    // no `options` definitely means stdout/stderr are `string`.
    export function execFile(file: string, callback: (error: Error | null, stdout: string, stderr: string) => void): ChildProcess;
    export function execFile(file: string, args: string[] | undefined | null, callback: (error: Error | null, stdout: string, stderr: string) => void): ChildProcess;

    // `options` with `"buffer"` or `null` for `encoding` means stdout/stderr are definitely `Buffer`.
    export function execFile(file: string, options: ExecFileOptionsWithBufferEncoding, callback: (error: Error | null, stdout: Buffer, stderr: Buffer) => void): ChildProcess;
    export function execFile(file: string, args: string[] | undefined | null, options: ExecFileOptionsWithBufferEncoding, callback: (error: Error | null, stdout: Buffer, stderr: Buffer) => void): ChildProcess;

    // `options` with well known `encoding` means stdout/stderr are definitely `string`.
    export function execFile(file: string, options: ExecFileOptionsWithStringEncoding, callback: (error: Error | null, stdout: string, stderr: string) => void): ChildProcess;
    export function execFile(file: string, args: string[] | undefined | null, options: ExecFileOptionsWithStringEncoding, callback: (error: Error | null, stdout: string, stderr: string) => void): ChildProcess;

    // `options` with an `encoding` whose type is `string` means stdout/stderr could either be `Buffer` or `string`.
    // There is no guarantee the `encoding` is unknown as `string` is a superset of `BufferEncoding`.
    export function execFile(file: string, options: ExecFileOptionsWithOtherEncoding, callback: (error: Error | null, stdout: string | Buffer, stderr: string | Buffer) => void): ChildProcess;
    export function execFile(file: string, args: string[] | undefined | null, options: ExecFileOptionsWithOtherEncoding, callback: (error: Error | null, stdout: string | Buffer, stderr: string | Buffer) => void): ChildProcess;

    // `options` without an `encoding` means stdout/stderr are definitely `string`.
    export function execFile(file: string, options: ExecFileOptions, callback: (error: Error | null, stdout: string, stderr: string) => void): ChildProcess;
    export function execFile(file: string, args: string[] | undefined | null, options: ExecFileOptions, callback: (error: Error | null, stdout: string, stderr: string) => void): ChildProcess;

    // fallback if nothing else matches. Worst case is always `string | Buffer`.
    export function execFile(file: string, options: ({ encoding?: string | null } & ExecFileOptions) | undefined | null, callback: ((error: Error | null, stdout: string | Buffer, stderr: string | Buffer) => void) | undefined | null): ChildProcess;
    export function execFile(file: string, args: string[] | undefined | null, options: ({ encoding?: string | null } & ExecFileOptions) | undefined | null, callback: ((error: Error | null, stdout: string | Buffer, stderr: string | Buffer) => void) | undefined | null): ChildProcess;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace execFile {
        export function __promisify__(file: string): Promise<{ stdout: string, stderr: string }>;
        export function __promisify__(file: string, args: string[] | undefined | null): Promise<{ stdout: string, stderr: string }>;
        export function __promisify__(file: string, options: ExecFileOptionsWithBufferEncoding): Promise<{ stdout: Buffer, stderr: Buffer }>;
        export function __promisify__(file: string, args: string[] | undefined | null, options: ExecFileOptionsWithBufferEncoding): Promise<{ stdout: Buffer, stderr: Buffer }>;
        export function __promisify__(file: string, options: ExecFileOptionsWithStringEncoding): Promise<{ stdout: string, stderr: string }>;
        export function __promisify__(file: string, args: string[] | undefined | null, options: ExecFileOptionsWithStringEncoding): Promise<{ stdout: string, stderr: string }>;
        export function __promisify__(file: string, options: ExecFileOptionsWithOtherEncoding): Promise<{ stdout: string | Buffer, stderr: string | Buffer }>;
        export function __promisify__(file: string, args: string[] | undefined | null, options: ExecFileOptionsWithOtherEncoding): Promise<{ stdout: string | Buffer, stderr: string | Buffer }>;
        export function __promisify__(file: string, options: ExecFileOptions): Promise<{ stdout: string, stderr: string }>;
        export function __promisify__(file: string, args: string[] | undefined | null, options: ExecFileOptions): Promise<{ stdout: string, stderr: string }>;
        export function __promisify__(file: string, options: ({ encoding?: string | null } & ExecFileOptions) | undefined | null): Promise<{ stdout: string | Buffer, stderr: string | Buffer }>;
        export function __promisify__(file: string, args: string[] | undefined | null, options: ({ encoding?: string | null } & ExecFileOptions) | undefined | null): Promise<{ stdout: string | Buffer, stderr: string | Buffer }>;
    }

    export interface ForkOptions {
        cwd?: string;
        env?: any;
        execPath?: string;
        execArgv?: string[];
        silent?: boolean;
        stdio?: any[];
        uid?: number;
        gid?: number;
        windowsVerbatimArguments?: boolean;
    }
    export function fork(modulePath: string, args?: string[], options?: ForkOptions): ChildProcess;

    export interface SpawnSyncOptions {
        cwd?: string;
        input?: string | Buffer;
        stdio?: any;
        env?: any;
        uid?: number;
        gid?: number;
        timeout?: number;
        killSignal?: string;
        maxBuffer?: number;
        encoding?: string;
        shell?: boolean | string;
        windowsHide?: boolean;
        windowsVerbatimArguments?: boolean;
    }
    export interface SpawnSyncOptionsWithStringEncoding extends SpawnSyncOptions {
        encoding: BufferEncoding;
    }
    export interface SpawnSyncOptionsWithBufferEncoding extends SpawnSyncOptions {
        encoding: string; // specify `null`.
    }
    export interface SpawnSyncReturns<T> {
        pid: number;
        output: string[];
        stdout: T;
        stderr: T;
        status: number;
        signal: string;
        error: Error;
    }
    export function spawnSync(command: string): SpawnSyncReturns<Buffer>;
    export function spawnSync(command: string, options?: SpawnSyncOptionsWithStringEncoding): SpawnSyncReturns<string>;
    export function spawnSync(command: string, options?: SpawnSyncOptionsWithBufferEncoding): SpawnSyncReturns<Buffer>;
    export function spawnSync(command: string, options?: SpawnSyncOptions): SpawnSyncReturns<Buffer>;
    export function spawnSync(command: string, args?: string[], options?: SpawnSyncOptionsWithStringEncoding): SpawnSyncReturns<string>;
    export function spawnSync(command: string, args?: string[], options?: SpawnSyncOptionsWithBufferEncoding): SpawnSyncReturns<Buffer>;
    export function spawnSync(command: string, args?: string[], options?: SpawnSyncOptions): SpawnSyncReturns<Buffer>;

    export interface ExecSyncOptions {
        cwd?: string;
        input?: string | Buffer;
        stdio?: any;
        env?: any;
        shell?: string;
        uid?: number;
        gid?: number;
        timeout?: number;
        killSignal?: string;
        maxBuffer?: number;
        encoding?: string;
        windowsHide?: boolean;
    }
    export interface ExecSyncOptionsWithStringEncoding extends ExecSyncOptions {
        encoding: BufferEncoding;
    }
    export interface ExecSyncOptionsWithBufferEncoding extends ExecSyncOptions {
        encoding: string; // specify `null`.
    }
    export function execSync(command: string): Buffer;
    export function execSync(command: string, options?: ExecSyncOptionsWithStringEncoding): string;
    export function execSync(command: string, options?: ExecSyncOptionsWithBufferEncoding): Buffer;
    export function execSync(command: string, options?: ExecSyncOptions): Buffer;

    export interface ExecFileSyncOptions {
        cwd?: string;
        input?: string | Buffer;
        stdio?: any;
        env?: any;
        uid?: number;
        gid?: number;
        timeout?: number;
        killSignal?: string;
        maxBuffer?: number;
        encoding?: string;
        windowsHide?: boolean;
    }
    export interface ExecFileSyncOptionsWithStringEncoding extends ExecFileSyncOptions {
        encoding: BufferEncoding;
    }
    export interface ExecFileSyncOptionsWithBufferEncoding extends ExecFileSyncOptions {
        encoding: string; // specify `null`.
    }
    export function execFileSync(command: string): Buffer;
    export function execFileSync(command: string, options?: ExecFileSyncOptionsWithStringEncoding): string;
    export function execFileSync(command: string, options?: ExecFileSyncOptionsWithBufferEncoding): Buffer;
    export function execFileSync(command: string, options?: ExecFileSyncOptions): Buffer;
    export function execFileSync(command: string, args?: string[], options?: ExecFileSyncOptionsWithStringEncoding): string;
    export function execFileSync(command: string, args?: string[], options?: ExecFileSyncOptionsWithBufferEncoding): Buffer;
    export function execFileSync(command: string, args?: string[], options?: ExecFileSyncOptions): Buffer;
}

declare module "url" {
    import { ParsedUrlQuery } from 'querystring';

    export interface UrlObjectCommon {
        auth?: string;
        hash?: string;
        host?: string;
        hostname?: string;
        href?: string;
        path?: string;
        pathname?: string;
        protocol?: string;
        search?: string;
        slashes?: boolean;
    }

    // Input to `url.format`
    export interface UrlObject extends UrlObjectCommon {
        port?: string | number;
        query?: string | null | { [key: string]: any };
    }

    // Output of `url.parse`
    export interface Url extends UrlObjectCommon {
        port?: string;
        query?: string | null | ParsedUrlQuery;
    }

    export interface UrlWithParsedQuery extends Url {
        query: ParsedUrlQuery;
    }

    export interface UrlWithStringQuery extends Url {
        query: string | null;
    }

    export function parse(urlStr: string): UrlWithStringQuery;
    export function parse(urlStr: string, parseQueryString: false | undefined, slashesDenoteHost?: boolean): UrlWithStringQuery;
    export function parse(urlStr: string, parseQueryString: true, slashesDenoteHost?: boolean): UrlWithParsedQuery;
    export function parse(urlStr: string, parseQueryString: boolean, slashesDenoteHost?: boolean): Url;

    export function format(URL: URL, options?: URLFormatOptions): string;
    export function format(urlObject: UrlObject | string): string;
    export function resolve(from: string, to: string): string;

    export function domainToASCII(domain: string): string;
    export function domainToUnicode(domain: string): string;

    export interface URLFormatOptions {
        auth?: boolean;
        fragment?: boolean;
        search?: boolean;
        unicode?: boolean;
    }

    export class URLSearchParams implements Iterable<[string, string]> {
        constructor(init?: URLSearchParams | string | { [key: string]: string | string[] | undefined } | Iterable<[string, string]> | Array<[string, string]>);
        append(name: string, value: string): void;
        delete(name: string): void;
        entries(): IterableIterator<[string, string]>;
        forEach(callback: (value: string, name: string) => void): void;
        get(name: string): string | null;
        getAll(name: string): string[];
        has(name: string): boolean;
        keys(): IterableIterator<string>;
        set(name: string, value: string): void;
        sort(): void;
        toString(): string;
        values(): IterableIterator<string>;
        [Symbol.iterator](): IterableIterator<[string, string]>;
    }

    export class URL {
        constructor(input: string, base?: string | URL);
        hash: string;
        host: string;
        hostname: string;
        href: string;
        readonly origin: string;
        password: string;
        pathname: string;
        port: string;
        protocol: string;
        search: string;
        readonly searchParams: URLSearchParams;
        username: string;
        toString(): string;
        toJSON(): string;
    }
}

declare module "dns" {
    // Supported getaddrinfo flags.
    export const ADDRCONFIG: number;
    export const V4MAPPED: number;

    export interface LookupOptions {
        family?: number;
        hints?: number;
        all?: boolean;
    }

    export interface LookupOneOptions extends LookupOptions {
        all?: false;
    }

    export interface LookupAllOptions extends LookupOptions {
        all: true;
    }

    export interface LookupAddress {
        address: string;
        family: number;
    }

    export function lookup(hostname: string, family: number, callback: (err: NodeJS.ErrnoException, address: string, family: number) => void): void;
    export function lookup(hostname: string, options: LookupOneOptions, callback: (err: NodeJS.ErrnoException, address: string, family: number) => void): void;
    export function lookup(hostname: string, options: LookupAllOptions, callback: (err: NodeJS.ErrnoException, addresses: LookupAddress[]) => void): void;
    export function lookup(hostname: string, options: LookupOptions, callback: (err: NodeJS.ErrnoException, address: string | LookupAddress[], family: number) => void): void;
    export function lookup(hostname: string, callback: (err: NodeJS.ErrnoException, address: string, family: number) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace lookup {
        export function __promisify__(hostname: string, options: LookupAllOptions): Promise<{ address: LookupAddress[] }>;
        export function __promisify__(hostname: string, options?: LookupOneOptions | number): Promise<{ address: string, family: number }>;
        export function __promisify__(hostname: string, options?: LookupOptions | number): Promise<{ address: string | LookupAddress[], family?: number }>;
    }

    export function lookupService(address: string, port: number, callback: (err: NodeJS.ErrnoException, hostname: string, service: string) => void): void;

    export namespace lookupService {
        export function __promisify__(address: string, port: number): Promise<{ hostname: string, service: string }>;
    }

    export interface ResolveOptions {
        ttl: boolean;
    }

    export interface ResolveWithTtlOptions extends ResolveOptions {
        ttl: true;
    }

    export interface RecordWithTtl {
        address: string;
        ttl: number;
    }

    export interface MxRecord {
        priority: number;
        exchange: string;
    }

    export interface NaptrRecord {
        flags: string;
        service: string;
        regexp: string;
        replacement: string;
        order: number;
        preference: number;
    }

    export interface SoaRecord {
        nsname: string;
        hostmaster: string;
        serial: number;
        refresh: number;
        retry: number;
        expire: number;
        minttl: number;
    }

    export interface SrvRecord {
        priority: number;
        weight: number;
        port: number;
        name: string;
    }

    export function resolve(hostname: string, callback: (err: NodeJS.ErrnoException, addresses: string[]) => void): void;
    export function resolve(hostname: string, rrtype: "A", callback: (err: NodeJS.ErrnoException, addresses: string[]) => void): void;
    export function resolve(hostname: string, rrtype: "AAAA", callback: (err: NodeJS.ErrnoException, addresses: string[]) => void): void;
    export function resolve(hostname: string, rrtype: "CNAME", callback: (err: NodeJS.ErrnoException, addresses: string[]) => void): void;
    export function resolve(hostname: string, rrtype: "MX", callback: (err: NodeJS.ErrnoException, addresses: MxRecord[]) => void): void;
    export function resolve(hostname: string, rrtype: "NAPTR", callback: (err: NodeJS.ErrnoException, addresses: NaptrRecord[]) => void): void;
    export function resolve(hostname: string, rrtype: "NS", callback: (err: NodeJS.ErrnoException, addresses: string[]) => void): void;
    export function resolve(hostname: string, rrtype: "PTR", callback: (err: NodeJS.ErrnoException, addresses: string[]) => void): void;
    export function resolve(hostname: string, rrtype: "SOA", callback: (err: NodeJS.ErrnoException, addresses: SoaRecord) => void): void;
    export function resolve(hostname: string, rrtype: "SRV", callback: (err: NodeJS.ErrnoException, addresses: SrvRecord[]) => void): void;
    export function resolve(hostname: string, rrtype: "TXT", callback: (err: NodeJS.ErrnoException, addresses: string[][]) => void): void;
    export function resolve(hostname: string, rrtype: string, callback: (err: NodeJS.ErrnoException, addresses: string[] | MxRecord[] | NaptrRecord[] | SoaRecord | SrvRecord[] | string[][]) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace resolve {
        export function __promisify__(hostname: string, rrtype?: "A" | "AAAA" | "CNAME" | "NS" | "PTR"): Promise<string[]>;
        export function __promisify__(hostname: string, rrtype: "MX"): Promise<MxRecord[]>;
        export function __promisify__(hostname: string, rrtype: "NAPTR"): Promise<NaptrRecord[]>;
        export function __promisify__(hostname: string, rrtype: "SOA"): Promise<SoaRecord>;
        export function __promisify__(hostname: string, rrtype: "SRV"): Promise<SrvRecord[]>;
        export function __promisify__(hostname: string, rrtype: "TXT"): Promise<string[][]>;
        export function __promisify__(hostname: string, rrtype?: string): Promise<string[] | MxRecord[] | NaptrRecord[] | SoaRecord | SrvRecord[] | string[][]>;
    }

    export function resolve4(hostname: string, callback: (err: NodeJS.ErrnoException, addresses: string[]) => void): void;
    export function resolve4(hostname: string, options: ResolveWithTtlOptions, callback: (err: NodeJS.ErrnoException, addresses: RecordWithTtl[]) => void): void;
    export function resolve4(hostname: string, options: ResolveOptions, callback: (err: NodeJS.ErrnoException, addresses: string[] | RecordWithTtl[]) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace resolve4 {
        export function __promisify__(hostname: string): Promise<string[]>;
        export function __promisify__(hostname: string, options: ResolveWithTtlOptions): Promise<RecordWithTtl[]>;
        export function __promisify__(hostname: string, options?: ResolveOptions): Promise<string[] | RecordWithTtl[]>;
    }

    export function resolve6(hostname: string, callback: (err: NodeJS.ErrnoException, addresses: string[]) => void): void;
    export function resolve6(hostname: string, options: ResolveWithTtlOptions, callback: (err: NodeJS.ErrnoException, addresses: RecordWithTtl[]) => void): void;
    export function resolve6(hostname: string, options: ResolveOptions, callback: (err: NodeJS.ErrnoException, addresses: string[] | RecordWithTtl[]) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace resolve6 {
        export function __promisify__(hostname: string): Promise<string[]>;
        export function __promisify__(hostname: string, options: ResolveWithTtlOptions): Promise<RecordWithTtl[]>;
        export function __promisify__(hostname: string, options?: ResolveOptions): Promise<string[] | RecordWithTtl[]>;
    }

    export function resolveCname(hostname: string, callback: (err: NodeJS.ErrnoException, addresses: string[]) => void): void;
    export function resolveMx(hostname: string, callback: (err: NodeJS.ErrnoException, addresses: MxRecord[]) => void): void;
    export function resolveNaptr(hostname: string, callback: (err: NodeJS.ErrnoException, addresses: NaptrRecord[]) => void): void;
    export function resolveNs(hostname: string, callback: (err: NodeJS.ErrnoException, addresses: string[]) => void): void;
    export function resolvePtr(hostname: string, callback: (err: NodeJS.ErrnoException, addresses: string[]) => void): void;
    export function resolveSoa(hostname: string, callback: (err: NodeJS.ErrnoException, address: SoaRecord) => void): void;
    export function resolveSrv(hostname: string, callback: (err: NodeJS.ErrnoException, addresses: SrvRecord[]) => void): void;
    export function resolveTxt(hostname: string, callback: (err: NodeJS.ErrnoException, addresses: string[][]) => void): void;

    export function reverse(ip: string, callback: (err: NodeJS.ErrnoException, hostnames: string[]) => void): void;
    export function setServers(servers: string[]): void;

    // Error codes
    export var NODATA: string;
    export var FORMERR: string;
    export var SERVFAIL: string;
    export var NOTFOUND: string;
    export var NOTIMP: string;
    export var REFUSED: string;
    export var BADQUERY: string;
    export var BADNAME: string;
    export var BADFAMILY: string;
    export var BADRESP: string;
    export var CONNREFUSED: string;
    export var TIMEOUT: string;
    export var EOF: string;
    export var FILE: string;
    export var NOMEM: string;
    export var DESTRUCTION: string;
    export var BADSTR: string;
    export var BADFLAGS: string;
    export var NONAME: string;
    export var BADHINTS: string;
    export var NOTINITIALIZED: string;
    export var LOADIPHLPAPI: string;
    export var ADDRGETNETWORKPARAMS: string;
    export var CANCELLED: string;
}

declare module "net" {
    import * as stream from "stream";
    import * as events from "events";
    import * as dns from "dns";

    type LookupFunction = (hostname: string, options: dns.LookupOneOptions, callback: (err: NodeJS.ErrnoException | null, address: string, family: number) => void) => void;

    export interface SocketConstructorOpts {
        fd?: number;
        allowHalfOpen?: boolean;
        readable?: boolean;
        writable?: boolean;
    }

    export interface TcpSocketConnectOpts {
        port: number;
        host?: string;
        localAddress?: string;
        localPort?: number;
        hints?: number;
        family?: number;
        lookup?: LookupFunction;
    }

    export interface IpcSocketConnectOpts {
        path: string;
    }

    export type SocketConnectOpts = TcpSocketConnectOpts | IpcSocketConnectOpts;

    export class Socket extends stream.Duplex {
        constructor(options?: SocketConstructorOpts);

        // Extended base methods
        write(buffer: Buffer): boolean;
        write(buffer: Buffer, cb?: Function): boolean;
        write(str: string, cb?: Function): boolean;
        write(str: string, encoding?: string, cb?: Function): boolean;
        write(str: string, encoding?: string, fd?: string): boolean;
        write(data: any, encoding?: string, callback?: Function): void;

        connect(options: SocketConnectOpts, connectionListener?: Function): this;
        connect(port: number, host: string, connectionListener?: Function): this;
        connect(port: number, connectionListener?: Function): this;
        connect(path: string, connectionListener?: Function): this;

        bufferSize: number;
        setEncoding(encoding?: string): this;
        destroy(err?: any): void;
        pause(): this;
        resume(): this;
        setTimeout(timeout: number, callback?: Function): this;
        setNoDelay(noDelay?: boolean): this;
        setKeepAlive(enable?: boolean, initialDelay?: number): this;
        address(): { port: number; family: string; address: string; };
        unref(): void;
        ref(): void;

        remoteAddress?: string;
        remoteFamily?: string;
        remotePort?: number;
        localAddress: string;
        localPort: number;
        bytesRead: number;
        bytesWritten: number;
        connecting: boolean;
        destroyed: boolean;

        // Extended base methods
        end(): void;
        end(buffer: Buffer, cb?: Function): void;
        end(str: string, cb?: Function): void;
        end(str: string, encoding?: string, cb?: Function): void;
        end(data?: any, encoding?: string): void;

        /**
         * events.EventEmitter
         *   1. close
         *   2. connect
         *   3. data
         *   4. drain
         *   5. end
         *   6. error
         *   7. lookup
         *   8. timeout
         */
        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "close", listener: (had_error: boolean) => void): this;
        addListener(event: "connect", listener: () => void): this;
        addListener(event: "data", listener: (data: Buffer) => void): this;
        addListener(event: "drain", listener: () => void): this;
        addListener(event: "end", listener: () => void): this;
        addListener(event: "error", listener: (err: Error) => void): this;
        addListener(event: "lookup", listener: (err: Error, address: string, family: string | number, host: string) => void): this;
        addListener(event: "timeout", listener: () => void): this;

        emit(event: string | symbol, ...args: any[]): boolean;
        emit(event: "close", had_error: boolean): boolean;
        emit(event: "connect"): boolean;
        emit(event: "data", data: Buffer): boolean;
        emit(event: "drain"): boolean;
        emit(event: "end"): boolean;
        emit(event: "error", err: Error): boolean;
        emit(event: "lookup", err: Error, address: string, family: string | number, host: string): boolean;
        emit(event: "timeout"): boolean;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "close", listener: (had_error: boolean) => void): this;
        on(event: "connect", listener: () => void): this;
        on(event: "data", listener: (data: Buffer) => void): this;
        on(event: "drain", listener: () => void): this;
        on(event: "end", listener: () => void): this;
        on(event: "error", listener: (err: Error) => void): this;
        on(event: "lookup", listener: (err: Error, address: string, family: string | number, host: string) => void): this;
        on(event: "timeout", listener: () => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "close", listener: (had_error: boolean) => void): this;
        once(event: "connect", listener: () => void): this;
        once(event: "data", listener: (data: Buffer) => void): this;
        once(event: "drain", listener: () => void): this;
        once(event: "end", listener: () => void): this;
        once(event: "error", listener: (err: Error) => void): this;
        once(event: "lookup", listener: (err: Error, address: string, family: string | number, host: string) => void): this;
        once(event: "timeout", listener: () => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "close", listener: (had_error: boolean) => void): this;
        prependListener(event: "connect", listener: () => void): this;
        prependListener(event: "data", listener: (data: Buffer) => void): this;
        prependListener(event: "drain", listener: () => void): this;
        prependListener(event: "end", listener: () => void): this;
        prependListener(event: "error", listener: (err: Error) => void): this;
        prependListener(event: "lookup", listener: (err: Error, address: string, family: string | number, host: string) => void): this;
        prependListener(event: "timeout", listener: () => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "close", listener: (had_error: boolean) => void): this;
        prependOnceListener(event: "connect", listener: () => void): this;
        prependOnceListener(event: "data", listener: (data: Buffer) => void): this;
        prependOnceListener(event: "drain", listener: () => void): this;
        prependOnceListener(event: "end", listener: () => void): this;
        prependOnceListener(event: "error", listener: (err: Error) => void): this;
        prependOnceListener(event: "lookup", listener: (err: Error, address: string, family: string | number, host: string) => void): this;
        prependOnceListener(event: "timeout", listener: () => void): this;
    }

    export interface ListenOptions {
        port?: number;
        host?: string;
        backlog?: number;
        path?: string;
        exclusive?: boolean;
    }

    // https://github.com/nodejs/node/blob/master/lib/net.js
    export class Server extends events.EventEmitter {
        constructor(connectionListener?: (socket: Socket) => void);
        constructor(options?: { allowHalfOpen?: boolean, pauseOnConnect?: boolean }, connectionListener?: (socket: Socket) => void);

        listen(port?: number, hostname?: string, backlog?: number, listeningListener?: Function): this;
        listen(port?: number, hostname?: string, listeningListener?: Function): this;
        listen(port?: number, backlog?: number, listeningListener?: Function): this;
        listen(port?: number, listeningListener?: Function): this;
        listen(path: string, backlog?: number, listeningListener?: Function): this;
        listen(path: string, listeningListener?: Function): this;
        listen(options: ListenOptions, listeningListener?: Function): this;
        listen(handle: any, backlog?: number, listeningListener?: Function): this;
        listen(handle: any, listeningListener?: Function): this;
        close(callback?: Function): this;
        address(): { port: number; family: string; address: string; };
        getConnections(cb: (error: Error | null, count: number) => void): void;
        ref(): this;
        unref(): this;
        maxConnections: number;
        connections: number;
        listening: boolean;

        /**
         * events.EventEmitter
         *   1. close
         *   2. connection
         *   3. error
         *   4. listening
         */
        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "close", listener: () => void): this;
        addListener(event: "connection", listener: (socket: Socket) => void): this;
        addListener(event: "error", listener: (err: Error) => void): this;
        addListener(event: "listening", listener: () => void): this;

        emit(event: string | symbol, ...args: any[]): boolean;
        emit(event: "close"): boolean;
        emit(event: "connection", socket: Socket): boolean;
        emit(event: "error", err: Error): boolean;
        emit(event: "listening"): boolean;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "close", listener: () => void): this;
        on(event: "connection", listener: (socket: Socket) => void): this;
        on(event: "error", listener: (err: Error) => void): this;
        on(event: "listening", listener: () => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "close", listener: () => void): this;
        once(event: "connection", listener: (socket: Socket) => void): this;
        once(event: "error", listener: (err: Error) => void): this;
        once(event: "listening", listener: () => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "close", listener: () => void): this;
        prependListener(event: "connection", listener: (socket: Socket) => void): this;
        prependListener(event: "error", listener: (err: Error) => void): this;
        prependListener(event: "listening", listener: () => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "close", listener: () => void): this;
        prependOnceListener(event: "connection", listener: (socket: Socket) => void): this;
        prependOnceListener(event: "error", listener: (err: Error) => void): this;
        prependOnceListener(event: "listening", listener: () => void): this;
    }

    export interface TcpNetConnectOpts extends TcpSocketConnectOpts, SocketConstructorOpts {
        timeout?: number;
    }

    export interface IpcNetConnectOpts extends IpcSocketConnectOpts, SocketConstructorOpts {
        timeout?: number;
    }

    export type NetConnectOpts = TcpNetConnectOpts | IpcNetConnectOpts;

    export function createServer(connectionListener?: (socket: Socket) => void): Server;
    export function createServer(options?: { allowHalfOpen?: boolean, pauseOnConnect?: boolean }, connectionListener?: (socket: Socket) => void): Server;
    export function connect(options: NetConnectOpts, connectionListener?: Function): Socket;
    export function connect(port: number, host?: string, connectionListener?: Function): Socket;
    export function connect(path: string, connectionListener?: Function): Socket;
    export function createConnection(options: NetConnectOpts, connectionListener?: Function): Socket;
    export function createConnection(port: number, host?: string, connectionListener?: Function): Socket;
    export function createConnection(path: string, connectionListener?: Function): Socket;
    export function isIP(input: string): number;
    export function isIPv4(input: string): boolean;
    export function isIPv6(input: string): boolean;
}

declare module "dgram" {
    import * as events from "events";
    import * as dns from "dns";

    interface RemoteInfo {
        address: string;
        family: string;
        port: number;
    }

    interface AddressInfo {
        address: string;
        family: string;
        port: number;
    }

    interface BindOptions {
        port: number;
        address?: string;
        exclusive?: boolean;
    }

    type SocketType = "udp4" | "udp6";

    interface SocketOptions {
        type: SocketType;
        reuseAddr?: boolean;
        recvBufferSize?: number;
        sendBufferSize?: number;
        lookup?: (hostname: string, options: dns.LookupOneOptions, callback: (err: NodeJS.ErrnoException, address: string, family: number) => void) => void;
    }

    export function createSocket(type: SocketType, callback?: (msg: Buffer, rinfo: RemoteInfo) => void): Socket;
    export function createSocket(options: SocketOptions, callback?: (msg: Buffer, rinfo: RemoteInfo) => void): Socket;

    export class Socket extends events.EventEmitter {
        send(msg: Buffer | string | Uint8Array | any[], port: number, address?: string, callback?: (error: Error | null, bytes: number) => void): void;
        send(msg: Buffer | string | Uint8Array, offset: number, length: number, port: number, address?: string, callback?: (error: Error | null, bytes: number) => void): void;
        bind(port?: number, address?: string, callback?: () => void): void;
        bind(port?: number, callback?: () => void): void;
        bind(callback?: () => void): void;
        bind(options: BindOptions, callback?: Function): void;
        close(callback?: () => void): void;
        address(): AddressInfo;
        setBroadcast(flag: boolean): void;
        setTTL(ttl: number): void;
        setMulticastTTL(ttl: number): void;
        setMulticastInterface(multicastInterface: string): void;
        setMulticastLoopback(flag: boolean): void;
        addMembership(multicastAddress: string, multicastInterface?: string): void;
        dropMembership(multicastAddress: string, multicastInterface?: string): void;
        ref(): this;
        unref(): this;
        setRecvBufferSize(size: number): void;
        setSendBufferSize(size: number): void;
        getRecvBufferSize(): number;
        getSendBufferSize(): number;

        /**
         * events.EventEmitter
         * 1. close
         * 2. error
         * 3. listening
         * 4. message
         */
        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "close", listener: () => void): this;
        addListener(event: "error", listener: (err: Error) => void): this;
        addListener(event: "listening", listener: () => void): this;
        addListener(event: "message", listener: (msg: Buffer, rinfo: AddressInfo) => void): this;

        emit(event: string | symbol, ...args: any[]): boolean;
        emit(event: "close"): boolean;
        emit(event: "error", err: Error): boolean;
        emit(event: "listening"): boolean;
        emit(event: "message", msg: Buffer, rinfo: AddressInfo): boolean;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "close", listener: () => void): this;
        on(event: "error", listener: (err: Error) => void): this;
        on(event: "listening", listener: () => void): this;
        on(event: "message", listener: (msg: Buffer, rinfo: AddressInfo) => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "close", listener: () => void): this;
        once(event: "error", listener: (err: Error) => void): this;
        once(event: "listening", listener: () => void): this;
        once(event: "message", listener: (msg: Buffer, rinfo: AddressInfo) => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "close", listener: () => void): this;
        prependListener(event: "error", listener: (err: Error) => void): this;
        prependListener(event: "listening", listener: () => void): this;
        prependListener(event: "message", listener: (msg: Buffer, rinfo: AddressInfo) => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "close", listener: () => void): this;
        prependOnceListener(event: "error", listener: (err: Error) => void): this;
        prependOnceListener(event: "listening", listener: () => void): this;
        prependOnceListener(event: "message", listener: (msg: Buffer, rinfo: AddressInfo) => void): this;
    }
}

declare module "fs" {
    import * as stream from "stream";
    import * as events from "events";
    import { URL } from "url";

    /**
     * Valid types for path values in "fs".
     */
    export type PathLike = string | Buffer | URL;

    export class Stats {
        isFile(): boolean;
        isDirectory(): boolean;
        isBlockDevice(): boolean;
        isCharacterDevice(): boolean;
        isSymbolicLink(): boolean;
        isFIFO(): boolean;
        isSocket(): boolean;
        dev: number;
        ino: number;
        mode: number;
        nlink: number;
        uid: number;
        gid: number;
        rdev: number;
        size: number;
        blksize: number;
        blocks: number;
        atimeMs: number;
        mtimeMs: number;
        ctimeMs: number;
        birthtimeMs: number;
        atime: Date;
        mtime: Date;
        ctime: Date;
        birthtime: Date;
    }

    export interface FSWatcher extends events.EventEmitter {
        close(): void;

        /**
         * events.EventEmitter
         *   1. change
         *   2. error
         */
        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "change", listener: (eventType: string, filename: string | Buffer) => void): this;
        addListener(event: "error", listener: (error: Error) => void): this;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "change", listener: (eventType: string, filename: string | Buffer) => void): this;
        on(event: "error", listener: (error: Error) => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "change", listener: (eventType: string, filename: string | Buffer) => void): this;
        once(event: "error", listener: (error: Error) => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "change", listener: (eventType: string, filename: string | Buffer) => void): this;
        prependListener(event: "error", listener: (error: Error) => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "change", listener: (eventType: string, filename: string | Buffer) => void): this;
        prependOnceListener(event: "error", listener: (error: Error) => void): this;
    }

    export class ReadStream extends stream.Readable {
        close(): void;
        destroy(): void;
        bytesRead: number;
        path: string | Buffer;

        /**
         * events.EventEmitter
         *   1. open
         *   2. close
         */
        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "open", listener: (fd: number) => void): this;
        addListener(event: "close", listener: () => void): this;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "open", listener: (fd: number) => void): this;
        on(event: "close", listener: () => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "open", listener: (fd: number) => void): this;
        once(event: "close", listener: () => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "open", listener: (fd: number) => void): this;
        prependListener(event: "close", listener: () => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "open", listener: (fd: number) => void): this;
        prependOnceListener(event: "close", listener: () => void): this;
    }

    export class WriteStream extends stream.Writable {
        close(): void;
        bytesWritten: number;
        path: string | Buffer;

        /**
         * events.EventEmitter
         *   1. open
         *   2. close
         */
        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "open", listener: (fd: number) => void): this;
        addListener(event: "close", listener: () => void): this;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "open", listener: (fd: number) => void): this;
        on(event: "close", listener: () => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "open", listener: (fd: number) => void): this;
        once(event: "close", listener: () => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "open", listener: (fd: number) => void): this;
        prependListener(event: "close", listener: () => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "open", listener: (fd: number) => void): this;
        prependOnceListener(event: "close", listener: () => void): this;
    }

    /**
     * Asynchronous rename(2) - Change the name or location of a file or directory.
     * @param oldPath A path to a file. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     * @param newPath A path to a file. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     */
    export function rename(oldPath: PathLike, newPath: PathLike, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace rename {
        /**
         * Asynchronous rename(2) - Change the name or location of a file or directory.
         * @param oldPath A path to a file. If a URL is provided, it must use the `file:` protocol.
         * URL support is _experimental_.
         * @param newPath A path to a file. If a URL is provided, it must use the `file:` protocol.
         * URL support is _experimental_.
         */
        export function __promisify__(oldPath: PathLike, newPath: PathLike): Promise<void>;
    }

    /**
     * Synchronous rename(2) - Change the name or location of a file or directory.
     * @param oldPath A path to a file. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     * @param newPath A path to a file. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     */
    export function renameSync(oldPath: PathLike, newPath: PathLike): void;

    /**
     * Asynchronous truncate(2) - Truncate a file to a specified length.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param len If not specified, defaults to `0`.
     */
    export function truncate(path: PathLike, len: number | undefined | null, callback: (err: NodeJS.ErrnoException) => void): void;

    /**
     * Asynchronous truncate(2) - Truncate a file to a specified length.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     */
    export function truncate(path: PathLike, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace truncate {
        /**
         * Asynchronous truncate(2) - Truncate a file to a specified length.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         * @param len If not specified, defaults to `0`.
         */
        export function __promisify__(path: PathLike, len?: number | null): Promise<void>;
    }

    /**
     * Synchronous truncate(2) - Truncate a file to a specified length.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param len If not specified, defaults to `0`.
     */
    export function truncateSync(path: PathLike, len?: number | null): void;

    /**
     * Asynchronous ftruncate(2) - Truncate a file to a specified length.
     * @param fd A file descriptor.
     * @param len If not specified, defaults to `0`.
     */
    export function ftruncate(fd: number, len: number | undefined | null, callback: (err: NodeJS.ErrnoException) => void): void;

    /**
     * Asynchronous ftruncate(2) - Truncate a file to a specified length.
     * @param fd A file descriptor.
     */
    export function ftruncate(fd: number, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace ftruncate {
        /**
         * Asynchronous ftruncate(2) - Truncate a file to a specified length.
         * @param fd A file descriptor.
         * @param len If not specified, defaults to `0`.
         */
        export function __promisify__(fd: number, len?: number | null): Promise<void>;
    }

    /**
     * Synchronous ftruncate(2) - Truncate a file to a specified length.
     * @param fd A file descriptor.
     * @param len If not specified, defaults to `0`.
     */
    export function ftruncateSync(fd: number, len?: number | null): void;

    /**
     * Asynchronous chown(2) - Change ownership of a file.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     */
    export function chown(path: PathLike, uid: number, gid: number, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace chown {
        /**
         * Asynchronous chown(2) - Change ownership of a file.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         */
        export function __promisify__(path: PathLike, uid: number, gid: number): Promise<void>;
    }

    /**
     * Synchronous chown(2) - Change ownership of a file.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     */
    export function chownSync(path: PathLike, uid: number, gid: number): void;

    /**
     * Asynchronous fchown(2) - Change ownership of a file.
     * @param fd A file descriptor.
     */
    export function fchown(fd: number, uid: number, gid: number, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace fchown {
        /**
         * Asynchronous fchown(2) - Change ownership of a file.
         * @param fd A file descriptor.
         */
        export function __promisify__(fd: number, uid: number, gid: number): Promise<void>;
    }

    /**
     * Synchronous fchown(2) - Change ownership of a file.
     * @param fd A file descriptor.
     */
    export function fchownSync(fd: number, uid: number, gid: number): void;

    /**
     * Asynchronous lchown(2) - Change ownership of a file. Does not dereference symbolic links.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     */
    export function lchown(path: PathLike, uid: number, gid: number, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace lchown {
        /**
         * Asynchronous lchown(2) - Change ownership of a file. Does not dereference symbolic links.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         */
        export function __promisify__(path: PathLike, uid: number, gid: number): Promise<void>;
    }

    /**
     * Synchronous lchown(2) - Change ownership of a file. Does not dereference symbolic links.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     */
    export function lchownSync(path: PathLike, uid: number, gid: number): void;

    /**
     * Asynchronous chmod(2) - Change permissions of a file.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param mode A file mode. If a string is passed, it is parsed as an octal integer.
     */
    export function chmod(path: PathLike, mode: string | number, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace chmod {
        /**
         * Asynchronous chmod(2) - Change permissions of a file.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         * @param mode A file mode. If a string is passed, it is parsed as an octal integer.
         */
        export function __promisify__(path: PathLike, mode: string | number): Promise<void>;
    }

    /**
     * Synchronous chmod(2) - Change permissions of a file.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param mode A file mode. If a string is passed, it is parsed as an octal integer.
     */
    export function chmodSync(path: PathLike, mode: string | number): void;

    /**
     * Asynchronous fchmod(2) - Change permissions of a file.
     * @param fd A file descriptor.
     * @param mode A file mode. If a string is passed, it is parsed as an octal integer.
     */
    export function fchmod(fd: number, mode: string | number, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace fchmod {
        /**
         * Asynchronous fchmod(2) - Change permissions of a file.
         * @param fd A file descriptor.
         * @param mode A file mode. If a string is passed, it is parsed as an octal integer.
         */
        export function __promisify__(fd: number, mode: string | number): Promise<void>;
    }

    /**
     * Synchronous fchmod(2) - Change permissions of a file.
     * @param fd A file descriptor.
     * @param mode A file mode. If a string is passed, it is parsed as an octal integer.
     */
    export function fchmodSync(fd: number, mode: string | number): void;

    /**
     * Asynchronous lchmod(2) - Change permissions of a file. Does not dereference symbolic links.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param mode A file mode. If a string is passed, it is parsed as an octal integer.
     */
    export function lchmod(path: PathLike, mode: string | number, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace lchmod {
        /**
         * Asynchronous lchmod(2) - Change permissions of a file. Does not dereference symbolic links.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         * @param mode A file mode. If a string is passed, it is parsed as an octal integer.
         */
        export function __promisify__(path: PathLike, mode: string | number): Promise<void>;
    }

    /**
     * Synchronous lchmod(2) - Change permissions of a file. Does not dereference symbolic links.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param mode A file mode. If a string is passed, it is parsed as an octal integer.
     */
    export function lchmodSync(path: PathLike, mode: string | number): void;

    /**
     * Asynchronous stat(2) - Get file status.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     */
    export function stat(path: PathLike, callback: (err: NodeJS.ErrnoException, stats: Stats) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace stat {
        /**
         * Asynchronous stat(2) - Get file status.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         */
        export function __promisify__(path: PathLike): Promise<Stats>;
    }

    /**
     * Synchronous stat(2) - Get file status.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     */
    export function statSync(path: PathLike): Stats;

    /**
     * Asynchronous fstat(2) - Get file status.
     * @param fd A file descriptor.
     */
    export function fstat(fd: number, callback: (err: NodeJS.ErrnoException, stats: Stats) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace fstat {
        /**
         * Asynchronous fstat(2) - Get file status.
         * @param fd A file descriptor.
         */
        export function __promisify__(fd: number): Promise<Stats>;
    }

    /**
     * Synchronous fstat(2) - Get file status.
     * @param fd A file descriptor.
     */
    export function fstatSync(fd: number): Stats;

    /**
     * Asynchronous lstat(2) - Get file status. Does not dereference symbolic links.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     */
    export function lstat(path: PathLike, callback: (err: NodeJS.ErrnoException, stats: Stats) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace lstat {
        /**
         * Asynchronous lstat(2) - Get file status. Does not dereference symbolic links.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         */
        export function __promisify__(path: PathLike): Promise<Stats>;
    }

    /**
     * Synchronous lstat(2) - Get file status. Does not dereference symbolic links.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     */
    export function lstatSync(path: PathLike): Stats;

    /**
     * Asynchronous link(2) - Create a new link (also known as a hard link) to an existing file.
     * @param existingPath A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param newPath A path to a file. If a URL is provided, it must use the `file:` protocol.
     */
    export function link(existingPath: PathLike, newPath: PathLike, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace link {
        /**
         * Asynchronous link(2) - Create a new link (also known as a hard link) to an existing file.
         * @param existingPath A path to a file. If a URL is provided, it must use the `file:` protocol.
         * @param newPath A path to a file. If a URL is provided, it must use the `file:` protocol.
         */
        export function link(existingPath: PathLike, newPath: PathLike): Promise<void>;
    }

    /**
     * Synchronous link(2) - Create a new link (also known as a hard link) to an existing file.
     * @param existingPath A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param newPath A path to a file. If a URL is provided, it must use the `file:` protocol.
     */
    export function linkSync(existingPath: PathLike, newPath: PathLike): void;

    /**
     * Asynchronous symlink(2) - Create a new symbolic link to an existing file.
     * @param target A path to an existing file. If a URL is provided, it must use the `file:` protocol.
     * @param path A path to the new symlink. If a URL is provided, it must use the `file:` protocol.
     * @param type May be set to `'dir'`, `'file'`, or `'junction'` (default is `'file'`) and is only available on Windows (ignored on other platforms).
     * When using `'junction'`, the `target` argument will automatically be normalized to an absolute path.
     */
    export function symlink(target: PathLike, path: PathLike, type: symlink.Type | undefined | null, callback: (err: NodeJS.ErrnoException) => void): void;

    /**
     * Asynchronous symlink(2) - Create a new symbolic link to an existing file.
     * @param target A path to an existing file. If a URL is provided, it must use the `file:` protocol.
     * @param path A path to the new symlink. If a URL is provided, it must use the `file:` protocol.
     */
    export function symlink(target: PathLike, path: PathLike, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace symlink {
        /**
         * Asynchronous symlink(2) - Create a new symbolic link to an existing file.
         * @param target A path to an existing file. If a URL is provided, it must use the `file:` protocol.
         * @param path A path to the new symlink. If a URL is provided, it must use the `file:` protocol.
         * @param type May be set to `'dir'`, `'file'`, or `'junction'` (default is `'file'`) and is only available on Windows (ignored on other platforms).
         * When using `'junction'`, the `target` argument will automatically be normalized to an absolute path.
         */
        export function __promisify__(target: PathLike, path: PathLike, type?: string | null): Promise<void>;

        export type Type = "dir" | "file" | "junction";
    }

    /**
     * Synchronous symlink(2) - Create a new symbolic link to an existing file.
     * @param target A path to an existing file. If a URL is provided, it must use the `file:` protocol.
     * @param path A path to the new symlink. If a URL is provided, it must use the `file:` protocol.
     * @param type May be set to `'dir'`, `'file'`, or `'junction'` (default is `'file'`) and is only available on Windows (ignored on other platforms).
     * When using `'junction'`, the `target` argument will automatically be normalized to an absolute path.
     */
    export function symlinkSync(target: PathLike, path: PathLike, type?: symlink.Type | null): void;

    /**
     * Asynchronous readlink(2) - read value of a symbolic link.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function readlink(path: PathLike, options: { encoding?: BufferEncoding | null } | BufferEncoding | undefined | null, callback: (err: NodeJS.ErrnoException, linkString: string) => void): void;

    /**
     * Asynchronous readlink(2) - read value of a symbolic link.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function readlink(path: PathLike, options: { encoding: "buffer" } | "buffer", callback: (err: NodeJS.ErrnoException, linkString: Buffer) => void): void;

    /**
     * Asynchronous readlink(2) - read value of a symbolic link.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function readlink(path: PathLike, options: { encoding?: string | null } | string | undefined | null, callback: (err: NodeJS.ErrnoException, linkString: string | Buffer) => void): void;

    /**
     * Asynchronous readlink(2) - read value of a symbolic link.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     */
    export function readlink(path: PathLike, callback: (err: NodeJS.ErrnoException, linkString: string) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace readlink {
        /**
         * Asynchronous readlink(2) - read value of a symbolic link.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
         */
        export function __promisify__(path: PathLike, options?: { encoding?: BufferEncoding | null } | BufferEncoding | null): Promise<string>;

        /**
         * Asynchronous readlink(2) - read value of a symbolic link.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
         */
        export function __promisify__(path: PathLike, options: { encoding: "buffer" } | "buffer"): Promise<Buffer>;

        /**
         * Asynchronous readlink(2) - read value of a symbolic link.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
         */
        export function __promisify__(path: PathLike, options?: { encoding?: string | null } | string | null): Promise<string | Buffer>;
    }

    /**
     * Synchronous readlink(2) - read value of a symbolic link.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function readlinkSync(path: PathLike, options?: { encoding?: BufferEncoding | null } | BufferEncoding | null): string;

    /**
     * Synchronous readlink(2) - read value of a symbolic link.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function readlinkSync(path: PathLike, options: { encoding: "buffer" } | "buffer"): Buffer;

    /**
     * Synchronous readlink(2) - read value of a symbolic link.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function readlinkSync(path: PathLike, options?: { encoding?: string | null } | string | null): string | Buffer;

    /**
     * Asynchronous realpath(3) - return the canonicalized absolute pathname.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function realpath(path: PathLike, options: { encoding?: BufferEncoding | null } | BufferEncoding | undefined | null, callback: (err: NodeJS.ErrnoException, resolvedPath: string) => void): void;

    /**
     * Asynchronous realpath(3) - return the canonicalized absolute pathname.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function realpath(path: PathLike, options: { encoding: "buffer" } | "buffer", callback: (err: NodeJS.ErrnoException, resolvedPath: Buffer) => void): void;

    /**
     * Asynchronous realpath(3) - return the canonicalized absolute pathname.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function realpath(path: PathLike, options: { encoding?: string | null } | string | undefined | null, callback: (err: NodeJS.ErrnoException, resolvedPath: string | Buffer) => void): void;

    /**
     * Asynchronous realpath(3) - return the canonicalized absolute pathname.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     */
    export function realpath(path: PathLike, callback: (err: NodeJS.ErrnoException, resolvedPath: string) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace realpath {
        /**
         * Asynchronous realpath(3) - return the canonicalized absolute pathname.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
         */
        export function __promisify__(path: PathLike, options?: { encoding?: BufferEncoding | null } | BufferEncoding | null): Promise<string>;

        /**
         * Asynchronous realpath(3) - return the canonicalized absolute pathname.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
         */
        export function __promisify__(path: PathLike, options: { encoding: "buffer" } | "buffer"): Promise<Buffer>;

        /**
         * Asynchronous realpath(3) - return the canonicalized absolute pathname.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
         */
        export function __promisify__(path: PathLike, options?: { encoding?: string | null } | string | null): Promise<string | Buffer>;
    }

    /**
     * Synchronous realpath(3) - return the canonicalized absolute pathname.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function realpathSync(path: PathLike, options?: { encoding?: BufferEncoding | null } | BufferEncoding | null): string;

    /**
     * Synchronous realpath(3) - return the canonicalized absolute pathname.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function realpathSync(path: PathLike, options: { encoding: "buffer" } | "buffer"): Buffer;

    /**
     * Synchronous realpath(3) - return the canonicalized absolute pathname.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function realpathSync(path: PathLike, options?: { encoding?: string | null } | string | null): string | Buffer;

    /**
     * Asynchronous unlink(2) - delete a name and possibly the file it refers to.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     */
    export function unlink(path: PathLike, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace unlink {
        /**
         * Asynchronous unlink(2) - delete a name and possibly the file it refers to.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         */
        export function __promisify__(path: PathLike): Promise<void>;
    }

    /**
     * Synchronous unlink(2) - delete a name and possibly the file it refers to.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     */
    export function unlinkSync(path: PathLike): void;

    /**
     * Asynchronous rmdir(2) - delete a directory.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     */
    export function rmdir(path: PathLike, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace rmdir {
        /**
         * Asynchronous rmdir(2) - delete a directory.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         */
        export function __promisify__(path: PathLike): Promise<void>;
    }

    /**
     * Synchronous rmdir(2) - delete a directory.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     */
    export function rmdirSync(path: PathLike): void;

    /**
     * Asynchronous mkdir(2) - create a directory.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param mode A file mode. If a string is passed, it is parsed as an octal integer. If not specified, defaults to `0o777`.
     */
    export function mkdir(path: PathLike, mode: number | string | undefined | null, callback: (err: NodeJS.ErrnoException) => void): void;

    /**
     * Asynchronous mkdir(2) - create a directory with a mode of `0o777`.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     */
    export function mkdir(path: PathLike, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace mkdir {
        /**
         * Asynchronous mkdir(2) - create a directory.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         * @param mode A file mode. If a string is passed, it is parsed as an octal integer. If not specified, defaults to `0o777`.
         */
        export function __promisify__(path: PathLike, mode?: number | string | null): Promise<void>;
    }

    /**
     * Synchronous mkdir(2) - create a directory.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param mode A file mode. If a string is passed, it is parsed as an octal integer. If not specified, defaults to `0o777`.
     */
    export function mkdirSync(path: PathLike, mode?: number | string | null): void;

    /**
     * Asynchronously creates a unique temporary directory.
     * Generates six random characters to be appended behind a required prefix to create a unique temporary directory.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function mkdtemp(prefix: string, options: { encoding?: BufferEncoding | null } | BufferEncoding | undefined | null, callback: (err: NodeJS.ErrnoException, folder: string) => void): void;

    /**
     * Asynchronously creates a unique temporary directory.
     * Generates six random characters to be appended behind a required prefix to create a unique temporary directory.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function mkdtemp(prefix: string, options: "buffer" | { encoding: "buffer" }, callback: (err: NodeJS.ErrnoException, folder: Buffer) => void): void;

    /**
     * Asynchronously creates a unique temporary directory.
     * Generates six random characters to be appended behind a required prefix to create a unique temporary directory.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function mkdtemp(prefix: string, options: { encoding?: string | null } | string | undefined | null, callback: (err: NodeJS.ErrnoException, folder: string | Buffer) => void): void;

    /**
     * Asynchronously creates a unique temporary directory.
     * Generates six random characters to be appended behind a required prefix to create a unique temporary directory.
     */
    export function mkdtemp(prefix: string, callback: (err: NodeJS.ErrnoException, folder: string) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace mkdtemp {
        /**
         * Asynchronously creates a unique temporary directory.
         * Generates six random characters to be appended behind a required prefix to create a unique temporary directory.
         * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
         */
        export function __promisify__(prefix: string, options?: { encoding?: BufferEncoding | null } | BufferEncoding | null): Promise<string>;

        /**
         * Asynchronously creates a unique temporary directory.
         * Generates six random characters to be appended behind a required prefix to create a unique temporary directory.
         * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
         */
        export function __promisify__(prefix: string, options: { encoding: "buffer" } | "buffer"): Promise<Buffer>;

        /**
         * Asynchronously creates a unique temporary directory.
         * Generates six random characters to be appended behind a required prefix to create a unique temporary directory.
         * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
         */
        export function __promisify__(prefix: string, options?: { encoding?: string | null } | string | null): Promise<string | Buffer>;
    }

    /**
     * Synchronously creates a unique temporary directory.
     * Generates six random characters to be appended behind a required prefix to create a unique temporary directory.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function mkdtempSync(prefix: string, options?: { encoding?: BufferEncoding | null } | BufferEncoding | null): string;

    /**
     * Synchronously creates a unique temporary directory.
     * Generates six random characters to be appended behind a required prefix to create a unique temporary directory.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function mkdtempSync(prefix: string, options: { encoding: "buffer" } | "buffer"): Buffer;

    /**
     * Synchronously creates a unique temporary directory.
     * Generates six random characters to be appended behind a required prefix to create a unique temporary directory.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function mkdtempSync(prefix: string, options?: { encoding?: string | null } | string | null): string | Buffer;

    /**
     * Asynchronous readdir(3) - read a directory.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function readdir(path: PathLike, options: { encoding: BufferEncoding | null } | BufferEncoding | undefined | null, callback: (err: NodeJS.ErrnoException, files: string[]) => void): void;

    /**
     * Asynchronous readdir(3) - read a directory.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function readdir(path: PathLike, options: { encoding: "buffer" } | "buffer", callback: (err: NodeJS.ErrnoException, files: Buffer[]) => void): void;

    /**
     * Asynchronous readdir(3) - read a directory.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function readdir(path: PathLike, options: { encoding?: string | null } | string | undefined | null, callback: (err: NodeJS.ErrnoException, files: string[] | Buffer[]) => void): void;

    /**
     * Asynchronous readdir(3) - read a directory.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     */
    export function readdir(path: PathLike, callback: (err: NodeJS.ErrnoException, files: string[]) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace readdir {
        /**
         * Asynchronous readdir(3) - read a directory.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
         */
        export function __promisify__(path: PathLike, options?: { encoding: BufferEncoding | null } | BufferEncoding | null): Promise<string[]>;

        /**
         * Asynchronous readdir(3) - read a directory.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
         */
        export function __promisify__(path: PathLike, options: "buffer" | { encoding: "buffer" }): Promise<Buffer[]>;

        /**
         * Asynchronous readdir(3) - read a directory.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
         */
        export function __promisify__(path: PathLike, options?: { encoding?: string | null } | string | null): Promise<string[] | Buffer[]>;
    }

    /**
     * Synchronous readdir(3) - read a directory.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function readdirSync(path: PathLike, options?: { encoding: BufferEncoding | null } | BufferEncoding | null): string[];

    /**
     * Synchronous readdir(3) - read a directory.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function readdirSync(path: PathLike, options: { encoding: "buffer" } | "buffer"): Buffer[];

    /**
     * Synchronous readdir(3) - read a directory.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param options The encoding (or an object specifying the encoding), used as the encoding of the result. If not provided, `'utf8'` is used.
     */
    export function readdirSync(path: PathLike, options?: { encoding?: string | null } | string | null): string[] | Buffer[];

    /**
     * Asynchronous close(2) - close a file descriptor.
     * @param fd A file descriptor.
     */
    export function close(fd: number, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace close {
        /**
         * Asynchronous close(2) - close a file descriptor.
         * @param fd A file descriptor.
         */
        export function __promisify__(fd: number): Promise<void>;
    }

    /**
     * Synchronous close(2) - close a file descriptor.
     * @param fd A file descriptor.
     */
    export function closeSync(fd: number): void;

    /**
     * Asynchronous open(2) - open and possibly create a file.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param mode A file mode. If a string is passed, it is parsed as an octal integer. If not supplied, defaults to `0o666`.
     */
    export function open(path: PathLike, flags: string | number, mode: string | number | undefined | null, callback: (err: NodeJS.ErrnoException, fd: number) => void): void;

    /**
     * Asynchronous open(2) - open and possibly create a file. If the file is created, its mode will be `0o666`.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     */
    export function open(path: PathLike, flags: string | number, callback: (err: NodeJS.ErrnoException, fd: number) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace open {
        /**
         * Asynchronous open(2) - open and possibly create a file.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         * @param mode A file mode. If a string is passed, it is parsed as an octal integer. If not supplied, defaults to `0o666`.
         */
        export function __promisify__(path: PathLike, flags: string | number, mode?: string | number | null): Promise<number>;
    }

    /**
     * Synchronous open(2) - open and possibly create a file, returning a file descriptor..
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param mode A file mode. If a string is passed, it is parsed as an octal integer. If not supplied, defaults to `0o666`.
     */
    export function openSync(path: PathLike, flags: string | number, mode?: string | number | null): number;

    /**
     * Asynchronously change file timestamps of the file referenced by the supplied path.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param atime The last access time. If a string is provided, it will be coerced to number.
     * @param mtime The last modified time. If a string is provided, it will be coerced to number.
     */
    export function utimes(path: PathLike, atime: string | number | Date, mtime: string | number | Date, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace utimes {
        /**
         * Asynchronously change file timestamps of the file referenced by the supplied path.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         * @param atime The last access time. If a string is provided, it will be coerced to number.
         * @param mtime The last modified time. If a string is provided, it will be coerced to number.
         */
        export function __promisify__(path: PathLike, atime: string | number | Date, mtime: string | number | Date): Promise<void>;
    }

    /**
     * Synchronously change file timestamps of the file referenced by the supplied path.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * @param atime The last access time. If a string is provided, it will be coerced to number.
     * @param mtime The last modified time. If a string is provided, it will be coerced to number.
     */
    export function utimesSync(path: PathLike, atime: string | number | Date, mtime: string | number | Date): void;

    /**
     * Asynchronously change file timestamps of the file referenced by the supplied file descriptor.
     * @param fd A file descriptor.
     * @param atime The last access time. If a string is provided, it will be coerced to number.
     * @param mtime The last modified time. If a string is provided, it will be coerced to number.
     */
    export function futimes(fd: number, atime: string | number | Date, mtime: string | number | Date, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace futimes {
        /**
         * Asynchronously change file timestamps of the file referenced by the supplied file descriptor.
         * @param fd A file descriptor.
         * @param atime The last access time. If a string is provided, it will be coerced to number.
         * @param mtime The last modified time. If a string is provided, it will be coerced to number.
         */
        export function __promisify__(fd: number, atime: string | number | Date, mtime: string | number | Date): Promise<void>;
    }

    /**
     * Synchronously change file timestamps of the file referenced by the supplied file descriptor.
     * @param fd A file descriptor.
     * @param atime The last access time. If a string is provided, it will be coerced to number.
     * @param mtime The last modified time. If a string is provided, it will be coerced to number.
     */
    export function futimesSync(fd: number, atime: string | number | Date, mtime: string | number | Date): void;

    /**
     * Asynchronous fsync(2) - synchronize a file's in-core state with the underlying storage device.
     * @param fd A file descriptor.
     */
    export function fsync(fd: number, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace fsync {
        /**
         * Asynchronous fsync(2) - synchronize a file's in-core state with the underlying storage device.
         * @param fd A file descriptor.
         */
        export function __promisify__(fd: number): Promise<void>;
    }

    /**
     * Synchronous fsync(2) - synchronize a file's in-core state with the underlying storage device.
     * @param fd A file descriptor.
     */
    export function fsyncSync(fd: number): void;

    /**
     * Asynchronously writes `buffer` to the file referenced by the supplied file descriptor.
     * @param fd A file descriptor.
     * @param offset The part of the buffer to be written. If not supplied, defaults to `0`.
     * @param length The number of bytes to write. If not supplied, defaults to `buffer.length - offset`.
     * @param position The offset from the beginning of the file where this data should be written. If not supplied, defaults to the current position.
     */
    export function write<TBuffer extends Buffer | Uint8Array>(fd: number, buffer: TBuffer, offset: number | undefined | null, length: number | undefined | null, position: number | undefined | null, callback: (err: NodeJS.ErrnoException, written: number, buffer: TBuffer) => void): void;

    /**
     * Asynchronously writes `buffer` to the file referenced by the supplied file descriptor.
     * @param fd A file descriptor.
     * @param offset The part of the buffer to be written. If not supplied, defaults to `0`.
     * @param length The number of bytes to write. If not supplied, defaults to `buffer.length - offset`.
     */
    export function write<TBuffer extends Buffer | Uint8Array>(fd: number, buffer: TBuffer, offset: number | undefined | null, length: number | undefined | null, callback: (err: NodeJS.ErrnoException, written: number, buffer: TBuffer) => void): void;

    /**
     * Asynchronously writes `buffer` to the file referenced by the supplied file descriptor.
     * @param fd A file descriptor.
     * @param offset The part of the buffer to be written. If not supplied, defaults to `0`.
     */
    export function write<TBuffer extends Buffer | Uint8Array>(fd: number, buffer: TBuffer, offset: number | undefined | null, callback: (err: NodeJS.ErrnoException, written: number, buffer: TBuffer) => void): void;

    /**
     * Asynchronously writes `buffer` to the file referenced by the supplied file descriptor.
     * @param fd A file descriptor.
     */
    export function write<TBuffer extends Buffer | Uint8Array>(fd: number, buffer: TBuffer, callback: (err: NodeJS.ErrnoException, written: number, buffer: TBuffer) => void): void;

    /**
     * Asynchronously writes `string` to the file referenced by the supplied file descriptor.
     * @param fd A file descriptor.
     * @param string A string to write. If something other than a string is supplied it will be coerced to a string.
     * @param position The offset from the beginning of the file where this data should be written. If not supplied, defaults to the current position.
     * @param encoding The expected string encoding.
     */
    export function write(fd: number, string: any, position: number | undefined | null, encoding: string | undefined | null, callback: (err: NodeJS.ErrnoException, written: number, str: string) => void): void;

    /**
     * Asynchronously writes `string` to the file referenced by the supplied file descriptor.
     * @param fd A file descriptor.
     * @param string A string to write. If something other than a string is supplied it will be coerced to a string.
     * @param position The offset from the beginning of the file where this data should be written. If not supplied, defaults to the current position.
     */
    export function write(fd: number, string: any, position: number | undefined | null, callback: (err: NodeJS.ErrnoException, written: number, str: string) => void): void;

    /**
     * Asynchronously writes `string` to the file referenced by the supplied file descriptor.
     * @param fd A file descriptor.
     * @param string A string to write. If something other than a string is supplied it will be coerced to a string.
     */
    export function write(fd: number, string: any, callback: (err: NodeJS.ErrnoException, written: number, str: string) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace write {
        /**
         * Asynchronously writes `buffer` to the file referenced by the supplied file descriptor.
         * @param fd A file descriptor.
         * @param offset The part of the buffer to be written. If not supplied, defaults to `0`.
         * @param length The number of bytes to write. If not supplied, defaults to `buffer.length - offset`.
         * @param position The offset from the beginning of the file where this data should be written. If not supplied, defaults to the current position.
         */
        export function __promisify__<TBuffer extends Buffer | Uint8Array>(fd: number, buffer?: TBuffer, offset?: number, length?: number, position?: number | null): Promise<{ bytesWritten: number, buffer: TBuffer }>;

        /**
         * Asynchronously writes `string` to the file referenced by the supplied file descriptor.
         * @param fd A file descriptor.
         * @param string A string to write. If something other than a string is supplied it will be coerced to a string.
         * @param position The offset from the beginning of the file where this data should be written. If not supplied, defaults to the current position.
         * @param encoding The expected string encoding.
         */
        export function __promisify__(fd: number, string: any, position?: number | null, encoding?: string | null): Promise<{ bytesWritten: number, buffer: string }>;
    }

    /**
     * Synchronously writes `buffer` to the file referenced by the supplied file descriptor, returning the number of bytes written.
     * @param fd A file descriptor.
     * @param offset The part of the buffer to be written. If not supplied, defaults to `0`.
     * @param length The number of bytes to write. If not supplied, defaults to `buffer.length - offset`.
     * @param position The offset from the beginning of the file where this data should be written. If not supplied, defaults to the current position.
     */
    export function writeSync(fd: number, buffer: Buffer | Uint8Array, offset?: number | null, length?: number | null, position?: number | null): number;

    /**
     * Synchronously writes `string` to the file referenced by the supplied file descriptor, returning the number of bytes written.
     * @param fd A file descriptor.
     * @param string A string to write. If something other than a string is supplied it will be coerced to a string.
     * @param position The offset from the beginning of the file where this data should be written. If not supplied, defaults to the current position.
     * @param encoding The expected string encoding.
     */
    export function writeSync(fd: number, string: any, position?: number | null, encoding?: string | null): number;

    /**
     * Asynchronously reads data from the file referenced by the supplied file descriptor.
     * @param fd A file descriptor.
     * @param buffer The buffer that the data will be written to.
     * @param offset The offset in the buffer at which to start writing.
     * @param length The number of bytes to read.
     * @param position The offset from the beginning of the file from which data should be read. If `null`, data will be read from the current position.
     */
    export function read<TBuffer extends Buffer | Uint8Array>(fd: number, buffer: TBuffer, offset: number, length: number, position: number | null, callback?: (err: NodeJS.ErrnoException, bytesRead: number, buffer: TBuffer) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace read {
        /**
         * @param fd A file descriptor.
         * @param buffer The buffer that the data will be written to.
         * @param offset The offset in the buffer at which to start writing.
         * @param length The number of bytes to read.
         * @param position The offset from the beginning of the file from which data should be read. If `null`, data will be read from the current position.
         */
        export function __promisify__<TBuffer extends Buffer | Uint8Array>(fd: number, buffer: TBuffer, offset: number, length: number, position: number | null): Promise<{ bytesRead: number, buffer: TBuffer }>;
    }

    /**
     * Synchronously reads data from the file referenced by the supplied file descriptor, returning the number of bytes read.
     * @param fd A file descriptor.
     * @param buffer The buffer that the data will be written to.
     * @param offset The offset in the buffer at which to start writing.
     * @param length The number of bytes to read.
     * @param position The offset from the beginning of the file from which data should be read. If `null`, data will be read from the current position.
     */
    export function readSync(fd: number, buffer: Buffer | Uint8Array, offset: number, length: number, position: number | null): number;

    /**
     * Asynchronously reads the entire contents of a file.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * If a file descriptor is provided, the underlying file will _not_ be closed automatically.
     * @param options An object that may contain an optional flag.
     * If a flag is not provided, it defaults to `'r'`.
     */
    export function readFile(path: PathLike | number, options: { encoding?: null; flag?: string; } | undefined | null, callback: (err: NodeJS.ErrnoException, data: Buffer) => void): void;

    /**
     * Asynchronously reads the entire contents of a file.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     * If a file descriptor is provided, the underlying file will _not_ be closed automatically.
     * @param options Either the encoding for the result, or an object that contains the encoding and an optional flag.
     * If a flag is not provided, it defaults to `'r'`.
     */
    export function readFile(path: PathLike | number, options: { encoding: string; flag?: string; } | string, callback: (err: NodeJS.ErrnoException, data: string) => void): void;

    /**
     * Asynchronously reads the entire contents of a file.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     * If a file descriptor is provided, the underlying file will _not_ be closed automatically.
     * @param options Either the encoding for the result, or an object that contains the encoding and an optional flag.
     * If a flag is not provided, it defaults to `'r'`.
     */
    export function readFile(path: PathLike | number, options: { encoding?: string | null; flag?: string; } | string | undefined | null, callback: (err: NodeJS.ErrnoException, data: string | Buffer) => void): void;

    /**
     * Asynchronously reads the entire contents of a file.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * If a file descriptor is provided, the underlying file will _not_ be closed automatically.
     */
    export function readFile(path: PathLike | number, callback: (err: NodeJS.ErrnoException, data: Buffer) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace readFile {
        /**
         * Asynchronously reads the entire contents of a file.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         * If a file descriptor is provided, the underlying file will _not_ be closed automatically.
         * @param options An object that may contain an optional flag.
         * If a flag is not provided, it defaults to `'r'`.
         */
        export function __promisify__(path: PathLike | number, options?: { encoding?: null; flag?: string; } | null): Promise<Buffer>;

        /**
         * Asynchronously reads the entire contents of a file.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         * URL support is _experimental_.
         * If a file descriptor is provided, the underlying file will _not_ be closed automatically.
         * @param options Either the encoding for the result, or an object that contains the encoding and an optional flag.
         * If a flag is not provided, it defaults to `'r'`.
         */
        export function __promisify__(path: PathLike | number, options: { encoding: string; flag?: string; } | string): Promise<string>;

        /**
         * Asynchronously reads the entire contents of a file.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         * URL support is _experimental_.
         * If a file descriptor is provided, the underlying file will _not_ be closed automatically.
         * @param options Either the encoding for the result, or an object that contains the encoding and an optional flag.
         * If a flag is not provided, it defaults to `'r'`.
         */
        export function __promisify__(path: PathLike | number, options?: { encoding?: string | null; flag?: string; } | string | null): Promise<string | Buffer>;
    }

    /**
     * Synchronously reads the entire contents of a file.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     * If a file descriptor is provided, the underlying file will _not_ be closed automatically.
     * @param options An object that may contain an optional flag. If a flag is not provided, it defaults to `'r'`.
     */
    export function readFileSync(path: PathLike | number, options?: { encoding?: null; flag?: string; } | null): Buffer;

    /**
     * Synchronously reads the entire contents of a file.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     * If a file descriptor is provided, the underlying file will _not_ be closed automatically.
     * @param options Either the encoding for the result, or an object that contains the encoding and an optional flag.
     * If a flag is not provided, it defaults to `'r'`.
     */
    export function readFileSync(path: PathLike | number, options: { encoding: string; flag?: string; } | string): string;

    /**
     * Synchronously reads the entire contents of a file.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     * If a file descriptor is provided, the underlying file will _not_ be closed automatically.
     * @param options Either the encoding for the result, or an object that contains the encoding and an optional flag.
     * If a flag is not provided, it defaults to `'r'`.
     */
    export function readFileSync(path: PathLike | number, options?: { encoding?: string | null; flag?: string; } | string | null): string | Buffer;

    /**
     * Asynchronously writes data to a file, replacing the file if it already exists.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     * If a file descriptor is provided, the underlying file will _not_ be closed automatically.
     * @param data The data to write. If something other than a Buffer or Uint8Array is provided, the value is coerced to a string.
     * @param options Either the encoding for the file, or an object optionally specifying the encoding, file mode, and flag.
     * If `encoding` is not supplied, the default of `'utf8'` is used.
     * If `mode` is not supplied, the default of `0o666` is used.
     * If `mode` is a string, it is parsed as an octal integer.
     * If `flag` is not supplied, the default of `'w'` is used.
     */
    export function writeFile(path: PathLike | number, data: any, options: { encoding?: string | null; mode?: number | string; flag?: string; } | string | undefined | null, callback: (err: NodeJS.ErrnoException) => void): void;

    /**
     * Asynchronously writes data to a file, replacing the file if it already exists.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     * If a file descriptor is provided, the underlying file will _not_ be closed automatically.
     * @param data The data to write. If something other than a Buffer or Uint8Array is provided, the value is coerced to a string.
     */
    export function writeFile(path: PathLike | number, data: any, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace writeFile {
        /**
         * Asynchronously writes data to a file, replacing the file if it already exists.
         * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
         * URL support is _experimental_.
         * If a file descriptor is provided, the underlying file will _not_ be closed automatically.
         * @param data The data to write. If something other than a Buffer or Uint8Array is provided, the value is coerced to a string.
         * @param options Either the encoding for the file, or an object optionally specifying the encoding, file mode, and flag.
         * If `encoding` is not supplied, the default of `'utf8'` is used.
         * If `mode` is not supplied, the default of `0o666` is used.
         * If `mode` is a string, it is parsed as an octal integer.
         * If `flag` is not supplied, the default of `'w'` is used.
         */
        export function __promisify__(path: PathLike | number, data: any, options?: { encoding?: string | null; mode?: number | string; flag?: string; } | string | null): Promise<void>;
    }

    /**
     * Synchronously writes data to a file, replacing the file if it already exists.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     * If a file descriptor is provided, the underlying file will _not_ be closed automatically.
     * @param data The data to write. If something other than a Buffer or Uint8Array is provided, the value is coerced to a string.
     * @param options Either the encoding for the file, or an object optionally specifying the encoding, file mode, and flag.
     * If `encoding` is not supplied, the default of `'utf8'` is used.
     * If `mode` is not supplied, the default of `0o666` is used.
     * If `mode` is a string, it is parsed as an octal integer.
     * If `flag` is not supplied, the default of `'w'` is used.
     */
    export function writeFileSync(path: PathLike | number, data: any, options?: { encoding?: string | null; mode?: number | string; flag?: string; } | string | null): void;

    /**
     * Asynchronously append data to a file, creating the file if it does not exist.
     * @param file A path to a file. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     * If a file descriptor is provided, the underlying file will _not_ be closed automatically.
     * @param data The data to write. If something other than a Buffer or Uint8Array is provided, the value is coerced to a string.
     * @param options Either the encoding for the file, or an object optionally specifying the encoding, file mode, and flag.
     * If `encoding` is not supplied, the default of `'utf8'` is used.
     * If `mode` is not supplied, the default of `0o666` is used.
     * If `mode` is a string, it is parsed as an octal integer.
     * If `flag` is not supplied, the default of `'a'` is used.
     */
    export function appendFile(file: PathLike | number, data: any, options: { encoding?: string | null, mode?: string | number, flag?: string } | string | undefined | null, callback: (err: NodeJS.ErrnoException) => void): void;

    /**
     * Asynchronously append data to a file, creating the file if it does not exist.
     * @param file A path to a file. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     * If a file descriptor is provided, the underlying file will _not_ be closed automatically.
     * @param data The data to write. If something other than a Buffer or Uint8Array is provided, the value is coerced to a string.
     */
    export function appendFile(file: PathLike | number, data: any, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace appendFile {
        /**
         * Asynchronously append data to a file, creating the file if it does not exist.
         * @param file A path to a file. If a URL is provided, it must use the `file:` protocol.
         * URL support is _experimental_.
         * If a file descriptor is provided, the underlying file will _not_ be closed automatically.
         * @param data The data to write. If something other than a Buffer or Uint8Array is provided, the value is coerced to a string.
         * @param options Either the encoding for the file, or an object optionally specifying the encoding, file mode, and flag.
         * If `encoding` is not supplied, the default of `'utf8'` is used.
         * If `mode` is not supplied, the default of `0o666` is used.
         * If `mode` is a string, it is parsed as an octal integer.
         * If `flag` is not supplied, the default of `'a'` is used.
         */
        export function __promisify__(file: PathLike | number, data: any, options?: { encoding?: string | null, mode?: string | number, flag?: string } | string | null): Promise<void>;
    }

    /**
     * Synchronously append data to a file, creating the file if it does not exist.
     * @param file A path to a file. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     * If a file descriptor is provided, the underlying file will _not_ be closed automatically.
     * @param data The data to write. If something other than a Buffer or Uint8Array is provided, the value is coerced to a string.
     * @param options Either the encoding for the file, or an object optionally specifying the encoding, file mode, and flag.
     * If `encoding` is not supplied, the default of `'utf8'` is used.
     * If `mode` is not supplied, the default of `0o666` is used.
     * If `mode` is a string, it is parsed as an octal integer.
     * If `flag` is not supplied, the default of `'a'` is used.
     */
    export function appendFileSync(file: PathLike | number, data: any, options?: { encoding?: string | null; mode?: number | string; flag?: string; } | string | null): void;

    /**
     * Watch for changes on `filename`. The callback `listener` will be called each time the file is accessed.
     */
    export function watchFile(filename: PathLike, options: { persistent?: boolean; interval?: number; } | undefined, listener: (curr: Stats, prev: Stats) => void): void;

    /**
     * Watch for changes on `filename`. The callback `listener` will be called each time the file is accessed.
     * @param filename A path to a file or directory. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     */
    export function watchFile(filename: PathLike, listener: (curr: Stats, prev: Stats) => void): void;

    /**
     * Stop watching for changes on `filename`.
     * @param filename A path to a file or directory. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     */
    export function unwatchFile(filename: PathLike, listener?: (curr: Stats, prev: Stats) => void): void;

    /**
     * Watch for changes on `filename`, where `filename` is either a file or a directory, returning an `FSWatcher`.
     * @param filename A path to a file or directory. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     * @param options Either the encoding for the filename provided to the listener, or an object optionally specifying encoding, persistent, and recursive options.
     * If `encoding` is not supplied, the default of `'utf8'` is used.
     * If `persistent` is not supplied, the default of `true` is used.
     * If `recursive` is not supplied, the default of `false` is used.
     */
    export function watch(filename: PathLike, options: { encoding?: BufferEncoding | null, persistent?: boolean, recursive?: boolean } | BufferEncoding | undefined | null, listener?: (event: string, filename: string) => void): FSWatcher;

    /**
     * Watch for changes on `filename`, where `filename` is either a file or a directory, returning an `FSWatcher`.
     * @param filename A path to a file or directory. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     * @param options Either the encoding for the filename provided to the listener, or an object optionally specifying encoding, persistent, and recursive options.
     * If `encoding` is not supplied, the default of `'utf8'` is used.
     * If `persistent` is not supplied, the default of `true` is used.
     * If `recursive` is not supplied, the default of `false` is used.
     */
    export function watch(filename: PathLike, options: { encoding: "buffer", persistent?: boolean, recursive?: boolean } | "buffer", listener?: (event: string, filename: Buffer) => void): FSWatcher;

    /**
     * Watch for changes on `filename`, where `filename` is either a file or a directory, returning an `FSWatcher`.
     * @param filename A path to a file or directory. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     * @param options Either the encoding for the filename provided to the listener, or an object optionally specifying encoding, persistent, and recursive options.
     * If `encoding` is not supplied, the default of `'utf8'` is used.
     * If `persistent` is not supplied, the default of `true` is used.
     * If `recursive` is not supplied, the default of `false` is used.
     */
    export function watch(filename: PathLike, options: { encoding?: string | null, persistent?: boolean, recursive?: boolean } | string | null, listener?: (event: string, filename: string | Buffer) => void): FSWatcher;

    /**
     * Watch for changes on `filename`, where `filename` is either a file or a directory, returning an `FSWatcher`.
     * @param filename A path to a file or directory. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     */
    export function watch(filename: PathLike, listener?: (event: string, filename: string) => any): FSWatcher;

    /**
     * Asynchronously tests whether or not the given path exists by checking with the file system.
     * @deprecated
     * @param path A path to a file or directory. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     */
    export function exists(path: PathLike, callback: (exists: boolean) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace exists {
        /**
         * @param path A path to a file or directory. If a URL is provided, it must use the `file:` protocol.
         * URL support is _experimental_.
         */
        function __promisify__(path: PathLike): Promise<boolean>;
    }

    /**
     * Synchronously tests whether or not the given path exists by checking with the file system.
     * @param path A path to a file or directory. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     */
    export function existsSync(path: PathLike): boolean;

    export namespace constants {
        // File Access Constants

        /** Constant for fs.access(). File is visible to the calling process. */
        export const F_OK: number;

        /** Constant for fs.access(). File can be read by the calling process. */
        export const R_OK: number;

        /** Constant for fs.access(). File can be written by the calling process. */
        export const W_OK: number;

        /** Constant for fs.access(). File can be executed by the calling process. */
        export const X_OK: number;

        // File Open Constants

        /** Constant for fs.open(). Flag indicating to open a file for read-only access. */
        export const O_RDONLY: number;

        /** Constant for fs.open(). Flag indicating to open a file for write-only access. */
        export const O_WRONLY: number;

        /** Constant for fs.open(). Flag indicating to open a file for read-write access. */
        export const O_RDWR: number;

        /** Constant for fs.open(). Flag indicating to create the file if it does not already exist. */
        export const O_CREAT: number;

        /** Constant for fs.open(). Flag indicating that opening a file should fail if the O_CREAT flag is set and the file already exists. */
        export const O_EXCL: number;

        /** Constant for fs.open(). Flag indicating that if path identifies a terminal device, opening the path shall not cause that terminal to become the controlling terminal for the process (if the process does not already have one). */
        export const O_NOCTTY: number;

        /** Constant for fs.open(). Flag indicating that if the file exists and is a regular file, and the file is opened successfully for write access, its length shall be truncated to zero. */
        export const O_TRUNC: number;

        /** Constant for fs.open(). Flag indicating that data will be appended to the end of the file. */
        export const O_APPEND: number;

        /** Constant for fs.open(). Flag indicating that the open should fail if the path is not a directory. */
        export const O_DIRECTORY: number;

        /** Constant for fs.open(). Flag indicating reading accesses to the file system will no longer result in an update to the atime information associated with the file. This flag is available on Linux operating systems only. */
        export const O_NOATIME: number;

        /** Constant for fs.open(). Flag indicating that the open should fail if the path is a symbolic link. */
        export const O_NOFOLLOW: number;

        /** Constant for fs.open(). Flag indicating that the file is opened for synchronous I/O. */
        export const O_SYNC: number;

        /** Constant for fs.open(). Flag indicating that the file is opened for synchronous I/O with write operations waiting for data integrity. */
        export const O_DSYNC: number;

        /** Constant for fs.open(). Flag indicating to open the symbolic link itself rather than the resource it is pointing to. */
        export const O_SYMLINK: number;

        /** Constant for fs.open(). When set, an attempt will be made to minimize caching effects of file I/O. */
        export const O_DIRECT: number;

        /** Constant for fs.open(). Flag indicating to open the file in nonblocking mode when possible. */
        export const O_NONBLOCK: number;

        // File Type Constants

        /** Constant for fs.Stats mode property for determining a file's type. Bit mask used to extract the file type code. */
        export const S_IFMT: number;

        /** Constant for fs.Stats mode property for determining a file's type. File type constant for a regular file. */
        export const S_IFREG: number;

        /** Constant for fs.Stats mode property for determining a file's type. File type constant for a directory. */
        export const S_IFDIR: number;

        /** Constant for fs.Stats mode property for determining a file's type. File type constant for a character-oriented device file. */
        export const S_IFCHR: number;

        /** Constant for fs.Stats mode property for determining a file's type. File type constant for a block-oriented device file. */
        export const S_IFBLK: number;

        /** Constant for fs.Stats mode property for determining a file's type. File type constant for a FIFO/pipe. */
        export const S_IFIFO: number;

        /** Constant for fs.Stats mode property for determining a file's type. File type constant for a symbolic link. */
        export const S_IFLNK: number;

        /** Constant for fs.Stats mode property for determining a file's type. File type constant for a socket. */
        export const S_IFSOCK: number;

        // File Mode Constants

        /** Constant for fs.Stats mode property for determining access permissions for a file. File mode indicating readable, writable and executable by owner. */
        export const S_IRWXU: number;

        /** Constant for fs.Stats mode property for determining access permissions for a file. File mode indicating readable by owner. */
        export const S_IRUSR: number;

        /** Constant for fs.Stats mode property for determining access permissions for a file. File mode indicating writable by owner. */
        export const S_IWUSR: number;

        /** Constant for fs.Stats mode property for determining access permissions for a file. File mode indicating executable by owner. */
        export const S_IXUSR: number;

        /** Constant for fs.Stats mode property for determining access permissions for a file. File mode indicating readable, writable and executable by group. */
        export const S_IRWXG: number;

        /** Constant for fs.Stats mode property for determining access permissions for a file. File mode indicating readable by group. */
        export const S_IRGRP: number;

        /** Constant for fs.Stats mode property for determining access permissions for a file. File mode indicating writable by group. */
        export const S_IWGRP: number;

        /** Constant for fs.Stats mode property for determining access permissions for a file. File mode indicating executable by group. */
        export const S_IXGRP: number;

        /** Constant for fs.Stats mode property for determining access permissions for a file. File mode indicating readable, writable and executable by others. */
        export const S_IRWXO: number;

        /** Constant for fs.Stats mode property for determining access permissions for a file. File mode indicating readable by others. */
        export const S_IROTH: number;

        /** Constant for fs.Stats mode property for determining access permissions for a file. File mode indicating writable by others. */
        export const S_IWOTH: number;

        /** Constant for fs.Stats mode property for determining access permissions for a file. File mode indicating executable by others. */
        export const S_IXOTH: number;

        /** Constant for fs.copyFile. Flag indicating the destination file should not be overwritten if it already exists. */
        export const COPYFILE_EXCL: number;
    }

    /**
     * Asynchronously tests a user's permissions for the file specified by path.
     * @param path A path to a file or directory. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     */
    export function access(path: PathLike, mode: number | undefined, callback: (err: NodeJS.ErrnoException) => void): void;

    /**
     * Asynchronously tests a user's permissions for the file specified by path.
     * @param path A path to a file or directory. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     */
    export function access(path: PathLike, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace access {
        /**
         * Asynchronously tests a user's permissions for the file specified by path.
         * @param path A path to a file or directory. If a URL is provided, it must use the `file:` protocol.
         * URL support is _experimental_.
         */
        export function __promisify__(path: PathLike, mode?: number): Promise<void>;
    }

    /**
     * Synchronously tests a user's permissions for the file specified by path.
     * @param path A path to a file or directory. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     */
    export function accessSync(path: PathLike, mode?: number): void;

    /**
     * Returns a new `ReadStream` object.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     */
    export function createReadStream(path: PathLike, options?: string | {
        flags?: string;
        encoding?: string;
        fd?: number;
        mode?: number;
        autoClose?: boolean;
        start?: number;
        end?: number;
        highWaterMark?: number;
    }): ReadStream;

    /**
     * Returns a new `WriteStream` object.
     * @param path A path to a file. If a URL is provided, it must use the `file:` protocol.
     * URL support is _experimental_.
     */
    export function createWriteStream(path: PathLike, options?: string | {
        flags?: string;
        encoding?: string;
        fd?: number;
        mode?: number;
        autoClose?: boolean;
        start?: number;
    }): WriteStream;

    /**
     * Asynchronous fdatasync(2) - synchronize a file's in-core state with storage device.
     * @param fd A file descriptor.
     */
    export function fdatasync(fd: number, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace fdatasync {
        /**
         * Asynchronous fdatasync(2) - synchronize a file's in-core state with storage device.
         * @param fd A file descriptor.
         */
        export function __promisify__(fd: number): Promise<void>;
    }

    /**
     * Synchronous fdatasync(2) - synchronize a file's in-core state with storage device.
     * @param fd A file descriptor.
     */
    export function fdatasyncSync(fd: number): void;

    /**
     * Asynchronously copies src to dest. By default, dest is overwritten if it already exists.
     * No arguments other than a possible exception are given to the callback function.
     * Node.js makes no guarantees about the atomicity of the copy operation.
     * If an error occurs after the destination file has been opened for writing, Node.js will attempt
     * to remove the destination.
     * @param src A path to the source file.
     * @param dest A path to the destination file.
     */
    export function copyFile(src: PathLike, dest: PathLike, callback: (err: NodeJS.ErrnoException) => void): void;
    /**
     * Asynchronously copies src to dest. By default, dest is overwritten if it already exists.
     * No arguments other than a possible exception are given to the callback function.
     * Node.js makes no guarantees about the atomicity of the copy operation.
     * If an error occurs after the destination file has been opened for writing, Node.js will attempt
     * to remove the destination.
     * @param src A path to the source file.
     * @param dest A path to the destination file.
     * @param flags An integer that specifies the behavior of the copy operation. The only supported flag is fs.constants.COPYFILE_EXCL, which causes the copy operation to fail if dest already exists.
     */
    export function copyFile(src: PathLike, dest: PathLike, flags: number, callback: (err: NodeJS.ErrnoException) => void): void;

    // NOTE: This namespace provides design-time support for util.promisify. Exported members do not exist at runtime.
    export namespace copyFile {
        /**
         * Asynchronously copies src to dest. By default, dest is overwritten if it already exists.
         * No arguments other than a possible exception are given to the callback function.
         * Node.js makes no guarantees about the atomicity of the copy operation.
         * If an error occurs after the destination file has been opened for writing, Node.js will attempt
         * to remove the destination.
         * @param src A path to the source file.
         * @param dest A path to the destination file.
         * @param flags An optional integer that specifies the behavior of the copy operation. The only supported flag is fs.constants.COPYFILE_EXCL, which causes the copy operation to fail if dest already exists.
         */
        export function __promisify__(src: PathLike, dst: PathLike, flags?: number): Promise<void>;
    }

    /**
     * Synchronously copies src to dest. By default, dest is overwritten if it already exists.
     * Node.js makes no guarantees about the atomicity of the copy operation.
     * If an error occurs after the destination file has been opened for writing, Node.js will attempt
     * to remove the destination.
     * @param src A path to the source file.
     * @param dest A path to the destination file.
     * @param flags An optional integer that specifies the behavior of the copy operation. The only supported flag is fs.constants.COPYFILE_EXCL, which causes the copy operation to fail if dest already exists.
     */
    export function copyFileSync(src: PathLike, dest: PathLike, flags?: number): void;
}

declare module "path" {
    /**
     * A parsed path object generated by path.parse() or consumed by path.format().
     */
    export interface ParsedPath {
        /**
         * The root of the path such as '/' or 'c:\'
         */
        root: string;
        /**
         * The full directory path such as '/home/user/dir' or 'c:\path\dir'
         */
        dir: string;
        /**
         * The file name including extension (if any) such as 'index.html'
         */
        base: string;
        /**
         * The file extension (if any) such as '.html'
         */
        ext: string;
        /**
         * The file name without extension (if any) such as 'index'
         */
        name: string;
    }
    export interface FormatInputPathObject {
        /**
         * The root of the path such as '/' or 'c:\'
         */
        root?: string;
        /**
         * The full directory path such as '/home/user/dir' or 'c:\path\dir'
         */
        dir?: string;
        /**
         * The file name including extension (if any) such as 'index.html'
         */
        base?: string;
        /**
         * The file extension (if any) such as '.html'
         */
        ext?: string;
        /**
         * The file name without extension (if any) such as 'index'
         */
        name?: string;
    }

    /**
     * Normalize a string path, reducing '..' and '.' parts.
     * When multiple slashes are found, they're replaced by a single one; when the path contains a trailing slash, it is preserved. On Windows backslashes are used.
     *
     * @param p string path to normalize.
     */
    export function normalize(p: string): string;
    /**
     * Join all arguments together and normalize the resulting path.
     * Arguments must be strings. In v0.8, non-string arguments were silently ignored. In v0.10 and up, an exception is thrown.
     *
     * @param paths paths to join.
     */
    export function join(...paths: string[]): string;
    /**
     * The right-most parameter is considered {to}.  Other parameters are considered an array of {from}.
     *
     * Starting from leftmost {from} paramter, resolves {to} to an absolute path.
     *
     * If {to} isn't already absolute, {from} arguments are prepended in right to left order, until an absolute path is found. If after using all {from} paths still no absolute path is found, the current working directory is used as well. The resulting path is normalized, and trailing slashes are removed unless the path gets resolved to the root directory.
     *
     * @param pathSegments string paths to join.  Non-string arguments are ignored.
     */
    export function resolve(...pathSegments: string[]): string;
    /**
     * Determines whether {path} is an absolute path. An absolute path will always resolve to the same location, regardless of the working directory.
     *
     * @param path path to test.
     */
    export function isAbsolute(path: string): boolean;
    /**
     * Solve the relative path from {from} to {to}.
     * At times we have two absolute paths, and we need to derive the relative path from one to the other. This is actually the reverse transform of path.resolve.
     */
    export function relative(from: string, to: string): string;
    /**
     * Return the directory name of a path. Similar to the Unix dirname command.
     *
     * @param p the path to evaluate.
     */
    export function dirname(p: string): string;
    /**
     * Return the last portion of a path. Similar to the Unix basename command.
     * Often used to extract the file name from a fully qualified path.
     *
     * @param p the path to evaluate.
     * @param ext optionally, an extension to remove from the result.
     */
    export function basename(p: string, ext?: string): string;
    /**
     * Return the extension of the path, from the last '.' to end of string in the last portion of the path.
     * If there is no '.' in the last portion of the path or the first character of it is '.', then it returns an empty string
     *
     * @param p the path to evaluate.
     */
    export function extname(p: string): string;
    /**
     * The platform-specific file separator. '\\' or '/'.
     */
    export var sep: '\\' | '/';
    /**
     * The platform-specific file delimiter. ';' or ':'.
     */
    export var delimiter: ';' | ':';
    /**
     * Returns an object from a path string - the opposite of format().
     *
     * @param pathString path to evaluate.
     */
    export function parse(pathString: string): ParsedPath;
    /**
     * Returns a path string from an object - the opposite of parse().
     *
     * @param pathString path to evaluate.
     */
    export function format(pathObject: FormatInputPathObject): string;

    export module posix {
        export function normalize(p: string): string;
        export function join(...paths: any[]): string;
        export function resolve(...pathSegments: any[]): string;
        export function isAbsolute(p: string): boolean;
        export function relative(from: string, to: string): string;
        export function dirname(p: string): string;
        export function basename(p: string, ext?: string): string;
        export function extname(p: string): string;
        export var sep: string;
        export var delimiter: string;
        export function parse(p: string): ParsedPath;
        export function format(pP: FormatInputPathObject): string;
    }

    export module win32 {
        export function normalize(p: string): string;
        export function join(...paths: any[]): string;
        export function resolve(...pathSegments: any[]): string;
        export function isAbsolute(p: string): boolean;
        export function relative(from: string, to: string): string;
        export function dirname(p: string): string;
        export function basename(p: string, ext?: string): string;
        export function extname(p: string): string;
        export var sep: string;
        export var delimiter: string;
        export function parse(p: string): ParsedPath;
        export function format(pP: FormatInputPathObject): string;
    }
}

declare module "string_decoder" {
    export interface NodeStringDecoder {
        write(buffer: Buffer): string;
        end(buffer?: Buffer): string;
    }
    export var StringDecoder: {
        new(encoding?: string): NodeStringDecoder;
    };
}

declare module "tls" {
    import * as crypto from "crypto";
    import * as dns from "dns";
    import * as net from "net";
    import * as stream from "stream";

    var CLIENT_RENEG_LIMIT: number;
    var CLIENT_RENEG_WINDOW: number;

    export interface Certificate {
        /**
         * Country code.
         */
        C: string;
        /**
         * Street.
         */
        ST: string;
        /**
         * Locality.
         */
        L: string;
        /**
         * Organization.
         */
        O: string;
        /**
         * Organizational unit.
         */
        OU: string;
        /**
         * Common name.
         */
        CN: string;
    }

    export interface PeerCertificate {
        subject: Certificate;
        issuer: Certificate;
        subjectaltname: string;
        infoAccess: { [index: string]: string[] | undefined };
        modulus: string;
        exponent: string;
        valid_from: string;
        valid_to: string;
        fingerprint: string;
        ext_key_usage: string[];
        serialNumber: string;
        raw: Buffer;
    }

    export interface DetailedPeerCertificate extends PeerCertificate {
        issuerCertificate: DetailedPeerCertificate;
    }

    export interface CipherNameAndProtocol {
        /**
         * The cipher name.
         */
        name: string;
        /**
         * SSL/TLS protocol version.
         */
        version: string;
    }

    export class TLSSocket extends net.Socket {
        /**
         * Construct a new tls.TLSSocket object from an existing TCP socket.
         */
        constructor(socket: net.Socket, options?: {
            /**
             * An optional TLS context object from tls.createSecureContext()
             */
            secureContext?: SecureContext,
            /**
             * If true the TLS socket will be instantiated in server-mode.
             * Defaults to false.
             */
            isServer?: boolean,
            /**
             * An optional net.Server instance.
             */
            server?: net.Server,
            /**
             * If true the server will request a certificate from clients that
             * connect and attempt to verify that certificate. Defaults to
             * false.
             */
            requestCert?: boolean,
            /**
             * If true the server will reject any connection which is not
             * authorized with the list of supplied CAs. This option only has an
             * effect if requestCert is true. Defaults to false.
             */
            rejectUnauthorized?: boolean,
            /**
             * An array of strings or a Buffer naming possible NPN protocols.
             * (Protocols should be ordered by their priority.)
             */
            NPNProtocols?: string[] | Buffer[] | Uint8Array[] | Buffer | Uint8Array,
            /**
             * An array of strings or a Buffer naming possible ALPN protocols.
             * (Protocols should be ordered by their priority.) When the server
             * receives both NPN and ALPN extensions from the client, ALPN takes
             * precedence over NPN and the server does not send an NPN extension
             * to the client.
             */
            ALPNProtocols?: string[] | Buffer[] | Uint8Array[] | Buffer | Uint8Array,
            /**
             * SNICallback(servername, cb) <Function> A function that will be
             * called if the client supports SNI TLS extension. Two arguments
             * will be passed when called: servername and cb. SNICallback should
             * invoke cb(null, ctx), where ctx is a SecureContext instance.
             * (tls.createSecureContext(...) can be used to get a proper
             * SecureContext.) If SNICallback wasn't provided the default callback
             * with high-level API will be used (see below).
             */
            SNICallback?: (servername: string, cb: (err: Error | null, ctx: SecureContext) => void) => void,
            /**
             * An optional Buffer instance containing a TLS session.
             */
            session?: Buffer,
            /**
             * If true, specifies that the OCSP status request extension will be
             * added to the client hello and an 'OCSPResponse' event will be
             * emitted on the socket before establishing a secure communication
             */
            requestOCSP?: boolean
        });

        /**
         * A boolean that is true if the peer certificate was signed by one of the specified CAs, otherwise false.
         */
        authorized: boolean;
        /**
         * The reason why the peer's certificate has not been verified.
         * This property becomes available only when tlsSocket.authorized === false.
         */
        authorizationError: Error;
        /**
         * Static boolean value, always true.
         * May be used to distinguish TLS sockets from regular ones.
         */
        encrypted: boolean;
        /**
         * Returns an object representing the cipher name and the SSL/TLS protocol version of the current connection.
         * @returns Returns an object representing the cipher name
         * and the SSL/TLS protocol version of the current connection.
         */
        getCipher(): CipherNameAndProtocol;
        /**
         * Returns an object representing the peer's certificate.
         * The returned object has some properties corresponding to the field of the certificate.
         * If detailed argument is true the full chain with issuer property will be returned,
         * if false only the top certificate without issuer property.
         * If the peer does not provide a certificate, it returns null or an empty object.
         * @param detailed - If true; the full chain with issuer property will be returned.
         * @returns An object representing the peer's certificate.
         */
        getPeerCertificate(detailed: true): DetailedPeerCertificate;
        getPeerCertificate(detailed?: false): PeerCertificate;
        getPeerCertificate(detailed?: boolean): PeerCertificate | DetailedPeerCertificate;
        /**
         * Returns a string containing the negotiated SSL/TLS protocol version of the current connection.
         * The value `'unknown'` will be returned for connected sockets that have not completed the handshaking process.
         * The value `null` will be returned for server sockets or disconnected client sockets.
         * See https://www.openssl.org/docs/man1.0.2/ssl/SSL_get_version.html for more information.
         * @returns negotiated SSL/TLS protocol version of the current connection
         */
        getProtocol(): string | null;
        /**
         * Could be used to speed up handshake establishment when reconnecting to the server.
         * @returns ASN.1 encoded TLS session or undefined if none was negotiated.
         */
        getSession(): any;
        /**
         * NOTE: Works only with client TLS sockets.
         * Useful only for debugging, for session reuse provide session option to tls.connect().
         * @returns TLS session ticket or undefined if none was negotiated.
         */
        getTLSTicket(): any;
        /**
         * Initiate TLS renegotiation process.
         *
         * NOTE: Can be used to request peer's certificate after the secure connection has been established.
         * ANOTHER NOTE: When running as the server, socket will be destroyed with an error after handshakeTimeout timeout.
         * @param options - The options may contain the following fields: rejectUnauthorized,
         * requestCert (See tls.createServer() for details).
         * @param callback - callback(err) will be executed with null as err, once the renegotiation
         * is successfully completed.
         */
        renegotiate(options: { rejectUnauthorized?: boolean, requestCert?: boolean }, callback: (err: Error | null) => void): any;
        /**
         * Set maximum TLS fragment size (default and maximum value is: 16384, minimum is: 512).
         * Smaller fragment size decreases buffering latency on the client: large fragments are buffered by
         * the TLS layer until the entire fragment is received and its integrity is verified;
         * large fragments can span multiple roundtrips, and their processing can be delayed due to packet
         * loss or reordering. However, smaller fragments add extra TLS framing bytes and CPU overhead,
         * which may decrease overall server throughput.
         * @param size - TLS fragment size (default and maximum value is: 16384, minimum is: 512).
         * @returns Returns true on success, false otherwise.
         */
        setMaxSendFragment(size: number): boolean;

        /**
         * events.EventEmitter
         * 1. OCSPResponse
         * 2. secureConnect
         */
        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "OCSPResponse", listener: (response: Buffer) => void): this;
        addListener(event: "secureConnect", listener: () => void): this;

        emit(event: string | symbol, ...args: any[]): boolean;
        emit(event: "OCSPResponse", response: Buffer): boolean;
        emit(event: "secureConnect"): boolean;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "OCSPResponse", listener: (response: Buffer) => void): this;
        on(event: "secureConnect", listener: () => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "OCSPResponse", listener: (response: Buffer) => void): this;
        once(event: "secureConnect", listener: () => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "OCSPResponse", listener: (response: Buffer) => void): this;
        prependListener(event: "secureConnect", listener: () => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "OCSPResponse", listener: (response: Buffer) => void): this;
        prependOnceListener(event: "secureConnect", listener: () => void): this;
    }

    export interface TlsOptions extends SecureContextOptions {
        handshakeTimeout?: number;
        requestCert?: boolean;
        rejectUnauthorized?: boolean;
        NPNProtocols?: string[] | Buffer[] | Uint8Array[] | Buffer | Uint8Array;
        ALPNProtocols?: string[] | Buffer[] | Uint8Array[] | Buffer | Uint8Array;
        SNICallback?: (servername: string, cb: (err: Error | null, ctx: SecureContext) => void) => void;
        sessionTimeout?: number;
        ticketKeys?: Buffer;
    }

    export interface ConnectionOptions extends SecureContextOptions {
        host?: string;
        port?: number;
        path?: string; // Creates unix socket connection to path. If this option is specified, `host` and `port` are ignored.
        socket?: net.Socket; // Establish secure connection on a given socket rather than creating a new socket
        rejectUnauthorized?: boolean; // Defaults to true
        NPNProtocols?: string[] | Buffer[] | Uint8Array[] | Buffer | Uint8Array;
        ALPNProtocols?: string[] | Buffer[] | Uint8Array[] | Buffer | Uint8Array;
        checkServerIdentity?: typeof checkServerIdentity;
        servername?: string; // SNI TLS Extension
        session?: Buffer;
        minDHSize?: number;
        secureContext?: SecureContext; // If not provided, the entire ConnectionOptions object will be passed to tls.createSecureContext()
        lookup?: net.LookupFunction;
    }

    export class Server extends net.Server {
        addContext(hostName: string, credentials: {
            key: string;
            cert: string;
            ca: string;
        }): void;

        /**
         * events.EventEmitter
         * 1. tlsClientError
         * 2. newSession
         * 3. OCSPRequest
         * 4. resumeSession
         * 5. secureConnection
         */
        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "tlsClientError", listener: (err: Error, tlsSocket: TLSSocket) => void): this;
        addListener(event: "newSession", listener: (sessionId: any, sessionData: any, callback: (err: Error, resp: Buffer) => void) => void): this;
        addListener(event: "OCSPRequest", listener: (certificate: Buffer, issuer: Buffer, callback: Function) => void): this;
        addListener(event: "resumeSession", listener: (sessionId: any, callback: (err: Error, sessionData: any) => void) => void): this;
        addListener(event: "secureConnection", listener: (tlsSocket: TLSSocket) => void): this;

        emit(event: string | symbol, ...args: any[]): boolean;
        emit(event: "tlsClientError", err: Error, tlsSocket: TLSSocket): boolean;
        emit(event: "newSession", sessionId: any, sessionData: any, callback: (err: Error, resp: Buffer) => void): boolean;
        emit(event: "OCSPRequest", certificate: Buffer, issuer: Buffer, callback: Function): boolean;
        emit(event: "resumeSession", sessionId: any, callback: (err: Error, sessionData: any) => void): boolean;
        emit(event: "secureConnection", tlsSocket: TLSSocket): boolean;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "tlsClientError", listener: (err: Error, tlsSocket: TLSSocket) => void): this;
        on(event: "newSession", listener: (sessionId: any, sessionData: any, callback: (err: Error, resp: Buffer) => void) => void): this;
        on(event: "OCSPRequest", listener: (certificate: Buffer, issuer: Buffer, callback: Function) => void): this;
        on(event: "resumeSession", listener: (sessionId: any, callback: (err: Error, sessionData: any) => void) => void): this;
        on(event: "secureConnection", listener: (tlsSocket: TLSSocket) => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "tlsClientError", listener: (err: Error, tlsSocket: TLSSocket) => void): this;
        once(event: "newSession", listener: (sessionId: any, sessionData: any, callback: (err: Error, resp: Buffer) => void) => void): this;
        once(event: "OCSPRequest", listener: (certificate: Buffer, issuer: Buffer, callback: Function) => void): this;
        once(event: "resumeSession", listener: (sessionId: any, callback: (err: Error, sessionData: any) => void) => void): this;
        once(event: "secureConnection", listener: (tlsSocket: TLSSocket) => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "tlsClientError", listener: (err: Error, tlsSocket: TLSSocket) => void): this;
        prependListener(event: "newSession", listener: (sessionId: any, sessionData: any, callback: (err: Error, resp: Buffer) => void) => void): this;
        prependListener(event: "OCSPRequest", listener: (certificate: Buffer, issuer: Buffer, callback: Function) => void): this;
        prependListener(event: "resumeSession", listener: (sessionId: any, callback: (err: Error, sessionData: any) => void) => void): this;
        prependListener(event: "secureConnection", listener: (tlsSocket: TLSSocket) => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "tlsClientError", listener: (err: Error, tlsSocket: TLSSocket) => void): this;
        prependOnceListener(event: "newSession", listener: (sessionId: any, sessionData: any, callback: (err: Error, resp: Buffer) => void) => void): this;
        prependOnceListener(event: "OCSPRequest", listener: (certificate: Buffer, issuer: Buffer, callback: Function) => void): this;
        prependOnceListener(event: "resumeSession", listener: (sessionId: any, callback: (err: Error, sessionData: any) => void) => void): this;
        prependOnceListener(event: "secureConnection", listener: (tlsSocket: TLSSocket) => void): this;
    }

    export interface ClearTextStream extends stream.Duplex {
        authorized: boolean;
        authorizationError: Error;
        getPeerCertificate(): any;
        getCipher: {
            name: string;
            version: string;
        };
        address: {
            port: number;
            family: string;
            address: string;
        };
        remoteAddress: string;
        remotePort: number;
    }

    export interface SecurePair {
        encrypted: any;
        cleartext: any;
    }

    export interface SecureContextOptions {
        pfx?: string | Buffer | Array<string | Buffer | Object>;
        key?: string | Buffer | Array<Buffer | Object>;
        passphrase?: string;
        cert?: string | Buffer | Array<string | Buffer>;
        ca?: string | Buffer | Array<string | Buffer>;
        ciphers?: string;
        honorCipherOrder?: boolean;
        ecdhCurve?: string;
        crl?: string | Buffer | Array<string | Buffer>;
        dhparam?: string | Buffer;
        secureOptions?: number; // Value is a numeric bitmask of the `SSL_OP_*` options
        secureProtocol?: string; // SSL Method, e.g. SSLv23_method
        sessionIdContext?: string;
    }

    export interface SecureContext {
        context: any;
    }

    /*
     * Verifies the certificate `cert` is issued to host `host`.
     * @host The hostname to verify the certificate against
     * @cert PeerCertificate representing the peer's certificate
     *
     * Returns Error object, populating it with the reason, host and cert on failure.  On success, returns undefined.
     */
    export function checkServerIdentity(host: string, cert: PeerCertificate): Error | undefined;
    export function createServer(options: TlsOptions, secureConnectionListener?: (socket: TLSSocket) => void): Server;
    export function connect(options: ConnectionOptions, secureConnectionListener?: () => void): TLSSocket;
    export function connect(port: number, host?: string, options?: ConnectionOptions, secureConnectListener?: () => void): TLSSocket;
    export function connect(port: number, options?: ConnectionOptions, secureConnectListener?: () => void): TLSSocket;
    export function createSecurePair(credentials?: crypto.Credentials, isServer?: boolean, requestCert?: boolean, rejectUnauthorized?: boolean): SecurePair;
    export function createSecureContext(details: SecureContextOptions): SecureContext;
    export function getCiphers(): string[];

    export var DEFAULT_ECDH_CURVE: string;
}

declare module "crypto" {
    export interface Certificate {
        exportChallenge(spkac: string | Buffer): Buffer;
        exportPublicKey(spkac: string | Buffer): Buffer;
        verifySpkac(spkac: Buffer): boolean;
    }
    export var Certificate: {
        new(): Certificate;
        (): Certificate;
    };

    export var fips: boolean;

    export interface CredentialDetails {
        pfx: string;
        key: string;
        passphrase: string;
        cert: string;
        ca: string | string[];
        crl: string | string[];
        ciphers: string;
    }
    export interface Credentials { context?: any; }
    export function createCredentials(details: CredentialDetails): Credentials;
    export function createHash(algorithm: string): Hash;
    export function createHmac(algorithm: string, key: string | Buffer): Hmac;

    type Utf8AsciiLatin1Encoding = "utf8" | "ascii" | "latin1";
    type HexBase64Latin1Encoding = "latin1" | "hex" | "base64";
    type Utf8AsciiBinaryEncoding = "utf8" | "ascii" | "binary";
    type HexBase64BinaryEncoding = "binary" | "base64" | "hex";
    type ECDHKeyFormat = "compressed" | "uncompressed" | "hybrid";

    export interface Hash extends NodeJS.ReadWriteStream {
        update(data: string | Buffer | DataView): Hash;
        update(data: string | Buffer | DataView, input_encoding: Utf8AsciiLatin1Encoding): Hash;
        digest(): Buffer;
        digest(encoding: HexBase64Latin1Encoding): string;
    }
    export interface Hmac extends NodeJS.ReadWriteStream {
        update(data: string | Buffer | DataView): Hmac;
        update(data: string | Buffer | DataView, input_encoding: Utf8AsciiLatin1Encoding): Hmac;
        digest(): Buffer;
        digest(encoding: HexBase64Latin1Encoding): string;
    }
    export function createCipher(algorithm: string, password: any): Cipher;
    export function createCipheriv(algorithm: string, key: any, iv: any): Cipher;
    export interface Cipher extends NodeJS.ReadWriteStream {
        update(data: Buffer | DataView): Buffer;
        update(data: string, input_encoding: Utf8AsciiBinaryEncoding): Buffer;
        update(data: Buffer | DataView, input_encoding: any, output_encoding: HexBase64BinaryEncoding): string;
        update(data: string, input_encoding: Utf8AsciiBinaryEncoding, output_encoding: HexBase64BinaryEncoding): string;
        final(): Buffer;
        final(output_encoding: string): string;
        setAutoPadding(auto_padding?: boolean): this;
        getAuthTag(): Buffer;
        setAAD(buffer: Buffer): this;
    }
    export function createDecipher(algorithm: string, password: any): Decipher;
    export function createDecipheriv(algorithm: string, key: any, iv: any): Decipher;
    export interface Decipher extends NodeJS.ReadWriteStream {
        update(data: Buffer | DataView): Buffer;
        update(data: string, input_encoding: HexBase64BinaryEncoding): Buffer;
        update(data: Buffer | DataView, input_encoding: any, output_encoding: Utf8AsciiBinaryEncoding): string;
        update(data: string, input_encoding: HexBase64BinaryEncoding, output_encoding: Utf8AsciiBinaryEncoding): string;
        final(): Buffer;
        final(output_encoding: string): string;
        setAutoPadding(auto_padding?: boolean): this;
        setAuthTag(tag: Buffer): this;
        setAAD(buffer: Buffer): this;
    }
    export function createSign(algorithm: string): Signer;
    export interface Signer extends NodeJS.WritableStream {
        update(data: string | Buffer | DataView): Signer;
        update(data: string | Buffer | DataView, input_encoding: Utf8AsciiLatin1Encoding): Signer;
        sign(private_key: string | { key: string; passphrase: string }): Buffer;
        sign(private_key: string | { key: string; passphrase: string }, output_format: HexBase64Latin1Encoding): string;
    }
    export function createVerify(algorith: string): Verify;
    export interface Verify extends NodeJS.WritableStream {
        update(data: string | Buffer | DataView): Verify;
        update(data: string | Buffer | DataView, input_encoding: Utf8AsciiLatin1Encoding): Verify;
        verify(object: string | Object, signature: Buffer | DataView): boolean;
        verify(object: string | Object, signature: string, signature_format: HexBase64Latin1Encoding): boolean;
        // https://nodejs.org/api/crypto.html#crypto_verifier_verify_object_signature_signature_format
        // The signature field accepts a TypedArray type, but it is only available starting ES2017
    }
    export function createDiffieHellman(prime_length: number, generator?: number): DiffieHellman;
    export function createDiffieHellman(prime: Buffer): DiffieHellman;
    export function createDiffieHellman(prime: string, prime_encoding: HexBase64Latin1Encoding): DiffieHellman;
    export function createDiffieHellman(prime: string, prime_encoding: HexBase64Latin1Encoding, generator: number | Buffer): DiffieHellman;
    export function createDiffieHellman(prime: string, prime_encoding: HexBase64Latin1Encoding, generator: string, generator_encoding: HexBase64Latin1Encoding): DiffieHellman;
    export interface DiffieHellman {
        generateKeys(): Buffer;
        generateKeys(encoding: HexBase64Latin1Encoding): string;
        computeSecret(other_public_key: Buffer): Buffer;
        computeSecret(other_public_key: string, input_encoding: HexBase64Latin1Encoding): Buffer;
        computeSecret(other_public_key: string, input_encoding: HexBase64Latin1Encoding, output_encoding: HexBase64Latin1Encoding): string;
        getPrime(): Buffer;
        getPrime(encoding: HexBase64Latin1Encoding): string;
        getGenerator(): Buffer;
        getGenerator(encoding: HexBase64Latin1Encoding): string;
        getPublicKey(): Buffer;
        getPublicKey(encoding: HexBase64Latin1Encoding): string;
        getPrivateKey(): Buffer;
        getPrivateKey(encoding: HexBase64Latin1Encoding): string;
        setPublicKey(public_key: Buffer): void;
        setPublicKey(public_key: string, encoding: string): void;
        setPrivateKey(private_key: Buffer): void;
        setPrivateKey(private_key: string, encoding: string): void;
        verifyError: number;
    }
    export function getDiffieHellman(group_name: string): DiffieHellman;
    export function pbkdf2(password: string | Buffer, salt: string | Buffer, iterations: number, keylen: number, digest: string, callback: (err: Error, derivedKey: Buffer) => any): void;
    export function pbkdf2Sync(password: string | Buffer, salt: string | Buffer, iterations: number, keylen: number, digest: string): Buffer;
    export function randomBytes(size: number): Buffer;
    export function randomBytes(size: number, callback: (err: Error, buf: Buffer) => void): void;
    export function pseudoRandomBytes(size: number): Buffer;
    export function pseudoRandomBytes(size: number, callback: (err: Error, buf: Buffer) => void): void;
    export function randomFillSync(buffer: Buffer | Uint8Array, offset?: number, size?: number): Buffer;
    export function randomFill(buffer: Buffer, callback: (err: Error, buf: Buffer) => void): void;
    export function randomFill(buffer: Uint8Array, callback: (err: Error, buf: Uint8Array) => void): void;
    export function randomFill(buffer: Buffer, offset: number, callback: (err: Error, buf: Buffer) => void): void;
    export function randomFill(buffer: Uint8Array, offset: number, callback: (err: Error, buf: Uint8Array) => void): void;
    export function randomFill(buffer: Buffer, offset: number, size: number, callback: (err: Error, buf: Buffer) => void): void;
    export function randomFill(buffer: Uint8Array, offset: number, size: number, callback: (err: Error, buf: Uint8Array) => void): void;
    export interface RsaPublicKey {
        key: string;
        padding?: number;
    }
    export interface RsaPrivateKey {
        key: string;
        passphrase?: string;
        padding?: number;
    }
    export function publicEncrypt(public_key: string | RsaPublicKey, buffer: Buffer): Buffer;
    export function privateDecrypt(private_key: string | RsaPrivateKey, buffer: Buffer): Buffer;
    export function privateEncrypt(private_key: string | RsaPrivateKey, buffer: Buffer): Buffer;
    export function publicDecrypt(public_key: string | RsaPublicKey, buffer: Buffer): Buffer;
    export function getCiphers(): string[];
    export function getCurves(): string[];
    export function getHashes(): string[];
    export interface ECDH {
        generateKeys(): Buffer;
        generateKeys(encoding: HexBase64Latin1Encoding): string;
        generateKeys(encoding: HexBase64Latin1Encoding, format: ECDHKeyFormat): string;
        computeSecret(other_public_key: Buffer): Buffer;
        computeSecret(other_public_key: string, input_encoding: HexBase64Latin1Encoding): Buffer;
        computeSecret(other_public_key: string, input_encoding: HexBase64Latin1Encoding, output_encoding: HexBase64Latin1Encoding): string;
        getPrivateKey(): Buffer;
        getPrivateKey(encoding: HexBase64Latin1Encoding): string;
        getPublicKey(): Buffer;
        getPublicKey(encoding: HexBase64Latin1Encoding): string;
        getPublicKey(encoding: HexBase64Latin1Encoding, format: ECDHKeyFormat): string;
        setPrivateKey(private_key: Buffer): void;
        setPrivateKey(private_key: string, encoding: HexBase64Latin1Encoding): void;
    }
    export function createECDH(curve_name: string): ECDH;
    export function timingSafeEqual(a: Buffer, b: Buffer): boolean;
    export var DEFAULT_ENCODING: string;
}

declare module "stream" {
    import * as events from "events";

    class internal extends events.EventEmitter {
        pipe<T extends NodeJS.WritableStream>(destination: T, options?: { end?: boolean; }): T;
    }

    namespace internal {
        export class Stream extends internal { }

        export interface ReadableOptions {
            highWaterMark?: number;
            encoding?: string;
            objectMode?: boolean;
            read?: (this: Readable, size?: number) => any;
            destroy?: (error?: Error) => any;
        }

        export class Readable extends Stream implements NodeJS.ReadableStream {
            readable: boolean;
            readonly readableHighWaterMark: number;
            constructor(opts?: ReadableOptions);
            _read(size: number): void;
            read(size?: number): any;
            setEncoding(encoding: string): this;
            pause(): this;
            resume(): this;
            isPaused(): boolean;
            unpipe<T extends NodeJS.WritableStream>(destination?: T): this;
            unshift(chunk: any): void;
            wrap(oldStream: NodeJS.ReadableStream): this;
            push(chunk: any, encoding?: string): boolean;
            _destroy(err: Error, callback: Function): void;
            destroy(error?: Error): void;

            /**
             * Event emitter
             * The defined events on documents including:
             * 1. close
             * 2. data
             * 3. end
             * 4. readable
             * 5. error
             */
            addListener(event: string, listener: (...args: any[]) => void): this;
            addListener(event: "close", listener: () => void): this;
            addListener(event: "data", listener: (chunk: Buffer | string) => void): this;
            addListener(event: "end", listener: () => void): this;
            addListener(event: "readable", listener: () => void): this;
            addListener(event: "error", listener: (err: Error) => void): this;

            emit(event: string | symbol, ...args: any[]): boolean;
            emit(event: "close"): boolean;
            emit(event: "data", chunk: Buffer | string): boolean;
            emit(event: "end"): boolean;
            emit(event: "readable"): boolean;
            emit(event: "error", err: Error): boolean;

            on(event: string, listener: (...args: any[]) => void): this;
            on(event: "close", listener: () => void): this;
            on(event: "data", listener: (chunk: Buffer | string) => void): this;
            on(event: "end", listener: () => void): this;
            on(event: "readable", listener: () => void): this;
            on(event: "error", listener: (err: Error) => void): this;

            once(event: string, listener: (...args: any[]) => void): this;
            once(event: "close", listener: () => void): this;
            once(event: "data", listener: (chunk: Buffer | string) => void): this;
            once(event: "end", listener: () => void): this;
            once(event: "readable", listener: () => void): this;
            once(event: "error", listener: (err: Error) => void): this;

            prependListener(event: string, listener: (...args: any[]) => void): this;
            prependListener(event: "close", listener: () => void): this;
            prependListener(event: "data", listener: (chunk: Buffer | string) => void): this;
            prependListener(event: "end", listener: () => void): this;
            prependListener(event: "readable", listener: () => void): this;
            prependListener(event: "error", listener: (err: Error) => void): this;

            prependOnceListener(event: string, listener: (...args: any[]) => void): this;
            prependOnceListener(event: "close", listener: () => void): this;
            prependOnceListener(event: "data", listener: (chunk: Buffer | string) => void): this;
            prependOnceListener(event: "end", listener: () => void): this;
            prependOnceListener(event: "readable", listener: () => void): this;
            prependOnceListener(event: "error", listener: (err: Error) => void): this;

            removeListener(event: string, listener: (...args: any[]) => void): this;
            removeListener(event: "close", listener: () => void): this;
            removeListener(event: "data", listener: (chunk: Buffer | string) => void): this;
            removeListener(event: "end", listener: () => void): this;
            removeListener(event: "readable", listener: () => void): this;
            removeListener(event: "error", listener: (err: Error) => void): this;
        }

        export interface WritableOptions {
            highWaterMark?: number;
            decodeStrings?: boolean;
            objectMode?: boolean;
            write?: (chunk: any, encoding: string, callback: Function) => any;
            writev?: (chunks: Array<{ chunk: any, encoding: string }>, callback: Function) => any;
            destroy?: (error?: Error) => any;
            final?: (callback: (error?: Error) => void) => void;
        }

        export class Writable extends Stream implements NodeJS.WritableStream {
            writable: boolean;
            readonly writableHighWaterMark: number;
            constructor(opts?: WritableOptions);
            _write(chunk: any, encoding: string, callback: (err?: Error) => void): void;
            _writev?(chunks: Array<{ chunk: any, encoding: string }>, callback: (err?: Error) => void): void;
            _destroy(err: Error, callback: Function): void;
            _final(callback: Function): void;
            write(chunk: any, cb?: Function): boolean;
            write(chunk: any, encoding?: string, cb?: Function): boolean;
            setDefaultEncoding(encoding: string): this;
            end(cb?: Function): void;
            end(chunk: any, cb?: Function): void;
            end(chunk: any, encoding?: string, cb?: Function): void;
            cork(): void;
            uncork(): void;
            destroy(error?: Error): void;

            /**
             * Event emitter
             * The defined events on documents including:
             * 1. close
             * 2. drain
             * 3. error
             * 4. finish
             * 5. pipe
             * 6. unpipe
             */
            addListener(event: string, listener: (...args: any[]) => void): this;
            addListener(event: "close", listener: () => void): this;
            addListener(event: "drain", listener: () => void): this;
            addListener(event: "error", listener: (err: Error) => void): this;
            addListener(event: "finish", listener: () => void): this;
            addListener(event: "pipe", listener: (src: Readable) => void): this;
            addListener(event: "unpipe", listener: (src: Readable) => void): this;

            emit(event: string | symbol, ...args: any[]): boolean;
            emit(event: "close"): boolean;
            emit(event: "drain", chunk: Buffer | string): boolean;
            emit(event: "error", err: Error): boolean;
            emit(event: "finish"): boolean;
            emit(event: "pipe", src: Readable): boolean;
            emit(event: "unpipe", src: Readable): boolean;

            on(event: string, listener: (...args: any[]) => void): this;
            on(event: "close", listener: () => void): this;
            on(event: "drain", listener: () => void): this;
            on(event: "error", listener: (err: Error) => void): this;
            on(event: "finish", listener: () => void): this;
            on(event: "pipe", listener: (src: Readable) => void): this;
            on(event: "unpipe", listener: (src: Readable) => void): this;

            once(event: string, listener: (...args: any[]) => void): this;
            once(event: "close", listener: () => void): this;
            once(event: "drain", listener: () => void): this;
            once(event: "error", listener: (err: Error) => void): this;
            once(event: "finish", listener: () => void): this;
            once(event: "pipe", listener: (src: Readable) => void): this;
            once(event: "unpipe", listener: (src: Readable) => void): this;

            prependListener(event: string, listener: (...args: any[]) => void): this;
            prependListener(event: "close", listener: () => void): this;
            prependListener(event: "drain", listener: () => void): this;
            prependListener(event: "error", listener: (err: Error) => void): this;
            prependListener(event: "finish", listener: () => void): this;
            prependListener(event: "pipe", listener: (src: Readable) => void): this;
            prependListener(event: "unpipe", listener: (src: Readable) => void): this;

            prependOnceListener(event: string, listener: (...args: any[]) => void): this;
            prependOnceListener(event: "close", listener: () => void): this;
            prependOnceListener(event: "drain", listener: () => void): this;
            prependOnceListener(event: "error", listener: (err: Error) => void): this;
            prependOnceListener(event: "finish", listener: () => void): this;
            prependOnceListener(event: "pipe", listener: (src: Readable) => void): this;
            prependOnceListener(event: "unpipe", listener: (src: Readable) => void): this;

            removeListener(event: string, listener: (...args: any[]) => void): this;
            removeListener(event: "close", listener: () => void): this;
            removeListener(event: "drain", listener: () => void): this;
            removeListener(event: "error", listener: (err: Error) => void): this;
            removeListener(event: "finish", listener: () => void): this;
            removeListener(event: "pipe", listener: (src: Readable) => void): this;
            removeListener(event: "unpipe", listener: (src: Readable) => void): this;
        }

        export interface DuplexOptions extends ReadableOptions, WritableOptions {
            allowHalfOpen?: boolean;
            readableObjectMode?: boolean;
            writableObjectMode?: boolean;
        }

        // Note: Duplex extends both Readable and Writable.
        export class Duplex extends Readable implements Writable {
            writable: boolean;
            readonly writableHighWaterMark: number;
            constructor(opts?: DuplexOptions);
            _write(chunk: any, encoding: string, callback: (err?: Error) => void): void;
            _writev?(chunks: Array<{ chunk: any, encoding: string }>, callback: (err?: Error) => void): void;
            _destroy(err: Error, callback: Function): void;
            _final(callback: Function): void;
            write(chunk: any, cb?: Function): boolean;
            write(chunk: any, encoding?: string, cb?: Function): boolean;
            setDefaultEncoding(encoding: string): this;
            end(cb?: Function): void;
            end(chunk: any, cb?: Function): void;
            end(chunk: any, encoding?: string, cb?: Function): void;
            cork(): void;
            uncork(): void;
        }

        export interface TransformOptions extends DuplexOptions {
            transform?: (chunk: string | Buffer, encoding: string, callback: Function) => any;
            flush?: (callback: Function) => any;
        }

        export class Transform extends Duplex {
            constructor(opts?: TransformOptions);
            _transform(chunk: any, encoding: string, callback: Function): void;
            destroy(error?: Error): void;
        }

        export class PassThrough extends Transform { }
    }

    export = internal;
}

declare module "util" {
    export interface InspectOptions extends NodeJS.InspectOptions { }
    export function format(format: any, ...param: any[]): string;
    export function debug(string: string): void;
    export function error(...param: any[]): void;
    export function puts(...param: any[]): void;
    export function print(...param: any[]): void;
    export function log(string: string): void;
    export var inspect: {
        (object: any, showHidden?: boolean, depth?: number | null, color?: boolean): string;
        (object: any, options: InspectOptions): string;
        colors: {
            [color: string]: [number, number] | undefined
        }
        styles: {
            [style: string]: string | undefined
        }
        defaultOptions: InspectOptions;
        custom: symbol;
    };
    export function isArray(object: any): object is any[];
    export function isRegExp(object: any): object is RegExp;
    export function isDate(object: any): object is Date;
    export function isError(object: any): object is Error;
    export function inherits(constructor: any, superConstructor: any): void;
    export function debuglog(key: string): (msg: string, ...param: any[]) => void;
    export function isBoolean(object: any): object is boolean;
    export function isBuffer(object: any): object is Buffer;
    export function isFunction(object: any): boolean;
    export function isNull(object: any): object is null;
    export function isNullOrUndefined(object: any): object is null | undefined;
    export function isNumber(object: any): object is number;
    export function isObject(object: any): boolean;
    export function isPrimitive(object: any): boolean;
    export function isString(object: any): object is string;
    export function isSymbol(object: any): object is symbol;
    export function isUndefined(object: any): object is undefined;
    export function deprecate<T extends Function>(fn: T, message: string): T;

    export interface CustomPromisify<TCustom extends Function> extends Function {
        __promisify__: TCustom;
    }

    export function callbackify(fn: () => Promise<void>): (callback: (err: NodeJS.ErrnoException) => void) => void;
    export function callbackify<TResult>(fn: () => Promise<TResult>): (callback: (err: NodeJS.ErrnoException, result: TResult) => void) => void;
    export function callbackify<T1>(fn: (arg1: T1) => Promise<void>): (arg1: T1, callback: (err: NodeJS.ErrnoException) => void) => void;
    export function callbackify<T1, TResult>(fn: (arg1: T1) => Promise<TResult>): (arg1: T1, callback: (err: NodeJS.ErrnoException, result: TResult) => void) => void;
    export function callbackify<T1, T2>(fn: (arg1: T1, arg2: T2) => Promise<void>): (arg1: T1, arg2: T2, callback: (err: NodeJS.ErrnoException) => void) => void;
    export function callbackify<T1, T2, TResult>(fn: (arg1: T1, arg2: T2) => Promise<TResult>): (arg1: T1, arg2: T2, callback: (err: NodeJS.ErrnoException, result: TResult) => void) => void;
    export function callbackify<T1, T2, T3>(fn: (arg1: T1, arg2: T2, arg3: T3) => Promise<void>): (arg1: T1, arg2: T2, arg3: T3, callback: (err: NodeJS.ErrnoException) => void) => void;
    export function callbackify<T1, T2, T3, TResult>(fn: (arg1: T1, arg2: T2, arg3: T3) => Promise<TResult>): (arg1: T1, arg2: T2, arg3: T3, callback: (err: NodeJS.ErrnoException, result: TResult) => void) => void;
    export function callbackify<T1, T2, T3, T4>(fn: (arg1: T1, arg2: T2, arg3: T3, arg4: T4) => Promise<void>): (arg1: T1, arg2: T2, arg3: T3, arg4: T4, callback: (err: NodeJS.ErrnoException) => void) => void;
    export function callbackify<T1, T2, T3, T4, TResult>(fn: (arg1: T1, arg2: T2, arg3: T3, arg4: T4) => Promise<TResult>): (arg1: T1, arg2: T2, arg3: T3, arg4: T4, callback: (err: NodeJS.ErrnoException, result: TResult) => void) => void;
    export function callbackify<T1, T2, T3, T4, T5>(fn: (arg1: T1, arg2: T2, arg3: T3, arg4: T4, arg5: T5) => Promise<void>): (arg1: T1, arg2: T2, arg3: T3, arg4: T4, arg5: T5, callback: (err: NodeJS.ErrnoException) => void) => void;
    export function callbackify<T1, T2, T3, T4, T5, TResult>(fn: (arg1: T1, arg2: T2, arg3: T3, arg4: T4, arg5: T5) => Promise<TResult>): (arg1: T1, arg2: T2, arg3: T3, arg4: T4, arg5: T5, callback: (err: NodeJS.ErrnoException, result: TResult) => void) => void;
    export function callbackify<T1, T2, T3, T4, T5, T6>(fn: (arg1: T1, arg2: T2, arg3: T3, arg4: T4, arg5: T5, arg6: T6) => Promise<void>): (arg1: T1, arg2: T2, arg3: T3, arg4: T4, arg5: T5, arg6: T6, callback: (err: NodeJS.ErrnoException) => void) => void;
    export function callbackify<T1, T2, T3, T4, T5, T6, TResult>(fn: (arg1: T1, arg2: T2, arg3: T3, arg4: T4, arg5: T5, arg6: T6) => Promise<TResult>): (arg1: T1, arg2: T2, arg3: T3, arg4: T4, arg5: T5, arg6: T6, callback: (err: NodeJS.ErrnoException, result: TResult) => void) => void;

    export function promisify<TCustom extends Function>(fn: CustomPromisify<TCustom>): TCustom;
    export function promisify<TResult>(fn: (callback: (err: Error | null, result: TResult) => void) => void): () => Promise<TResult>;
    export function promisify(fn: (callback: (err: Error | null) => void) => void): () => Promise<void>;
    export function promisify<T1, TResult>(fn: (arg1: T1, callback: (err: Error | null, result: TResult) => void) => void): (arg1: T1) => Promise<TResult>;
    export function promisify<T1>(fn: (arg1: T1, callback: (err: Error | null) => void) => void): (arg1: T1) => Promise<void>;
    export function promisify<T1, T2, TResult>(fn: (arg1: T1, arg2: T2, callback: (err: Error | null, result: TResult) => void) => void): (arg1: T1, arg2: T2) => Promise<TResult>;
    export function promisify<T1, T2>(fn: (arg1: T1, arg2: T2, callback: (err: Error | null) => void) => void): (arg1: T1, arg2: T2) => Promise<void>;
    export function promisify<T1, T2, T3, TResult>(fn: (arg1: T1, arg2: T2, arg3: T3, callback: (err: Error | null, result: TResult) => void) => void): (arg1: T1, arg2: T2, arg3: T3) => Promise<TResult>;
    export function promisify<T1, T2, T3>(fn: (arg1: T1, arg2: T2, arg3: T3, callback: (err: Error | null) => void) => void): (arg1: T1, arg2: T2, arg3: T3) => Promise<void>;
    export function promisify<T1, T2, T3, T4, TResult>(fn: (arg1: T1, arg2: T2, arg3: T3, arg4: T4, callback: (err: Error | null, result: TResult) => void) => void): (arg1: T1, arg2: T2, arg3: T3, arg4: T4) => Promise<TResult>;
    export function promisify<T1, T2, T3, T4>(fn: (arg1: T1, arg2: T2, arg3: T3, arg4: T4, callback: (err: Error | null) => void) => void): (arg1: T1, arg2: T2, arg3: T3, arg4: T4) => Promise<void>;
    export function promisify<T1, T2, T3, T4, T5, TResult>(fn: (arg1: T1, arg2: T2, arg3: T3, arg4: T4, arg5: T5, callback: (err: Error | null, result: TResult) => void) => void): (arg1: T1, arg2: T2, arg3: T3, arg4: T4, arg5: T5) => Promise<TResult>;
    export function promisify<T1, T2, T3, T4, T5>(fn: (arg1: T1, arg2: T2, arg3: T3, arg4: T4, arg5: T5, callback: (err: Error | null) => void) => void): (arg1: T1, arg2: T2, arg3: T3, arg4: T4, arg5: T5) => Promise<void>;
    export function promisify(fn: Function): Function;
    export namespace promisify {
        const custom: symbol;
    }

    export class TextDecoder {
        readonly encoding: string;
        readonly fatal: boolean;
        readonly ignoreBOM: boolean;
        constructor(
          encoding?: string,
          options?: { fatal?: boolean; ignoreBOM?: boolean }
        );
        decode(
          input?:
            Int8Array
            | Int16Array
            | Int32Array
            | Uint8Array
            | Uint16Array
            | Uint32Array
            | Uint8ClampedArray
            | Float32Array
            | Float64Array
            | DataView
            | ArrayBuffer
            | null,
          options?: { stream?: boolean }
        ): string;
    }

    export class TextEncoder {
        readonly encoding: string;
        constructor();
        encode(input?: string): Uint8Array;
    }
}

declare module "assert" {
    function internal(value: any, message?: string): void;
    namespace internal {
        export class AssertionError implements Error {
            name: string;
            message: string;
            actual: any;
            expected: any;
            operator: string;
            generatedMessage: boolean;

            constructor(options?: {
                message?: string; actual?: any; expected?: any;
                operator?: string; stackStartFunction?: Function
            });
        }

        export function fail(message: string): never;
        export function fail(actual: any, expected: any, message?: string, operator?: string): never;
        export function ok(value: any, message?: string): void;
        export function equal(actual: any, expected: any, message?: string): void;
        export function notEqual(actual: any, expected: any, message?: string): void;
        export function deepEqual(actual: any, expected: any, message?: string): void;
        export function notDeepEqual(acutal: any, expected: any, message?: string): void;
        export function strictEqual(actual: any, expected: any, message?: string): void;
        export function notStrictEqual(actual: any, expected: any, message?: string): void;
        export function deepStrictEqual(actual: any, expected: any, message?: string): void;
        export function notDeepStrictEqual(actual: any, expected: any, message?: string): void;

        export function throws(block: Function, message?: string): void;
        export function throws(block: Function, error: Function, message?: string): void;
        export function throws(block: Function, error: RegExp, message?: string): void;
        export function throws(block: Function, error: (err: any) => boolean, message?: string): void;

        export function doesNotThrow(block: Function, message?: string): void;
        export function doesNotThrow(block: Function, error: Function, message?: string): void;
        export function doesNotThrow(block: Function, error: RegExp, message?: string): void;
        export function doesNotThrow(block: Function, error: (err: any) => boolean, message?: string): void;

        export function ifError(value: any): void;
    }

    export = internal;
}

declare module "tty" {
    import * as net from "net";

    export function isatty(fd: number): boolean;
    export class ReadStream extends net.Socket {
        isRaw: boolean;
        setRawMode(mode: boolean): void;
        isTTY: boolean;
    }
    export class WriteStream extends net.Socket {
        columns: number;
        rows: number;
        isTTY: boolean;
    }
}

declare module "domain" {
    import * as events from "events";

    export class Domain extends events.EventEmitter implements NodeJS.Domain {
        run(fn: Function): void;
        add(emitter: events.EventEmitter): void;
        remove(emitter: events.EventEmitter): void;
        bind(cb: (err: Error, data: any) => any): any;
        intercept(cb: (data: any) => any): any;
        dispose(): void;
        members: any[];
        enter(): void;
        exit(): void;
    }

    export function create(): Domain;
}

declare module "constants" {
    export var E2BIG: number;
    export var EACCES: number;
    export var EADDRINUSE: number;
    export var EADDRNOTAVAIL: number;
    export var EAFNOSUPPORT: number;
    export var EAGAIN: number;
    export var EALREADY: number;
    export var EBADF: number;
    export var EBADMSG: number;
    export var EBUSY: number;
    export var ECANCELED: number;
    export var ECHILD: number;
    export var ECONNABORTED: number;
    export var ECONNREFUSED: number;
    export var ECONNRESET: number;
    export var EDEADLK: number;
    export var EDESTADDRREQ: number;
    export var EDOM: number;
    export var EEXIST: number;
    export var EFAULT: number;
    export var EFBIG: number;
    export var EHOSTUNREACH: number;
    export var EIDRM: number;
    export var EILSEQ: number;
    export var EINPROGRESS: number;
    export var EINTR: number;
    export var EINVAL: number;
    export var EIO: number;
    export var EISCONN: number;
    export var EISDIR: number;
    export var ELOOP: number;
    export var EMFILE: number;
    export var EMLINK: number;
    export var EMSGSIZE: number;
    export var ENAMETOOLONG: number;
    export var ENETDOWN: number;
    export var ENETRESET: number;
    export var ENETUNREACH: number;
    export var ENFILE: number;
    export var ENOBUFS: number;
    export var ENODATA: number;
    export var ENODEV: number;
    export var ENOENT: number;
    export var ENOEXEC: number;
    export var ENOLCK: number;
    export var ENOLINK: number;
    export var ENOMEM: number;
    export var ENOMSG: number;
    export var ENOPROTOOPT: number;
    export var ENOSPC: number;
    export var ENOSR: number;
    export var ENOSTR: number;
    export var ENOSYS: number;
    export var ENOTCONN: number;
    export var ENOTDIR: number;
    export var ENOTEMPTY: number;
    export var ENOTSOCK: number;
    export var ENOTSUP: number;
    export var ENOTTY: number;
    export var ENXIO: number;
    export var EOPNOTSUPP: number;
    export var EOVERFLOW: number;
    export var EPERM: number;
    export var EPIPE: number;
    export var EPROTO: number;
    export var EPROTONOSUPPORT: number;
    export var EPROTOTYPE: number;
    export var ERANGE: number;
    export var EROFS: number;
    export var ESPIPE: number;
    export var ESRCH: number;
    export var ETIME: number;
    export var ETIMEDOUT: number;
    export var ETXTBSY: number;
    export var EWOULDBLOCK: number;
    export var EXDEV: number;
    export var WSAEINTR: number;
    export var WSAEBADF: number;
    export var WSAEACCES: number;
    export var WSAEFAULT: number;
    export var WSAEINVAL: number;
    export var WSAEMFILE: number;
    export var WSAEWOULDBLOCK: number;
    export var WSAEINPROGRESS: number;
    export var WSAEALREADY: number;
    export var WSAENOTSOCK: number;
    export var WSAEDESTADDRREQ: number;
    export var WSAEMSGSIZE: number;
    export var WSAEPROTOTYPE: number;
    export var WSAENOPROTOOPT: number;
    export var WSAEPROTONOSUPPORT: number;
    export var WSAESOCKTNOSUPPORT: number;
    export var WSAEOPNOTSUPP: number;
    export var WSAEPFNOSUPPORT: number;
    export var WSAEAFNOSUPPORT: number;
    export var WSAEADDRINUSE: number;
    export var WSAEADDRNOTAVAIL: number;
    export var WSAENETDOWN: number;
    export var WSAENETUNREACH: number;
    export var WSAENETRESET: number;
    export var WSAECONNABORTED: number;
    export var WSAECONNRESET: number;
    export var WSAENOBUFS: number;
    export var WSAEISCONN: number;
    export var WSAENOTCONN: number;
    export var WSAESHUTDOWN: number;
    export var WSAETOOMANYREFS: number;
    export var WSAETIMEDOUT: number;
    export var WSAECONNREFUSED: number;
    export var WSAELOOP: number;
    export var WSAENAMETOOLONG: number;
    export var WSAEHOSTDOWN: number;
    export var WSAEHOSTUNREACH: number;
    export var WSAENOTEMPTY: number;
    export var WSAEPROCLIM: number;
    export var WSAEUSERS: number;
    export var WSAEDQUOT: number;
    export var WSAESTALE: number;
    export var WSAEREMOTE: number;
    export var WSASYSNOTREADY: number;
    export var WSAVERNOTSUPPORTED: number;
    export var WSANOTINITIALISED: number;
    export var WSAEDISCON: number;
    export var WSAENOMORE: number;
    export var WSAECANCELLED: number;
    export var WSAEINVALIDPROCTABLE: number;
    export var WSAEINVALIDPROVIDER: number;
    export var WSAEPROVIDERFAILEDINIT: number;
    export var WSASYSCALLFAILURE: number;
    export var WSASERVICE_NOT_FOUND: number;
    export var WSATYPE_NOT_FOUND: number;
    export var WSA_E_NO_MORE: number;
    export var WSA_E_CANCELLED: number;
    export var WSAEREFUSED: number;
    export var SIGHUP: number;
    export var SIGINT: number;
    export var SIGILL: number;
    export var SIGABRT: number;
    export var SIGFPE: number;
    export var SIGKILL: number;
    export var SIGSEGV: number;
    export var SIGTERM: number;
    export var SIGBREAK: number;
    export var SIGWINCH: number;
    export var SSL_OP_ALL: number;
    export var SSL_OP_ALLOW_UNSAFE_LEGACY_RENEGOTIATION: number;
    export var SSL_OP_CIPHER_SERVER_PREFERENCE: number;
    export var SSL_OP_CISCO_ANYCONNECT: number;
    export var SSL_OP_COOKIE_EXCHANGE: number;
    export var SSL_OP_CRYPTOPRO_TLSEXT_BUG: number;
    export var SSL_OP_DONT_INSERT_EMPTY_FRAGMENTS: number;
    export var SSL_OP_EPHEMERAL_RSA: number;
    export var SSL_OP_LEGACY_SERVER_CONNECT: number;
    export var SSL_OP_MICROSOFT_BIG_SSLV3_BUFFER: number;
    export var SSL_OP_MICROSOFT_SESS_ID_BUG: number;
    export var SSL_OP_MSIE_SSLV2_RSA_PADDING: number;
    export var SSL_OP_NETSCAPE_CA_DN_BUG: number;
    export var SSL_OP_NETSCAPE_CHALLENGE_BUG: number;
    export var SSL_OP_NETSCAPE_DEMO_CIPHER_CHANGE_BUG: number;
    export var SSL_OP_NETSCAPE_REUSE_CIPHER_CHANGE_BUG: number;
    export var SSL_OP_NO_COMPRESSION: number;
    export var SSL_OP_NO_QUERY_MTU: number;
    export var SSL_OP_NO_SESSION_RESUMPTION_ON_RENEGOTIATION: number;
    export var SSL_OP_NO_SSLv2: number;
    export var SSL_OP_NO_SSLv3: number;
    export var SSL_OP_NO_TICKET: number;
    export var SSL_OP_NO_TLSv1: number;
    export var SSL_OP_NO_TLSv1_1: number;
    export var SSL_OP_NO_TLSv1_2: number;
    export var SSL_OP_PKCS1_CHECK_1: number;
    export var SSL_OP_PKCS1_CHECK_2: number;
    export var SSL_OP_SINGLE_DH_USE: number;
    export var SSL_OP_SINGLE_ECDH_USE: number;
    export var SSL_OP_SSLEAY_080_CLIENT_DH_BUG: number;
    export var SSL_OP_SSLREF2_REUSE_CERT_TYPE_BUG: number;
    export var SSL_OP_TLS_BLOCK_PADDING_BUG: number;
    export var SSL_OP_TLS_D5_BUG: number;
    export var SSL_OP_TLS_ROLLBACK_BUG: number;
    export var ENGINE_METHOD_DSA: number;
    export var ENGINE_METHOD_DH: number;
    export var ENGINE_METHOD_RAND: number;
    export var ENGINE_METHOD_ECDH: number;
    export var ENGINE_METHOD_ECDSA: number;
    export var ENGINE_METHOD_CIPHERS: number;
    export var ENGINE_METHOD_DIGESTS: number;
    export var ENGINE_METHOD_STORE: number;
    export var ENGINE_METHOD_PKEY_METHS: number;
    export var ENGINE_METHOD_PKEY_ASN1_METHS: number;
    export var ENGINE_METHOD_ALL: number;
    export var ENGINE_METHOD_NONE: number;
    export var DH_CHECK_P_NOT_SAFE_PRIME: number;
    export var DH_CHECK_P_NOT_PRIME: number;
    export var DH_UNABLE_TO_CHECK_GENERATOR: number;
    export var DH_NOT_SUITABLE_GENERATOR: number;
    export var NPN_ENABLED: number;
    export var RSA_PKCS1_PADDING: number;
    export var RSA_SSLV23_PADDING: number;
    export var RSA_NO_PADDING: number;
    export var RSA_PKCS1_OAEP_PADDING: number;
    export var RSA_X931_PADDING: number;
    export var RSA_PKCS1_PSS_PADDING: number;
    export var POINT_CONVERSION_COMPRESSED: number;
    export var POINT_CONVERSION_UNCOMPRESSED: number;
    export var POINT_CONVERSION_HYBRID: number;
    export var O_RDONLY: number;
    export var O_WRONLY: number;
    export var O_RDWR: number;
    export var S_IFMT: number;
    export var S_IFREG: number;
    export var S_IFDIR: number;
    export var S_IFCHR: number;
    export var S_IFBLK: number;
    export var S_IFIFO: number;
    export var S_IFSOCK: number;
    export var S_IRWXU: number;
    export var S_IRUSR: number;
    export var S_IWUSR: number;
    export var S_IXUSR: number;
    export var S_IRWXG: number;
    export var S_IRGRP: number;
    export var S_IWGRP: number;
    export var S_IXGRP: number;
    export var S_IRWXO: number;
    export var S_IROTH: number;
    export var S_IWOTH: number;
    export var S_IXOTH: number;
    export var S_IFLNK: number;
    export var O_CREAT: number;
    export var O_EXCL: number;
    export var O_NOCTTY: number;
    export var O_DIRECTORY: number;
    export var O_NOATIME: number;
    export var O_NOFOLLOW: number;
    export var O_SYNC: number;
    export var O_DSYNC: number;
    export var O_SYMLINK: number;
    export var O_DIRECT: number;
    export var O_NONBLOCK: number;
    export var O_TRUNC: number;
    export var O_APPEND: number;
    export var F_OK: number;
    export var R_OK: number;
    export var W_OK: number;
    export var X_OK: number;
    export var UV_UDP_REUSEADDR: number;
    export var SIGQUIT: number;
    export var SIGTRAP: number;
    export var SIGIOT: number;
    export var SIGBUS: number;
    export var SIGUSR1: number;
    export var SIGUSR2: number;
    export var SIGPIPE: number;
    export var SIGALRM: number;
    export var SIGCHLD: number;
    export var SIGSTKFLT: number;
    export var SIGCONT: number;
    export var SIGSTOP: number;
    export var SIGTSTP: number;
    export var SIGTTIN: number;
    export var SIGTTOU: number;
    export var SIGURG: number;
    export var SIGXCPU: number;
    export var SIGXFSZ: number;
    export var SIGVTALRM: number;
    export var SIGPROF: number;
    export var SIGIO: number;
    export var SIGPOLL: number;
    export var SIGPWR: number;
    export var SIGSYS: number;
    export var SIGUNUSED: number;
    export var defaultCoreCipherList: string;
    export var defaultCipherList: string;
    export var ENGINE_METHOD_RSA: number;
    export var ALPN_ENABLED: number;
}

declare module "module" {
    export = NodeJS.Module;
}

declare module "process" {
    export = process;
}

// tslint:disable-next-line:no-declare-current-package
declare module "v8" {
    interface HeapSpaceInfo {
        space_name: string;
        space_size: number;
        space_used_size: number;
        space_available_size: number;
        physical_space_size: number;
    }

    // ** Signifies if the --zap_code_space option is enabled or not.  1 == enabled, 0 == disabled. */
    type DoesZapCodeSpaceFlag = 0 | 1;

    interface HeapInfo {
        total_heap_size: number;
        total_heap_size_executable: number;
        total_physical_size: number;
        total_available_size: number;
        used_heap_size: number;
        heap_size_limit: number;
        malloced_memory: number;
        peak_malloced_memory: number;
        does_zap_garbage: DoesZapCodeSpaceFlag;
    }

    export function getHeapStatistics(): HeapInfo;
    export function getHeapSpaceStatistics(): HeapSpaceInfo[];
    export function setFlagsFromString(flags: string): void;
}

declare module "timers" {
    export function setTimeout(callback: (...args: any[]) => void, ms: number, ...args: any[]): NodeJS.Timer;
    export namespace setTimeout {
        export function __promisify__(ms: number): Promise<void>;
        export function __promisify__<T>(ms: number, value: T): Promise<T>;
    }
    export function clearTimeout(timeoutId: NodeJS.Timer): void;
    export function setInterval(callback: (...args: any[]) => void, ms: number, ...args: any[]): NodeJS.Timer;
    export function clearInterval(intervalId: NodeJS.Timer): void;
    export function setImmediate(callback: (...args: any[]) => void, ...args: any[]): any;
    export namespace setImmediate {
        export function __promisify__(): Promise<void>;
        export function __promisify__<T>(value: T): Promise<T>;
    }
    export function clearImmediate(immediateId: any): void;
}

declare module "console" {
    export = console;
}

/**
 * Async Hooks module: https://nodejs.org/api/async_hooks.html
 */
declare module "async_hooks" {
    /**
     * Returns the asyncId of the current execution context.
     */
    export function executionAsyncId(): number;
    /// @deprecated - replaced by executionAsyncId()
    export function currentId(): number;

    /**
     * Returns the ID of the resource responsible for calling the callback that is currently being executed.
     */
    export function triggerAsyncId(): number;
    /// @deprecated - replaced by triggerAsyncId()
    export function triggerId(): number;

    export interface HookCallbacks {
        /**
         * Called when a class is constructed that has the possibility to emit an asynchronous event.
         * @param asyncId a unique ID for the async resource
         * @param type the type of the async resource
         * @param triggerAsyncId the unique ID of the async resource in whose execution context this async resource was created
         * @param resource reference to the resource representing the async operation, needs to be released during destroy
         */
        init?(asyncId: number, type: string, triggerAsyncId: number, resource: Object): void;

        /**
         * When an asynchronous operation is initiated or completes a callback is called to notify the user.
         * The before callback is called just before said callback is executed.
         * @param asyncId the unique identifier assigned to the resource about to execute the callback.
         */
        before?(asyncId: number): void;

        /**
         * Called immediately after the callback specified in before is completed.
         * @param asyncId the unique identifier assigned to the resource which has executed the callback.
         */
        after?(asyncId: number): void;

        /**
         * Called when a promise has resolve() called. This may not be in the same execution id
         * as the promise itself.
         * @param asyncId the unique id for the promise that was resolve()d.
         */
        promiseResolve?(asyncId: number): void;

        /**
         * Called after the resource corresponding to asyncId is destroyed
         * @param asyncId a unique ID for the async resource
         */
        destroy?(asyncId: number): void;
    }

    export interface AsyncHook {
        /**
         * Enable the callbacks for a given AsyncHook instance. If no callbacks are provided enabling is a noop.
         */
        enable(): this;

        /**
         * Disable the callbacks for a given AsyncHook instance from the global pool of AsyncHook callbacks to be executed. Once a hook has been disabled it will not be called again until enabled.
         */
        disable(): this;
    }

    /**
     * Registers functions to be called for different lifetime events of each async operation.
     * @param options the callbacks to register
     * @return an AsyncHooks instance used for disabling and enabling hooks
     */
    export function createHook(options: HookCallbacks): AsyncHook;

    export interface AsyncResourceOptions {
      /**
       * The ID of the execution context that created this async event.
       * Default: `executionAsyncId()`
       */
      triggerAsyncId?: number;

      /**
       * Disables automatic `emitDestroy` when the object is garbage collected.
       * This usually does not need to be set (even if `emitDestroy` is called
       * manually), unless the resource's `asyncId` is retrieved and the
       * sensitive API's `emitDestroy` is called with it.
       * Default: `false`
       */
      requireManualDestroy?: boolean;
    }

    /**
     * The class AsyncResource was designed to be extended by the embedder's async resources.
     * Using this users can easily trigger the lifetime events of their own resources.
     */
    export class AsyncResource {
        /**
         * AsyncResource() is meant to be extended. Instantiating a
         * new AsyncResource() also triggers init. If triggerAsyncId is omitted then
         * async_hook.executionAsyncId() is used.
         * @param type The type of async event.
         * @param triggerAsyncId The ID of the execution context that created
         *   this async event (default: `executionAsyncId()`), or an
         *   AsyncResourceOptions object (since 8.10)
         */
        constructor(type: string, triggerAsyncId?: number|AsyncResourceOptions);

        /**
         * Call AsyncHooks before callbacks.
         */
        emitBefore(): void;

        /**
         * Call AsyncHooks after callbacks
         */
        emitAfter(): void;

        /**
         * Call AsyncHooks destroy callbacks.
         */
        emitDestroy(): void;

        /**
         * @return the unique ID assigned to this AsyncResource instance.
         */
        asyncId(): number;

        /**
         * @return the trigger ID for this AsyncResource instance.
         */
        triggerAsyncId(): number;
    }
}

declare module "http2" {
    import * as events from "events";
    import * as fs from "fs";
    import * as net from "net";
    import * as stream from "stream";
    import * as tls from "tls";
    import * as url from "url";

    import { IncomingHttpHeaders, OutgoingHttpHeaders } from "http";
    export { IncomingHttpHeaders, OutgoingHttpHeaders } from "http";

    // Http2Stream

    export interface StreamPriorityOptions {
        exclusive?: boolean;
        parent?: number;
        weight?: number;
        silent?: boolean;
    }

    export interface StreamState {
        localWindowSize?: number;
        state?: number;
        streamLocalClose?: number;
        streamRemoteClose?: number;
        sumDependencyWeight?: number;
        weight?: number;
    }

    export interface ServerStreamResponseOptions {
        endStream?: boolean;
        getTrailers?: (trailers: OutgoingHttpHeaders) => void;
    }

    export interface StatOptions {
        offset: number;
        length: number;
    }

    export interface ServerStreamFileResponseOptions {
        statCheck?: (stats: fs.Stats, headers: OutgoingHttpHeaders, statOptions: StatOptions) => void | boolean;
        getTrailers?: (trailers: OutgoingHttpHeaders) => void;
        offset?: number;
        length?: number;
    }

    export interface ServerStreamFileResponseOptionsWithError extends ServerStreamFileResponseOptions {
        onError?: (err: NodeJS.ErrnoException) => void;
    }

    export interface Http2Stream extends stream.Duplex {
        readonly aborted: boolean;
        readonly destroyed: boolean;
        priority(options: StreamPriorityOptions): void;
        readonly rstCode: number;
        rstStream(code: number): void;
        rstWithNoError(): void;
        rstWithProtocolError(): void;
        rstWithCancel(): void;
        rstWithRefuse(): void;
        rstWithInternalError(): void;
        readonly session: Http2Session;
        setTimeout(msecs: number, callback?: () => void): void;
        readonly state: StreamState;

        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "aborted", listener: () => void): this;
        addListener(event: "close", listener: () => void): this;
        addListener(event: "data", listener: (chunk: Buffer | string) => void): this;
        addListener(event: "drain", listener: () => void): this;
        addListener(event: "end", listener: () => void): this;
        addListener(event: "error", listener: (err: Error) => void): this;
        addListener(event: "finish", listener: () => void): this;
        addListener(event: "frameError", listener: (frameType: number, errorCode: number) => void): this;
        addListener(event: "pipe", listener: (src: stream.Readable) => void): this;
        addListener(event: "unpipe", listener: (src: stream.Readable) => void): this;
        addListener(event: "streamClosed", listener: (code: number) => void): this;
        addListener(event: "timeout", listener: () => void): this;
        addListener(event: "trailers", listener: (trailers: IncomingHttpHeaders, flags: number) => void): this;

        emit(event: string | symbol, ...args: any[]): boolean;
        emit(event: "aborted"): boolean;
        emit(event: "close"): boolean;
        emit(event: "data", chunk: Buffer | string): boolean;
        emit(event: "drain"): boolean;
        emit(event: "end"): boolean;
        emit(event: "error", err: Error): boolean;
        emit(event: "finish"): boolean;
        emit(event: "frameError", frameType: number, errorCode: number): boolean;
        emit(event: "pipe", src: stream.Readable): boolean;
        emit(event: "unpipe", src: stream.Readable): boolean;
        emit(event: "streamClosed", code: number): boolean;
        emit(event: "timeout"): boolean;
        emit(event: "trailers", trailers: IncomingHttpHeaders, flags: number): boolean;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "aborted", listener: () => void): this;
        on(event: "close", listener: () => void): this;
        on(event: "data", listener: (chunk: Buffer | string) => void): this;
        on(event: "drain", listener: () => void): this;
        on(event: "end", listener: () => void): this;
        on(event: "error", listener: (err: Error) => void): this;
        on(event: "finish", listener: () => void): this;
        on(event: "frameError", listener: (frameType: number, errorCode: number) => void): this;
        on(event: "pipe", listener: (src: stream.Readable) => void): this;
        on(event: "unpipe", listener: (src: stream.Readable) => void): this;
        on(event: "streamClosed", listener: (code: number) => void): this;
        on(event: "timeout", listener: () => void): this;
        on(event: "trailers", listener: (trailers: IncomingHttpHeaders, flags: number) => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "aborted", listener: () => void): this;
        once(event: "close", listener: () => void): this;
        once(event: "data", listener: (chunk: Buffer | string) => void): this;
        once(event: "drain", listener: () => void): this;
        once(event: "end", listener: () => void): this;
        once(event: "error", listener: (err: Error) => void): this;
        once(event: "finish", listener: () => void): this;
        once(event: "frameError", listener: (frameType: number, errorCode: number) => void): this;
        once(event: "pipe", listener: (src: stream.Readable) => void): this;
        once(event: "unpipe", listener: (src: stream.Readable) => void): this;
        once(event: "streamClosed", listener: (code: number) => void): this;
        once(event: "timeout", listener: () => void): this;
        once(event: "trailers", listener: (trailers: IncomingHttpHeaders, flags: number) => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "aborted", listener: () => void): this;
        prependListener(event: "close", listener: () => void): this;
        prependListener(event: "data", listener: (chunk: Buffer | string) => void): this;
        prependListener(event: "drain", listener: () => void): this;
        prependListener(event: "end", listener: () => void): this;
        prependListener(event: "error", listener: (err: Error) => void): this;
        prependListener(event: "finish", listener: () => void): this;
        prependListener(event: "frameError", listener: (frameType: number, errorCode: number) => void): this;
        prependListener(event: "pipe", listener: (src: stream.Readable) => void): this;
        prependListener(event: "unpipe", listener: (src: stream.Readable) => void): this;
        prependListener(event: "streamClosed", listener: (code: number) => void): this;
        prependListener(event: "timeout", listener: () => void): this;
        prependListener(event: "trailers", listener: (trailers: IncomingHttpHeaders, flags: number) => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "aborted", listener: () => void): this;
        prependOnceListener(event: "close", listener: () => void): this;
        prependOnceListener(event: "data", listener: (chunk: Buffer | string) => void): this;
        prependOnceListener(event: "drain", listener: () => void): this;
        prependOnceListener(event: "end", listener: () => void): this;
        prependOnceListener(event: "error", listener: (err: Error) => void): this;
        prependOnceListener(event: "finish", listener: () => void): this;
        prependOnceListener(event: "frameError", listener: (frameType: number, errorCode: number) => void): this;
        prependOnceListener(event: "pipe", listener: (src: stream.Readable) => void): this;
        prependOnceListener(event: "unpipe", listener: (src: stream.Readable) => void): this;
        prependOnceListener(event: "streamClosed", listener: (code: number) => void): this;
        prependOnceListener(event: "timeout", listener: () => void): this;
        prependOnceListener(event: "trailers", listener: (trailers: IncomingHttpHeaders, flags: number) => void): this;
    }

    export interface ClientHttp2Stream extends Http2Stream {
        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "headers", listener: (headers: IncomingHttpHeaders, flags: number) => void): this;
        addListener(event: "push", listener: (headers: IncomingHttpHeaders, flags: number) => void): this;
        addListener(event: "response", listener: (headers: IncomingHttpHeaders, flags: number) => void): this;

        emit(event: string | symbol, ...args: any[]): boolean;
        emit(event: "headers", headers: IncomingHttpHeaders, flags: number): boolean;
        emit(event: "push", headers: IncomingHttpHeaders, flags: number): boolean;
        emit(event: "response", headers: IncomingHttpHeaders, flags: number): boolean;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "headers", listener: (headers: IncomingHttpHeaders, flags: number) => void): this;
        on(event: "push", listener: (headers: IncomingHttpHeaders, flags: number) => void): this;
        on(event: "response", listener: (headers: IncomingHttpHeaders, flags: number) => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "headers", listener: (headers: IncomingHttpHeaders, flags: number) => void): this;
        once(event: "push", listener: (headers: IncomingHttpHeaders, flags: number) => void): this;
        once(event: "response", listener: (headers: IncomingHttpHeaders, flags: number) => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "headers", listener: (headers: IncomingHttpHeaders, flags: number) => void): this;
        prependListener(event: "push", listener: (headers: IncomingHttpHeaders, flags: number) => void): this;
        prependListener(event: "response", listener: (headers: IncomingHttpHeaders, flags: number) => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "headers", listener: (headers: IncomingHttpHeaders, flags: number) => void): this;
        prependOnceListener(event: "push", listener: (headers: IncomingHttpHeaders, flags: number) => void): this;
        prependOnceListener(event: "response", listener: (headers: IncomingHttpHeaders, flags: number) => void): this;
    }

    export interface ServerHttp2Stream extends Http2Stream {
        additionalHeaders(headers: OutgoingHttpHeaders): void;
        readonly headersSent: boolean;
        readonly pushAllowed: boolean;
        pushStream(headers: OutgoingHttpHeaders, callback?: (pushStream: ServerHttp2Stream) => void): void;
        pushStream(headers: OutgoingHttpHeaders, options?: StreamPriorityOptions, callback?: (pushStream: ServerHttp2Stream) => void): void;
        respond(headers?: OutgoingHttpHeaders, options?: ServerStreamResponseOptions): void;
        respondWithFD(fd: number, headers?: OutgoingHttpHeaders, options?: ServerStreamFileResponseOptions): void;
        respondWithFile(path: string, headers?: OutgoingHttpHeaders, options?: ServerStreamFileResponseOptionsWithError): void;
    }

    // Http2Session

    export interface Settings {
        headerTableSize?: number;
        enablePush?: boolean;
        initialWindowSize?: number;
        maxFrameSize?: number;
        maxConcurrentStreams?: number;
        maxHeaderListSize?: number;
    }

    export interface ClientSessionRequestOptions {
        endStream?: boolean;
        exclusive?: boolean;
        parent?: number;
        weight?: number;
        getTrailers?: (trailers: OutgoingHttpHeaders, flags: number) => void;
    }

    export interface SessionShutdownOptions {
        graceful?: boolean;
        errorCode?: number;
        lastStreamID?: number;
        opaqueData?: Buffer | Uint8Array;
    }

    export interface SessionState {
        effectiveLocalWindowSize?: number;
        effectiveRecvDataLength?: number;
        nextStreamID?: number;
        localWindowSize?: number;
        lastProcStreamID?: number;
        remoteWindowSize?: number;
        outboundQueueSize?: number;
        deflateDynamicTableSize?: number;
        inflateDynamicTableSize?: number;
    }

    export interface Http2Session extends events.EventEmitter {
        destroy(): void;
        readonly destroyed: boolean;
        readonly localSettings: Settings;
        readonly pendingSettingsAck: boolean;
        readonly remoteSettings: Settings;
        rstStream(stream: Http2Stream, code?: number): void;
        setTimeout(msecs: number, callback?: () => void): void;
        shutdown(callback?: () => void): void;
        shutdown(options: SessionShutdownOptions, callback?: () => void): void;
        readonly socket: net.Socket | tls.TLSSocket;
        readonly state: SessionState;
        priority(stream: Http2Stream, options: StreamPriorityOptions): void;
        settings(settings: Settings): void;
        readonly type: number;

        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "close", listener: () => void): this;
        addListener(event: "error", listener: (err: Error) => void): this;
        addListener(event: "frameError", listener: (frameType: number, errorCode: number, streamID: number) => void): this;
        addListener(event: "goaway", listener: (errorCode: number, lastStreamID: number, opaqueData: Buffer) => void): this;
        addListener(event: "localSettings", listener: (settings: Settings) => void): this;
        addListener(event: "remoteSettings", listener: (settings: Settings) => void): this;
        addListener(event: "socketError", listener: (err: Error) => void): this;
        addListener(event: "timeout", listener: () => void): this;

        emit(event: string | symbol, ...args: any[]): boolean;
        emit(event: "close"): boolean;
        emit(event: "error", err: Error): boolean;
        emit(event: "frameError", frameType: number, errorCode: number, streamID: number): boolean;
        emit(event: "goaway", errorCode: number, lastStreamID: number, opaqueData: Buffer): boolean;
        emit(event: "localSettings", settings: Settings): boolean;
        emit(event: "remoteSettings", settings: Settings): boolean;
        emit(event: "socketError", err: Error): boolean;
        emit(event: "timeout"): boolean;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "close", listener: () => void): this;
        on(event: "error", listener: (err: Error) => void): this;
        on(event: "frameError", listener: (frameType: number, errorCode: number, streamID: number) => void): this;
        on(event: "goaway", listener: (errorCode: number, lastStreamID: number, opaqueData: Buffer) => void): this;
        on(event: "localSettings", listener: (settings: Settings) => void): this;
        on(event: "remoteSettings", listener: (settings: Settings) => void): this;
        on(event: "socketError", listener: (err: Error) => void): this;
        on(event: "timeout", listener: () => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "close", listener: () => void): this;
        once(event: "error", listener: (err: Error) => void): this;
        once(event: "frameError", listener: (frameType: number, errorCode: number, streamID: number) => void): this;
        once(event: "goaway", listener: (errorCode: number, lastStreamID: number, opaqueData: Buffer) => void): this;
        once(event: "localSettings", listener: (settings: Settings) => void): this;
        once(event: "remoteSettings", listener: (settings: Settings) => void): this;
        once(event: "socketError", listener: (err: Error) => void): this;
        once(event: "timeout", listener: () => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "close", listener: () => void): this;
        prependListener(event: "error", listener: (err: Error) => void): this;
        prependListener(event: "frameError", listener: (frameType: number, errorCode: number, streamID: number) => void): this;
        prependListener(event: "goaway", listener: (errorCode: number, lastStreamID: number, opaqueData: Buffer) => void): this;
        prependListener(event: "localSettings", listener: (settings: Settings) => void): this;
        prependListener(event: "remoteSettings", listener: (settings: Settings) => void): this;
        prependListener(event: "socketError", listener: (err: Error) => void): this;
        prependListener(event: "timeout", listener: () => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "close", listener: () => void): this;
        prependOnceListener(event: "error", listener: (err: Error) => void): this;
        prependOnceListener(event: "frameError", listener: (frameType: number, errorCode: number, streamID: number) => void): this;
        prependOnceListener(event: "goaway", listener: (errorCode: number, lastStreamID: number, opaqueData: Buffer) => void): this;
        prependOnceListener(event: "localSettings", listener: (settings: Settings) => void): this;
        prependOnceListener(event: "remoteSettings", listener: (settings: Settings) => void): this;
        prependOnceListener(event: "socketError", listener: (err: Error) => void): this;
        prependOnceListener(event: "timeout", listener: () => void): this;
    }

    export interface ClientHttp2Session extends Http2Session {
        request(headers?: OutgoingHttpHeaders, options?: ClientSessionRequestOptions): ClientHttp2Stream;

        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "connect", listener: (session: ClientHttp2Session, socket: net.Socket | tls.TLSSocket) => void): this;
        addListener(event: "stream", listener: (stream: ClientHttp2Stream, headers: IncomingHttpHeaders, flags: number) => void): this;

        emit(event: string | symbol, ...args: any[]): boolean;
        emit(event: "connect", session: ClientHttp2Session, socket: net.Socket | tls.TLSSocket): boolean;
        emit(event: "stream", stream: ClientHttp2Stream, headers: IncomingHttpHeaders, flags: number): boolean;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "connect", listener: (session: ClientHttp2Session, socket: net.Socket | tls.TLSSocket) => void): this;
        on(event: "stream", listener: (stream: ClientHttp2Stream, headers: IncomingHttpHeaders, flags: number) => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "connect", listener: (session: ClientHttp2Session, socket: net.Socket | tls.TLSSocket) => void): this;
        once(event: "stream", listener: (stream: ClientHttp2Stream, headers: IncomingHttpHeaders, flags: number) => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "connect", listener: (session: ClientHttp2Session, socket: net.Socket | tls.TLSSocket) => void): this;
        prependListener(event: "stream", listener: (stream: ClientHttp2Stream, headers: IncomingHttpHeaders, flags: number) => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "connect", listener: (session: ClientHttp2Session, socket: net.Socket | tls.TLSSocket) => void): this;
        prependOnceListener(event: "stream", listener: (stream: ClientHttp2Stream, headers: IncomingHttpHeaders, flags: number) => void): this;
    }

    export interface ServerHttp2Session extends Http2Session {
        readonly server: Http2Server | Http2SecureServer;

        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "connect", listener: (session: ServerHttp2Session, socket: net.Socket | tls.TLSSocket) => void): this;
        addListener(event: "stream", listener: (stream: ServerHttp2Stream, headers: IncomingHttpHeaders, flags: number) => void): this;

        emit(event: string | symbol, ...args: any[]): boolean;
        emit(event: "connect", session: ServerHttp2Session, socket: net.Socket | tls.TLSSocket): boolean;
        emit(event: "stream", stream: ServerHttp2Stream, headers: IncomingHttpHeaders, flags: number): boolean;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "connect", listener: (session: ServerHttp2Session, socket: net.Socket | tls.TLSSocket) => void): this;
        on(event: "stream", listener: (stream: ServerHttp2Stream, headers: IncomingHttpHeaders, flags: number) => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "connect", listener: (session: ServerHttp2Session, socket: net.Socket | tls.TLSSocket) => void): this;
        once(event: "stream", listener: (stream: ServerHttp2Stream, headers: IncomingHttpHeaders, flags: number) => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "connect", listener: (session: ServerHttp2Session, socket: net.Socket | tls.TLSSocket) => void): this;
        prependListener(event: "stream", listener: (stream: ServerHttp2Stream, headers: IncomingHttpHeaders, flags: number) => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "connect", listener: (session: ServerHttp2Session, socket: net.Socket | tls.TLSSocket) => void): this;
        prependOnceListener(event: "stream", listener: (stream: ServerHttp2Stream, headers: IncomingHttpHeaders, flags: number) => void): this;
    }

    // Http2Server

    export interface SessionOptions {
        maxDeflateDynamicTableSize?: number;
        maxReservedRemoteStreams?: number;
        maxSendHeaderBlockLength?: number;
        paddingStrategy?: number;
        peerMaxConcurrentStreams?: number;
        selectPadding?: (frameLen: number, maxFrameLen: number) => number;
        settings?: Settings;
    }

    export type ClientSessionOptions = SessionOptions;
    export type ServerSessionOptions = SessionOptions;

    export interface SecureClientSessionOptions extends ClientSessionOptions, tls.ConnectionOptions { }
    export interface SecureServerSessionOptions extends ServerSessionOptions, tls.TlsOptions { }

    export interface ServerOptions extends ServerSessionOptions {
        allowHTTP1?: boolean;
    }

    export interface SecureServerOptions extends SecureServerSessionOptions {
        allowHTTP1?: boolean;
    }

    export interface Http2Server extends net.Server {
        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "request", listener: (request: Http2ServerRequest, response: Http2ServerResponse) => void): this;
        addListener(event: "sessionError", listener: (err: Error) => void): this;
        addListener(event: "socketError", listener: (err: Error) => void): this;
        addListener(event: "stream", listener: (stream: ServerHttp2Stream, headers: IncomingHttpHeaders, flags: number) => void): this;
        addListener(event: "timeout", listener: () => void): this;

        emit(event: string | symbol, ...args: any[]): boolean;
        emit(event: "request", request: Http2ServerRequest, response: Http2ServerResponse): boolean;
        emit(event: "sessionError", err: Error): boolean;
        emit(event: "socketError", err: Error): boolean;
        emit(event: "stream", stream: ServerHttp2Stream, headers: IncomingHttpHeaders, flags: number): boolean;
        emit(event: "timeout"): boolean;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "request", listener: (request: Http2ServerRequest, response: Http2ServerResponse) => void): this;
        on(event: "sessionError", listener: (err: Error) => void): this;
        on(event: "socketError", listener: (err: Error) => void): this;
        on(event: "stream", listener: (stream: ServerHttp2Stream, headers: IncomingHttpHeaders, flags: number) => void): this;
        on(event: "timeout", listener: () => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "request", listener: (request: Http2ServerRequest, response: Http2ServerResponse) => void): this;
        once(event: "sessionError", listener: (err: Error) => void): this;
        once(event: "socketError", listener: (err: Error) => void): this;
        once(event: "stream", listener: (stream: ServerHttp2Stream, headers: IncomingHttpHeaders, flags: number) => void): this;
        once(event: "timeout", listener: () => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "request", listener: (request: Http2ServerRequest, response: Http2ServerResponse) => void): this;
        prependListener(event: "sessionError", listener: (err: Error) => void): this;
        prependListener(event: "socketError", listener: (err: Error) => void): this;
        prependListener(event: "stream", listener: (stream: ServerHttp2Stream, headers: IncomingHttpHeaders, flags: number) => void): this;
        prependListener(event: "timeout", listener: () => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "request", listener: (request: Http2ServerRequest, response: Http2ServerResponse) => void): this;
        prependOnceListener(event: "sessionError", listener: (err: Error) => void): this;
        prependOnceListener(event: "socketError", listener: (err: Error) => void): this;
        prependOnceListener(event: "stream", listener: (stream: ServerHttp2Stream, headers: IncomingHttpHeaders, flags: number) => void): this;
        prependOnceListener(event: "timeout", listener: () => void): this;
    }

    export interface Http2SecureServer extends tls.Server {
        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "request", listener: (request: Http2ServerRequest, response: Http2ServerResponse) => void): this;
        addListener(event: "sessionError", listener: (err: Error) => void): this;
        addListener(event: "socketError", listener: (err: Error) => void): this;
        addListener(event: "stream", listener: (stream: ServerHttp2Stream, headers: IncomingHttpHeaders, flags: number) => void): this;
        addListener(event: "timeout", listener: () => void): this;
        addListener(event: "unknownProtocol", listener: (socket: tls.TLSSocket) => void): this;

        emit(event: string | symbol, ...args: any[]): boolean;
        emit(event: "request", request: Http2ServerRequest, response: Http2ServerResponse): boolean;
        emit(event: "sessionError", err: Error): boolean;
        emit(event: "socketError", err: Error): boolean;
        emit(event: "stream", stream: ServerHttp2Stream, headers: IncomingHttpHeaders, flags: number): boolean;
        emit(event: "timeout"): boolean;
        emit(event: "unknownProtocol", socket: tls.TLSSocket): boolean;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "request", listener: (request: Http2ServerRequest, response: Http2ServerResponse) => void): this;
        on(event: "sessionError", listener: (err: Error) => void): this;
        on(event: "socketError", listener: (err: Error) => void): this;
        on(event: "stream", listener: (stream: ServerHttp2Stream, headers: IncomingHttpHeaders, flags: number) => void): this;
        on(event: "timeout", listener: () => void): this;
        on(event: "unknownProtocol", listener: (socket: tls.TLSSocket) => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "request", listener: (request: Http2ServerRequest, response: Http2ServerResponse) => void): this;
        once(event: "sessionError", listener: (err: Error) => void): this;
        once(event: "socketError", listener: (err: Error) => void): this;
        once(event: "stream", listener: (stream: ServerHttp2Stream, headers: IncomingHttpHeaders, flags: number) => void): this;
        once(event: "timeout", listener: () => void): this;
        once(event: "unknownProtocol", listener: (socket: tls.TLSSocket) => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "request", listener: (request: Http2ServerRequest, response: Http2ServerResponse) => void): this;
        prependListener(event: "sessionError", listener: (err: Error) => void): this;
        prependListener(event: "socketError", listener: (err: Error) => void): this;
        prependListener(event: "stream", listener: (stream: ServerHttp2Stream, headers: IncomingHttpHeaders, flags: number) => void): this;
        prependListener(event: "timeout", listener: () => void): this;
        prependListener(event: "unknownProtocol", listener: (socket: tls.TLSSocket) => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "request", listener: (request: Http2ServerRequest, response: Http2ServerResponse) => void): this;
        prependOnceListener(event: "sessionError", listener: (err: Error) => void): this;
        prependOnceListener(event: "socketError", listener: (err: Error) => void): this;
        prependOnceListener(event: "stream", listener: (stream: ServerHttp2Stream, headers: IncomingHttpHeaders, flags: number) => void): this;
        prependOnceListener(event: "timeout", listener: () => void): this;
        prependOnceListener(event: "unknownProtocol", listener: (socket: tls.TLSSocket) => void): this;
    }

    export interface Http2ServerRequest extends stream.Readable {
        headers: IncomingHttpHeaders;
        httpVersion: string;
        method: string;
        rawHeaders: string[];
        rawTrailers: string[];
        setTimeout(msecs: number, callback?: () => void): void;
        socket: net.Socket | tls.TLSSocket;
        stream: ServerHttp2Stream;
        trailers: IncomingHttpHeaders;
        url: string;

        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "aborted", listener: (hadError: boolean, code: number) => void): this;

        emit(event: string | symbol, ...args: any[]): boolean;
        emit(event: "aborted", hadError: boolean, code: number): boolean;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "aborted", listener: (hadError: boolean, code: number) => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "aborted", listener: (hadError: boolean, code: number) => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "aborted", listener: (hadError: boolean, code: number) => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "aborted", listener: (hadError: boolean, code: number) => void): this;
    }

    export interface Http2ServerResponse extends events.EventEmitter {
        addTrailers(trailers: OutgoingHttpHeaders): void;
        connection: net.Socket | tls.TLSSocket;
        end(callback?: () => void): void;
        end(data?: string | Buffer, callback?: () => void): void;
        end(data?: string | Buffer, encoding?: string, callback?: () => void): void;
        readonly finished: boolean;
        getHeader(name: string): string;
        getHeaderNames(): string[];
        getHeaders(): OutgoingHttpHeaders;
        hasHeader(name: string): boolean;
        readonly headersSent: boolean;
        removeHeader(name: string): void;
        sendDate: boolean;
        setHeader(name: string, value: number | string | string[]): void;
        setTimeout(msecs: number, callback?: () => void): void;
        socket: net.Socket | tls.TLSSocket;
        statusCode: number;
        statusMessage: '';
        stream: ServerHttp2Stream;
        write(chunk: string | Buffer, callback?: (err: Error) => void): boolean;
        write(chunk: string | Buffer, encoding?: string, callback?: (err: Error) => void): boolean;
        writeContinue(): void;
        writeHead(statusCode: number, headers?: OutgoingHttpHeaders): void;
        writeHead(statusCode: number, statusMessage?: string, headers?: OutgoingHttpHeaders): void;
        createPushResponse(headers: OutgoingHttpHeaders, callback: (err: Error | null, res: Http2ServerResponse) => void): void;

        addListener(event: string, listener: (...args: any[]) => void): this;
        addListener(event: "aborted", listener: (hadError: boolean, code: number) => void): this;
        addListener(event: "close", listener: () => void): this;
        addListener(event: "drain", listener: () => void): this;
        addListener(event: "error", listener: (error: Error) => void): this;
        addListener(event: "finish", listener: () => void): this;

        emit(event: string | symbol, ...args: any[]): boolean;
        emit(event: "aborted", hadError: boolean, code: number): boolean;
        emit(event: "close"): boolean;
        emit(event: "drain"): boolean;
        emit(event: "error", error: Error): boolean;
        emit(event: "finish"): boolean;

        on(event: string, listener: (...args: any[]) => void): this;
        on(event: "aborted", listener: (hadError: boolean, code: number) => void): this;
        on(event: "close", listener: () => void): this;
        on(event: "drain", listener: () => void): this;
        on(event: "error", listener: (error: Error) => void): this;
        on(event: "finish", listener: () => void): this;

        once(event: string, listener: (...args: any[]) => void): this;
        once(event: "aborted", listener: (hadError: boolean, code: number) => void): this;
        once(event: "close", listener: () => void): this;
        once(event: "drain", listener: () => void): this;
        once(event: "error", listener: (error: Error) => void): this;
        once(event: "finish", listener: () => void): this;

        prependListener(event: string, listener: (...args: any[]) => void): this;
        prependListener(event: "aborted", listener: (hadError: boolean, code: number) => void): this;
        prependListener(event: "close", listener: () => void): this;
        prependListener(event: "drain", listener: () => void): this;
        prependListener(event: "error", listener: (error: Error) => void): this;
        prependListener(event: "finish", listener: () => void): this;

        prependOnceListener(event: string, listener: (...args: any[]) => void): this;
        prependOnceListener(event: "aborted", listener: (hadError: boolean, code: number) => void): this;
        prependOnceListener(event: "close", listener: () => void): this;
        prependOnceListener(event: "drain", listener: () => void): this;
        prependOnceListener(event: "error", listener: (error: Error) => void): this;
        prependOnceListener(event: "finish", listener: () => void): this;
    }

    // Public API

    export namespace constants {
        export const NGHTTP2_SESSION_SERVER: number;
        export const NGHTTP2_SESSION_CLIENT: number;
        export const NGHTTP2_STREAM_STATE_IDLE: number;
        export const NGHTTP2_STREAM_STATE_OPEN: number;
        export const NGHTTP2_STREAM_STATE_RESERVED_LOCAL: number;
        export const NGHTTP2_STREAM_STATE_RESERVED_REMOTE: number;
        export const NGHTTP2_STREAM_STATE_HALF_CLOSED_LOCAL: number;
        export const NGHTTP2_STREAM_STATE_HALF_CLOSED_REMOTE: number;
        export const NGHTTP2_STREAM_STATE_CLOSED: number;
        export const NGHTTP2_NO_ERROR: number;
        export const NGHTTP2_PROTOCOL_ERROR: number;
        export const NGHTTP2_INTERNAL_ERROR: number;
        export const NGHTTP2_FLOW_CONTROL_ERROR: number;
        export const NGHTTP2_SETTINGS_TIMEOUT: number;
        export const NGHTTP2_STREAM_CLOSED: number;
        export const NGHTTP2_FRAME_SIZE_ERROR: number;
        export const NGHTTP2_REFUSED_STREAM: number;
        export const NGHTTP2_CANCEL: number;
        export const NGHTTP2_COMPRESSION_ERROR: number;
        export const NGHTTP2_CONNECT_ERROR: number;
        export const NGHTTP2_ENHANCE_YOUR_CALM: number;
        export const NGHTTP2_INADEQUATE_SECURITY: number;
        export const NGHTTP2_HTTP_1_1_REQUIRED: number;
        export const NGHTTP2_ERR_FRAME_SIZE_ERROR: number;
        export const NGHTTP2_FLAG_NONE: number;
        export const NGHTTP2_FLAG_END_STREAM: number;
        export const NGHTTP2_FLAG_END_HEADERS: number;
        export const NGHTTP2_FLAG_ACK: number;
        export const NGHTTP2_FLAG_PADDED: number;
        export const NGHTTP2_FLAG_PRIORITY: number;
        export const DEFAULT_SETTINGS_HEADER_TABLE_SIZE: number;
        export const DEFAULT_SETTINGS_ENABLE_PUSH: number;
        export const DEFAULT_SETTINGS_INITIAL_WINDOW_SIZE: number;
        export const DEFAULT_SETTINGS_MAX_FRAME_SIZE: number;
        export const MAX_MAX_FRAME_SIZE: number;
        export const MIN_MAX_FRAME_SIZE: number;
        export const MAX_INITIAL_WINDOW_SIZE: number;
        export const NGHTTP2_DEFAULT_WEIGHT: number;
        export const NGHTTP2_SETTINGS_HEADER_TABLE_SIZE: number;
        export const NGHTTP2_SETTINGS_ENABLE_PUSH: number;
        export const NGHTTP2_SETTINGS_MAX_CONCURRENT_STREAMS: number;
        export const NGHTTP2_SETTINGS_INITIAL_WINDOW_SIZE: number;
        export const NGHTTP2_SETTINGS_MAX_FRAME_SIZE: number;
        export const NGHTTP2_SETTINGS_MAX_HEADER_LIST_SIZE: number;
        export const PADDING_STRATEGY_NONE: number;
        export const PADDING_STRATEGY_MAX: number;
        export const PADDING_STRATEGY_CALLBACK: number;
        export const HTTP2_HEADER_STATUS: string;
        export const HTTP2_HEADER_METHOD: string;
        export const HTTP2_HEADER_AUTHORITY: string;
        export const HTTP2_HEADER_SCHEME: string;
        export const HTTP2_HEADER_PATH: string;
        export const HTTP2_HEADER_ACCEPT_CHARSET: string;
        export const HTTP2_HEADER_ACCEPT_ENCODING: string;
        export const HTTP2_HEADER_ACCEPT_LANGUAGE: string;
        export const HTTP2_HEADER_ACCEPT_RANGES: string;
        export const HTTP2_HEADER_ACCEPT: string;
        export const HTTP2_HEADER_ACCESS_CONTROL_ALLOW_ORIGIN: string;
        export const HTTP2_HEADER_AGE: string;
        export const HTTP2_HEADER_ALLOW: string;
        export const HTTP2_HEADER_AUTHORIZATION: string;
        export const HTTP2_HEADER_CACHE_CONTROL: string;
        export const HTTP2_HEADER_CONNECTION: string;
        export const HTTP2_HEADER_CONTENT_DISPOSITION: string;
        export const HTTP2_HEADER_CONTENT_ENCODING: string;
        export const HTTP2_HEADER_CONTENT_LANGUAGE: string;
        export const HTTP2_HEADER_CONTENT_LENGTH: string;
        export const HTTP2_HEADER_CONTENT_LOCATION: string;
        export const HTTP2_HEADER_CONTENT_MD5: string;
        export const HTTP2_HEADER_CONTENT_RANGE: string;
        export const HTTP2_HEADER_CONTENT_TYPE: string;
        export const HTTP2_HEADER_COOKIE: string;
        export const HTTP2_HEADER_DATE: string;
        export const HTTP2_HEADER_ETAG: string;
        export const HTTP2_HEADER_EXPECT: string;
        export const HTTP2_HEADER_EXPIRES: string;
        export const HTTP2_HEADER_FROM: string;
        export const HTTP2_HEADER_HOST: string;
        export const HTTP2_HEADER_IF_MATCH: string;
        export const HTTP2_HEADER_IF_MODIFIED_SINCE: string;
        export const HTTP2_HEADER_IF_NONE_MATCH: string;
        export const HTTP2_HEADER_IF_RANGE: string;
        export const HTTP2_HEADER_IF_UNMODIFIED_SINCE: string;
        export const HTTP2_HEADER_LAST_MODIFIED: string;
        export const HTTP2_HEADER_LINK: string;
        export const HTTP2_HEADER_LOCATION: string;
        export const HTTP2_HEADER_MAX_FORWARDS: string;
        export const HTTP2_HEADER_PREFER: string;
        export const HTTP2_HEADER_PROXY_AUTHENTICATE: string;
        export const HTTP2_HEADER_PROXY_AUTHORIZATION: string;
        export const HTTP2_HEADER_RANGE: string;
        export const HTTP2_HEADER_REFERER: string;
        export const HTTP2_HEADER_REFRESH: string;
        export const HTTP2_HEADER_RETRY_AFTER: string;
        export const HTTP2_HEADER_SERVER: string;
        export const HTTP2_HEADER_SET_COOKIE: string;
        export const HTTP2_HEADER_STRICT_TRANSPORT_SECURITY: string;
        export const HTTP2_HEADER_TRANSFER_ENCODING: string;
        export const HTTP2_HEADER_TE: string;
        export const HTTP2_HEADER_UPGRADE: string;
        export const HTTP2_HEADER_USER_AGENT: string;
        export const HTTP2_HEADER_VARY: string;
        export const HTTP2_HEADER_VIA: string;
        export const HTTP2_HEADER_WWW_AUTHENTICATE: string;
        export const HTTP2_HEADER_HTTP2_SETTINGS: string;
        export const HTTP2_HEADER_KEEP_ALIVE: string;
        export const HTTP2_HEADER_PROXY_CONNECTION: string;
        export const HTTP2_METHOD_ACL: string;
        export const HTTP2_METHOD_BASELINE_CONTROL: string;
        export const HTTP2_METHOD_BIND: string;
        export const HTTP2_METHOD_CHECKIN: string;
        export const HTTP2_METHOD_CHECKOUT: string;
        export const HTTP2_METHOD_CONNECT: string;
        export const HTTP2_METHOD_COPY: string;
        export const HTTP2_METHOD_DELETE: string;
        export const HTTP2_METHOD_GET: string;
        export const HTTP2_METHOD_HEAD: string;
        export const HTTP2_METHOD_LABEL: string;
        export const HTTP2_METHOD_LINK: string;
        export const HTTP2_METHOD_LOCK: string;
        export const HTTP2_METHOD_MERGE: string;
        export const HTTP2_METHOD_MKACTIVITY: string;
        export const HTTP2_METHOD_MKCALENDAR: string;
        export const HTTP2_METHOD_MKCOL: string;
        export const HTTP2_METHOD_MKREDIRECTREF: string;
        export const HTTP2_METHOD_MKWORKSPACE: string;
        export const HTTP2_METHOD_MOVE: string;
        export const HTTP2_METHOD_OPTIONS: string;
        export const HTTP2_METHOD_ORDERPATCH: string;
        export const HTTP2_METHOD_PATCH: string;
        export const HTTP2_METHOD_POST: string;
        export const HTTP2_METHOD_PRI: string;
        export const HTTP2_METHOD_PROPFIND: string;
        export const HTTP2_METHOD_PROPPATCH: string;
        export const HTTP2_METHOD_PUT: string;
        export const HTTP2_METHOD_REBIND: string;
        export const HTTP2_METHOD_REPORT: string;
        export const HTTP2_METHOD_SEARCH: string;
        export const HTTP2_METHOD_TRACE: string;
        export const HTTP2_METHOD_UNBIND: string;
        export const HTTP2_METHOD_UNCHECKOUT: string;
        export const HTTP2_METHOD_UNLINK: string;
        export const HTTP2_METHOD_UNLOCK: string;
        export const HTTP2_METHOD_UPDATE: string;
        export const HTTP2_METHOD_UPDATEREDIRECTREF: string;
        export const HTTP2_METHOD_VERSION_CONTROL: string;
        export const HTTP_STATUS_CONTINUE: number;
        export const HTTP_STATUS_SWITCHING_PROTOCOLS: number;
        export const HTTP_STATUS_PROCESSING: number;
        export const HTTP_STATUS_OK: number;
        export const HTTP_STATUS_CREATED: number;
        export const HTTP_STATUS_ACCEPTED: number;
        export const HTTP_STATUS_NON_AUTHORITATIVE_INFORMATION: number;
        export const HTTP_STATUS_NO_CONTENT: number;
        export const HTTP_STATUS_RESET_CONTENT: number;
        export const HTTP_STATUS_PARTIAL_CONTENT: number;
        export const HTTP_STATUS_MULTI_STATUS: number;
        export const HTTP_STATUS_ALREADY_REPORTED: number;
        export const HTTP_STATUS_IM_USED: number;
        export const HTTP_STATUS_MULTIPLE_CHOICES: number;
        export const HTTP_STATUS_MOVED_PERMANENTLY: number;
        export const HTTP_STATUS_FOUND: number;
        export const HTTP_STATUS_SEE_OTHER: number;
        export const HTTP_STATUS_NOT_MODIFIED: number;
        export const HTTP_STATUS_USE_PROXY: number;
        export const HTTP_STATUS_TEMPORARY_REDIRECT: number;
        export const HTTP_STATUS_PERMANENT_REDIRECT: number;
        export const HTTP_STATUS_BAD_REQUEST: number;
        export const HTTP_STATUS_UNAUTHORIZED: number;
        export const HTTP_STATUS_PAYMENT_REQUIRED: number;
        export const HTTP_STATUS_FORBIDDEN: number;
        export const HTTP_STATUS_NOT_FOUND: number;
        export const HTTP_STATUS_METHOD_NOT_ALLOWED: number;
        export const HTTP_STATUS_NOT_ACCEPTABLE: number;
        export const HTTP_STATUS_PROXY_AUTHENTICATION_REQUIRED: number;
        export const HTTP_STATUS_REQUEST_TIMEOUT: number;
        export const HTTP_STATUS_CONFLICT: number;
        export const HTTP_STATUS_GONE: number;
        export const HTTP_STATUS_LENGTH_REQUIRED: number;
        export const HTTP_STATUS_PRECONDITION_FAILED: number;
        export const HTTP_STATUS_PAYLOAD_TOO_LARGE: number;
        export const HTTP_STATUS_URI_TOO_LONG: number;
        export const HTTP_STATUS_UNSUPPORTED_MEDIA_TYPE: number;
        export const HTTP_STATUS_RANGE_NOT_SATISFIABLE: number;
        export const HTTP_STATUS_EXPECTATION_FAILED: number;
        export const HTTP_STATUS_TEAPOT: number;
        export const HTTP_STATUS_MISDIRECTED_REQUEST: number;
        export const HTTP_STATUS_UNPROCESSABLE_ENTITY: number;
        export const HTTP_STATUS_LOCKED: number;
        export const HTTP_STATUS_FAILED_DEPENDENCY: number;
        export const HTTP_STATUS_UNORDERED_COLLECTION: number;
        export const HTTP_STATUS_UPGRADE_REQUIRED: number;
        export const HTTP_STATUS_PRECONDITION_REQUIRED: number;
        export const HTTP_STATUS_TOO_MANY_REQUESTS: number;
        export const HTTP_STATUS_REQUEST_HEADER_FIELDS_TOO_LARGE: number;
        export const HTTP_STATUS_UNAVAILABLE_FOR_LEGAL_REASONS: number;
        export const HTTP_STATUS_INTERNAL_SERVER_ERROR: number;
        export const HTTP_STATUS_NOT_IMPLEMENTED: number;
        export const HTTP_STATUS_BAD_GATEWAY: number;
        export const HTTP_STATUS_SERVICE_UNAVAILABLE: number;
        export const HTTP_STATUS_GATEWAY_TIMEOUT: number;
        export const HTTP_STATUS_HTTP_VERSION_NOT_SUPPORTED: number;
        export const HTTP_STATUS_VARIANT_ALSO_NEGOTIATES: number;
        export const HTTP_STATUS_INSUFFICIENT_STORAGE: number;
        export const HTTP_STATUS_LOOP_DETECTED: number;
        export const HTTP_STATUS_BANDWIDTH_LIMIT_EXCEEDED: number;
        export const HTTP_STATUS_NOT_EXTENDED: number;
        export const HTTP_STATUS_NETWORK_AUTHENTICATION_REQUIRED: number;
    }

    export function getDefaultSettings(): Settings;
    export function getPackedSettings(settings: Settings): Settings;
    export function getUnpackedSettings(buf: Buffer | Uint8Array): Settings;

    export function createServer(onRequestHandler?: (request: Http2ServerRequest, response: Http2ServerResponse) => void): Http2Server;
    export function createServer(options: ServerOptions, onRequestHandler?: (request: Http2ServerRequest, response: Http2ServerResponse) => void): Http2Server;

    export function createSecureServer(onRequestHandler?: (request: Http2ServerRequest, response: Http2ServerResponse) => void): Http2SecureServer;
    export function createSecureServer(options: SecureServerOptions, onRequestHandler?: (request: Http2ServerRequest, response: Http2ServerResponse) => void): Http2SecureServer;

    export function connect(authority: string | url.URL, listener?: (session: ClientHttp2Session, socket: net.Socket | tls.TLSSocket) => void): ClientHttp2Session;
    export function connect(authority: string | url.URL, options?: ClientSessionOptions | SecureClientSessionOptions, listener?: (session: ClientHttp2Session, socket: net.Socket | tls.TLSSocket) => void): ClientHttp2Session;
}

declare module "perf_hooks" {
    export interface PerformanceEntry {
        /**
         * The total number of milliseconds elapsed for this entry.
         * This value will not be meaningful for all Performance Entry types.
         */
        readonly duration: number;

        /**
         * The name of the performance entry.
         */
        readonly name: string;

        /**
         * The high resolution millisecond timestamp marking the starting time of the Performance Entry.
         */
        readonly startTime: number;

        /**
         * The type of the performance entry.
         * Currently it may be one of: 'node', 'mark', 'measure', 'gc', or 'function'.
         */
        readonly entryType: string;

        /**
         * When performanceEntry.entryType is equal to 'gc', the performance.kind property identifies
         * the type of garbage collection operation that occurred.
         * The value may be one of perf_hooks.constants.
         */
        readonly kind?: number;
    }

    export interface PerformanceNodeTiming extends PerformanceEntry {
        /**
         * The high resolution millisecond timestamp at which the Node.js process completed bootstrap.
         */
        readonly bootstrapComplete: number;

        /**
         * The high resolution millisecond timestamp at which cluster processing ended.
         */
        readonly clusterSetupEnd: number;

        /**
         * The high resolution millisecond timestamp at which cluster processing started.
         */
        readonly clusterSetupStart: number;

        /**
         * The high resolution millisecond timestamp at which the Node.js event loop exited.
         */
        readonly loopExit: number;

        /**
         * The high resolution millisecond timestamp at which the Node.js event loop started.
         */
        readonly loopStart: number;

        /**
         * The high resolution millisecond timestamp at which main module load ended.
         */
        readonly moduleLoadEnd: number;

        /**
         * The high resolution millisecond timestamp at which main module load started.
         */
        readonly moduleLoadStart: number;

        /**
         * The high resolution millisecond timestamp at which the Node.js process was initialized.
         */
        readonly nodeStart: number;

        /**
         * The high resolution millisecond timestamp at which preload module load ended.
         */
        readonly preloadModuleLoadEnd: number;

        /**
         * The high resolution millisecond timestamp at which preload module load started.
         */
        readonly preloadModuleLoadStart: number;

        /**
         * The high resolution millisecond timestamp at which third_party_main processing ended.
         */
        readonly thirdPartyMainEnd: number;

        /**
         * The high resolution millisecond timestamp at which third_party_main processing started.
         */
        readonly thirdPartyMainStart: number;

        /**
         * The high resolution millisecond timestamp at which the V8 platform was initialized.
         */
        readonly v8Start: number;
    }

    export interface Performance {
        /**
         * If name is not provided, removes all PerformanceFunction objects from the Performance Timeline.
         * If name is provided, removes entries with name.
         * @param name
         */
        clearFunctions(name?: string): void;

        /**
         * If name is not provided, removes all PerformanceMark objects from the Performance Timeline.
         * If name is provided, removes only the named mark.
         * @param name
         */
        clearMarks(name?: string): void;

        /**
         * If name is not provided, removes all PerformanceMeasure objects from the Performance Timeline.
         * If name is provided, removes only objects whose performanceEntry.name matches name.
         */
        clearMeasures(name?: string): void;

        /**
         * Returns a list of all PerformanceEntry objects in chronological order with respect to performanceEntry.startTime.
         * @return list of all PerformanceEntry objects
         */
        getEntries(): PerformanceEntry[];

        /**
         * Returns a list of all PerformanceEntry objects in chronological order with respect to performanceEntry.startTime
         * whose performanceEntry.name is equal to name, and optionally, whose performanceEntry.entryType is equal to type.
         * @param name
         * @param type
         * @return list of all PerformanceEntry objects
         */
        getEntriesByName(name: string, type?: string): PerformanceEntry[];

        /**
         * Returns a list of all PerformanceEntry objects in chronological order with respect to performanceEntry.startTime
         * whose performanceEntry.entryType is equal to type.
         * @param type
         * @return list of all PerformanceEntry objects
         */
        getEntriesByType(type: string): PerformanceEntry[];

        /**
         * Creates a new PerformanceMark entry in the Performance Timeline.
         * A PerformanceMark is a subclass of PerformanceEntry whose performanceEntry.entryType is always 'mark',
         * and whose performanceEntry.duration is always 0.
         * Performance marks are used to mark specific significant moments in the Performance Timeline.
         * @param name
         */
        mark(name?: string): void;

        /**
         * Creates a new PerformanceMeasure entry in the Performance Timeline.
         * A PerformanceMeasure is a subclass of PerformanceEntry whose performanceEntry.entryType is always 'measure',
         * and whose performanceEntry.duration measures the number of milliseconds elapsed since startMark and endMark.
         *
         * The startMark argument may identify any existing PerformanceMark in the the Performance Timeline, or may identify
         * any of the timestamp properties provided by the PerformanceNodeTiming class. If the named startMark does not exist,
         * then startMark is set to timeOrigin by default.
         *
         * The endMark argument must identify any existing PerformanceMark in the the Performance Timeline or any of the timestamp
         * properties provided by the PerformanceNodeTiming class. If the named endMark does not exist, an error will be thrown.
         * @param name
         * @param startMark
         * @param endMark
         */
        measure(name: string, startMark: string, endMark: string): void;

        /**
         * An instance of the PerformanceNodeTiming class that provides performance metrics for specific Node.js operational milestones.
         */
        readonly nodeTiming: PerformanceNodeTiming;

        /**
         * @return the current high resolution millisecond timestamp
         */
        now(): number;

        /**
         * The timeOrigin specifies the high resolution millisecond timestamp from which all performance metric durations are measured.
         */
        readonly timeOrigin: number;

        /**
         * Wraps a function within a new function that measures the running time of the wrapped function.
         * A PerformanceObserver must be subscribed to the 'function' event type in order for the timing details to be accessed.
         * @param fn
         */
        timerify<T extends (...optionalParams: any[]) => any>(fn: T): T;
    }

    export interface PerformanceObserverEntryList {
        /**
         * @return a list of PerformanceEntry objects in chronological order with respect to performanceEntry.startTime.
         */
        getEntries(): PerformanceEntry[];

        /**
         * @return a list of PerformanceEntry objects in chronological order with respect to performanceEntry.startTime
         * whose performanceEntry.name is equal to name, and optionally, whose performanceEntry.entryType is equal to type.
         */
        getEntriesByName(name: string, type?: string): PerformanceEntry[];

        /**
         * @return Returns a list of PerformanceEntry objects in chronological order with respect to performanceEntry.startTime
         * whose performanceEntry.entryType is equal to type.
         */
        getEntriesByType(type: string): PerformanceEntry[];
    }

    export type PerformanceObserverCallback = (list: PerformanceObserverEntryList, observer: PerformanceObserver) => void;

    export class PerformanceObserver {
        constructor(callback: PerformanceObserverCallback);

        /**
         * Disconnects the PerformanceObserver instance from all notifications.
         */
        disconnect(): void;

        /**
         * Subscribes the PerformanceObserver instance to notifications of new PerformanceEntry instances identified by options.entryTypes.
         * When options.buffered is false, the callback will be invoked once for every PerformanceEntry instance.
         * Property buffered defaults to false.
         * @param options
         */
        observe(options: { entryTypes: string[], buffered?: boolean }): void;
    }

    export namespace constants {
        export const NODE_PERFORMANCE_GC_MAJOR: number;
        export const NODE_PERFORMANCE_GC_MINOR: number;
        export const NODE_PERFORMANCE_GC_INCREMENTAL: number;
        export const NODE_PERFORMANCE_GC_WEAKCB: number;
    }

    const performance: Performance;
}
"""
let [<Literal>] monaco =
  """/*!-----------------------------------------------------------
 * Copyright (c) Microsoft Corporation. All rights reserved.
 * Type definitions for monaco-editor v0.10.1
 * Released under the MIT license
*-----------------------------------------------------------*/
/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Microsoft Corporation. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

declare module monaco {

    interface Thenable<T> {
        /**
         * Attaches callbacks for the resolution and/or rejection of the Promise.
         * @param onfulfilled The callback to execute when the Promise is resolved.
         * @param onrejected The callback to execute when the Promise is rejected.
         * @returns A Promise for the completion of which ever callback is executed.
         */
        then<TResult>(onfulfilled?: (value: T) => TResult | Thenable<TResult>, onrejected?: (reason: any) => TResult | Thenable<TResult>): Thenable<TResult>;
        then<TResult>(onfulfilled?: (value: T) => TResult | Thenable<TResult>, onrejected?: (reason: any) => void): Thenable<TResult>;
    }

    export interface IDisposable {
        dispose(): void;
    }

    export interface IEvent<T> {
        (listener: (e: T) => any, thisArg?: any): IDisposable;
    }

    /**
     * A helper that allows to emit and listen to typed events
     */
    export class Emitter<T> {
        constructor();
        readonly event: IEvent<T>;
        fire(event?: T): void;
        dispose(): void;
    }

    export enum Severity {
        Ignore = 0,
        Info = 1,
        Warning = 2,
        Error = 3,
    }



    /**
     * The value callback to complete a promise
     */
    export interface TValueCallback<T> {
        (value: T | Thenable<T>): void;
    }


    export interface ProgressCallback {
        (progress: any): any;
    }


    /**
     * A Promise implementation that supports progress and cancelation.
     */
    export class Promise<V> {

        constructor(init: (complete: TValueCallback<V>, error: (err: any) => void, progress: ProgressCallback) => void, oncancel?: any);

        public then<U>(success?: (value: V) => Promise<U>, error?: (err: any) => Promise<U>, progress?: ProgressCallback): Promise<U>;
        public then<U>(success?: (value: V) => Promise<U>, error?: (err: any) => Promise<U> | U, progress?: ProgressCallback): Promise<U>;
        public then<U>(success?: (value: V) => Promise<U>, error?: (err: any) => U, progress?: ProgressCallback): Promise<U>;
        public then<U>(success?: (value: V) => Promise<U>, error?: (err: any) => void, progress?: ProgressCallback): Promise<U>;
        public then<U>(success?: (value: V) => Promise<U> | U, error?: (err: any) => Promise<U>, progress?: ProgressCallback): Promise<U>;
        public then<U>(success?: (value: V) => Promise<U> | U, error?: (err: any) => Promise<U> | U, progress?: ProgressCallback): Promise<U>;
        public then<U>(success?: (value: V) => Promise<U> | U, error?: (err: any) => U, progress?: ProgressCallback): Promise<U>;
        public then<U>(success?: (value: V) => Promise<U> | U, error?: (err: any) => void, progress?: ProgressCallback): Promise<U>;
        public then<U>(success?: (value: V) => U, error?: (err: any) => Promise<U>, progress?: ProgressCallback): Promise<U>;
        public then<U>(success?: (value: V) => U, error?: (err: any) => Promise<U> | U, progress?: ProgressCallback): Promise<U>;
        public then<U>(success?: (value: V) => U, error?: (err: any) => U, progress?: ProgressCallback): Promise<U>;
        public then<U>(success?: (value: V) => U, error?: (err: any) => void, progress?: ProgressCallback): Promise<U>;

        public done(success?: (value: V) => void, error?: (err: any) => any, progress?: ProgressCallback): void;
        public cancel(): void;

        public static as(value: null): Promise<null>;
        public static as(value: undefined): Promise<undefined>;
        public static as<ValueType>(value: Promise<ValueType>): Promise<ValueType>;
        public static as<ValueType>(value: Thenable<ValueType>): Thenable<ValueType>;
        public static as<ValueType>(value: ValueType): Promise<ValueType>;

        public static is(value: any): value is Thenable<any>;
        public static timeout(delay: number): Promise<void>;
        public static join<ValueType>(promises: Promise<ValueType>[]): Promise<ValueType[]>;
        public static join<ValueType>(promises: Thenable<ValueType>[]): Thenable<ValueType[]>;
        public static join<ValueType>(promises: { [n: string]: Promise<ValueType> }): Promise<{ [n: string]: ValueType }>;
        public static any<ValueType>(promises: Promise<ValueType>[]): Promise<{ key: string; value: Promise<ValueType>; }>;

        public static wrap<ValueType>(value: Thenable<ValueType>): Promise<ValueType>;
        public static wrap<ValueType>(value: ValueType): Promise<ValueType>;

        public static wrapError<ValueType>(error: Error): Promise<ValueType>;
    }

    export class CancellationTokenSource {
        readonly token: CancellationToken;
        cancel(): void;
        dispose(): void;
    }

    export interface CancellationToken {
        readonly isCancellationRequested: boolean;
        /**
         * An event emitted when cancellation is requested
         * @event
         */
        readonly onCancellationRequested: IEvent<any>;
    }
    /**
     * Uniform Resource Identifier (Uri) http://tools.ietf.org/html/rfc3986.
     * This class is a simple parser which creates the basic component paths
     * (http://tools.ietf.org/html/rfc3986#section-3) with minimal validation
     * and encoding.
     *
     *       foo://example.com:8042/over/there?name=ferret#nose
     *       \_/   \______________/\_________/ \_________/ \__/
     *        |           |            |            |        |
     *     scheme     authority       path        query   fragment
     *        |   _____________________|__
     *       / \ /                        \
     *       urn:example:animal:ferret:nose
     *
     *
     */
    export class Uri {
        static isUri(thing: any): thing is Uri;
        /**
         * scheme is the 'http' part of 'http://www.msft.com/some/path?query#fragment'.
         * The part before the first colon.
         */
        readonly scheme: string;
        /**
         * authority is the 'www.msft.com' part of 'http://www.msft.com/some/path?query#fragment'.
         * The part between the first double slashes and the next slash.
         */
        readonly authority: string;
        /**
         * path is the '/some/path' part of 'http://www.msft.com/some/path?query#fragment'.
         */
        readonly path: string;
        /**
         * query is the 'query' part of 'http://www.msft.com/some/path?query#fragment'.
         */
        readonly query: string;
        /**
         * fragment is the 'fragment' part of 'http://www.msft.com/some/path?query#fragment'.
         */
        readonly fragment: string;
        /**
         * Returns a string representing the corresponding file system path of this Uri.
         * Will handle UNC paths and normalize windows drive letters to lower-case. Also
         * uses the platform specific path separator. Will *not* validate the path for
         * invalid characters and semantics. Will *not* look at the scheme of this Uri.
         */
        readonly fsPath: string;
        with(change: {
            scheme?: string;
            authority?: string;
            path?: string;
            query?: string;
            fragment?: string;
        }): Uri;
        static parse(value: string): Uri;
        static file(path: string): Uri;
        static from(components: {
            scheme?: string;
            authority?: string;
            path?: string;
            query?: string;
            fragment?: string;
        }): Uri;
        /**
         *
         * @param skipEncoding Do not encode the result, default is `false`
         */
        toString(skipEncoding?: boolean): string;
        toJSON(): any;
        static revive(data: any): Uri;
    }

    /**
     * Virtual Key Codes, the value does not hold any inherent meaning.
     * Inspired somewhat from https://msdn.microsoft.com/en-us/library/windows/desktop/dd375731(v=vs.85).aspx
     * But these are "more general", as they should work across browsers & OS`s.
     */
    export enum KeyCode {
        /**
         * Placed first to cover the 0 value of the enum.
         */
        Unknown = 0,
        Backspace = 1,
        Tab = 2,
        Enter = 3,
        Shift = 4,
        Ctrl = 5,
        Alt = 6,
        PauseBreak = 7,
        CapsLock = 8,
        Escape = 9,
        Space = 10,
        PageUp = 11,
        PageDown = 12,
        End = 13,
        Home = 14,
        LeftArrow = 15,
        UpArrow = 16,
        RightArrow = 17,
        DownArrow = 18,
        Insert = 19,
        Delete = 20,
        KEY_0 = 21,
        KEY_1 = 22,
        KEY_2 = 23,
        KEY_3 = 24,
        KEY_4 = 25,
        KEY_5 = 26,
        KEY_6 = 27,
        KEY_7 = 28,
        KEY_8 = 29,
        KEY_9 = 30,
        KEY_A = 31,
        KEY_B = 32,
        KEY_C = 33,
        KEY_D = 34,
        KEY_E = 35,
        KEY_F = 36,
        KEY_G = 37,
        KEY_H = 38,
        KEY_I = 39,
        KEY_J = 40,
        KEY_K = 41,
        KEY_L = 42,
        KEY_M = 43,
        KEY_N = 44,
        KEY_O = 45,
        KEY_P = 46,
        KEY_Q = 47,
        KEY_R = 48,
        KEY_S = 49,
        KEY_T = 50,
        KEY_U = 51,
        KEY_V = 52,
        KEY_W = 53,
        KEY_X = 54,
        KEY_Y = 55,
        KEY_Z = 56,
        Meta = 57,
        ContextMenu = 58,
        F1 = 59,
        F2 = 60,
        F3 = 61,
        F4 = 62,
        F5 = 63,
        F6 = 64,
        F7 = 65,
        F8 = 66,
        F9 = 67,
        F10 = 68,
        F11 = 69,
        F12 = 70,
        F13 = 71,
        F14 = 72,
        F15 = 73,
        F16 = 74,
        F17 = 75,
        F18 = 76,
        F19 = 77,
        NumLock = 78,
        ScrollLock = 79,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         * For the US standard keyboard, the ';:' key
         */
        US_SEMICOLON = 80,
        /**
         * For any country/region, the '+' key
         * For the US standard keyboard, the '=+' key
         */
        US_EQUAL = 81,
        /**
         * For any country/region, the ',' key
         * For the US standard keyboard, the ',<' key
         */
        US_COMMA = 82,
        /**
         * For any country/region, the '-' key
         * For the US standard keyboard, the '-_' key
         */
        US_MINUS = 83,
        /**
         * For any country/region, the '.' key
         * For the US standard keyboard, the '.>' key
         */
        US_DOT = 84,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         * For the US standard keyboard, the '/?' key
         */
        US_SLASH = 85,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         * For the US standard keyboard, the '`~' key
         */
        US_BACKTICK = 86,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         * For the US standard keyboard, the '[{' key
         */
        US_OPEN_SQUARE_BRACKET = 87,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         * For the US standard keyboard, the '\|' key
         */
        US_BACKSLASH = 88,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         * For the US standard keyboard, the ']}' key
         */
        US_CLOSE_SQUARE_BRACKET = 89,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         * For the US standard keyboard, the ''"' key
         */
        US_QUOTE = 90,
        /**
         * Used for miscellaneous characters; it can vary by keyboard.
         */
        OEM_8 = 91,
        /**
         * Either the angle bracket key or the backslash key on the RT 102-key keyboard.
         */
        OEM_102 = 92,
        NUMPAD_0 = 93,
        NUMPAD_1 = 94,
        NUMPAD_2 = 95,
        NUMPAD_3 = 96,
        NUMPAD_4 = 97,
        NUMPAD_5 = 98,
        NUMPAD_6 = 99,
        NUMPAD_7 = 100,
        NUMPAD_8 = 101,
        NUMPAD_9 = 102,
        NUMPAD_MULTIPLY = 103,
        NUMPAD_ADD = 104,
        NUMPAD_SEPARATOR = 105,
        NUMPAD_SUBTRACT = 106,
        NUMPAD_DECIMAL = 107,
        NUMPAD_DIVIDE = 108,
        /**
         * Cover all key codes when IME is processing input.
         */
        KEY_IN_COMPOSITION = 109,
        ABNT_C1 = 110,
        ABNT_C2 = 111,
        /**
         * Placed last to cover the length of the enum.
         * Please do not depend on this value!
         */
        MAX_VALUE = 112,
    }

    export class KeyMod {
        static readonly CtrlCmd: number;
        static readonly Shift: number;
        static readonly Alt: number;
        static readonly WinCtrl: number;
        static chord(firstPart: number, secondPart: number): number;
    }
    /**
     * MarkedString can be used to render human readable text. It is either a markdown string
     * or a code-block that provides a language and a code snippet. Note that
     * markdown strings will be sanitized - that means html will be escaped.
     */
    export type MarkedString = string | {
        readonly language: string;
        readonly value: string;
    };

    export interface IKeyboardEvent {
        readonly browserEvent: KeyboardEvent;
        readonly target: HTMLElement;
        readonly ctrlKey: boolean;
        readonly shiftKey: boolean;
        readonly altKey: boolean;
        readonly metaKey: boolean;
        readonly keyCode: KeyCode;
        readonly code: string;
        equals(keybinding: number): boolean;
        preventDefault(): void;
        stopPropagation(): void;
    }
    export interface IMouseEvent {
        readonly browserEvent: MouseEvent;
        readonly leftButton: boolean;
        readonly middleButton: boolean;
        readonly rightButton: boolean;
        readonly target: HTMLElement;
        readonly detail: number;
        readonly posx: number;
        readonly posy: number;
        readonly ctrlKey: boolean;
        readonly shiftKey: boolean;
        readonly altKey: boolean;
        readonly metaKey: boolean;
        readonly timestamp: number;
        preventDefault(): void;
        stopPropagation(): void;
    }

    export interface IScrollEvent {
        readonly scrollTop: number;
        readonly scrollLeft: number;
        readonly scrollWidth: number;
        readonly scrollHeight: number;
        readonly scrollTopChanged: boolean;
        readonly scrollLeftChanged: boolean;
        readonly scrollWidthChanged: boolean;
        readonly scrollHeightChanged: boolean;
    }
    /**
     * A position in the editor. This interface is suitable for serialization.
     */
    export interface IPosition {
        /**
         * line number (starts at 1)
         */
        readonly lineNumber: number;
        /**
         * column (the first character in a line is between column 1 and column 2)
         */
        readonly column: number;
    }

    /**
     * A position in the editor.
     */
    export class Position {
        /**
         * line number (starts at 1)
         */
        readonly lineNumber: number;
        /**
         * column (the first character in a line is between column 1 and column 2)
         */
        readonly column: number;
        constructor(lineNumber: number, column: number);
        /**
         * Test if this position equals other position
         */
        equals(other: IPosition): boolean;
        /**
         * Test if position `a` equals position `b`
         */
        static equals(a: IPosition, b: IPosition): boolean;
        /**
         * Test if this position is before other position.
         * If the two positions are equal, the result will be false.
         */
        isBefore(other: IPosition): boolean;
        /**
         * Test if position `a` is before position `b`.
         * If the two positions are equal, the result will be false.
         */
        static isBefore(a: IPosition, b: IPosition): boolean;
        /**
         * Test if this position is before other position.
         * If the two positions are equal, the result will be true.
         */
        isBeforeOrEqual(other: IPosition): boolean;
        /**
         * Test if position `a` is before position `b`.
         * If the two positions are equal, the result will be true.
         */
        static isBeforeOrEqual(a: IPosition, b: IPosition): boolean;
        /**
         * A function that compares positions, useful for sorting
         */
        static compare(a: IPosition, b: IPosition): number;
        /**
         * Clone this position.
         */
        clone(): Position;
        /**
         * Convert to a human-readable representation.
         */
        toString(): string;
        /**
         * Create a `Position` from an `IPosition`.
         */
        static lift(pos: IPosition): Position;
        /**
         * Test if `obj` is an `IPosition`.
         */
        static isIPosition(obj: any): obj is IPosition;
    }

    /**
     * A range in the editor. This interface is suitable for serialization.
     */
    export interface IRange {
        /**
         * Line number on which the range starts (starts at 1).
         */
        readonly startLineNumber: number;
        /**
         * Column on which the range starts in line `startLineNumber` (starts at 1).
         */
        readonly startColumn: number;
        /**
         * Line number on which the range ends.
         */
        readonly endLineNumber: number;
        /**
         * Column on which the range ends in line `endLineNumber`.
         */
        readonly endColumn: number;
    }

    /**
     * A range in the editor. (startLineNumber,startColumn) is <= (endLineNumber,endColumn)
     */
    export class Range {
        /**
         * Line number on which the range starts (starts at 1).
         */
        readonly startLineNumber: number;
        /**
         * Column on which the range starts in line `startLineNumber` (starts at 1).
         */
        readonly startColumn: number;
        /**
         * Line number on which the range ends.
         */
        readonly endLineNumber: number;
        /**
         * Column on which the range ends in line `endLineNumber`.
         */
        readonly endColumn: number;
        constructor(startLineNumber: number, startColumn: number, endLineNumber: number, endColumn: number);
        /**
         * Test if this range is empty.
         */
        isEmpty(): boolean;
        /**
         * Test if `range` is empty.
         */
        static isEmpty(range: IRange): boolean;
        /**
         * Test if position is in this range. If the position is at the edges, will return true.
         */
        containsPosition(position: IPosition): boolean;
        /**
         * Test if `position` is in `range`. If the position is at the edges, will return true.
         */
        static containsPosition(range: IRange, position: IPosition): boolean;
        /**
         * Test if range is in this range. If the range is equal to this range, will return true.
         */
        containsRange(range: IRange): boolean;
        /**
         * Test if `otherRange` is in `range`. If the ranges are equal, will return true.
         */
        static containsRange(range: IRange, otherRange: IRange): boolean;
        /**
         * A reunion of the two ranges.
         * The smallest position will be used as the start point, and the largest one as the end point.
         */
        plusRange(range: IRange): Range;
        /**
         * A reunion of the two ranges.
         * The smallest position will be used as the start point, and the largest one as the end point.
         */
        static plusRange(a: IRange, b: IRange): Range;
        /**
         * A intersection of the two ranges.
         */
        intersectRanges(range: IRange): Range;
        /**
         * A intersection of the two ranges.
         */
        static intersectRanges(a: IRange, b: IRange): Range;
        /**
         * Test if this range equals other.
         */
        equalsRange(other: IRange): boolean;
        /**
         * Test if range `a` equals `b`.
         */
        static equalsRange(a: IRange, b: IRange): boolean;
        /**
         * Return the end position (which will be after or equal to the start position)
         */
        getEndPosition(): Position;
        /**
         * Return the start position (which will be before or equal to the end position)
         */
        getStartPosition(): Position;
        /**
         * Clone this range.
         */
        cloneRange(): Range;
        /**
         * Transform to a user presentable string representation.
         */
        toString(): string;
        /**
         * Create a new range using this range's start position, and using endLineNumber and endColumn as the end position.
         */
        setEndPosition(endLineNumber: number, endColumn: number): Range;
        /**
         * Create a new range using this range's end position, and using startLineNumber and startColumn as the start position.
         */
        setStartPosition(startLineNumber: number, startColumn: number): Range;
        /**
         * Create a new empty range using this range's start position.
         */
        collapseToStart(): Range;
        /**
         * Create a new empty range using this range's start position.
         */
        static collapseToStart(range: IRange): Range;
        static fromPositions(start: IPosition, end?: IPosition): Range;
        /**
         * Create a `Range` from an `IRange`.
         */
        static lift(range: IRange): Range;
        /**
         * Test if `obj` is an `IRange`.
         */
        static isIRange(obj: any): obj is IRange;
        /**
         * Test if the two ranges are touching in any way.
         */
        static areIntersectingOrTouching(a: IRange, b: IRange): boolean;
        /**
         * A function that compares ranges, useful for sorting ranges
         * It will first compare ranges on the startPosition and then on the endPosition
         */
        static compareRangesUsingStarts(a: IRange, b: IRange): number;
        /**
         * A function that compares ranges, useful for sorting ranges
         * It will first compare ranges on the endPosition and then on the startPosition
         */
        static compareRangesUsingEnds(a: IRange, b: IRange): number;
        /**
         * Test if the range spans multiple lines.
         */
        static spansMultipleLines(range: IRange): boolean;
    }

    /**
     * A selection in the editor.
     * The selection is a range that has an orientation.
     */
    export interface ISelection {
        /**
         * The line number on which the selection has started.
         */
        readonly selectionStartLineNumber: number;
        /**
         * The column on `selectionStartLineNumber` where the selection has started.
         */
        readonly selectionStartColumn: number;
        /**
         * The line number on which the selection has ended.
         */
        readonly positionLineNumber: number;
        /**
         * The column on `positionLineNumber` where the selection has ended.
         */
        readonly positionColumn: number;
    }

    /**
     * A selection in the editor.
     * The selection is a range that has an orientation.
     */
    export class Selection extends Range {
        /**
         * The line number on which the selection has started.
         */
        readonly selectionStartLineNumber: number;
        /**
         * The column on `selectionStartLineNumber` where the selection has started.
         */
        readonly selectionStartColumn: number;
        /**
         * The line number on which the selection has ended.
         */
        readonly positionLineNumber: number;
        /**
         * The column on `positionLineNumber` where the selection has ended.
         */
        readonly positionColumn: number;
        constructor(selectionStartLineNumber: number, selectionStartColumn: number, positionLineNumber: number, positionColumn: number);
        /**
         * Clone this selection.
         */
        clone(): Selection;
        /**
         * Transform to a human-readable representation.
         */
        toString(): string;
        /**
         * Test if equals other selection.
         */
        equalsSelection(other: ISelection): boolean;
        /**
         * Test if the two selections are equal.
         */
        static selectionsEqual(a: ISelection, b: ISelection): boolean;
        /**
         * Get directions (LTR or RTL).
         */
        getDirection(): SelectionDirection;
        /**
         * Create a new selection with a different `positionLineNumber` and `positionColumn`.
         */
        setEndPosition(endLineNumber: number, endColumn: number): Selection;
        /**
         * Get the position at `positionLineNumber` and `positionColumn`.
         */
        getPosition(): Position;
        /**
         * Create a new selection with a different `selectionStartLineNumber` and `selectionStartColumn`.
         */
        setStartPosition(startLineNumber: number, startColumn: number): Selection;
        /**
         * Create a `Selection` from one or two positions
         */
        static fromPositions(start: IPosition, end?: IPosition): Selection;
        /**
         * Create a `Selection` from an `ISelection`.
         */
        static liftSelection(sel: ISelection): Selection;
        /**
         * `a` equals `b`.
         */
        static selectionsArrEqual(a: ISelection[], b: ISelection[]): boolean;
        /**
         * Test if `obj` is an `ISelection`.
         */
        static isISelection(obj: any): obj is ISelection;
        /**
         * Create with a direction.
         */
        static createWithDirection(startLineNumber: number, startColumn: number, endLineNumber: number, endColumn: number, direction: SelectionDirection): Selection;
    }

    /**
     * The direction of a selection.
     */
    export enum SelectionDirection {
        /**
         * The selection starts above where it ends.
         */
        LTR = 0,
        /**
         * The selection starts below where it ends.
         */
        RTL = 1,
    }

    export class Token {
        _tokenBrand: void;
        readonly offset: number;
        readonly type: string;
        readonly language: string;
        constructor(offset: number, type: string, language: string);
        toString(): string;
    }
}

declare module monaco.editor {


    /**
     * Create a new editor under `domElement`.
     * `domElement` should be empty (not contain other dom nodes).
     * The editor will read the size of `domElement`.
     */
    export function create(domElement: HTMLElement, options?: IEditorConstructionOptions, override?: IEditorOverrideServices): IStandaloneCodeEditor;

    /**
     * Emitted when an editor is created.
     * Creating a diff editor might cause this listener to be invoked with the two editors.
     * @event
     */
    export function onDidCreateEditor(listener: (codeEditor: ICodeEditor) => void): IDisposable;

    /**
     * Create a new diff editor under `domElement`.
     * `domElement` should be empty (not contain other dom nodes).
     * The editor will read the size of `domElement`.
     */
    export function createDiffEditor(domElement: HTMLElement, options?: IDiffEditorConstructionOptions, override?: IEditorOverrideServices): IStandaloneDiffEditor;

    export interface IDiffNavigator {
        revealFirst: boolean;
        canNavigate(): boolean;
        next(): void;
        previous(): void;
        dispose(): void;
    }

    export interface IDiffNavigatorOptions {
        readonly followsCaret?: boolean;
        readonly ignoreCharChanges?: boolean;
        readonly alwaysRevealFirst?: boolean;
    }

    export function createDiffNavigator(diffEditor: IStandaloneDiffEditor, opts?: IDiffNavigatorOptions): IDiffNavigator;

    /**
     * Create a new editor model.
     * You can specify the language that should be set for this model or let the language be inferred from the `uri`.
     */
    export function createModel(value: string, language?: string, uri?: Uri): IModel;

    /**
     * Change the language for a model.
     */
    export function setModelLanguage(model: IModel, language: string): void;

    /**
     * Set the markers for a model.
     */
    export function setModelMarkers(model: IModel, owner: string, markers: IMarkerData[]): void;

    /**
     * Get markers for owner ant/or resource
     * @returns {IMarkerData[]} list of markers
     * @param filter
     */
    export function getModelMarkers(filter: {
        owner?: string;
        resource?: Uri;
        take?: number;
    }): IMarker[];

    /**
     * Get the model that has `uri` if it exists.
     */
    export function getModel(uri: Uri): IModel;

    /**
     * Get all the created models.
     */
    export function getModels(): IModel[];

    /**
     * Emitted when a model is created.
     * @event
     */
    export function onDidCreateModel(listener: (model: IModel) => void): IDisposable;

    /**
     * Emitted right before a model is disposed.
     * @event
     */
    export function onWillDisposeModel(listener: (model: IModel) => void): IDisposable;

    /**
     * Emitted when a different language is set to a model.
     * @event
     */
    export function onDidChangeModelLanguage(listener: (e: {
        readonly model: IModel;
        readonly oldLanguage: string;
    }) => void): IDisposable;

    /**
     * Create a new web worker that has model syncing capabilities built in.
     * Specify an AMD module to load that will `create` an object that will be proxied.
     */
    export function createWebWorker<T>(opts: IWebWorkerOptions): MonacoWebWorker<T>;

    /**
     * Colorize the contents of `domNode` using attribute `data-lang`.
     */
    export function colorizeElement(domNode: HTMLElement, options: IColorizerElementOptions): Promise<void>;

    /**
     * Colorize `text` using language `languageId`.
     */
    export function colorize(text: string, languageId: string, options: IColorizerOptions): Promise<string>;

    /**
     * Colorize a line in a model.
     */
    export function colorizeModelLine(model: IModel, lineNumber: number, tabSize?: number): string;

    /**
     * Tokenize `text` using language `languageId`
     */
    export function tokenize(text: string, languageId: string): Token[][];

    /**
     * Define a new theme.
     */
    export function defineTheme(themeName: string, themeData: IStandaloneThemeData): void;

    /**
     * Switches to a theme.
     */
    export function setTheme(themeName: string): void;

    export type BuiltinTheme = 'vs' | 'vs-dark' | 'hc-black';

    export interface IStandaloneThemeData {
        base: BuiltinTheme;
        inherit: boolean;
        rules: ITokenThemeRule[];
        colors: IColors;
    }

    export type IColors = {
        [colorId: string]: string;
    };

    export interface ITokenThemeRule {
        token: string;
        foreground?: string;
        background?: string;
        fontStyle?: string;
    }

    /**
     * A web worker that can provide a proxy to an arbitrary file.
     */
    export interface MonacoWebWorker<T> {
        /**
         * Terminate the web worker, thus invalidating the returned proxy.
         */
        dispose(): void;
        /**
         * Get a proxy to the arbitrary loaded code.
         */
        getProxy(): Promise<T>;
        /**
         * Synchronize (send) the models at `resources` to the web worker,
         * making them available in the monaco.worker.getMirrorModels().
         */
        withSyncedResources(resources: Uri[]): Promise<T>;
    }

    export interface IWebWorkerOptions {
        /**
         * The AMD moduleId to load.
         * It should export a function `create` that should return the exported proxy.
         */
        moduleId: string;
        /**
         * The data to send over when calling create on the module.
         */
        createData?: any;
        /**
         * A label to be used to identify the web worker for debugging purposes.
         */
        label?: string;
    }

    /**
     * The options to create an editor.
     */
    export interface IEditorConstructionOptions extends IEditorOptions {
        /**
         * The initial model associated with this code editor.
         */
        model?: IModel;
        /**
         * The initial value of the auto created model in the editor.
         * To not create automatically a model, use `model: null`.
         */
        value?: string;
        /**
         * The initial language of the auto created model in the editor.
         * To not create automatically a model, use `model: null`.
         */
        language?: string;
        /**
         * Initial theme to be used for rendering.
         * The current out-of-the-box available themes are: 'vs' (default), 'vs-dark', 'hc-black'.
         * You can create custom themes via `monaco.editor.defineTheme`.
         * To switch a theme, use `monaco.editor.setTheme`
         */
        theme?: string;
        /**
         * An URL to open when Ctrl+H (Windows and Linux) or Cmd+H (OSX) is pressed in
         * the accessibility help dialog in the editor.
         *
         * Defaults to "https://go.microsoft.com/fwlink/?linkid=852450"
         */
        accessibilityHelpUrl?: string;
    }

    /**
     * The options to create a diff editor.
     */
    export interface IDiffEditorConstructionOptions extends IDiffEditorOptions {
        /**
         * Initial theme to be used for rendering.
         * The current out-of-the-box available themes are: 'vs' (default), 'vs-dark', 'hc-black'.
         * You can create custom themes via `monaco.editor.defineTheme`.
         * To switch a theme, use `monaco.editor.setTheme`
         */
        theme?: string;
    }

    export interface IStandaloneCodeEditor extends ICodeEditor {
        addCommand(keybinding: number, handler: ICommandHandler, context: string): string;
        createContextKey<T>(key: string, defaultValue: T): IContextKey<T>;
        addAction(descriptor: IActionDescriptor): IDisposable;
    }

    export interface IStandaloneDiffEditor extends IDiffEditor {
        addCommand(keybinding: number, handler: ICommandHandler, context: string): string;
        createContextKey<T>(key: string, defaultValue: T): IContextKey<T>;
        addAction(descriptor: IActionDescriptor): IDisposable;
        getOriginalEditor(): IStandaloneCodeEditor;
        getModifiedEditor(): IStandaloneCodeEditor;
    }
    export interface ICommandHandler {
        (...args: any[]): void;
    }

    export interface IContextKey<T> {
        set(value: T): void;
        reset(): void;
        get(): T;
    }

    export interface IEditorOverrideServices {
        [index: string]: any;
    }

    export interface IMarker {
        owner: string;
        resource: Uri;
        severity: Severity;
        code?: string;
        message: string;
        source?: string;
        startLineNumber: number;
        startColumn: number;
        endLineNumber: number;
        endColumn: number;
    }

    /**
     * A structure defining a problem/warning/etc.
     */
    export interface IMarkerData {
        code?: string;
        severity: Severity;
        message: string;
        source?: string;
        startLineNumber: number;
        startColumn: number;
        endLineNumber: number;
        endColumn: number;
    }

    export interface IColorizerOptions {
        tabSize?: number;
    }

    export interface IColorizerElementOptions extends IColorizerOptions {
        theme?: string;
        mimeType?: string;
    }

    export enum ScrollbarVisibility {
        Auto = 1,
        Hidden = 2,
        Visible = 3,
    }

    export interface ThemeColor {
        id: string;
    }

    /**
     * Vertical Lane in the overview ruler of the editor.
     */
    export enum OverviewRulerLane {
        Left = 1,
        Center = 2,
        Right = 4,
        Full = 7,
    }

    /**
     * Options for rendering a model decoration in the overview ruler.
     */
    export interface IModelDecorationOverviewRulerOptions {
        /**
         * CSS color to render in the overview ruler.
         * e.g.: rgba(100, 100, 100, 0.5) or a color from the color registry
         */
        color: string | ThemeColor;
        /**
         * CSS color to render in the overview ruler.
         * e.g.: rgba(100, 100, 100, 0.5) or a color from the color registry
         */
        darkColor: string | ThemeColor;
        /**
         * CSS color to render in the overview ruler.
         * e.g.: rgba(100, 100, 100, 0.5) or a color from the color registry
         */
        hcColor?: string | ThemeColor;
        /**
         * The position in the overview ruler.
         */
        position: OverviewRulerLane;
    }

    /**
     * Options for a model decoration.
     */
    export interface IModelDecorationOptions {
        /**
         * Customize the growing behavior of the decoration when typing at the edges of the decoration.
         * Defaults to TrackedRangeStickiness.AlwaysGrowsWhenTypingAtEdges
         */
        stickiness?: TrackedRangeStickiness;
        /**
         * CSS class name describing the decoration.
         */
        className?: string;
        /**
         * Message to be rendered when hovering over the glyph margin decoration.
         */
        glyphMarginHoverMessage?: MarkedString | MarkedString[];
        /**
         * Array of MarkedString to render as the decoration message.
         */
        hoverMessage?: MarkedString | MarkedString[];
        /**
         * Should the decoration expand to encompass a whole line.
         */
        isWholeLine?: boolean;
        /**
         * If set, render this decoration in the overview ruler.
         */
        overviewRuler?: IModelDecorationOverviewRulerOptions;
        /**
         * If set, the decoration will be rendered in the glyph margin with this CSS class name.
         */
        glyphMarginClassName?: string;
        /**
         * If set, the decoration will be rendered in the lines decorations with this CSS class name.
         */
        linesDecorationsClassName?: string;
        /**
         * If set, the decoration will be rendered in the margin (covering its full width) with this CSS class name.
         */
        marginClassName?: string;
        /**
         * If set, the decoration will be rendered inline with the text with this CSS class name.
         * Please use this only for CSS rules that must impact the text. For example, use `className`
         * to have a background color decoration.
         */
        inlineClassName?: string;
        /**
         * If set, the decoration will be rendered before the text with this CSS class name.
         */
        beforeContentClassName?: string;
        /**
         * If set, the decoration will be rendered after the text with this CSS class name.
         */
        afterContentClassName?: string;
    }

    /**
     * New model decorations.
     */
    export interface IModelDeltaDecoration {
        /**
         * Range that this decoration covers.
         */
        range: IRange;
        /**
         * Options associated with this decoration.
         */
        options: IModelDecorationOptions;
    }

    /**
     * A decoration in the model.
     */
    export interface IModelDecoration {
        /**
         * Identifier for a decoration.
         */
        readonly id: string;
        /**
         * Identifier for a decoration's owener.
         */
        readonly ownerId: number;
        /**
         * Range that this decoration covers.
         */
        readonly range: Range;
        /**
         * Options associated with this decoration.
         */
        readonly options: IModelDecorationOptions;
        /**
         * A flag describing if this is a problem decoration (e.g. warning/error).
         */
        readonly isForValidation: boolean;
    }

    /**
     * Word inside a model.
     */
    export interface IWordAtPosition {
        /**
         * The word.
         */
        readonly word: string;
        /**
         * The column where the word starts.
         */
        readonly startColumn: number;
        /**
         * The column where the word ends.
         */
        readonly endColumn: number;
    }

    /**
     * End of line character preference.
     */
    export enum EndOfLinePreference {
        /**
         * Use the end of line character identified in the text buffer.
         */
        TextDefined = 0,
        /**
         * Use line feed (\n) as the end of line character.
         */
        LF = 1,
        /**
         * Use carriage return and line feed (\r\n) as the end of line character.
         */
        CRLF = 2,
    }

    /**
     * The default end of line to use when instantiating models.
     */
    export enum DefaultEndOfLine {
        /**
         * Use line feed (\n) as the end of line character.
         */
        LF = 1,
        /**
         * Use carriage return and line feed (\r\n) as the end of line character.
         */
        CRLF = 2,
    }

    /**
     * End of line character preference.
     */
    export enum EndOfLineSequence {
        /**
         * Use line feed (\n) as the end of line character.
         */
        LF = 0,
        /**
         * Use carriage return and line feed (\r\n) as the end of line character.
         */
        CRLF = 1,
    }

    /**
     * An identifier for a single edit operation.
     */
    export interface ISingleEditOperationIdentifier {
        /**
         * Identifier major
         */
        major: number;
        /**
         * Identifier minor
         */
        minor: number;
    }

    /**
     * A builder and helper for edit operations for a command.
     */
    export interface IEditOperationBuilder {
        /**
         * Add a new edit operation (a replace operation).
         * @param range The range to replace (delete). May be empty to represent a simple insert.
         * @param text The text to replace with. May be null to represent a simple delete.
         */
        addEditOperation(range: Range, text: string): void;
        /**
         * Add a new edit operation (a replace operation).
         * The inverse edits will be accessible in `ICursorStateComputerData.getInverseEditOperations()`
         * @param range The range to replace (delete). May be empty to represent a simple insert.
         * @param text The text to replace with. May be null to represent a simple delete.
         */
        addTrackedEditOperation(range: Range, text: string): void;
        /**
         * Track `selection` when applying edit operations.
         * A best effort will be made to not grow/expand the selection.
         * An empty selection will clamp to a nearby character.
         * @param selection The selection to track.
         * @param trackPreviousOnEmpty If set, and the selection is empty, indicates whether the selection
         *           should clamp to the previous or the next character.
         * @return A unique identifer.
         */
        trackSelection(selection: Selection, trackPreviousOnEmpty?: boolean): string;
    }

    /**
     * A helper for computing cursor state after a command.
     */
    export interface ICursorStateComputerData {
        /**
         * Get the inverse edit operations of the added edit operations.
         */
        getInverseEditOperations(): IIdentifiedSingleEditOperation[];
        /**
         * Get a previously tracked selection.
         * @param id The unique identifier returned by `trackSelection`.
         * @return The selection.
         */
        getTrackedSelection(id: string): Selection;
    }

    /**
     * A command that modifies text / cursor state on a model.
     */
    export interface ICommand {
        /**
         * Get the edit operations needed to execute this command.
         * @param model The model the command will execute on.
         * @param builder A helper to collect the needed edit operations and to track selections.
         */
        getEditOperations(model: ITokenizedModel, builder: IEditOperationBuilder): void;
        /**
         * Compute the cursor state after the edit operations were applied.
         * @param model The model the commad has executed on.
         * @param helper A helper to get inverse edit operations and to get previously tracked selections.
         * @return The cursor state after the command executed.
         */
        computeCursorState(model: ITokenizedModel, helper: ICursorStateComputerData): Selection;
    }

    /**
     * A single edit operation, that acts as a simple replace.
     * i.e. Replace text at `range` with `text` in model.
     */
    export interface ISingleEditOperation {
        /**
         * The range to replace. This can be empty to emulate a simple insert.
         */
        range: IRange;
        /**
         * The text to replace with. This can be null to emulate a simple delete.
         */
        text: string;
        /**
         * This indicates that this operation has "insert" semantics.
         * i.e. forceMoveMarkers = true => if `range` is collapsed, all markers at the position will be moved.
         */
        forceMoveMarkers?: boolean;
    }

    /**
     * A single edit operation, that has an identifier.
     */
    export interface IIdentifiedSingleEditOperation {
        /**
         * An identifier associated with this single edit operation.
         */
        identifier: ISingleEditOperationIdentifier;
        /**
         * The range to replace. This can be empty to emulate a simple insert.
         */
        range: Range;
        /**
         * The text to replace with. This can be null to emulate a simple delete.
         */
        text: string;
        /**
         * This indicates that this operation has "insert" semantics.
         * i.e. forceMoveMarkers = true => if `range` is collapsed, all markers at the position will be moved.
         */
        forceMoveMarkers: boolean;
        /**
         * This indicates that this operation is inserting automatic whitespace
         * that can be removed on next model edit operation if `config.trimAutoWhitespace` is true.
         */
        isAutoWhitespaceEdit?: boolean;
    }

    /**
     * A callback that can compute the cursor state after applying a series of edit operations.
     */
    export interface ICursorStateComputer {
        /**
         * A callback that can compute the resulting cursors state after some edit operations have been executed.
         */
        (inverseEditOperations: IIdentifiedSingleEditOperation[]): Selection[];
    }

    export class TextModelResolvedOptions {
        _textModelResolvedOptionsBrand: void;
        readonly tabSize: number;
        readonly insertSpaces: boolean;
        readonly defaultEOL: DefaultEndOfLine;
        readonly trimAutoWhitespace: boolean;
    }

    export interface ITextModelUpdateOptions {
        tabSize?: number;
        insertSpaces?: boolean;
        trimAutoWhitespace?: boolean;
    }

    /**
     * A textual read-only model.
     */
    export interface ITextModel {
        /**
         * Get the resolved options for this model.
         */
        getOptions(): TextModelResolvedOptions;
        /**
         * Get the current version id of the model.
         * Anytime a change happens to the model (even undo/redo),
         * the version id is incremented.
         */
        getVersionId(): number;
        /**
         * Get the alternative version id of the model.
         * This alternative version id is not always incremented,
         * it will return the same values in the case of undo-redo.
         */
        getAlternativeVersionId(): number;
        /**
         * Replace the entire text buffer value contained in this model.
         */
        setValue(newValue: string): void;
        /**
         * Get the text stored in this model.
         * @param eol The end of line character preference. Defaults to `EndOfLinePreference.TextDefined`.
         * @param preserverBOM Preserve a BOM character if it was detected when the model was constructed.
         * @return The text.
         */
        getValue(eol?: EndOfLinePreference, preserveBOM?: boolean): string;
        /**
         * Get the length of the text stored in this model.
         */
        getValueLength(eol?: EndOfLinePreference, preserveBOM?: boolean): number;
        /**
         * Get the text in a certain range.
         * @param range The range describing what text to get.
         * @param eol The end of line character preference. This will only be used for multiline ranges. Defaults to `EndOfLinePreference.TextDefined`.
         * @return The text.
         */
        getValueInRange(range: IRange, eol?: EndOfLinePreference): string;
        /**
         * Get the length of text in a certain range.
         * @param range The range describing what text length to get.
         * @return The text length.
         */
        getValueLengthInRange(range: IRange): number;
        /**
         * Get the number of lines in the model.
         */
        getLineCount(): number;
        /**
         * Get the text for a certain line.
         */
        getLineContent(lineNumber: number): string;
        /**
         * Get the text for all lines.
         */
        getLinesContent(): string[];
        /**
         * Get the end of line sequence predominantly used in the text buffer.
         * @return EOL char sequence (e.g.: '\n' or '\r\n').
         */
        getEOL(): string;
        /**
         * Change the end of line sequence used in the text buffer.
         */
        setEOL(eol: EndOfLineSequence): void;
        /**
         * Get the minimum legal column for line at `lineNumber`
         */
        getLineMinColumn(lineNumber: number): number;
        /**
         * Get the maximum legal column for line at `lineNumber`
         */
        getLineMaxColumn(lineNumber: number): number;
        /**
         * Returns the column before the first non whitespace character for line at `lineNumber`.
         * Returns 0 if line is empty or contains only whitespace.
         */
        getLineFirstNonWhitespaceColumn(lineNumber: number): number;
        /**
         * Returns the column after the last non whitespace character for line at `lineNumber`.
         * Returns 0 if line is empty or contains only whitespace.
         */
        getLineLastNonWhitespaceColumn(lineNumber: number): number;
        /**
         * Create a valid position,
         */
        validatePosition(position: IPosition): Position;
        /**
         * Advances the given position by the given offest (negative offsets are also accepted)
         * and returns it as a new valid position.
         *
         * If the offset and position are such that their combination goes beyond the beginning or
         * end of the model, throws an exception.
         *
         * If the ofsset is such that the new position would be in the middle of a multi-byte
         * line terminator, throws an exception.
         */
        modifyPosition(position: IPosition, offset: number): Position;
        /**
         * Create a valid range.
         */
        validateRange(range: IRange): Range;
        /**
         * Converts the position to a zero-based offset.
         *
         * The position will be [adjusted](#TextDocument.validatePosition).
         *
         * @param position A position.
         * @return A valid zero-based offset.
         */
        getOffsetAt(position: IPosition): number;
        /**
         * Converts a zero-based offset to a position.
         *
         * @param offset A zero-based offset.
         * @return A valid [position](#Position).
         */
        getPositionAt(offset: number): Position;
        /**
         * Get a range covering the entire model
         */
        getFullModelRange(): Range;
        /**
         * Returns iff the model was disposed or not.
         */
        isDisposed(): boolean;
        /**
         * Search the model.
         * @param searchString The string used to search. If it is a regular expression, set `isRegex` to true.
         * @param searchOnlyEditableRange Limit the searching to only search inside the editable range of the model.
         * @param isRegex Used to indicate that `searchString` is a regular expression.
         * @param matchCase Force the matching to match lower/upper case exactly.
         * @param wordSeparators Force the matching to match entire words only. Pass null otherwise.
         * @param captureMatches The result will contain the captured groups.
         * @param limitResultCount Limit the number of results
         * @return The ranges where the matches are. It is empty if not matches have been found.
         */
        findMatches(searchString: string, searchOnlyEditableRange: boolean, isRegex: boolean, matchCase: boolean, wordSeparators: string, captureMatches: boolean, limitResultCount?: number): FindMatch[];
        /**
         * Search the model.
         * @param searchString The string used to search. If it is a regular expression, set `isRegex` to true.
         * @param searchScope Limit the searching to only search inside this range.
         * @param isRegex Used to indicate that `searchString` is a regular expression.
         * @param matchCase Force the matching to match lower/upper case exactly.
         * @param wordSeparators Force the matching to match entire words only. Pass null otherwise.
         * @param captureMatches The result will contain the captured groups.
         * @param limitResultCount Limit the number of results
         * @return The ranges where the matches are. It is empty if no matches have been found.
         */
        findMatches(searchString: string, searchScope: IRange, isRegex: boolean, matchCase: boolean, wordSeparators: string, captureMatches: boolean, limitResultCount?: number): FindMatch[];
        /**
         * Search the model for the next match. Loops to the beginning of the model if needed.
         * @param searchString The string used to search. If it is a regular expression, set `isRegex` to true.
         * @param searchStart Start the searching at the specified position.
         * @param isRegex Used to indicate that `searchString` is a regular expression.
         * @param matchCase Force the matching to match lower/upper case exactly.
         * @param wordSeparators Force the matching to match entire words only. Pass null otherwise.
         * @param captureMatches The result will contain the captured groups.
         * @return The range where the next match is. It is null if no next match has been found.
         */
        findNextMatch(searchString: string, searchStart: IPosition, isRegex: boolean, matchCase: boolean, wordSeparators: string, captureMatches: boolean): FindMatch;
        /**
         * Search the model for the previous match. Loops to the end of the model if needed.
         * @param searchString The string used to search. If it is a regular expression, set `isRegex` to true.
         * @param searchStart Start the searching at the specified position.
         * @param isRegex Used to indicate that `searchString` is a regular expression.
         * @param matchCase Force the matching to match lower/upper case exactly.
         * @param wordSeparators Force the matching to match entire words only. Pass null otherwise.
         * @param captureMatches The result will contain the captured groups.
         * @return The range where the previous match is. It is null if no previous match has been found.
         */
        findPreviousMatch(searchString: string, searchStart: IPosition, isRegex: boolean, matchCase: boolean, wordSeparators: string, captureMatches: boolean): FindMatch;
    }

    export class FindMatch {
        _findMatchBrand: void;
        readonly range: Range;
        readonly matches: string[];
    }

    export interface IReadOnlyModel extends ITextModel {
        /**
         * Gets the resource associated with this editor model.
         */
        readonly uri: Uri;
        /**
         * Get the language associated with this model.
         */
        getModeId(): string;
        /**
         * Get the word under or besides `position`.
         * @param position The position to look for a word.
         * @param skipSyntaxTokens Ignore syntax tokens, as identified by the mode.
         * @return The word under or besides `position`. Might be null.
         */
        getWordAtPosition(position: IPosition): IWordAtPosition;
        /**
         * Get the word under or besides `position` trimmed to `position`.column
         * @param position The position to look for a word.
         * @param skipSyntaxTokens Ignore syntax tokens, as identified by the mode.
         * @return The word under or besides `position`. Will never be null.
         */
        getWordUntilPosition(position: IPosition): IWordAtPosition;
    }

    /**
     * A model that is tokenized.
     */
    export interface ITokenizedModel extends ITextModel {
        /**
         * Get the language associated with this model.
         */
        getModeId(): string;
        /**
         * Get the word under or besides `position`.
         * @param position The position to look for a word.
         * @param skipSyntaxTokens Ignore syntax tokens, as identified by the mode.
         * @return The word under or besides `position`. Might be null.
         */
        getWordAtPosition(position: IPosition): IWordAtPosition;
        /**
         * Get the word under or besides `position` trimmed to `position`.column
         * @param position The position to look for a word.
         * @param skipSyntaxTokens Ignore syntax tokens, as identified by the mode.
         * @return The word under or besides `position`. Will never be null.
         */
        getWordUntilPosition(position: IPosition): IWordAtPosition;
    }

    /**
     * A model that can track markers.
     */
    export interface ITextModelWithMarkers extends ITextModel {
    }

    /**
     * Describes the behavior of decorations when typing/editing near their edges.
     * Note: Please do not edit the values, as they very carefully match `DecorationRangeBehavior`
     */
    export enum TrackedRangeStickiness {
        AlwaysGrowsWhenTypingAtEdges = 0,
        NeverGrowsWhenTypingAtEdges = 1,
        GrowsOnlyWhenTypingBefore = 2,
        GrowsOnlyWhenTypingAfter = 3,
    }

    /**
     * A model that can have decorations.
     */
    export interface ITextModelWithDecorations {
        /**
         * Perform a minimum ammount of operations, in order to transform the decorations
         * identified by `oldDecorations` to the decorations described by `newDecorations`
         * and returns the new identifiers associated with the resulting decorations.
         *
         * @param oldDecorations Array containing previous decorations identifiers.
         * @param newDecorations Array describing what decorations should result after the call.
         * @param ownerId Identifies the editor id in which these decorations should appear. If no `ownerId` is provided, the decorations will appear in all editors that attach this model.
         * @return An array containing the new decorations identifiers.
         */
        deltaDecorations(oldDecorations: string[], newDecorations: IModelDeltaDecoration[], ownerId?: number): string[];
        /**
         * Get the options associated with a decoration.
         * @param id The decoration id.
         * @return The decoration options or null if the decoration was not found.
         */
        getDecorationOptions(id: string): IModelDecorationOptions;
        /**
         * Get the range associated with a decoration.
         * @param id The decoration id.
         * @return The decoration range or null if the decoration was not found.
         */
        getDecorationRange(id: string): Range;
        /**
         * Gets all the decorations for the line `lineNumber` as an array.
         * @param lineNumber The line number
         * @param ownerId If set, it will ignore decorations belonging to other owners.
         * @param filterOutValidation If set, it will ignore decorations specific to validation (i.e. warnings, errors).
         * @return An array with the decorations
         */
        getLineDecorations(lineNumber: number, ownerId?: number, filterOutValidation?: boolean): IModelDecoration[];
        /**
         * Gets all the decorations for the lines between `startLineNumber` and `endLineNumber` as an array.
         * @param startLineNumber The start line number
         * @param endLineNumber The end line number
         * @param ownerId If set, it will ignore decorations belonging to other owners.
         * @param filterOutValidation If set, it will ignore decorations specific to validation (i.e. warnings, errors).
         * @return An array with the decorations
         */
        getLinesDecorations(startLineNumber: number, endLineNumber: number, ownerId?: number, filterOutValidation?: boolean): IModelDecoration[];
        /**
         * Gets all the deocorations in a range as an array. Only `startLineNumber` and `endLineNumber` from `range` are used for filtering.
         * So for now it returns all the decorations on the same line as `range`.
         * @param range The range to search in
         * @param ownerId If set, it will ignore decorations belonging to other owners.
         * @param filterOutValidation If set, it will ignore decorations specific to validation (i.e. warnings, errors).
         * @return An array with the decorations
         */
        getDecorationsInRange(range: IRange, ownerId?: number, filterOutValidation?: boolean): IModelDecoration[];
        /**
         * Gets all the decorations as an array.
         * @param ownerId If set, it will ignore decorations belonging to other owners.
         * @param filterOutValidation If set, it will ignore decorations specific to validation (i.e. warnings, errors).
         */
        getAllDecorations(ownerId?: number, filterOutValidation?: boolean): IModelDecoration[];
    }

    /**
     * An editable text model.
     */
    export interface IEditableTextModel extends ITextModelWithMarkers {
        /**
         * Normalize a string containing whitespace according to indentation rules (converts to spaces or to tabs).
         */
        normalizeIndentation(str: string): string;
        /**
         * Get what is considered to be one indent (e.g. a tab character or 4 spaces, etc.).
         */
        getOneIndent(): string;
        /**
         * Change the options of this model.
         */
        updateOptions(newOpts: ITextModelUpdateOptions): void;
        /**
         * Detect the indentation options for this model from its content.
         */
        detectIndentation(defaultInsertSpaces: boolean, defaultTabSize: number): void;
        /**
         * Push a stack element onto the undo stack. This acts as an undo/redo point.
         * The idea is to use `pushEditOperations` to edit the model and then to
         * `pushStackElement` to create an undo/redo stop point.
         */
        pushStackElement(): void;
        /**
         * Push edit operations, basically editing the model. This is the preferred way
         * of editing the model. The edit operations will land on the undo stack.
         * @param beforeCursorState The cursor state before the edit operaions. This cursor state will be returned when `undo` or `redo` are invoked.
         * @param editOperations The edit operations.
         * @param cursorStateComputer A callback that can compute the resulting cursors state after the edit operations have been executed.
         * @return The cursor state returned by the `cursorStateComputer`.
         */
        pushEditOperations(beforeCursorState: Selection[], editOperations: IIdentifiedSingleEditOperation[], cursorStateComputer: ICursorStateComputer): Selection[];
        /**
         * Edit the model without adding the edits to the undo stack.
         * This can have dire consequences on the undo stack! See @pushEditOperations for the preferred way.
         * @param operations The edit operations.
         * @return The inverse edit operations, that, when applied, will bring the model back to the previous state.
         */
        applyEdits(operations: IIdentifiedSingleEditOperation[]): IIdentifiedSingleEditOperation[];
    }

    /**
     * A model.
     */
    export interface IModel extends IReadOnlyModel, IEditableTextModel, ITextModelWithMarkers, ITokenizedModel, ITextModelWithDecorations {
        /**
         * An event emitted when the contents of the model have changed.
         * @event
         */
        onDidChangeContent(listener: (e: IModelContentChangedEvent) => void): IDisposable;
        /**
         * An event emitted when decorations of the model have changed.
         * @event
         */
        onDidChangeDecorations(listener: (e: IModelDecorationsChangedEvent) => void): IDisposable;
        /**
         * An event emitted when the model options have changed.
         * @event
         */
        onDidChangeOptions(listener: (e: IModelOptionsChangedEvent) => void): IDisposable;
        /**
         * An event emitted when the language associated with the model has changed.
         * @event
         */
        onDidChangeLanguage(listener: (e: IModelLanguageChangedEvent) => void): IDisposable;
        /**
         * An event emitted right before disposing the model.
         * @event
         */
        onWillDispose(listener: () => void): IDisposable;
        /**
         * A unique identifier associated with this model.
         */
        readonly id: string;
        /**
         * Destroy this model. This will unbind the model from the mode
         * and make all necessary clean-up to release this object to the GC.
         */
        dispose(): void;
    }

    /**
     * A model for the diff editor.
     */
    export interface IDiffEditorModel {
        /**
         * Original model.
         */
        original: IModel;
        /**
         * Modified model.
         */
        modified: IModel;
    }

    /**
     * An event describing that an editor has had its model reset (i.e. `editor.setModel()`).
     */
    export interface IModelChangedEvent {
        /**
         * The `uri` of the previous model or null.
         */
        readonly oldModelUrl: Uri;
        /**
         * The `uri` of the new model or null.
         */
        readonly newModelUrl: Uri;
    }

    export interface IDimension {
        width: number;
        height: number;
    }

    /**
     * A change
     */
    export interface IChange {
        readonly originalStartLineNumber: number;
        readonly originalEndLineNumber: number;
        readonly modifiedStartLineNumber: number;
        readonly modifiedEndLineNumber: number;
    }

    /**
     * A character level change.
     */
    export interface ICharChange extends IChange {
        readonly originalStartColumn: number;
        readonly originalEndColumn: number;
        readonly modifiedStartColumn: number;
        readonly modifiedEndColumn: number;
    }

    /**
     * A line change
     */
    export interface ILineChange extends IChange {
        readonly charChanges: ICharChange[];
    }

    /**
     * Information about a line in the diff editor
     */
    export interface IDiffLineInformation {
        readonly equivalentLineNumber: number;
    }

    export interface INewScrollPosition {
        scrollLeft?: number;
        scrollTop?: number;
    }

    /**
     * Description of an action contribution
     */
    export interface IActionDescriptor {
        /**
         * An unique identifier of the contributed action.
         */
        id: string;
        /**
         * A label of the action that will be presented to the user.
         */
        label: string;
        /**
         * Precondition rule.
         */
        precondition?: string;
        /**
         * An array of keybindings for the action.
         */
        keybindings?: number[];
        /**
         * The keybinding rule (condition on top of precondition).
         */
        keybindingContext?: string;
        /**
         * Control if the action should show up in the context menu and where.
         * The context menu of the editor has these default:
         *   navigation - The navigation group comes first in all cases.
         *   1_modification - This group comes next and contains commands that modify your code.
         *   9_cutcopypaste - The last default group with the basic editing commands.
         * You can also create your own group.
         * Defaults to null (don't show in context menu).
         */
        contextMenuGroupId?: string;
        /**
         * Control the order in the context menu group.
         */
        contextMenuOrder?: number;
        /**
         * Method that will be executed when the action is triggered.
         * @param editor The editor instance is passed in as a convinience
         */
        run(editor: ICommonCodeEditor): void | Promise<void>;
    }

    export interface IEditorAction {
        readonly id: string;
        readonly label: string;
        readonly alias: string;
        isSupported(): boolean;
        run(): Promise<void>;
    }

    export type IEditorModel = IModel | IDiffEditorModel;

    /**
     * A (serializable) state of the cursors.
     */
    export interface ICursorState {
        inSelectionMode: boolean;
        selectionStart: IPosition;
        position: IPosition;
    }

    /**
     * A (serializable) state of the view.
     */
    export interface IViewState {
        scrollTop: number;
        scrollTopWithoutViewZones: number;
        scrollLeft: number;
    }

    /**
     * A (serializable) state of the code editor.
     */
    export interface ICodeEditorViewState {
        cursorState: ICursorState[];
        viewState: IViewState;
        contributionsState: {
            [id: string]: any;
        };
    }

    /**
     * (Serializable) View state for the diff editor.
     */
    export interface IDiffEditorViewState {
        original: ICodeEditorViewState;
        modified: ICodeEditorViewState;
    }

    /**
     * An editor view state.
     */
    export type IEditorViewState = ICodeEditorViewState | IDiffEditorViewState;

    /**
     * An editor.
     */
    export interface IEditor {
        /**
         * An event emitted when the editor has been disposed.
         * @event
         */
        onDidDispose(listener: () => void): IDisposable;
        /**
         * Dispose the editor.
         */
        dispose(): void;
        /**
         * Get a unique id for this editor instance.
         */
        getId(): string;
        /**
         * Get the editor type. Please see `EditorType`.
         * This is to avoid an instanceof check
         */
        getEditorType(): string;
        /**
         * Update the editor's options after the editor has been created.
         */
        updateOptions(newOptions: IEditorOptions): void;
        /**
         * Instructs the editor to remeasure its container. This method should
         * be called when the container of the editor gets resized.
         */
        layout(dimension?: IDimension): void;
        /**
         * Brings browser focus to the editor text
         */
        focus(): void;
        /**
         * Returns true if this editor has keyboard focus (e.g. cursor is blinking).
         */
        isFocused(): boolean;
        /**
         * Returns all actions associated with this editor.
         */
        getActions(): IEditorAction[];
        /**
         * Returns all actions associated with this editor.
         */
        getSupportedActions(): IEditorAction[];
        /**
         * Saves current view state of the editor in a serializable object.
         */
        saveViewState(): IEditorViewState;
        /**
         * Restores the view state of the editor from a serializable object generated by `saveViewState`.
         */
        restoreViewState(state: IEditorViewState): void;
        /**
         * Given a position, returns a column number that takes tab-widths into account.
         */
        getVisibleColumnFromPosition(position: IPosition): number;
        /**
         * Returns the primary position of the cursor.
         */
        getPosition(): Position;
        /**
         * Set the primary position of the cursor. This will remove any secondary cursors.
         * @param position New primary cursor's position
         */
        setPosition(position: IPosition): void;
        /**
         * Scroll vertically as necessary and reveal a line.
         */
        revealLine(lineNumber: number): void;
        /**
         * Scroll vertically as necessary and reveal a line centered vertically.
         */
        revealLineInCenter(lineNumber: number): void;
        /**
         * Scroll vertically as necessary and reveal a line centered vertically only if it lies outside the viewport.
         */
        revealLineInCenterIfOutsideViewport(lineNumber: number): void;
        /**
         * Scroll vertically or horizontally as necessary and reveal a position.
         */
        revealPosition(position: IPosition, revealVerticalInCenter?: boolean, revealHorizontal?: boolean): void;
        /**
         * Scroll vertically or horizontally as necessary and reveal a position centered vertically.
         */
        revealPositionInCenter(position: IPosition): void;
        /**
         * Scroll vertically or horizontally as necessary and reveal a position centered vertically only if it lies outside the viewport.
         */
        revealPositionInCenterIfOutsideViewport(position: IPosition): void;
        /**
         * Returns the primary selection of the editor.
         */
        getSelection(): Selection;
        /**
         * Returns all the selections of the editor.
         */
        getSelections(): Selection[];
        /**
         * Set the primary selection of the editor. This will remove any secondary cursors.
         * @param selection The new selection
         */
        setSelection(selection: IRange): void;
        /**
         * Set the primary selection of the editor. This will remove any secondary cursors.
         * @param selection The new selection
         */
        setSelection(selection: Range): void;
        /**
         * Set the primary selection of the editor. This will remove any secondary cursors.
         * @param selection The new selection
         */
        setSelection(selection: ISelection): void;
        /**
         * Set the primary selection of the editor. This will remove any secondary cursors.
         * @param selection The new selection
         */
        setSelection(selection: Selection): void;
        /**
         * Set the selections for all the cursors of the editor.
         * Cursors will be removed or added, as necessary.
         */
        setSelections(selections: ISelection[]): void;
        /**
         * Scroll vertically as necessary and reveal lines.
         */
        revealLines(startLineNumber: number, endLineNumber: number): void;
        /**
         * Scroll vertically as necessary and reveal lines centered vertically.
         */
        revealLinesInCenter(lineNumber: number, endLineNumber: number): void;
        /**
         * Scroll vertically as necessary and reveal lines centered vertically only if it lies outside the viewport.
         */
        revealLinesInCenterIfOutsideViewport(lineNumber: number, endLineNumber: number): void;
        /**
         * Scroll vertically or horizontally as necessary and reveal a range.
         */
        revealRange(range: IRange): void;
        /**
         * Scroll vertically or horizontally as necessary and reveal a range centered vertically.
         */
        revealRangeInCenter(range: IRange): void;
        /**
         * Scroll vertically or horizontally as necessary and reveal a range at the top of the viewport.
         */
        revealRangeAtTop(range: IRange): void;
        /**
         * Scroll vertically or horizontally as necessary and reveal a range centered vertically only if it lies outside the viewport.
         */
        revealRangeInCenterIfOutsideViewport(range: IRange): void;
        /**
         * Directly trigger a handler or an editor action.
         * @param source The source of the call.
         * @param handlerId The id of the handler or the id of a contribution.
         * @param payload Extra data to be sent to the handler.
         */
        trigger(source: string, handlerId: string, payload: any): void;
        /**
         * Gets the current model attached to this editor.
         */
        getModel(): IEditorModel;
        /**
         * Sets the current model attached to this editor.
         * If the previous model was created by the editor via the value key in the options
         * literal object, it will be destroyed. Otherwise, if the previous model was set
         * via setModel, or the model key in the options literal object, the previous model
         * will not be destroyed.
         * It is safe to call setModel(null) to simply detach the current model from the editor.
         */
        setModel(model: IEditorModel): void;
    }

    /**
     * An editor contribution that gets created every time a new editor gets created and gets disposed when the editor gets disposed.
     */
    export interface IEditorContribution {
        /**
         * Get a unique identifier for this contribution.
         */
        getId(): string;
        /**
         * Dispose this contribution.
         */
        dispose(): void;
        /**
         * Store view state.
         */
        saveViewState?(): any;
        /**
         * Restore view state.
         */
        restoreViewState?(state: any): void;
    }

    export interface ICommonCodeEditor extends IEditor {
        /**
         * An event emitted when the content of the current model has changed.
         * @event
         */
        onDidChangeModelContent(listener: (e: IModelContentChangedEvent) => void): IDisposable;
        /**
         * An event emitted when the language of the current model has changed.
         * @event
         */
        onDidChangeModelLanguage(listener: (e: IModelLanguageChangedEvent) => void): IDisposable;
        /**
         * An event emitted when the options of the current model has changed.
         * @event
         */
        onDidChangeModelOptions(listener: (e: IModelOptionsChangedEvent) => void): IDisposable;
        /**
         * An event emitted when the configuration of the editor has changed. (e.g. `editor.updateOptions()`)
         * @event
         */
        onDidChangeConfiguration(listener: (e: IConfigurationChangedEvent) => void): IDisposable;
        /**
         * An event emitted when the cursor position has changed.
         * @event
         */
        onDidChangeCursorPosition(listener: (e: ICursorPositionChangedEvent) => void): IDisposable;
        /**
         * An event emitted when the cursor selection has changed.
         * @event
         */
        onDidChangeCursorSelection(listener: (e: ICursorSelectionChangedEvent) => void): IDisposable;
        /**
         * An event emitted when the model of this editor has changed (e.g. `editor.setModel()`).
         * @event
         */
        onDidChangeModel(listener: (e: IModelChangedEvent) => void): IDisposable;
        /**
         * An event emitted when the decorations of the current model have changed.
         * @event
         */
        onDidChangeModelDecorations(listener: (e: IModelDecorationsChangedEvent) => void): IDisposable;
        /**
         * An event emitted when the text inside this editor gained focus (i.e. cursor blinking).
         * @event
         */
        onDidFocusEditorText(listener: () => void): IDisposable;
        /**
         * An event emitted when the text inside this editor lost focus.
         * @event
         */
        onDidBlurEditorText(listener: () => void): IDisposable;
        /**
         * An event emitted when the text inside this editor or an editor widget gained focus.
         * @event
         */
        onDidFocusEditor(listener: () => void): IDisposable;
        /**
         * An event emitted when the text inside this editor or an editor widget lost focus.
         * @event
         */
        onDidBlurEditor(listener: () => void): IDisposable;
        /**
         * Saves current view state of the editor in a serializable object.
         */
        saveViewState(): ICodeEditorViewState;
        /**
         * Restores the view state of the editor from a serializable object generated by `saveViewState`.
         */
        restoreViewState(state: ICodeEditorViewState): void;
        /**
         * Returns true if this editor or one of its widgets has keyboard focus.
         */
        hasWidgetFocus(): boolean;
        /**
         * Get a contribution of this editor.
         * @id Unique identifier of the contribution.
         * @return The contribution or null if contribution not found.
         */
        getContribution<T extends IEditorContribution>(id: string): T;
        /**
         * Type the getModel() of IEditor.
         */
        getModel(): IModel;
        /**
         * Returns the current editor's configuration
         */
        getConfiguration(): InternalEditorOptions;
        /**
         * Get value of the current model attached to this editor.
         * @see IModel.getValue
         */
        getValue(options?: {
            preserveBOM: boolean;
            lineEnding: string;
        }): string;
        /**
         * Set the value of the current model attached to this editor.
         * @see IModel.setValue
         */
        setValue(newValue: string): void;
        /**
         * Get the scrollWidth of the editor's viewport.
         */
        getScrollWidth(): number;
        /**
         * Get the scrollLeft of the editor's viewport.
         */
        getScrollLeft(): number;
        /**
         * Get the scrollHeight of the editor's viewport.
         */
        getScrollHeight(): number;
        /**
         * Get the scrollTop of the editor's viewport.
         */
        getScrollTop(): number;
        /**
         * Change the scrollLeft of the editor's viewport.
         */
        setScrollLeft(newScrollLeft: number): void;
        /**
         * Change the scrollTop of the editor's viewport.
         */
        setScrollTop(newScrollTop: number): void;
        /**
         * Change the scroll position of the editor's viewport.
         */
        setScrollPosition(position: INewScrollPosition): void;
        /**
         * Get an action that is a contribution to this editor.
         * @id Unique identifier of the contribution.
         * @return The action or null if action not found.
         */
        getAction(id: string): IEditorAction;
        /**
         * Execute a command on the editor.
         * The edits will land on the undo-redo stack, but no "undo stop" will be pushed.
         * @param source The source of the call.
         * @param command The command to execute
         */
        executeCommand(source: string, command: ICommand): void;
        /**
         * Push an "undo stop" in the undo-redo stack.
         */
        pushUndoStop(): boolean;
        /**
         * Execute edits on the editor.
         * The edits will land on the undo-redo stack, but no "undo stop" will be pushed.
         * @param source The source of the call.
         * @param edits The edits to execute.
         * @param endCursoState Cursor state after the edits were applied.
         */
        executeEdits(source: string, edits: IIdentifiedSingleEditOperation[], endCursoState?: Selection[]): boolean;
        /**
         * Execute multiple (concommitent) commands on the editor.
         * @param source The source of the call.
         * @param command The commands to execute
         */
        executeCommands(source: string, commands: ICommand[]): void;
        /**
         * Get all the decorations on a line (filtering out decorations from other editors).
         */
        getLineDecorations(lineNumber: number): IModelDecoration[];
        /**
         * All decorations added through this call will get the ownerId of this editor.
         * @see IModel.deltaDecorations
         */
        deltaDecorations(oldDecorations: string[], newDecorations: IModelDeltaDecoration[]): string[];
        /**
         * Get the layout info for the editor.
         */
        getLayoutInfo(): EditorLayoutInfo;
    }

    export interface ICommonDiffEditor extends IEditor {
        /**
         * An event emitted when the diff information computed by this diff editor has been updated.
         * @event
         */
        onDidUpdateDiff(listener: () => void): IDisposable;
        /**
         * Saves current view state of the editor in a serializable object.
         */
        saveViewState(): IDiffEditorViewState;
        /**
         * Restores the view state of the editor from a serializable object generated by `saveViewState`.
         */
        restoreViewState(state: IDiffEditorViewState): void;
        /**
         * Type the getModel() of IEditor.
         */
        getModel(): IDiffEditorModel;
        /**
         * Get the `original` editor.
         */
        getOriginalEditor(): ICommonCodeEditor;
        /**
         * Get the `modified` editor.
         */
        getModifiedEditor(): ICommonCodeEditor;
        /**
         * Get the computed diff information.
         */
        getLineChanges(): ILineChange[];
        /**
         * Get information based on computed diff about a line number from the original model.
         * If the diff computation is not finished or the model is missing, will return null.
         */
        getDiffLineInformationForOriginal(lineNumber: number): IDiffLineInformation;
        /**
         * Get information based on computed diff about a line number from the modified model.
         * If the diff computation is not finished or the model is missing, will return null.
         */
        getDiffLineInformationForModified(lineNumber: number): IDiffLineInformation;
        /**
         * @see ICodeEditor.getValue
         */
        getValue(options?: {
            preserveBOM: boolean;
            lineEnding: string;
        }): string;
    }

    /**
     * The type of the `IEditor`.
     */
    export var EditorType: {
        ICodeEditor: string;
        IDiffEditor: string;
    };

    /**
     * An event describing that the current mode associated with a model has changed.
     */
    export interface IModelLanguageChangedEvent {
        /**
         * Previous language
         */
        readonly oldLanguage: string;
        /**
         * New language
         */
        readonly newLanguage: string;
    }

    export interface IModelContentChange {
        /**
         * The range that got replaced.
         */
        readonly range: IRange;
        /**
         * The length of the range that got replaced.
         */
        readonly rangeLength: number;
        /**
         * The new text for the range.
         */
        readonly text: string;
    }

    /**
     * An event describing a change in the text of a model.
     */
    export interface IModelContentChangedEvent {
        readonly changes: IModelContentChange[];
        /**
         * The (new) end-of-line character.
         */
        readonly eol: string;
        /**
         * The new version id the model has transitioned to.
         */
        readonly versionId: number;
        /**
         * Flag that indicates that this event was generated while undoing.
         */
        readonly isUndoing: boolean;
        /**
         * Flag that indicates that this event was generated while redoing.
         */
        readonly isRedoing: boolean;
        /**
         * Flag that indicates that all decorations were lost with this edit.
         * The model has been reset to a new value.
         */
        readonly isFlush: boolean;
    }

    /**
     * An event describing that model decorations have changed.
     */
    export interface IModelDecorationsChangedEvent {
        /**
         * Lists of ids for added decorations.
         */
        readonly addedDecorations: string[];
        /**
         * Lists of ids for changed decorations.
         */
        readonly changedDecorations: string[];
        /**
         * List of ids for removed decorations.
         */
        readonly removedDecorations: string[];
    }

    /**
     * An event describing that some ranges of lines have been tokenized (their tokens have changed).
     */
    export interface IModelTokensChangedEvent {
        readonly ranges: {
            /**
             * The start of the range (inclusive)
             */
            readonly fromLineNumber: number;
            /**
             * The end of the range (inclusive)
             */
            readonly toLineNumber: number;
        }[];
    }

    export interface IModelOptionsChangedEvent {
        readonly tabSize: boolean;
        readonly insertSpaces: boolean;
        readonly trimAutoWhitespace: boolean;
    }

    /**
     * Describes the reason the cursor has changed its position.
     */
    export enum CursorChangeReason {
        /**
         * Unknown or not set.
         */
        NotSet = 0,
        /**
         * A `model.setValue()` was called.
         */
        ContentFlush = 1,
        /**
         * The `model` has been changed outside of this cursor and the cursor recovers its position from associated markers.
         */
        RecoverFromMarkers = 2,
        /**
         * There was an explicit user gesture.
         */
        Explicit = 3,
        /**
         * There was a Paste.
         */
        Paste = 4,
        /**
         * There was an Undo.
         */
        Undo = 5,
        /**
         * There was a Redo.
         */
        Redo = 6,
    }

    /**
     * An event describing that the cursor position has changed.
     */
    export interface ICursorPositionChangedEvent {
        /**
         * Primary cursor's position.
         */
        readonly position: Position;
        /**
         * Secondary cursors' position.
         */
        readonly secondaryPositions: Position[];
        /**
         * Reason.
         */
        readonly reason: CursorChangeReason;
        /**
         * Source of the call that caused the event.
         */
        readonly source: string;
    }

    /**
     * An event describing that the cursor selection has changed.
     */
    export interface ICursorSelectionChangedEvent {
        /**
         * The primary selection.
         */
        readonly selection: Selection;
        /**
         * The secondary selections.
         */
        readonly secondarySelections: Selection[];
        /**
         * Source of the call that caused the event.
         */
        readonly source: string;
        /**
         * Reason.
         */
        readonly reason: CursorChangeReason;
    }

    /**
     * Configuration options for editor scrollbars
     */
    export interface IEditorScrollbarOptions {
        /**
         * The size of arrows (if displayed).
         * Defaults to 11.
         */
        arrowSize?: number;
        /**
         * Render vertical scrollbar.
         * Accepted values: 'auto', 'visible', 'hidden'.
         * Defaults to 'auto'.
         */
        vertical?: string;
        /**
         * Render horizontal scrollbar.
         * Accepted values: 'auto', 'visible', 'hidden'.
         * Defaults to 'auto'.
         */
        horizontal?: string;
        /**
         * Cast horizontal and vertical shadows when the content is scrolled.
         * Defaults to true.
         */
        useShadows?: boolean;
        /**
         * Render arrows at the top and bottom of the vertical scrollbar.
         * Defaults to false.
         */
        verticalHasArrows?: boolean;
        /**
         * Render arrows at the left and right of the horizontal scrollbar.
         * Defaults to false.
         */
        horizontalHasArrows?: boolean;
        /**
         * Listen to mouse wheel events and react to them by scrolling.
         * Defaults to true.
         */
        handleMouseWheel?: boolean;
        /**
         * Height in pixels for the horizontal scrollbar.
         * Defaults to 10 (px).
         */
        horizontalScrollbarSize?: number;
        /**
         * Width in pixels for the vertical scrollbar.
         * Defaults to 10 (px).
         */
        verticalScrollbarSize?: number;
        /**
         * Width in pixels for the vertical slider.
         * Defaults to `verticalScrollbarSize`.
         */
        verticalSliderSize?: number;
        /**
         * Height in pixels for the horizontal slider.
         * Defaults to `horizontalScrollbarSize`.
         */
        horizontalSliderSize?: number;
    }

    /**
     * Configuration options for editor find widget
     */
    export interface IEditorFindOptions {
        /**
         * Controls if we seed search string in the Find Widget with editor selection.
         */
        seedSearchStringFromSelection?: boolean;
        /**
         * Controls if Find in Selection flag is turned on when multiple lines of text are selected in the editor.
         */
        autoFindInSelection: boolean;
    }

    /**
     * Configuration options for editor minimap
     */
    export interface IEditorMinimapOptions {
        /**
         * Enable the rendering of the minimap.
         * Defaults to false.
         */
        enabled?: boolean;
        /**
         * Control the rendering of the minimap slider.
         * Defaults to 'mouseover'.
         */
        showSlider?: 'always' | 'mouseover';
        /**
         * Render the actual text on a line (as opposed to color blocks).
         * Defaults to true.
         */
        renderCharacters?: boolean;
        /**
         * Limit the width of the minimap to render at most a certain number of columns.
         * Defaults to 120.
         */
        maxColumn?: number;
    }

    /**
     * Configuration options for the editor.
     */
    export interface IEditorOptions {
        /**
         * The aria label for the editor's textarea (when it is focused).
         */
        ariaLabel?: string;
        /**
         * Render vertical lines at the specified columns.
         * Defaults to empty array.
         */
        rulers?: number[];
        /**
         * A string containing the word separators used when doing word navigation.
         * Defaults to `~!@#$%^&*()-=+[{]}\\|;:\'",.<>/?
         */
        wordSeparators?: string;
        /**
         * Enable Linux primary clipboard.
         * Defaults to true.
         */
        selectionClipboard?: boolean;
        /**
         * Control the rendering of line numbers.
         * If it is a function, it will be invoked when rendering a line number and the return value will be rendered.
         * Otherwise, if it is a truey, line numbers will be rendered normally (equivalent of using an identity function).
         * Otherwise, line numbers will not be rendered.
         * Defaults to true.
         */
        lineNumbers?: 'on' | 'off' | 'relative' | ((lineNumber: number) => string);
        /**
         * Should the corresponding line be selected when clicking on the line number?
         * Defaults to true.
         */
        selectOnLineNumbers?: boolean;
        /**
         * Control the width of line numbers, by reserving horizontal space for rendering at least an amount of digits.
         * Defaults to 5.
         */
        lineNumbersMinChars?: number;
        /**
         * Enable the rendering of the glyph margin.
         * Defaults to true in vscode and to false in monaco-editor.
         */
        glyphMargin?: boolean;
        /**
         * The width reserved for line decorations (in px).
         * Line decorations are placed between line numbers and the editor content.
         * You can pass in a string in the format floating point followed by "ch". e.g. 1.3ch.
         * Defaults to 10.
         */
        lineDecorationsWidth?: number | string;
        /**
         * When revealing the cursor, a virtual padding (px) is added to the cursor, turning it into a rectangle.
         * This virtual padding ensures that the cursor gets revealed before hitting the edge of the viewport.
         * Defaults to 30 (px).
         */
        revealHorizontalRightPadding?: number;
        /**
         * Render the editor selection with rounded borders.
         * Defaults to true.
         */
        roundedSelection?: boolean;
        /**
         * Class name to be added to the editor.
         */
        extraEditorClassName?: string;
        /**
         * Should the editor be read only.
         * Defaults to false.
         */
        readOnly?: boolean;
        /**
         * Control the behavior and rendering of the scrollbars.
         */
        scrollbar?: IEditorScrollbarOptions;
        /**
         * Control the behavior and rendering of the minimap.
         */
        minimap?: IEditorMinimapOptions;
        /**
         * Control the behavior of the find widget.
         */
        find?: IEditorFindOptions;
        /**
         * Display overflow widgets as `fixed`.
         * Defaults to `false`.
         */
        fixedOverflowWidgets?: boolean;
        /**
         * The number of vertical lanes the overview ruler should render.
         * Defaults to 2.
         */
        overviewRulerLanes?: number;
        /**
         * Controls if a border should be drawn around the overview ruler.
         * Defaults to `true`.
         */
        overviewRulerBorder?: boolean;
        /**
         * Control the cursor animation style, possible values are 'blink', 'smooth', 'phase', 'expand' and 'solid'.
         * Defaults to 'blink'.
         */
        cursorBlinking?: string;
        /**
         * Zoom the font in the editor when using the mouse wheel in combination with holding Ctrl.
         * Defaults to false.
         */
        mouseWheelZoom?: boolean;
        /**
         * Control the cursor style, either 'block' or 'line'.
         * Defaults to 'line'.
         */
        cursorStyle?: string;
        /**
         * Enable font ligatures.
         * Defaults to false.
         */
        fontLigatures?: boolean;
        /**
         * Disable the use of `will-change` for the editor margin and lines layers.
         * The usage of `will-change` acts as a hint for browsers to create an extra layer.
         * Defaults to false.
         */
        disableLayerHinting?: boolean;
        /**
         * Disable the optimizations for monospace fonts.
         * Defaults to false.
         */
        disableMonospaceOptimizations?: boolean;
        /**
         * Should the cursor be hidden in the overview ruler.
         * Defaults to false.
         */
        hideCursorInOverviewRuler?: boolean;
        /**
         * Enable that scrolling can go one screen size after the last line.
         * Defaults to true.
         */
        scrollBeyondLastLine?: boolean;
        /**
         * Enable that the editor will install an interval to check if its container dom node size has changed.
         * Enabling this might have a severe performance impact.
         * Defaults to false.
         */
        automaticLayout?: boolean;
        /**
         * Control the wrapping of the editor.
         * When `wordWrap` = "off", the lines will never wrap.
         * When `wordWrap` = "on", the lines will wrap at the viewport width.
         * When `wordWrap` = "wordWrapColumn", the lines will wrap at `wordWrapColumn`.
         * When `wordWrap` = "bounded", the lines will wrap at min(viewport width, wordWrapColumn).
         * Defaults to "off".
         */
        wordWrap?: 'off' | 'on' | 'wordWrapColumn' | 'bounded';
        /**
         * Control the wrapping of the editor.
         * When `wordWrap` = "off", the lines will never wrap.
         * When `wordWrap` = "on", the lines will wrap at the viewport width.
         * When `wordWrap` = "wordWrapColumn", the lines will wrap at `wordWrapColumn`.
         * When `wordWrap` = "bounded", the lines will wrap at min(viewport width, wordWrapColumn).
         * Defaults to 80.
         */
        wordWrapColumn?: number;
        /**
         * Force word wrapping when the text appears to be of a minified/generated file.
         * Defaults to true.
         */
        wordWrapMinified?: boolean;
        /**
         * Control indentation of wrapped lines. Can be: 'none', 'same' or 'indent'.
         * Defaults to 'same' in vscode and to 'none' in monaco-editor.
         */
        wrappingIndent?: string;
        /**
         * Configure word wrapping characters. A break will be introduced before these characters.
         * Defaults to '{([+'.
         */
        wordWrapBreakBeforeCharacters?: string;
        /**
         * Configure word wrapping characters. A break will be introduced after these characters.
         * Defaults to ' \t})]?|&,;'.
         */
        wordWrapBreakAfterCharacters?: string;
        /**
         * Configure word wrapping characters. A break will be introduced after these characters only if no `wordWrapBreakBeforeCharacters` or `wordWrapBreakAfterCharacters` were found.
         * Defaults to '.'.
         */
        wordWrapBreakObtrusiveCharacters?: string;
        /**
         * Performance guard: Stop rendering a line after x characters.
         * Defaults to 10000.
         * Use -1 to never stop rendering
         */
        stopRenderingLineAfter?: number;
        /**
         * Enable hover.
         * Defaults to true.
         */
        hover?: boolean;
        /**
         * Enable detecting links and making them clickable.
         * Defaults to true.
         */
        links?: boolean;
        /**
         * Enable custom contextmenu.
         * Defaults to true.
         */
        contextmenu?: boolean;
        /**
         * A multiplier to be used on the `deltaX` and `deltaY` of mouse wheel scroll events.
         * Defaults to 1.
         */
        mouseWheelScrollSensitivity?: number;
        /**
         * The modifier to be used to add multiple cursors with the mouse.
         * Defaults to 'alt'
         */
        multiCursorModifier?: 'ctrlCmd' | 'alt';
        /**
         * Configure the editor's accessibility support.
         * Defaults to 'auto'. It is best to leave this to 'auto'.
         */
        accessibilitySupport?: 'auto' | 'off' | 'on';
        /**
         * Enable quick suggestions (shadow suggestions)
         * Defaults to true.
         */
        quickSuggestions?: boolean | {
            other: boolean;
            comments: boolean;
            strings: boolean;
        };
        /**
         * Quick suggestions show delay (in ms)
         * Defaults to 500 (ms)
         */
        quickSuggestionsDelay?: number;
        /**
         * Enables parameter hints
         */
        parameterHints?: boolean;
        /**
         * Render icons in suggestions box.
         * Defaults to true.
         */
        iconsInSuggestions?: boolean;
        /**
         * Enable auto closing brackets.
         * Defaults to true.
         */
        autoClosingBrackets?: boolean;
        /**
         * Enable auto indentation adjustment.
         * Defaults to false.
         */
        autoIndent?: boolean;
        /**
         * Enable format on type.
         * Defaults to false.
         */
        formatOnType?: boolean;
        /**
         * Enable format on paste.
         * Defaults to false.
         */
        formatOnPaste?: boolean;
        /**
         * Controls if the editor should allow to move selections via drag and drop.
         * Defaults to false.
         */
        dragAndDrop?: boolean;
        /**
         * Enable the suggestion box to pop-up on trigger characters.
         * Defaults to true.
         */
        suggestOnTriggerCharacters?: boolean;
        /**
         * Accept suggestions on ENTER.
         * Defaults to 'on'.
         */
        acceptSuggestionOnEnter?: 'on' | 'smart' | 'off';
        /**
         * Accept suggestions on provider defined characters.
         * Defaults to true.
         */
        acceptSuggestionOnCommitCharacter?: boolean;
        /**
         * Enable snippet suggestions. Default to 'true'.
         */
        snippetSuggestions?: 'top' | 'bottom' | 'inline' | 'none';
        /**
         * Copying without a selection copies the current line.
         */
        emptySelectionClipboard?: boolean;
        /**
         * Enable word based suggestions. Defaults to 'true'
         */
        wordBasedSuggestions?: boolean;
        /**
         * The font size for the suggest widget.
         * Defaults to the editor font size.
         */
        suggestFontSize?: number;
        /**
         * The line height for the suggest widget.
         * Defaults to the editor line height.
         */
        suggestLineHeight?: number;
        /**
         * Enable selection highlight.
         * Defaults to true.
         */
        selectionHighlight?: boolean;
        /**
         * Enable semantic occurrences highlight.
         * Defaults to true.
         */
        occurrencesHighlight?: boolean;
        /**
         * Show code lens
         * Defaults to true.
         */
        codeLens?: boolean;
        /**
         * Enable code folding
         * Defaults to true in vscode and to false in monaco-editor.
         */
        folding?: boolean;
        /**
         * Controls whether the fold actions in the gutter stay always visible or hide unless the mouse is over the gutter.
         * Defaults to 'mouseover'.
         */
        showFoldingControls?: 'always' | 'mouseover';
        /**
         * Enable highlighting of matching brackets.
         * Defaults to true.
         */
        matchBrackets?: boolean;
        /**
         * Enable rendering of whitespace.
         * Defaults to none.
         */
        renderWhitespace?: 'none' | 'boundary' | 'all';
        /**
         * Enable rendering of control characters.
         * Defaults to false.
         */
        renderControlCharacters?: boolean;
        /**
         * Enable rendering of indent guides.
         * Defaults to false.
         */
        renderIndentGuides?: boolean;
        /**
         * Enable rendering of current line highlight.
         * Defaults to all.
         */
        renderLineHighlight?: 'none' | 'gutter' | 'line' | 'all';
        /**
         * Inserting and deleting whitespace follows tab stops.
         */
        useTabStops?: boolean;
        /**
         * The font family
         */
        fontFamily?: string;
        /**
         * The font weight
         */
        fontWeight?: 'normal' | 'bold' | 'bolder' | 'lighter' | 'initial' | 'inherit' | '100' | '200' | '300' | '400' | '500' | '600' | '700' | '800' | '900';
        /**
         * The font size
         */
        fontSize?: number;
        /**
         * The line height
         */
        lineHeight?: number;
        /**
         * The letter spacing
         */
        letterSpacing?: number;
    }

    /**
     * Configuration options for the diff editor.
     */
    export interface IDiffEditorOptions extends IEditorOptions {
        /**
         * Allow the user to resize the diff editor split view.
         * Defaults to true.
         */
        enableSplitViewResizing?: boolean;
        /**
         * Render the differences in two side-by-side editors.
         * Defaults to true.
         */
        renderSideBySide?: boolean;
        /**
         * Compute the diff by ignoring leading/trailing whitespace
         * Defaults to true.
         */
        ignoreTrimWhitespace?: boolean;
        /**
         * Render +/- indicators for added/deleted changes.
         * Defaults to true.
         */
        renderIndicators?: boolean;
        /**
         * Original model should be editable?
         * Defaults to false.
         */
        originalEditable?: boolean;
    }

    export enum RenderMinimap {
        None = 0,
        Small = 1,
        Large = 2,
        SmallBlocks = 3,
        LargeBlocks = 4,
    }

    /**
     * Describes how to indent wrapped lines.
     */
    export enum WrappingIndent {
        /**
         * No indentation => wrapped lines begin at column 1.
         */
        None = 0,
        /**
         * Same => wrapped lines get the same indentation as the parent.
         */
        Same = 1,
        /**
         * Indent => wrapped lines get +1 indentation as the parent.
         */
        Indent = 2,
    }

    /**
     * The kind of animation in which the editor's cursor should be rendered.
     */
    export enum TextEditorCursorBlinkingStyle {
        /**
         * Hidden
         */
        Hidden = 0,
        /**
         * Blinking
         */
        Blink = 1,
        /**
         * Blinking with smooth fading
         */
        Smooth = 2,
        /**
         * Blinking with prolonged filled state and smooth fading
         */
        Phase = 3,
        /**
         * Expand collapse animation on the y axis
         */
        Expand = 4,
        /**
         * No-Blinking
         */
        Solid = 5,
    }

    /**
     * The style in which the editor's cursor should be rendered.
     */
    export enum TextEditorCursorStyle {
        /**
         * As a vertical line (sitting between two characters).
         */
        Line = 1,
        /**
         * As a block (sitting on top of a character).
         */
        Block = 2,
        /**
         * As a horizontal line (sitting under a character).
         */
        Underline = 3,
        /**
         * As a thin vertical line (sitting between two characters).
         */
        LineThin = 4,
        /**
         * As an outlined block (sitting on top of a character).
         */
        BlockOutline = 5,
        /**
         * As a thin horizontal line (sitting under a character).
         */
        UnderlineThin = 6,
    }

    export interface InternalEditorScrollbarOptions {
        readonly arrowSize: number;
        readonly vertical: ScrollbarVisibility;
        readonly horizontal: ScrollbarVisibility;
        readonly useShadows: boolean;
        readonly verticalHasArrows: boolean;
        readonly horizontalHasArrows: boolean;
        readonly handleMouseWheel: boolean;
        readonly horizontalScrollbarSize: number;
        readonly horizontalSliderSize: number;
        readonly verticalScrollbarSize: number;
        readonly verticalSliderSize: number;
        readonly mouseWheelScrollSensitivity: number;
    }

    export interface InternalEditorMinimapOptions {
        readonly enabled: boolean;
        readonly showSlider: 'always' | 'mouseover';
        readonly renderCharacters: boolean;
        readonly maxColumn: number;
    }

    export interface InternalEditorFindOptions {
        readonly seedSearchStringFromSelection: boolean;
        readonly autoFindInSelection: boolean;
    }

    export interface EditorWrappingInfo {
        readonly inDiffEditor: boolean;
        readonly isDominatedByLongLines: boolean;
        readonly isWordWrapMinified: boolean;
        readonly isViewportWrapping: boolean;
        readonly wrappingColumn: number;
        readonly wrappingIndent: WrappingIndent;
        readonly wordWrapBreakBeforeCharacters: string;
        readonly wordWrapBreakAfterCharacters: string;
        readonly wordWrapBreakObtrusiveCharacters: string;
    }

    export interface InternalEditorViewOptions {
        readonly extraEditorClassName: string;
        readonly disableMonospaceOptimizations: boolean;
        readonly rulers: number[];
        readonly ariaLabel: string;
        readonly renderLineNumbers: boolean;
        readonly renderCustomLineNumbers: (lineNumber: number) => string;
        readonly renderRelativeLineNumbers: boolean;
        readonly selectOnLineNumbers: boolean;
        readonly glyphMargin: boolean;
        readonly revealHorizontalRightPadding: number;
        readonly roundedSelection: boolean;
        readonly overviewRulerLanes: number;
        readonly overviewRulerBorder: boolean;
        readonly cursorBlinking: TextEditorCursorBlinkingStyle;
        readonly mouseWheelZoom: boolean;
        readonly cursorStyle: TextEditorCursorStyle;
        readonly hideCursorInOverviewRuler: boolean;
        readonly scrollBeyondLastLine: boolean;
        readonly stopRenderingLineAfter: number;
        readonly renderWhitespace: 'none' | 'boundary' | 'all';
        readonly renderControlCharacters: boolean;
        readonly fontLigatures: boolean;
        readonly renderIndentGuides: boolean;
        readonly renderLineHighlight: 'none' | 'gutter' | 'line' | 'all';
        readonly scrollbar: InternalEditorScrollbarOptions;
        readonly minimap: InternalEditorMinimapOptions;
        readonly fixedOverflowWidgets: boolean;
    }

    export interface EditorContribOptions {
        readonly selectionClipboard: boolean;
        readonly hover: boolean;
        readonly links: boolean;
        readonly contextmenu: boolean;
        readonly quickSuggestions: boolean | {
            other: boolean;
            comments: boolean;
            strings: boolean;
        };
        readonly quickSuggestionsDelay: number;
        readonly parameterHints: boolean;
        readonly iconsInSuggestions: boolean;
        readonly formatOnType: boolean;
        readonly formatOnPaste: boolean;
        readonly suggestOnTriggerCharacters: boolean;
        readonly acceptSuggestionOnEnter: 'on' | 'smart' | 'off';
        readonly acceptSuggestionOnCommitCharacter: boolean;
        readonly snippetSuggestions: 'top' | 'bottom' | 'inline' | 'none';
        readonly wordBasedSuggestions: boolean;
        readonly suggestFontSize: number;
        readonly suggestLineHeight: number;
        readonly selectionHighlight: boolean;
        readonly occurrencesHighlight: boolean;
        readonly codeLens: boolean;
        readonly folding: boolean;
        readonly showFoldingControls: 'always' | 'mouseover';
        readonly matchBrackets: boolean;
        readonly find: InternalEditorFindOptions;
    }

    /**
     * Internal configuration options (transformed or computed) for the editor.
     */
    export class InternalEditorOptions {
        readonly _internalEditorOptionsBrand: void;
        readonly canUseLayerHinting: boolean;
        readonly pixelRatio: number;
        readonly editorClassName: string;
        readonly lineHeight: number;
        readonly readOnly: boolean;
        readonly multiCursorModifier: 'altKey' | 'ctrlKey' | 'metaKey';
        readonly wordSeparators: string;
        readonly autoClosingBrackets: boolean;
        readonly autoIndent: boolean;
        readonly useTabStops: boolean;
        readonly tabFocusMode: boolean;
        readonly dragAndDrop: boolean;
        readonly emptySelectionClipboard: boolean;
        readonly layoutInfo: EditorLayoutInfo;
        readonly fontInfo: FontInfo;
        readonly viewInfo: InternalEditorViewOptions;
        readonly wrappingInfo: EditorWrappingInfo;
        readonly contribInfo: EditorContribOptions;
    }

    /**
     * A description for the overview ruler position.
     */
    export interface OverviewRulerPosition {
        /**
         * Width of the overview ruler
         */
        readonly width: number;
        /**
         * Height of the overview ruler
         */
        readonly height: number;
        /**
         * Top position for the overview ruler
         */
        readonly top: number;
        /**
         * Right position for the overview ruler
         */
        readonly right: number;
    }

    /**
     * The internal layout details of the editor.
     */
    export interface EditorLayoutInfo {
        /**
         * Full editor width.
         */
        readonly width: number;
        /**
         * Full editor height.
         */
        readonly height: number;
        /**
         * Left position for the glyph margin.
         */
        readonly glyphMarginLeft: number;
        /**
         * The width of the glyph margin.
         */
        readonly glyphMarginWidth: number;
        /**
         * The height of the glyph margin.
         */
        readonly glyphMarginHeight: number;
        /**
         * Left position for the line numbers.
         */
        readonly lineNumbersLeft: number;
        /**
         * The width of the line numbers.
         */
        readonly lineNumbersWidth: number;
        /**
         * The height of the line numbers.
         */
        readonly lineNumbersHeight: number;
        /**
         * Left position for the line decorations.
         */
        readonly decorationsLeft: number;
        /**
         * The width of the line decorations.
         */
        readonly decorationsWidth: number;
        /**
         * The height of the line decorations.
         */
        readonly decorationsHeight: number;
        /**
         * Left position for the content (actual text)
         */
        readonly contentLeft: number;
        /**
         * The width of the content (actual text)
         */
        readonly contentWidth: number;
        /**
         * The height of the content (actual height)
         */
        readonly contentHeight: number;
        /**
         * The width of the minimap
         */
        readonly minimapWidth: number;
        /**
         * Minimap render type
         */
        readonly renderMinimap: RenderMinimap;
        /**
         * The number of columns (of typical characters) fitting on a viewport line.
         */
        readonly viewportColumn: number;
        /**
         * The width of the vertical scrollbar.
         */
        readonly verticalScrollbarWidth: number;
        /**
         * The height of the horizontal scrollbar.
         */
        readonly horizontalScrollbarHeight: number;
        /**
         * The position of the overview ruler.
         */
        readonly overviewRuler: OverviewRulerPosition;
    }

    /**
     * An event describing that the configuration of the editor has changed.
     */
    export interface IConfigurationChangedEvent {
        readonly canUseLayerHinting: boolean;
        readonly pixelRatio: boolean;
        readonly editorClassName: boolean;
        readonly lineHeight: boolean;
        readonly readOnly: boolean;
        readonly accessibilitySupport: boolean;
        readonly multiCursorModifier: boolean;
        readonly wordSeparators: boolean;
        readonly autoClosingBrackets: boolean;
        readonly autoIndent: boolean;
        readonly useTabStops: boolean;
        readonly tabFocusMode: boolean;
        readonly dragAndDrop: boolean;
        readonly emptySelectionClipboard: boolean;
        readonly layoutInfo: boolean;
        readonly fontInfo: boolean;
        readonly viewInfo: boolean;
        readonly wrappingInfo: boolean;
        readonly contribInfo: boolean;
    }

    /**
     * A view zone is a full horizontal rectangle that 'pushes' text down.
     * The editor reserves space for view zones when rendering.
     */
    export interface IViewZone {
        /**
         * The line number after which this zone should appear.
         * Use 0 to place a view zone before the first line number.
         */
        afterLineNumber: number;
        /**
         * The column after which this zone should appear.
         * If not set, the maxLineColumn of `afterLineNumber` will be used.
         */
        afterColumn?: number;
        /**
         * Suppress mouse down events.
         * If set, the editor will attach a mouse down listener to the view zone and .preventDefault on it.
         * Defaults to false
         */
        suppressMouseDown?: boolean;
        /**
         * The height in lines of the view zone.
         * If specified, `heightInPx` will be used instead of this.
         * If neither `heightInPx` nor `heightInLines` is specified, a default of `heightInLines` = 1 will be chosen.
         */
        heightInLines?: number;
        /**
         * The height in px of the view zone.
         * If this is set, the editor will give preference to it rather than `heightInLines` above.
         * If neither `heightInPx` nor `heightInLines` is specified, a default of `heightInLines` = 1 will be chosen.
         */
        heightInPx?: number;
        /**
         * The dom node of the view zone
         */
        domNode: HTMLElement;
        /**
         * An optional dom node for the view zone that will be placed in the margin area.
         */
        marginDomNode?: HTMLElement;
        /**
         * Callback which gives the relative top of the view zone as it appears (taking scrolling into account).
         */
        onDomNodeTop?: (top: number) => void;
        /**
         * Callback which gives the height in pixels of the view zone.
         */
        onComputedHeight?: (height: number) => void;
    }

    /**
     * An accessor that allows for zones to be added or removed.
     */
    export interface IViewZoneChangeAccessor {
        /**
         * Create a new view zone.
         * @param zone Zone to create
         * @return A unique identifier to the view zone.
         */
        addZone(zone: IViewZone): number;
        /**
         * Remove a zone
         * @param id A unique identifier to the view zone, as returned by the `addZone` call.
         */
        removeZone(id: number): void;
        /**
         * Change a zone's position.
         * The editor will rescan the `afterLineNumber` and `afterColumn` properties of a view zone.
         */
        layoutZone(id: number): void;
    }

    /**
     * A positioning preference for rendering content widgets.
     */
    export enum ContentWidgetPositionPreference {
        /**
         * Place the content widget exactly at a position
         */
        EXACT = 0,
        /**
         * Place the content widget above a position
         */
        ABOVE = 1,
        /**
         * Place the content widget below a position
         */
        BELOW = 2,
    }

    /**
     * A position for rendering content widgets.
     */
    export interface IContentWidgetPosition {
        /**
         * Desired position for the content widget.
         * `preference` will also affect the placement.
         */
        position: IPosition;
        /**
         * Placement preference for position, in order of preference.
         */
        preference: ContentWidgetPositionPreference[];
    }

    /**
     * A content widget renders inline with the text and can be easily placed 'near' an editor position.
     */
    export interface IContentWidget {
        /**
         * Render this content widget in a location where it could overflow the editor's view dom node.
         */
        allowEditorOverflow?: boolean;
        suppressMouseDown?: boolean;
        /**
         * Get a unique identifier of the content widget.
         */
        getId(): string;
        /**
         * Get the dom node of the content widget.
         */
        getDomNode(): HTMLElement;
        /**
         * Get the placement of the content widget.
         * If null is returned, the content widget will be placed off screen.
         */
        getPosition(): IContentWidgetPosition;
    }

    /**
     * A positioning preference for rendering overlay widgets.
     */
    export enum OverlayWidgetPositionPreference {
        /**
         * Position the overlay widget in the top right corner
         */
        TOP_RIGHT_CORNER = 0,
        /**
         * Position the overlay widget in the bottom right corner
         */
        BOTTOM_RIGHT_CORNER = 1,
        /**
         * Position the overlay widget in the top center
         */
        TOP_CENTER = 2,
    }

    /**
     * A position for rendering overlay widgets.
     */
    export interface IOverlayWidgetPosition {
        /**
         * The position preference for the overlay widget.
         */
        preference: OverlayWidgetPositionPreference;
    }

    /**
     * An overlay widgets renders on top of the text.
     */
    export interface IOverlayWidget {
        /**
         * Get a unique identifier of the overlay widget.
         */
        getId(): string;
        /**
         * Get the dom node of the overlay widget.
         */
        getDomNode(): HTMLElement;
        /**
         * Get the placement of the overlay widget.
         * If null is returned, the overlay widget is responsible to place itself.
         */
        getPosition(): IOverlayWidgetPosition;
    }

    /**
     * Type of hit element with the mouse in the editor.
     */
    export enum MouseTargetType {
        /**
         * Mouse is on top of an unknown element.
         */
        UNKNOWN = 0,
        /**
         * Mouse is on top of the textarea used for input.
         */
        TEXTAREA = 1,
        /**
         * Mouse is on top of the glyph margin
         */
        GUTTER_GLYPH_MARGIN = 2,
        /**
         * Mouse is on top of the line numbers
         */
        GUTTER_LINE_NUMBERS = 3,
        /**
         * Mouse is on top of the line decorations
         */
        GUTTER_LINE_DECORATIONS = 4,
        /**
         * Mouse is on top of the whitespace left in the gutter by a view zone.
         */
        GUTTER_VIEW_ZONE = 5,
        /**
         * Mouse is on top of text in the content.
         */
        CONTENT_TEXT = 6,
        /**
         * Mouse is on top of empty space in the content (e.g. after line text or below last line)
         */
        CONTENT_EMPTY = 7,
        /**
         * Mouse is on top of a view zone in the content.
         */
        CONTENT_VIEW_ZONE = 8,
        /**
         * Mouse is on top of a content widget.
         */
        CONTENT_WIDGET = 9,
        /**
         * Mouse is on top of the decorations overview ruler.
         */
        OVERVIEW_RULER = 10,
        /**
         * Mouse is on top of a scrollbar.
         */
        SCROLLBAR = 11,
        /**
         * Mouse is on top of an overlay widget.
         */
        OVERLAY_WIDGET = 12,
        /**
         * Mouse is outside of the editor.
         */
        OUTSIDE_EDITOR = 13,
    }

    /**
     * Target hit with the mouse in the editor.
     */
    export interface IMouseTarget {
        /**
         * The target element
         */
        readonly element: Element;
        /**
         * The target type
         */
        readonly type: MouseTargetType;
        /**
         * The 'approximate' editor position
         */
        readonly position: Position;
        /**
         * Desired mouse column (e.g. when position.column gets clamped to text length -- clicking after text on a line).
         */
        readonly mouseColumn: number;
        /**
         * The 'approximate' editor range
         */
        readonly range: Range;
        /**
         * Some extra detail.
         */
        readonly detail: any;
    }

    /**
     * A mouse event originating from the editor.
     */
    export interface IEditorMouseEvent {
        readonly event: IMouseEvent;
        readonly target: IMouseTarget;
    }

    /**
     * A rich code editor.
     */
    export interface ICodeEditor extends ICommonCodeEditor {
        /**
         * An event emitted on a "mouseup".
         * @event
         */
        onMouseUp(listener: (e: IEditorMouseEvent) => void): IDisposable;
        /**
         * An event emitted on a "mousedown".
         * @event
         */
        onMouseDown(listener: (e: IEditorMouseEvent) => void): IDisposable;
        /**
         * An event emitted on a "contextmenu".
         * @event
         */
        onContextMenu(listener: (e: IEditorMouseEvent) => void): IDisposable;
        /**
         * An event emitted on a "mousemove".
         * @event
         */
        onMouseMove(listener: (e: IEditorMouseEvent) => void): IDisposable;
        /**
         * An event emitted on a "mouseleave".
         * @event
         */
        onMouseLeave(listener: (e: IEditorMouseEvent) => void): IDisposable;
        /**
         * An event emitted on a "keyup".
         * @event
         */
        onKeyUp(listener: (e: IKeyboardEvent) => void): IDisposable;
        /**
         * An event emitted on a "keydown".
         * @event
         */
        onKeyDown(listener: (e: IKeyboardEvent) => void): IDisposable;
        /**
         * An event emitted when the layout of the editor has changed.
         * @event
         */
        onDidLayoutChange(listener: (e: EditorLayoutInfo) => void): IDisposable;
        /**
         * An event emitted when the scroll in the editor has changed.
         * @event
         */
        onDidScrollChange(listener: (e: IScrollEvent) => void): IDisposable;
        /**
         * Returns the editor's dom node
         */
        getDomNode(): HTMLElement;
        /**
         * Add a content widget. Widgets must have unique ids, otherwise they will be overwritten.
         */
        addContentWidget(widget: IContentWidget): void;
        /**
         * Layout/Reposition a content widget. This is a ping to the editor to call widget.getPosition()
         * and update appropiately.
         */
        layoutContentWidget(widget: IContentWidget): void;
        /**
         * Remove a content widget.
         */
        removeContentWidget(widget: IContentWidget): void;
        /**
         * Add an overlay widget. Widgets must have unique ids, otherwise they will be overwritten.
         */
        addOverlayWidget(widget: IOverlayWidget): void;
        /**
         * Layout/Reposition an overlay widget. This is a ping to the editor to call widget.getPosition()
         * and update appropiately.
         */
        layoutOverlayWidget(widget: IOverlayWidget): void;
        /**
         * Remove an overlay widget.
         */
        removeOverlayWidget(widget: IOverlayWidget): void;
        /**
         * Change the view zones. View zones are lost when a new model is attached to the editor.
         */
        changeViewZones(callback: (accessor: IViewZoneChangeAccessor) => void): void;
        /**
         * Returns the range that is currently centered in the view port.
         */
        getCenteredRangeInViewport(): Range;
        /**
         * Get the horizontal position (left offset) for the column w.r.t to the beginning of the line.
         * This method works only if the line `lineNumber` is currently rendered (in the editor's viewport).
         * Use this method with caution.
         */
        getOffsetForColumn(lineNumber: number, column: number): number;
        /**
         * Force an editor render now.
         */
        render(): void;
        /**
         * Get the vertical position (top offset) for the line w.r.t. to the first line.
         */
        getTopForLineNumber(lineNumber: number): number;
        /**
         * Get the vertical position (top offset) for the position w.r.t. to the first line.
         */
        getTopForPosition(lineNumber: number, column: number): number;
        /**
         * Get the hit test target at coordinates `clientX` and `clientY`.
         * The coordinates are relative to the top-left of the viewport.
         *
         * @returns Hit test target or null if the coordinates fall outside the editor or the editor has no model.
         */
        getTargetAtClientPoint(clientX: number, clientY: number): IMouseTarget;
        /**
         * Get the visible position for `position`.
         * The result position takes scrolling into account and is relative to the top left corner of the editor.
         * Explanation 1: the results of this method will change for the same `position` if the user scrolls the editor.
         * Explanation 2: the results of this method will not change if the container of the editor gets repositioned.
         * Warning: the results of this method are innacurate for positions that are outside the current editor viewport.
         */
        getScrolledVisiblePosition(position: IPosition): {
            top: number;
            left: number;
            height: number;
        };
        /**
         * Apply the same font settings as the editor to `target`.
         */
        applyFontInfo(target: HTMLElement): void;
    }

    /**
     * A rich diff editor.
     */
    export interface IDiffEditor extends ICommonDiffEditor {
        /**
         * @see ICodeEditor.getDomNode
         */
        getDomNode(): HTMLElement;
    }

    export class FontInfo extends BareFontInfo {
        readonly _editorStylingBrand: void;
        readonly isTrusted: boolean;
        readonly isMonospace: boolean;
        readonly typicalHalfwidthCharacterWidth: number;
        readonly typicalFullwidthCharacterWidth: number;
        readonly spaceWidth: number;
        readonly maxDigitWidth: number;
    }
    export class BareFontInfo {
        readonly _bareFontInfoBrand: void;
        readonly zoomLevel: number;
        readonly fontFamily: string;
        readonly fontWeight: string;
        readonly fontSize: number;
        readonly lineHeight: number;
        readonly letterSpacing: number;
    }
}

declare module monaco.languages {


    /**
     * Register information about a new language.
     */
    export function register(language: ILanguageExtensionPoint): void;

    /**
     * Get the information of all the registered languages.
     */
    export function getLanguages(): ILanguageExtensionPoint[];

    /**
     * An event emitted when a language is first time needed (e.g. a model has it set).
     * @event
     */
    export function onLanguage(languageId: string, callback: () => void): IDisposable;

    /**
     * Set the editing configuration for a language.
     */
    export function setLanguageConfiguration(languageId: string, configuration: LanguageConfiguration): IDisposable;

    /**
     * A token.
     */
    export interface IToken {
        startIndex: number;
        scopes: string;
    }

    /**
     * The result of a line tokenization.
     */
    export interface ILineTokens {
        /**
         * The list of tokens on the line.
         */
        tokens: IToken[];
        /**
         * The tokenization end state.
         * A pointer will be held to this and the object should not be modified by the tokenizer after the pointer is returned.
         */
        endState: IState;
    }

    /**
     * A "manual" provider of tokens.
     */
    export interface TokensProvider {
        /**
         * The initial state of a language. Will be the state passed in to tokenize the first line.
         */
        getInitialState(): IState;
        /**
         * Tokenize a line given the state at the beginning of the line.
         */
        tokenize(line: string, state: IState): ILineTokens;
    }

    /**
     * Set the tokens provider for a language (manual implementation).
     */
    export function setTokensProvider(languageId: string, provider: TokensProvider): IDisposable;

    /**
     * Set the tokens provider for a language (monarch implementation).
     */
    export function setMonarchTokensProvider(languageId: string, languageDef: IMonarchLanguage): IDisposable;

    /**
     * Register a reference provider (used by e.g. reference search).
     */
    export function registerReferenceProvider(languageId: string, provider: ReferenceProvider): IDisposable;

    /**
     * Register a rename provider (used by e.g. rename symbol).
     */
    export function registerRenameProvider(languageId: string, provider: RenameProvider): IDisposable;

    /**
     * Register a signature help provider (used by e.g. paremeter hints).
     */
    export function registerSignatureHelpProvider(languageId: string, provider: SignatureHelpProvider): IDisposable;

    /**
     * Register a hover provider (used by e.g. editor hover).
     */
    export function registerHoverProvider(languageId: string, provider: HoverProvider): IDisposable;

    /**
     * Register a document symbol provider (used by e.g. outline).
     */
    export function registerDocumentSymbolProvider(languageId: string, provider: DocumentSymbolProvider): IDisposable;

    /**
     * Register a document highlight provider (used by e.g. highlight occurrences).
     */
    export function registerDocumentHighlightProvider(languageId: string, provider: DocumentHighlightProvider): IDisposable;

    /**
     * Register a definition provider (used by e.g. go to definition).
     */
    export function registerDefinitionProvider(languageId: string, provider: DefinitionProvider): IDisposable;

    /**
     * Register a implementation provider (used by e.g. go to implementation).
     */
    export function registerImplementationProvider(languageId: string, provider: ImplementationProvider): IDisposable;

    /**
     * Register a type definition provider (used by e.g. go to type definition).
     */
    export function registerTypeDefinitionProvider(languageId: string, provider: TypeDefinitionProvider): IDisposable;

    /**
     * Register a code lens provider (used by e.g. inline code lenses).
     */
    export function registerCodeLensProvider(languageId: string, provider: CodeLensProvider): IDisposable;

    /**
     * Register a code action provider (used by e.g. quick fix).
     */
    export function registerCodeActionProvider(languageId: string, provider: CodeActionProvider): IDisposable;

    /**
     * Register a formatter that can handle only entire models.
     */
    export function registerDocumentFormattingEditProvider(languageId: string, provider: DocumentFormattingEditProvider): IDisposable;

    /**
     * Register a formatter that can handle a range inside a model.
     */
    export function registerDocumentRangeFormattingEditProvider(languageId: string, provider: DocumentRangeFormattingEditProvider): IDisposable;

    /**
     * Register a formatter than can do formatting as the user types.
     */
    export function registerOnTypeFormattingEditProvider(languageId: string, provider: OnTypeFormattingEditProvider): IDisposable;

    /**
     * Register a link provider that can find links in text.
     */
    export function registerLinkProvider(languageId: string, provider: LinkProvider): IDisposable;

    /**
     * Register a completion item provider (use by e.g. suggestions).
     */
    export function registerCompletionItemProvider(languageId: string, provider: CompletionItemProvider): IDisposable;

    /**
     * Contains additional diagnostic information about the context in which
     * a [code action](#CodeActionProvider.provideCodeActions) is run.
     */
    export interface CodeActionContext {
        /**
         * An array of diagnostics.
         *
         * @readonly
         */
        readonly markers: editor.IMarkerData[];
    }

    /**
     * The code action interface defines the contract between extensions and
     * the [light bulb](https://code.visualstudio.com/docs/editor/editingevolved#_code-action) feature.
     */
    export interface CodeActionProvider {
        /**
         * Provide commands for the given document and range.
         */
        provideCodeActions(model: editor.IReadOnlyModel, range: Range, context: CodeActionContext, token: CancellationToken): Command[] | Thenable<Command[]>;
    }

    /**
     * Completion item kinds.
     */
    export enum CompletionItemKind {
        Text = 0,
        Method = 1,
        Function = 2,
        Constructor = 3,
        Field = 4,
        Variable = 5,
        Class = 6,
        Interface = 7,
        Module = 8,
        Property = 9,
        Unit = 10,
        Value = 11,
        Enum = 12,
        Keyword = 13,
        Snippet = 14,
        Color = 15,
        File = 16,
        Reference = 17,
        Folder = 18,
    }

    /**
     * A snippet string is a template which allows to insert text
     * and to control the editor cursor when insertion happens.
     *
     * A snippet can define tab stops and placeholders with `$1`, `$2`
     * and `${3:foo}`. `$0` defines the final tab stop, it defaults to
     * the end of the snippet. Variables are defined with `$name` and
     * `${name:default value}`. The full snippet syntax is documented
     * [here](http://code.visualstudio.com/docs/editor/userdefinedsnippets#_creating-your-own-snippets).
     */
    export interface SnippetString {
        /**
         * The snippet string.
         */
        value: string;
    }

    /**
     * A completion item represents a text snippet that is
     * proposed to complete text that is being typed.
     */
    export interface CompletionItem {
        /**
         * The label of this completion item. By default
         * this is also the text that is inserted when selecting
         * this completion.
         */
        label: string;
        /**
         * The kind of this completion item. Based on the kind
         * an icon is chosen by the editor.
         */
        kind: CompletionItemKind;
        /**
         * A human-readable string with additional information
         * about this item, like type or symbol information.
         */
        detail?: string;
        /**
         * A human-readable string that represents a doc-comment.
         */
        documentation?: string;
        /**
         * A string that should be used when comparing this item
         * with other items. When `falsy` the [label](#CompletionItem.label)
         * is used.
         */
        sortText?: string;
        /**
         * A string that should be used when filtering a set of
         * completion items. When `falsy` the [label](#CompletionItem.label)
         * is used.
         */
        filterText?: string;
        /**
         * A string or snippet that should be inserted in a document when selecting
         * this completion. When `falsy` the [label](#CompletionItem.label)
         * is used.
         */
        insertText?: string | SnippetString;
        /**
         * A range of text that should be replaced by this completion item.
         *
         * Defaults to a range from the start of the [current word](#TextDocument.getWordRangeAtPosition) to the
         * current position.
         *
         * *Note:* The range must be a [single line](#Range.isSingleLine) and it must
         * [contain](#Range.contains) the position at which completion has been [requested](#CompletionItemProvider.provideCompletionItems).
         */
        range?: Range;
        /**
         * @deprecated **Deprecated** in favor of `CompletionItem.insertText` and `CompletionItem.range`.
         *
         * ~~An [edit](#TextEdit) which is applied to a document when selecting
         * this completion. When an edit is provided the value of
         * [insertText](#CompletionItem.insertText) is ignored.~~
         *
         * ~~The [range](#Range) of the edit must be single-line and on the same
         * line completions were [requested](#CompletionItemProvider.provideCompletionItems) at.~~
         */
        textEdit?: editor.ISingleEditOperation;
    }

    /**
     * Represents a collection of [completion items](#CompletionItem) to be presented
     * in the editor.
     */
    export interface CompletionList {
        /**
         * This list it not complete. Further typing should result in recomputing
         * this list.
         */
        isIncomplete?: boolean;
        /**
         * The completion items.
         */
        items: CompletionItem[];
    }

    /**
     * The completion item provider interface defines the contract between extensions and
     * the [IntelliSense](https://code.visualstudio.com/docs/editor/intellisense).
     *
     * When computing *complete* completion items is expensive, providers can optionally implement
     * the `resolveCompletionItem`-function. In that case it is enough to return completion
     * items with a [label](#CompletionItem.label) from the
     * [provideCompletionItems](#CompletionItemProvider.provideCompletionItems)-function. Subsequently,
     * when a completion item is shown in the UI and gains focus this provider is asked to resolve
     * the item, like adding [doc-comment](#CompletionItem.documentation) or [details](#CompletionItem.detail).
     */
    export interface CompletionItemProvider {
        triggerCharacters?: string[];
        /**
         * Provide completion items for the given position and document.
         */
        provideCompletionItems(model: editor.IReadOnlyModel, position: Position, token: CancellationToken): CompletionItem[] | Thenable<CompletionItem[]> | CompletionList | Thenable<CompletionList>;
        /**
         * Given a completion item fill in more data, like [doc-comment](#CompletionItem.documentation)
         * or [details](#CompletionItem.detail).
         *
         * The editor will only resolve a completion item once.
         */
        resolveCompletionItem?(item: CompletionItem, token: CancellationToken): CompletionItem | Thenable<CompletionItem>;
    }

    /**
     * Describes how comments for a language work.
     */
    export interface CommentRule {
        /**
         * The line comment token, like `// this is a comment`
         */
        lineComment?: string;
        /**
         * The block comment character pair, like `/* block comment *&#47;`
         */
        blockComment?: CharacterPair;
    }

    /**
     * The language configuration interface defines the contract between extensions and
     * various editor features, like automatic bracket insertion, automatic indentation etc.
     */
    export interface LanguageConfiguration {
        /**
         * The language's comment settings.
         */
        comments?: CommentRule;
        /**
         * The language's brackets.
         * This configuration implicitly affects pressing Enter around these brackets.
         */
        brackets?: CharacterPair[];
        /**
         * The language's word definition.
         * If the language supports Unicode identifiers (e.g. JavaScript), it is preferable
         * to provide a word definition that uses exclusion of known separators.
         * e.g.: A regex that matches anything except known separators (and dot is allowed to occur in a floating point number):
         *   /(-?\d*\.\d\w*)|([^\`\~\!\@\#\%\^\&\*\(\)\-\=\+\[\{\]\}\\\|\;\:\'\"\,\.\<\>\/\?\s]+)/g
         */
        wordPattern?: RegExp;
        /**
         * The language's indentation settings.
         */
        indentationRules?: IndentationRule;
        /**
         * The language's rules to be evaluated when pressing Enter.
         */
        onEnterRules?: OnEnterRule[];
        /**
         * The language's auto closing pairs. The 'close' character is automatically inserted with the
         * 'open' character is typed. If not set, the configured brackets will be used.
         */
        autoClosingPairs?: IAutoClosingPairConditional[];
        /**
         * The language's surrounding pairs. When the 'open' character is typed on a selection, the
         * selected string is surrounded by the open and close characters. If not set, the autoclosing pairs
         * settings will be used.
         */
        surroundingPairs?: IAutoClosingPair[];
        /**
         * **Deprecated** Do not use.
         *
         * @deprecated Will be replaced by a better API soon.
         */
        __electricCharacterSupport?: IBracketElectricCharacterContribution;
    }

    /**
     * Describes indentation rules for a language.
     */
    export interface IndentationRule {
        /**
         * If a line matches this pattern, then all the lines after it should be unindendented once (until another rule matches).
         */
        decreaseIndentPattern: RegExp;
        /**
         * If a line matches this pattern, then all the lines after it should be indented once (until another rule matches).
         */
        increaseIndentPattern: RegExp;
        /**
         * If a line matches this pattern, then **only the next line** after it should be indented once.
         */
        indentNextLinePattern?: RegExp;
        /**
         * If a line matches this pattern, then its indentation should not be changed and it should not be evaluated against the other rules.
         */
        unIndentedLinePattern?: RegExp;
    }

    /**
     * Describes a rule to be evaluated when pressing Enter.
     */
    export interface OnEnterRule {
        /**
         * This rule will only execute if the text before the cursor matches this regular expression.
         */
        beforeText: RegExp;
        /**
         * This rule will only execute if the text after the cursor matches this regular expression.
         */
        afterText?: RegExp;
        /**
         * The action to execute.
         */
        action: EnterAction;
    }

    export interface IBracketElectricCharacterContribution {
        docComment?: IDocComment;
    }

    /**
     * Definition of documentation comments (e.g. Javadoc/JSdoc)
     */
    export interface IDocComment {
        /**
         * The string that starts a doc comment (e.g. '/**')
         */
        open: string;
        /**
         * The string that appears on the last line and closes the doc comment (e.g. ' * /').
         */
        close: string;
    }

    /**
     * A tuple of two characters, like a pair of
     * opening and closing brackets.
     */
    export type CharacterPair = [string, string];

    export interface IAutoClosingPair {
        open: string;
        close: string;
    }

    export interface IAutoClosingPairConditional extends IAutoClosingPair {
        notIn?: string[];
    }

    /**
     * Describes what to do with the indentation when pressing Enter.
     */
    export enum IndentAction {
        /**
         * Insert new line and copy the previous line's indentation.
         */
        None = 0,
        /**
         * Insert new line and indent once (relative to the previous line's indentation).
         */
        Indent = 1,
        /**
         * Insert two new lines:
         *  - the first one indented which will hold the cursor
         *  - the second one at the same indentation level
         */
        IndentOutdent = 2,
        /**
         * Insert new line and outdent once (relative to the previous line's indentation).
         */
        Outdent = 3,
    }

    /**
     * Describes what to do when pressing Enter.
     */
    export interface EnterAction {
        /**
         * Describe what to do with the indentation.
         */
        indentAction: IndentAction;
        /**
         * Describe whether to outdent current line.
         */
        outdentCurrentLine?: boolean;
        /**
         * Describes text to be appended after the new line and after the indentation.
         */
        appendText?: string;
        /**
         * Describes the number of characters to remove from the new line's indentation.
         */
        removeText?: number;
    }

    /**
     * The state of the tokenizer between two lines.
     * It is useful to store flags such as in multiline comment, etc.
     * The model will clone the previous line's state and pass it in to tokenize the next line.
     */
    export interface IState {
        clone(): IState;
        equals(other: IState): boolean;
    }

    /**
     * A hover represents additional information for a symbol or word. Hovers are
     * rendered in a tooltip-like widget.
     */
    export interface Hover {
        /**
         * The contents of this hover.
         */
        contents: MarkedString[];
        /**
         * The range to which this hover applies. When missing, the
         * editor will use the range at the current position or the
         * current position itself.
         */
        range: IRange;
    }

    /**
     * The hover provider interface defines the contract between extensions and
     * the [hover](https://code.visualstudio.com/docs/editor/intellisense)-feature.
     */
    export interface HoverProvider {
        /**
         * Provide a hover for the given position and document. Multiple hovers at the same
         * position will be merged by the editor. A hover can have a range which defaults
         * to the word range at the position when omitted.
         */
        provideHover(model: editor.IReadOnlyModel, position: Position, token: CancellationToken): Hover | Thenable<Hover>;
    }

    /**
     * Represents a parameter of a callable-signature. A parameter can
     * have a label and a doc-comment.
     */
    export interface ParameterInformation {
        /**
         * The label of this signature. Will be shown in
         * the UI.
         */
        label: string;
        /**
         * The human-readable doc-comment of this signature. Will be shown
         * in the UI but can be omitted.
         */
        documentation?: string;
    }

    /**
     * Represents the signature of something callable. A signature
     * can have a label, like a function-name, a doc-comment, and
     * a set of parameters.
     */
    export interface SignatureInformation {
        /**
         * The label of this signature. Will be shown in
         * the UI.
         */
        label: string;
        /**
         * The human-readable doc-comment of this signature. Will be shown
         * in the UI but can be omitted.
         */
        documentation?: string;
        /**
         * The parameters of this signature.
         */
        parameters: ParameterInformation[];
    }

    /**
     * Signature help represents the signature of something
     * callable. There can be multiple signatures but only one
     * active and only one active parameter.
     */
    export interface SignatureHelp {
        /**
         * One or more signatures.
         */
        signatures: SignatureInformation[];
        /**
         * The active signature.
         */
        activeSignature: number;
        /**
         * The active parameter of the active signature.
         */
        activeParameter: number;
    }

    /**
     * The signature help provider interface defines the contract between extensions and
     * the [parameter hints](https://code.visualstudio.com/docs/editor/intellisense)-feature.
     */
    export interface SignatureHelpProvider {
        signatureHelpTriggerCharacters: string[];
        /**
         * Provide help for the signature at the given position and document.
         */
        provideSignatureHelp(model: editor.IReadOnlyModel, position: Position, token: CancellationToken): SignatureHelp | Thenable<SignatureHelp>;
    }

    /**
     * A document highlight kind.
     */
    export enum DocumentHighlightKind {
        /**
         * A textual occurrence.
         */
        Text = 0,
        /**
         * Read-access of a symbol, like reading a variable.
         */
        Read = 1,
        /**
         * Write-access of a symbol, like writing to a variable.
         */
        Write = 2,
    }

    /**
     * A document highlight is a range inside a text document which deserves
     * special attention. Usually a document highlight is visualized by changing
     * the background color of its range.
     */
    export interface DocumentHighlight {
        /**
         * The range this highlight applies to.
         */
        range: IRange;
        /**
         * The highlight kind, default is [text](#DocumentHighlightKind.Text).
         */
        kind: DocumentHighlightKind;
    }

    /**
     * The document highlight provider interface defines the contract between extensions and
     * the word-highlight-feature.
     */
    export interface DocumentHighlightProvider {
        /**
         * Provide a set of document highlights, like all occurrences of a variable or
         * all exit-points of a function.
         */
        provideDocumentHighlights(model: editor.IReadOnlyModel, position: Position, token: CancellationToken): DocumentHighlight[] | Thenable<DocumentHighlight[]>;
    }

    /**
     * Value-object that contains additional information when
     * requesting references.
     */
    export interface ReferenceContext {
        /**
         * Include the declaration of the current symbol.
         */
        includeDeclaration: boolean;
    }

    /**
     * The reference provider interface defines the contract between extensions and
     * the [find references](https://code.visualstudio.com/docs/editor/editingevolved#_peek)-feature.
     */
    export interface ReferenceProvider {
        /**
         * Provide a set of project-wide references for the given position and document.
         */
        provideReferences(model: editor.IReadOnlyModel, position: Position, context: ReferenceContext, token: CancellationToken): Location[] | Thenable<Location[]>;
    }

    /**
     * Represents a location inside a resource, such as a line
     * inside a text file.
     */
    export interface Location {
        /**
         * The resource identifier of this location.
         */
        uri: Uri;
        /**
         * The document range of this locations.
         */
        range: IRange;
    }

    /**
     * The definition of a symbol represented as one or many [locations](#Location).
     * For most programming languages there is only one location at which a symbol is
     * defined.
     */
    export type Definition = Location | Location[];

    /**
     * The definition provider interface defines the contract between extensions and
     * the [go to definition](https://code.visualstudio.com/docs/editor/editingevolved#_go-to-definition)
     * and peek definition features.
     */
    export interface DefinitionProvider {
        /**
         * Provide the definition of the symbol at the given position and document.
         */
        provideDefinition(model: editor.IReadOnlyModel, position: Position, token: CancellationToken): Definition | Thenable<Definition>;
    }

    /**
     * The implementation provider interface defines the contract between extensions and
     * the go to implementation feature.
     */
    export interface ImplementationProvider {
        /**
         * Provide the implementation of the symbol at the given position and document.
         */
        provideImplementation(model: editor.IReadOnlyModel, position: Position, token: CancellationToken): Definition | Thenable<Definition>;
    }

    /**
     * The type definition provider interface defines the contract between extensions and
     * the go to type definition feature.
     */
    export interface TypeDefinitionProvider {
        /**
         * Provide the type definition of the symbol at the given position and document.
         */
        provideTypeDefinition(model: editor.IReadOnlyModel, position: Position, token: CancellationToken): Definition | Thenable<Definition>;
    }

    /**
     * A symbol kind.
     */
    export enum SymbolKind {
        File = 0,
        Module = 1,
        Namespace = 2,
        Package = 3,
        Class = 4,
        Method = 5,
        Property = 6,
        Field = 7,
        Constructor = 8,
        Enum = 9,
        Interface = 10,
        Function = 11,
        Variable = 12,
        Constant = 13,
        String = 14,
        Number = 15,
        Boolean = 16,
        Array = 17,
        Object = 18,
        Key = 19,
        Null = 20,
        EnumMember = 21,
        Struct = 22,
        Event = 23,
        Operator = 24,
        TypeParameter = 25,
    }

    /**
     * Represents information about programming constructs like variables, classes,
     * interfaces etc.
     */
    export interface SymbolInformation {
        /**
         * The name of this symbol.
         */
        name: string;
        /**
         * The name of the symbol containing this symbol.
         */
        containerName?: string;
        /**
         * The kind of this symbol.
         */
        kind: SymbolKind;
        /**
         * The location of this symbol.
         */
        location: Location;
    }

    /**
     * The document symbol provider interface defines the contract between extensions and
     * the [go to symbol](https://code.visualstudio.com/docs/editor/editingevolved#_goto-symbol)-feature.
     */
    export interface DocumentSymbolProvider {
        /**
         * Provide symbol information for the given document.
         */
        provideDocumentSymbols(model: editor.IReadOnlyModel, token: CancellationToken): SymbolInformation[] | Thenable<SymbolInformation[]>;
    }

    export interface TextEdit {
        range: IRange;
        text: string;
        eol?: editor.EndOfLineSequence;
    }

    /**
     * Interface used to format a model
     */
    export interface FormattingOptions {
        /**
         * Size of a tab in spaces.
         */
        tabSize: number;
        /**
         * Prefer spaces over tabs.
         */
        insertSpaces: boolean;
    }

    /**
     * The document formatting provider interface defines the contract between extensions and
     * the formatting-feature.
     */
    export interface DocumentFormattingEditProvider {
        /**
         * Provide formatting edits for a whole document.
         */
        provideDocumentFormattingEdits(model: editor.IReadOnlyModel, options: FormattingOptions, token: CancellationToken): TextEdit[] | Thenable<TextEdit[]>;
    }

    /**
     * The document formatting provider interface defines the contract between extensions and
     * the formatting-feature.
     */
    export interface DocumentRangeFormattingEditProvider {
        /**
         * Provide formatting edits for a range in a document.
         *
         * The given range is a hint and providers can decide to format a smaller
         * or larger range. Often this is done by adjusting the start and end
         * of the range to full syntax nodes.
         */
        provideDocumentRangeFormattingEdits(model: editor.IReadOnlyModel, range: Range, options: FormattingOptions, token: CancellationToken): TextEdit[] | Thenable<TextEdit[]>;
    }

    /**
     * The document formatting provider interface defines the contract between extensions and
     * the formatting-feature.
     */
    export interface OnTypeFormattingEditProvider {
        autoFormatTriggerCharacters: string[];
        /**
         * Provide formatting edits after a character has been typed.
         *
         * The given position and character should hint to the provider
         * what range the position to expand to, like find the matching `{`
         * when `}` has been entered.
         */
        provideOnTypeFormattingEdits(model: editor.IReadOnlyModel, position: Position, ch: string, options: FormattingOptions, token: CancellationToken): TextEdit[] | Thenable<TextEdit[]>;
    }

    /**
     * A link inside the editor.
     */
    export interface ILink {
        range: IRange;
        url: string;
    }

    /**
     * A provider of links.
     */
    export interface LinkProvider {
        provideLinks(model: editor.IReadOnlyModel, token: CancellationToken): ILink[] | Thenable<ILink[]>;
        resolveLink?: (link: ILink, token: CancellationToken) => ILink | Thenable<ILink>;
    }

    export interface IResourceEdit {
        resource: Uri;
        range: IRange;
        newText: string;
    }

    export interface WorkspaceEdit {
        edits: IResourceEdit[];
        rejectReason?: string;
    }

    export interface RenameProvider {
        provideRenameEdits(model: editor.IReadOnlyModel, position: Position, newName: string, token: CancellationToken): WorkspaceEdit | Thenable<WorkspaceEdit>;
    }

    export interface Command {
        id: string;
        title: string;
        tooltip?: string;
        arguments?: any[];
    }

    export interface ICodeLensSymbol {
        range: IRange;
        id?: string;
        command?: Command;
    }

    export interface CodeLensProvider {
        onDidChange?: IEvent<this>;
        provideCodeLenses(model: editor.IReadOnlyModel, token: CancellationToken): ICodeLensSymbol[] | Thenable<ICodeLensSymbol[]>;
        resolveCodeLens?(model: editor.IReadOnlyModel, codeLens: ICodeLensSymbol, token: CancellationToken): ICodeLensSymbol | Thenable<ICodeLensSymbol>;
    }

    export interface ILanguageExtensionPoint {
        id: string;
        extensions?: string[];
        filenames?: string[];
        filenamePatterns?: string[];
        firstLine?: string;
        aliases?: string[];
        mimetypes?: string[];
        configuration?: string;
    }
    /**
     * A Monarch language definition
     */
    export interface IMonarchLanguage {
        /**
         * map from string to ILanguageRule[]
         */
        tokenizer: {
            [name: string]: IMonarchLanguageRule[];
        };
        /**
         * is the language case insensitive?
         */
        ignoreCase?: boolean;
        /**
         * if no match in the tokenizer assign this token class (default 'source')
         */
        defaultToken?: string;
        /**
         * for example [['{','}','delimiter.curly']]
         */
        brackets?: IMonarchLanguageBracket[];
        /**
         * start symbol in the tokenizer (by default the first entry is used)
         */
        start?: string;
        /**
         * attach this to every token class (by default '.' + name)
         */
        tokenPostfix: string;
    }

    /**
     * A rule is either a regular expression and an action
     * 		shorthands: [reg,act] == { regex: reg, action: act}
     *		and       : [reg,act,nxt] == { regex: reg, action: act{ next: nxt }}
     */
    export interface IMonarchLanguageRule {
        /**
         * match tokens
         */
        regex?: string | RegExp;
        /**
         * action to take on match
         */
        action?: IMonarchLanguageAction;
        /**
         * or an include rule. include all rules from the included state
         */
        include?: string;
    }

    /**
     * An action is either an array of actions...
     * ... or a case statement with guards...
     * ... or a basic action with a token value.
     */
    export interface IMonarchLanguageAction {
        /**
         * array of actions for each parenthesized match group
         */
        group?: IMonarchLanguageAction[];
        /**
         * map from string to ILanguageAction
         */
        cases?: Object;
        /**
         * token class (ie. css class) (or "@brackets" or "@rematch")
         */
        token?: string;
        /**
         * the next state to push, or "@push", "@pop", "@popall"
         */
        next?: string;
        /**
         * switch to this state
         */
        switchTo?: string;
        /**
         * go back n characters in the stream
         */
        goBack?: number;
        /**
         * @open or @close
         */
        bracket?: string;
        /**
         * switch to embedded language (useing the mimetype) or get out using "@pop"
         */
        nextEmbedded?: string;
        /**
         * log a message to the browser console window
         */
        log?: string;
    }

    /**
     * This interface can be shortened as an array, ie. ['{','}','delimiter.curly']
     */
    export interface IMonarchLanguageBracket {
        /**
         * open bracket
         */
        open: string;
        /**
         * closeing bracket
         */
        close: string;
        /**
         * token class
         */
        token: string;
    }

}

declare module monaco.worker {


    export interface IMirrorModel {
        readonly uri: Uri;
        readonly version: number;
        getValue(): string;
    }

    export interface IWorkerContext {
        /**
         * Get all available mirror models in this worker.
         */
        getMirrorModels(): IMirrorModel[];
    }

}


declare module monaco.languages.typescript {

    enum ModuleKind {
        None = 0,
        CommonJS = 1,
        AMD = 2,
        UMD = 3,
        System = 4,
        ES2015 = 5,
    }
    enum JsxEmit {
        None = 0,
        Preserve = 1,
        React = 2,
    }
    enum NewLineKind {
        CarriageReturnLineFeed = 0,
        LineFeed = 1,
    }

    enum ScriptTarget {
        ES3 = 0,
        ES5 = 1,
        ES2015 = 2,
        ES2016 = 3,
        ES2017 = 4,
        ESNext = 5,
        Latest = 5,
    }

    export enum ModuleResolutionKind {
        Classic = 1,
        NodeJs = 2,
    }

    type CompilerOptionsValue = string | number | boolean | (string | number)[] | string[];
    interface CompilerOptions {
        allowJs?: boolean;
        allowSyntheticDefaultImports?: boolean;
        allowUnreachableCode?: boolean;
        allowUnusedLabels?: boolean;
        alwaysStrict?: boolean;
        baseUrl?: string;
        charset?: string;
        declaration?: boolean;
        declarationDir?: string;
        disableSizeLimit?: boolean;
        emitBOM?: boolean;
        emitDecoratorMetadata?: boolean;
        experimentalDecorators?: boolean;
        forceConsistentCasingInFileNames?: boolean;
        importHelpers?: boolean;
        inlineSourceMap?: boolean;
        inlineSources?: boolean;
        isolatedModules?: boolean;
        jsx?: JsxEmit;
        lib?: string[];
        locale?: string;
        mapRoot?: string;
        maxNodeModuleJsDepth?: number;
        module?: ModuleKind;
        moduleResolution?: ModuleResolutionKind;
        newLine?: NewLineKind;
        noEmit?: boolean;
        noEmitHelpers?: boolean;
        noEmitOnError?: boolean;
        noErrorTruncation?: boolean;
        noFallthroughCasesInSwitch?: boolean;
        noImplicitAny?: boolean;
        noImplicitReturns?: boolean;
        noImplicitThis?: boolean;
        noUnusedLocals?: boolean;
        noUnusedParameters?: boolean;
        noImplicitUseStrict?: boolean;
        noLib?: boolean;
        noResolve?: boolean;
        out?: string;
        outDir?: string;
        outFile?: string;
        preserveConstEnums?: boolean;
        project?: string;
        reactNamespace?: string;
        jsxFactory?: string;
        removeComments?: boolean;
        rootDir?: string;
        rootDirs?: string[];
        skipLibCheck?: boolean;
        skipDefaultLibCheck?: boolean;
        sourceMap?: boolean;
        sourceRoot?: string;
        strictNullChecks?: boolean;
        suppressExcessPropertyErrors?: boolean;
        suppressImplicitAnyIndexErrors?: boolean;
        target?: ScriptTarget;
        traceResolution?: boolean;
        types?: string[];
        /** Paths used to compute primary types search locations */
        typeRoots?: string[];
        [option: string]: CompilerOptionsValue | undefined;
    }

    export interface DiagnosticsOptions {
        noSemanticValidation?: boolean;
        noSyntaxValidation?: boolean;
    }

    export interface LanguageServiceDefaults {
        /**
         * Add an additional source file to the language service. Use this
         * for typescript (definition) files that won't be loaded as editor
         * document, like `jquery.d.ts`.
         *
         * @param content The file content
         * @param filePath An optional file path
         * @returns A disposabled which will remove the file from the
         * language service upon disposal.
         */
        addExtraLib(content: string, filePath?: string): IDisposable;

        /**
         * Set TypeScript compiler options.
         */
        setCompilerOptions(options: CompilerOptions): void;

        /**
         * Configure whether syntactic and/or semantic validation should
         * be performed
         */
        setDiagnosticsOptions(options: DiagnosticsOptions): void;

        /**
         * Configure when the worker shuts down. By default that is 2mins.
         *
         * @param value The maximun idle time in milliseconds. Values less than one
         * mean never shut down.
         */
        setMaximunWorkerIdleTime(value: number): void;

        /**
         * Configure if all existing models should be eagerly sync'd
         * to the worker on start or restart.
         */
        setEagerModelSync(value: boolean): void;
    }

    export var typescriptDefaults: LanguageServiceDefaults;
    export var javascriptDefaults: LanguageServiceDefaults;

    export var getTypeScriptWorker: () => monaco.Promise<any>;
    export var getJavaScriptWorker: () => monaco.Promise<any>;
}

/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Microsoft Corporation. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
declare module monaco.languages.css {
    export interface DiagnosticsOptions {
        readonly validate?: boolean;
        readonly lint?: {
            readonly compatibleVendorPrefixes?: 'ignore' | 'warning' | 'error',
            readonly vendorPrefix?: 'ignore' | 'warning' | 'error',
            readonly duplicateProperties?: 'ignore' | 'warning' | 'error',
            readonly emptyRules?: 'ignore' | 'warning' | 'error',
            readonly importStatement?: 'ignore' | 'warning' | 'error',
            readonly boxModel?: 'ignore' | 'warning' | 'error',
            readonly universalSelector?: 'ignore' | 'warning' | 'error',
            readonly zeroUnits?: 'ignore' | 'warning' | 'error',
            readonly fontFaceProperties?: 'ignore' | 'warning' | 'error',
            readonly hexColorLength?: 'ignore' | 'warning' | 'error',
            readonly argumentsInColorFunction?: 'ignore' | 'warning' | 'error',
            readonly unknownProperties?: 'ignore' | 'warning' | 'error',
            readonly ieHack?: 'ignore' | 'warning' | 'error',
            readonly unknownVendorSpecificProperties?: 'ignore' | 'warning' | 'error',
            readonly propertyIgnoredDueToDisplay?: 'ignore' | 'warning' | 'error',
            readonly important?: 'ignore' | 'warning' | 'error',
            readonly float?: 'ignore' | 'warning' | 'error',
            readonly idSelector?: 'ignore' | 'warning' | 'error'
        }
    }

    export interface LanguageServiceDefaults {
        readonly onDidChange: IEvent<LanguageServiceDefaults>;
        readonly diagnosticsOptions: DiagnosticsOptions;
        setDiagnosticsOptions(options: DiagnosticsOptions): void;
    }

    export var cssDefaults: LanguageServiceDefaults;
    export var lessDefaults: LanguageServiceDefaults;
    export var scssDefaults: LanguageServiceDefaults;
}
/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Microsoft Corporation. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

declare module monaco.languages.json {
    export interface DiagnosticsOptions {
        /**
         * If set, the validator will be enabled and perform syntax validation as well as schema based validation.
         */
        readonly validate?: boolean;
        /**
         * If set, comments are tolerated. If set to false, syntax errors will be emmited for comments.
         */
        readonly allowComments?: boolean;
        /**
         * A list of known schemas and/or associations of schemas to file names.
         */
        readonly schemas?: {
            /**
             * The URI of the schema, which is also the identifier of the schema.
             */
            readonly uri: string;
            /**
             * A list of file names that are associated to the schema. The '*' wildcard can be used. For example '*.schema.json', 'package.json'
             */
            readonly fileMatch?: string[];
            /**
             * The schema for the given URI.
             */
            readonly schema?: any;
        }[];
    }

    export interface LanguageServiceDefaults {
        readonly onDidChange: IEvent<LanguageServiceDefaults>;
        readonly diagnosticsOptions: DiagnosticsOptions;
        setDiagnosticsOptions(options: DiagnosticsOptions): void;
    }

    export var jsonDefaults: LanguageServiceDefaults;
}
/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Microsoft Corporation. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

declare module monaco.languages.html {
    export interface HTMLFormatConfiguration {
        readonly tabSize: number;
        readonly insertSpaces: boolean;
        readonly wrapLineLength: number;
        readonly unformatted: string;
        readonly contentUnformatted: string;
        readonly indentInnerHtml: boolean;
        readonly preserveNewLines: boolean;
        readonly maxPreserveNewLines: number;
        readonly indentHandlebars: boolean;
        readonly endWithNewline: boolean;
        readonly extraLiners: string;
        readonly wrapAttributes: 'auto' | 'force' | 'force-aligned' | 'force-expand-multiline';
    }

    export interface CompletionConfiguration {
        [provider: string]: boolean;
    }

    export interface Options {
        /**
         * If set, comments are tolerated. If set to false, syntax errors will be emmited for comments.
         */
        readonly format?: HTMLFormatConfiguration;
        /**
         * A list of known schemas and/or associations of schemas to file names.
         */
        readonly suggest?: CompletionConfiguration;
    }

    export interface LanguageServiceDefaults {
        readonly onDidChange: IEvent<LanguageServiceDefaults>;
        readonly options: Options;
        setOptions(options: Options): void;
    }

    export var htmlDefaults: LanguageServiceDefaults;
    export var handlebarDefaults: LanguageServiceDefaults;
    export var razorDefaults: LanguageServiceDefaults;
}"""