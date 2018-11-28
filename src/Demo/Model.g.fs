namespace Demo.Model

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Demo.Model

[<AutoOpen>]
module Mutable =

    
    
    type MTrafoPrimitive(__initial : Demo.Model.TrafoPrimitive) =
        inherit obj()
        let mutable __current : Aardvark.Base.Incremental.IModRef<Demo.Model.TrafoPrimitive> = Aardvark.Base.Incremental.EqModRef<Demo.Model.TrafoPrimitive>(__initial) :> Aardvark.Base.Incremental.IModRef<Demo.Model.TrafoPrimitive>
        let _primitive = ResetMod.Create(__initial.primitive)
        let _trafo = ResetMod.Create(__initial.trafo)
        
        member x.primitive = _primitive :> IMod<_>
        member x.trafo = _trafo :> IMod<_>
        
        member x.Current = __current :> IMod<_>
        member x.Update(v : Demo.Model.TrafoPrimitive) =
            if not (System.Object.ReferenceEquals(__current.Value, v)) then
                __current.Value <- v
                
                ResetMod.Update(_primitive,v.primitive)
                ResetMod.Update(_trafo,v.trafo)
                
        
        static member Create(__initial : Demo.Model.TrafoPrimitive) : MTrafoPrimitive = MTrafoPrimitive(__initial)
        static member Update(m : MTrafoPrimitive, v : Demo.Model.TrafoPrimitive) = m.Update(v)
        
        override x.ToString() = __current.Value.ToString()
        member x.AsString = sprintf "%A" __current.Value
        interface IUpdatable<Demo.Model.TrafoPrimitive> with
            member x.Update v = x.Update v
    
    
    
    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module TrafoPrimitive =
        [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
        module Lens =
            let primitive =
                { new Lens<Demo.Model.TrafoPrimitive, Demo.Model.Primitive>() with
                    override x.Get(r) = r.primitive
                    override x.Set(r,v) = { r with primitive = v }
                    override x.Update(r,f) = { r with primitive = f r.primitive }
                }
            let trafo =
                { new Lens<Demo.Model.TrafoPrimitive, Aardvark.Base.Trafo3d>() with
                    override x.Get(r) = r.trafo
                    override x.Set(r,v) = { r with trafo = v }
                    override x.Update(r,f) = { r with trafo = f r.trafo }
                }
    
    
    type MModel(__initial : Demo.Model.Model) =
        inherit obj()
        let mutable __current : Aardvark.Base.Incremental.IModRef<Demo.Model.Model> = Aardvark.Base.Incremental.EqModRef<Demo.Model.Model>(__initial) :> Aardvark.Base.Incremental.IModRef<Demo.Model.Model>
        let _cameraState = Aardvark.UI.Primitives.Mutable.MCameraControllerState.Create(__initial.cameraState)
        
        member x.cameraState = _cameraState
        
        member x.Current = __current :> IMod<_>
        member x.Update(v : Demo.Model.Model) =
            if not (System.Object.ReferenceEquals(__current.Value, v)) then
                __current.Value <- v
                
                Aardvark.UI.Primitives.Mutable.MCameraControllerState.Update(_cameraState, v.cameraState)
                
        
        static member Create(__initial : Demo.Model.Model) : MModel = MModel(__initial)
        static member Update(m : MModel, v : Demo.Model.Model) = m.Update(v)
        
        override x.ToString() = __current.Value.ToString()
        member x.AsString = sprintf "%A" __current.Value
        interface IUpdatable<Demo.Model.Model> with
            member x.Update v = x.Update v
    
    
    
    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module Model =
        [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
        module Lens =
            let cameraState =
                { new Lens<Demo.Model.Model, Aardvark.UI.Primitives.CameraControllerState>() with
                    override x.Get(r) = r.cameraState
                    override x.Set(r,v) = { r with cameraState = v }
                    override x.Update(r,f) = { r with cameraState = f r.cameraState }
                }
