using FluentAssertions;
using NUnit.Framework;

namespace TextSplitter.UnitTest._TextSplitter
{
    [TestFixture]
    public class WrapWords
    {
        [TestCase(9,"Es blaut die Nacht,\ndie Sternlein blinken,\nSchneeflöcklein leis hernieder sinken.", "Es blaut\ndie\nNacht,\ndie\nSternlein\nblinken,\nSchneeflö\ncklein\nleis\nhernieder\nsinken.")]
        [TestCase(14,"Es blaut die Nacht,\ndie Sternlein blinken,\nSchneeflöcklein leis hernieder sinken.", "Es blaut die\nNacht, die\nSternlein\nblinken,\nSchneeflöcklei\nn leis\nhernieder\nsinken.")]
        public void ShouldWrapWords(int lineLength, string input, string expected)
        {

            var result = TextSplitter.WrapWords(lineLength,input);

            result.Should().Be(expected);
        }
    }
}