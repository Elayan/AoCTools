using System;
using AoCTools.Error.Exception;

namespace AoCTools.Strings
{
    public static class StringHelper
    {
        /// <summary>
        /// Count the amount of differences between two strings of same length
        /// </summary>
        /// <returns>Difference count.</returns>
        /// <exception cref="InvalidParameterException"/>
        public static int CountDifferences(this string s1, string s2)
        {
            if (s1.Length != s2.Length)
                throw new InvalidParameterException(nameof(s1), $"Strings must have the same length.");

            var minLength = Math.Min(s1.Length, s2.Length);
            var diffLength = Math.Max(s1.Length, s2.Length) - minLength;

            var diffs = 0;
            for (var i = 0; i < minLength; i++)
                if (s1[i] != s2[i])
                    diffs++;

            return diffs + diffLength;
        }
    }
}