﻿module BinaryHeap.Core

type BinaryHeap<'a> = array<'a>
type Order = Min | Max

let private isEven (n: int) : bool = n &&& 1 = 0

let private flip (f: 'b -> 'a -> 'c) (x: 'a) (y: 'b) = f y x

let swapInPlace (x: int) (y: int) (arr: array<'a>) : unit =
    let length = Array.length arr
    match length with
    | 0 -> ()
    | length when x >= length || y >= length -> ()
    | _ ->
        let aux : 'a = arr.[x]
        arr.[x] <- arr.[y]
        arr.[y] <- aux 

let findChildrenIndexes (iLast: int) (iParent: int) : (Option<int> * Option<int>) =
    let checkBoundary (iChild: int) = if iChild > iLast then None else Some iChild 
    let double : int = iParent * 2
    let iLeftChild : int = double + 1
    let iRightChild : int = double + 2
    (checkBoundary iLeftChild, checkBoundary iRightChild)

let findParentIndex (iChild: int) : Option<int> = 
    match iChild with
    | 0 -> None
    | iChild when isEven iChild -> Some <| (iChild - 1) / 2
    | iChild -> Some <| iChild / 2

let empty : BinaryHeap<'a> = Array.empty

let rec shiftUpOrderInPlace (order: Order) (iItem: int) (heap: BinaryHeap<'a>) : unit =
    let childParentComparison : 'a -> 'a -> bool = 
        match order with 
        | Min -> (<=) 
        | Max -> (>=)
    match iItem, heap with
    | _, [||] -> ()
    | iItem, heap when iItem <= 0 -> ()
    | iItem, heap when iItem >= Array.length heap -> ()
    | iItem, heap ->
        let iParent : Option<int> = findParentIndex iItem
        match iParent with
        | None -> ()
        | Some iParent -> 
            if childParentComparison heap.[iParent] heap.[iItem]
            then ()
            else 
                swapInPlace iParent iItem heap
                shiftUpOrderInPlace order iParent heap

let pushOrder (order: Order) (item: 'a) (heap: BinaryHeap<'a>) : BinaryHeap<'a> =
    let shiftUpInPlace : int -> BinaryHeap<'a> -> unit = 
        match order with | Min -> shiftUpOrderInPlace Min | Max -> shiftUpOrderInPlace Max 
    let heapWithItem : BinaryHeap<'a> = Array.append heap [|item|]
    let iLast : int = Array.length heapWithItem - 1
    shiftUpInPlace iLast heapWithItem
    heapWithItem

let shiftDownOrderInPlace (order: Order) (iItem: int) (heap: BinaryHeap<'a>) : unit =
    let (comparison, shiftUpInPlace) = 
        match order with | Min -> ((>), shiftUpOrderInPlace Min) | Max -> ((<), shiftUpOrderInPlace Max)
    let iLast : int = Array.length heap - 1
    match iItem, heap with
    | _, [||] -> ()
    | iItem, heap when iItem < 0 -> ()
    | iItem, heap when iItem > iLast -> ()
    | iItem, heap ->
        let rec loop iItem =
            let (iChildL, iChildR) = findChildrenIndexes iLast iItem
            let iChild = 
                match iChildL, iChildR with
                | None, None -> iItem
                | Some l, None -> l
                | Some l, Some r when comparison heap.[l] heap.[r] -> r
                | Some l, Some r -> l
                | _ -> failwith "BUG: Index of right child cannot be bigger than left child"
            // `iChild` should not never be lower than `iItem`, but the comparison is safer.
            if iChild <= iItem || comparison heap.[iChild] heap.[iItem] 
            then iItem
            else 
                swapInPlace iItem iChild heap
                loop iChild
        loop iItem |> ignore

let popOrder (order: Order) (heap: BinaryHeap<'a>) : Option<'a * BinaryHeap<'a>> =
    let shiftDownInPlace : int -> BinaryHeap<'a> -> unit =
        match order with | Min -> shiftDownOrderInPlace Min | Max -> shiftDownOrderInPlace Max
    match heap with
    | [||] -> None
    | [|item|] -> Some (item, empty)
    | heap -> 
        let poppedItem : 'a = heap.[0]
        let lastItem : 'a = Array.last heap
        let heapWithoutLastItem : BinaryHeap<'a> = heap.[0 .. Array.length heap - 2]
        heapWithoutLastItem.[0] <- lastItem
        shiftDownInPlace 0 heapWithoutLastItem
        Some (poppedItem, heapWithoutLastItem)

let heapifyOrderInPlace (order: Order) (arr: array<'a>) : unit =
    let shiftUp : int -> BinaryHeap<'a> -> BinaryHeap<'a> = 
        match order with | Min -> shiftUpOrder Min | Max -> shiftUpOrder Max
    let length : int = Array.length arr
    // TODO: Is necessary traverse the whole range?
    let range : array<int> = [|1 .. length - 1|]
    (arr, range) ||> Array.fold (fun heap iItem -> shiftUp iItem heap) |> ignore

let heapifyOrder (order: Order) (arr: array<'a>) : BinaryHeap<'a> =
    let copy : array<'a> = Array.copy arr
    heapifyOrderInPlace order copy
    copy

let pushPopOrder (order: Order) (item: 'a) (heap: BinaryHeap<'a>) : 'a * BinaryHeap<'a> =
    let (push, pop, comparison) = 
        match order with 
        | Min -> (pushOrder Min, popOrder Min, (>=)) 
        | Max -> (pushOrder Max, popOrder Max, (<=))
    match heap with
    | [||] -> (item, heap)
    | [|x|] when comparison x item -> (item, heap)
    | heap ->
        let pushedHeap : BinaryHeap<'a> = push item heap
        let popped = pop pushedHeap
        match popped with
        | None -> failwith "BUG: Pop after push should always find an element"
        | Some poppedItemAndHeap -> poppedItemAndHeap

let replaceOrder (order: Order) (item: 'a) (heap: BinaryHeap<'a>) : Option<'a * BinaryHeap<'a>> =
    let (push, pop) = 
        match order with 
        | Min -> (pushOrder Min, popOrder Min) 
        | Max -> (pushOrder Max, popOrder Max)
    match heap with
    | [||] -> None
    | heap ->
        let popped : Option<'a * BinaryHeap<'a>> = pop heap
        match popped with
        | None -> None
        | Some (poppedItem, poppedHeap) ->
            let popPushedHeap = push item poppedHeap
            Some (poppedItem, popPushedHeap)

let updateOrder (order: Order) (f: 'a -> 'a) (heap: BinaryHeap<'a>) : Option<BinaryHeap<'a>> =
    let (push, pop) = 
        match order with 
        | Min -> (pushOrder Min, popOrder Min) 
        | Max -> (pushOrder Max, popOrder Max)
    match heap with
    | [||] -> None
    | heap ->
        let popped : Option<'a * BinaryHeap<'a>> = pop heap
        match popped with
        | None -> None
        | Some (poppedItem, poppedHeap) ->
            let uItem : 'a = f poppedItem
            let uHeap : BinaryHeap<'a> = push uItem poppedHeap
            Some uHeap

let sortOrder (order: Order) (xs: seq<'a>) : list<'a> =
    let (push, pop) = 
        match order with 
        | Min -> (pushOrder Min, popOrder Min) 
        | Max -> (pushOrder Max, popOrder Max)
    let heap : BinaryHeap<'a> = (empty, xs) ||> Seq.fold (fun heap x -> push x heap)
    let rec loop heap =
        let popped = pop heap
        match popped with
        | None -> []
        | Some (item, poppedHeap) -> item :: loop poppedHeap
    loop heap

let private arrayToKeyIndexedHeap 
        (order: Order) 
        (projection: 'a -> 'key) 
        (arr: array<'a>) 
        : BinaryHeap<'key * int * 'a> =
    let length : int = Array.length arr
    let range : array<int> = 
        match order with 
        | Min -> [|0 .. length - 1|] 
        | Max -> [|0 .. -1 .. -length + 1|]
    arr
    |> Array.zip range
    |> Array.map (fun (i, item) -> (projection item, i, item)) 
    |> heapifyOrder order

let nArrangementBy (order: Order) (projection: 'a -> 'key) (n: int) (coll: seq<'a>) : array<'a> =
    let (sortingArr, sortingBinaryHeap, keyComparison, next) = 
        match order with
        | Max -> (Array.sort, Array.sort, (>=), ((+) 1))
        | Min -> (Array.sortDescending, Array.sortDescending, (<=), (flip (-) 1))
    let arr : array<'a> = Seq.toArray coll
    let iLast : int = Array.length arr - 1
    match n, arr with
    | _, [||] -> Array.empty
    | n, _ when n <= 0 -> Array.empty
    | n, arr when n > iLast -> sortingArr arr
    | n, arr ->
        let arrToHeapConversion : array<'a> = Array.take n arr
        let heap : BinaryHeap<'key * int * 'a> = 
            arrayToKeyIndexedHeap order projection arrToHeapConversion
        let top : 'key = heap.[0] |> (fun (key, index, item) -> key)
        let rec loop i top heap n =
            if i > iLast
            then heap
            else
                let key = projection arr.[i]
                match i with
                | i when keyComparison key top -> loop (i + 1) top heap n
                | i ->
                    let replacedHeap = replaceOrder order (key, n, arr.[i]) heap
                    match replacedHeap with
                    | None -> failwith "BUG: Empty heap found, although it was checked at the beginning of the function"
                    | Some (item, uHeap) ->
                        let uTop = uHeap.[0] |> (fun (key, index, item) -> key)
                        let uN = next n
                        loop (i + 1) uTop uHeap uN
        loop n top heap n |> sortingBinaryHeap |> Array.map (fun (key, index, item) -> item)

        