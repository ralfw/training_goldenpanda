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
            var filePaths = new FileIO().GetFilePaths(@"./testFiles/");
            
            filePaths.Count.Should().Be(2);
        }
    }
}