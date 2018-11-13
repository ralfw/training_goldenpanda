using System;

namespace TerminExport.adapters
{
    internal class ExportDlg
    {
        public void Show() {
            var (defaultBegin, defaultEnd) = CalcDefaults();
            var (begin, end) = AskForBeginEnd(defaultBegin, defaultEnd);
            OnExportRequested(begin, end);

        }

        private (DateTime begin, DateTime end) CalcDefaults() {
            var today = DateTime.Today;
            var prevMonth = new DateTime(today.Year, today.Month, 1);   
            var begin = prevMonth.AddMonths(-1);
            var end = prevMonth.AddDays(-1);
            return (begin, end);
        }
        
        private (DateTime begin, DateTime end) AskForBeginEnd(DateTime defaultBegin, DateTime defaultEnd) {
            Console.Write($"Begin ({defaultBegin}): ");
            var sBegin = Console.ReadLine();
            var begin = sBegin == "" ? defaultBegin : DateTime.Parse(sBegin);
            
            Console.Write($"End ({defaultEnd}): ");
            var sEnd = Console.ReadLine();
            var end = sBegin == "" ? defaultEnd : DateTime.Parse(sEnd);

            return (begin, end);
        }
        
        
        public void DisplayStatus(int nExported) {
            Console.WriteLine($"{nExported} appointments exported");
        }

        
        public event Action<DateTime, DateTime> OnExportRequested;
    }
}