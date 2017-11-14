using System;
using System.Collections.Generic;

namespace BankOcr
{
    public static class Converter
    {
        public static IEnumerable<string> Convert(string[] content)
        {
            var groupedLines = SevenSegmentAccountParser.GroupLines(content);

            return ConvertSevenSegmentAccounts(groupedLines);
        }

        public static IEnumerable<string> ConvertSevenSegmentAccounts(SevenSegmentAccount[] groupedLines)
        {
            throw new NotImplementedException();
        }
    }
}
