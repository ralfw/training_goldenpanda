using System.Collections.Generic;
using System.Linq;
using bbp.dto;
using FluentAssertions;
using NUnit.Framework;

namespace bbp.UnitTest._Predictor
{
    [TestFixture]
    public class Predict
    {
        [Test]
        public void ShouldPredict()
        {
            const float reliabilityLevel = .83f;
            var testData = new List<UserStory>
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

            var result = (PredictorResultEnumerable)Predictor.Predict(testData, reliabilityLevel);

            result.ReliabilityLevelIndex.Should().Be(2);

            result[0].Duration.Should().Be(2);
            result[0].Frequency.Should().Be(2);
            result[0].AccumulatedPercentile.Should().BeApproximately(.25f, .001f);

            result[1].Duration.Should().Be(3);
            result[1].Frequency.Should().Be(3);
            result[1].AccumulatedPercentile.Should().BeApproximately(.625f, .001f);

            result[2].Duration.Should().Be(4);
            result[2].Frequency.Should().Be(1);
            result[2].AccumulatedPercentile.Should().BeApproximately(.75f, .001f);

            result[3].Duration.Should().Be(5);
            result[3].Frequency.Should().Be(1);
            result[3].AccumulatedPercentile.Should().BeApproximately(.875f, .001f);

            result[4].Duration.Should().Be(7);
            result[4].Frequency.Should().Be(1);
            result[4].AccumulatedPercentile.Should().BeApproximately(1f, .001f);
        }
    }
}