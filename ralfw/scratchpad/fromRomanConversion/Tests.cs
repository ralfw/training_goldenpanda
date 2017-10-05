using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace fromRomanConversion
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            Assert.True(true);
        }
    }
}


namespace romanConversion
{
    public class FromRomanConverter
    {
        public static int[] Convert(IEnumerable<string> romanNumbers) {
            return romanNumbers.Select(r => r.Length).ToArray();
        }
    }
}