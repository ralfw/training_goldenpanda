using System.Collections.Generic;

namespace BankOCR
{
    public class Converter
    {
        public static string[] Convert(string[] lines)
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