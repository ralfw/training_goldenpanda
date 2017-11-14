using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace BankOCR.UnitTest
{
    [TestFixture]
    public class Convert
    {
        [SetUp]
        public void SetUp()
        {
            var lines = new List<string>
            {
                "    _  _     _  _  _  _  _ ",
                "  | _| _||_||_ |_   ||_||_|",
                "  ||_  _|  | _||_|  ||_| _|",
                "                           ",
                "    _  _  _  _  _  _     _ ",
                "|_||_|| || ||_   |  |  ||_ ",
                "  | _||_||_||_|  |  |  | _|"
            };
        }

        

    }
}
