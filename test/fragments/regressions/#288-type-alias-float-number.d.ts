
/** it should NOT emit "type float = float" */
export module TypeAloasFloatNumber {

    /** this pattern is used by babylonjs */
    type float = number;

    class Class {
        /** param f and return value should be "float" */
        Foo(f: float): float;
    }
}
