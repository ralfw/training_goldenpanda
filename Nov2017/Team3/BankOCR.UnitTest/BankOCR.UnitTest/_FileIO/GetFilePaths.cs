using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace BankOCR.UnitTest._FileIO
{
    [TestFixture]
    public class GetFilePaths
    {
        [Test]
        public void ShouldReturnFilePathsFromSource()
        {
            string[] dirs = Directory.GetFiles(@"./testFiles/");

            dirs.Length.Should().Be(2);
        }
    }
}