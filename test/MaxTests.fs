module BinaryHeapTest.MaxTests

open BinaryHeap.Max
open Xunit

type BinaryHeap<'a> = BinaryHeap.Core.BinaryHeap<'a>

[<Fact>]
let ``Test shiftUpInPlace`` () =
    let emptyHeap : BinaryHeap<int> = Array.empty
    shiftUpInPlace 0 emptyHeap
    Assert.Empty(emptyHeap)
    shiftUpInPlace 6 emptyHeap
    Assert.Empty(emptyHeap)

    let heap : BinaryHeap<int> = [|1|]
    shiftUpInPlace 0 heap
    Assert.Equal<BinaryHeap<int>>([|1|], heap)
    shiftUpInPlace 1 heap
    Assert.Equal<BinaryHeap<int>>([|1|], heap)

    let heap : BinaryHeap<int> = [|2; 1|]
    shiftUpInPlace 0 heap
    Assert.Equal<BinaryHeap<int>>([|2; 1|], heap)
    shiftUpInPlace 1 heap
    Assert.Equal<BinaryHeap<int>>([|2; 1|], heap)

    let heap : BinaryHeap<int> = [|1; 2|]
    shiftUpInPlace 0 heap
    Assert.Equal<BinaryHeap<int>>([|1; 2|], heap)
    shiftUpInPlace 1 heap
    Assert.Equal<BinaryHeap<int>>([|2; 1|], heap)

    let heap : BinaryHeap<int> = [|12; 10; 7; 3; 8; 5; 4; 2; 1; 11|]
    let iLast : int = Array.length heap - 1
    shiftUpInPlace -4 heap
    Assert.Equal<BinaryHeap<int>>([|12; 10; 7; 3; 8; 5; 4; 2; 1; 11|], heap)
    shiftUpInPlace 0 heap
    Assert.Equal<BinaryHeap<int>>([|12; 10; 7; 3; 8; 5; 4; 2; 1; 11|], heap)
    // The unique element that violates heap rules is the last one, so
    // any other will return the same heap unmodified.
    shiftUpInPlace 4 heap
    Assert.Equal<BinaryHeap<int>>([|12; 10; 7; 3; 8; 5; 4; 2; 1; 11|], heap)
    // Progress: Swap the 11 with its parent (8), and then again with 
    // its new parent (10).
    shiftUpInPlace iLast heap
    Assert.Equal<BinaryHeap<int>>([|12; 11; 7; 3; 10; 5; 4; 2; 1; 8|], heap)

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

[<Fact>]
let ``Test shiftDownInPlace`` () =
    let emptyHeap : BinaryHeap<int> = Array.empty
    shiftDownInPlace 0 emptyHeap
    Assert.Empty(emptyHeap)
    shiftDownInPlace 6 emptyHeap
    Assert.Empty(emptyHeap)

    let heap : BinaryHeap<int> = [|1|]
    shiftDownInPlace 0 heap
    Assert.Equal<BinaryHeap<int>>([|1|], heap)
    shiftDownInPlace 1 heap
    Assert.Equal<BinaryHeap<int>>([|1|], heap)

    let heap : BinaryHeap<int> = [|1; 2|]
    shiftDownInPlace 1 heap
    Assert.Equal<BinaryHeap<int>>([|1; 2|], heap)
    shiftDownInPlace 0 heap
    Assert.Equal<BinaryHeap<int>>([|2; 1|], heap)
    

    let heap : BinaryHeap<int> = [|6; 11; 7; 3; 10; 5; 4; 2; 1; 8|]
    let iLast : int = Array.length heap - 1
    shiftDownInPlace -1 heap
    Assert.Equal<BinaryHeap<int>>([|6; 11; 7; 3; 10; 5; 4; 2; 1; 8|], heap)
    // The root element is the unique that violates heap rules, so
    // any other will return the same heap unmodified.
    shiftDownInPlace 4 heap
    Assert.Equal<BinaryHeap<int>>([|6; 11; 7; 3; 10; 5; 4; 2; 1; 8|], heap)
    // Progress: Swap the 6 with its higher child (11), and then again with 
    // following lower children until the leaf (10 and 8).
    shiftDownInPlace 0 heap
    Assert.Equal<BinaryHeap<int>>([|11; 10; 7; 3; 8; 5; 4; 2; 1; 6|], heap)

