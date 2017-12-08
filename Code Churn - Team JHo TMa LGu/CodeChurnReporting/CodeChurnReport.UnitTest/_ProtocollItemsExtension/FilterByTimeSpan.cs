using System;
using System.Collections.Generic;
using System.Linq;
using CodeChurnReport.Structs;
using FluentAssertions;
using NUnit.Framework;

namespace CodeChurnReport.UnitTest._ProtocollItemsExtension
{
    [TestFixture]
    public class FilterProtocolItemsByTimeSpan
    {
        [Test]
        public void ShouldReturnEmptyListWhenNoItemMatchesFilter()
        {
            var source = new List<ProtocolItem>
            {
                new ProtocolItem {TimeStamp = new DateTime(2000, 1, 1), UncFilePath = "a", LineOfCode = 1},
                new ProtocolItem {TimeStamp = new DateTime(2000, 2, 1), UncFilePath = "b", LineOfCode = 2},
                new ProtocolItem {TimeStamp = new DateTime(2000, 3, 1), UncFilePath = "c", LineOfCode = 3}
            };

            var result = source.FilterByTimeSpan(new DateTime(2000, 4, 1), new DateTime(2000, 5, 1)).ToArray();

            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        [Test]
        public void ShouldReturnFilterProtocolItemsByStartAndEndDate()
        {
            var source = new List<ProtocolItem>
            {
                new ProtocolItem {TimeStamp = new DateTime(2000, 1, 1), UncFilePath = "a", LineOfCode = 1},
                new ProtocolItem {TimeStamp = new DateTime(2000, 2, 1), UncFilePath = "b", LineOfCode = 2},
                new ProtocolItem {TimeStamp = new DateTime(2000, 3, 1), UncFilePath = "c", LineOfCode = 3}
            };

            var result = source.FilterByTimeSpan(new DateTime(2000, 2, 1), new DateTime(2000, 2, 10)).ToArray();

            result.Should().NotBeNull();
            result.Length.Should().Be(1);
            result[0].TimeStamp.Should().Be(new DateTime(2000, 2, 1));
            result[0].UncFilePath.Should().Be("b");
            result[0].LineOfCode.Should().Be(2);
        }
    }
}