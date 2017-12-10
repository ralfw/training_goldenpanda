using System;
using ChurnServer;
using ChurnServer.Adapter;
using ChurnServer.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace ReportBuilder.IntegrationTests
{
    [TestFixture]
    public class GenerateReport
    {
        [SetUp]
        public void SetUp()
        {
            AdapterProvider.TimeProvider = new TimeProvider();
        }

        [Test]
        public void ShouldThrowNotImplementedException()
        {
            string[] fileExtensions = { "", "" };

            Action call = () => ProtocolBuilder.GenerateReport("observableDir", "protocolFilePath", fileExtensions);

            call.ShouldThrow<NotImplementedException>();
        }
    }
}