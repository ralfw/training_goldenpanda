using System;
using System.Collections.Generic;

namespace BankOCR
{
    public class SevenSegmentAccountParser
    {
        public static IEnumerable<SevenSegmentAccount> GroupLines(string[] lines)
        {
            for (int i = 0; i < lines.Length; i+=4)
            {
                yield return (new SevenSegmentAccount(new []
                {
                    lines[i],lines[i+1],lines[i+2]
                }));
            }
        }
    }
}