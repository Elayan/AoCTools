using AoCTools.Error.Exception;
using AoCTools.Numbers;
using NUnit.Framework;

namespace AoCTools_Tests.Numbers
{
    [TestFixture]
    public class RangeTests
    {
        private const long A = 1;
        private const long B = 3;
        private const long C = 5;
        private const long D = 6;
        private const long E = 8;
        private const long F = 10;

        [Test]
        public void CreateFromMinMax()
        {
            var ac = Range.CreateFromMinMax(A, C);
            Assert.NotNull(ac);
            Assert.True(ac.Min == A, $"Min value should be {A}");
            Assert.True(ac.Max == C, $"Max value should be {C}");
            Assert.True(ac.Size == C - A + 1, $"Size should be {C - A + 1} (from {A} to {C})");

            Assert.Throws<InvalidParameterException>(() => Range.CreateFromMinMax(C, A));
        }

        [Test]
        public void CreateFromRange()
        {
            var range = Range.CreateFromRange(A, 10);
            Assert.NotNull(range);
            Assert.True(range.Min == A, $"Min value should be {A}");
            Assert.True(range.Max == A + 9, $"Max value should be {A + 9}");
            Assert.True(range.Size == 10, "Size should be 10");
        }

        [Test]
        public void CreateFromCopy()
        {
            var ac = Range.CreateFromMinMax(A, C);
            var copy = Range.CreateFromCopy(ac);
            Assert.NotNull(copy);
            Assert.True(copy.Min == A, $"Min value should be {A}");
            Assert.True(copy.Max == C, $"Max value should be {C}");
            Assert.True(copy.Size == C - A + 1, $"Size should be {C - A + 1} (from {A} to {C})");
        }

        [Test]
        public void CreateFromExcluding()
        {
            var ac = Range.CreateFromMinMax(A, C);
            var af = Range.CreateFromMinMax(A, F);
            var bd = Range.CreateFromMinMax(B, D);
            var be = Range.CreateFromMinMax(B, E);
            var df = Range.CreateFromMinMax(D, F);

            // exclude right part
            var expected = Range.CreateFromMinMax(A, B - 1);
            var excludeRight = Range.CreateFromExcluding(ac, bd);
            Assert.NotNull(excludeRight, "[excluding right] creation returned null");
            Assert.True(excludeRight.Length == 1, $"[excluding right] creation returned {excludeRight.Length} ranges (expected 1)");
            var acWithoutBd = excludeRight[0];
            Assert.True(acWithoutBd.Min == expected.Min, $"[excluding right] Min value should be {expected.Min}");
            Assert.True(acWithoutBd.Max == expected.Max, $"[excluding right] Max value should be {expected.Max}");
            Assert.True(acWithoutBd.Size == expected.Size, $"[excluding right] Size should be {expected.Size} (from {acWithoutBd.Min} to {acWithoutBd.Max})");

            // exclude left part
            expected = Range.CreateFromMinMax(C + 1, D);
            var excludeLeft = Range.CreateFromExcluding(bd, ac);
            Assert.NotNull(excludeLeft, "[excluding left] creation returned null");
            Assert.True(excludeLeft.Length == 1, $"[excluding left] creation returned {excludeLeft.Length} ranges (expected 1)");
            var bdWithoutAc = excludeLeft[0];
            Assert.True(bdWithoutAc.Min == expected.Min, $"[excluding left] Min value should be {expected.Min}");
            Assert.True(bdWithoutAc.Max == expected.Max, $"[excluding left] Max value should be {expected.Max}");
            Assert.True(bdWithoutAc.Size == expected.Size, $"[excluding left] Size should be {expected.Size} (from {bdWithoutAc.Min} to {bdWithoutAc.Max})");

            // exclude middle part
            var excludeMiddle = Range.CreateFromExcluding(af, be);
            Assert.NotNull(excludeMiddle, "[excluding middle] creation returned null");
            Assert.True(excludeMiddle.Length == 2, $"[excluding middle] creation returned {excludeMiddle.Length} ranges (expected 2)");

            expected = Range.CreateFromMinMax(A, B - 1);
            var left = excludeMiddle[0];
            Assert.True(left.Min == expected.Min, $"[excluding middle] Min value should be {expected.Min}");
            Assert.True(left.Max == expected.Max, $"[excluding middle] Max value should be {expected.Max}");
            Assert.True(left.Size == expected.Size, $"[excluding middle] Size should be {expected.Size} (from {left.Min} to {left.Max})");

            expected = Range.CreateFromMinMax(E + 1, F);
            var right = excludeMiddle[1];
            Assert.True(right.Min == expected.Min, $"[excluding middle] Min value should be {expected.Min}");
            Assert.True(right.Max == expected.Max, $"[excluding middle] Max value should be {expected.Max}");
            Assert.True(right.Size == expected.Size, $"[excluding middle] Size should be {expected.Size} (from {right.Min} to {right.Max})");

            // exclude nothing (right)
            var excludeNothing = Range.CreateFromExcluding(ac, df);
            Assert.NotNull(excludeNothing, "[excluding nothing (r)] creation returned null");
            Assert.True(excludeNothing.Length == 1, $"[excluding nothing (r)] creation return {excludeNothing.Length} ranges (expected 1)");
            var acComplete = excludeNothing[0];
            Assert.True(acComplete.Min == ac.Min, $"[excluding nothing (r)] Min value should be {ac.Min}");
            Assert.True(acComplete.Max == ac.Max, $"[excluding nothing (r)] Max value should be {ac.Max}");
            Assert.True(acComplete.Size == ac.Size, $"[excluding nothing (r)] Size should be {ac.Size} (from {acComplete.Min} to {acComplete.Max})");

            // exclude nothing (left)
            excludeNothing = Range.CreateFromExcluding(df, ac);
            Assert.NotNull(excludeNothing, "[excluding nothing (l)] creation returned null");
            Assert.True(excludeNothing.Length == 1, $"[excluding nothing (l)] creation return {excludeNothing.Length} ranges (expected 1)");
            var dfComplete = excludeNothing[0];
            Assert.True(dfComplete.Min == df.Min, $"[excluding nothing (l)] Min value should be {df.Min}");
            Assert.True(dfComplete.Max == df.Max, $"[excluding nothing (l)] Max value should be {df.Max}");
            Assert.True(dfComplete.Size == df.Size, $"[excluding nothing (l)] Size should be {df.Size} (from {dfComplete.Min} to {dfComplete.Max})");

            // exclude all
            var excludeAll = Range.CreateFromExcluding(ac, ac);
            Assert.NotNull(excludeAll, "[excluding all] creation returned null");
            Assert.True(excludeAll.Length == 0, $"[excluding all] creation returned {excludeAll.Length} ranges (expected 0)");
        }

