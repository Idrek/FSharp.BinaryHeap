module BinaryHeap.Min

type BinaryHeap<'a> = Core.BinaryHeap<'a>
type Order = Core.Order

let shiftUp (iItem: int) (heap: BinaryHeap<'a>) : BinaryHeap<'a> =
    Core.shiftUpOrder Order.Min iItem heap

let push (item: 'a) (heap: BinaryHeap<'a>) : BinaryHeap<'a> =
    Core.pushOrder Order.Min item heap

let shiftDown (iItem: int) (heap: BinaryHeap<'a>) : BinaryHeap<'a> =
    Core.shiftDownOrder Order.Min iItem heap

let pop (heap: BinaryHeap<'a>) : Option<'a * BinaryHeap<'a>> =
    Core.popOrder Order.Min heap

let heapify (arr: array<'a>) : BinaryHeap<'a> =
    Core.heapifyOrder Order.Min arr

let pushPop (item: 'a) (heap: BinaryHeap<'a>) : 'a * BinaryHeap<'a> =
    Core.pushPopOrder Order.Min item heap

let replace (item: 'a) (heap: BinaryHeap<'a>) : Option<'a * BinaryHeap<'a>> =
    Core.replaceOrder Order.Min item heap

let update (f: 'a -> 'a) (heap: BinaryHeap<'a>) : Option<BinaryHeap<'a>> =
    Core.updateOrder Order.Min f heap    

let sort (xs: seq<'a>) : list<'a> =
    Core.sortOrder Order.Min xs    

let nsmallestBy (projection: 'a -> 'key) (n: int) (coll: seq<'a>) : array<'a> =
    Core.nArrangementBy Order.Max projection n coll 

let nsmallest (n: int) (coll: seq<'a>) : array<'a> =
    Core.nArrangementBy Order.Max id n coll

    