using System;
using FluentAssertions;
using NUnit.Framework;

namespace ChurnServer.UnitTests
{
    [TestFixture]
    public class ChurnServerConfigurationTests
    {
        [Test]
        public void ShouldGetValidConfiguration()
        {
            var args = new []
            {
                @"c:\codeToWatch",
                "45",
                @"c:\output\churnlog.csv"
            };

            var configuration = ChurnServerConfigurationProvider.CreateConfiguration(args);

            configuration.Should().NotBeNull();
            configuration.ObservableDirectoryPath.Should().Be(@"c:\codeToWatch");
            configuration.SamplingRateInSeconds.Should().Be(45);
            configuration.ProtocolFilePath.Should().Be(@"c:\output\churnlog.csv");
            configuration.FileExtensions.Should().ContainInOrder(".txt", ".cs");
        }
    }
}