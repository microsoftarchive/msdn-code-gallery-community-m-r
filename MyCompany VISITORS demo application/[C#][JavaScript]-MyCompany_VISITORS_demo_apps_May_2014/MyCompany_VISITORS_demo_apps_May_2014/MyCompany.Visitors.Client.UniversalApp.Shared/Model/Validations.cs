namespace MyCompany.Visitors.Client.UniversalApp.Model
{
    using MyCompany.Visitors.Client.UniversalApp.ViewModel.Base;

    /// <summary>
    /// This class contains validations for the Visitor class.
    /// </summary>
    public class Validations : VMBase
    {
        private bool firstNameValidationFailed;
        private bool lastNameValidationFailed;
        private bool emailValidationFailed;
        private bool correctEmailValidationFailed;
        private bool companyValidationFailed;

        /// <summary>
        /// First Name validation failed.
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
        /// Last Name validation failed.
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
        /// Email validation failed.
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

        /// <summary>
        /// Correct Email validation failed.
        /// </summary>
        public bool CorrectEmailValidationFailed
        {
            get { return this.correctEmailValidationFailed; }
            set
            {
                this.correctEmailValidationFailed = value;
                RaisePropertyChanged(() => CorrectEmailValidationFailed);
            }
        }

        /// <summary>
        /// CompanyValidation failed.
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
    }
}
