using System;
using System.Collections.Generic;
using System.Linq;
using ChurnServer.Infrastructure;
using ChurnServer.Statistic;

namespace ChurnServer
{
    public static class ProtocolBuilder
    {
        public static void GenerateReport(string observableDirectoryPath, string protocolFilepath, string[] fileExtensions)
        {
            var timeProvider = AdapterProvider.TimeProvider;
            var fileIo = AdapterProvider.FileIo;
            var ui = AdapterProvider.Ui;

            var startTime = timeProvider.GetCurrentDateAndTime();
            var observableFiles = fileIo.GetObservableFiles(observableDirectoryPath, fileExtensions).ToArray();

            UpdateProtocol(startTime, protocolFilepath, observableFiles);

            var reportStatistic = StatisticBuilder.BuildStatistic(startTime, observableFiles);
            ui.ShowStatistic(reportStatistic);
        }

        private static void UpdateProtocol(DateTime startTime, string protocolFilepath, IEnumerable<string> observableFiles)
        {
            var protocol = observableFiles
                .Select(observableFile => ProtocolLine.BuildProtocolLine(startTime, observableFile)).ToArray();

            AdapterProvider.FileIo.StoreFileContent(protocolFilepath, protocol);
        }

        public static int GetLinesOfCode(string filePath)
        {
            // empty lines are counted as well
            return AdapterProvider.FileIo.GetFileContent(filePath).Length;
        }
    }
}
