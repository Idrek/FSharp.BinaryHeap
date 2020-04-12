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

