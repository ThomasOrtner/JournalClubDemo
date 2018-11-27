namespace Aardvark.Media.Example

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Aardvark.UI
open Aardvark.UI.Primitives
open Aardvark.Base.Rendering
open Aardvark.Media.Example
open Aardvark.Media.Example.Boxes

type Message =
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

    let initial = 
      { 
        boxes = Boxes.mkBoxes 3 |> List.mapi (fun i k -> Boxes.mkVisibleBox Boxes.colors.[i % 5] k) |> PList.ofList
        cameraState = FreeFlyController.initial
        dockConfig = initialDocking 
      }

    let update (m : Model) (msg : Message) =
      match msg with              
        | CameraMessage msg ->
            { m with cameraState = FreeFlyController.update m.cameraState msg }
        | UpdateConfig cfg ->
            { m with dockConfig = cfg }
            

    let view (m : MModel) =

        let frustum = 
            Frustum.perspective 60.0 0.1 100.0 1.0 
                |> Mod.constant

        let drawBox (box: MVisibleBox) : ISg<Message> =
          Sg.box box.color box.geometry
            |> Sg.shader {
                do! DefaultSurfaces.trafo
                do! DefaultSurfaces.simpleLighting
            }

        let drawBoxes (m : MModel) = 
          m.boxes |> AList.map drawBox |> AList.toASet |> Sg.set

        let att = 
          [ 
            style "position: fixed; left: 0; top: 0; width: 100%; height: 100%" 
            attribute "data-samples" "8"
          ]

        let showRenderControl = 
          body [] [
              FreeFlyController.controlledControl m.cameraState CameraMessage frustum (AttributeMap.ofList att) (drawBoxes m)          
              div [style "position: fixed; left: 20px; top: 20px"] [
                  //button [] [text "Toggle Model"]
              ]          
          ]

        let showControls = 
           let guiStyle = style "width: 100%; height:100%; background: transparent; overflow-x: hidden; overflow-y: scroll"
           body[guiStyle] [
             text "Where's my UI"
           ]

        page(fun request -> 
          match Map.tryFind "page" request.queryParams with
            | Some "render" -> 
              require Html.semui (showRenderControl)
            | Some "controls" -> 
              
              require Html.semui (showControls)              
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