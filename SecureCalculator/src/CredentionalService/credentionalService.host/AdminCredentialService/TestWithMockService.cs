using NUnit.Framework;

namespace AdminCredentialService
{
    [TestFixture]
    public class TestWithMockService
    {
        [Test]
        public void CallWithoutError()
        {
            var service = new AdminCredentialService();
            var error = string.Empty;
            var success = false;


            service.CreateUser("test@mich.de", "student", () => { success = true; }, s => { error = s; });
            Assert.True(string.IsNullOrEmpty(error));
            Assert.True(success);
        }

        [Test]
        public void CallWithError()
        {
            var service = new AdminCredentialService();
            var error = string.Empty;
            var success = false;


            service.CreateUser("test_error@mich.de", "student", () => { success = true; }, s => { error = s; });
            Assert.True(!string.IsNullOrEmpty(error));
            Assert.False(success);
        }
    }
}