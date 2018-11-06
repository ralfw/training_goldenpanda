using System;
using FluentAssertions;
using NUnit.Framework;
using tvspike.contracts;

namespace tvspike.es.tests
{
    [TestFixture]
    public class EventFilenameTests
    {
        [Test]
        public void ShouldCreateFilenameFromEventAndClientId()
        {
            var clientId = Guid.NewGuid().ToString();
            var @event = new Event { Nummer = 100, Id = "1", Name = "EventA" };

            var filename = EventFilename.From(@event, clientId);

            filename.Number.Should().Be("00000000000000000100");
            filename.ClientId.Should().Be(clientId);
            filename.EventId.Should().Be("000000000000000000000000000000000001");
            filename.EventName.Should().Be("EventA");

            filename.Name.Should()
                    .Be($"00000000000000000100_{clientId}_000000000000000000000000000000000001_EventA.txt");

        }

        [Test]
        public void ShouldThrowExceptionIfEventNameIsLongerThan20Characters()
        {
            var clientId = Guid.NewGuid().ToString();
            var @event = new Event { Nummer = 100, Id = "1", Name = "123456789012345678901" };

            Action call = () => EventFilename.From(@event, clientId);

            call.ShouldThrow<InvalidOperationException>().WithMessage("Event name exceeds maximum of 20 characters.");
        }
    }
}