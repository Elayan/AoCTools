using AoCTools.Frame.TwoDimensions.Map;
using NUnit.Framework;

namespace AoCTools_Tests.Frame.TwoDimensions.Map
{
    [TestFixture]
    public class MapCellTests
    {
        private const char TestChar = 'c';
        private const int TestRow = 24;
        private const int TestColumn = 42;
        private static readonly CharMapCell TestCell = new CharMapCell(TestChar, TestRow, TestColumn);

        [Test]
        public void CellCreation()
        {
            Assert.NotNull(TestCell);
            Assert.True(TestCell.Content == TestChar);
            Assert.NotNull(TestCell.Coordinates);
            Assert.True(TestCell.Coordinates.Row == TestRow);
            Assert.True(TestCell.Coordinates.Col == TestColumn);
        }
    }
}