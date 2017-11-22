using System;

namespace Server
{
    public static class ChurnLogServer
    {
        public static void Execute(string rootPath, string protocolFilePath)
        {
            var start = DateTime.Now;
            var fileCount = Scan(rootPath, protocolFilePath);
            var end = DateTime.Now;
            var scanData = new ScanData(fileCount, start, end);
            LogMsgAdapter.OutputLogMsg(scanData);
        }

        #region Private methods

        private static int Scan(string rootPath, string protocolFilePath)
        {
            var filePaths = FileSystemAdapter.GetFilePaths(rootPath);
            var protocolEntries = FileSystemAdapter.GetProtocolEntries(filePaths);
            FileSystemAdapter.PersistProtocolEntries(protocolEntries, protocolFilePath);

            return filePaths.Count;
        }

        #endregion
    }
}