using FluentAssertions;
using NUnit.Framework;
using sc.administration.Providers;

namespace sc.administration.UnitTest.config_provider
{
    [TestFixture]
    public class GetConfig
    {
        [Test]
        public void ShouldGetConfigFromArguments()
        {
            var args = new string[] {"cu", "test@wago.com", "student", "uri"};

            var config = ConfigProvider.GetConfig(args);

            config.Command.Should().Be("cu");
            config.Email.Should().Be("test@wago.com");
            config.Role.Should().Be("student");
            config.ServerUri.Should().Be("uri");
        }
    }
}