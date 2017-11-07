using FluentAssertions;
using NUnit.Framework;

namespace TextSplitter.UnitTest._TextSplitter
{
    [TestFixture]
    public class Split
    {
        [TestCase("Es blaut die Nacht, gagelpu", new[] {"Es", "blaut", "die", "Nacht,", "gagelpu"})]
        [TestCase("Es\tblaut\ndie\rNacht, gagelpu", new[] { "Es", "blaut", "die", "Nacht,", "gagelpu" })]
        [TestCase("A  B", new[] { "A", "B" })]
        public void ShouldSplitTextIntoSingleWords(string text, string[] expectedWords)
        {
            var textSplitter = new TextSplitter();

            var result = TextSplitter.Split_into_words(text);

            result.Should().ContainInOrder(expectedWords);
        }
    }
}
