using System;
using System.Collections.Generic;
using CodeChurnReport.Behavior.Core;
using CodeChurnReport.Data;
using FluentAssertions;
using NUnit.Framework;

namespace CodeChurnReport.UnitTest._ProtocollItemsExtension
{
    [TestFixture]
    public class GetChurnRate
    {
        [Test]
        public void ShouldReturnZeroForEmptyItems()
        {
            var items = new ProtocolItem[] { };

            var result = items.GetChurnRate();

            result.Should().Be(0);
        }

        [Test]
        public void ShouldReturnIncreaseWhenLinesOfCodeareNotEqual()
        {
            var source = new List<ProtocolItem>
            {
                new ProtocolItem {TimeStamp = new DateTime(2000, 1, 1), UncFilePath = "a", LineOfCode = 1},
                new ProtocolItem {TimeStamp = new DateTime(2000, 2, 1), UncFilePath = "a", LineOfCode = 2},
                new ProtocolItem {TimeStamp = new DateTime(2000, 3, 1), UncFilePath = "a", LineOfCode = 2},
                new ProtocolItem {TimeStamp = new DateTime(2000, 4, 1), UncFilePath = "a", LineOfCode = 1}
            };

            var result = source.GetChurnRate();

            result.Should().Be(3);
        }
    }
}