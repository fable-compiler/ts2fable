
export module TypeLiteralsAsReturnValue {
    
    class Class {
        /**
         * This should generate two interfaces: 'Class' and 'ClassFooReturn'. The second should have a single property 'i'.
         */
        Foo(): { i: number };
    }

}