        [Test]
        public void CreateFromIntersecting()
        {
            var ac = Range.CreateFromMinMax(A, C);
            var af = Range.CreateFromMinMax(A, F);
            var be = Range.CreateFromMinMax(B, E);
            var df = Range.CreateFromMinMax(D, F);

            // intersect left
            var intersectLeft = Range.CreateFromIntersecting(be, ac);
            Assert.NotNull(intersectLeft, "[intersecting left] creation returned null");
            var expected = Range.CreateFromMinMax(B, C);
            Assert.True(intersectLeft.Min == expected.Min, $"[intersecting left] Min value should be {expected.Min}");
            Assert.True(intersectLeft.Max == expected.Max, $"[intersecting left] Max value should be {expected.Max}");
            Assert.True(intersectLeft.Size == expected.Size, $"[intersecting left] Size should be {expected.Size} (from {intersectLeft.Min} to {intersectLeft.Max})");

            // intersect right
            var intersectRight = Range.CreateFromIntersecting(be, df);
            Assert.NotNull(intersectRight, "[intersecting right] creation returned null");
            expected = Range.CreateFromMinMax(D, E);
            Assert.True(intersectRight.Min == expected.Min, $"[intersecting right] Min value should be {expected.Min}");
            Assert.True(intersectRight.Max == expected.Max, $"[intersecting right] Max value should be {expected.Max}");
            Assert.True(intersectRight.Size == expected.Size, $"[intersecting right] Size should be {expected.Size} (from {intersectRight.Min} to {intersectRight.Max})");

            // intersect middle
            var intersectMiddle = Range.CreateFromIntersecting(af, be);
            Assert.NotNull(intersectMiddle, "[intersecting middle] creation returned null");
            expected = Range.CreateFromMinMax(B, E);
            Assert.True(intersectMiddle.Min == expected.Min, $"[intersecting middle] Min value should be {expected.Min}");
            Assert.True(intersectMiddle.Max == expected.Max, $"[intersecting middle] Max value should be {expected.Max}");
            Assert.True(intersectMiddle.Size == expected.Size, $"[intersecting middle] Size should be {expected.Size} (from {intersectMiddle.Min} to {intersectMiddle.Max})");

            // no intersection
            Assert.Throws<InvalidParameterException>(() => Range.CreateFromIntersecting(ac, df));
        }

