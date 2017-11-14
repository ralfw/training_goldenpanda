﻿using System;
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

        public static SevenSegmentAccount[] GroupLines(List<string> lines)
        {
            var sevenSegentAccounts = new List<SevenSegmentAccount>();
            var linesHelper = new List<string>();
            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                  linesHelper.Add(line);
                if (linesHelper.Count == 3)
                {
                    sevenSegentAccounts.Add(new SevenSegmentAccount(linesHelper.ToArray()));
                    linesHelper.Clear();
                }
            }

            return sevenSegentAccounts.ToArray();
        }
    }
}
