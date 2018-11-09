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
        public void ShouldCreateInitialStorageFileIfNotExists()
        {
            var storeRootFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "eventStore_0");
            if (Directory.Exists(storeRootFolder))
                Directory.Delete(storeRootFolder, true);
            Directory.CreateDirectory(storeRootFolder);

            var storageFilePath = Path.Combine(storeRootFolder, "eventnumbers.txt");

            // ReSharper disable once ObjectCreationAsStatement
            new FileNumberStore(storeRootFolder);

            File.ReadAllText(storageFilePath).Trim().Should().Be("499");
        }

        [Test]
        public void ShouldGetConsecutiveNumbersWithEachCallFromNewStorageFile()
        {
            var storeRootFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "eventStore_1");
            if(Directory.Exists(storeRootFolder))
                Directory.Delete(storeRootFolder, true);
            Directory.CreateDirectory(storeRootFolder);
            var store = new FileNumberStore(storeRootFolder);

            store.NextNumber().Should().Be(500L);
            store.NextNumber().Should().Be(501L);
            store.NextNumber().Should().Be(502L);

            File.ReadAllText(Path.Combine(storeRootFolder, "eventnumbers.txt")).Trim()
                .Should().Be("502");
        }

        [Test]
        public void ShouldGetConsecutiveNumbersWithEachCallFromExistingStorageFile()
        {
            var storeRootFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "eventStore_1");
            if (Directory.Exists(storeRootFolder))
                Directory.Delete(storeRootFolder, true);
            Directory.CreateDirectory(storeRootFolder);
            var store = new FileNumberStore(storeRootFolder);
            // overwrite existing storage file
            File.WriteAllText(Path.Combine(storeRootFolder, "eventnumbers.txt"), 5.ToString());

            store.NextNumber().Should().Be(6L);
            store.NextNumber().Should().Be(7L);
            store.NextNumber().Should().Be(8L);

            File.ReadAllText(Path.Combine(storeRootFolder, "eventnumbers.txt")).Trim()
                .Should().Be("8");
        }

        [Test]
        public void ShouldThrowExceptionOnFullStore()
        {
            var storeRootFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "eventStore_3");
            if (Directory.Exists(storeRootFolder))
                Directory.Delete(storeRootFolder, true);
            Directory.CreateDirectory(storeRootFolder);
            // create storage file
            File.WriteAllText(Path.Combine(storeRootFolder, "eventnumbers.txt"), long.MaxValue.ToString());
            var store = new FileNumberStore(storeRootFolder);

            Action call = () => store.NextNumber();

            call.ShouldThrow<InvalidOperationException>()
                .WithMessage("Store is full.");
        }
    }
}