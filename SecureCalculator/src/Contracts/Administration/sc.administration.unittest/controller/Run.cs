using System;
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
            var config = new Config() {Command = "cu", Email = "test@wago.com", Role = "student"};
            var credentialServiceMock = new Mock<IAdminCredentialService>();

            Controller.Run(config, credentialServiceMock.Object);

            credentialServiceMock.Verify( m => m.CreateUser("test@wago.com", "student", It.IsAny<Action>(), It.IsAny<Action<string>>()));



        }
    }
}