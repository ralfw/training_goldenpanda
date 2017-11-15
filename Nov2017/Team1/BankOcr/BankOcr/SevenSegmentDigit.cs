using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BankOcr
{
    public class SevenSegmentDigit

    {
        private static IReadOnlyDictionary<string, int> MapTable = new ReadOnlyDictionary<string, int>
        (
            new Dictionary<string, int>
            {
                {
                    " _ " +
                    "| |" +
                    "|_|",
                    0
                },   {
                    "   " +
                    "  |" +
                    "  |",
                    1
                },
                {
                    " _ " +
                    " _|" +
                    "|_ ",
                    2
                },
                {
                    " _ " +
                    " _|" +
                    " _|",
                    3
                },
                {
                    "   " +
                    "|_|" +
                    "  |",
                    4
                },
                {
                    " _ " +
                    "|_ " +
                    " _|",
                    5
                },
                {
                    " _ " +
                    "|_ " +
                    "|_|",
                    6
                },
                {
                    " _ " +
                    "  |" +
                    "  |",
                    7
                },
                {
                    " _ " +
                    "|_|" +
                    "|_|",
                   8
                },
                {
                    " _ " +
                    "|_|" +
                    " _|",
                    9
                }
            });

        public SevenSegmentDigit(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public int Map()
        {
            return MapTable[Value];
        }
    }
}