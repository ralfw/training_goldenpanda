using NUnit.Framework;

namespace credentionalService.core.tests
{
    [TestFixture]
    internal class user_services_tests
    {
        [Test, Ignore]
        public void ShouldAddUser()
        {
            user_services service = new user_services();
            service.AddUser("nikolai.falke@wago.com", "Student", () => { }, p => { });
        }
    }
}