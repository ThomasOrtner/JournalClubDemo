namespace Aardvark.Media.Example

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Aardvark.Media.Example

[<AutoOpen>]
module Mutable =

    
    
    type MModel(__initial : Aardvark.Media.Example.Model) =
        inherit obj()
        let mutable __current : Aardvark.Base.Incremental.IModRef<Aardvark.Media.Example.Model> = Aardvark.Base.Incremental.EqModRef<Aardvark.Media.Example.Model>(__initial) :> Aardvark.Base.Incremental.IModRef<Aardvark.Media.Example.Model>
        let _boxes = MList.Create(__initial.boxes, (fun v -> Aardvark.Media.Example.Boxes.Mutable.MVisibleBox.Create(v)), (fun (m,v) -> Aardvark.Media.Example.Boxes.Mutable.MVisibleBox.Update(m, v)), (fun v -> v))
        let _cameraState = Aardvark.UI.Primitives.Mutable.MCameraControllerState.Create(__initial.cameraState)
        let _dockConfig = ResetMod.Create(__initial.dockConfig)
        
        member x.boxes = _boxes :> alist<_>
        member x.cameraState = _cameraState
        member x.dockConfig = _dockConfig :> IMod<_>
        
        member x.Current = __current :> IMod<_>
        member x.Update(v : Aardvark.Media.Example.Model) =
            if not (System.Object.ReferenceEquals(__current.Value, v)) then
                __current.Value <- v
                
                MList.Update(_boxes, v.boxes)
                Aardvark.UI.Primitives.Mutable.MCameraControllerState.Update(_cameraState, v.cameraState)
                ResetMod.Update(_dockConfig,v.dockConfig)
                
        
        static member Create(__initial : Aardvark.Media.Example.Model) : MModel = MModel(__initial)
        static member Update(m : MModel, v : Aardvark.Media.Example.Model) = m.Update(v)
        
        override x.ToString() = __current.Value.ToString()
        member x.AsString = sprintf "%A" __current.Value
        interface IUpdatable<Aardvark.Media.Example.Model> with
            member x.Update v = x.Update v
    
    
    
    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module Model =
        [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
        module Lens =
            let boxes =
                { new Lens<Aardvark.Media.Example.Model, Aardvark.Base.plist<Aardvark.Media.Example.Boxes.VisibleBox>>() with
                    override x.Get(r) = r.boxes
                    override x.Set(r,v) = { r with boxes = v }
                    override x.Update(r,f) = { r with boxes = f r.boxes }
                }
            let cameraState =
                { new Lens<Aardvark.Media.Example.Model, Aardvark.UI.Primitives.CameraControllerState>() with
                    override x.Get(r) = r.cameraState
                    override x.Set(r,v) = { r with cameraState = v }
                    override x.Update(r,f) = { r with cameraState = f r.cameraState }
                }
            let dockConfig =
                { new Lens<Aardvark.Media.Example.Model, Aardvark.UI.Primitives.DockConfig>() with
                    override x.Get(r) = r.dockConfig
                    override x.Set(r,v) = { r with dockConfig = v }
                    override x.Update(r,f) = { r with dockConfig = f r.dockConfig }
                }