[<Fact>]
let ``Test pop`` () =
    let emptyHeap : BinaryHeap<int> = Array.empty
    Assert.Equal(None, pop emptyHeap)

    let heap : BinaryHeap<int> = [|1|]
    Assert.Equal(Some (1, emptyHeap), pop heap)

    let heap : BinaryHeap<int> = [|2; 1|]
    Assert.Equal(Some (2, [|1|]), pop heap)

    let heap : BinaryHeap<int> = [|12; 11; 7; 3; 10; 5; 4; 2; 1; 8|]
    Assert.Equal(Some (12, [|11; 10; 7; 3; 8; 5; 4; 2; 1|]), pop heap)

[<Fact>]
let ``Test heapify`` () =
    let emptyHeap : BinaryHeap<int> = Array.empty
    Assert.Equal<BinaryHeap<int>>(emptyHeap, heapify emptyHeap)

    let heap : BinaryHeap<int> = [|1|]
    Assert.Equal<BinaryHeap<int>>(heap, heapify heap)

    let heap : BinaryHeap<int> = [|2; 1|]
    Assert.Equal<BinaryHeap<int>>(heap, heapify heap)

    let heap : BinaryHeap<int> = [|1; 2|]
    Assert.Equal<BinaryHeap<int>>([|2; 1|], heapify heap)

    let heap : BinaryHeap<int> = [|10 .. -1 .. 1|]
    Assert.Equal<BinaryHeap<int>>(heap, heapify heap)

    let heap : BinaryHeap<int> = [|1 .. 10|]
    Assert.Equal<BinaryHeap<int>>([|10; 9; 6; 7; 8; 2; 5; 1; 4; 3|], heapify heap)

[<Fact>]
let ``Test heapifyInPlace`` () =
    let emptyHeap : BinaryHeap<int> = Array.empty
    heapifyInPlace emptyHeap
    Assert.Empty(emptyHeap)

    let heap : BinaryHeap<int> = [|1|]
    heapifyInPlace heap
    Assert.Equal<BinaryHeap<int>>([|1|], heap)

    let heap : BinaryHeap<int> = [|2; 1|]
    heapifyInPlace heap
    Assert.Equal<BinaryHeap<int>>([|2; 1|], heap)

    let heap : BinaryHeap<int> = [|1; 2|]
    heapifyInPlace heap
    Assert.Equal<BinaryHeap<int>>([|2; 1|], heap)

    let heap : BinaryHeap<int> = [|10 .. -1 .. 1|]
    heapifyInPlace heap
    Assert.Equal<BinaryHeap<int>>([|10 .. -1 .. 1|], heap)

    let heap : BinaryHeap<int> = [|1 .. 10|]
    heapifyInPlace heap
    Assert.Equal<BinaryHeap<int>>([|10; 9; 6; 7; 8; 2; 5; 1; 4; 3|], heap)

[<Fact>]
let ``Test pushPop`` () =
    let emptyHeap : BinaryHeap<int> = Array.empty
    Assert.Equal<int * BinaryHeap<int>>((1, emptyHeap), pushPop 1 emptyHeap)

    let heap : BinaryHeap<int> = [|1|]
    Assert.Equal<int * BinaryHeap<int>>((2, [|1|]), pushPop 2 heap)

    let heap : BinaryHeap<int> = [|2|]
    Assert.Equal<int * BinaryHeap<int>>((2, [|1|]), pushPop 1 heap)

    let heap : BinaryHeap<int> = [|12 .. -2 .. 0|]
    Assert.Equal<int * BinaryHeap<int>>((12, [|10; 7; 8; 6; 4; 2; 0|]), pushPop 7 heap)

