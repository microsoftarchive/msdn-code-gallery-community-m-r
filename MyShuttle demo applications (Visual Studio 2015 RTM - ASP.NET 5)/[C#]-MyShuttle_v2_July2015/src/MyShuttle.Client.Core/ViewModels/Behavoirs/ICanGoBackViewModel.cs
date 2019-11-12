using System.Windows.Input;

namespace MyShuttle.Client.Core.ViewModels.Behavoirs
{
    public interface ICanGoBackViewModel
    {
        ICommand GoBackCommand { get; }
    }
}
