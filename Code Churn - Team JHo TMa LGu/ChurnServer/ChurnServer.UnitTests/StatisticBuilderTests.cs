using System;
using ChurnServer.AdapterInterfaces;
using ChurnServer.Statistic;
using NUnit.Framework;
using FluentAssertions;
using Microsoft.Win32;
using Moq;

namespace ChurnServer.UnitTests
{
    [TestFixture]
    public class StatisticBuilderTests
    {
        [Test]
        public void ShouldBuildStatistic()
        {
            var startTime = DateTime.Parse("09.12.2017 17:00:00");
            var endTime = DateTime.Parse("09.12.2017 17:01:33");

            string[] filesPaths = {
                @"filePath1",
                @"filePath2",
                @"filePath3",
            };
            var timeProvider = new Mock<ITimeProvider>();
            timeProvider.Setup(x => x.GetCurrentDateAndTime()).Returns(endTime);

            var statistic = StatisticBuilder.BuildStatistic(timeProvider.Object, startTime, filesPaths);

            statistic.StartTime.Should().Be(startTime);
            statistic.Duration.Should().Be(TimeSpan.FromSeconds(93));
            statistic.NumberOfFiles.Should().Be(filesPaths.Length);
        }
    }
}