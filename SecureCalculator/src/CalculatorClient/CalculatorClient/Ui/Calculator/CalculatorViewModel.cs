using System.Collections.ObjectModel;
using CalculatorClient.Infrastructure;
using sc.contracts;

namespace CalculatorClient
{
    public class CalculatorViewModel : ViewModelBase
    {
        
        private string _email;
        private ObservableCollection<Permissions> _permissionSet;

        public CalculatorViewModel(PermissionSet permissionSet)
        {
            PermissionSet = new ObservableCollection<Permissions>(permissionSet.Permissions);
        }

        public CalculatorViewModel(PermissionSet permissionSet, string email)
        {
            PermissionSet = new ObservableCollection<Permissions>(permissionSet.Permissions);
            Email = email;
        }

        public override string Title => "Secure Calculator";

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email)); 
            }
        }

        public ObservableCollection<Permissions> PermissionSet
        {
            get { return _permissionSet; }
            set
            {
                _permissionSet = value;
                OnPropertyChanged(nameof(PermissionSet));
            }
        }
    }
}
