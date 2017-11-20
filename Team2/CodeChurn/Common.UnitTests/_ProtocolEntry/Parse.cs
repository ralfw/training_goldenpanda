using System;
using FluentAssertions;
using NUnit.Framework;

namespace Common.UnitTests._ProtocolEntry
{
    [TestFixture]
    public class Parse
    {
        [TestCase(@"2017-11-20T12:10:49; 120; c:\y.txt", "2017-11-20T12:10:49", 120, @"c:\y.txt")]
        public void ShouldParseTextLine(string line, string expectedDateTime, int expectedLoc, string expectedFilePath)
        {
            var protocolEntry = ProtocolEntry.Parse(line);

            protocolEntry.Timestamp.Should().Be(DateTime.Parse(expectedDateTime));
            protocolEntry.Loc.Should().Be(expectedLoc);
            protocolEntry.FilePath.Should().Be(expectedFilePath);

        }
    }
}