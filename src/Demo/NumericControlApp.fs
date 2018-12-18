namespace Demo.Numeric

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Aardvark.UI
open Aardvark.UI.Primitives
open Aardvark.Base.Rendering

type NumericAction = 
    | DoNothing

module NumericControlApp =        
    
    let update (m : NumericModel) (msg : NumericAction) =
        match msg with                                    
            | DoNothing -> m
                
    let view (m : MNumericModel) =
        require Html.semui (
          div [style "position: fixed; left: 20px; top: 20px"] [
              button [onClick (fun _ -> DoNothing)] [text "This button does nothing"]                
            ]
        )
      

    let app =
        {
            initial = { value = 0.0 }
            update = update
            view = view
            threads = (fun _ -> ThreadPool.Empty)
            unpersist = Unpersist.instance
        }

