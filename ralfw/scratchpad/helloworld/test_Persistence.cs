using System;
using System.IO;
using NUnit.Framework;

namespace helloworld
{
    [TestFixture]
    public class test_Persistence
    {
        private const string VISITORS_FILENAME = "visitors.txt";
        
        [SetUp]
        public void Setup() {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }
        
        [Test]
        public void LoadEmpty() {
            if (File.Exists(VISITORS_FILENAME)) File.Delete(VISITORS_FILENAME);
            
            var result = Program.Load();
            
            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void StoreLoad() {
            if (File.Exists(VISITORS_FILENAME)) File.Delete(VISITORS_FILENAME);
            
            Program.Store(new[]{"a","b"});
            var result = Program.Load();
            
            Assert.AreEqual(new[]{"a","b"}, result);
        }
    }
}