using System;
using System.IO;

namespace tvspike.es
{
    public class FileNumberStore
    {
        private readonly string _storageFilePath;

        public FileNumberStore(string workingFolder)
        {
            _storageFilePath = Path.Combine(workingFolder, "eventnumbers.txt");
        }

        public long NextNumber()
        {
            var lastNumber = ReadLastNumber();
            var nextNumber = GenerateNextNumber();
            WriteNextNumber();
            return nextNumber;

            long GenerateNextNumber() => lastNumber + 1;
            void WriteNextNumber() => File.WriteAllText(_storageFilePath, nextNumber.ToString());
        }

        private long ReadLastNumber()
        {
            var readAllText = File.ReadAllText(_storageFilePath);

            var lastNumber = long.Parse(readAllText.Trim());
            if (lastNumber == long.MaxValue)
                throw new InvalidOperationException("Store is full.");

            return lastNumber;
        }
    }
}