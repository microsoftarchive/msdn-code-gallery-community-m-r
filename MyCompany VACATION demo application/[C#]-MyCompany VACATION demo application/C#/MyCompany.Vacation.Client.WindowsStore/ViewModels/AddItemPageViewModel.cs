using GalaSoft.MvvmLight;
using MyCompany.Vacation.Client.WindowsStore.Common;
using MyCompany.Vacation.Client.WindowsStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCompany.Vacation.Client.WindowsStore
{
    /// <summary>
    /// Add item page view model.
    /// </summary>
    public class AddItemPageViewModel : ViewModelBase
    {
        private ICommand saveCommand;

        private DateTime from;
        private DateTime to;
        private string comment;
        
        /// <summary>
        /// Add item page item view model contructor.
        /// </summary>
        public AddItemPageViewModel()
        {
            InitializeData();
            InitializeCommands();
        }

        /// <summary>
        /// Save command.
        /// </summary>
        public ICommand SaveCommand
        {
            get { return saveCommand; }
            private set
            {
                saveCommand = value;
                RaisePropertyChanged(() => SaveCommand);
            }
        }

        /// <summary>
        /// From date.
        /// </summary>
        public DateTime From
        {
            get { return from; }
            set
            {
                from = value;
                RaisePropertyChanged(() => From);
            }
        }

        /// <summary>
        /// To date.
        /// </summary>
        public DateTime To
        {
            get { return to; }
            set
            {
                to = value;
                RaisePropertyChanged(() => To);
            }
        }

        /// <summary>
        /// Comment.
        /// </summary>
        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                RaisePropertyChanged(() => Comment);
            }
        }

        private void InitializeData()
        {
            From = DateTime.Now.AddDays(1);
            To = DateTime.Now.AddDays(1);
            Comment = string.Empty;
        }

        private void InitializeCommands()
        {
            this.SaveCommand = new RelayCommand(SaveCommandExecute);
        }

        private void SaveCommandExecute()
        {
            // TODO: Insert code here

            App.rootFrame.Navigate(typeof(HubPage));
        }
    }
}
