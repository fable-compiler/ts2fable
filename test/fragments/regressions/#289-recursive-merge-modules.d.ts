declare module 'Outer' {
    export = Outer;
}

/** It should generate one module Outer.Inner with two interfaces C1 and C2 */
declare module Outer.Inner {
    class C1 {

    }
}

declare module Outer.Inner {
    class C2 extends C1 {

    }
}