using System.Collections.Generic;

namespace BankOCR
{
    public class SevenSegmentDigit
    {
        public string Segments { get; private set; }

        public SevenSegmentDigit(string segments)
        {
            Segments = segments;
        }

        public char Map()
        {
            return mapping[Segments];
        }

        private Dictionary<string,char> mapping = new Dictionary<string, char>
        {
            { "   " +
            "  |" +
            "  |",'1'},
            { " _ " +
              " _|" +
              "|_ ",'2'},
            { " _ " +
              " _|" +
              " _|",'3'},
            { "   " +
              "|_|" +
              "  |",'4'},
            { " _ " +
              "|_ " +
              " _|",'5'},
            { " _ " +
              "|_ " +
              "|_|",'6'},
            { " _ " +
              "  |" +
              "  |",'7'},
            { " _ " +
              "|_|" +
              "|_|",'8'},
            { " _ " +
              "|_|" +
              " _|",'9'},
            { " _ " +
              "| |" +
              "|_|",'0'},
        };
    }
}