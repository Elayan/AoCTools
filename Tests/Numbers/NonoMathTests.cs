using AoCTools.Error.Exception;
using AoCTools.Numbers;
using NUnit.Framework;

namespace AoCTools_Tests.Numbers
{
    [TestFixture]
    public class NonoMathTests
    {
        [Test]
        public void GCF()
        {
            Assert.Throws<InvalidParameterException>(() => NonoMath.GCF(0, 2));
            Assert.Throws<InvalidParameterException>(() => NonoMath.GCF(2, 0));

            Assert.That(NonoMath.GCF(30, 20), Is.EqualTo(10));
        }

        [Test]
        public void LCM()
        {
            Assert.Throws<InvalidParameterException>(() => NonoMath.LCM(0, 2));
            Assert.Throws<InvalidParameterException>(() => NonoMath.LCM(2, 0));

            Assert.That(NonoMath.LCM(30, 20), Is.EqualTo(60));
            Assert.That(NonoMath.LCM(5, 15), Is.EqualTo(15));
        }

        [Test]
        public void SumFirstIntegers()
        {
            Assert.That(NonoMath.SumFirstIntegers(0), Is.EqualTo(0));
            Assert.That(NonoMath.SumFirstIntegers(1), Is.EqualTo(1));
            Assert.That(NonoMath.SumFirstIntegers(10), Is.EqualTo(55));
        }
    }
}