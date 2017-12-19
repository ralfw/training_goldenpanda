using System;
using System.Threading;
using CalculatorClient.Controller;
using CalculatorClient.Resources;
using FluentAssertions;
using NUnit.Framework;
using sc.contracts;

namespace CalculatorClient.UnitTests
{
    [TestFixture]
    public class MainControllerTests
    {
        [SetUp]
        public void SetUp()
        {
            MainController.LoginUi = new TestLoginUi();
            MainController.CalculatorCredentialService = new TestCalculatorCredentialService();
        }

        [TestCase("","0")]
        [TestCase(" ","1")]
        [TestCase("123","3")]
        public void ShouldHashPassword(string password, string expectedHashedPassword)
        {
            var hashPassword = MainController.HashPassword(password);

            hashPassword.Should().Be(expectedHashedPassword);
        }

        [Test]
        public void ShouldOpenLoginViewOnRun()
        {
            var testLoginUi = MainController.LoginUi.As<ITestLoginUi>();

            testLoginUi.IsOpen().Should().BeFalse();

            MainController.Run();

            testLoginUi.IsOpen().Should().BeTrue();
        }

        [Test]
        public void ShouldRegisterItselfToLoginRequested()
        {
            var loginRequested = new AutoResetEvent(false);
            MainController.LoginUi.OnLoginRequested += (email, passwd) => { loginRequested.Set(); };

            MainController.Run();

            MainController.LoginUi.InvokeLoginRequested("a", "b");

            loginRequested.WaitOne(TimeSpan.FromMilliseconds(300)).Should().BeTrue();
        }
    }

    public class TestCalculatorCredentialService : ICalculatorCredentialService
    {
        public void LogIn(string emailAddress, string passwordHash, Action<PermissionSet> onSuccess, Action<string> onError)
        {
            
        }
    }
}