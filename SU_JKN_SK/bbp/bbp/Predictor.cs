using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace bbp
{
    public static class Predictor
    {
        public static int CalcDuration(string startDate, string endDate)
        {
            var start = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.CurrentCulture);
            var end = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.CurrentCulture);

            var duration = (end - start).Days + 1;
            return duration;
        }

        public static List<int> CalcDurations(List<Tuple<string, string>> data)
        {
            var result = new List<int>();
            foreach (var tuple in data)
            {
                result.Add(CalcDuration(tuple.Item1, tuple.Item2));
            }

            return result;
        }

        public static List<int> Predict(List<Tuple<string, string>> data)
        {
            var unsortedDurations = CalcDurations(data);
            var sortedDurations = SortDurations(unsortedDurations);
            return sortedDurations;
        }

        public static List<int> SortDurations(List<int> unsortedDurations)
        {
            return unsortedDurations.OrderBy(i => i).ToList();
        }
    }
}