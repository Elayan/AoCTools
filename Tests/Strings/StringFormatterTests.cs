using AoCTools.Strings;
using NUnit.Framework;

namespace AoCTools_Tests.Strings
{
    [TestFixture]
    public class StringFormatterTests
    {
        private const long Nine = 9;
        private const long Hundred = 123;
        private const long Thousand = 1234;
        private const long HundredThousand = 123456;

        [Test]
        public void HumanFriendlyTime()
        {
            var formatter = new StringFormatter();
            Assert.That(formatter, Is.Not.Null);

            Assert.That(
                formatter.GetHumanFriendlyTime(Nine),
                Is.EqualTo("9ms"));
            Assert.That(
                formatter.GetHumanFriendlyTime(Hundred),
                Is.EqualTo("123ms"));
            Assert.That(
                formatter.GetHumanFriendlyTime(Thousand),
                Is.EqualTo("1s 234ms"));
            Assert.That(
                formatter.GetHumanFriendlyTime(HundredThousand),
                Is.EqualTo("12min 3s 456ms"));
        }

        [Test]
        public void HumanFriendlyDistance()
        {
            var formatter = new StringFormatter();
            Assert.That(formatter, Is.Not.Null);

            Assert.That(
                formatter.GetHumanFriendlyDistance(Nine),
                Is.EqualTo("9mm"));
            Assert.That(
                formatter.GetHumanFriendlyDistance(Hundred),
                Is.EqualTo("12cm 3mm"));
            Assert.That(
                formatter.GetHumanFriendlyDistance(Thousand),
                Is.EqualTo("1m 23cm 4mm"));
            Assert.That(
                formatter.GetHumanFriendlyDistance(HundredThousand),
                Is.EqualTo("123m 45cm 6mm"));
        }
    }
}