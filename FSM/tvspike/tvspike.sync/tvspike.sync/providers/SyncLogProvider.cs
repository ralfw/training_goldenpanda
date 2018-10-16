using System;
using System.IO;
using System.Web.Script.Serialization;

namespace tvspike.sync
{
    internal class SyncLogProvider : IDisposable
    {
        public class SyncLogEntry
        {
            public string LetztePullSignatur = "";
            public string LetztePushSignatur = "";
        }

        readonly JavaScriptSerializer _json = new JavaScriptSerializer();
        
        private readonly string _logFilename;
        private SyncLogEntry _entry = new SyncLogEntry();
        
        
        public SyncLogProvider() : this ("synclog.json") {}
        internal SyncLogProvider(string logFilename) {
            _logFilename = logFilename;
            if (!File.Exists(_logFilename)) Store();
            Load();
        }

        
        public string LetztePullSignatur {
            get => _entry.LetztePullSignatur;
            set => _entry.LetztePullSignatur = value;
        }

        public string LetztePushSignatur {
            get => _entry.LetztePushSignatur;
            set => _entry.LetztePushSignatur = value;
        }


        private void Load() {
            var entryJson = File.ReadAllText(_logFilename);
            _entry = _json.Deserialize<SyncLogEntry>(entryJson);
        }
        
        private void Store() {
            var syncLogJson = _json.Serialize(_entry);
            File.WriteAllText(_logFilename, syncLogJson);
        }

        
        public void Dispose() => Store();
    }
}