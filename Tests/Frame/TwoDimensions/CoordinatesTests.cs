using AoCTools.Frame.TwoDimensions;
using NUnit.Framework;

namespace AoCTools_Tests.Frame.TwoDimensions
{
    [TestFixture]
    public class CoordinatesTests
    {
        [Test]
        public void StaticValues()
        {
            var zero = Coordinates.Zero;
            Assert.NotNull(zero);
            Assert.True(zero.Row == 0, $"{nameof(Coordinates.Zero)} should have its {nameof(zero.Row)} set to 0.");
            Assert.True(zero.Col == 0, $"{nameof(Coordinates.Zero)} should have its {nameof(zero.Col)} set to 0.");

            var one = Coordinates.One;
            Assert.NotNull(one);
            Assert.True(one.Row == 1, $"{nameof(Coordinates.One)} should have its {nameof(one.Row)} set to 1.");
            Assert.True(one.Col == 1, $"{nameof(Coordinates.One)} should have its {nameof(one.Col)} set to 1.");
        }

        private const long RowValue = 24;
        private const long ColValue = 42;
        private readonly Coordinates _testCoordinates = new Coordinates(RowValue, ColValue);
        private readonly Coordinates _otherCoordinates = new Coordinates(ColValue, RowValue);

        [Test]
        public void Constructors()
        {
            Assert.NotNull(_testCoordinates);
            Assert.AreEqual(RowValue, _testCoordinates.Row, $"{nameof(_testCoordinates)} was constructed with {nameof(RowValue)} of {RowValue}.");
            Assert.AreEqual(ColValue, _testCoordinates.Col, $"{nameof(_testCoordinates)} was constructed with {nameof(ColValue)} of {ColValue}.");

            var copy = new Coordinates(_testCoordinates);
            Assert.NotNull(copy);
            Assert.AreEqual(RowValue, copy.Row, $"Copy of {nameof(_testCoordinates)} should have {nameof(copy.Row)} value of {RowValue}.");
            Assert.AreEqual(ColValue, copy.Col, $"Copy of {nameof(_testCoordinates)} should have {nameof(copy.Col)} value of {ColValue}.");
        }

        [Test]
        public void Equality()
        {
            Assert.AreEqual(_testCoordinates, _testCoordinates);
            Assert.AreNotEqual(_testCoordinates, _otherCoordinates);
        }

        [Test]
        public void Operators()
        {
            var twice = new Coordinates(RowValue * 2, ColValue * 2);
            Assert.AreEqual(twice, _testCoordinates * 2);
            Assert.AreEqual(twice, 2 * _testCoordinates);
            Assert.AreEqual(twice, _testCoordinates * 2L);
            Assert.AreEqual(twice, 2L * _testCoordinates);

            var half = new Coordinates(RowValue / 2, ColValue / 2);
            Assert.AreEqual(half, _testCoordinates / 2);
            Assert.AreEqual(half, _testCoordinates / 2L);

            var plusOne = new Coordinates(RowValue + 1, ColValue + 1);
            Assert.AreEqual(plusOne, _testCoordinates + Coordinates.One);
            Assert.AreEqual(plusOne, Coordinates.One + _testCoordinates);

            var minusOne = new Coordinates(RowValue - 1, ColValue - 1);
            Assert.AreEqual(minusOne, _testCoordinates - Coordinates.One);
        }

