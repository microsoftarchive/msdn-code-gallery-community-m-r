namespace MyCompany.Travel.Client.Desktop.Model
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using MyCompany.Travel.Client.Desktop.Converters;
    using System;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Dialog message
    /// </summary>
    public class CustomDialogMessage : ObservableObject
    {
        private Action acceptAction;
        private Action cancelAction;

        private RelayCommand acceptCommand;
        private RelayCommand cancelCommand;

        private TravelTypeToTextConverter travelTypeToTextConverter = new TravelTypeToTextConverter();
        private string message;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="acceptAction">Action to be executed when pressing accept button</param>
        /// <param name="message">Dialog message</param>
        /// <param name="showCancel">Show or not cancel button</param>
        public CustomDialogMessage(Action acceptAction, string message, Visibility showCancel)
        {
            this.acceptAction = acceptAction;
            Message = message;
            ShowCancel = showCancel;

            this.acceptCommand = new RelayCommand(() =>
            {
                this.acceptAction();
                this.cancelAction();
            });
        }

        /// <summary>
        /// Confirms dialog.
        /// </summary>
        public ICommand AcceptCommand
        {
            get { return this.acceptCommand; }
        }

        /// <summary>
        /// Do nothing.
        /// </summary>
        public ICommand CancelCommand
        {
            get { return this.cancelCommand; }
        }

        /// <summary>
        /// Action to be executed when cancel command is called.
        /// </summary>
        public Action CancelAction {
            get { return this.cancelAction; }
            set
            {
                this.cancelAction = value;
                this.cancelCommand = new RelayCommand(this.cancelAction);
            }
        }

        /// <summary>
        /// Dialog message.
        /// </summary>
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
                RaisePropertyChanged(() => Message);
            } 
        }

        /// <summary>
        /// Show or not cancel button
        /// </summary>
        public Visibility ShowCancel { get; set; }
    }
}
