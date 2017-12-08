using System;
using ChurnServer;
using FluentAssertions;
using NUnit.Framework;

namespace ReportBuilder.IntegrationTests
{
    [TestFixture]
    public class GenerateReport
    {
        [Test]
        public void ShouldThrowNotImplementedException()
        {
            string[] fileExtensions = { "", "" };

            Action call = () => ProtocolBuilder.GenerateReport("observableDir", "protocolFilePath", fileExtensions);

            call.ShouldThrow<NotImplementedException>();
        }
    }
}