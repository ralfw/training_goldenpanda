using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOCR
{
    static class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Converter
    {
        public string[] Convert(string[] lines)
        {
            return new[] {""};
        }
    }

    public class SevenSegmentDigit
    {
        public string Segments { get; private set; }

        public SevenSegmentDigit(string segments)
        {
            Segments = segments;
        }

        public char Map()
        {
            return '1';
        }
    }
}
