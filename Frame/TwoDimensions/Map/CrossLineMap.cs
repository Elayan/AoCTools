using System.Linq;
using System.Text;
using AoCTools.Error.Exception;
using AoCTools.Frame.TwoDimensions.Map.Interfaces;

namespace AoCTools.Frame.TwoDimensions.Map
{
    public class CrossLineMap : IMap
    {
        /// <summary>
        /// Represents a map made of intersecting string lines.
        /// </summary>
        /// <param name="lines">String lines.</param>
        public CrossLineMap(string[] lines)
        {
            if (lines == null || lines.Length == 0)
                throw new InvalidParameterException(nameof(lines), "can't create with empty data.");
            var minLength = lines.Min(l => l.Length);
            if (lines.Any(l => l.Length != minLength))
                throw new InvalidParameterException(nameof(lines), "lines must be of equal length.");

            Lines = lines;
            LinesSize = Lines[0].Length;
            Columns = Enumerable.Range(0, lines[0].Length)
                .Select(r => string.Join("", lines.Select(l => l[r])))
                .ToArray();
            ColumnsSize = Columns[0].Length;
        }

        /// <summary>
        /// String lines.
        /// </summary>
        public string[] Lines { get; protected set; }
        /// <summary>
        /// String columns.
        /// </summary>
        public string[] Columns { get; protected set; }

        /// <summary>
        /// Line size.
        /// </summary>
        public int LinesSize { get; private set; }
        /// <summary>
        /// Column size.
        /// </summary>
        public int ColumnsSize { get; private set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var line in Lines)
                sb.AppendLine(line);
            return sb.ToString();
        }
    }
}