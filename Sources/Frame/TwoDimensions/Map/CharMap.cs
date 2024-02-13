using AoCTools.Error.Exception;
using AoCTools.Frame.TwoDimensions.Map.Abstracts;

namespace AoCTools.Frame.TwoDimensions.Map
{
    /// <summary>
    /// Represents a map.
    /// </summary>
    public class CharMap : Map<CharMapCell>
    {
        /// <summary>
        /// Creates a Map which cells each contain a char.
        /// </summary>
        /// <param name="mapChars">Chars in cells.</param>
        /// <exception cref="InvalidParameterException"/>
        public CharMap(char[][] mapChars) : base(mapChars)
        {
        }

        /// <inheritdoc/>
        protected override string LogTitle => "=== MAP ===";
    }
}