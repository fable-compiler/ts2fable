// ts2fable 0.0.0
module rec #292-static-props
open System
open Fable.Core
open Fable.Core.JS

let [<Import("StaticTests","#292-static-props")>] staticTests: StaticTests.IExports = jsNative

module StaticTests =

    type [<AllowNullLiteral>] IExports =
        abstract PropertiesClass: PropertiesClassStatic

    /// this class should contain 4 properties - 2 static ones, 2 instance ones. 
    type [<AllowNullLiteral>] PropertiesClass =
        abstract instanceProperty: float with get, set
        abstract publicInstanceProperty: float with get, set

    /// this class should contain 4 properties - 2 static ones, 2 instance ones. 
    type [<AllowNullLiteral>] PropertiesClassStatic =
        [<Emit "new $0($1...)">] abstract Create: unit -> PropertiesClass
        abstract staticProperty: float with get, set
        abstract publicStaticProperty: float with get, set
