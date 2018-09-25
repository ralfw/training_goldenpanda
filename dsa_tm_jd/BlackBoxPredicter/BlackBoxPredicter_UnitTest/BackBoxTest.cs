// ----------------------------------------------------------------------------------------------------
//  <copyright file="BackBox.cs" company="WAGO Kontakttechnik GmbH & Co. KG.">
//      Copyright (c) WAGO Kontakttechnik GmbH & Co. KG.. All rights reserved.
//  </copyright>
// ----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using BlackBoxPredicter;
using BlackBoxPredicter.Dto;
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
            IList<UserStory> userStories = new List<UserStory>
            {
                new UserStory(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-02")),
                new UserStory(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-03"))
            };

            var cycleTimes = BlackBox.CalculateCycleTimes(userStories);

            cycleTimes.Count.Should().Be(2);
            cycleTimes[0]
              .Should()
              .Be(2);

            cycleTimes[1]
              .Should()
              .Be(3);
        }

    }
}