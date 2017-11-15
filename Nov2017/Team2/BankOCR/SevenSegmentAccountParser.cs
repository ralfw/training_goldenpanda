using System;
using System.Collections.Generic;

namespace BankOCR
{
    public class SevenSegmentAccountParser
    {
        public static SevenSegmentAccount[] GroupLines(List<string> lines)
        {
            var sevenSegentAccounts = new List<SevenSegmentAccount>();
            var linesHelper = new List<string>();
            foreach (var line in lines)
            {
                if (!String.IsNullOrEmpty(line))
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