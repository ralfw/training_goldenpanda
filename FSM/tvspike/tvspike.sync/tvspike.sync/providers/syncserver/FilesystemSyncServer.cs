using System.Collections.Generic;

namespace tvspike.sync
{
    internal class FilesystemSyncServer : ISyncServer
    {
        private readonly string _eventfolderPath;
        
        public FilesystemSyncServer(string eventfolderPath) {
            _eventfolderPath = eventfolderPath;
        }
        
        public Event[] Pull(string letztePullSignatur, string clientId)
        {
            throw new System.NotImplementedException();
        }

        public void Push(IEnumerable<Event> events)
        {
            throw new System.NotImplementedException();
        }
    }
}