module BinaryHeapTest.MinTests

open BinaryHeap.Min
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

    let heap : BinaryHeap<int> = [|1; 2|]
    shiftUpInPlace 0 heap
    Assert.Equal<BinaryHeap<int>>([|1; 2|], heap)
    shiftUpInPlace 1 heap
    Assert.Equal<BinaryHeap<int>>([|1; 2|], heap)

    let heap : BinaryHeap<int> = [|2; 1|]
    shiftUpInPlace 0 heap
    Assert.Equal<BinaryHeap<int>>([|2; 1|], heap)
    shiftUpInPlace 1 heap
    Assert.Equal<BinaryHeap<int>>([|1; 2|], heap)

    let heap : BinaryHeap<int> = [|4; 4; 8; 9; 4; 12; 9; 11; 13; 7; 10; 5|]
    let iLast : int = Array.length heap - 1
    shiftUpInPlace -4 heap
    Assert.Equal<BinaryHeap<int>>([|4; 4; 8; 9; 4; 12; 9; 11; 13; 7; 10; 5|], heap)
    shiftUpInPlace 0 heap
    Assert.Equal<BinaryHeap<int>>([|4; 4; 8; 9; 4; 12; 9; 11; 13; 7; 10; 5|], heap)
    // The unique element that violates heap rules is the last one, so
    // any other will return the same heap unmodified.
    shiftUpInPlace 7 heap
    Assert.Equal<BinaryHeap<int>>([|4; 4; 8; 9; 4; 12; 9; 11; 13; 7; 10; 5|], heap)
    // Progress: Swap the 5 with its parent (12), and then again with 
    // its new parent (8).
    shiftUpInPlace iLast heap
    Assert.Equal<BinaryHeap<int>>([|4; 4; 5; 9; 4; 8; 9; 11; 13; 7; 10; 12|], heap)

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
    shiftDownInPlace 0 heap
    Assert.Equal<BinaryHeap<int>>([|1; 2|], heap)
    shiftDownInPlace 1 heap
    Assert.Equal<BinaryHeap<int>>([|1; 2|], heap)

    let heap : BinaryHeap<int> = [|2; 1|]
    shiftDownInPlace 0 heap
    Assert.Equal<BinaryHeap<int>>([|1; 2|], heap)

    let heap : BinaryHeap<int> = [|2; 1|]
    shiftDownInPlace 1 heap
    Assert.Equal<BinaryHeap<int>>([|2; 1|], heap)

    let heap : BinaryHeap<int> = [|14; 4; 8; 9; 4; 12; 9; 11; 13; 7; 10|]
    let iLast : int = Array.length heap - 1
    shiftDownInPlace -1 heap
    Assert.Equal<BinaryHeap<int>>([|14; 4; 8; 9; 4; 12; 9; 11; 13; 7; 10|], heap)
    // The root element is the unique that violates heap rules, so
    // any other will return the same heap unmodified.
    shiftDownInPlace 4 heap
    Assert.Equal<BinaryHeap<int>>([|14; 4; 8; 9; 4; 12; 9; 11; 13; 7; 10|], heap)
    // Progress: Swap the 14 with its lower child (4), and then again with 
    // following lower children until the leaf (4 again and 7).
    shiftDownInPlace 0 heap
    Assert.Equal<BinaryHeap<int>>([|4; 4; 8; 9; 7; 12; 9; 11; 13; 14; 10|], heap)

[<Fact>]
let ``Test pop`` () =
    let emptyHeap : BinaryHeap<int> = Array.empty
    Assert.Equal(None, pop emptyHeap)

    let heap : BinaryHeap<int> = [|1|]
    Assert.Equal(Some (1, emptyHeap), pop heap)

    let heap : BinaryHeap<int> = [|1; 2|]
    Assert.Equal(Some (1, [|2|]), pop heap)

    let heap : BinaryHeap<int> = [|4; 4; 8; 9; 4; 12; 9; 11; 13|]
    Assert.Equal(Some (4, [|4; 4; 8; 9; 13; 12; 9; 11|]), pop heap)

[<Fact>]
let ``Test heapify`` () =
    let emptyHeap : BinaryHeap<int> = Array.empty
    Assert.Equal<BinaryHeap<int>>(emptyHeap, heapify emptyHeap)

    let heap : BinaryHeap<int> = [|1|]
    Assert.Equal<BinaryHeap<int>>(heap, heapify heap)

    let heap : BinaryHeap<int> = [|1; 2|]
    Assert.Equal<BinaryHeap<int>>(heap, heapify heap)

    let heap : BinaryHeap<int> = [|2; 1|]
    Assert.Equal<BinaryHeap<int>>([|1; 2|], heapify heap)

    let heap : BinaryHeap<int> = [|1 .. 10|]
    Assert.Equal<BinaryHeap<int>>(heap, heapify heap)

    let heap : BinaryHeap<int> = [|10 .. -1 .. 1|]
    Assert.Equal<BinaryHeap<int>>([|1; 2; 5; 4; 3; 9; 6; 10; 7; 8|], heapify heap)

