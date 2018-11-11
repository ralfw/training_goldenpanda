using System;
using System.IO;

namespace tvspike.es
{
    public class FileClientIdStore
    {
        public string ClientId { get; private set; }

        public FileClientIdStore(string storeRootFolder)
        {
            BuildStorageFilePath(storeRootFolder);
            EnsureClientId();
        }

        private void BuildStorageFilePath(string storeRootFolder)
        {
            _storageFilePath = Path.Combine(storeRootFolder, "clientId.txt");
        }

        private void EnsureClientId()
        {
            if (File.Exists(_storageFilePath))
            {
                LoadClientId();
            }
            else
            {
                GenerateNewClientId();
                StoreClientId();
            }
        }

        private void LoadClientId()
        {
            var content = File.ReadAllText(_storageFilePath).Trim();
            ClientId = Guid.Parse(content).ToString().ToLower();
        }

        private void GenerateNewClientId()
        {
            ClientId = Guid.NewGuid().ToString().ToLower();
        }

        private void StoreClientId()
        {
            File.WriteAllText(_storageFilePath, ClientId);
        }

        private string _storageFilePath;
    }
}