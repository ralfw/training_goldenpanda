using TerminExport.adapters;

namespace TerminExport.integration
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var dlg = new ExportDlg();
            var repo = new SQLiteAppointmentProvider();
            var rh = new RequestHandler(repo);

            dlg.OnExportRequested += (begin, end) => {
                var nExported = rh.HandleExportRequest(begin, end);
                dlg.DisplayStatus(nExported);
            };
            
            dlg.Show();
        }
    }
}