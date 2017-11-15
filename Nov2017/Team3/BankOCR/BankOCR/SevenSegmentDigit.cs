using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BankOCR
{
    public class SevenSegmentDigit
    {
        Dictionary<string, char> SevSegDigits = new Dictionary<string, char> {
        {"     I  I", '1'},
        {" I  IIII ", '2'},
        {" I  II II", '3'},
        {"   III  I", '4'},
        {" I II  II", '5'},
        {" I II III", '6'},
        {" I   I  I", '7'},
        {" I IIIIII", '8'},
        {" I III II", '9'},
        {" I I IIII", '0'}};


        public SevenSegmentDigit(string segments)
        {
            SevenSegments = segments;
        }
        public string SevenSegments { get; set; }

        public char Map(string segmentString)
        {
            char symbol;
            SevSegDigits.TryGetValue(segmentString, out symbol);

            return symbol;
        }
    }
}
