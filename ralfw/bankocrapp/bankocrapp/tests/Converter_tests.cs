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
            Assert.AreEqual(new[]{"12???????","???????1?"}, result);
        }
    }
}