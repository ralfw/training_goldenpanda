using System.ComponentModel;

namespace CalculatorClient
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
        string Title { get; }
    }
}