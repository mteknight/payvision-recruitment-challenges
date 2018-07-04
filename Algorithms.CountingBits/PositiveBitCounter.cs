namespace Payvision.CodeChallenge.Algorithms.CountingBits
{
    using System;
    using System.Collections.Generic;

    public class PositiveBitCounter
    {
        public IEnumerable<int> Count(int input)
        {
            if (input < 0) throw new ArgumentException(nameof(input), "Parameter must be a positive integer.");

            return GetCountData(input);
        }

        private static IEnumerable<int> GetCountData(long input)
        {
            yield return input.GetBitCount();

            foreach (var position in GetSwitchedBitPositions(input))
            {
                yield return position;
            }
        }

        /// <remarks>Code adapted from https://stackoverflow.com/questions/3142867/finding-bit-positions-in-an-unsigned-32-bit-integer</remarks>
        private static IEnumerable<int> GetSwitchedBitPositions(long input)
        {
            var iterator = 0;

            for (var i = 1; i < 256; i <<= 1, iterator++)
            {
                if ((input & i) > 0)
                {
                    yield return iterator;
                }
            }
        }
    }
}
