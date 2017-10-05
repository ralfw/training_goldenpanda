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
            var display = new Display();

            var importedResult = fromRomanImportAdapter.Import();
            var decimals = FromRomanConverter.ConvertRomanNumbersToArabInts(importedResult.Item1);
            FromRomanExportAdapter.ExportAdapter.Export(decimals, outputPath);
            display.ShowStatistic(importedResult.Item2, importedResult.Item1.Length);
        }
    }
}