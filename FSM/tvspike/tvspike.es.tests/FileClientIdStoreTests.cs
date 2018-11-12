using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace tvspike.es.tests
{
    [TestFixture]
    public class FileClientIdStoreTests
    {
        [SetUp]
        public void SetUp()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }

        [Test]
        public void ShouldCreateInitialStorageFileIfNotExists()
        {
            var storeRootFolder = EventStoreTestHelper.EnsureDeletedRootFolder("clientIdStore_0");
            var storageFilePath = Path.Combine(storeRootFolder, "clientId.txt");

            // ReSharper disable once ObjectCreationAsStatement
            var clientIdStore = new FileClientIdStore(storeRootFolder);
            var clientId = clientIdStore.ClientId;

            Guid.TryParse(clientId, out var generatedClientIdGuid).Should().BeTrue();
            generatedClientIdGuid.Should().NotBe(Guid.Empty);

            EventStoreTestHelper.AssertFileContent(storageFilePath, 
                s => Guid.TryParse(s, out var guidFromFile) && guidFromFile == generatedClientIdGuid);
        }

        [Test]
        public void ShouldGetClientIdFromExistingFile()
        {
            var storeRootFolder = EventStoreTestHelper.EnsureEmptyRootFolder("clientIdStore_1");
            var expectedClientId = Guid.NewGuid().ToString().ToLower();
            EventStoreTestHelper.CreateTestFile(storeRootFolder, "clientId.txt", expectedClientId);

            var store = new FileClientIdStore(storeRootFolder);

            store.ClientId.Should().Be(expectedClientId);
        }
    }
}