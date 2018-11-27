namespace Handson

module AttisExamples =
  let add a b = a + b
  let mul a b = a * b
  
  let bla = 
      234234 |> add 3 
             |> mul 2 
             |> string 
             |> String.length
  
  let bla2 = 
       add 3 
         >> mul 2 
         >> string 
         >> String.length    
  
  let bla3 = [1;2;3;4] |> List.map ( fun v -> v * 2 )
  let bla4 = [1;2;3;4] |> List.fold ( fun v acc -> v + acc ) 0
  
  type BinTree<'a> =
      | Empty
      | Node of 'a * BinTree<'a> * BinTree<'a>
     
  
  let rec insert (v : 'a) (t : BinTree<'a>) : BinTree<'a> =
      match t with    
          | Empty -> Node(v, Empty, Empty)
          | Node(a,l,r) ->
              if a = v then t
              elif v < a then Node(a,insert v l, r)
              else Node(a,l,insert v r)
  
  let rec exist (v : 'a) (t : BinTree<'a>) = 
      match t with
          | Empty -> false
          | Node(a,l,r) ->
              if v = a then true
              elif v < a then exist v l
              else exist v r

module HarrisExamples = 
  let cons (x : 'a) (xs : list<'a>) = 
    x :: xs

  let rec addEnd (e : 'a) (xs : list<'a>) =
      match xs with
          | x::xs -> x::addEnd e xs
          | [] -> [e]
  
  let rec sum (xs : list<int>) = 
      match xs with
          | [] -> 0
          | x::xs -> x + sum xs
  
  let rec sumBy (f : 'a -> int) (xs : list<'a>) = 
      match xs with
          | [] -> 0
          | x::xs -> f x + sumBy f xs
  
  let sumBy2 = sumBy id
  
  let flip f a b = f b a
  
  
  let rec foldr f z xs = // aka List.foldBack
      match xs with   
          | x::xs -> f x (foldr f z xs) 
          | []-> z
  
  
  let rec foldl f z xs = // aka List.fold
      match xs with
          | x::xs -> 
              let z = f z x
              foldl f z xs
          | [] -> z
  
  let folded = foldl (fun acc x -> x :: acc) [] [1;2;3]
  let foldBacked = foldr (fun x acc -> x :: acc) [] [1;2;3]
  
  let rec revList3 xs =
      match xs with
          | [] -> []
          | x::xs -> revList3 xs @ [x]
  
  let rec revList4 xs acc =
      match xs with   
          | [] -> acc
          | x::xs -> revList4 xs (x :: acc)
          
  let rev2 xs = List.fold (flip cons) [] xs
  
  let reverseList xs = foldl (flip cons) [] xs
  
  let collect xs = foldr (@) [] xs
  
  let b = collect [[1;2];[3;4]]

