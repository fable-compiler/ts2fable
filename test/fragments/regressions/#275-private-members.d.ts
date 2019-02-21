
export module PrivateMembersTests {

    /** No explicit ctor, so an implicit one should be generated */
    class ImplicitCtor {

    }

    /** explicit paramless ctor, no implicit one should be generated */
    class ExplicitCtor {
        constructor();
    }

    /** explicit ctor, no implicit one should be generated */
    class ExplicitCtor2 {
        constructor(i: number);
    }

    /** explicit private ctor, no ctor should be emitted at all */
    class PrivateCtor {
        private constructor(i: number);
    }

    // ----------------------------

    /** public property named i should be emitted */
    class PublicField {
        i : number
    }

    /** no field should be emitted */
    class PrivateField {
        /** this should not be emitted */
        private i: number
    }
}

