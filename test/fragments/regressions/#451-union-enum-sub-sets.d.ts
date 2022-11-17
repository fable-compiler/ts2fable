export declare namespace alpha {
    enum RootKind {
        Alpha = 1,
        Beta = 2,
        Gamma = 3,
        Delta = 4
    }
    type AliasKind = RootKind
    type SingleKind = RootKind.Beta
    type Sub1Kind = RootKind.Alpha | RootKind.Gamma;
    /**
     * Should keep `Sub1Kind` and not replace with source enum!
     */
    type Sub1KindAlias = Sub1Kind

    type Sub1AddKind = Sub1Kind;
    type Sub11Kind = RootKind.Alpha | Sub1AddKind
    type Sub12Kind = Sub1Kind | Sub1Kind
    type Sub13Kind = Sub1Kind | RootKind.Beta
    type Sub3Kind = RootKind.Alpha | RootKind.Beta;
    type Sub4Kind = Sub1Kind | Sub3Kind;
    type Sub5Kind = RootKind.Delta | Sub3Kind;
    type Sub6Kind = Sub4Kind | Sub5Kind

    enum OtherKind {
        Foo = 1,
        Bar = 2,
        Baz = 3
    }
    type OtherSub1Kind = OtherKind.Foo | OtherKind.Bar;
    type OtherSub2Kind = OtherKind.Foo | OtherKind.Baz;
    type OtherSub12Kind = OtherSub1Kind | OtherSub2Kind;

    /**
     * Two different enums -> keep Union
     */
    type MixedKind = RootKind.Alpha | OtherKind.Bar;
    /**
     * Two different enums -> keep Union
     */
    type MixedSubKind = Sub1Kind | OtherSub1Kind;
}
export declare namespace beta {
    type AlphaSub1 = alpha.Sub1Kind | alpha.Sub3Kind;
    type AlphaSub2 = alpha.Sub4Kind | alpha.Sub3Kind;
}
export declare namespace gamma {
    /**
     * must reference `RootKind` with correct namespace (`alpha`)
     */
    type BetaSub = beta.AlphaSub1 | beta.AlphaSub2;
}