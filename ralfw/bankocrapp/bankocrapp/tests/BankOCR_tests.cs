using NUnit.Framework;

namespace bankocrapp.tests
{
    [TestFixture]
    public class BankOCR_tests
    {
        [Test]
        public void Decode()
        {
            var sut = new BankOCR();

            var result = sut.decode(new[] {"tests/ocr1.txt"});
            
            Assert.AreEqual(new[]{"123456789","490067715"}, result);
        }
    }
}