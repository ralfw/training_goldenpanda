using System;
using System.IO;
using NUnit.Framework;

namespace tvspike.sync.tests
{
    [TestFixture]
    public class SyncLogProvider_tests
    {
        private const string TEST_FILENAME = "testsl.txt";
        
        [SetUp]
        public void Setup()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }
        
        
        [Test]
        public void Akzeptanz()
        {
            if (File.Exists(TEST_FILENAME)) File.Delete(TEST_FILENAME);
            
            using (var sut = new SyncLogProvider(TEST_FILENAME))
            {
                Assert.AreEqual("", sut.LetztePullSignatur);
                Assert.AreEqual("", sut.LetztePushSignatur);

                sut.LetztePullSignatur = "pull";
                sut.LetztePushSignatur = "push";                
            }
            
            var sut2 = new SyncLogProvider(TEST_FILENAME);
            Assert.AreEqual("pull", sut2.LetztePullSignatur);
            Assert.AreEqual("push", sut2.LetztePushSignatur);
        }
    }
}