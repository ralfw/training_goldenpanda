using NUnit.Framework;

namespace bankocrapp
{
    [TestFixture]
    public class SevenSDigit_tests
    {
        [Test]
        public void ToChar()
        {
            var sut = new SevenSDigit(' ', ' ', ' ', ' ', ' ', 'I', ' ', ' ', 'I');
            Assert.AreEqual('1', sut.ToDecimalDigit());
        }
    }
}