        [Test]
        public void NeighborsOfSingleCoordinate()
        {
            var zeroCorner = new[] { Coordinates.Zero };
            var zeroCornerNeighbors = Coordinates.GetNeighbors(zeroCorner, considerDiagonals: false);
            Assert.AreEqual(4, zeroCornerNeighbors.Length, "Should return 4 horizontal and vertical neighbors.");
            Assert.That(zeroCornerNeighbors, Is.EquivalentTo(
                new Coordinates[]
                { 
                    new Coordinates(-1, 0),
                    new Coordinates(1, 0),
                    new Coordinates(0, -1),
                    new Coordinates(0, 1),
                }));

            zeroCornerNeighbors = Coordinates.GetNeighbors(zeroCorner);
            Assert.AreEqual(8, zeroCornerNeighbors.Length, "Should return 8 neighbors.");
            Assert.That(zeroCornerNeighbors, Is.EquivalentTo(
                new Coordinates[]
                {
                    new Coordinates(-1, 0),
                    new Coordinates(1, 0),
                    new Coordinates(0, -1),
                    new Coordinates(0, 1),
                    new Coordinates(-1, -1),
                    new Coordinates(-1, 1),
                    new Coordinates(1, -1),
                    new Coordinates(1, 1),
                }));
        }

        [Test]
        public void NeighborsOfCoordinates()
        {
            var coords = new[] { Coordinates.Zero, _testCoordinates };
            var neighbors = Coordinates.GetNeighbors(coords, considerDiagonals: false);
            Assert.AreEqual(8, neighbors.Length, "Should return 8 horizontal and vertical neighbors of two distant coordinates.");
            Assert.That(neighbors, Is.EquivalentTo(
                new Coordinates[]
                {
                    new Coordinates(-1, 0),
                    new Coordinates(1, 0),
                    new Coordinates(0, -1),
                    new Coordinates(0, 1),

                    new Coordinates(RowValue - 1, ColValue),
                    new Coordinates(RowValue + 1, ColValue),
                    new Coordinates(RowValue, ColValue - 1),
                    new Coordinates(RowValue, ColValue + 1),
                }));

            neighbors = Coordinates.GetNeighbors(coords);
            Assert.AreEqual(16, neighbors.Length, "Should return 16 neighbors of two distant coordinates.");
            Assert.That(neighbors, Is.EquivalentTo(
                new Coordinates[]
                {
                    new Coordinates(-1, 0),
                    new Coordinates(1, 0),
                    new Coordinates(0, -1),
                    new Coordinates(0, 1),
                    new Coordinates(-1, -1),
                    new Coordinates(-1, 1),
                    new Coordinates(1, -1),
                    new Coordinates(1, 1),

                    new Coordinates(RowValue - 1, ColValue),
                    new Coordinates(RowValue + 1, ColValue),
                    new Coordinates(RowValue, ColValue - 1),
                    new Coordinates(RowValue, ColValue + 1),
                    new Coordinates(RowValue - 1, ColValue - 1),
                    new Coordinates(RowValue - 1, ColValue + 1),
                    new Coordinates(RowValue + 1, ColValue - 1),
                    new Coordinates(RowValue + 1, ColValue + 1),
                }));
        }

        [Test]
        public void NeighborsOfGroupedCoordinates()
        {
            var coords = new[] { Coordinates.Zero, new Coordinates(0, 1) };
            var neighbors = Coordinates.GetNeighbors(coords, considerDiagonals: false);
            Assert.AreEqual(6, neighbors.Length, "Should return 6 horizontal and vertical neighbors for group of two coords.");
            Assert.That(neighbors, Is.EquivalentTo(
                new Coordinates[]
                {
                    new Coordinates(0, -1),
                    new Coordinates(-1, 0),
                    new Coordinates(-1, 1),
                    new Coordinates(1, 0),
                    new Coordinates(1, 1),
                    new Coordinates(0, 2),
                }));

            neighbors = Coordinates.GetNeighbors(coords);
            Assert.AreEqual(10, neighbors.Length, "Should return 10 neighbors for group of two coords.");
            Assert.That(neighbors, Is.EquivalentTo(
                new Coordinates[]
                {
                    new Coordinates(0, -1),
                    new Coordinates(-1, 0),
                    new Coordinates(-1, 1),
                    new Coordinates(1, 0),
                    new Coordinates(1, 1),
                    new Coordinates(0, 2),
                    new Coordinates(-1, -1),
                    new Coordinates(1, -1),
                    new Coordinates(-1, 2),
                    new Coordinates(1, 2),
                }));
        }
    }
}