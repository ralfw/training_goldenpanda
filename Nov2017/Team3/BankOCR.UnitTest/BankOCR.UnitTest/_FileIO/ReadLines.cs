using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace BankOCR.UnitTest._FileIO
{
    [TestFixture]
    public class ReadLines
    {
        [Test]
        public void ShouldReturnLines()
        {
            string[] filePaths = Directory.GetFiles(@"./testFiles/");
            var sut = new FileIO();

            var result = sut.ReadLines(filePaths.ToList());

            result.Should().HaveCount(24);
        }
    }
}