namespace MyCompany.Visitors.Client.WP.Model
{
    using GalaSoft.MvvmLight;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This class contains Validations for the PersonalInformation class.
    /// </summary>
    public class Validations : ViewModelBase
    {
        private bool firstNameValidationFailed;
        private bool lastNameValidationFailed;
        private bool companyValidationFailed;
        private bool emailValidationFailed;

        /// <summary>
        /// First name validation
        /// </summary>
        public bool FirstNameValidationFailed
        {
            get { return this.firstNameValidationFailed; }
            set
            {
                this.firstNameValidationFailed = value;
                RaisePropertyChanged(() => FirstNameValidationFailed);
            }
        }

        /// <summary>
        /// Last name validation
        /// </summary>
        public bool LastNameValidationFailed
        { 
            get { return this.lastNameValidationFailed; }
            set
            {
                this.lastNameValidationFailed = value;
                RaisePropertyChanged(() => LastNameValidationFailed);
            }
        }

        /// <summary>
        /// Company validation
        /// </summary>
        public bool CompanyValidationFailed
        { 
            get { return this.companyValidationFailed; }
            set
            {
                this.companyValidationFailed = value;
                RaisePropertyChanged(() => CompanyValidationFailed);
            }
        }

        /// <summary>
        /// Email validation
        /// </summary>
        public bool EmailValidationFailed
        {
            get { return this.emailValidationFailed; }
            set
            {
                this.emailValidationFailed = value;
                RaisePropertyChanged(() => EmailValidationFailed);
            }
        }
    }
}
