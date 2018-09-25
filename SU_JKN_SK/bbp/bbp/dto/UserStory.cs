using System;
using System.Globalization;

namespace bbp.dto
{
    public class UserStory
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public int Duration => (EndDate - StartDate).Days + 1;

        public UserStory(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public UserStory(string startDate, string endDate) 
            : this(DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.CurrentCulture),
                   DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.CurrentCulture))
        {
        }
    }
}