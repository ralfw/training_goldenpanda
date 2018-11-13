using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace AppointmentData.Tests
{
    [TestFixture]
    public class FileWriterFixture
    {
        [SetUp]
        public void Setup()
        {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
            foreach (var file in Directory.GetFiles(".", "*.test", SearchOption.TopDirectoryOnly))
                File.Delete(file);
        }

        [TestCase(".\\Output.test", "Lorem ipsum")]
        public async Task Usage(string filePath, string testData)
        {
            await FileWriter.WriteFileContentAsync(filePath, testData);

            File.Exists(filePath).Should().BeTrue();
            using (var reader = new StreamReader(filePath))
                (await reader.ReadToEndAsync()).Should().Be(testData);
        }
    }
}
