using AoCTools.Frame.ThreeDimensions;
using NUnit.Framework;

namespace AoCTools_Tests.Frame.ThreeDimensions
{
    [TestFixture]
    public class CoordinatesTests
    {
        private const long X = 2;
        private const long Y = 4;
        private const long Z = 6;
        private static readonly Coordinates CoordTest = new Coordinates(X, Y, Z);

        [Test]
        public void Creation()
        {
            Assert.NotNull(CoordTest);
            Assert.True(CoordTest.Row == X);
            Assert.True(CoordTest.Col == Y);
            Assert.True(CoordTest.Height == Z);
        }
    }
}