namespace Aardvark.Media.Example.Boxes

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Aardvark.Media.Example.Boxes

[<AutoOpen>]
module Mutable =

    
    
    type MVisibleBox(__initial : Aardvark.Media.Example.Boxes.VisibleBox) =
        inherit obj()
        let mutable __current : Aardvark.Base.Incremental.IModRef<Aardvark.Media.Example.Boxes.VisibleBox> = Aardvark.Base.Incremental.EqModRef<Aardvark.Media.Example.Boxes.VisibleBox>(__initial) :> Aardvark.Base.Incremental.IModRef<Aardvark.Media.Example.Boxes.VisibleBox>
        let _geometry = ResetMod.Create(__initial.geometry)
        let _color = ResetMod.Create(__initial.color)
        
        member x.geometry = _geometry :> IMod<_>
        member x.color = _color :> IMod<_>
        member x.id = __current.Value.id
        
        member x.Current = __current :> IMod<_>
        member x.Update(v : Aardvark.Media.Example.Boxes.VisibleBox) =
            if not (System.Object.ReferenceEquals(__current.Value, v)) then
                __current.Value <- v
                
                ResetMod.Update(_geometry,v.geometry)
                ResetMod.Update(_color,v.color)
                
        
        static member Create(__initial : Aardvark.Media.Example.Boxes.VisibleBox) : MVisibleBox = MVisibleBox(__initial)
        static member Update(m : MVisibleBox, v : Aardvark.Media.Example.Boxes.VisibleBox) = m.Update(v)
        
        override x.ToString() = __current.Value.ToString()
        member x.AsString = sprintf "%A" __current.Value
        interface IUpdatable<Aardvark.Media.Example.Boxes.VisibleBox> with
            member x.Update v = x.Update v
    
    
    
    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module VisibleBox =
        [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
        module Lens =
            let geometry =
                { new Lens<Aardvark.Media.Example.Boxes.VisibleBox, Aardvark.Base.Box3d>() with
                    override x.Get(r) = r.geometry
                    override x.Set(r,v) = { r with geometry = v }
                    override x.Update(r,f) = { r with geometry = f r.geometry }
                }
            let color =
                { new Lens<Aardvark.Media.Example.Boxes.VisibleBox, Aardvark.Base.C4b>() with
                    override x.Get(r) = r.color
                    override x.Set(r,v) = { r with color = v }
                    override x.Update(r,f) = { r with color = f r.color }
                }
            let id =
                { new Lens<Aardvark.Media.Example.Boxes.VisibleBox, System.String>() with
                    override x.Get(r) = r.id
                    override x.Set(r,v) = { r with id = v }
                    override x.Update(r,f) = { r with id = f r.id }
                }
