// ----------------------------------------------------------------------------------------------------
//  <copyright file="BackBox.cs" company="WAGO Kontakttechnik GmbH & Co. KG.">
//      Copyright (c) WAGO Kontakttechnik GmbH & Co. KG.. All rights reserved.
//  </copyright>
// ----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using BlackBoxPredicter;
using FluentAssertions;
using NUnit.Framework;

namespace BlackBoxPredicter_UnitTest
{
    [TestFixture]
    public class BackBoxTest
    {
        [Test]
        public void ShouldCalculatedDurations()
        {
            BlackBox bl = new BlackBox();

            IList<Tuple<DateTime, DateTime>> dates = new List<Tuple<DateTime, DateTime>>();

            dates.Add(new Tuple<DateTime, DateTime>(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-02")));
            dates.Add(new Tuple<DateTime, DateTime>(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-03")));

            bl.CalculateCycles(dates).Count.Should().Be(2);
            bl.CalculateCycles(dates)[0]
              .Should()
              .Be(2);

            bl.CalculateCycles(dates)[1]
              .Should()
              .Be(3);


        }

    }
}