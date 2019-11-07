namespace MyCompany.Travel.Client.Desktop.ViewModel.Base
{
    using GalaSoft.MvvmLight;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
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
        /// Internal dispose called if not overrided
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// RaisePropertyChanged override to implement CallerMemberName
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            base.RaisePropertyChanged(propertyName);
        }

        /// <summary>
        /// Dispose override
        /// </summary>
        /// <param name="dispose"></param>
        protected virtual void Dispose(bool dispose)
        {
        }
    }
}
