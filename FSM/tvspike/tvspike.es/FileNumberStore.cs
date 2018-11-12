﻿using System;
using System.IO;

namespace tvspike.es
{
    public class FileNumberStore
    {
        private readonly string _storageFilePath;

        public FileNumberStore(string storeRootFolderPath) : this(storeRootFolderPath, 1L)
        {
            
        }

        internal FileNumberStore(string storeRootFolderPath, long initialEventNumber)
        {
            if (!Directory.Exists(storeRootFolderPath))
                Directory.CreateDirectory(storeRootFolderPath);

            _storageFilePath = Path.Combine(storeRootFolderPath, "eventnumbers.txt");
            if (!File.Exists(_storageFilePath))
            {
                WriteNextNumber(initialEventNumber-1);
            }
        }



        public long NextNumber()
        {
            var lastNumber = ReadLastNumber();
            var nextNumber = GenerateNextNumber();
            WriteNextNumber(nextNumber);
            return nextNumber;

            long GenerateNextNumber() => lastNumber + 1;
            
        }

        void WriteNextNumber(long nextNumber)
        {
            File.WriteAllText(_storageFilePath, nextNumber.ToString());
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