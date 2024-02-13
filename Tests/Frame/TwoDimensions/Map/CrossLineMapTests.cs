using System;
using AoCTools.Error.Exception;
using AoCTools.Frame.TwoDimensions.Map;
using NUnit.Framework;

namespace AoCTools_Tests.Frame.TwoDimensions.Map
{
    [TestFixture]
    public class CrossLineMapTests
    {
        private const string Line1 = "abc";
        private const string Line2 = "def";
        private const string Line3 = "ghi";
        private static readonly string[] Content = { Line1, Line2, Line3 };
        private static readonly string[] InvalidContent = { Line1, "zz" };

        [Test]
        public void Creation()
        {
            var map = new CrossLineMap(Content);
            Assert.NotNull(map);
            Assert.True(map.LinesSize == 3);
            Assert.True(map.Lines[0] == Line1);
            Assert.True(map.Lines[1] == Line2);
            Assert.True(map.Lines[2] == Line3);
            Assert.True(map.ColumnsSize == 3);
            Assert.True(map.Columns[0] == "adg");
            Assert.True(map.Columns[1] == "beh");
            Assert.True(map.Columns[2] == "cfi");

            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<InvalidParameterException>(() => new CrossLineMap(InvalidContent));
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<InvalidParameterException>(() => new CrossLineMap(Array.Empty<string>()));
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<InvalidParameterException>(() => new CrossLineMap(null));
        }
    }
}