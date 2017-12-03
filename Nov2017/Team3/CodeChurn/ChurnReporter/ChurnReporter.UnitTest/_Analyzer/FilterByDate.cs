using System;
using System.IO;
using NUnit.Framework;

namespace ChurnReporter.UnitTest._Analyzer
{
    [TestFixture]
    public class FilterByDate
    {
        [Test]
        public void ShouldFilterEntriesByDate()
        {
            var startTime = "03.12.2017 17:58:09";
            var endTime = "03.12.2017 17:19:12";
            var protocolFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"./testFiles/protocol.csv");
        }
    }
}