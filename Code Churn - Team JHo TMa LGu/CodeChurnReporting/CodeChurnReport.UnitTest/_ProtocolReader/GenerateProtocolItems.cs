using System;
using System.Globalization;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace CodeChurnReport.UnitTest._ProtocolReader
{
    [TestFixture]
    public class GenerateProtocolItems
    {
        [Test]
        public void ShouldGenerateProtocolItemsFromProtocolLines()
        {
            var timeStamp = new DateTime(2000,1,1);
            var protocolLines = new[]
            {
                $"{timeStamp.ToString(CultureInfo.InvariantCulture)};a;1",
                $"{timeStamp.ToString(CultureInfo.InvariantCulture)};b;2"
            };
            var result = ProtocolReader.GenerateProtocolItems(protocolLines).ToArray();
            result.Length.Should().Be(2);
            result[0].TimeStamp.Should().Be(timeStamp);
            result[0].UncFilePath.Should().Be("a");
            result[0].LineOfCode.Should().Be(1);
            result[0].TimeStamp.Should().Be(timeStamp);
            result[1].UncFilePath.Should().Be("b");
            result[1].LineOfCode.Should().Be(2);

        }

        [Test]
        public void ShouldReturnEmptyListForNoLines()
        {
            var result = ProtocolReader.GenerateProtocolItems(new string[]{});
            result.Should().BeEmpty();
        }

    }
}