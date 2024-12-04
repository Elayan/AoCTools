using System;
using System.Collections.Generic;
using AoCTools.Error.Exception;

namespace AoCTools.Numbers
{
    /// <summary>
    /// Represents a range of integers.
    /// </summary>
    public class Range : IEquatable<Range>
    {
        protected Range() { }

        #region Factories

        /// <summary>
        /// Creates a Range between two given integers.
        /// </summary>
        /// <param name="min">Min integer.</param>
        /// <param name="max">Max integer.</param>
        /// <exception cref="InvalidParameterException"/>
        public static Range CreateFromMinMax(long min, long max)
        {
            if (min > max)
                throw new InvalidParameterException(nameof(min), $"trying to create range with {nameof(min)} > {nameof(max)}");

            return new Range
            {
                Min = min,
                Max = max,
                Size = (ulong)(max - min + 1)
            };
        }

        /// <summary>
        /// Creates a Range from given integer with given size.
        /// </summary>
        /// <param name="min">Min integer.</param>
        /// <param name="size">Size of range.</param>
        public static Range CreateFromRange(long min, ulong size)
        {
            return new Range
            {
                Min = min,
                Max = min + (long)size - 1,
                Size = size
            };
        }

        /// <summary>
        /// Creates a Range that is a copy of another Range.
        /// </summary>
        /// <param name="copy">Range to copy from.</param>
        public static Range CreateFromCopy(Range copy)
        {
            return new Range
            {
                Min = copy.Min,
                Max = copy.Max,
                Size = copy.Size
            };
        }

        /// <summary>
        /// Creates one or two Ranges that represent a Range excluding another Range.
        /// Example: this = 1 to 5, param = 2 to 3, result = 1 to 2 and 4 to 5.
        /// </summary>
        /// <param name="reference">Reference range.</param>
        /// <param name="excluding">Excluded range.</param>
        /// <returns>One or two ranges, matching <paramref name="reference"/> range excluding <paramref name="excluding"/> one.</returns>
        public static Range[] CreateFromExcluding(Range reference, Range excluding)
        {
            var filteredRanges = new List<Range>();
            if (reference.Min <= excluding.Min - 1)
            {
                filteredRanges.Add(CreateFromMinMax(reference.Min, excluding.Min - 1));
            }
            if (excluding.Max + 1 <= reference.Max)
            {
                filteredRanges.Add(CreateFromMinMax(excluding.Max + 1, reference.Max));
            }
            return filteredRanges.ToArray();
        }

        /// <summary>
        /// Creates a Range that represent the intersection of two ranges.
        /// Example: A = 1 to 3, B = 2 to 5, result = 2 to 3.
        /// </summary>
        /// <param name="rangeA">Intersecting range.</param>
        /// <param name="rangeB">Intersecting range.</param>
        /// <returns>Range that is intersecting both</returns>
        /// <exception cref="InvalidParameterException"/>
        public static Range CreateFromIntersecting(Range rangeA, Range rangeB)
        {
            var resMin = Math.Max(rangeA.Min, rangeB.Min);
            var resMax = Math.Min(rangeA.Max, rangeB.Max);
            if (resMax < resMin)
                throw new InvalidParameterException(nameof(rangeB), "ranges are not intersecting");

            return CreateFromMinMax(resMin, resMax);
        }

        #endregion

        /// <summary>
        /// Min value.
        /// </summary>
        public long Min { get; protected set; }
        /// <summary>
        /// Max value.
        /// </summary>
        public long Max { get; protected set; }
        /// <summary>
        /// Range size.
        /// </summary>
        public ulong Size { get; protected set; }

        #region IEquatable

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Min.GetHashCode() * 17 + Max.GetHashCode();
        }

        /// <summary>
        /// Check if Ranges are equal.
        /// </summary>
        public bool Equals(Range other)
        {
            return other != null && Min == other.Min && Max == other.Max;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (!(obj is Range other))
                return false;

            return Equals(other);
        }

        #endregion

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Min} to {Max}";
        }

        /// <summary>
        /// Checks if a value is within the Range.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>TRUE if value is within Range boundaries.</returns>
        public bool IsInRange(long value)
        {
            return Min <= value && value <= Max;
        }

        /// <summary>
        /// Checks if a Range is within the Range.
        /// </summary>
        /// <param name="range">Range to check.</param>
        /// <param name="validRange">Part of range that is within the range boundaries.</param>
        /// <returns>TRUE if a part of the range is within the boundaries.</returns>
        public bool IsInRange(Range range, out Range validRange)
        {
            if (range.Max < Min || range.Min > Max)
            {
                validRange = null;
                return false;
            }

            validRange = CreateFromMinMax(
                Math.Max(Min, range.Min),
                Math.Min(Max, range.Max));
            return true;
        }

        /// <summary>
        /// Finds index of a value within the range.
        /// Example: value = 3, range = (2 to 5), index = 1.
        /// </summary>
        /// <param name="value">Value to consider.</param>
        /// <returns>Index of value within range.</returns>
        /// <exception cref="InvalidParameterException"/>
        public ulong GetRangeIndex(long value)
        {
            if (!IsInRange(value))
                throw new InvalidParameterException(nameof(value), "value is not within range.");

            return (ulong)(value - Min);
        }

        /// <summary>
        /// Finds index range of a range within the range.
        /// Example: value = 3 to 5, range = (2 to 5), index = 1 to 4.
        /// </summary>
        /// <param name="range">Range of values to consider.</param>
        /// <returns>Index range of given values within range.</returns>
        /// <exception cref="InvalidParameterException"/>
        public Range GetRangeIndexes(Range range)
        {
            if (!IsInRange(range, out var valid) || !valid.Equals(range))
                throw new InvalidParameterException(nameof(range), "range is not within range.");

            return CreateFromRange(
                range.Min - Min,
                range.Size);
        }

        /// <summary>
        /// Get value at index within range.
        /// Example: range = (2 to 5), index = 1, value = 3.
        /// </summary>
        /// <param name="index">Index of the value in the range.</param>
        /// <returns>Value at given index.</returns>
        /// <exception cref="InvalidParameterException"/>
        public long GetRangedValue(ulong index)
        {
            if (index > Size)
                throw new InvalidParameterException(nameof(index), "index out of range.");

            return Min + (long)index;
        }

        /// <summary>
        /// Get range of values within range, at range of indexes.
        /// Example: range = (2 to 5), index = 1 to 3, value = 3 to 5.
        /// </summary>
        /// <param name="range">Range of indexes.</param>
        /// <returns>Range of values.</returns>
        /// <exception cref="InvalidParameterException"/>
        public Range GetRangedValues(Range range)
        {
            if (range.Min < 0 || range.Max > (long)Size || Size - (ulong)range.Min < range.Size)
                throw new InvalidParameterException(nameof(range), "given range is not within bounds.");

            return CreateFromRange(
                Min + range.Min,
                range.Size);
        }
    }
}