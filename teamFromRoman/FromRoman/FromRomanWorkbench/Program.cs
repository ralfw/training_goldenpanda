using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FromRoman;


namespace FromRomanWorkbench
{
    class Program
    {
        static void Main(string[] args)
        {
            var display = new Display();
            Console.WriteLine("Output Test");
            display.ShowStatistic(2,3);
        }
    }
}
