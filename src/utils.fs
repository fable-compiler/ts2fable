[<AutoOpen>]
module ts2fable.Utils

module List =
  let sequenceOption ls =
    let folder ele state =
      ele
      |> Option.bind (fun ele ->
        state |> Option.map (fun state -> ele::state)
      )
    List.foldBack folder ls (Some [])
