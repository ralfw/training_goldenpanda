using CodeChurnReport.Behavior.Portals;
using CodeChurnReport.Data;
using FluentAssertions;
using NUnit.Framework;

namespace CodeChurnReport.UnitTest._StatisticAdapter
{
    [TestFixture]
    public class GenerateStatistic
    {
        [Test]
        public void ShouldReturnEmptyStatisticForNoReportItems()
        {
            var reportItems = new ReportItem[] { };

            var result = StatisticAdapter.GenerateStatistic(reportItems);

            result.Should().NotBeNull();
            result.FileCount.Should().Be(0);
            result.MaxChurnRate.Should().Be(0);
            result.MaxLinesOfCode.Should().Be(0);
        }

        [Test]
        public void ShouldReturnValidStatisticForReportItems()
        {
            var reportItems = new[]
            {
                new ReportItem {ChurnRate = 2, LinesOfCode = 10, UncFilePath = "a"},
                new ReportItem {ChurnRate = 1, LinesOfCode = 100, UncFilePath = "b"},
                new ReportItem {ChurnRate = 5, LinesOfCode = 20, UncFilePath = "c"}
            };

            var result = StatisticAdapter.GenerateStatistic(reportItems);

            result.Should().NotBeNull();
            result.FileCount.Should().Be(3);
            result.MaxChurnRate.Should().Be(5);
            result.MaxLinesOfCode.Should().Be(100);
        }
    }
}