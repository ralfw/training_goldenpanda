using System.Collections.Generic;
using bbp.dto;

namespace bbp
{
    public static class DataProvider 
    {
        //Note: Test as soon as real data is used
        public static IEnumerable<UserStory> GetUserStories()
        {
            return new List<UserStory>
            {
                new UserStory("2018-01-01", "2018-01-02"),
                new UserStory("2018-01-01", "2018-01-03"),
                new UserStory("2018-01-01", "2018-01-02"),
                new UserStory("2018-01-01", "2018-01-07"),
                new UserStory("2018-01-01", "2018-01-03"),
                new UserStory("2018-01-01", "2018-01-05"),
                new UserStory("2018-01-01", "2018-01-03"),
                new UserStory("2017-12-30", "2018-01-02")
            };
        }
    }
}