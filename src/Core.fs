module BinaryHeap.Core

type BinaryHeap<'a> = array<'a>
type Order = Min | Max

let private isEven (n: int) : bool = n &&& 1 = 0

let private swap (x: int) (y: int) (arr: array<'a>) : array<'a> =
    let length = Array.length arr
    match length with
    | 0 -> ()
    | length when x >= length || y >= length -> ()
    | _ ->
        let aux : 'a = arr.[x]
        arr.[x] <- arr.[y]
        arr.[y] <- aux 
    arr

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

let rec shiftUpOrder (order: Order) (iItem: int) (heap: BinaryHeap<'a>) : BinaryHeap<'a> =
    let childParentComparison : 'a -> 'a -> bool = 
        match order with 
        | Min -> (<=) 
        | Max -> (>=)
    match iItem, heap with
    | _, [||] -> empty
    | iItem, heap when iItem <= 0 -> heap
    | iItem, heap when iItem >= Array.length heap -> heap
    | iItem, heap ->
        let iParent : Option<int> = findParentIndex iItem
        match iParent with
        | None -> heap
        | Some iParent -> 
            if childParentComparison heap.[iParent] heap.[iItem]
            then heap
            else 
                let newHeap : BinaryHeap<'a> = swap iParent iItem heap
                shiftUpOrder order iParent newHeap

let pushOrder (order: Order) (item: 'a) (heap: BinaryHeap<'a>) : BinaryHeap<'a> =
    let shiftUp : int -> BinaryHeap<'a> -> BinaryHeap<'a> = 
        match order with | Min -> shiftUpOrder Min | Max -> shiftUpOrder Max 
    let heapWithItem : BinaryHeap<'a> = Array.append heap [|item|]
    let iLast : int = Array.length heapWithItem - 1
    shiftUp iLast heapWithItem

let shiftDownOrder (order: Order) (iItem: int) (heap: BinaryHeap<'a>) : BinaryHeap<'a> =
    let (comparison, shiftUp) = 
        match order with | Min -> ((>), shiftUpOrder Min) | Max -> ((<), shiftUpOrder Max)
    let iLast : int = Array.length heap - 1
    match iItem, heap with
    | _, [||] -> empty
    | iItem, heap when iItem < 0 -> heap
    | iItem, heap when iItem > iLast -> heap
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
                swap iItem iChild heap |> ignore
                loop iChild
        let iShift = loop iItem
        shiftUp iShift heap

let popOrder (order: Order) (heap: BinaryHeap<'a>) : Option<'a * BinaryHeap<'a>> =
    let shiftDown : int -> BinaryHeap<'a> -> BinaryHeap<'a> =
        match order with | Min -> shiftDownOrder Min | Max -> shiftDownOrder Max
    match heap with
    | [||] -> None
    | [|item|] -> Some (item, empty)
    | heap -> 
        let poppedItem : 'a = heap.[0]
        let lastItem : 'a = Array.last heap
        heap.[0] <- lastItem
        let heapWithoutLastItem : BinaryHeap<'a> = heap.[0 .. Array.length heap - 2]
        let fixedHeap : BinaryHeap<'a> = shiftDown 0 heapWithoutLastItem
        Some (poppedItem, fixedHeap)

let heapifyOrder (order: Order) (arr: array<'a>) : BinaryHeap<'a> =
    let shiftUp : int -> BinaryHeap<'a> -> BinaryHeap<'a> = 
        match order with | Min -> shiftUpOrder Min | Max -> shiftUpOrder Max
    let length : int = Array.length arr
    // TODO: Is necessary traverse the whole range?
    let range : array<int> = [|1 .. length - 1|]
    (arr, range) ||> Array.fold (fun heap iItem -> shiftUp iItem heap)

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

    