using System;
using System.Collections.Generic;
using CodeChurnReport.Behavior.Core;
using CodeChurnReport.Data;
using FluentAssertions;
using NUnit.Framework;

namespace CodeChurnReport.UnitTest._ProtocollItemsExtension
{
    [TestFixture]
    public class GetDistinctFilePaths
    {
        [Test]
        public void ShouldReturnEmptyListForNoItems()
        {
            var emptyItems = new ProtocolItem[] { };
            var result = emptyItems.GetDistinctFilePaths();
            result.Should().BeEmpty();
        }

        [Test]
        public void ShouldReturnUniqueFileNamesFromProtocolItems()
        {
            var items = new List<ProtocolItem>
            {
                new ProtocolItem {TimeStamp = new DateTime(2000, 1, 1), UncFilePath = "a", LineOfCode = 1},
                new ProtocolItem {TimeStamp = new DateTime(2000, 1, 2), UncFilePath = "b", LineOfCode = 2},
                new ProtocolItem {TimeStamp = new DateTime(2000, 1, 3), UncFilePath = "a", LineOfCode = 3},
                new ProtocolItem {TimeStamp = new DateTime(2000, 1, 4), UncFilePath = "c", LineOfCode = 4}
            };

            var result = items.GetDistinctFilePaths();

            result.Should().ContainInOrder("a", "b", "c");
        }
    }
}