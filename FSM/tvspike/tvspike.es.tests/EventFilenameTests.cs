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
        public void ShouldCreateFilenameFromEventFileInfoAndClientId()
        {
            var clientId = Guid.NewGuid().ToString();
            var eventFileInfo = new EventFileInfo {EventNumber = "100", EventId = "1", EventName = "EventA", EventData = null};
              
            var filename = EventFilename.From(eventFileInfo, clientId);

            filename.Number.Should().Be("00000000000000000100");
            filename.ClientId.Should().Be(clientId);
            filename.EventId.Should().Be("000000000000000000000000000000000001");
            filename.EventName.Should().Be("EventA");

            filename.Name.Should()
                    .Be($"00000000000000000100_{clientId}_000000000000000000000000000000000001_EventA.txt");

        }

        [TestCase("00000000000000000500_572e2387-00f9-4f8c-af7a-952f1a06b8d2_a2a45ecd-3060-415d-ab5c-ff1f33b8c9a4_EventA.txt")]
        [TestCase(@"somedir\00000000000000000500_572e2387-00f9-4f8c-af7a-952f1a06b8d2_a2a45ecd-3060-415d-ab5c-ff1f33b8c9a4_EventA.txt")]
        [TestCase(@"c:\somedir\00000000000000000500_572e2387-00f9-4f8c-af7a-952f1a06b8d2_a2a45ecd-3060-415d-ab5c-ff1f33b8c9a4_EventA.txt")]
        public void ShouldCreateFilenameFromFullPathString(string path)
        {
            var eventFilename = EventFilename.From(path);

            eventFilename.Number.Should().Be("00000000000000000500");
            eventFilename.ClientId.Should().Be("572e2387-00f9-4f8c-af7a-952f1a06b8d2");
            eventFilename.EventId.Should().Be("a2a45ecd-3060-415d-ab5c-ff1f33b8c9a4");
            eventFilename.EventName.Should().Be("EventA");
        }

        [Test]
        public void ShouldThrowExceptionIfEventNameInInputStringIsLongerThan20Characters()
        {
            const string path = "00000000000000000500_572e2387-00f9-4f8c-af7a-952f1a06b8d2_a2a45ecd-3060-415d-ab5c-ff1f33b8c9a4_123456789012345678901.txt";

            Action call = () => EventFilename.From(path);

            call.ShouldThrow<InvalidOperationException>().WithMessage("Event name exceeds maximum of 20 characters.");
        }

        [Test]
        public void ShouldThrowExceptionIfEventNameIsLongerThan20CharactersOld()
        {
            var clientId = Guid.NewGuid().ToString();
            var @event = new Event { Nummer = 100, Id = "1", Name = "123456789012345678901" };

            Action call = () => EventFilename.From(@event, clientId);

            call.ShouldThrow<InvalidOperationException>().WithMessage("Event name exceeds maximum of 20 characters.");
        }

        [Test]
        public void ShouldThrowExceptionIfEventNameIsLongerThan20Characters()
        {
            var clientId = Guid.NewGuid().ToString();
            var eventFileInfo = new EventFileInfo { EventNumber = "100", EventId = "1", EventName = "123456789012345678901", EventData = null };

            Action call = () => EventFilename.From(eventFileInfo, clientId);

            call.ShouldThrow<InvalidOperationException>().WithMessage("Event name exceeds maximum of 20 characters.");
        }
    }
}