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
            var testDirectory = "ReadFileNamesInFolder";
            Directory.CreateDirectory(testDirectory);

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

            File.WriteAllLines(Path.Combine(testDirectory, "a.txt"), testData1);
            File.WriteAllLines(Path.Combine(testDirectory, "b.txt"), testData2);

            _importAdapter = new ImportAdapter("ReadFileNamesInFolder");
        }

        #region Fields

        protected ImportAdapter _importAdapter;

        #endregion
    }
}