using NUnit.Framework;

namespace bankocrapp
{
    [TestFixture]
    public class Converter_tests
    {
        [Test]
        public void Convert()
        {
            var result = Converter.convert(new[] {
                "    _  _     _  _  _  _  _ ",
                "  | _| _||_||_ |_   ||_||_|",
                "  ||_  _|  | _||_|  ||_| _|",
                "",
                "    _  _  _  _  _  _     _ ",
                "|_||_|| || ||_   |  |  ||_ ",
                "  | _||_||_||_|  |  |  | _|",
            });
            Assert.AreEqual(new[]{"123456789","490067715"}, result);
        }
        
        
        [Test]
        public void Convert_with_invalid_chars()
        {
            var result = Converter.convert(new[] {
                "    _  _     _  _  _  _  _ ",
                "  | _| _||_||_ |_   ||_||_|",
                "  ||_  _|  | _||_O  ||_| _|",
                "",
                "    _  _  _  _  _  _     _ ",
                "|_||_|| || ||_   |  |  ||_ ",
                "  | _||_||_||_|  |  |  | _|",
            });
            Assert.AreEqual(new[]{"Fehlerhafte Kontonummer!","490067715"}, result);
        }
    }
}