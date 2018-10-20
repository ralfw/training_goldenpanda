using System.Linq;

namespace tvspike.sync
{
    internal class App
    {
        private readonly CLI _cli;
        private readonly ISyncServer _syncServer;
        private readonly EventStoreProvider _eventStore;
        private readonly SyncLogProvider _synclogProvider;

        public App(CLI cli, ISyncServer syncServer, EventStoreProvider eventStore, SyncLogProvider synclogProvider) {
            _cli = cli;
            _syncServer = syncServer;
            _eventStore = eventStore;
            _synclogProvider = synclogProvider;
        }

        public void Run() {
            Pull();
            Push();
        }

        internal void Pull() {
            var events = _syncServer.Pull(_synclogProvider.LetztePullSignatur, _cli.ClientId);
            _eventStore.Import(events);
            _synclogProvider.LetztePullSignatur = events.Last().Dateiname;
        }

        internal void Push() {
            var events = _eventStore.Export(_synclogProvider.LetztePushSignatur, _cli.ClientId);
            _syncServer.Push(events);
            _synclogProvider.LetztePushSignatur = events.Last().Dateiname;
        }
    }
}