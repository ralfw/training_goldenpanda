using System.Collections.Generic;
using System.Linq;

namespace romanConversion
{
    public class FromRomanConverter
    {
        public static int[] Convert(IEnumerable<string> romanNumbers) {
            return romanNumbers.Select(Convert).ToArray();
        }

        static int Convert(string romanNumber) {
            var digits = romanNumber.ToCharArray();
            var values = Map(digits);
            return values.Sum();

            IEnumerable<int> Map(IEnumerable<char> romanDigits) {
                foreach(var d in romanDigits)
                    switch (char.ToUpper(d)) {
                        case 'I': yield return 1;
                            break;
                        case 'V': yield return 5;
                            break;
                        case 'X': yield return 10;
                            break;
                        case 'L': yield return 50;
                            break;
                        case 'C': yield return 100;
                            break;
                        case 'D': yield return 500;
                            break;                                        
                        case 'M': yield return 1000;
                            break;                     
                    }
            }
        }
    }
}