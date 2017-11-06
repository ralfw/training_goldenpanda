using FluentAssertions;
using NUnit.Framework;

namespace TextSplitter.UnitTest._TextSplitter
{
    [TestFixture]
    public class Split
    {
        [TestCase("Es blaut die Nacht, gagelpu", new[] {"Es", "blaut", "die", "Nacht,", "gagelpu"})]
        [TestCase("Es\tblaut\ndie\rNacht, gagelpu", new[] { "Es", "blaut", "die", "Nacht,", "gagelpu" })]
        public void ShouldSplitTextIntoSingleWords(string text, string[] expectedWords)
        {
            var textSplitter = new TextSplitter();

            var result = TextSplitter.Split(text);

            result.Should().ContainInOrder(expectedWords);
        }
    }
}
