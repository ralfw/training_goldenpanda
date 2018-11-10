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
            var storeRootFolder = EventStoreTestHelper.EnsureEmptyRootFolder("eventStore_0");
            var storageFilePath = Path.Combine(storeRootFolder, "eventnumbers.txt");

            // ReSharper disable once ObjectCreationAsStatement
            new FileNumberStore(storeRootFolder);

            EventStoreTestHelper.AssertFileContent(storageFilePath, "499");
        }

        [Test]
        public void ShouldGetConsecutiveNumbersWithEachCallFromNewStorageFile()
        {
            var storeRootFolder = EventStoreTestHelper.EnsureEmptyRootFolder("eventStore_1");
            var store = new FileNumberStore(storeRootFolder);

            store.NextNumber().Should().Be(500L);
            store.NextNumber().Should().Be(501L);
            store.NextNumber().Should().Be(502L);

            EventStoreTestHelper.AssertFileContent(Path.Combine(storeRootFolder, "eventnumbers.txt"), "502");
        }

        [Test]
        public void ShouldGetConsecutiveNumbersWithEachCallFromExistingStorageFile()
        {
            var storeRootFolder = EventStoreTestHelper.EnsureEmptyRootFolder("eventStore_2");
            var store = new FileNumberStore(storeRootFolder);
            // overwrite existing storage file

            EventStoreTestHelper.CreateTestFile(storeRootFolder, "eventnumbers.txt", 5.ToString());

            store.NextNumber().Should().Be(6L);
            store.NextNumber().Should().Be(7L);
            store.NextNumber().Should().Be(8L);

            EventStoreTestHelper.AssertFileContent(Path.Combine(storeRootFolder, "eventnumbers.txt"), "8");
        }

        [Test]
        public void ShouldThrowExceptionOnFullStore()
        {
            var storeRootFolder = EventStoreTestHelper.EnsureEmptyRootFolder("eventStore_3");
            // create storage file
            EventStoreTestHelper.CreateTestFile(storeRootFolder, "eventnumbers.txt", long.MaxValue.ToString());
            var store = new FileNumberStore(storeRootFolder);

            Action call = () => store.NextNumber();

            call.ShouldThrow<InvalidOperationException>()
                .WithMessage("Store is full.");
        }
    }
}