using System;
using System.Collections.Generic;
using Common;
using FluentAssertions;
using NUnit.Framework;

namespace Client.UnitTests._ReportGenerator
{
    [TestFixture]
    public class GenerateReportEntries
    {
        [Test]
        public void ShouldGenerateReportEntries()
        {
            var protocolEntries = new List<ProtocolEntry>
            {
                new ProtocolEntry(new DateTime(2017, 11, 10), 120, "x"),
                new ProtocolEntry(new DateTime(2017, 11, 10), 120, "y"),
                new ProtocolEntry(new DateTime(2017, 11, 11), 130, "x"),
                new ProtocolEntry(new DateTime(2017, 11, 11), 120, "y"),
                new ProtocolEntry(new DateTime(2017, 11, 12), 140, "x"),
                new ProtocolEntry(new DateTime(2017, 11, 12), 120, "y")
            };


            var reportEntries = ReportGenerator.GenerateReportEntries(protocolEntries);

            reportEntries.Count.Should().Be(2);
            reportEntries[0].FilePath.Should().Be("x");
            reportEntries[0].Churns.Should().Be(2);
            reportEntries[0].LastLoc.Should().Be(140);
            reportEntries[1].FilePath.Should().Be("y");
            reportEntries[1].Churns.Should().Be(0);
            reportEntries[1].LastLoc.Should().Be(120);
        }

        [Test]
        public void ShouldGenerateReportEntriesWithoutChanges()
        {
            var protocolEntries = new List<ProtocolEntry>
            {
                new ProtocolEntry(new DateTime(2017, 11, 10), 120, "x"),
                new ProtocolEntry(new DateTime(2017, 11, 10), 130, "y"),
                new ProtocolEntry(new DateTime(2017, 11, 11), 120, "x"),
                new ProtocolEntry(new DateTime(2017, 11, 11), 130, "y"),
                new ProtocolEntry(new DateTime(2017, 11, 12), 120, "x"),
                new ProtocolEntry(new DateTime(2017, 11, 12), 130, "y")
            };


            var reportEntries = ReportGenerator.GenerateReportEntries(protocolEntries);

            reportEntries.Count.Should().Be(2);
            reportEntries[0].FilePath.Should().Be("x");
            reportEntries[0].Churns.Should().Be(0);
            reportEntries[0].LastLoc.Should().Be(120);
            reportEntries[1].FilePath.Should().Be("y");
            reportEntries[1].Churns.Should().Be(0);
            reportEntries[1].LastLoc.Should().Be(130);
        }
    }
}