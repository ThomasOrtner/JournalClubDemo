namespace Demo.Model

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open Aardvark.Base.Rendering
open Aardvark.UI.Primitives
open Aardvark.UI
open Demo.Numeric

[<DomainType>]
type Model =
    {                
        x : NumericModel
    }