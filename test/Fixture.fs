module BinaryHeapTest.Fixture

open BinaryHeap.Core

type Random = System.Random

let shuffle (arr: array<'a>) : array<'a> =
    let length : int = Array.length arr
    let rand : Random = Random ()
    arr |> Array.iteri (fun i _ -> swapInPlace i (rand.Next(i, length)) arr)
    arr