        [Test]
        public void Equality()
        {
            var ac = Range.CreateFromMinMax(A, C);
            var copy = Range.CreateFromCopy(ac);
            var ac2 = Range.CreateFromMinMax(A, C);
            var be = Range.CreateFromMinMax(B, E);

            Assert.True(ac.Equals(ac), "Range should equal itself.");
            Assert.True(ac.Equals(copy), "Range should equal copy of itself.");
            Assert.True(ac.Equals(ac2), "Range should equal range created with same parameters.");
            Assert.False(ac.Equals(be), $"Range {ac} should not equal {be}");
        }

        [Test]
        public void IsInRange_Value()
        {
            var ac = Range.CreateFromMinMax(A, C);

            Assert.True(ac.IsInRange(A), $"{A} should be in range {ac}");
            Assert.True(ac.IsInRange(C), $"{C} should be in range {ac}");
            Assert.True(ac.IsInRange(A + 1), $"{A - 1} should be in range {ac}");
            Assert.False(ac.IsInRange(A - 1), $"{A - 1} should not be in range {ac}");
            Assert.False(ac.IsInRange(C + 1), $"{C + 1} should not be in range {ac}");
        }

        [Test]
        public void IsInRange_Range()
        {
            var ac = Range.CreateFromMinMax(A, C);
            var af = Range.CreateFromMinMax(A, F);
            var be = Range.CreateFromMinMax(B, E);
            var cd = Range.CreateFromMinMax(C, D);
            var df = Range.CreateFromMinMax(D, F);

            // overlap right
            var overlapRight = ac.IsInRange(be, out var beInAc);
            var expected = Range.CreateFromMinMax(B, C);
            Assert.True(overlapRight, $"{be} should be in range {ac}");
            Assert.True(beInAc.Equals(expected), $"Part of {be} in {ac} should be {expected} (found: {beInAc})");

            // overlap left
            var overlapLeft = be.IsInRange(ac, out var acInBe);
            expected = Range.CreateFromMinMax(B, C);
            Assert.True(overlapLeft, $"{ac} should be in range {be}");
            Assert.True(acInBe.Equals(expected), $"Part of {ac} in {be} should be {expected} (found: {acInBe})");

            // completely within
            var completelyWithin = af.IsInRange(be, out var beInAf);
            Assert.True(completelyWithin, $"{be} should be in range {af}");
            Assert.True(beInAf.Equals(be), $"Part of {be} in {af} should be {be} (found: {beInAf})");

            // same range
            var same = ac.IsInRange(ac, out var acInAc);
            Assert.True(same, $"{ac} should be in range {ac}");
            Assert.True(acInAc.Equals(ac), $"Part of {ac} in {ac} should be {ac} (found: {acInAc})");

            // not in range
            var notInside = ac.IsInRange(df, out var dfInAc);
            Assert.False(notInside, $"{df} should not be in range {ac}");
            Assert.IsNull(dfInAc, $"Not overlapping ranges should return null valid range (found: {dfInAc})");

            // touching right
            var touchRight = ac.IsInRange(cd, out var cdInAc);
            expected = Range.CreateFromMinMax(C, C);
            Assert.True(touchRight, $"{cd} should be in range {ac}");
            Assert.True(cdInAc.Equals(expected), $"Part of {cd} in {ac} should be {expected} (found: {cdInAc})");

            // touching left
            var touchLeft = cd.IsInRange(ac, out var acInCd);
            Assert.True(touchLeft, $"{ac} should be in range {cd}");
            Assert.True(acInCd.Equals(expected), $"Part of {ac} in {cd} should be {expected} (found: {acInCd})");
        }

