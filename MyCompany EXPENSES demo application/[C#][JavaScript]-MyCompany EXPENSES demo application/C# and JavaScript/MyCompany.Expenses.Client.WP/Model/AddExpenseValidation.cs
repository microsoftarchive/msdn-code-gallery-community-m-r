namespace MyCompany.Expenses.Client.WP.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Validation fields for add expense screen
    /// </summary>
    public class AddExpenseValidation : INotifyPropertyChanged
    {

        private bool titleFailed;
        private bool descriptionFailed;
        private bool amountFailed;

        /// <summary>
        /// True if failed to validate title
        /// </summary>
        public bool TitleFailed 
        {
            get { return this.titleFailed; }
            set
            {
                this.titleFailed = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// True if failed to validate description
        /// </summary>
        public bool DescriptionFailed 
        {
            get { return this.descriptionFailed; }
            set
            {
                this.descriptionFailed = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// True if failed to validate amount
        /// </summary>
        public bool AmountFailed 
        {
            get { return this.amountFailed; }
            set
            {
                this.amountFailed = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
