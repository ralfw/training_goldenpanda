using System.ComponentModel;
using System.Runtime.CompilerServices;
using CalculatorClient.Properties;

namespace CalculatorClient.Infrastructure
{
    public abstract class ViewModelBase : IViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public abstract string Title { get; }
    }
}