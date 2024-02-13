using Coordinates2d = AoCTools.Frame.TwoDimensions.Coordinates;

namespace AoCTools.Frame.ThreeDimensions
{
    public class Coordinates : Coordinates2d
    {
        public Coordinates(long row, long col, long height) : base(row, col)
        {
            Height = height;
        }

        public long Height { get; }

        public override string ToString()
        {
            return $"({Row},{Col},{Height})";
        }
    }
}