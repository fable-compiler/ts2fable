export declare namespace ns {
  enum Kind {
    Alpha = 1,
    Beta = 2,
    Gamma = 3,
    Delta = 4,
  }
  interface Token<TKind extends Kind> {}
  
  type TokenAlpha = Token<Kind.Alpha>
  type TokenBeta = Token<Kind.Beta>
  type TokenGamma = Token<Kind.Gamma>
  /**
   * Common type
   */
  type TokenABDirect1 = Token<Kind.Alpha> | Token<Kind.Beta>
  type TokenABDirect2 = Token<Kind.Alpha | Kind.Beta>
  type TokenBC1 = Token<Kind.Beta | Kind.Gamma>
  type TokenBC2 = Token<Kind.Beta> | Token<Kind.Gamma>
  type TokenBC = TokenBeta | TokenGamma
  type ABCToken = TokenAlpha | TokenBC | TokenGamma
  type ABCToken2 = TokenAlpha | TokenBC | Token<Kind.Gamma>
  type ABCToken3 = TokenAlpha | TokenBC | Token<Kind.Gamma | Kind.Delta>
  
  enum Other {
    Foo = 1,
    Bar = 2,
  }
  interface OtherToken<TKind extends Other> {}
  type OtherTokenFoo = OtherToken<Other.Foo>
  type OtherTokenBoth = OtherTokenFoo | OtherToken<Other.Bar>
  /**
   * No common type
   */
  type MixedToken = TokenGamma | OtherTokenFoo
  
  interface BasicToken<TKind> {}
  /**
   * No common type
   */
  type SimpleMixedToken = TokenGamma | BasicToken<Kind.Alpha>
  type BasicKindToken = BasicToken<Kind.Alpha> | BasicToken<Kind.Beta>
  
  /**
   * Doesn't need any adjustments or comments
   */
  type Simple = Token<Kind>
}

export declare namespace other {
  /**
   * requires namespace
   */
  type InOther1 = ns.TokenBC | ns.Token<ns.Kind.Gamma> | ns.Token<ns.Kind.Delta | ns.Kind.Alpha>
  /**
   * requires namespace
   */
  type InOther2 = ns.Token<ns.Kind.Delta | ns.Kind.Alpha>

  
  /**
   * Doesn't need any adjustments or comments
   */
  type Simple = ns.Token<ns.Kind>
}