namespace Demo

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Aardvark.UI
open Aardvark.UI.Primitives
open Aardvark.Base.Rendering
open Demo.Model

type Message =    
    | CameraMessage  of FreeFlyController.Message
    //numeric action

module App =        

    let initial = 
      { 
        cameraState = FreeFlyController.initial; 
      }

    let update (m : Model) (msg : Message) =
        match msg with                                    
            | CameraMessage msg ->
                { m with cameraState = FreeFlyController.update m.cameraState msg }
            //react to numerica action

    let view (m : MModel) =

        let frustum = 
            Frustum.perspective 90.0 0.1 100.0 1.0 
                |> Mod.constant

        let drawSg m =
          Sg.box (Mod.constant C4b.Red) (Mod.constant (Box3d(-V3d.III, V3d.III)))                                 
            |> Sg.shader {
                do! DefaultSurfaces.trafo
                do! DefaultSurfaces.simpleLighting
            }
                        
        let att =
            [
                style "position: fixed; left: 0; top: 0; width: 100%; height: 100%"
                attribute "data-samples" "8"
            ]

        body [] [
            FreeFlyController.controlledControl m.cameraState CameraMessage frustum (AttributeMap.ofList att) (drawSg m)

            div [style "position: fixed; left: 20px; top: 20px"] [
                //button [onClick (fun _ -> ToggleModel)] [text "Toggle Model"]                
              ]
        ]

    let app =
        {
            initial = initial
            update = update
            view = view
            threads = Model.Lens.cameraState.Get >> FreeFlyController.threads >> ThreadPool.map CameraMessage
            unpersist = Unpersist.instance
        }