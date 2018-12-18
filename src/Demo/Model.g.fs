namespace Demo.Model

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Demo.Model

[<AutoOpen>]
module Mutable =

    
    
    type MModel(__initial : Demo.Model.Model) =
        inherit obj()
        let mutable __current : Aardvark.Base.Incremental.IModRef<Demo.Model.Model> = Aardvark.Base.Incremental.EqModRef<Demo.Model.Model>(__initial) :> Aardvark.Base.Incremental.IModRef<Demo.Model.Model>
        let _x = Demo.Numeric.Mutable.MNumericModel.Create(__initial.x)
        
        member x.x = _x
        
        member x.Current = __current :> IMod<_>
        member x.Update(v : Demo.Model.Model) =
            if not (System.Object.ReferenceEquals(__current.Value, v)) then
                __current.Value <- v
                
                Demo.Numeric.Mutable.MNumericModel.Update(_x, v.x)
                
        
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
            let x =
                { new Lens<Demo.Model.Model, Demo.Numeric.NumericModel>() with
                    override x.Get(r) = r.x
                    override x.Set(r,v) = { r with x = v }
                    override x.Update(r,f) = { r with x = f r.x }
                }
