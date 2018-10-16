using System.Collections;
using System.Collections.Generic;

namespace tvspike.sync
{
    internal interface ISyncServer
    {
        Event[] Pull(string letztePullSignatur, string clientId);
        void Push(IEnumerable<Event> events);
    }
}