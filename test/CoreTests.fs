module BinaryHeapTest.CoreTests

open BinaryHeap.Core
open Xunit

[<Fact>]
let ``Test findChildrenIndexes`` () =
    Assert.Equal((Some 1, Some 2), findChildrenIndexes 6 0)
    Assert.Equal((Some 3, Some 4), findChildrenIndexes 6 1)
    Assert.Equal((Some 5, Some 6), findChildrenIndexes 6 2)
    Assert.Equal((Some 5, None), findChildrenIndexes 5 2)
    Assert.Equal((None, None), findChildrenIndexes 4 2)

[<Fact>]
let ``Test findParentIndex`` () =
    Assert.Equal(None, findParentIndex 0)
    Assert.Equal(Some 0, findParentIndex 1)
    Assert.Equal(Some 0, findParentIndex 2)
    Assert.Equal(Some 1, findParentIndex 3)
    Assert.Equal(Some 1, findParentIndex 4)
    Assert.Equal(Some 2, findParentIndex 5)
    Assert.Equal(Some 3, findParentIndex 7)

    