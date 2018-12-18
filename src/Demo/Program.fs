open Demo

open Aardium
open Aardvark.Service
open Aardvark.UI
open Suave
open Suave.WebPart
open Aardvark.Rendering.Vulkan
open Aardvark.Base
open System
open Demo.Numeric

//function
let timeTwo a = a * 2

let apply f a = f a

//type
type Person = { 
  firstName : string
  lastName : string
  }

[<EntryPoint>]
let main args =
    Ag.initialize()
    Aardvark.Init()
    Aardium.init()
    
    let app = new HeadlessVulkanApplication(true)

    WebPart.startServer 4321 [
    //    MutableApp.toWebPart' app.Runtime false (App.start App.app)
        MutableApp.toWebPart' app.Runtime false (App.start VectorControlApp.app)
    ]
    
    Aardium.run {
        title "Aardvark rocks \\o/"
        width 1024
        height 768
        url "http://localhost:4321/"
    }

    0
