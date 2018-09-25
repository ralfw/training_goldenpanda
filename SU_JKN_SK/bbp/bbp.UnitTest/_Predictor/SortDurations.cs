using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace bbp.UnitTest._Predictor
{
    [TestFixture]
    public class SortDurations
    {
        [Test]
        public void ShouldReturnSortedDurations()
        {
            var unsorted = new List<int> {1, 5, 2, 7, 3, 1, 2};

            var sorted = Predictor.SortDurations(unsorted);

            sorted[0].Should().Be(1);
            sorted[1].Should().Be(1);
            sorted[2].Should().Be(2);
            sorted[3].Should().Be(2);
            sorted[4].Should().Be(3);
            sorted[5].Should().Be(5);
            sorted[6].Should().Be(7);
        }
    }
}