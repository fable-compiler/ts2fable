
export module StaticTests {

    /** this class should contain 4 properties - 2 static ones, 2 instance ones. */
    class PropertiesClass {
        static staticProperty: number;
        instanceProperty: number;

        /** these should NOT be emitted */
        private static privateStaticProperty: number;
        /** these should NOT be emitted */
        private privateInstanceProperty: number;

        public static publicStaticProperty: number;
        public publicInstanceProperty: number;
    }
}

