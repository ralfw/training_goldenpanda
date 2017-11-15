using System.Collections.Generic;

namespace BankOCR
{
    public class SevenSegmentParser
    {
        public List<SevenSegmentAccountNumber> GroupLines(List<string> lines)
        {
            List<SevenSegmentAccountNumber> sevenSegmentAccountNumbers = new List<SevenSegmentAccountNumber>();
            List<string> lineGroup = new List<string>();
            foreach (var line in lines)
            {
                lineGroup.Add(line);
                if (lineGroup.Count == 4)
                {
                    sevenSegmentAccountNumbers.Add(new SevenSegmentAccountNumber() {Line1 = lineGroup[0], Line2 = lineGroup[1], Line3 = lineGroup[2]});
                    lineGroup.Clear();
                }

            }
            return sevenSegmentAccountNumbers;
        }
    }
}