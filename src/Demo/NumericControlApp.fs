namespace Demo.Numeric

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Aardvark.UI
open Aardvark.UI.Primitives
open Aardvark.Base.Rendering
open Demo.Model

type NumericModel =
  {
    value : float
  }

type Message = 
    | DoNothing

module NumericControlApp =        

    let initial = 
      { 
        cameraState = FreeFlyController.initial; 
      }

    let update (m : Model) (msg : Message) =
        match msg with                                    
            | DoNothing -> m
                
    let view (m : MModel) =
                                                        
        require Html.semui (
          div [style "position: fixed; left: 20px; top: 20px"] [
              button [onClick (fun _ -> DoNothing)] [text "This button does nothing"]                
            ]
        )
      

    let app =
        {
            initial = initial
            update = update
            view = view
            threads = (fun _ -> ThreadPool.Empty)
            unpersist = Unpersist.instance
        }

