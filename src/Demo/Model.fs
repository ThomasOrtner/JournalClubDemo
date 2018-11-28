namespace Demo.Model

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Aardvark.Base.Rendering
open Aardvark.UI.Primitives
open Aardvark.UI

[<DomainType>]
type Model =
    {                
        cameraState     : CameraControllerState
        //add numeric model
    }