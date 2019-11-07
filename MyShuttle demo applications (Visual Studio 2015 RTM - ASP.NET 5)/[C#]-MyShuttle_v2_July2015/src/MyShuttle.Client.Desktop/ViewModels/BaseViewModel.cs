
namespace MyShuttle.Client.Desktop.ViewModels
{
    public abstract class BaseViewModel : ObservableViewModel
    {
        public abstract void Load();

        public abstract void Update();
    }
}
