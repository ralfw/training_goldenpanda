using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace RomanNumberImportAdapter.UnitTest._ImportAdapter
{
    public class TesterBase
    {
        [SetUp]
        public void SetUp()
        {
            _testDirectory = "ReadFileNamesInFolder";
            Directory.CreateDirectory(_testDirectory);

            var testData1 = new List<string>
            {
                "MCD",
                "XII",
                "IX"
            };
            var testData2 = new List<string>
            {
                "LX"
            };

            File.WriteAllLines(Path.Combine(_testDirectory, "a.txt"), testData1);
            File.WriteAllLines(Path.Combine(_testDirectory, "b.txt"), testData2);

            _importAdapter = new ImportAdapter(_testDirectory);
        }

        #region Fields

        protected ImportAdapter _importAdapter;
        protected string _testDirectory;

        #endregion
    }
}