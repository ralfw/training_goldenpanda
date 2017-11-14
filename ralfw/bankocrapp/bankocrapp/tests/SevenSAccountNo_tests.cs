using System.Text;
using NUnit.Framework;

namespace bankocrapp
{
    [TestFixture]
    public class SevenSAccountNo_tests
    {
        [Test]
        public void Digits()
        {
            var sut = new SevenSAccountNo("    _  _     _  _  _  _  _ ", "  | _| _||_||_ |_   ||_||_|", "  ||_  _|  | _||_|  ||_| _|");
            var result = sut.Digits;
            Assert.AreEqual(9, result.Length);
            Assert.AreEqual('1', result[0].ToDecimalDigit());
            Assert.AreEqual('2', result[1].ToDecimalDigit());
        }
    }
}