// ts2fable 0.0.0
module rec stringEnumWithPeriod
open System
open Fable.Core
open Fable.Import.JS


type [<StringEnum>] [<RequireQualifiedAccess>] Permissions =
    | Public_profile
    | User_friends
    | Email
    | User_about_me
    | [<CompiledName "user_actions.books">] User_actions_books
    | [<CompiledName "user_actions.fitness">] User_actions_fitness
    | [<CompiledName "user_actions.music">] User_actions_music
    | [<CompiledName "user_actions.news">] User_actions_news
    | [<CompiledName "user_actions.video">] User_actions_video
    | User_birthday
    | User_education_history
