using System;
using System.Globalization;

namespace bbp.dto
{
    internal class UserStory
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public UserStory(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public UserStory(string startDate, string endDate) : this(DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.CurrentCulture),
            DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.CurrentCulture))
        {
        }
    }
}