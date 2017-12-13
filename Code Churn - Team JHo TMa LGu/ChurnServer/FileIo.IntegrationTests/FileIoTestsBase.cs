using System.IO;
using NUnit.Framework;

namespace FileIo.IntegrationTests
{
    public class FileIoTestsBase
    {
        protected static string GetPathToTestDirectory()
        {
            return Path.Combine(
                TestContext.CurrentContext.TestDirectory.Replace(@"\bin\Debug", ""), "TestDirectory");
        }

        protected static string GetPathToTestDirectoryForReadWrite()
        {
            return Path.Combine(
                TestContext.CurrentContext.TestDirectory.Replace(@"\bin\Debug", ""), "TestDirectoryRw");
        }
    }
}