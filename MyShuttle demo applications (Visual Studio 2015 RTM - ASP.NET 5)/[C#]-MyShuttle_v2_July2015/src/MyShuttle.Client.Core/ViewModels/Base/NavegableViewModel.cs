using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.ViewModels.Behavoirs;
using System.Windows.Input;

namespace MyShuttle.Client.Core.ViewModels.Base
{
    public class NavegableViewModel : MvxViewModel, ICanGoBackViewModel
    {
        private ICommand _goBackCommand;
        
        public ICommand GoBackCommand
        {
            get
            {
                return this._goBackCommand;
            }
        }

        public NavegableViewModel()
        {
            InitializeCommands();
        }
        
        private void InitializeCommands()
        {
            this._goBackCommand = new MvxCommand(this.GoBack);
        }
        
        private void GoBack()
        {
            Close(this);
        }
    }
}
