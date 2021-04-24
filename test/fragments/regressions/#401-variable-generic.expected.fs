// ts2fable 0.0.0
module rec ``#401-variable-generic``
open System
open Fable.Core
open Fable.Core.JS

let [<Import("N","#401-variable-generic")>] n: N.IExports = jsNative

type [<AllowNullLiteral>] IExports =
    abstract C: CStatic

type [<AllowNullLiteral>] A =
    abstract name: string

type [<AllowNullLiteral>] B =
    abstract id: float

module N =

    type [<AllowNullLiteral>] IExports =
        /// get
        abstract v1: string
        /// get
        abstract f1: string -> float
        /// get
        abstract f2: 'T -> float
        /// get
        abstract f3: string -> 'T
        /// get
        abstract f4: 'T -> 'T
        /// get, set
        abstract f5: ('T -> 'T) with get, set
        /// get, set
        abstract f6: ('T -> 'T) with get, set
        /// get
        abstract f7: 'T -> 'T -> 'T
        /// get
        abstract f8: U2<'T, string> -> U3<string, float, 'T> -> 'T
        /// get
        abstract f9: ('T -> 'T -> 'T) -> (U2<string, 'T> -> U2<'T, float> -> U2<string, 'T>) -> U2<'T, string>
        /// get, set
        abstract f10: (('T -> 'T -> 'T) -> (U2<string, 'T> -> U2<'T, float> -> U2<string, 'T>) -> U2<'T, string>) with get, set
        /// get
        abstract f11: 'T -> 'T -> 'T
        /// get, set
        abstract f12: ('T -> 'T -> 'T) with get, set
        /// get 
        /// 
        /// T extends A
        abstract gf1: 'T -> 'T when 'T :> A
        /// get, set
        /// 
        /// T extends A
        abstract gf2: ('T -> 'T) when 'T :> A with get, set
        /// get 
        /// 
        /// T extends A
        abstract gf3: 'T -> 'T -> 'T when 'T :> A
        /// get, set
        /// 
        /// T extends A
        abstract gf4: ('T -> 'T -> 'T) when 'T :> A with get, set
        /// get
        /// 
        /// T extends A
        /// U extends A & B
        abstract gf5: 'T -> 'U -> 'U when 'T :> A and 'U :> A and 'U :> B
        /// get, set
        /// 
        /// T extends A
        /// U extends A & B
        abstract gf6: ('T -> 'U -> 'U) when 'T :> A and 'U :> A and 'U :> B with get, set
        /// get
        abstract arrayify: U2<'T, ResizeArray<'T>> -> ResizeArray<'T>
        /// get
        abstract izi1: string -> string -> unit
        /// get, set
        abstract izi3: (string -> string -> unit) with get, set
        /// get
        abstract izi4: 'T -> 'T -> 'T
        /// get, set
        abstract izi5: ('T -> 'T -> 'T) with get, set
        /// get
        abstract t1: string * string
        /// get
        abstract ft1: 'T * 'T -> 'T
        /// get, set
        abstract ft2: ('T * 'T -> 'T) with get, set
        /// get
        /// 
        /// v2 optional
        abstract o1: 'T -> ('T) option -> 'T
        /// get, set
        /// 
        /// v2 optional
        abstract o2: ('T -> ('T) option -> 'T) with get, set
        /// get
        /// 
        /// v2 optional
        abstract o3: 'T -> ('T * 'T) option -> 'T
        /// get, set
        /// 
        /// v2 optional
        abstract o4: ('T -> ('T * 'T) option -> 'T) with get, set
        /// function
        abstract ff1: f: ('T -> 'T) -> ('T -> 'T)
        /// function
        abstract ff2: f: ('T -> 'T) * v: 'T -> string
        /// function
        abstract ff3: v1: 'T * v2: U2<'T, string> -> U2<'T, float>

type [<AllowNullLiteral>] I =
    /// get
    abstract v1: string
    /// get
    abstract f1: string -> float
    /// get
    abstract f2: 'T -> float
    /// get
    abstract f3: string -> 'T
    /// get
    abstract f4: 'T -> 'T
    /// get, set
    abstract f5: ('T -> 'T) with get, set
    /// get, set
    abstract f6: ('T -> 'T) with get, set
    /// get
    abstract f7: 'T -> 'T -> 'T
    /// get
    abstract f8: U2<'T, string> -> U3<string, float, 'T> -> 'T
    /// get
    abstract f9: ('T -> 'T -> 'T) -> (U2<string, 'T> -> U2<'T, float> -> U2<string, 'T>) -> U2<'T, string>
    /// get, set
    abstract f10: (('T -> 'T -> 'T) -> (U2<string, 'T> -> U2<'T, float> -> U2<string, 'T>) -> U2<'T, string>) with get, set
    /// get
    abstract f11: 'T -> 'T -> 'T
    /// get, set
    abstract f12: ('T -> 'T -> 'T) with get, set
    /// get 
    /// 
    /// T extends A
    abstract gf1: 'T -> 'T when 'T :> A
    /// get, set
    /// 
    /// T extends A
    abstract gf2: ('T -> 'T) when 'T :> A with get, set
    /// get 
    /// 
    /// T extends A
    abstract gf3: 'T -> 'T -> 'T when 'T :> A
    /// get, set
    /// 
    /// T extends A
    abstract gf4: ('T -> 'T -> 'T) when 'T :> A with get, set
    /// get
    /// 
    /// T extends A
    /// U extends A & B
    abstract gf5: 'T -> 'U -> 'U when 'T :> A and 'U :> A and 'U :> B
    /// get, set
    /// 
    /// T extends A
    /// U extends A & B
    abstract gf6: ('T -> 'U -> 'U) when 'T :> A and 'U :> A and 'U :> B with get, set
    /// get
    abstract arrayify: U2<'T, ResizeArray<'T>> -> ResizeArray<'T>
    /// get
    abstract izi1: string -> string -> unit
    /// get, set
    abstract izi3: (string -> string -> unit) with get, set
    /// get
    abstract izi4: 'T -> 'T -> 'T
    /// get, set
    abstract izi5: ('T -> 'T -> 'T) with get, set
    /// get
    abstract t1: string * string
    /// get
    abstract ft1: 'T * 'T -> 'T
    /// get, set
    abstract ft2: ('T * 'T -> 'T) with get, set
    /// get
    /// 
    /// v2 optional
    abstract o1: 'T -> ('T) option -> 'T
    /// get, set
    /// 
    /// v2 optional
    abstract o2: ('T -> ('T) option -> 'T) with get, set
    /// get
    /// 
    /// v2 optional
    abstract o3: 'T -> ('T * 'T) option -> 'T
    /// get, set
    /// 
    /// v2 optional
    abstract o4: ('T -> ('T * 'T) option -> 'T) with get, set
    /// function
    abstract ff1: f: ('T -> 'T) -> ('T -> 'T)
    /// function
    abstract ff2: f: ('T -> 'T) * v: 'T -> string
    /// function
    abstract ff3: v1: 'T * v2: U2<'T, string> -> U2<'T, float>

type [<AllowNullLiteral>] C =
    /// get
    abstract v1: string
    /// get
    abstract f1: string -> float
    /// get
    abstract f2: 'T -> float
    /// get
    abstract f3: string -> 'T
    /// get
    abstract f4: 'T -> 'T
    /// get, set
    abstract f5: ('T -> 'T) with get, set
    /// get, set
    abstract f6: ('T -> 'T) with get, set
    /// get
    abstract f7: 'T -> 'T -> 'T
    /// get
    abstract f8: U2<'T, string> -> U3<string, float, 'T> -> 'T
    /// get
    abstract f9: ('T -> 'T -> 'T) -> (U2<string, 'T> -> U2<'T, float> -> U2<string, 'T>) -> U2<'T, string>
    /// get, set
    abstract f10: (('T -> 'T -> 'T) -> (U2<string, 'T> -> U2<'T, float> -> U2<string, 'T>) -> U2<'T, string>) with get, set
    /// get
    abstract f11: 'T -> 'T -> 'T
    /// get, set
    abstract f12: ('T -> 'T -> 'T) with get, set
    /// get 
    /// 
    /// T extends A
    abstract gf1: 'T -> 'T when 'T :> A
    /// get, set
    /// 
    /// T extends A
    abstract gf2: ('T -> 'T) when 'T :> A with get, set
    /// get 
    /// 
    /// T extends A
    abstract gf3: 'T -> 'T -> 'T when 'T :> A
    /// get, set
    /// 
    /// T extends A
    abstract gf4: ('T -> 'T -> 'T) when 'T :> A with get, set
    /// get
    /// 
    /// T extends A
    /// U extends A & B
    abstract gf5: 'T -> 'U -> 'U when 'T :> A and 'U :> A and 'U :> B
    /// get, set
    /// 
    /// T extends A
    /// U extends A & B
    abstract gf6: ('T -> 'U -> 'U) when 'T :> A and 'U :> A and 'U :> B with get, set
    /// get
    abstract arrayify: U2<'T, ResizeArray<'T>> -> ResizeArray<'T>
    /// get
    abstract izi1: string -> string -> unit
    /// get, set
    abstract izi3: (string -> string -> unit) with get, set
    /// get
    abstract izi4: 'T -> 'T -> 'T
    /// get, set
    abstract izi5: ('T -> 'T -> 'T) with get, set
    /// get
    abstract t1: string * string
    /// get
    abstract ft1: 'T * 'T -> 'T
    /// get, set
    abstract ft2: ('T * 'T -> 'T) with get, set
    /// get
    /// 
    /// v2 optional
    abstract o1: 'T -> ('T) option -> 'T
    /// get, set
    /// 
    /// v2 optional
    abstract o2: ('T -> ('T) option -> 'T) with get, set
    /// get
    /// 
    /// v2 optional
    abstract o3: 'T -> ('T * 'T) option -> 'T
    /// get, set
    /// 
    /// v2 optional
    abstract o4: ('T -> ('T * 'T) option -> 'T) with get, set
    /// function
    abstract ff1: f: ('T -> 'T) -> ('T -> 'T)
    /// function
    abstract ff2: f: ('T -> 'T) * v: 'T -> string
    /// function
    abstract ff3: v1: 'T * v2: U2<'T, string> -> U2<'T, float>

type [<AllowNullLiteral>] CStatic =
    [<EmitConstructor>] abstract Create: unit -> C
