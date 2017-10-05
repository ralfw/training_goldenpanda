using System;
using System.IO;
using FluentAssertions;
using FromRomanExportAdapter;
using NUnit.Framework;

namespace FromRomanExportAdapterTest._ExportAdapter
{
    [TestFixture]
    public class Export
    {
        private const string OutputPath = @"fromRoman\output\";

        [SetUp]
        public void SetUp()
        {
            if(Directory.Exists(OutputPath))
                Directory.Delete(OutputPath, true);
            Directory.CreateDirectory(OutputPath);
        }

        [Test]
        public void ShouldCreateFileWithGivenNumbers()
        {
            int[] numbers = {1, 2};

            ExportAdapter.Export(numbers, OutputPath);
            
            Directory.GetFiles(OutputPath).Length.Should().Be(1);

            var file = Directory.GetFiles(OutputPath)[0];
            var fileContent = File.ReadAllLines(file);
            fileContent[0].Should().Be(numbers[0].ToString());
            fileContent[1].Should().Be(numbers[1].ToString());
        }


        [Test]
        public void ShouldCreateEmptyFileForNoNumbers()
        {
            int[] numbers = {};

            ExportAdapter.Export(numbers, OutputPath);

            Directory.GetFiles(OutputPath).Length.Should().Be(1);

            var file = Directory.GetFiles(OutputPath)[0];
            var fileContent = File.ReadAllLines(file);
            fileContent.Should().BeEmpty();

        }

        [Test]
        public void ShouldNotOverwriteExistingFile()
        {
            int[] numbers = { 1, 2 };

            File.WriteAllLines($"{OutputPath}{Guid.NewGuid()}.txt", new string []{});

            ExportAdapter.Export(numbers, OutputPath);

            Directory.GetFiles(OutputPath).Length.Should().Be(2);
        }


    }
}