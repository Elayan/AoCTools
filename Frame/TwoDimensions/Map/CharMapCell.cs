using AoCTools.Frame.TwoDimensions.Map.Abstracts;

namespace AoCTools.Frame.TwoDimensions.Map
{
    /// <summary>
    /// Represents the cell of a map.
    /// </summary>
    public class CharMapCell : MapCell<char>
    {
        /// <inheritdoc/>
        public CharMapCell(char c, int row, int col) : base(c, row, col)
        {
        }

        /// <inheritdoc/>
        public override string ToString() => Content.ToString();
    }
}