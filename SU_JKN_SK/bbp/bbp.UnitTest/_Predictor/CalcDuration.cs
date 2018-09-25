using FluentAssertions;
using NUnit.Framework;

namespace bbp.UnitTest._Predictor
{
    [TestFixture]
    public class CalcDuration
    {
        [TestCase("2018-01-01", "2018-01-02", 2)]
        [TestCase("2018-01-01", "2018-01-03", 3)]
        [TestCase("2018-01-01", "2018-01-02", 2)]
        [TestCase("2018-01-01", "2018-01-07", 7)]
        [TestCase("2018-01-01", "2018-01-03", 3)]
        [TestCase("2018-01-01", "2018-01-05", 5)]
        [TestCase("2018-01-01", "2018-01-03", 3)]
        [TestCase("2017-12-30", "2018-01-02", 4)]
        public void ShouldCalculateDuration(string startDate, string endDate, int expectedDuration)
        {
            var duration = Predictor.CalcDuration(startDate, endDate);
            duration.Should().Be(expectedDuration);
        }
    }
}