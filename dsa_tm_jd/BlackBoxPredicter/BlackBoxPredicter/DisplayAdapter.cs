using System;
using System.Collections;
using System.Collections.Generic;

namespace BlackBoxPredicter
{
    public class DisplayAdapter
    {
        public static void Display(IList<int> cycleTimes)
        {
            Console.Out.WriteLine("Cycle times");
            Console.Out.WriteLine("-----------");
            foreach (var cycleTime in cycleTimes)
            {
                Console.Out.WriteLine(cycleTime);
            }

            Console.Out.WriteLine("");
        }
    }
}