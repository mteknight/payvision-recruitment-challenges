namespace Payvision.CodeChallenge.Algorithms.CountingBits
{
    public static class IntegerAsBinaryExtensions
    {
        private const long M1 = 0x5555555555555555; //binary: 0101...
        private const long M2 = 0x3333333333333333; //binary: 00110011..
        private const long M4 = 0x0f0f0f0f0f0f0f0f; //binary:  4 zeros,  4 ones ...

        /// <remarks>Original code from https://en.wikipedia.org/wiki/Hamming_weight</remarks>
        internal static int GetBitCount(this long input)
        {
            input -= (input >> 1) & M1; //put count of each 2 bits into those 2 bits
            input = (input & M2) + ((input >> 2) & M2); //put count of each 4 bits into those 4 bits
            input = (input + (input >> 4)) & M4; //put count of each 8 bits into those 8 bits
            input += input >> 8; //put count of each 16 bits into their lowest 8 bits
            input += input >> 16; //put count of each 32 bits into their lowest 8 bits
            input += input >> 32; //put count of each 64 bits into their lowest 8 bits
            return (int)(input & 0x7f);
        }
    }
}
