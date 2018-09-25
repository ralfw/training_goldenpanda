using System;
using System.Collections.Generic;

namespace BlackBoxPredicter
{
    public class DisplayAdapter
    {
        public static void Display(IEnumerable<Tuple<int, double>> result)
        {
            Console.Out.WriteLine("Cycle times");
            Console.Out.WriteLine("-----------");
            foreach (var resultItem in result)
            {
                Console.Out.WriteLine($"{resultItem.Item1};{resultItem.Item2}");
            }

            Console.Out.WriteLine("");
        }
    }
}