[<Fact>]
let ``Test replace`` () =
    let emptyHeap : BinaryHeap<int> = Array.empty
    Assert.Equal(None, replace 1 emptyHeap)

    let heap : BinaryHeap<int> = [|1|]
    Assert.Equal(Some (1, [|2|]), replace 2 heap)

    let heap : BinaryHeap<int> = [|2|]
    Assert.Equal(Some (2, [|1|]), replace 1 heap)

    let heap : BinaryHeap<int> = [|12; 11; 7; 3; 10; 5; 4; 2; 1; 8|]
    Assert.Equal(Some (12, [|11; 10; 7; 3; 9; 5; 4; 2; 1; 8|]), replace 9 heap)

[<Fact>]
let ``Test update`` () =
    let emptyHeap : BinaryHeap<int> = Array.empty
    let f n = if n &&& 1 = 1 then n / 2 else n  // Divide odd numbers.
    Assert.Equal(None, update f emptyHeap)

    let heap : BinaryHeap<int> = [|1|]
    Assert.Equal(Some [|0|], update f heap)

    let heap : BinaryHeap<int> = [|2|]
    Assert.Equal(Some [|2|], update f heap)

    let heap : BinaryHeap<int> = [|12; 11; 7; 3; 10; 5; 4; 2; 1; 8|]
    Assert.Equal(Some [|12; 11; 7; 3; 10; 5; 4; 2; 1; 8|], update f heap)

    let heap : BinaryHeap<int> = [|19; 11; 7; 3; 10; 5; 4; 2; 1; 8|]
    Assert.Equal(Some [|11; 10; 7; 3; 9; 5; 4; 2; 1; 8|], update f heap)

    let heap : BinaryHeap<int> = [|31; 11; 7; 3; 10; 5; 4; 2; 1; 8|]
    Assert.Equal(Some [|15; 11; 7; 3; 10; 5; 4; 2; 1; 8|], update f heap)

[<Fact>]
let ``Test sort`` () =
    let emptySeq : seq<int> = Seq.empty
    Assert.Equal<list<int>>(List.empty, sort emptySeq)
    Assert.Equal<list<int>>([1], sort (seq [1]))
    Assert.Equal<list<int>>([2; 1], sort (seq [1; 2]))
    Assert.Equal<list<int>>([2; 1], sort (seq [2; 1]))
    Assert.Equal<list<int>>([10 .. -1 .. 1], sort <| Fixture.shuffle [|1 .. 10|])

[<Fact>]
let ``Test nlargestBy`` () =
    let f (s: string) : int = s.Length
    let empty : array<string> = Array.empty
    Assert.Equal<array<string>>(Array.empty, nlargestBy f 3 empty)
    Assert.Equal<array<string>>(
        [|"february"; "january"; "august"|], 
        nlargestBy f 3 ["january"; "february"; "march"; "april"; "may"; "june"; "july"; "august"])

[<Fact>]
let ``Test nlargest`` () =
    let empty : array<int> = Array.empty
    Assert.Equal<array<int>>(Array.empty, nlargest 3 empty)
    Assert.Equal<array<int>>([|1|], nlargest 3 [|1|])
    Assert.Equal<array<int>>([|3; 2; 1|], nlargest 3 [|3; 2; 1|])
    Assert.Equal<array<int>>([|4; 3; 2|], nlargest 3 [|4; 3; 2; 1|])
    Assert.Equal<array<int>>([|8; 6; 4|], nlargest 3 [|6; 8; 4; 3; 1|])
    Assert.Equal<array<int>>([|3; 2; 1|], nlargest 3 [|1; 2; 3|])
    Assert.Equal<array<int>>([|6; 6; 6; 3|], nlargest 4 [|6; 1; 6; 3; 1; 6|])

    