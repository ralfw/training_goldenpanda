using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace tvspike.sync.tests
{
    [TestFixture]
    public class EventStoreProvider_tests
    {
        private const string TEST_PATH = "testevents";
        
        [SetUp]
        public void Setup() {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }


        [Test]
        public void Eventeigenschaften()
        {
            var sut = new Event {Dateiname = "0_cli_id1_eva.txt"};
            Assert.AreEqual("cli", sut.ClientId);
            Assert.AreEqual("0_cli_id1_eva", sut.Signatur);
        }
        
        
        [Test]
        public void Events_exportieren_ab_Anfang()
        {
            var sut = new EventStoreProvider("test_source_eventstore");
            
            var result = sut.Export("", "cli2");
            
            var eventPayloads = result.Select(e => e.Daten).ToArray();
            Assert.AreEqual(new[]{"ev2", "ev4"}, eventPayloads);
        }
    }
}