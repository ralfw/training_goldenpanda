using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace tvspike.es.tests
{
    [TestFixture]
    public class FileNumberStoreTests
    {
        [SetUp]
        public void SetUp()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [Test]
        public void ShouldGetConsecutiveNumbersWithEachCall()
        {
            var storeRootFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "eventStore_0");
            if(Directory.Exists(storeRootFolder))
                Directory.Delete(storeRootFolder, true);
            Directory.CreateDirectory(storeRootFolder);
            var storageFilePath = Path.Combine(storeRootFolder, "eventnumbers.txt");
            File.WriteAllText(storageFilePath, 0.ToString());

            var store = new FileNumberStore(storeRootFolder);

            store.NextNumber().Should().Be(1L);
            store.NextNumber().Should().Be(2L);
            store.NextNumber().Should().Be(3L);

            File.ReadAllText(storageFilePath).Trim().Should().Be("3");
        }

        [Test]
        public void ShouldThrowExceptionOnFullStore()
        {
            var storeRootFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "eventStore_0");
            if (Directory.Exists(storeRootFolder))
                Directory.Delete(storeRootFolder, true);
            Directory.CreateDirectory(storeRootFolder);
            File.WriteAllText(Path.Combine(storeRootFolder, "eventnumbers.txt"), long.MaxValue.ToString());

            var store = new FileNumberStore(storeRootFolder);

            Action call = () => store.NextNumber();

            call.ShouldThrow<InvalidOperationException>()
                .WithMessage("Store is full.");
        }
    }
}