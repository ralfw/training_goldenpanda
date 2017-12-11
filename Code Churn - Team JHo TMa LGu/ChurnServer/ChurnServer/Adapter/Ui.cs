using System;
using System.Text;
using ChurnServer.AdapterInterfaces;
using ChurnServer.Statistic;

namespace ChurnServer.Adapter
{
    public class Ui : IUi
    {
        public void ShowStatistic(ReportStatistic statistic)
        {
            var lineBuilder = new StringBuilder();
            lineBuilder.Append(statistic.StartTime);
            lineBuilder.Append(", ");
            lineBuilder.AppendFormat("{0} sec", statistic.Duration.TotalSeconds);
            lineBuilder.Append(", ");
            lineBuilder.AppendFormat("{0} files", statistic.NumberOfFiles);

            Console.Out.WriteLine(lineBuilder.ToString());
        }
    }
}