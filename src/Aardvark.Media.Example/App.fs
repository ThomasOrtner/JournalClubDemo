namespace Aardvark.Media.Example

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Aardvark.UI
open Aardvark.UI.Primitives
open Aardvark.Base.Rendering
open Aardvark.Media.Example.Model

type Message =
    | ToggleModel
    | CameraMessage of FreeFlyController.Message
    | UpdateConfig of DockConfig

module App =
    
    let initialDocking = 
      config {
        content (
            horizontal 10.0 [
                element { id "render"; title "Render View"; weight 20 }
                element { id "controls"; title "Controls"; weight 5 }                
            ]
        )
        appName "JCDemo"
        useCachedConfig false
      }

    let initial = { currentModel = Box; cameraState = FreeFlyController.initial; dockConfig = initialDocking }

    let update (m : Model) (msg : Message) =
        match msg with
            | ToggleModel -> 
                match m.currentModel with
                    | Box -> { m with currentModel = Sphere }
                    | Sphere -> { m with currentModel = Box }            
            | CameraMessage msg ->
                { m with cameraState = FreeFlyController.update m.cameraState msg }
            | UpdateConfig cfg ->
                { m with dockConfig = cfg }
            

    let view (m : MModel) =

        let frustum = 
            Frustum.perspective 60.0 0.1 100.0 1.0 
                |> Mod.constant

        let sg =
            m.currentModel |> Mod.map (fun v ->
                match v with
                    | Box -> Sg.box (Mod.constant C4b.Red) (Mod.constant (Box3d(-V3d.III, V3d.III)))
                    | Sphere -> Sg.sphere 5 (Mod.constant C4b.Green) (Mod.constant 1.0)
            )
            |> Sg.dynamic
            |> Sg.shader {
                do! DefaultSurfaces.trafo
                do! DefaultSurfaces.simpleLighting
            }

        let att =
            [
                style "position: fixed; left: 0; top: 0; width: 100%; height: 100%"
            ]

        let showRenderControl = 
          body [] [
              FreeFlyController.controlledControl m.cameraState CameraMessage frustum (AttributeMap.ofList att) sg
          
              div [style "position: fixed; left: 20px; top: 20px"] [
                  button [onClick (fun _ -> ToggleModel)] [text "Toggle Model"]
              ]
          
          ]



        page(fun request -> 
          match Map.tryFind "page" request.queryParams with
            | Some "render" -> 
              require Html.semui (showRenderControl)
            | Some "controls" -> 
              let guiStyle = style "width: 100%; height:100%; background: transparent; overflow-x: hidden; overflow-y: scroll"
              require Html.semui (
                body[guiStyle] [text "Where's my UI"]
              )              
            | Some s ->
              s |> sprintf "invalid page %s"|> failwith
            | None -> 
              m.dockConfig |> Mod.force |> Mod.constant |> docking [
                    style "width:100%;height:100%;"
                    onLayoutChanged UpdateConfig
              ]
        )        

    let app =
        {
            initial = initial
            update = update
            view = view
            threads = Model.Lens.cameraState.Get >> FreeFlyController.threads >> ThreadPool.map CameraMessage
            unpersist = Unpersist.instance
        }