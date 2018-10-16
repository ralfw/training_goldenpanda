using System;

namespace tvspike.sync
{
    internal class SyncServerFactory
    {
        public static ISyncServer Create(string syncServerUrl) {
            if (syncServerUrl.StartsWith("http://"))
                return new RESTSyncServer(new Uri(syncServerUrl));
            return new FilesystemSyncServer(syncServerUrl);
        }
    }
}