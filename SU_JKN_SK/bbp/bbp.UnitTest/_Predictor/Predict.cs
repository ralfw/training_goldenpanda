using System;
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
        [TestCase(TEST_DATA_KEY_DEFAULT)]
        [TestCase(TEST_DATA_KEY_ALTERNATIVE)]
        [TestCase(TEST_DATA_KEY_NO_DATA)]
        public void ShouldPredict(string testDataKey)
        {
            var dataArchive = _testDataArchive[testDataKey];
            var result = (PredictorResultEnumerable) Predictor.Predict(dataArchive.Data, dataArchive.ReliabilityLevel);

            result.ReliabilityLevelIndex.Should().Be(dataArchive.ExpectedReliabilityLevelIndex);
            result.Count.Should().Be(dataArchive.ExpectedResults.Count());

            var expectedResults = dataArchive.ExpectedResults.ToArray();
            for (var i = 0; i < result.Count; i++)
            {
                result[i].Duration.Should().Be(expectedResults[i].Duration);
                result[i].Frequency.Should().Be(expectedResults[i].Frequency);
                result[i].AccumulatedPercentile.Should().BeApproximately(expectedResults[i].AccumulatedPercentile, .001f);
            }
        }

        // Is this test really necessary??
        [Test, SetUICulture("en-US")]
        public void ShouldThrowArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => Predictor.Predict(null, 0));

            exception.Should().NotBeNull();
            exception.GetType().Should().Be(typeof(ArgumentNullException));
            exception.Message.Should().StartWith("Value cannot be null.");
        }

        public const string TEST_DATA_KEY_DEFAULT = "DEFAULT";
        public const string TEST_DATA_KEY_ALTERNATIVE = "ALTERNATIVE";
        public const string TEST_DATA_KEY_NO_DATA = "NO_DATA";

        private readonly Dictionary<string, TestData> _testDataArchive = new Dictionary<string, TestData>
        {
            {
                TEST_DATA_KEY_DEFAULT,
                new TestData
                {
                    ReliabilityLevel = .83f,
                    Data = new[]
                    {
                        new UserStory("2018-01-01", "2018-01-02"),
                        new UserStory("2018-01-01", "2018-01-03"),
                        new UserStory("2018-01-01", "2018-01-02"),
                        new UserStory("2018-01-01", "2018-01-07"),
                        new UserStory("2018-01-01", "2018-01-03"),
                        new UserStory("2018-01-01", "2018-01-05"),
                        new UserStory("2018-01-01", "2018-01-03"),
                        new UserStory("2017-12-30", "2018-01-02")
                    },
                    ExpectedReliabilityLevelIndex = 2,
                    ExpectedResults = new[]
                    {
                        new PredictorResult(2, .25f, 2),
                        new PredictorResult(3, .625f, 3),
                        new PredictorResult(4, .75f, 1),
                        new PredictorResult(5, .875f, 1),
                        new PredictorResult(7, 1f, 1)
                    }
                }
            },

            {
                TEST_DATA_KEY_ALTERNATIVE,
                new TestData
                {
                    ReliabilityLevel = .62f,
                    Data = new[]
                    {
                        new UserStory("2018-01-01", "2018-01-01"),
                        new UserStory("2018-01-01", "2018-01-01"),
                        new UserStory("2018-01-01", "2018-01-02"),
                        new UserStory("2018-01-01", "2018-01-02"),
                        new UserStory("2018-01-01", "2018-01-02"),
                        new UserStory("2018-01-01", "2018-01-03"),
                        new UserStory("2018-01-01", "2018-01-03"),
                        new UserStory("2017-12-30", "2018-01-03")
                    },
                    ExpectedReliabilityLevelIndex = 0,
                    ExpectedResults = new[]
                    {
                        new PredictorResult(1, .25f, 2),
                        new PredictorResult(2, .625f, 3),
                        new PredictorResult(3, .875f, 2),
                        new PredictorResult(5, 1f, 1)
                    }
                }
            },

            {
                TEST_DATA_KEY_NO_DATA,
                new TestData
                {
                    ReliabilityLevel = 0,
                    Data = new UserStory[0],
                    ExpectedReliabilityLevelIndex = 0,
                    ExpectedResults = new PredictorResult[0]
                }
            }
        };

        private class TestData
        {
            public float ReliabilityLevel { get; set; }
            public IEnumerable<UserStory> Data { get; set; }

            public int ExpectedReliabilityLevelIndex { get; set; }
            public IEnumerable<PredictorResult> ExpectedResults { get; set; }
        }
    }
}