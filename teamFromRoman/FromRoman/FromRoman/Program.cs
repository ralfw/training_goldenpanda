using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fromRomanConverter;
using RomanNumberImportAdapter;

namespace FromRoman
{
    class Program
    {
        private static string inputPath = @"./input/";
        private static string outputPath = @"./output/";

        static void Main(string[] args)
        {
            var fromRomanImportAdapter = new ImportAdapter(inputPath);
            var importedResult = fromRomanImportAdapter.Import();

            //var decimals = FromRomanConverter.;

            //var fromRomanExporter = new FromRomanExporter();
            //fromRomanExporter.Export(decimals, outputPaht);

            //ShowStatistic(numberOfFiles, numberOfRomans);
        }
    }
}