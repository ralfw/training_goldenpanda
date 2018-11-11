using System.Collections.Generic;
using tvspike.contracts;
using tvspike.es;
using tvspike.rm;

namespace tvspike.client
{
    public class RequestHandler
    {
        private readonly EventStoreProvider _eventStoreProvider;
        private readonly ReadModelProvider _readModelProvider;

        public RequestHandler(EventStoreProvider eventStoreProvider, ReadModelProvider readModelProvider)
        {
            _eventStoreProvider = eventStoreProvider;
            _readModelProvider = readModelProvider;
        }

        public IEnumerable<TerminRM> Handle(QueryTerminliste queryTerminliste)
        {
            var events = _eventStoreProvider.ReplayAll();
            return _readModelProvider.Aufbauen(events);
        }

        public void Handle(TerminLöschenCommand terminLöschenCommand) {
            var events = TerminAggregator.Process(terminLöschenCommand);
            _eventStoreProvider.Record(events);
        }

        public void Handle(NeuerTerminCommand neuerTerminCommand)
        {
            var neuesEvent = TerminAggregator.Process(neuerTerminCommand);
            _eventStoreProvider.Record(new []{neuesEvent});
        }
    }
}