# Change log

## Version 0.0.3 (Current)

- API Breaking changes:

    - `Min.pop` and `Max.pop` functions now don't mutate the heap parameter.
    - `Min.heapify` and `Max.heapify` functions now don't mutate the array parameter.

- Additions:

    - New `Min.heapifyInPlace` and `Max.heapifyInPlace` functions.
    - New tests for `Min.heapifyInPlace` and `Max.heapifyInPlace`

- Fixes:

    - Refactor `Core.shiftDown` to `Core.shiftDownInPlace`
    - Refactor `Core.shiftUp` to `Core.shiftUpInPlace`
    - Refactor internal `Core.swap` to `Core.swapInPlace`
    - Refactor multiple functions to use the previous ones.

----------------

## Version 0.0.2 

- Additions:

    - New `Min.update` and `Max.update` API functions.

----------------

## Version 0.0.1

First release. Same API than Python's `heapq` module with the exception of the `merge` function. 

----------------