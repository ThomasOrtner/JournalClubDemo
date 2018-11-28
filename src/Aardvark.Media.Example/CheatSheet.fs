namespace Aardvark.Media.Example

open Aardvark.UI
open Aardvark.Media.Example.Boxes
open Aardvark.Base.Incremental

module CheatSheet =

  let mkColor (model : MModel) (box : MVisibleBox) =
      let id = box.id 
  
      let color =  box.color
          //model.selectedBoxes 
          //    |> ASet.contains id 
          //    |> Mod.bind (function 
          //        | true -> Mod.constant Primitives.selectionColor 
          //        | false -> box.color
          //      )
  
      //let color = 
      //    model.boxHovered |> Mod.bind (function 
      //        | Some k -> if k = id then Mod.constant Primitives.hoverColor else color
      //        | None -> color
      //    )
  
      color

  let drawList (m:MModel) = 
    Incremental.div 
        (AttributeMap.ofList [clazz "ui divided list"]) (
            alist {                                
                for b in m.boxes do
                    let! c = mkColor m b
                    let bgc = sprintf "background: %s" (Html.ofC4b c)
                        
                    yield 
                        div [
                            clazz "item"; style bgc; 
                            //onClick(fun _ -> Select b.id)
                            //onMouseEnter(fun _ -> Enter b.id)
                            //onMouseLeave(fun _ -> Exit)
                         ] [
                            i [clazz "file outline middle aligned icon"][]
                         ]                                                                    
            }     
      )