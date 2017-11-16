using bankocrapp.data;
using NUnit.Framework;

namespace bankocrapp.tests
{
    [TestFixture]
    public class SevenSAccountNo_tests
    {
        [Test]
        public void Digits() {
            var sut = new OcrLine("    _  _     _  _  _  _  _ ", "  | _| _||_||_ |_   ||_||_|", "  ||_  _|  | _||_|  ||_| _|");
            var result = sut.Chars;
            Assert.AreEqual(9, result.Length);
            Assert.AreEqual('1', result[0].ToDecimalDigit());
            Assert.AreEqual('2', result[1].ToDecimalDigit());
        }
    }
}