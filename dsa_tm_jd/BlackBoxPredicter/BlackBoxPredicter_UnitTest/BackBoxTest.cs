// ----------------------------------------------------------------------------------------------------
//  <copyright file="BackBox.cs" company="WAGO Kontakttechnik GmbH & Co. KG.">
//      Copyright (c) WAGO Kontakttechnik GmbH & Co. KG.. All rights reserved.
//  </copyright>
// ----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using BlackBoxPredicter;
using BlackBoxPredicter.Dto;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace BlackBoxPredicter_UnitTest
{
    [TestFixture]
    public class BackBoxTest
    {
        [Test]
        public void ShouldCalculateCycleTimesFromUserStories()
        {
            var dates = new List<UserStory>
            {
                new UserStory(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-01")),
                new UserStory(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-02")),
            };

            var cycleTimes = BlackBox.CalculateCycleTimes(dates);

            cycleTimes.Count.Should().Be(2);
            cycleTimes[0].Should().Be(1);
            cycleTimes[1].Should().Be(2);
        }

        [Test]
        public void ShouldCalculatePercentilesFromOrderedCycleTimes()
        {
            var orderedInput = new List<int> {2, 2, 3, 3, 3, 4, 5, 7};

            var percentiles = BlackBox.CalculatePercentiles(orderedInput).ToArray();

            percentiles.Should().BeEquivalentTo(new List<Tuple<int, double>>
            {
                new Tuple<int, double>(2, 0.125),
                new Tuple<int, double>(2, 0.25),
                new Tuple<int, double>(3, 0.375),
                new Tuple<int, double>(3, 0.5),
                new Tuple<int, double>(3, 0.625),
                new Tuple<int, double>(4, 0.75),
                new Tuple<int, double>(5, 0.875),
                new Tuple<int, double>(7, 1.0),
            });
        }

        [Test]
        public void ShouldFindHighestPercentilePerCycleTime()
        {
            var cycleTimes = new List<Tuple<int, double>>
            {
                new Tuple<int, double>(1, 1),
                new Tuple<int, double>(2, 2),
                new Tuple<int, double>(2, 3),
                new Tuple<int, double>(3, 4),
                new Tuple<int, double>(3, 5),
                new Tuple<int, double>(3, 6),
            };

            var highestPercentiles = BlackBox.FindHighestPercentilesPerCycleTime(cycleTimes).ToList();

            highestPercentiles.Count.Should().Be(3);
            highestPercentiles.Should().BeEquivalentTo(new List<Tuple<int, double>>
            {
                new Tuple<int, double>(1, 1),
                new Tuple<int, double>(2, 3),
                new Tuple<int, double>(3, 6),
            });
        }

        // ---
        // All values up to the 1st entry's percentile will also get the zero index
        // because the cycle time of the first entry is the first data point we have.
        [TestCase(0f,0)]
        [TestCase(24.99f,0)]
        // --
        [TestCase(25f,0)]
        [TestCase(25.01f,0)]
        [TestCase(62.49f,0)]
        [TestCase(62.5f,1)]
        [TestCase(74.99f,1)]
        [TestCase(75f,2)]
        [TestCase(87.49f,2)]
        [TestCase(88.5f,3)]
        [TestCase(99.99f,3)]
        [TestCase(100f,4)]
        public void ShouldDetermineMarkerIndex(float marker, int expectedIndex)
        {
            var entries = new List<HistogramEntry>
            {
                new HistogramEntry(2,2,0.25),
                new HistogramEntry(3,3,0.625),
                new HistogramEntry(4,1,0.75),
                new HistogramEntry(5,1,0.875),
                new HistogramEntry(7,1,1.0),
            };

            var markerIndex = BlackBox.DetermineMarkerIndex(entries, marker);

            markerIndex.Should().Be(expectedIndex);
        }
    }
}