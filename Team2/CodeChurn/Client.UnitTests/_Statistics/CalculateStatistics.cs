using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Client.UnitTests._Statistics
{
    [TestFixture]
    public class CalculateStatistics
    {
        [Test]
        public void ShouldCalculateStatistics()
        {
            var reportEntries = new List<ReportEntry>
            {
                new ReportEntry("x", 120) {Churns = 2},
                new ReportEntry("y", 130) {Churns = 3},
                new ReportEntry("z", 90) {Churns = 4},
                new ReportEntry("sameChurnsAsX", 90) {Churns = 2}
            };


            var statisticInfo = Statistics.Calculate(reportEntries);


            statisticInfo.FileCount.Should().Be(4);
            statisticInfo.MaxLoc.Should().Be(130);
            statisticInfo.MaxChurns.Should().Be(4);
        }
    }
}