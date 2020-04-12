module BinaryHeapTest.MinTests

open BinaryHeap.Min
open Xunit

type BinaryHeap<'a> = BinaryHeap.Core.BinaryHeap<'a>

[<Fact>]
let ``Test shiftUp`` () =
    let emptyHeap : BinaryHeap<int> = Array.empty
    Assert.Equal<BinaryHeap<int>>(emptyHeap, shiftUp 0 emptyHeap)
    Assert.Equal<BinaryHeap<int>>(emptyHeap, shiftUp 6 emptyHeap)

    let heap : BinaryHeap<int> = [|1|]
    Assert.Equal<BinaryHeap<int>>(heap, shiftUp 0 heap)
    Assert.Equal<BinaryHeap<int>>(heap, shiftUp 1 heap)

    let heap : BinaryHeap<int> = [|1; 2|]
    Assert.Equal<BinaryHeap<int>>(heap, shiftUp 0 heap)
    Assert.Equal<BinaryHeap<int>>(heap, shiftUp 1 heap)

    let heap : BinaryHeap<int> = [|2; 1|]
    Assert.Equal<BinaryHeap<int>>(heap, shiftUp 0 heap)
    Assert.Equal<BinaryHeap<int>>([|1; 2|], shiftUp 1 heap)

    let heap : BinaryHeap<int> = [|4; 4; 8; 9; 4; 12; 9; 11; 13; 7; 10; 5|]
    let iLast : int = Array.length heap - 1
    Assert.Equal<BinaryHeap<int>>(heap, shiftUp -4 heap)
    Assert.Equal<BinaryHeap<int>>(heap, shiftUp 0 heap)
    // The unique element that violates heap rules is the last one, so
    // any other will return the same heap unmodified.
    Assert.Equal<BinaryHeap<int>>(heap, shiftUp 7 heap)
    // Progress: Swap the 5 with its parent (12), and then again with 
    // its new parent (8).
    Assert.Equal<BinaryHeap<int>>(
        [|4; 4; 5; 9; 4; 8; 9; 11; 13; 7; 10; 12|], 
        shiftUp iLast heap)

[<Fact>]
let ``Test push`` () =
    let emptyHeap : BinaryHeap<int> = Array.empty
    Assert.Equal<BinaryHeap<int>>([|1; 2|], emptyHeap |> push 1 |> push 2)
    Assert.Equal<BinaryHeap<int>>([|1; 2|], emptyHeap |> push 2 |> push 1)
    Assert.Equal<BinaryHeap<int>>(
        [|0; 1; 2; 6; 3; 5; 4; 7; 8; 9|],
        Array.fold 
            (fun heap item -> push item heap) 
            emptyHeap 
            [|1; 3; 5; 7; 9; 2; 4; 6; 8; 0|])

    let heap : BinaryHeap<int> = [|4; 4; 8; 9; 4; 12; 9; 11; 13|]
    Assert.Equal<BinaryHeap<int>>(
        [|4; 4; 5; 9; 4; 8; 9; 11; 13; 7; 10; 12|], 
        heap |> push 7 |> push 10 |> push 5)

[<Fact>]
let ``Test shiftDown`` () =
    let emptyHeap : BinaryHeap<int> = Array.empty
    Assert.Equal<BinaryHeap<int>>(emptyHeap, shiftDown 0 emptyHeap)
    Assert.Equal<BinaryHeap<int>>(emptyHeap, shiftDown 6 emptyHeap)

    let heap : BinaryHeap<int> = [|1|]
    Assert.Equal<BinaryHeap<int>>(heap, shiftDown 0 heap)
    Assert.Equal<BinaryHeap<int>>(heap, shiftDown 1 heap)

    let heap : BinaryHeap<int> = [|1; 2|]
    Assert.Equal<BinaryHeap<int>>(heap, shiftDown 0 heap)
    Assert.Equal<BinaryHeap<int>>(heap, shiftDown 1 heap)

    let heap : BinaryHeap<int> = [|2; 1|]
    Assert.Equal<BinaryHeap<int>>([|1; 2|], shiftDown 0 heap)
    Assert.Equal<BinaryHeap<int>>(heap, shiftDown 1 heap)

    let heap : BinaryHeap<int> = [|14; 4; 8; 9; 4; 12; 9; 11; 13; 7; 10|]
    let iLast : int = Array.length heap - 1
    Assert.Equal<BinaryHeap<int>>(heap, shiftDown -1 heap)
    // The root element is the unique that violates heap rules, so
    // any other will return the same heap unmodified.
    Assert.Equal<BinaryHeap<int>>(heap, shiftDown 4 heap)
    // Progress: Swap the 14 with its lower child (4), and then again with 
    // following lower children until the leaf (4 again and 7).
    Assert.Equal<BinaryHeap<int>>(
        [|4; 4; 8; 9; 7; 12; 9; 11; 13; 14; 10|], 
        shiftDown 0 heap)

