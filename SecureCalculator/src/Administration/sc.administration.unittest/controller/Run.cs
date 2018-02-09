using System;
using System.Dynamic;
using Moq;
using NUnit.Framework;
using sc.administration.Providers;
using sc.contracts;

namespace sc.administration.UnitTest.controller
{
    [TestFixture]
    public class Run
    {
        [Test]
        public void ShouldCallCreateUser()
        {
            dynamic config = new ExpandoObject();
            config._RoutePath = "cu";
            config.email = "test@wago.com";
            config.role = "student";
            config.serveruri = "uri";

            var credentialServiceMock = new Mock<IAdminCredentialService>();

            Controller.Run(config, credentialServiceMock.Object);

            credentialServiceMock.Verify( m => m.CreateUser("test@wago.com", "student", It.IsAny<Action>(), It.IsAny<Action<string>>()));
        }
    }
}