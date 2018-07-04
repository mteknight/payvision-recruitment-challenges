# Solution

## The solution was implemented in 2 parts:
1. Getting a bit count using the Hamming Weight technique with the best performant code taking slow machine into consideration.
2. Enumerating the switched bits was done by using an iterative flag based on binary switching.

## Remarks
1. Code was implemented following the original classes/contracts;
2. Best practices were considered, especially clean code and naming conventions;
3. No design patterns were used as I did not find it necessary for such a small solution, and the overhead was not worth it;
4. The GetBitCount method was extracted to an extension class with the intent of encapsulating and making it easily accessible.

## References
1. Hamming Weight: https://en.wikipedia.org/wiki/Hamming_weight
2. Switched positions: https://stackoverflow.com/questions/3142867/finding-bit-positions-in-an-unsigned-32-bit-integer
