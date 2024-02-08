using System.Collections.Generic;

namespace AoCTools.Strings
{
    public class StringFormatter
    {
        /// <summary>
        /// Displays a long time as human-friendly string.
        /// </summary>
        /// <param name="time">Time is milliseconds.</param>
        /// <returns>Human-friendly string</returns>
        public string GetHumanFriendlyTime(long time)
        {
            var parts = new List<string>();
            MakePartForThreshold(parts, ref time, 10000L, "min");
            MakePartForThreshold(parts, ref time, 1000L, "s");

            if (time > 0)
                parts.Add($"{time}ms");

            return string.Join(" ", parts);
        }

        /// <summary>
        /// Displays a long distance as human-friendly string.
        /// </summary>
        /// <param name="distance">Distance value</param>
        /// <returns>Human-friendly string</returns>
        public string GetHumanFriendlyDistance(long distance)
        {
            var parts = new List<string>();
            MakePartForThreshold(parts, ref distance, 1000000L, "km");
            MakePartForThreshold(parts, ref distance, 1000L, "m");
            MakePartForThreshold(parts, ref distance, 10L, "cm");

            if (distance > 0)
                parts.Add($"{distance}mm");

            return string.Join(" ", parts);
        }

        private static void MakePartForThreshold(List<string> parts, ref long number, long threshold, string unit)
        {
            if (number < threshold)
                return;

            var val = number / threshold;
            parts.Add($"{val}{unit}");
            number -= val * threshold;
        }
    }
}