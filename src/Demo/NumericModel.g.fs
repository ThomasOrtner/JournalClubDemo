namespace Demo.Numeric

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Demo.Numeric

[<AutoOpen>]
module Mutable =

    
    
    type MNumericModel(__initial : Demo.Numeric.NumericModel) =
        inherit obj()
        let mutable __current : Aardvark.Base.Incremental.IModRef<Demo.Numeric.NumericModel> = Aardvark.Base.Incremental.EqModRef<Demo.Numeric.NumericModel>(__initial) :> Aardvark.Base.Incremental.IModRef<Demo.Numeric.NumericModel>
        let _value = ResetMod.Create(__initial.value)
        
        member x.value = _value :> IMod<_>
        
        member x.Current = __current :> IMod<_>
        member x.Update(v : Demo.Numeric.NumericModel) =
            if not (System.Object.ReferenceEquals(__current.Value, v)) then
                __current.Value <- v
                
                ResetMod.Update(_value,v.value)
                
        
        static member Create(__initial : Demo.Numeric.NumericModel) : MNumericModel = MNumericModel(__initial)
        static member Update(m : MNumericModel, v : Demo.Numeric.NumericModel) = m.Update(v)
        
        override x.ToString() = __current.Value.ToString()
        member x.AsString = sprintf "%A" __current.Value
        interface IUpdatable<Demo.Numeric.NumericModel> with
            member x.Update v = x.Update v
    
    
    
    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module NumericModel =
        [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
        module Lens =
            let value =
                { new Lens<Demo.Numeric.NumericModel, System.Double>() with
                    override x.Get(r) = r.value
                    override x.Set(r,v) = { r with value = v }
                    override x.Update(r,f) = { r with value = f r.value }
                }
