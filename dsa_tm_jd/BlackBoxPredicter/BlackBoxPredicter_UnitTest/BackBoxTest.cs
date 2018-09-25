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

namespace BlackBoxPredicter_UnitTest
{
    [TestFixture]
    public class BackBoxTest
    {

        [Test]
        public void ShouldGetFighestPercentiles()
        {
            IList<Tuple<int, double> > input = new List<Tuple<int, double>>()
                                                   {
                                                       new Tuple<int, double>(1,2),
                                                       new Tuple<int, double>(1,3),
                                                       new Tuple<int, double>(3,4),
                                                       new Tuple<int, double>(3,5),
                                                   };

            var filtered = BlackBox.FindHighestPercentils(input);

            filtered.Count()
                    .Should()
                    .Be(2);
        }

        [Test]
        public void ShouldCalculatePercentiles()
        {
            IList<int> inputList = new List<int>(){2,2,3,3,3,4,5,7};

            var r = inputList.GroupBy(o => o);
            

            var result = BlackBox.CalculatePercentiles(inputList).ToArray();

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


            List<UserStory> dates = new List<UserStory>();

            dates.Add(new UserStory(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-02")));
            dates.Add(new UserStory(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-03")));
  
        
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