using System;
using System.Collections.Generic;
using System.Linq;
using CodeChurnReport.Structs;
using FluentAssertions;
using NUnit.Framework;

namespace CodeChurnReport.UnitTest._ReportBuilder
{
    [TestFixture]
    public class BuildReportItems
    {
        [Test]
        public void ShouldBuildValidReportItems()
        {
            var protocollItems = new List<ProtocolItem>
            {
                new ProtocolItem {TimeStamp = new DateTime(2000, 1, 1), UncFilePath = "a", LineOfCode = 1},
                new ProtocolItem {TimeStamp = new DateTime(2000, 1, 2), UncFilePath = "b", LineOfCode = 2},
                new ProtocolItem {TimeStamp = new DateTime(2000, 1, 3), UncFilePath = "a", LineOfCode = 3},
                new ProtocolItem {TimeStamp = new DateTime(2000, 1, 4), UncFilePath = "c", LineOfCode = 4}
            };
            var result = ReportBuilder.BuildReportItems(protocollItems).ToArray();
            result.Should().NotBeNull();
            result.Length.Should().Be(3);
            result[0].UncFilePath.Should().Be("a");
            result[0].LinesOfCode.Should().Be(3);
            result[0].ChurnRate.Should().Be(2);
            result[1].UncFilePath.Should().Be("b");
            result[1].LinesOfCode.Should().Be(2);
            result[1].ChurnRate.Should().Be(1);
            result[2].UncFilePath.Should().Be("c");
            result[2].LinesOfCode.Should().Be(4);
            result[2].ChurnRate.Should().Be(1);
            result.Should().NotBeNull();
        }
    }
}