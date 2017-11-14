using System.Collections.Generic;

namespace BankOcr
{
    public static class SevenSegmentAccountParser
    {
        public static SevenSegmentAccount[] GroupLines(string[] lines)
        {
            var accounts = new List<SevenSegmentAccount>();
            var tempLines = new[] {"", "", ""};

            var counter = 1;
            foreach (var line in lines)
            {
                switch (counter)
                {
                    case 1:
                        tempLines[0] = line;
                        break;
                    case 2:
                        tempLines[1] = line;
                        break;
                    case 3:
                        tempLines[2] = line;
                        accounts.Add(new SevenSegmentAccount(tempLines));
                        break;
                    case 4:
                        counter = 0;
                        break;
                }
                counter++;
            }

            return accounts.ToArray();
        }
    }
}