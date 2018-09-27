using System;
using System.Collections.Generic;
using BlackBoxPredicter.Dto;

namespace BlackBoxPredicter
{
    public class DatesProvider
    {
        public static IEnumerable<UserStory> GetDates()
        {
            return new List<UserStory>
            {
                new UserStory(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-02")),
                new UserStory(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-03")),
                new UserStory(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-02")),
                new UserStory(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-07")),
                new UserStory(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-03")),
                new UserStory(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-05")),
                new UserStory(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-03")),
                new UserStory(DateTime.Parse("2017-12-30"), DateTime.Parse("2018-01-02")),
            };
        }
    }
}
