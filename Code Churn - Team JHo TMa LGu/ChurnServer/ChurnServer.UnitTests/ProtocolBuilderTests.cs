using System;
using FluentAssertions;
using NUnit.Framework;

namespace ChurnServer.UnitTests
{
    [TestFixture]
    public class ProtocolBuilderTests
    {
        [Test]
        public void ShouldThrowNotImplementedException()
        {
            string[] fileExtensions = {"", ""};

            Action call = () => ProtocolBuilder.GenerateReport("observableDir", "protocolFilePath", fileExtensions);

            call.ShouldThrow<NotImplementedException>();
        }
    }
}