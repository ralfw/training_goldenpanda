using System;
using System.Collections.Generic;

namespace tvspike.sync
{
    internal class EventStoreProvider
    {
        private readonly string _path;

        public EventStoreProvider(string path) {
            _path = path;
        }


        public void Import(IEnumerable<Event> @event) {
            
        }

        public Event[] Export(string letztePushSignatur, string clientId)
        {
            throw new NotImplementedException();
        }
    }
}