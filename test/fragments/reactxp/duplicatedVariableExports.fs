// ts2fable 0.0.0
module rec duplicatedVariableExports
open System
open Fable.Core
open Fable.Import.JS

let [<Import("*","reactxp")>] reactXP: ReactXP.IExports = jsNative

module ReactXP =

    type [<AllowNullLiteral>] IExports =
        abstract Accessibility: RXInterfaces.Accessibility
        abstract ActivityIndicator: obj
        abstract Alert: RXInterfaces.Alert
        abstract App: RXInterfaces.App
        abstract Button: obj
        abstract Picker: obj
        abstract Clipboard: RXInterfaces.Clipboard
        abstract GestureView: obj
        abstract Image: RXInterfaces.ImageConstructor
        abstract Input: RXInterfaces.Input
        abstract International: RXInterfaces.International
        abstract Link: obj
        abstract Linking: RXInterfaces.Linking
        abstract Location: RXInterfaces.Location
        abstract Modal: RXInterfaces.Modal
        abstract Network: RXInterfaces.Network
        abstract Platform: RXInterfaces.Platform
        abstract Popup: RXInterfaces.Popup
        abstract ScrollView: RXInterfaces.ScrollViewConstructor
        abstract StatusBar: RXInterfaces.StatusBar
        abstract Storage: RXInterfaces.Storage
        abstract Styles: RXInterfaces.Styles
        abstract Text: obj
        abstract TextInput: obj
        abstract UserInterface: RXInterfaces.UserInterface
        abstract UserPresence: RXInterfaces.UserPresence
        abstract View: obj
        abstract WebView: RXInterfaces.WebViewConstructor
        abstract createElement: obj
        abstract Children: React.ReactChildren
        abstract __spread: obj option

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
