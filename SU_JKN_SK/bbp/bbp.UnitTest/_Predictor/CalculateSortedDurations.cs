using System.Collections.Generic;
using System.Linq;
using bbp.dto;
using FluentAssertions;
using NUnit.Framework;

namespace bbp.UnitTest._Predictor
{
    [TestFixture]
    public class CalculateSortedDurations
    {
        [Test]
        public void ShouldCalculateSortedDurations()
        {
            var testdata = new List<UserStory>
            {
                new UserStory("2018-01-01", "2018-01-02"),
                new UserStory("2018-01-01", "2018-01-03"),
                new UserStory("2018-01-01", "2018-01-02"),
                new UserStory("2018-01-01", "2018-01-07"),
                new UserStory("2018-01-01", "2018-01-03"),
                new UserStory("2018-01-01", "2018-01-05"),
                new UserStory("2018-01-01", "2018-01-03"),
                new UserStory("2017-12-30", "2018-01-02")
            };

            var result = Predictor.CalculateSortedDurations(testdata).ToList();

            result[0].Should().Be(2);
            result[1].Should().Be(2);
            result[2].Should().Be(3);
            result[3].Should().Be(3);
            result[4].Should().Be(3);
            result[5].Should().Be(4);
            result[6].Should().Be(5);
            result[7].Should().Be(7);
        }
    }
}