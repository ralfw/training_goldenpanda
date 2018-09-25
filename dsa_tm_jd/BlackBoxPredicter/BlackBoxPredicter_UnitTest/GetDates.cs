﻿using System;
using FluentAssertions;
using NUnit.Framework;

namespace BlackBoxPredicter_UnitTest
{
    [TestFixture]
    public class DatesProvider
    {
        [Test]
        public void GetDates()
        {
            var result = BlackBoxPredicter.DatesProvider.GetDates();

            result.Count.Should().Be(8);
            result[0].ShouldBeEquivalentTo(new Tuple<DateTime,DateTime>(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-02")));
            result[1].ShouldBeEquivalentTo(new Tuple<DateTime,DateTime>(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-03")));
            result[2].ShouldBeEquivalentTo(new Tuple<DateTime,DateTime>(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-02")));
            result[3].ShouldBeEquivalentTo(new Tuple<DateTime,DateTime>(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-07")));
            result[4].ShouldBeEquivalentTo(new Tuple<DateTime,DateTime>(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-03")));
            result[5].ShouldBeEquivalentTo(new Tuple<DateTime,DateTime>(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-05")));
            result[6].ShouldBeEquivalentTo(new Tuple<DateTime,DateTime>(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-03")));
            result[7].ShouldBeEquivalentTo(new Tuple<DateTime,DateTime>(DateTime.Parse("2018-12-30"), DateTime.Parse("2018-01-02")));
        }

        
    }
}
