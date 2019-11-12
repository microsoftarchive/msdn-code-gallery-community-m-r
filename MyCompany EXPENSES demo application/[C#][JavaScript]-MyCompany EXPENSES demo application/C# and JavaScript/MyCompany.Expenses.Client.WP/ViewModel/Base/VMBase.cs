namespace MyCompany.Expenses.Client.WP.ViewModel.Base
{
    using GalaSoft.MvvmLight;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Extended base viewmodel
    /// </summary>
    public class VMBase : ViewModelBase, IDisposable
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
                this.RaisePropertyChanged();
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
                this.RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Overrided RaisePropertyChanged to use CallerMemberName
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            base.RaisePropertyChanged(propertyName);
        }

        /// <summary>
        /// Dispose of this VMBase
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// DIspose Overridable by ViewModels.
        /// </summary>
        /// <param name="dispose"></param>
        protected virtual void Dispose(bool dispose)
        {
        }
    }
}
