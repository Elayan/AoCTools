using AoCTools.Error.Exception;

namespace AoCTools.Numbers
{
    public static class NonoMath
    {
        /// <summary>
        /// Greatest Common Factor (fr: PGCD).
        /// </summary>
        public static int GCF(int a, int b)
        {
            if (a == 0)
                throw new InvalidParameterException(nameof(a), "can't be zero");
            if (b == 0)
                throw new InvalidParameterException(nameof(b), "can't be zero");

            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        /// <summary>
        /// Least Common Multiple (fr: PPCM).
        /// </summary>
        public static int LCM(int a, int b)
        {
            if (a == 0)
                throw new InvalidParameterException(nameof(a), "can't be zero");
            if (b == 0)
                throw new InvalidParameterException(nameof(b), "can't be zero");

            return a / GCF(a, b) * b;
        }

        /// <summary>
        /// Sum of the n first integers.
        /// </summary>
        public static long SumFirstIntegers(long n)
        {
            return n * (n + 1) / 2;
        }
    }
}