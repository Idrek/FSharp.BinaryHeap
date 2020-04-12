module BinaryHeap.Core

let private isEven (n: int) : bool = n &&& 1 = 0

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

