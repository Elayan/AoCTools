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
            Assert.True(_testCoordinates.Row == RowValue, $"{nameof(_testCoordinates)} was constructed with {nameof(RowValue)} of {RowValue}.");
            Assert.True(_testCoordinates.Col == ColValue, $"{nameof(_testCoordinates)} was constructed with {nameof(ColValue)} of {ColValue}.");

            var copy = new Coordinates(_testCoordinates);
            Assert.NotNull(copy);
            Assert.True(copy.Row == RowValue, $"Copy of {nameof(_testCoordinates)} should have {nameof(copy.Row)} value of {RowValue}.");
            Assert.True(copy.Col == ColValue, $"Copy of {nameof(_testCoordinates)} should have {nameof(copy.Col)} value of {ColValue}.");
        }

        [Test]
        public void Equality()
        {
            Assert.True(_testCoordinates.Equals(_testCoordinates));
            Assert.False(_testCoordinates.Equals(_otherCoordinates));
        }

        [Test]
        public void Operators()
        {
            var twice = new Coordinates(RowValue * 2, ColValue * 2);
            Assert.True(twice.Equals(_testCoordinates * 2));
            Assert.True(twice.Equals(2 * _testCoordinates));
            Assert.True(twice.Equals(_testCoordinates * 2L));
            Assert.True(twice.Equals(2L * _testCoordinates));

            var half = new Coordinates(RowValue / 2, ColValue / 2);
            Assert.True(half.Equals(_testCoordinates / 2));
            Assert.True(half.Equals(_testCoordinates / 2L));

            var plusOne = new Coordinates(RowValue + 1, ColValue + 1);
            Assert.True(plusOne.Equals(_testCoordinates + Coordinates.One));
            Assert.True(plusOne.Equals(Coordinates.One + _testCoordinates));

            var minusOne = new Coordinates(RowValue - 1, ColValue - 1);
            Assert.True(minusOne.Equals(_testCoordinates - Coordinates.One));
        }
    }
}