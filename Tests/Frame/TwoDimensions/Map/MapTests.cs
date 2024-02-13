using System;
using AoCTools.Error.Exception;
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
                new CharMap(null);
            }, "Can't create map with null content.");

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
    }
}