# FSharp.BinaryHeap

A binary heap data structure with an API similar to Python's [heapq module](https://docs.python.org/3.8/library/heapq.html)

## Installation

From Nuget:
```
dotnet add package FSharp.BinaryHeap --version 0.0.1
```

## Usage

A snippet of code:

```
module Program

open BinaryHeap.Min

[<EntryPoint>]
let main _ = 
    nsmallest 4 [|6; 1; 9; 5; 4; 3|] |> printfn "Output: %A"     // [|1; 3; 4; 5|]
    0
```

Look into the tests for examples of API functions.

## API

Min heap:
```
module BinaryHeap.Min

type BinaryHeap<'a> = Core.BinaryHeap<'a>

let shiftUp (iItem: int) (heap: BinaryHeap<'a>) : BinaryHeap<'a>
let push (item: 'a) (heap: BinaryHeap<'a>) : BinaryHeap<'a>
let shiftDown (iItem: int) (heap: BinaryHeap<'a>) : BinaryHeap<'a>
let pop (heap: BinaryHeap<'a>) : Option<'a * BinaryHeap<'a>>
let heapify (arr: array<'a>) : BinaryHeap<'a>
let pushPop (item: 'a) (heap: BinaryHeap<'a>) : 'a * BinaryHeap<'a>
let replace (item: 'a) (heap: BinaryHeap<'a>) : Option<'a * BinaryHeap<'a>>
let sort (xs: seq<'a>) : list<'a>
let nsmallestBy (projection: 'a -> 'key) (n: int) (coll: seq<'a>) : array<'a>
let nsmallest (n: int) (coll: seq<'a>) : array<'a>
```

Max heap:
```
module BinaryHeap.Max

type BinaryHeap<'a> = Core.BinaryHeap<'a>

let shiftUp (iItem: int) (heap: BinaryHeap<'a>) : BinaryHeap<'a>
let push (item: 'a) (heap: BinaryHeap<'a>) : BinaryHeap<'a>
let shiftDown (iItem: int) (heap: BinaryHeap<'a>) : BinaryHeap<'a>
let pop (heap: BinaryHeap<'a>) : Option<'a * BinaryHeap<'a>>
let heapify (arr: array<'a>) : BinaryHeap<'a>
let pushPop (item: 'a) (heap: BinaryHeap<'a>) : 'a * BinaryHeap<'a>
let replace (item: 'a) (heap: BinaryHeap<'a>) : Option<'a * BinaryHeap<'a>>
let sort (xs: seq<'a>) : list<'a>
let nlargestBy (projection: 'a -> 'key) (n: int) (coll: seq<'a>) : array<'a>
let nlargest (n: int) (coll: seq<'a>) : array<'a>
```