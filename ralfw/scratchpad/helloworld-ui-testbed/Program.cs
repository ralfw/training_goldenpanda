using System;
using System.Collections.Generic;

namespace helloworld_ui_testbed
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Test Ask():");
            var name = helloworld.Program.Ask();
            Console.WriteLine("  Name: {0}", name);
            
            Console.WriteLine("Test Display():");
            helloworld.Program.Display("Hello, World!");
        }
    }
}