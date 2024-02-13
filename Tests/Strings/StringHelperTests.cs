using AoCTools.Error.Exception;
using AoCTools.Strings;
using NUnit.Framework;

namespace AoCTools_Tests.Strings
{
    [TestFixture]
    public class StringHelperTests
    {
        [Test]
        public void CountDifferences()
        {
            var str = "abcde";
            var strA = "abcde";
            var strB = "_bcd_";
            var strC = "wxyz_";

            var res = str.CountDifferences(str);
            Assert.True(res == 0, "should have 0 difference with itself");
            res = str.CountDifferences(strA);
            Assert.True(res == 0, $"should have 0 differences between {str} and {strA} (found {res})");
            res = str.CountDifferences(strB);
            Assert.True(res == 2, $"should have 2 differences between {str} and {strB} (found {res})");
            res = str.CountDifferences(strC);
            Assert.True(res == 5, $"should have 2 differences between {str} and {strC} (found {res})");

            Assert.Throws<InvalidParameterException>(() => str.CountDifferences(string.Empty));
        }
    }
}