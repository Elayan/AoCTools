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

        /// <summary>
        /// Sum of integers from n to m.
        /// </summary>
        public static long SumIntegersBetween(long n, long m)
        {
            return (n + m) * (m - n + 1) / 2L;
        }

        /// <summary>
        /// Count the digits of given n.
        /// </summary>
        public static int GetDigitCount(long n)
        {
            if (n == 0)
                return 1;

            return (int)Math.Floor(Math.Log10(Math.Abs(n)) + 1);
        }

        /// <summary>
        /// Split given n in two parts at given index.
        /// </summary>
        public static void SplitDigits(long n, int index, out long left, out long right)
        {
            var divisor = (int)Math.Pow(10, index);
            left = n / divisor;
            right = n % divisor;
        }
    }
}