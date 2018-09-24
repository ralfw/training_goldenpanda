using System;
using System.Dynamic;
using Moq;
using NUnit.Framework;
using sc.administration.Data;
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
            dynamic config = new {_RoutePath = "cu", email = "test@wago.com", role ="student", serveruri ="uri"};

            var credentialServiceMock = new Mock<IAdminCredentialService>();

            Controller.Run(config, credentialServiceMock.Object);

            credentialServiceMock.Verify( m => m.CreateUser("test@wago.com", "student", It.IsAny<Action>(), It.IsAny<Action<string>>()));
        }
    }
}