[<Fact>]
let ``Test heapifyInPlace`` () =
    let emptyHeap : BinaryHeap<int> = Array.empty
    heapifyInPlace emptyHeap
    Assert.Empty(emptyHeap)

    let heap : BinaryHeap<int> = [|1|]
    heapifyInPlace heap
    Assert.Equal<BinaryHeap<int>>([|1|], heap)

    let heap : BinaryHeap<int> = [|1; 2|]
    heapifyInPlace heap
    Assert.Equal<BinaryHeap<int>>([|1; 2|], heap)

    let heap : BinaryHeap<int> = [|2; 1|]
    heapifyInPlace heap
    Assert.Equal<BinaryHeap<int>>([|1; 2|], heap)

    let heap : BinaryHeap<int> = [|1 .. 10|]
    heapifyInPlace heap
    Assert.Equal<BinaryHeap<int>>([|1 .. 10|], heap)

    let heap : BinaryHeap<int> = [|10 .. -1 .. 1|]
    heapifyInPlace heap
    Assert.Equal<BinaryHeap<int>>([|1; 2; 5; 4; 3; 9; 6; 10; 7; 8|], heap)    

[<Fact>]
let ``Test pushPop`` () =
    let emptyHeap : BinaryHeap<int> = Array.empty
    Assert.Equal<int * BinaryHeap<int>>((1, emptyHeap), pushPop 1 emptyHeap)

    let heap : BinaryHeap<int> = [|1|]
    Assert.Equal<int * BinaryHeap<int>>((1, [|2|]), pushPop 2 heap)

    let heap : BinaryHeap<int> = [|2|]
    Assert.Equal<int * BinaryHeap<int>>((1, [|2|]), pushPop 1 heap)

    let heap : BinaryHeap<int> = [|0 .. 2 .. 12|]
    Assert.Equal<int * BinaryHeap<int>>((0, [|2; 6; 4; 7; 8; 10; 12|]), pushPop 7 heap)

[<Fact>]
let ``Test replace`` () =
    let emptyHeap : BinaryHeap<int> = Array.empty
    Assert.Equal(None, replace 1 emptyHeap)

    let heap : BinaryHeap<int> = [|1|]
    Assert.Equal(Some (1, [|2|]), replace 2 heap)

    let heap : BinaryHeap<int> = [|2|]
    Assert.Equal(Some (2, [|1|]), replace 1 heap)

    let heap : BinaryHeap<int> = [|4; 4; 8; 9; 4; 12; 9; 11; 13|]
    Assert.Equal(Some (4, [|4; 4; 8; 5; 13; 12; 9; 11; 9|]), replace 5 heap)

[<Fact>]
let ``Test update`` () =
    let emptyHeap : BinaryHeap<int> = Array.empty
    let f n = if n &&& 1 = 1 then n * 2 else n
    Assert.Equal(None, update f emptyHeap)

    let heap : BinaryHeap<int> = [|1|]
    Assert.Equal(Some [|2|], update f heap)

    let heap : BinaryHeap<int> = [|2|]
    Assert.Equal(Some [|2|], update f heap)

    let heap : BinaryHeap<int> = [|4; 4; 8; 9; 4; 12; 9; 11; 13|]
    Assert.Equal(Some [|4; 4; 8; 4; 13; 12; 9; 11; 9|], update f heap)

    let heap : BinaryHeap<int> = [|3; 4; 8; 9; 4; 12; 9; 11; 13|]
    Assert.Equal(Some [|4; 4; 8; 6; 13; 12; 9; 11; 9|], update f heap)

    let heap : BinaryHeap<int> = [|1; 4; 8; 9; 4; 12; 9; 11; 13|]
    Assert.Equal(Some [|2; 4; 8; 4; 13; 12; 9; 11; 9|], update f heap)

[<Fact>]
let ``Test sort`` () =
    let emptySeq : seq<int> = Seq.empty
    Assert.Equal<list<int>>(List.empty, sort emptySeq)
    Assert.Equal<list<int>>([1], sort (seq [1]))
    Assert.Equal<list<int>>([1; 2], sort (seq [1; 2]))
    Assert.Equal<list<int>>([1; 2], sort (seq [2; 1]))
    Assert.Equal<list<int>>([1 .. 10], sort <| Fixture.shuffle [|1 .. 10|])

[<Fact>]
let ``Test nsmallestBy`` () =
    let f (s: string) : int = s.Length
    let empty : array<string> = Array.empty
    Assert.Equal<array<string>>(Array.empty, nsmallestBy f 3 empty)
    Assert.Equal<array<string>>(
        [|"may"; "june"; "july"|], 
        nsmallestBy f 3 ["january"; "february"; "march"; "april"; "may"; "june"; "july"; "august"])

[<Fact>]
let ``Test nsmallest`` () =
    let empty : array<int> = Array.empty
    Assert.Equal<array<int>>(Array.empty, nsmallest 3 empty)
    Assert.Equal<array<int>>([|1|], nsmallest 3 [|1|])
    Assert.Equal<array<int>>([|1; 2; 3|], nsmallest 3 [|1; 2; 3|])
    Assert.Equal<array<int>>([|1; 2; 3|], nsmallest 3 [|1; 2; 3; 4|])
    Assert.Equal<array<int>>([|1; 3; 4|], nsmallest 3 [|6; 8; 4; 3; 1|])
    Assert.Equal<array<int>>([|1; 2; 3|], nsmallest 3 [|3; 2; 1|])
    Assert.Equal<array<int>>([|1; 1; 1; 3|], nsmallest 4 [|6; 1; 1; 3; 1|])

    