using System;
using System.Linq;
using AoCTools.Frame.Map.Extensions;
using AoCTools.Frame.TwoDimensions;
using NUnit.Framework;

namespace AoCTools_Tests.Frame.TwoDimensions.Map.Extensions
{
    [TestFixture]
    public class MapCardinalDirectionExtensionsTests
    {
        private readonly CardinalDirection[] _allDirections =
            Enum.GetValues(typeof(CardinalDirection)).Cast<CardinalDirection>().ToArray();

        [Test]
        public void HorizontalVertical()
        {
            foreach (var direction in _allDirections)
            {
                Assert.True(direction.IsHorizontal() || direction.IsVertical(), $"{direction} can't be horizontal and vertical at the same time.");
            }
        }

        [Test]
        public void Opposites()
        {
            foreach (var direction in _allDirections)
            {
                if (direction == CardinalDirection.None)
                    Assert.True(direction.GetOpposite() == CardinalDirection.None);
                else
                    Assert.True(direction.GetOpposite() != CardinalDirection.None, $"Opposite of {direction} can't be {CardinalDirection.None}");
            }

            Assert.True(CardinalDirection.None.GetOpposite() == CardinalDirection.None,
                $"Opposite of {CardinalDirection.None} should be {CardinalDirection.None} (found {CardinalDirection.None.GetOpposite()} instead.)");
            Assert.True(CardinalDirection.North.GetOpposite() == CardinalDirection.South,
                $"Opposite of {CardinalDirection.North} should be {CardinalDirection.South} (found {CardinalDirection.North.GetOpposite()} instead.)");
            Assert.True(CardinalDirection.South.GetOpposite() == CardinalDirection.North,
                $"Opposite of {CardinalDirection.South} should be {CardinalDirection.North} (found {CardinalDirection.South.GetOpposite()} instead.)");
            Assert.True(CardinalDirection.West.GetOpposite() == CardinalDirection.East,
                $"Opposite of {CardinalDirection.West} should be {CardinalDirection.East} (found {CardinalDirection.West.GetOpposite()} instead.)");
            Assert.True(CardinalDirection.East.GetOpposite() == CardinalDirection.West,
                $"Opposite of {CardinalDirection.East} should be {CardinalDirection.West} (found {CardinalDirection.East.GetOpposite()} instead.)");
        }

        [Test]
        public void Formatting()
        {
            foreach (var direction in _allDirections)
            {
                Assert.DoesNotThrow(() => direction.ToShortName());
            }
        }
    }
}