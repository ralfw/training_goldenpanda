using System;
using System.Collections.Generic;

namespace bankocrapp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var bankocr = new BankOCR();

            var accountnumbers = bankocr.decode(args);
            display(accountnumbers);
        }


        static void display(IEnumerable<string> accountnumbers) {
            foreach (var a in accountnumbers) {
                Console.WriteLine(a);
            }
        }
    }
}