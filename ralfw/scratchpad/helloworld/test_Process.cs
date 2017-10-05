using NUnit.Framework;

namespace helloworld
{
    [TestFixture]
    public class test_Process
    {
        [TestCase("r", new string[0], "Hello, r!", new[]{"r"})]
        [TestCase("r", new[]{"r"}, "Welcome back, r!", new[]{"r", "r"})]
        [TestCase("r", new[]{"r", "r"}, "Hello my good friend, r!", new[]{"r", "r", "r"})]
        [TestCase("b", new[]{"r"}, "Hello, b!", new[]{"r", "b"})]
        [TestCase("r", new[]{"r", "r", "b", "r"}, "Hello my good friend, r!", new[]{"r", "r", "b", "r", "r"})]
        public void Several(string name, string[] visitors, string greeting, string[] visitors_) {
            var (g, v) = Program.Process(name, visitors);
            Assert.AreEqual(greeting, g);
            Assert.AreEqual(visitors_, v);
        }
    }
}