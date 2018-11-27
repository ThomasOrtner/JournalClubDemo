namespace Aardvark.Media.Example

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Aardvark.UI.Primitives
open Aardvark.Media.Example.Boxes


[<DomainType>]
type Model =
    {
        boxes           : plist<VisibleBox>
        cameraState     : CameraControllerState
        dockConfig      : DockConfig
    }

module Model =
  let addBox m = 
    let i = m.boxes.Count                
    let box = Boxes.mkNthBox i (i+1) |> Boxes.mkVisibleBox Boxes.colors.[i % 5]                                 
    { m with boxes = PList.append box m.boxes }

  let removeBox m = 
    let i = m.boxes.Count - 1
    let boxes = PList.removeAt i m.boxes
    {m with boxes = boxes}