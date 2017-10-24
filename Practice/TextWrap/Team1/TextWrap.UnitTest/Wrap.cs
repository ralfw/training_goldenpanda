using System;
using FluentAssertions;
using NUnit.Framework;

namespace TextWrap.UnitTest
{
    [TestFixture]
    public class Wrap
    {
        [Test]
        public void ShouldCreateTextWithOneWord()
        {
            var text = "test";

            var result = TextWrapper.Wrap(text, 5);

            result.Should().Be("test");
        }

        [Test]
        public void ShouldWrapText()
        {
            var inputText = "Es blaut die Nacht," + Environment.NewLine
                + "die Sternlein blinken," + Environment.NewLine
                + "Schneeflöcklein leis hernieder sinken.";

            var expectedText = "Es blaut" + Environment.NewLine
                                 + "die" + Environment.NewLine
                                 + "Nacht," + Environment.NewLine
                                 + "die" + Environment.NewLine
                                 + "Sternlein" + Environment.NewLine
                                 + "blinken," + Environment.NewLine
                                 + "Schneeflö" + Environment.NewLine
                                 + "cklein" + Environment.NewLine
                                 + "leis" + Environment.NewLine
                                 + "hernieder" + Environment.NewLine
                                 + "sinken.";

            var outputText = TextWrapper.Wrap(inputText, 9);

            outputText.Should().Be(expectedText);
        }

    }
}