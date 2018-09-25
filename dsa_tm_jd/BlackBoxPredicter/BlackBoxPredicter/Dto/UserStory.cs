using System;

namespace BlackBoxPredicter.Dto
{
    public class UserStory
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public UserStory(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }
}