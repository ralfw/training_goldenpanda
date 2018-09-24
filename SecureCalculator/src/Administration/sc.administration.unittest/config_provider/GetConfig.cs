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
            var args = new[] {"cu", "-email:test@wago.com", "-role:student", "-serveruri:uri"};

            var config = ConfigProvider.GetConfig(args);
 
            ((string)config._RoutePath).Should().Be("cu");
            ((string)config.email).Should().Be("test@wago.com");
            ((string)config.role).Should().Be("student");
            ((string)config.serveruri).Should().Be("uri");
        }
    }
}