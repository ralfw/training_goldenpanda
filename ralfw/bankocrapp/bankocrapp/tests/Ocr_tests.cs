using bankocrapp.data;
using bankocrapp.interior;
using NUnit.Framework;

namespace bankocrapp.tests
{
    [TestFixture]
    public class Ocr_tests
    {
        [Test]
        public void Recognize()
        {
            var sut = new OcrChar(' ', ' ', ' ', ' ', ' ', 'I', ' ', ' ', 'I');
            Assert.AreEqual('1', Ocr.Recognize(sut));
        }
    }
}