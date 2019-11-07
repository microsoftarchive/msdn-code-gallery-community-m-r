namespace MyCompany.Visitors.Client.UniversalApp.ViewModel.Base
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Custom viewmodel base.
    /// </summary>
    public class VMBase : ViewModelBase, IDisposable
    {
        private bool isBusy;

        /// <summary>
        /// Set or get if the viewmodel is in busy mode (performing server petitions...)
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
        /// Dispose method.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Overrides Dispose method.
        /// </summary>
        /// <param name="dispose"></param>
        protected virtual void Dispose(bool dispose)
        {
        }
    }
}
