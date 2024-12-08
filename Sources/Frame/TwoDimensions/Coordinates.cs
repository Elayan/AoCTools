using System;
using System.Diagnostics;

namespace AoCTools.Frame.TwoDimensions
{
    /// <summary>
    /// Couple of values corresponding to Row and Column values in a grid.
    /// </summary>
    public class Coordinates : IEquatable<Coordinates>
    {
        /// <summary>
        /// Creates a Coordinate instance that is (0,0).
        /// </summary>
        public static Coordinates Zero => new Coordinates(0, 0);
        /// <summary>
        /// Creates a Coordinate instance that is (1,1).
        /// </summary>
        public static Coordinates One => new Coordinates(1, 1);

        /// <summary>
        /// Creates a coordinate couple of Row and Column values for a grid.
        /// </summary>
        /// <param name="row">Row value.</param>
        /// <param name="col">Column value.</param>
        public Coordinates(long row, long col)
        {
            Row = row;
            Col = col;
        }

        /// <summary>
        /// Creates a copy of a Coordinate instance.
        /// </summary>
        /// <param name="copy">Coordinate instance to copy from.</param>
        public Coordinates(Coordinates copy)
        {
            Row = copy.Row;
            Col = copy.Col;
        }

        /// <summary>
        /// Row value.
        /// </summary>
        public long Row { get; set; }
        /// <summary>
        /// Column value.
        /// </summary>
        public long Col { get; set; }

        /// <summary>
        /// Formats instance as "(R,C)", with R being Row value and C being Column value.
        /// </summary>
        public override string ToString()
        {
            return $"({Row},{Col})";
        }

        #region IEquatable

        /// <inheritsdoc/>
        public override int GetHashCode()
        {
            return Row.GetHashCode() * 17 + Col.GetHashCode();
        }

        /// <inheritsdoc/>
        public override bool Equals(object obj)
        {
            if (!(obj is Coordinates other))
                return false;

            return Equals(other);
        }

        public bool Equals(Coordinates other)
        {
            if (other == null)
                return false;

            return Row == other.Row && Col == other.Col;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Creates new Coordinate instance which represents the addition of two Coordinate instances.
        /// </summary>
        public static Coordinates operator +(Coordinates a, Coordinates b)
        {
            return new Coordinates(a.Row + b.Row, a.Col + b.Col);
        }
        /// <summary>
        /// Creates new Coordinate instance which represents the substraction of two Coordinate instances.
        /// </summary>
        public static Coordinates operator -(Coordinates a, Coordinates b) => a + -1 * b;

        /// <summary>
        /// Creates new Coordinate instance which represents the multiplication of a value to a Coordinate instance.
        /// </summary>
        public static Coordinates operator *(Coordinates a, long mul)
        {
            return new Coordinates(mul * a.Row, mul * a.Col);
        }
        /// <summary>
        /// Creates new Coordinate instance which represents the multiplication of a value to a Coordinate instance.
        /// </summary>
        public static Coordinates operator *(int mul, Coordinates a) => a * (long)mul;
        /// <summary>
        /// Creates new Coordinate instance which represents the multiplication of a value to a Coordinate instance.
        /// </summary>
        public static Coordinates operator *(Coordinates a, int mul) => a * (long)mul;
        /// <summary>
        /// Creates new Coordinate instance which represents the multiplication of a value to a Coordinate instance.
        /// </summary>
        public static Coordinates operator *(long mul, Coordinates a) => a * mul;

        /// <summary>
        /// Creates new Coordinate instance which represents the division of a value to a Coordinate instance.
        /// </summary>
        public static Coordinates operator /(Coordinates a, int div) => a / (long)div;
        /// <summary>
        /// Creates new Coordinate instance which represents the division of a value to a Coordinate instance.
        /// </summary>
        public static Coordinates operator /(Coordinates a, long div)
        {
            return new Coordinates(a.Row / div, a.Col / div);
        }

        #endregion

        /// <summary>
        /// Gets all neighbors for all given <paramref name="coords"/>.
        /// </summary>
        /// <param name="coords"></param>
        /// <param name="considerDiagonals">If set to FALSE, it will only consider horizontal and vertical neighbors.</param>
        /// <returns>Array of all neighbors, no sort, no check for uniqueness.</returns>
        public static Coordinates[] GetNeighbors(Coordinates[] coords, bool considerDiagonals = true)
        {
            var neighbors = new HashSet<Coordinates>(coords);

            for (var i = 0; i < coords.Length; i++)
            {
                var coord = coords[i];

                // left
                neighbors.Add(new Coordinates(coord.Row, coord.Col - 1));

                if (considerDiagonals)
                {
                    neighbors.Add(new Coordinates(coord.Row - 1, coord.Col - 1));
                    neighbors.Add(new Coordinates(coord.Row + 1, coord.Col - 1));
                }

                // up and down
                neighbors.Add(new Coordinates(coord.Row - 1, coord.Col));
                neighbors.Add(new Coordinates(coord.Row + 1, coord.Col));

                // right
                neighbors.Add(new Coordinates(coord.Row, coord.Col + 1));

                if (considerDiagonals)
                {
                    neighbors.Add(new Coordinates(coord.Row - 1, coord.Col + 1));
                    neighbors.Add(new Coordinates(coord.Row + 1, coord.Col + 1));
                }
            }

            for (var i = 0; i < coords.Length; i++)
            {
                neighbors.Remove(coords[i]);
                Debug.WriteLine("Hellow?");
            }

            return neighbors.ToArray();
        }

        /// <summary>
        /// Computes coordinates of point between <paramref name="coordinates1"/> and <paramref name="coordinates2"/>.
        /// </summary>
        /// <param name="coordinates1"></param>
        /// <param name="coordinates2"></param>
        /// <returns></returns>
        public static Coordinates GetCenter(Coordinates coordinates1, Coordinates coordinates2)
        {
            return (coordinates1 + coordinates2) / 2L;
        }

        /// <summary>
        /// Computes coordinates of the point being the symetric of <paramref name="reference"/> by <paramref name="center"/>
        /// </summary>
        /// <param name="reference">The reference point for symetry</param>
        /// <param name="center">Center of symetry</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static Coordinates GetSymmetric(Coordinates reference, Coordinates center)
        {
            return center + center - reference;
        }
    }
}