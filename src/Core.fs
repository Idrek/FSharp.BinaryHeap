module BinaryHeap.Core

let findChildrenIndexes (iLast: int) (iParent: int) : (Option<int> * Option<int>) =
    let checkBoundary (iChild: int) = if iChild > iLast then None else Some iChild 
    let double : int = iParent * 2
    let iLeftChild : int = double + 1
    let iRightChild : int = double + 2
    (checkBoundary iLeftChild, checkBoundary iRightChild)
