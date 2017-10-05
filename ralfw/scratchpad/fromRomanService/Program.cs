using System;
using System.Collections.Generic;
using System.Linq;

namespace fromRomanService
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var import = new romanConversion.adapters.ImportAdapter();
            var export = new romanConversion.adapters.ExportAdapter();
            var ui = new UIAdapter();

            var imported = import.Import("input");
            var decimalNumbers = romanConversion.FromRomanConverter.Convert(imported.romanNumbers);
            export.Export(decimalNumbers.Select(d => d.ToString()), "output");
            ui.Display_stats(imported.numberOfImportedFiles, decimalNumbers.Length);
        }
    }


    class UIAdapter
    {
        public void Display_stats(int numberOfFilesImported, int numberOfNumbersProcessed) {
            Console.WriteLine($"{numberOfFilesImported} files with {numberOfNumbersProcessed} numbers processed.");
        }
    }
}