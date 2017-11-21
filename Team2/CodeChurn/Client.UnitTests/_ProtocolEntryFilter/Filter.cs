using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using FluentAssertions;
using NUnit.Framework;

namespace Client.UnitTests._ProtocolEntryFilter
{
    [TestFixture]
    public class Filter
    {
        [SetUp]
        public void Setup()
        {
            _entries = new List<ProtocolEntry>();
            _entries.Add(new ProtocolEntry(new DateTime(2017, 11, 10, 12, 0, 0), 120, "x"));
            _entries.Add(new ProtocolEntry(new DateTime(2017, 11, 10, 12, 0, 0), 120, "y"));
            _entries.Add(new ProtocolEntry(new DateTime(2017, 11, 11, 12, 0, 0), 120, "x"));
            _entries.Add(new ProtocolEntry(new DateTime(2017, 11, 11, 12, 0, 0), 120, "y"));
            _entries.Add(new ProtocolEntry(new DateTime(2017, 11, 12, 12, 0, 0), 120, "x"));
            _entries.Add(new ProtocolEntry(new DateTime(2017, 11, 12, 12, 0, 0), 120, "y"));
        }

        [Test]
        public void ShouldReturnFilteredEntriesForTimeSpan()
        {
            var start = new DateTime(2017, 11, 11);
            var end = new DateTime(2017, 11, 11);

            var expectedEntries = new List<ProtocolEntry>();
            expectedEntries.Add(new ProtocolEntry(new DateTime(2017, 11, 11, 12, 0, 0), 120, "x"));
            expectedEntries.Add(new ProtocolEntry(new DateTime(2017, 11, 11, 12, 0, 0), 120, "y"));


            var filteredEntries = ProtocolEntryFilter.Filter(_entries.ToList(), start, end);


            filteredEntries.Count.Should().Be(2);
            for (int i = 0; i < filteredEntries.Count; i++)
            {
                filteredEntries[i].Timestamp.Should().Be(expectedEntries[i].Timestamp);
            }
        }

        [Test]
        public void ShouldReturnFilteredEntriesForTimeSpan2()
        {
            var start = new DateTime(2017, 10, 11);
            var end = new DateTime(2017, 11, 11);

            var expectedEntries = new List<ProtocolEntry>();
            expectedEntries.Add(new ProtocolEntry(new DateTime(2017, 11, 10, 12, 0, 0), 120, "x"));
            expectedEntries.Add(new ProtocolEntry(new DateTime(2017, 11, 10, 12, 0, 0), 120, "y"));
            expectedEntries.Add(new ProtocolEntry(new DateTime(2017, 11, 11, 12, 0, 0), 120, "x"));
            expectedEntries.Add(new ProtocolEntry(new DateTime(2017, 11, 11, 12, 0, 0), 120, "y"));


            var filteredEntries = ProtocolEntryFilter.Filter(_entries.ToList(), start, end);


            filteredEntries.Count.Should().Be(4);
            for (int i = 0; i < filteredEntries.Count; i++)
            {
                filteredEntries[i].Timestamp.Should().Be(expectedEntries[i].Timestamp);
            }
        }

        private List<ProtocolEntry> _entries;
    }
}