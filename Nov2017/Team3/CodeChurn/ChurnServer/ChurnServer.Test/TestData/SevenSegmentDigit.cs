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
        {" _  _II_ ", '2'},
        {" _  _I _I", '3'},
        {"   I_I  I", '4'},
        {" _ I_  _I", '5'},
        {" _ I_ I_I", '6'},
        {" _   I  I", '7'},
        {" _ I_II_I", '8'},
        {" _ I_I _I", '9'},
        {" _ I II_I", '0'}};


        public SevenSegmentDigit(string segments)
        {
            SevenSegments = segments;
        }
        public string SevenSegments { get; set; }

        public char Map()
        {
            char symbol;
            SevSegDigits.TryGetValue(SevenSegments, out symbol);

            return symbol;
        }
    }
}
