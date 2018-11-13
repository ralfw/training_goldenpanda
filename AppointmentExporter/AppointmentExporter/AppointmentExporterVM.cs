using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace AppointmentExporter
{
    public class AppointmentExporterVM : ViewModelBase
    {
        public event Action<DateTime, DateTime> onExport;
        public RelayCommand ExportCsvCommand { get; set; }

        public DateTime Begin
        {
            get => _begin;
            set
            {
                if (value == _begin) return;
                _begin = value;
                RaisePropertyChanged(() => Begin);
            }
        }

        public DateTime End
        {
            get => _end;
            set
            {
                if (value == _end) return;
                _end = value;
                RaisePropertyChanged(() => End);
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                if (value == _status) return;
                _status = value;
                RaisePropertyChanged(() => Status);
            }
        }

        public AppointmentExporterVM()
        {
            var lastMonth = DateTime.Now.AddMonths(-1);
            Begin = new DateTime(lastMonth.Year, lastMonth.Month, 1);
            End = Begin.AddMonths(1).AddDays(-1);
            ExportCsvCommand = new RelayCommand(() => onExport?.Invoke(Begin, End), () => true);
        }

        public void DisplayStatus(int numberOfExportedAppointments)
        {
            Status = $"{numberOfExportedAppointments} Termine exportiert";
        }

        #region Fields

        private DateTime _begin;
        private DateTime _end;
        private string _status;

        #endregion
    }
}