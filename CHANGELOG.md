# Change log

## Version 0.0.3 (Current)

- API Breaking changes:

    - `Min.pop` and `Max.pop` functions now don't mutate the heap parameter.
    - `Min.heapify` and `Max.heapify` functions now don't mutate the array parameter.

- Additions:

    - New `Min.heapifyInPlace` and `Max.heapifyInPlace` API functions.
    - New tests for `Min.heapifyInPlace` and `Max.heapifyInPlace` functions.

- Fixes:

    - Rename and refactor `Core.shiftDown` function to `Core.shiftDownInPlace`
    - Rename and refactor `Core.shiftUp` function to `Core.shiftUpInPlace`
    - Rename and refactor internal `Core.swap` function to `Core.swapInPlace`
    - Refactor several functions to use the previous ones.

----------------

## Version 0.0.2 

- Additions:

    - New `Min.update` and `Max.update` API functions.

----------------

## Version 0.0.1

First release. Same API than Python's `heapq` module with the exception of the `merge` function. 

----------------