        [Test]
        public void GetRangeIndex_Value()
        {
            var ac = Range.CreateFromMinMax(A, C);

            var val = ac.GetRangeIndex(A);
            Assert.True(val == 0, $"Index of {A} in {ac} should be 0 (found {val})");
            val = ac.GetRangeIndex(C);
            Assert.True(val == ac.Size - 1, $"Index of {C} in {ac} should be {ac.Size - 1} (found {val})");
            val = ac.GetRangeIndex(A + 1);
            Assert.True(val == 1, $"Index of {A + 1} in {ac} should be 1 (found {val})");
            Assert.Throws<InvalidParameterException>(() => ac.GetRangeIndex(42), "No index for values out of range.");
        }

        [Test]
        public void GetRangeIndex_Range()
        {
            var ac = Range.CreateFromMinMax(A, C);
            var bc = Range.CreateFromMinMax(B, C);
            var bd = Range.CreateFromMinMax(B, D);
            var cd = Range.CreateFromMinMax(C, D);
            var de = Range.CreateFromMinMax(D, E);

            // touch left
            var val = bd.GetRangeIndexes(bc);
            Assert.NotNull(val, $"Should find indexes for range {bc} inside {bd} (found null)");
            var expected = Range.CreateFromRange(0, bc.Size);
            Assert.True(val.Equals(expected), $"Indexes for {bc} inside {bd} should be {expected} (found {val})");

            // touch right
            val = bd.GetRangeIndexes(cd);
            Assert.NotNull(val, $"Should find indexes for range {cd} inside {bd} (found null)");
            expected = Range.CreateFromRange((long)(bd.Size - cd.Size), cd.Size);
            Assert.True(val.Equals(expected), $"Indexes for {cd} inside {bd} should be {expected} (found {val})");

            // overflow left
            Assert.Throws<InvalidParameterException>(() => bd.GetRangeIndexes(ac));

            // overflow right
            Assert.Throws<InvalidParameterException>(() => ac.GetRangeIndexes(bd));

            // not in range
            Assert.Throws<InvalidParameterException>(() => ac.GetRangeIndexes(de));
        }

        [Test]
        public void GetRangedValue_Value()
        {
            var ac = Range.CreateFromMinMax(A, C);

            var val = ac.GetRangedValue(0);
            Assert.True(val == A, $"Value at index 0 should be {A} (found {val})");
            val = ac.GetRangedValue(ac.Size - 1);
            Assert.True(val == C, $"Value at index {ac.Size - 1} should be {C} (found {val})");
            val = ac.GetRangedValue(1);
            Assert.True(val == A + 1, $"Value at index 1 should be {A + 1} (found {val})");
            Assert.Throws<InvalidParameterException>(() => ac.GetRangedValue(42), "No value at index out of range.");
        }

        [Test]
        public void GetRangedValue_Range()
        {
            var af = Range.CreateFromMinMax(A, F);

            // within
            var indexes = Range.CreateFromMinMax(6, 8);
            var val = af.GetRangedValues(indexes);
            Assert.NotNull(val, $"Should return a value range for indexes {indexes} (found null)");
            var expected = Range.CreateFromMinMax(7, 9);
            Assert.True(val.Equals(expected), $"Values for indexes {indexes} in {af} should be {expected} (found {val})");

            // touch left
            indexes = Range.CreateFromRange(0, 2);
            val = af.GetRangedValues(indexes);
            Assert.NotNull(val, $"Should return a value range for indexes {indexes} (found null)");
            expected = Range.CreateFromRange(af.Min, 2);
            Assert.True(val.Equals(expected), $"Values for indexes {indexes} in {af} should be {expected} (found {val})");

            // touch right
            indexes = Range.CreateFromRange(1, af.Size - 2);
            val = af.GetRangedValues(indexes);
            Assert.NotNull(val, $"Should return a value range for indexes {indexes} (found null)");
            expected = Range.CreateFromRange(af.Min + 1, af.Size - 2);
            Assert.True(val.Equals(expected), $"Values for indexes {indexes} in {af} should be {expected} (found {val})");

            // overflow
            indexes = Range.CreateFromRange(1, af.Size + 1);
            Assert.Throws<InvalidParameterException>(() => af.GetRangedValues(indexes));

            // not in range
            indexes = Range.CreateFromRange(42, 2);
            Assert.Throws<InvalidParameterException>(() => af.GetRangedValues(indexes));
        }
    }
}