using System;
using AoCTools.Error.Exception;
using AoCTools.Frame.TwoDimensions;
using AoCTools.Frame.TwoDimensions.Map;
using NUnit.Framework;

namespace AoCTools_Tests.Frame.TwoDimensions.Map
{
    [TestFixture]
    public class MapTests
    {
        private static readonly char[][] Content =
            {
                new [] { 'a', 'b', 'c' },
                new [] { 'd', 'e', 'f' },
                new [] { 'g', 'h', 'i' },
                new [] { 'j', 'k', 'l' }
            };
        private static readonly char[][] InvalidContent =
        {
            new [] { 'a', 'b', 'c' },
            new [] { 'd', 'e', 'f', 'g' }
        };

        private static readonly CharMap TestCharMap = new CharMap(Content);

        [Test]
        public void MapCreation()
        {
            Assert.NotNull(TestCharMap);
            Assert.True(TestCharMap.RowCount == Content.Length);
            Assert.True(TestCharMap.ColCount == Content[0].Length);
            Assert.True(TestCharMap.MapCells.Length == Content.Length);
            Assert.True(TestCharMap.MapCells[0].Length == Content[0].Length);
        }

        [Test]
        public void InvalidMapCreation()
        {
            Assert.Throws<InvalidParameterException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new CharMap(null as char[][]);
            }, "Can't create map with null char[][] content.");

            Assert.Throws<InvalidParameterException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new CharMap(null as string[]);
            }, "Can't create map with null string[] content.");

            Assert.Throws<InvalidParameterException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new CharMap(Array.Empty<char[]>());
            }, "Can't create map with empty content.");

            Assert.Throws<InvalidParameterException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new CharMap(new [] { Array.Empty<char>() });
            }, "Can't create map with empty lines.");

            Assert.Throws<InvalidParameterException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new CharMap(InvalidContent);
            }, "Can't create map with uneven lines.");
        }

        [Test]
        public void GetCells()
        {
            Assert.AreEqual(TestCharMap.GetCell(0, 0), TestCharMap.GetCell(Coordinates.Zero), $"GetCell(0,0) = {TestCharMap.GetCell(0, 0)} and GetCell(Coordinates.Zero) = {TestCharMap.GetCell(Coordinates.Zero)}");
            Assert.AreEqual(TestCharMap.GetCell(1, 1), TestCharMap.GetCell(Coordinates.One), $"GetCell(1,1) = {TestCharMap.GetCell(0, 0)} and GetCell(Coordinates.One) = {TestCharMap.GetCell(Coordinates.Zero)}");
        }

        [Test]
        public void IsCoordinatesWithinBounds()
        {
            Assert.IsTrue(TestCharMap.IsCoordinateInMap(0, 0), "(0,0) should be in map.");
            Assert.IsTrue(TestCharMap.IsCoordinateInMap(Coordinates.Zero), "Coordinates.Zero should be in map.");

            Assert.IsFalse(TestCharMap.IsCoordinateInMap(-1, 0), "(-1,0) should not be in map.");
            Assert.IsFalse(TestCharMap.IsCoordinateInMap(new Coordinates(-1,0)), "Coordinates(-1,0) should not be in map.");
            Assert.IsFalse(TestCharMap.IsCoordinateInMap(0, -1), "(0,-1) should not be in map.");
            Assert.IsFalse(TestCharMap.IsCoordinateInMap(new Coordinates(0, -1)), "Coordinates(0,-1) should not be in map.");

            Assert.IsFalse(TestCharMap.IsCoordinateInMap(100, 0), "(100,0) should not be in map.");
            Assert.IsFalse(TestCharMap.IsCoordinateInMap(new Coordinates(100, 0)), "Coordinates(100,0) should not be in map.");
            Assert.IsFalse(TestCharMap.IsCoordinateInMap(0, 100), "(0,100) should not be in map.");
            Assert.IsFalse(TestCharMap.IsCoordinateInMap(new Coordinates(0, 100)), "Coordinates(0,100) should not be in map.");
        }
    }
}