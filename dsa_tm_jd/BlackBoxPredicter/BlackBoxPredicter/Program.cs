using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackBoxPredicter
{
   
    class Program
    {
        static void Main(string[] args)
        {
            var dates = DatesProvider.GetDates();
            var cycles = BlackBox.CalculateCycleTimes(dates);
            DisplayAdapter.Display(cycles);
        }

        private static void TestDisplay()
        {
            var cycleTimes = new List<int>{1,2,3,4};
            DisplayAdapter.Display(cycleTimes);
        }
    }
}
