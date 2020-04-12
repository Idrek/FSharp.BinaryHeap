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

