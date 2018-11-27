namespace Aardvark.Media.Example.Boxes

open System
open Aardvark.Base
open Aardvark.Base.Incremental

[<DomainType>]
type VisibleBox = {
  geometry : Box3d
  color    : C4b    
  
  [<NonIncremental; PrimaryKey>]
  id : string
}

module Boxes =
  

  let hoverColor = C4b.Blue
  let selectionColor = C4b.Red
  let colors = [new C4b(166,206,227); new C4b(178,223,138); new C4b(251,154,153); new C4b(253,191,111); new C4b(202,178,214)]
  let colorsBlue = [new C4b(241,238,246); new C4b(189,201,225); new C4b(116,169,207); new C4b(43,140,190); new C4b(4,90,141)]
  
  let mkNthBox i n = 
      let min = -V3d.One
      let max =  V3d.One
  
      let offset = 0.0 * (float n) * V3d.IOO
  
      new Box3d(min + V3d.IOO * 2.5 * (float i) - offset, max + V3d.IOO * 2.5 * (float i) - offset)
  
  let mkBoxes number =        
      [0..number-1] |> List.map (function x -> mkNthBox x number)

  let mkVisibleBox (color : C4b) (box : Box3d) : VisibleBox = 
    {
      id = Guid.NewGuid().ToString()
      geometry = box
      color = color           
    }

