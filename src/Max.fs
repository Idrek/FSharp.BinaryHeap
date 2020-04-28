module BinaryHeap.Max

type BinaryHeap<'a> = Core.BinaryHeap<'a>
type Order = Core.Order

let shiftUp (iItem: int) (heap: BinaryHeap<'a>) : BinaryHeap<'a> =
    Core.shiftUpOrder Order.Max iItem heap

let push (item: 'a) (heap: BinaryHeap<'a>) : BinaryHeap<'a> =
    Core.pushOrder Order.Max item heap

let shiftDown (iItem: int) (heap: BinaryHeap<'a>) : BinaryHeap<'a> =
    Core.shiftDownOrder Order.Max iItem heap

let pop (heap: BinaryHeap<'a>) : Option<'a * BinaryHeap<'a>> =
    Core.popOrder Order.Max heap

let heapify (arr: array<'a>) : BinaryHeap<'a> =
    Core.heapifyOrder Order.Max arr

let pushPop (item: 'a) (heap: BinaryHeap<'a>) : 'a * BinaryHeap<'a> =
    Core.pushPopOrder Order.Max item heap

let replace (item: 'a) (heap: BinaryHeap<'a>) : Option<'a * BinaryHeap<'a>> =
    Core.replaceOrder Order.Max item heap 

let update (f: 'a -> 'a) (heap: BinaryHeap<'a>) : Option<BinaryHeap<'a>> =
    Core.updateOrder Order.Max f heap 

let sort (xs: seq<'a>) : list<'a> =
    Core.sortOrder Order.Max xs

let nlargestBy (projection: 'a -> 'key) (n: int) (coll: seq<'a>) : array<'a> =
    Core.nArrangementBy Order.Min projection n coll 

let nlargest (n: int) (coll: seq<'a>) : array<'a> =
    Core.nArrangementBy Order.Min id n coll

    