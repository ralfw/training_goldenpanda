using System.IO;
using NUnit.Framework;

namespace FileIo.IntegrationTests
{
    public class FileIoTestsBase
    {
        public static string GetPathToTestDirectory()
        {
            return Path.Combine(
                TestContext.CurrentContext.TestDirectory.Replace(@"\bin\Debug", ""), "TestDirectory");
        }
    }
}