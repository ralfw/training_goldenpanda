using NUnit.Framework;
using FluentAssertions;

namespace FileIo.IntegrationTests
{
    [TestFixture]
    public class ToUncPath
    {
        [Test]
        public void ShouldReturnSameValueForNonUncPath()
        {
            ChurnServer.Adapter.FileIo fileIo = new ChurnServer.Adapter.FileIo();

            var uncPath = fileIo.ToUncPath(@"c:\anyDir\anyFile.ext");

            uncPath.Should().Be(@"c:\anyDir\anyFile.ext");
        }
    }
}