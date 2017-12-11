using System;
using System.IO;
using CodeChurnReport.Structs;
using FluentAssertions;
using NUnit.Framework;

namespace CodeChurnReport.UnitTest._ReportPersistence
{
    [TestFixture]
    public class StoreReport
    {
        [Test]
        public void ShouldCreateReportFileAndSaveContentToIt()
        {
            var startDate = new DateTime(2000, 1, 1);
            var endDate = new DateTime(2000, 2, 1);
            var currentDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}";
            var expectedFilePath = $"{currentDirectory}\\churnreport_20000101_20000201.csv";
            if (File.Exists(expectedFilePath))
                File.Delete(expectedFilePath);
            var config = new Config {StartDate = startDate, EndDate = endDate, ProtocolFilePath = $"{currentDirectory}\\test.csv" };
            var content = new[]
            {
                new ReportItem {ChurnRate = 1, LinesOfCode = 100, UncFilePath = "a"},
                new ReportItem {ChurnRate = 2, LinesOfCode = 200, UncFilePath = "b"}
            };

            ReportPersistence.StoreReport(config, content);

            File.Exists(expectedFilePath).Should().BeTrue();
            var fileContent = File.ReadAllLines(expectedFilePath);
            fileContent.Length.Should().Be(2);
            fileContent[0].Should().Be("100;1;a");
            fileContent[1].Should().Be("200;2;b");
        }
    }
}