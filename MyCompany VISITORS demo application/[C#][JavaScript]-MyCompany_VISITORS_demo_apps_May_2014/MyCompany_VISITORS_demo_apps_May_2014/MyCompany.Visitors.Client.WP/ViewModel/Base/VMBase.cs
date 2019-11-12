namespace MyCompany.Visitors.Client.WP.ViewModel.Base
{
    using GalaSoft.MvvmLight;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Extended base viewmodel
    /// </summary>
    public class VMBase : ViewModelBase
    {
        private bool isBusy;
        private string busyText;

        /// <summary>
        /// Constructor
        /// </summary>
        public VMBase()
        {
        }

        /// <summary>
        /// Controls when to show progress bar in UX
        /// </summary>
        public bool IsBusy
        {
            get { return this.isBusy; }
            set
            {
                this.isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        /// <summary>
        /// Stablish the loading message.
        /// </summary>
        public string BusyText
        {
            get { return this.busyText; }
            set
            {
                this.busyText = value;
                RaisePropertyChanged(() => BusyText);
            }
        }
    }
}
