using bankocrapp.data;
using bankocrapp.interior;
using NUnit.Framework;

namespace bankocrapp.tests
{
    [TestFixture]
    public class OcrLine_and_char_tests
    {
        [Test]
        public void Digits() {
            var sut = new OcrLine("    _  _     _  _  _  _  _ ", "  | _| _||_||_ |_   ||_||_|", "  ||_  _|  | _||_|  ||_| _|");
            var result = sut.Chars;
            Assert.AreEqual(9, result.Length);
            Assert.AreEqual("...x..x", result[0].Segments);
            Assert.AreEqual("xxxx.xx", result[8].Segments);
        }
    }
}