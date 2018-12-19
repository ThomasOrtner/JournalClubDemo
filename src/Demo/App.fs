namespace Demo

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Aardvark.UI
open Aardvark.UI.Primitives
open Aardvark.Base.Rendering
open Demo.Model
open Demo.Numeric

type Message =    
    | SetNumeric of NumericAction
    //numeric action

module VectorControlApp =

    let initial = 
      { 
        x = { value = 0.0 }
      }

    let update (m : Model) (msg : Message) =
      match msg with
        | SetNumeric msg -> m

    let view (m : MModel) =
      body [] [        
        div [style "position: fixed; left: 20px; top: 20px"] [
          button [onClick (fun _ -> SetNumeric NumericAction.DoNothing)] [text "Toggle Model"]
          NumericControlApp.view m.x |> UI.map SetNumeric
          NumericControlApp.view m.x |> UI.map SetNumeric
          NumericControlApp.view m.x |> UI.map SetNumeric
        ]
      ]

    let app =
      {
        initial   = initial
        update    = update
        view      = view
        threads   = (fun _ -> ThreadPool.empty)
        unpersist = Unpersist.instance
      }