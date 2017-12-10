using System;
using ChurnServer.Adapter;
using ChurnServer.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace ProtocolBuilder.IntegrationTests
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

            Action call = () => ChurnServer.ProtocolBuilder.GenerateReport("observableDir", "protocolFilePath", fileExtensions);

            call.ShouldThrow<NotImplementedException>();
        }
    }
}