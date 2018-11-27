namespace Handson

module OrtisExamples = 
  open System

  //functions
  let add a b = 
    a + b

  let mult a b = 
    a * b

  //piping
  let dostuff = 
    12345 
      |> add 10 
      |> mult 5 
      |> sprintf "%A" 
      |> Console.WriteLine

  //composition
  let superFunc =
    add 10 
      >> mult 5 
      >> sprintf "%A" 
      >> Console.WriteLine

  let dostuff2 =
    12345 |> superFunc
   
  //list map
  let dostuff3 =
    [0 .. 1 .. 10] |> List.map(fun x -> x |> mult 2) |> List.sum

  //imperative sum
  let sum xs = 
    let mutable sum = 0
    for v in xs do
      sum <- sum + v
    sum

  //recursive sum
  let rec sum2 (xs : list<int>) : int = 
    match xs with
    | [] -> 0
    | x::xs -> x + sum2 xs

  let dostuff4 =
    [0 .. 1 .. 10] |> sum  |> printfn "%A"
    [0 .. 1 .. 10] |> sum2 |> printfn "%A"
    [0 .. 1 .. 10] |> List.fold (fun v acc -> v + acc) 0 |> printfn "%A"
    [0 .. 1 .. 10] |> List.fold (fun v acc -> min v acc) Int32.MaxValue |> printfn "%A"

    
  //bintree of a
  type BinaryTree<'a> =
    | Empty 
    | Node of 'a * BinaryTree<'a> * BinaryTree<'a>

  //insert in bintree
  let rec insert (v:'a) (t:BinaryTree<'a>) : BinaryTree<'a> =
    match t with 
      | Empty -> Node(v, Empty, Empty)
      | Node (a, l, r) ->
        if a = v then t
        elif a < v then Node(a, insert v l, r)
        else Node (a, l, insert v r)  

  let doStuff5 =
    let tree = [0 .. 3 .. 10] |> List.fold (fun v acc -> v |> AttisExamples.insert acc) AttisExamples.BinTree.Empty
    [0 .. 1 .. 10] |> List.map (fun v -> AttisExamples.exist v tree)
