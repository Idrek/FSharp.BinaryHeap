# Change log

## Version 0.0.2 (Current)

New `Min.update` and `Max.update` API functions with signature:
```
let update (f: 'a -> 'a) (heap: BinaryHeap<'a>) : Option<BinaryHeap<'a>>
```

----------------

## Version 0.0.1

First release. Same API than Python's `heapq` module with the exception of the `merge` function. 

----------------