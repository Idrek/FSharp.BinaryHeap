module BinaryHeapTest.MaxTests

open BinaryHeap.Max
open Xunit

type BinaryHeap<'a> = BinaryHeap.Core.BinaryHeap<'a>

[<Fact>]
let ``Test shiftUp`` () =
    let emptyHeap : BinaryHeap<int> = Array.empty
    Assert.Equal<BinaryHeap<int>>(emptyHeap, shiftUp 0 emptyHeap)
    Assert.Equal<BinaryHeap<int>>(emptyHeap, shiftUp 6 emptyHeap)

    let singleHeap : BinaryHeap<int> = [|1|]
    Assert.Equal<BinaryHeap<int>>(singleHeap, shiftUp 0 singleHeap)
    Assert.Equal<BinaryHeap<int>>(singleHeap, shiftUp 1 singleHeap)

    let heap : BinaryHeap<int> = [|2; 1|]
    Assert.Equal<BinaryHeap<int>>(heap, shiftUp 0 heap)
    Assert.Equal<BinaryHeap<int>>(heap, shiftUp 1 heap)

    let heap : BinaryHeap<int> = [|1; 2|]
    Assert.Equal<BinaryHeap<int>>(heap, shiftUp 0 heap)
    Assert.Equal<BinaryHeap<int>>([|2; 1|], shiftUp 1 heap)

    let heap : BinaryHeap<int> = [|12; 10; 7; 3; 8; 5; 4; 2; 1; 11|]
    let iLast : int = Array.length heap - 1
    Assert.Equal<BinaryHeap<int>>(heap, shiftUp -4 heap)
    Assert.Equal<BinaryHeap<int>>(heap, shiftUp 0 heap)
    // The unique element that violates heap rules is the last one, so
    // any other will return the same heap unmodified.
    Assert.Equal<BinaryHeap<int>>(heap, shiftUp 4 heap)
    // Progress: Swap the 11 with its parent (8), and then again with 
    // its new parent (10).
    Assert.Equal<BinaryHeap<int>>(
        [|12; 11; 7; 3; 10; 5; 4; 2; 1; 8|], 
        shiftUp iLast heap)

[<Fact>]
let ``Test push`` () =
    let emptyHeap : BinaryHeap<int> = Array.empty
    Assert.Equal<BinaryHeap<int>>([|2; 1|], emptyHeap |> push 1 |> push 2)
    Assert.Equal<BinaryHeap<int>>([|2; 1|], emptyHeap |> push 2 |> push 1)
    Assert.Equal<BinaryHeap<int>>(
        [|9; 8; 4; 7; 5; 2; 3; 1; 6; 0|],
        Array.fold 
            (fun heap item -> push item heap) 
            emptyHeap 
            [|1; 3; 5; 7; 9; 2; 4; 6; 8; 0|])
    let heap : BinaryHeap<int> = [|12; 11; 7; 3; 10; 5; 4; 2; 1; 8|]
    Assert.Equal<BinaryHeap<int>>(
        [|12; 11; 8; 3; 10; 7; 4; 2; 1; 8; 7; 3; 5|], 
        heap |> push 7 |> push 3 |> push 8)

        