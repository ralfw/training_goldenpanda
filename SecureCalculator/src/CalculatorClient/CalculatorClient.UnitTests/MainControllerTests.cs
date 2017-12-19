using System;
using CalculatorClient.Controller;
using FluentAssertions;
using NUnit.Framework;

namespace CalculatorClient.UnitTests
{
    [TestFixture]
    public class MainControllerTests
    {
        [TestCase("","0")]
        [TestCase(" ","1")]
        [TestCase("123","3")]
        public void ShouldHashPassword(string password, string expectedHashedPassword)
        {
            var hashPassword = MainController.HashPassword(password);

            hashPassword.Should().Be(expectedHashedPassword);
        }
    }
}