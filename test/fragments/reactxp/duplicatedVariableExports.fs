// ts2fable 0.0.0
module rec duplicatedVariableExports
open System
open Fable.Core
open Fable.Core.JS

let [<Import("*","reactxp")>] reactXP: ReactXP.IExports = jsNative

module ReactXP =

    type [<AllowNullLiteral>] IExports =
        abstract Accessibility: RXInterfaces.Accessibility with get, set
        abstract ActivityIndicator: obj with get, set
        abstract Alert: RXInterfaces.Alert with get, set
        abstract App: RXInterfaces.App with get, set
        abstract Button: obj with get, set
        abstract Picker: obj with get, set
        abstract Clipboard: RXInterfaces.Clipboard with get, set
        abstract GestureView: obj with get, set
        abstract Image: RXInterfaces.ImageConstructor with get, set
        abstract Input: RXInterfaces.Input with get, set
        abstract International: RXInterfaces.International with get, set
        abstract Link: obj with get, set
        abstract Linking: RXInterfaces.Linking with get, set
        abstract Location: RXInterfaces.Location with get, set
        abstract Modal: RXInterfaces.Modal with get, set
        abstract Network: RXInterfaces.Network with get, set
        abstract Platform: RXInterfaces.Platform with get, set
        abstract Popup: RXInterfaces.Popup with get, set
        abstract ScrollView: RXInterfaces.ScrollViewConstructor with get, set
        abstract StatusBar: RXInterfaces.StatusBar with get, set
        abstract Storage: RXInterfaces.Storage with get, set
        abstract Styles: RXInterfaces.Styles with get, set
        abstract Text: obj with get, set
        abstract TextInput: obj with get, set
        abstract UserInterface: RXInterfaces.UserInterface with get, set
        abstract UserPresence: RXInterfaces.UserPresence with get, set
        abstract View: obj with get, set
        abstract WebView: RXInterfaces.WebViewConstructor with get, set
        abstract __spread: obj option with get, set

    type Accessibility =
        RXInterfaces.Accessibility

    type ActivityIndicator =
        RXInterfaces.ActivityIndicator

    type Alert =
        RXInterfaces.Alert

    type App =
        RXInterfaces.App

    type Button =
        RXInterfaces.Button

    type Picker =
        RXInterfaces.Picker

    type Clipboard =
        RXInterfaces.Clipboard

    type GestureView =
        RXInterfaces.GestureView

    type Image =
        RXInterfaces.Image

    type Input =
        RXInterfaces.Input

    type International =
        RXInterfaces.International

    type Link =
        RXInterfaces.Link

    type Linking =
        RXInterfaces.Linking

    type Location =
        RXInterfaces.Location

    type Modal =
        RXInterfaces.Modal

    type Network =
        RXInterfaces.Network

    type Platform =
        RXInterfaces.Platform

    type Popup =
        RXInterfaces.Popup

    type ScrollView =
        RXInterfaces.ScrollView

    type StatusBar =
        RXInterfaces.StatusBar

    type Storage =
        RXInterfaces.Storage

    type Styles =
        RXInterfaces.Styles

    type Text =
        RXInterfaces.Text

    type TextInput =
        RXInterfaces.TextInput

    type UserInterface =
        RXInterfaces.UserInterface

    type UserPresence =
        RXInterfaces.UserPresence

    type View =
        RXInterfaces.View

    type WebView =
        RXInterfaces.WebView
