module BinaryHeap.Min

type BinaryHeap<'a> = Core.BinaryHeap<'a>
type Order = Core.Order

let shiftUp (iItem: int) (heap: BinaryHeap<'a>) : BinaryHeap<'a> =
    Core.shiftUpOrder Order.Min iItem heap

let push (item: 'a) (heap: BinaryHeap<'a>) : BinaryHeap<'a> =
    Core.pushOrder Order.Min item heap

