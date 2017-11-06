using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TextSplitter.UnitTest._TextSplitter
{
    [TestFixture]
    public class Concatenate
    {
        [Test]
        public void ShouldReturnConcatenatedStringForLines()
        {
            var lines = new string[] {"Es blaut", "die Nacht"};
            var expected = $"Es blaut\ndie Nacht";

            var result = TextSplitter.Concatenate(lines);

            result.Should().Be(expected);
        }

    }
}