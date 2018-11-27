namespace Aardvark.Media.Example.Model

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Aardvark.Media.Example.Model

[<AutoOpen>]
module Mutable =

    
    
    type MModel(__initial : Aardvark.Media.Example.Model.Model) =
        inherit obj()
        let mutable __current : Aardvark.Base.Incremental.IModRef<Aardvark.Media.Example.Model.Model> = Aardvark.Base.Incremental.EqModRef<Aardvark.Media.Example.Model.Model>(__initial) :> Aardvark.Base.Incremental.IModRef<Aardvark.Media.Example.Model.Model>
        let _currentModel = ResetMod.Create(__initial.currentModel)
        let _cameraState = Aardvark.UI.Primitives.Mutable.MCameraControllerState.Create(__initial.cameraState)
        let _dockConfig = ResetMod.Create(__initial.dockConfig)
        
        member x.currentModel = _currentModel :> IMod<_>
        member x.cameraState = _cameraState
        member x.dockConfig = _dockConfig :> IMod<_>
        
        member x.Current = __current :> IMod<_>
        member x.Update(v : Aardvark.Media.Example.Model.Model) =
            if not (System.Object.ReferenceEquals(__current.Value, v)) then
                __current.Value <- v
                
                ResetMod.Update(_currentModel,v.currentModel)
                Aardvark.UI.Primitives.Mutable.MCameraControllerState.Update(_cameraState, v.cameraState)
                ResetMod.Update(_dockConfig,v.dockConfig)
                
        
        static member Create(__initial : Aardvark.Media.Example.Model.Model) : MModel = MModel(__initial)
        static member Update(m : MModel, v : Aardvark.Media.Example.Model.Model) = m.Update(v)
        
        override x.ToString() = __current.Value.ToString()
        member x.AsString = sprintf "%A" __current.Value
        interface IUpdatable<Aardvark.Media.Example.Model.Model> with
            member x.Update v = x.Update v
    
    
    
    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module Model =
        [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
        module Lens =
            let currentModel =
                { new Lens<Aardvark.Media.Example.Model.Model, Aardvark.Media.Example.Model.Primitive>() with
                    override x.Get(r) = r.currentModel
                    override x.Set(r,v) = { r with currentModel = v }
                    override x.Update(r,f) = { r with currentModel = f r.currentModel }
                }
            let cameraState =
                { new Lens<Aardvark.Media.Example.Model.Model, Aardvark.UI.Primitives.CameraControllerState>() with
                    override x.Get(r) = r.cameraState
                    override x.Set(r,v) = { r with cameraState = v }
                    override x.Update(r,f) = { r with cameraState = f r.cameraState }
                }
            let dockConfig =
                { new Lens<Aardvark.Media.Example.Model.Model, Aardvark.UI.Primitives.DockConfig>() with
                    override x.Get(r) = r.dockConfig
                    override x.Set(r,v) = { r with dockConfig = v }
                    override x.Update(r,f) = { r with dockConfig = f r.dockConfig }
                }
