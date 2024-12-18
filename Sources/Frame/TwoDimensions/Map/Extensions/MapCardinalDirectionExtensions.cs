using AoCTools.Error.Exception;
using AoCTools.Frame.TwoDimensions;

namespace AoCTools.Frame.Map.Extensions
{
    /// <summary>
    /// Extensions for CardinalDirection in Map context.
    /// </summary>
    public static class CardinalDirectionExtension
    {
        /// <summary>
        /// Indicates if direction is horizontal (westward or eastward).
        /// </summary>
        /// <param name="c">Considered direction.</param>
        /// <returns>TRUE is West or East, FALSE otherwise.</returns>
        public static bool IsHorizontal(this CardinalDirection c) =>
            c == CardinalDirection.West || c == CardinalDirection.East;

        /// <summary>
        /// Indicates if direction is vertical (southward or northward).
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool IsVertical(this CardinalDirection c) => !IsHorizontal(c);

        /// <summary>
        /// Get opposite direction.
        /// </summary>
        /// <param name="c">Considered direction.</param>
        /// <returns>Opposite direction.</returns>
        public static CardinalDirection GetOpposite(this CardinalDirection c) =>
            c == CardinalDirection.East ? CardinalDirection.West
            : c == CardinalDirection.North ? CardinalDirection.South
            : c == CardinalDirection.South ? CardinalDirection.North
            : c == CardinalDirection.West ? CardinalDirection.East
            : CardinalDirection.None;

        public static CardinalDirection GetRightTurn(this CardinalDirection c) =>
            c == CardinalDirection.East ? CardinalDirection.South
            : c == CardinalDirection.North ? CardinalDirection.East
            : c == CardinalDirection.South ? CardinalDirection.West
            : c == CardinalDirection.West ? CardinalDirection.North
            : CardinalDirection.None;

        public static CardinalDirection GetLeftTurn(this CardinalDirection c) =>
            c == CardinalDirection.East ? CardinalDirection.North
            : c == CardinalDirection.North ? CardinalDirection.West
            : c == CardinalDirection.South ? CardinalDirection.East
            : c == CardinalDirection.West ? CardinalDirection.South
            : CardinalDirection.None;

        private static Dictionary<CardinalDirection, Coordinates> _cardinalCoords =
            new Dictionary<CardinalDirection, Coordinates>
            {
                { CardinalDirection.North, new Coordinates(-1, 0) },
                { CardinalDirection.South, new Coordinates(1, 0) },
                { CardinalDirection.West, new Coordinates(0, -1) },
                { CardinalDirection.East, new Coordinates(0, 1) }
            };

        /// <summary>
        /// Get directional vector matching given direction.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Coordinates ToCoordinates(this CardinalDirection c) =>
            c == CardinalDirection.None
                ? null
                : _cardinalCoords[c];

        /// <summary>
        /// Provides one-letter short name for direction.
        /// </summary>
        /// <param name="c">Considered cardinal direction.</param>
        /// <returns>One-letter short name.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string ToShortName(this CardinalDirection c)
        {
            switch (c)
            {
                case CardinalDirection.North: return "N";
                case CardinalDirection.South: return "S";
                case CardinalDirection.East: return "E";
                case CardinalDirection.West: return "W";
                case CardinalDirection.None: return "_";
                default:
                    throw new UnknownEnumValueException(c, typeof(CardinalDirection));
            }
        }
    }
}