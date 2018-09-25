using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackBoxPredicter
{
    public class DatesProvider
    {
        public static IList<Tuple<DateTime, DateTime>> GetDates()
        {
            return new List<Tuple<DateTime, DateTime>>
            {
                new Tuple<DateTime, DateTime>(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-02")),
                new Tuple<DateTime, DateTime>(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-03")),
                new Tuple<DateTime, DateTime>(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-02")),
                new Tuple<DateTime, DateTime>(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-07")),
                new Tuple<DateTime, DateTime>(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-03")),
                new Tuple<DateTime, DateTime>(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-05")),
                new Tuple<DateTime, DateTime>(DateTime.Parse("2018-01-01"), DateTime.Parse("2018-01-03")),
                new Tuple<DateTime, DateTime>(DateTime.Parse("2018-12-30"), DateTime.Parse("2018-01-02")),
            };
        }
    }
}
