using System.Collections.Generic;

namespace BankOCR
{
    public class SevenSegmentParser
    {
        public List<SevenSegmentAccountNumber> GroupLines(List<string> lines)
        {
            List<SevenSegmentAccountNumber> result = new List<SevenSegmentAccountNumber>();
            List<string> rows = new List<string>();
            foreach (var line in lines)
            {
                rows.Add(line);
                if (rows.Count == 4)
                {
                    result.Add(new SevenSegmentAccountNumber() {Line1 = rows[0], Line2 = rows[1], Line3 = rows[2]});
                    rows.Clear();
                }

            }
            return result;
        }
    }
}