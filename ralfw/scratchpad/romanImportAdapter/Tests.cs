using System;
using System.IO;
using System.Security.Policy;
using NUnit.Framework;
using romanConversion.adapters;


namespace fromRoman.adapters.tests
{
    [TestFixture]
    public class test_ImportAdapter
    {
        [SetUp]
        public void Setup() {
            Environment.CurrentDirectory = TestContext.CurrentContext.TestDirectory;
        }
        
        [Test]
        public void Find_files()
        {
            if (Directory.Exists("test_input")) Directory.Delete("test_input", true);
            Directory.CreateDirectory("test_input");
            File.WriteAllText("test_input/a.txt", "x");
            File.WriteAllText("test_input/b.txt", "y");

            var sut = new ImportAdapter();
            var result = sut.Find_input_files("test_input");
            Assert.AreEqual(new[] {"test_input/a.txt", "test_input/b.txt"}, result);
        }


        [Test]
        public void Import_roman_numbers()
        {
            if (Directory.Exists("test_input")) Directory.Delete("test_input", true);
            Directory.CreateDirectory("test_input");
            File.WriteAllText("test_input/a.txt", "x");
            File.WriteAllLines("test_input/b.txt", new[]{"y", "z"});
            
            var sut = new ImportAdapter();
            var result = sut.Import_numbers(new[] {"test_input/a.txt", "test_input/b.txt"});
            
            Assert.AreEqual(new[]{"x", "y", "z"}, result);
        }

        
        [Test]
        public void Acceptance()
        {
            if (Directory.Exists("test_input")) Directory.Delete("test_input", true);
            Directory.CreateDirectory("test_input");
            File.WriteAllText("test_input/a.txt", "x");
            File.WriteAllLines("test_input/b.txt", new[]{"y", "z"});
            
            var sut = new ImportAdapter();
            var (n, numbers) = sut.Import("test_input");
            
            Assert.AreEqual(2, n);
            Assert.AreEqual(new[]{"x", "y", "z"}, numbers);
            Assert.AreEqual(0, Directory.GetFiles("test_input").Length);
        }
    }
}