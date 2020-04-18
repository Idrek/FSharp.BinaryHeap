# FSharp.BinaryHeap

A binary heap data structure with an API similar to Python's [heapq module](https://docs.python.org/3.8/library/heapq.html)

## Installation

From [Nuget](https://www.nuget.org/packages/FSharp.BinaryHeap/):
```
dotnet add package FSharp.BinaryHeap --version 0.0.1
```

## Usage

A snippet of F# code:

```
module Program

open BinaryHeap.Min

[<EntryPoint>]
let main _ = 
    nsmallest 4 [|6; 1; 9; 5; 4; 3|] |> printfn "Output: %A"     // [|1; 3; 4; 5|]
    0
```

A snippet of C# code:

```
using System;
using BinaryHeap;

namespace DemoCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var r1 = Min.nsmallest(4, new int[] { 6, 1, 9, 5, 4, 3});     // int[] {1, 3, 4, 5}

            var heap = Min.heapify(new int[] { 6, 1, 9, 5, 4, 3});
            var popped = Min.pop(heap)?.Value;              // (1, int[] {3, 4, 9, 6, 5})
            var poppedNull = Min.pop(new int[] {})?.Value;  // null
        }
    }
}
```

Look into the tests for examples of API functions.

## API

Min heap:
```
module BinaryHeap.Min

type BinaryHeap<'a> = Core.BinaryHeap<'a>

// Insert item in heap.
let push (item: 'a) (heap: BinaryHeap<'a>) : BinaryHeap<'a>

// Take an item from the heap. It could be None if heap is empty.
let pop (heap: BinaryHeap<'a>) : Option<'a * BinaryHeap<'a>>

// Transform an array (not commited to heap rules) to a heap. Items could
// end up sorted but it's not guaranteed.
let heapify (arr: array<'a>) : BinaryHeap<'a>

// Push first followed by a pop.
let pushPop (item: 'a) (heap: BinaryHeap<'a>) : 'a * BinaryHeap<'a>

// Pop first followed by a push. It could be None if heap is empty.
let replace (item: 'a) (heap: BinaryHeap<'a>) : Option<'a * BinaryHeap<'a>>

// Sort a sequence using a heap.
let sort (xs: seq<'a>) : list<'a>

// Find the `n` first smallest items from the collection transformed by
// the projection function.
let nsmallestBy (projection: 'a -> 'key) (n: int) (coll: seq<'a>) : array<'a>

// Find the `n` first smallest items from the collection.
let nsmallest (n: int) (coll: seq<'a>) : array<'a>
```

Max heap:
```
module BinaryHeap.Max

type BinaryHeap<'a> = Core.BinaryHeap<'a>

// Insert item in heap.
let push (item: 'a) (heap: BinaryHeap<'a>) : BinaryHeap<'a>

// Take an item from the heap. It could be None if heap is empty.
let pop (heap: BinaryHeap<'a>) : Option<'a * BinaryHeap<'a>>

// Transform an array (not commited to heap rules) to a heap. Items could
// end up sorted but it's not guaranteed.
let heapify (arr: array<'a>) : BinaryHeap<'a>

// Push first followed by a pop.
let pushPop (item: 'a) (heap: BinaryHeap<'a>) : 'a * BinaryHeap<'a>

// Pop first followed by a push. It could be None if heap is empty.
let replace (item: 'a) (heap: BinaryHeap<'a>) : Option<'a * BinaryHeap<'a>>

// Sort a sequence using a heap.
let sort (xs: seq<'a>) : list<'a>

// Find the `n` first largest items from the collection transformed by
// the projection function.
let nlargestBy (projection: 'a -> 'key) (n: int) (coll: seq<'a>) : array<'a>

// Find the `n` first largest items from the collection.
let nlargest (n: int) (coll: seq<'a>) : array<'a>
```