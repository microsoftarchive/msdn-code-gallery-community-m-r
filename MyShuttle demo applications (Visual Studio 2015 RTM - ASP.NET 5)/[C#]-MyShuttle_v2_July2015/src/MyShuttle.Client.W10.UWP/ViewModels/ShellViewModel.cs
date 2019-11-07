using Cirrious.MvvmCross.ViewModels;
using System;
using System.Windows.Input;

namespace MyShuttle.Client.W10.UniversalApp.ViewModels
{
    public class ShellViewModel : MvxViewModel
    {
        ICommand _navCommand;

        public ICommand NavCommand
        {
            get
            {
                if (_navCommand == null)
                {
                    _navCommand = new MvxCommand<Type>((viewModel) =>
                    {
                        ShowViewModel(viewModel);
                    });
                }
                return _navCommand;
            }
        }
    }
}
