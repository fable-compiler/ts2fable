/**
 * extends int enum
 * -> `: enum<int>` constraint
 */
export declare namespace n {
    enum RootKind {
        Alpha = 1,
        Beta = 2,
        Gamma = 3,
        Delta = 4
    }
    type SubKind = RootKind.Alpha | RootKind.Gamma;

    interface InterfaceRootKind<TKind extends RootKind> { }
    interface InterfaceSubKind<TKind extends SubKind> { }
    interface InterfaceSubValue<TKind extends RootKind.Alpha> { }
    interface InterfaceSubSet<TKind extends RootKind.Alpha | RootKind.Beta> { }

    function fRootKind<TKind extends RootKind>(value: TKind): TKind
    function fSub1Kind<TKind extends SubKind>(value: TKind): TKind
    function fSubValue<TKind extends RootKind.Alpha>(value: TKind): TKind
    function fSubSet<TKind extends RootKind.Alpha | RootKind.Beta>(value: TKind): TKind
}

/**
 * extends string enum
 * -> remove constraint
 */
export declare namespace s {
    enum MyKind {
        Alpha = "foo",
        Beta = "bar",
        Gamma = "baz",
    }
    type SubKind = MyKind.Alpha | MyKind.Gamma;
    interface InterfaceMyKind<TKind extends MyKind> { }
    interface InterfaceSubKind<TKind extends SubKind> { }
    interface InterfaceSubValue<TKind extends MyKind.Alpha> { }
    interface InterfaceSubSet<TKind extends MyKind.Alpha | MyKind.Beta> { }

    function fMyKind<TKind extends MyKind>(value: TKind): TKind
    function fSub1Kind<TKind extends SubKind>(value: TKind): TKind
    function fSubValue<TKind extends MyKind.Alpha>(value: TKind): TKind
    function fSubSet<TKind extends MyKind.Alpha | MyKind.Beta>(value: TKind): TKind
}