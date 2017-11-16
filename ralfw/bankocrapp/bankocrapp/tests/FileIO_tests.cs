using bankocrapp.adapters;
using NUnit.Framework;

namespace bankocrapp.tests
{
    [TestFixture]
    public class FileIO_tests
    {
        [Test]
        public void get_all_lines()
        {
            var sut = new FileIO();
            
            var lines = sut.get_source_lines(new[] {"tests/text1.txt", "tests/subdir"});
            
            Assert.AreEqual(new[]{"1","2","a","b","c"}, lines);
        }
    }
}