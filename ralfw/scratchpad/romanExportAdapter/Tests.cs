using System.IO;
using System.Linq;
using NUnit.Framework;

namespace romanExportAdapter
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Acceptance()
        {
            if (Directory.Exists("output_test")) Directory.Delete("output_test");
            var sut = new romanConversion.adapters.ExportAdapter();
            
            sut.Export(new[]{"1", "2"}, "output_test");

            var outputFilenames = Directory.GetFiles("output_test");
            Assert.AreEqual(1, outputFilenames.Length);
            Assert.AreEqual(new[]{"1", "2"}, File.ReadAllLines(outputFilenames.First()));
        }
    }
}
