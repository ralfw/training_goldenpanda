using FluentAssertions;
using NUnit.Framework;

namespace TextSplitter.UnitTest._TextSplitter
{
    [TestFixture]
    public class GenerateLines
    {
        [Test]
        public void ShouldGenereateNewLines()
        {
            var textSpliter = new TextSplitter();

            var result = TextSplitter.GenerateLines(9, new[] {"Es", "blaut", "die", "Nacht"});

            result.Should().ContainInOrder(new[]{"Es blaut","die Nacht"});
        }
    }
}
