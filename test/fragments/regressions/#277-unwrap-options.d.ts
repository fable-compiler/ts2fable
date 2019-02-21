
export module UnwrapOptions {
    
    class Class {
        /**
         * The parameter should be "?p : string", NOT "?p : Option<string>"
         */
        Foo(p?: string | null);

        /**
         * The parameter should be "?p : string", NOT "?p : Option<string>"
         */
        Foo2(p?: string | null | undefined);

        /** the return value should be Option<string> */
        Bar(): string | null

        /** the return value should be Option<string> */
        Bar2(): string | null | undefined
    }

}

export module UnwrapOptionsAlias {

    // this pattern is used by babylonjs

    /** Alias type for value that can be null */
    type Nullable<T> = T | null;


    class Class {

        /**
         * The parameter should be "?p : string", NOT "?p : Option<string>"
         */
        Foo(p?: Nullable<string>);

        /**
         * (DOES NOT YET WORK) The parameter should be "?p : string", NOT "?p : Option<string>"
         */
        Foo2(p?: Nullable<string> | undefined);

        /**
         * (DOES NOT YET WORK) The parameter should be "?p : string", NOT "?p : Option<string>"
         */
        Foo3(p?: Nullable<string> | undefined | null);

        /** the return value should be Option<string> */
        Bar(): Nullable<string>

        /** (DOES NOT YET WORK) the return value should be Option<string> */
        Bar2(): Nullable<string> | undefined

        /** (DOES NOT YET WORK) the return value should be Option<string> */
        Bar3(): Nullable<string> | undefined | null
    }
}