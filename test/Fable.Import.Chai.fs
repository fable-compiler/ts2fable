module rec Fable.Import.Chai
open System
open Fable.Core
open Fable.Import.JS

// type [<AllowNullLiteral>] IExports =
//     abstract chai: Chai with get, set

module Chai =

    type [<AllowNullLiteral>] IExports =
        abstract AssertionError: AssertionErrorStatic with get, set

    type [<AllowNullLiteral>] ChaiStatic =
        abstract expect: ExpectStatic with get, set
        abstract should: unit -> Should
        abstract ``use``: fn: (obj -> obj -> unit) -> ChaiStatic
        abstract ``assert``: AssertStatic with get, set
        abstract config: Config with get, set
        abstract AssertionError: obj with get, set
        abstract version: string with get, set

    type [<AllowNullLiteral>] ExpectStatic =
        inherit AssertionStatic
        abstract fail: ?actual: obj * ?expected: obj * ?message: string * ?operator: Operator -> unit

    type [<AllowNullLiteral>] AssertStatic =
        inherit Assert

    type [<AllowNullLiteral>] AssertionStatic =
        [<Emit "$0($1...)">] abstract Invoke: target: obj * ?message: string -> Assertion

    type Operator =
        string

    type OperatorComparable =
        U4<bool, float, string, DateTime> option

    type [<AllowNullLiteral>] ShouldAssertion =
        abstract equal: value1: obj * value2: obj * ?message: string -> unit
        abstract Throw: ShouldThrow with get, set
        abstract throw: ShouldThrow with get, set
        abstract exist: value: obj * ?message: string -> unit

    type [<AllowNullLiteral>] Should =
        inherit ShouldAssertion
        abstract not: ShouldAssertion with get, set
        abstract fail: actual: obj * expected: obj * ?message: string * ?operator: Operator -> unit

    type [<AllowNullLiteral>] ShouldThrow =
        [<Emit "$0($1...)">] abstract Invoke: actual: Function -> unit
        [<Emit "$0($1...)">] abstract Invoke: actual: Function * expected: U2<string, RegExp> * ?message: string -> unit
        [<Emit "$0($1...)">] abstract Invoke: actual: Function * ``constructor``: U2<Error, Function> * ?expected: U2<string, RegExp> * ?message: string -> unit

    type [<AllowNullLiteral>] Assertion =
        inherit LanguageChains
        inherit NumericComparison
        inherit TypeComparison
        abstract not: Assertion with get, set
        abstract deep: Deep with get, set
        abstract ordered: Ordered with get, set
        abstract nested: Nested with get, set
        abstract any: KeyFilter with get, set
        abstract all: KeyFilter with get, set
        abstract a: TypeComparison with get, set
        abstract an: TypeComparison with get, set
        abstract ``include``: Include with get, set
        abstract includes: Include with get, set
        abstract contain: Include with get, set
        abstract contains: Include with get, set
        abstract ok: Assertion with get, set
        abstract ``true``: Assertion with get, set
        abstract ``false``: Assertion with get, set
        abstract ``null``: Assertion with get, set
        abstract undefined: Assertion with get, set
        abstract NaN: Assertion with get, set
        abstract exist: Assertion with get, set
        abstract empty: Assertion with get, set
        abstract arguments: Assertion with get, set
        abstract Arguments: Assertion with get, set
        abstract equal: Equal with get, set
        abstract equals: Equal with get, set
        abstract eq: Equal with get, set
        abstract eql: Equal with get, set
        abstract eqls: Equal with get, set
        abstract property: Property with get, set
        abstract ownProperty: OwnProperty with get, set
        abstract haveOwnProperty: OwnProperty with get, set
        abstract ownPropertyDescriptor: OwnPropertyDescriptor with get, set
        abstract haveOwnPropertyDescriptor: OwnPropertyDescriptor with get, set
        abstract length: Length with get, set
        abstract lengthOf: Length with get, set
        abstract ``match``: Match with get, set
        abstract matches: Match with get, set
        abstract string: string: string * ?message: string -> Assertion
        abstract keys: Keys with get, set
        abstract key: string: string -> Assertion
        abstract throw: Throw with get, set
        abstract throws: Throw with get, set
        abstract Throw: Throw with get, set
        abstract respondTo: RespondTo with get, set
        abstract respondsTo: RespondTo with get, set
        abstract itself: Assertion with get, set
        abstract satisfy: Satisfy with get, set
        abstract satisfies: Satisfy with get, set
        abstract closeTo: CloseTo with get, set
        abstract approximately: CloseTo with get, set
        abstract members: Members with get, set
        abstract increase: PropertyChange with get, set
        abstract increases: PropertyChange with get, set
        abstract decrease: PropertyChange with get, set
        abstract decreases: PropertyChange with get, set
        abstract change: PropertyChange with get, set
        abstract changes: PropertyChange with get, set
        abstract extensible: Assertion with get, set
        abstract ``sealed``: Assertion with get, set
        abstract frozen: Assertion with get, set
        abstract oneOf: list: ResizeArray<obj> * ?message: string -> Assertion

    type [<AllowNullLiteral>] LanguageChains =
        abstract ``to``: Assertion with get, set
        abstract be: Assertion with get, set
        abstract been: Assertion with get, set
        abstract is: Assertion with get, set
        abstract that: Assertion with get, set
        abstract which: Assertion with get, set
        abstract ``and``: Assertion with get, set
        abstract has: Assertion with get, set
        abstract have: Assertion with get, set
        abstract ``with``: Assertion with get, set
        abstract at: Assertion with get, set
        abstract ``of``: Assertion with get, set
        abstract same: Assertion with get, set
        abstract but: Assertion with get, set
        abstract does: Assertion with get, set

    type [<AllowNullLiteral>] NumericComparison =
        abstract above: NumberComparer with get, set
        abstract gt: NumberComparer with get, set
        abstract greaterThan: NumberComparer with get, set
        abstract least: NumberComparer with get, set
        abstract gte: NumberComparer with get, set
        abstract below: NumberComparer with get, set
        abstract lt: NumberComparer with get, set
        abstract lessThan: NumberComparer with get, set
        abstract most: NumberComparer with get, set
        abstract lte: NumberComparer with get, set
        abstract within: start: float * finish: float * ?message: string -> Assertion

    type [<AllowNullLiteral>] NumberComparer =
        [<Emit "$0($1...)">] abstract Invoke: value: float * ?message: string -> Assertion

    type [<AllowNullLiteral>] TypeComparison =
        [<Emit "$0($1...)">] abstract Invoke: ``type``: string * ?message: string -> Assertion
        abstract instanceof: InstanceOf with get, set
        abstract instanceOf: InstanceOf with get, set

    type [<AllowNullLiteral>] InstanceOf =
        [<Emit "$0($1...)">] abstract Invoke: ``constructor``: Object * ?message: string -> Assertion

    type [<AllowNullLiteral>] CloseTo =
        [<Emit "$0($1...)">] abstract Invoke: expected: float * delta: float * ?message: string -> Assertion

    type [<AllowNullLiteral>] Nested =
        abstract ``include``: Include with get, set
        abstract property: Property with get, set
        abstract members: Members with get, set

    type [<AllowNullLiteral>] Deep =
        abstract equal: Equal with get, set
        abstract equals: Equal with get, set
        abstract eq: Equal with get, set
        abstract ``include``: Include with get, set
        abstract property: Property with get, set
        abstract members: Members with get, set
        abstract ordered: Ordered with get, set

    type [<AllowNullLiteral>] Ordered =
        abstract members: Members with get, set

    type [<AllowNullLiteral>] KeyFilter =
        abstract keys: Keys with get, set

    type [<AllowNullLiteral>] Equal =
        [<Emit "$0($1...)">] abstract Invoke: value: obj * ?message: string -> Assertion

    type [<AllowNullLiteral>] Property =
        [<Emit "$0($1...)">] abstract Invoke: name: string * ?value: obj * ?message: string -> Assertion

    type [<AllowNullLiteral>] OwnProperty =
        [<Emit "$0($1...)">] abstract Invoke: name: string * ?message: string -> Assertion

    type [<AllowNullLiteral>] OwnPropertyDescriptor =
        [<Emit "$0($1...)">] abstract Invoke: name: string * descriptor: PropertyDescriptor * ?message: string -> Assertion
        [<Emit "$0($1...)">] abstract Invoke: name: string * ?message: string -> Assertion

    type [<AllowNullLiteral>] Length =
        inherit LanguageChains
        inherit NumericComparison
        [<Emit "$0($1...)">] abstract Invoke: length: float * ?message: string -> Assertion

    type [<AllowNullLiteral>] Include =
        [<Emit "$0($1...)">] abstract Invoke: value: Object * ?message: string -> Assertion
        [<Emit "$0($1...)">] abstract Invoke: value: string * ?message: string -> Assertion
        [<Emit "$0($1...)">] abstract Invoke: value: float * ?message: string -> Assertion
        abstract keys: Keys with get, set
        abstract deep: Deep with get, set
        abstract ordered: Ordered with get, set
        abstract members: Members with get, set
        abstract any: KeyFilter with get, set
        abstract all: KeyFilter with get, set

    type [<AllowNullLiteral>] Match =
        [<Emit "$0($1...)">] abstract Invoke: regexp: U2<RegExp, string> * ?message: string -> Assertion

    type [<AllowNullLiteral>] Keys =
        [<Emit "$0($1...)">] abstract Invoke: [<ParamArray>] keys: string -> Assertion
        [<Emit "$0($1...)">] abstract Invoke: keys: ResizeArray<obj> -> Assertion
        [<Emit "$0($1...)">] abstract Invoke: keys: Object -> Assertion

    type [<AllowNullLiteral>] Throw =
        [<Emit "$0($1...)">] abstract Invoke: unit -> Assertion
        [<Emit "$0($1...)">] abstract Invoke: expected: string * ?message: string -> Assertion
        [<Emit "$0($1...)">] abstract Invoke: expected: RegExp * ?message: string -> Assertion
        [<Emit "$0($1...)">] abstract Invoke: ``constructor``: Error * ?expected: string * ?message: string -> Assertion
        [<Emit "$0($1...)">] abstract Invoke: ``constructor``: Error * ?expected: RegExp * ?message: string -> Assertion
        [<Emit "$0($1...)">] abstract Invoke: ``constructor``: Function * ?expected: string * ?message: string -> Assertion
        [<Emit "$0($1...)">] abstract Invoke: ``constructor``: Function * ?expected: RegExp * ?message: string -> Assertion

    type [<AllowNullLiteral>] RespondTo =
        [<Emit "$0($1...)">] abstract Invoke: ``method``: string * ?message: string -> Assertion

    type [<AllowNullLiteral>] Satisfy =
        [<Emit "$0($1...)">] abstract Invoke: matcher: Function * ?message: string -> Assertion

    type [<AllowNullLiteral>] Members =
        [<Emit "$0($1...)">] abstract Invoke: set: ResizeArray<obj> * ?message: string -> Assertion

    type [<AllowNullLiteral>] PropertyChange =
        [<Emit "$0($1...)">] abstract Invoke: ``object``: Object * property: string * ?message: string -> Assertion

    type [<AllowNullLiteral>] Assert =
        [<Emit "$0($1...)">] abstract Invoke: expression: obj * ?message: string -> unit
        abstract fail: ?actual: 'T * ?expected: 'T * ?message: string * ?operator: Operator -> unit
        abstract isOk: value: 'T * ?message: string -> unit
        abstract ok: value: 'T * ?message: string -> unit
        abstract isNotOk: value: 'T * ?message: string -> unit
        abstract notOk: value: 'T * ?message: string -> unit
        abstract equal: actual: 'T * expected: 'T * ?message: string -> unit
        abstract notEqual: actual: 'T * expected: 'T * ?message: string -> unit
        abstract strictEqual: actual: 'T * expected: 'T * ?message: string -> unit
        abstract notStrictEqual: actual: 'T * expected: 'T * ?message: string -> unit
        abstract deepEqual: actual: 'T * expected: 'T * ?message: string -> unit
        abstract notDeepEqual: actual: 'T * expected: 'T * ?message: string -> unit
        abstract isAbove: valueToCheck: float * valueToBeAbove: float * ?message: string -> unit
        abstract isAtLeast: valueToCheck: float * valueToBeAtLeast: float * ?message: string -> unit
        abstract isBelow: valueToCheck: float * valueToBeBelow: float * ?message: string -> unit
        abstract isAtMost: valueToCheck: float * valueToBeAtMost: float * ?message: string -> unit
        abstract isTrue: value: 'T * ?message: string -> unit
        abstract isFalse: value: 'T * ?message: string -> unit
        abstract isNotTrue: value: 'T * ?message: string -> unit
        abstract isNotFalse: value: 'T * ?message: string -> unit
        abstract isNull: value: 'T * ?message: string -> unit
        abstract isNotNull: value: 'T * ?message: string -> unit
        abstract isNaN: value: 'T * ?message: string -> unit
        abstract isNotNaN: value: 'T * ?message: string -> unit
        abstract exists: value: 'T * ?message: string -> unit
        abstract notExists: value: 'T * ?message: string -> unit
        abstract isUndefined: value: 'T * ?message: string -> unit
        abstract isDefined: value: 'T * ?message: string -> unit
        abstract isFunction: value: 'T * ?message: string -> unit
        abstract isNotFunction: value: 'T * ?message: string -> unit
        abstract isObject: value: 'T * ?message: string -> unit
        abstract isNotObject: value: 'T * ?message: string -> unit
        abstract isArray: value: 'T * ?message: string -> unit
        abstract isNotArray: value: 'T * ?message: string -> unit
        abstract isString: value: 'T * ?message: string -> unit
        abstract isNotString: value: 'T * ?message: string -> unit
        abstract isNumber: value: 'T * ?message: string -> unit
        abstract isNotNumber: value: 'T * ?message: string -> unit
        abstract isBoolean: value: 'T * ?message: string -> unit
        abstract isNotBoolean: value: 'T * ?message: string -> unit
        abstract typeOf: value: 'T * name: string * ?message: string -> unit
        abstract notTypeOf: value: 'T * name: string * ?message: string -> unit
        abstract instanceOf: value: 'T * ``constructor``: Function * ?message: string -> unit
        abstract notInstanceOf: value: 'T * ``type``: Function * ?message: string -> unit
        abstract ``include``: haystack: string * needle: string * ?message: string -> unit
        abstract ``include``: haystack: ResizeArray<'T> * needle: 'T * ?message: string -> unit
        abstract notInclude: haystack: string * needle: obj * ?message: string -> unit
        abstract notInclude: haystack: ResizeArray<obj> * needle: obj * ?message: string -> unit
        abstract ``match``: value: string * regexp: RegExp * ?message: string -> unit
        abstract notMatch: expected: obj * regexp: RegExp * ?message: string -> unit
        abstract property: ``object``: 'T * property: string * ?message: string -> unit
        abstract notProperty: ``object``: 'T * property: string * ?message: string -> unit
        abstract deepProperty: ``object``: 'T * property: string * ?message: string -> unit
        abstract notDeepProperty: ``object``: 'T * property: string * ?message: string -> unit
        abstract propertyVal: ``object``: 'T * property: string * value: 'V * ?message: string -> unit
        abstract propertyNotVal: ``object``: 'T * property: string * value: 'V * ?message: string -> unit
        abstract deepPropertyVal: ``object``: 'T * property: string * value: 'V * ?message: string -> unit
        abstract deepPropertyNotVal: ``object``: 'T * property: string * value: 'V * ?message: string -> unit
        abstract lengthOf: ``object``: 'T * length: float * ?message: string -> unit
        abstract throw: fn: Function * ?message: string -> unit
        abstract throw: fn: Function * regExp: RegExp -> unit
        abstract throw: fn: Function * ``constructor``: Function * ?message: string -> unit
        abstract throw: fn: Function * ``constructor``: Function * regExp: RegExp -> unit
        abstract throws: fn: Function * ?message: string -> unit
        abstract throws: fn: Function * regExp: RegExp * ?message: string -> unit
        abstract throws: fn: Function * errType: Function * ?message: string -> unit
        abstract throws: fn: Function * errType: Function * regExp: RegExp -> unit
        abstract Throw: fn: Function * ?message: string -> unit
        abstract Throw: fn: Function * regExp: RegExp -> unit
        abstract Throw: fn: Function * errType: Function * ?message: string -> unit
        abstract Throw: fn: Function * errType: Function * regExp: RegExp -> unit
        abstract doesNotThrow: fn: Function * ?message: string -> unit
        abstract doesNotThrow: fn: Function * regExp: RegExp -> unit
        abstract doesNotThrow: fn: Function * errType: Function * ?message: string -> unit
        abstract doesNotThrow: fn: Function * errType: Function * regExp: RegExp -> unit
        abstract operator: val1: OperatorComparable * operator: Operator * val2: OperatorComparable * ?message: string -> unit
        abstract closeTo: actual: float * expected: float * delta: float * ?message: string -> unit
        abstract approximately: act: float * exp: float * delta: float * ?message: string -> unit
        abstract sameMembers: set1: ResizeArray<'T> * set2: ResizeArray<'T> * ?message: string -> unit
        abstract sameDeepMembers: set1: ResizeArray<'T> * set2: ResizeArray<'T> * ?message: string -> unit
        abstract sameOrderedMembers: set1: ResizeArray<'T> * set2: ResizeArray<'T> * ?message: string -> unit
        abstract notSameOrderedMembers: set1: ResizeArray<'T> * set2: ResizeArray<'T> * ?message: string -> unit
        abstract sameDeepOrderedMembers: set1: ResizeArray<'T> * set2: ResizeArray<'T> * ?message: string -> unit
        abstract notSameDeepOrderedMembers: set1: ResizeArray<'T> * set2: ResizeArray<'T> * ?message: string -> unit
        abstract includeOrderedMembers: superset: ResizeArray<'T> * subset: ResizeArray<'T> * ?message: string -> unit
        abstract notIncludeOrderedMembers: superset: ResizeArray<'T> * subset: ResizeArray<'T> * ?message: string -> unit
        abstract includeDeepOrderedMembers: superset: ResizeArray<'T> * subset: ResizeArray<'T> * ?message: string -> unit
        abstract notIncludeDeepOrderedMembers: superset: ResizeArray<'T> * subset: ResizeArray<'T> * ?message: string -> unit
        abstract includeMembers: superset: ResizeArray<'T> * subset: ResizeArray<'T> * ?message: string -> unit
        abstract includeDeepMembers: superset: ResizeArray<'T> * subset: ResizeArray<'T> * ?message: string -> unit
        abstract oneOf: inList: 'T * list: ResizeArray<'T> * ?message: string -> unit
        abstract changes: modifier: Function * ``object``: 'T * property: string * ?message: string -> unit
        abstract doesNotChange: modifier: Function * ``object``: 'T * property: string * ?message: string -> unit
        abstract increases: modifier: Function * ``object``: 'T * property: string * ?message: string -> unit
        abstract doesNotIncrease: modifier: Function * ``object``: 'T * property: string * ?message: string -> unit
        abstract decreases: modifier: Function * ``object``: 'T * property: string * ?message: string -> unit
        abstract doesNotDecrease: modifier: Function * ``object``: 'T * property: string * ?message: string -> unit
        abstract ifError: ``object``: 'T * ?message: string -> unit
        abstract isExtensible: ``object``: 'T * ?message: string -> unit
        abstract extensible: ``object``: 'T * ?message: string -> unit
        abstract isNotExtensible: ``object``: 'T * ?message: string -> unit
        abstract notExtensible: ``object``: 'T * ?message: string -> unit
        abstract isSealed: ``object``: 'T * ?message: string -> unit
        abstract ``sealed``: ``object``: 'T * ?message: string -> unit
        abstract isNotSealed: ``object``: 'T * ?message: string -> unit
        abstract notSealed: ``object``: 'T * ?message: string -> unit
        abstract isFrozen: ``object``: 'T * ?message: string -> unit
        abstract frozen: ``object``: 'T * ?message: string -> unit
        abstract isNotFrozen: ``object``: 'T * ?message: string -> unit
        abstract notFrozen: ``object``: 'T * ?message: string -> unit
        abstract isEmpty: ``object``: 'T * ?message: string -> unit
        abstract isNotEmpty: ``object``: 'T * ?message: string -> unit

    type [<AllowNullLiteral>] Config =
        abstract includeStack: bool with get, set
        abstract showDiff: bool with get, set
        abstract truncateThreshold: float with get, set

    type [<AllowNullLiteral>] AssertionError =
        abstract name: string with get, set
        abstract message: string with get, set
        abstract showDiff: bool with get, set
        abstract stack: string with get, set

    type [<AllowNullLiteral>] AssertionErrorStatic =
        [<Emit "new $0($1...)">] abstract Create: message: string * ?_props: obj * ?ssf: Function -> AssertionError

// module chai =

// type [<AllowNullLiteral>] Object =
//     abstract should: Chai with get, set
