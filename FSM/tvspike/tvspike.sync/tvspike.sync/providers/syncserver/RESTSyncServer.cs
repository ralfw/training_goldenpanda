using System;
using System.Collections.Generic;

namespace tvspike.sync
{
    internal class RESTSyncServer : ISyncServer
    {
        public RESTSyncServer(Uri address) {}
        
        public Event[] Pull(string letztePullSignatur, string clientId)
        {
            throw new NotImplementedException();
        }

        public void Push(IEnumerable<Event> events)
        {
            throw new NotImplementedException();
        }
    }
}