using System.Text;
using AoCTools.Error.Exception;
using AoCTools.Frame.TwoDimensions.Map.Interfaces;

namespace AoCTools.Frame.TwoDimensions.Map.Abstracts
{
    public abstract class Map<T> : IMap
        where T : IMapCell
    {
        /// <summary>
        /// Creates a Map of cells.
        /// </summary>
        /// <param name="mapCells">Map cells.</param>
        /// <exception cref="InvalidParameterException"/>
        public Map(T[][] mapCells)
        {
            if (mapCells == null || mapCells.Length == 0 || mapCells[0].Length == 0)
                throw new InvalidParameterException(nameof(mapCells), "map content can't be empty");
            if (mapCells.Select(m => m.Length).Any(l => l != mapCells[0].Length))
                throw new InvalidParameterException(nameof(mapCells), "all lines should have the same length");

            MapCells = mapCells;
            RowCount = MapCells.Length;
            ColCount = MapCells[0].Length;
        }

        /// <summary>
        /// Creates a Map of cells, which are each built from a char.
        /// </summary>
        /// <param name="charCells">Map cells as chars.</param>
        /// <exception cref="InvalidParameterException"/>
        public Map(char[][] charCells) : this(ConvertToCells(charCells))
        { }

        private static T[][] ConvertToCells(char[][] charContent)
        {
            if (charContent == null)
                return null;

            var cellMap = new List<T[]>();
            for (var i = 0; i < charContent.Length; i++)
            {
                var cellLine = new List<T>();
                for (var j = 0; j < charContent[i].Length; j++)
                {
                    cellLine.Add((T)Activator.CreateInstance(typeof(T), charContent[i][j], i, j));
                }
                cellMap.Add(cellLine.ToArray());
            }
            return cellMap.ToArray();
        }

        /// <summary>
        /// Map content.
        /// </summary>
        public T[][] MapCells { get; }
        /// <summary>
        /// Map as flattened cells.
        /// </summary>
        public T[] AllCells => MapCells.SelectMany(row => row).ToArray();

        /// <summary>
        /// Get cell as coordinates.
        /// </summary>
        /// <param name="row">Row position.</param>
        /// <param name="col">Column position.</param>
        /// <returns>Cell at coordinates.</returns>
        public T GetCell(long row, long col) => MapCells[row][col];

        /// <summary>
        /// Get cell as coordinates.
        /// </summary>
        /// <param name="coord">Coordinates.</param>
        /// <returns>Cell at coordinates.</returns>
        public T GetCell(Coordinates coord) => GetCell(coord.Row, coord.Col);

        /// <summary>
        /// Row count.
        /// </summary>
        public int RowCount { get; private set; }
        /// <summary>
        /// Column count.
        /// </summary>
        public int ColCount { get; private set; }

        /// <summary>
        /// Indicates if coordinates are within map bounds.
        /// </summary>
        /// <param name="row">Row position.</param>
        /// <param name="col">Column position.</param>
        /// <returns>TRUE if coordinates are within map bounds.</returns>
        public bool IsCoordinateInMap(long row, long col) => row >= 0 && col >= 0 && row < RowCount && col < ColCount;

        /// <summary>
        /// Indicates if coordinates are within map bounds.
        /// </summary>
        /// <param name="coord">Coordinates.</param>
        /// <returns>TRUE if coordinates are within map bounds.</returns>
        public bool IsCoordinateInMap(Coordinates coord) => IsCoordinateInMap(coord.Row, coord.Col);

        /// <summary>
        /// Title of the Map.
        /// </summary>
        protected virtual string LogTitle => "=== MAP ===";
        /// <inheritdoc/>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(LogTitle);
            foreach (var line in MapCells)
            {
                foreach (var cell in line)
                    sb.Append(cell);
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}