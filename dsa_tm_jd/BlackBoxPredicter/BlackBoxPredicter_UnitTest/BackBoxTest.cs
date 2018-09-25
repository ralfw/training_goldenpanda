﻿// ----------------------------------------------------------------------------------------------------
//  <copyright file="BackBox.cs" company="WAGO Kontakttechnik GmbH & Co. KG.">
//      Copyright (c) WAGO Kontakttechnik GmbH & Co. KG.. All rights reserved.
//  </copyright>
// ----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using BlackBoxPredicter;
using FluentAssertions;
using NUnit.Framework;

namespace BlackBoxPredicter_UnitTest
{
    [TestFixture]
    public class BackBoxTest
    {
        [Test]
        public void ShouldCalculatePercentils()
        {
            IList<int> inputList = new List<int>(){2,2,3,3,3,4,5,7};

            var result = BlackBox.CalculatePercintles(inputList).ToArray();

            result[0]
                .Item1.Should()
                .Be(2);

            result[0]
                .Item2.Should()
                .Be(0.125);


            result[7]
                .Item1.Should()
                .Be(7);

            result[7]
                .Item2.Should()
                .Be(1);
        }

        [Test]
        public void ShouldCalculatedDurations()
        {
      

            IList<Tuple<DateTime, DateTime>> dates = new List<Tuple<DateTime, DateTime>>();

            dates.Add(new Tuple<DateTime, DateTime>(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-02")));
            dates.Add(new Tuple<DateTime, DateTime>(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-03")));

            BlackBox.CalculateCycleTimes(dates).Count.Should().Be(2);
            BlackBox.CalculateCycleTimes(dates)[0]
              .Should()
              .Be(2);

            BlackBox.CalculateCycleTimes(dates)[1]
              .Should()
              .Be(3);


        }

    }
}