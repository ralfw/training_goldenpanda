using System;
using System.Collections.Generic;

namespace bbp
{
    public static class ConsoleAdapter
    {
        public static void Output(List<int> data)
        {
            foreach (var i in data)
            {
                Console.WriteLine(i);
            }
        }
    }
}