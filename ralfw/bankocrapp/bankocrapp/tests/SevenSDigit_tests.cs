using bankocrapp.data;
using NUnit.Framework;

namespace bankocrapp.tests
{
    [TestFixture]
    public class SevenSDigit_tests
    {
        [Test]
        public void ToChar()
        {
            var sut = new OcrChar(' ', ' ', ' ', ' ', ' ', 'I', ' ', ' ', 'I');
            Assert.AreEqual('1', sut.ToDecimalDigit());
        }
    }
}