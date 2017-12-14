using System.ComponentModel;

namespace CalculatorClient.Infrastructure
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
        string Title { get; }
    }
}