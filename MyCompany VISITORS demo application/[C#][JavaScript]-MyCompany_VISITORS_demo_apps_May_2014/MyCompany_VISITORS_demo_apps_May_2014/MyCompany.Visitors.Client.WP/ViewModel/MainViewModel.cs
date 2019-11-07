
namespace MyCompany.Visitors.Client.WP.ViewModel
{
    using System.Windows;
    using GalaSoft.MvvmLight;
    using MyCompany.Visitors.Client.WP.Services.Storage;
    using MyCompany.Visitors.Client.WP.Model;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using MyCompany.Visitors.Client.WP.Services.Photo;
    using MyCompany.Visitors.Client.WP.Services.NFC;
    using System.Collections.Generic;

    /// <summary>
    /// Viewmodel for the main view.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IStorageService storageService;
        private readonly IPhotoService photoService;
        private readonly INfcService nfcService;

        private RelayCommand editInformationCommand;
        private RelayCommand saveInformationCommand;
        private RelayCommand takePhotoCommand;

        private Visitor pInformation = new Visitor();
        private Validations visitorValidations = new Validations();
        private ScreenStates viewState;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IStorageService storageService, IPhotoService photoService, INfcService nfcService)
        {
            this.storageService = storageService;
            this.photoService = photoService;
            this.nfcService = nfcService;

            ExistInformation = this.storageService.ExistInformation();
            if (this.storageService.ExistInformation())
            {
                Information = this.storageService.LoadPersonalInformation();
                ViewState = ScreenStates.Viewing;
                this.nfcService.StartPeerFinder(Information);
            }
            else
            {
                ViewState = ScreenStates.Editing;
            }

            InitializeCommand();
        }

        /// <summary>
        /// True if exist some Visitor created.
        /// </summary>
        public bool ExistInformation;

        /// <summary>
        /// Controls the view visual state.
        /// </summary>
        public ScreenStates ViewState
        {
            get { return this.viewState; }
            set
            {
                this.viewState = value;
                base.RaisePropertyChanged(() => ViewState);
            }
        }

        /// <summary>
        /// User personal information
        /// </summary>
        public Visitor Information
        {
            get { return this.pInformation; }
            set
            {
                this.pInformation = value;
                base.RaisePropertyChanged(() => Information);
            }
        }

        /// <summary>
        /// Contains field validations result for personal information class.
        /// </summary>
        public Validations VisitorValidations
        {
            get { return this.visitorValidations; }
        }

        /// <summary>
        /// Command to change view to edit mode.
        /// </summary>
        public ICommand EditInformationCommand
        {
            get { return this.editInformationCommand; }
        }

        /// <summary>
        /// Command to change view to reading mode.
        /// </summary>
        public ICommand SaveInformationCommand
        {
            get { return this.saveInformationCommand; }
        }

        /// <summary>
        /// Command to take a photo of the user.
        /// </summary>
        public ICommand TakePhotoCommand
        {
            get { return this.takePhotoCommand; }
        }

        private void InitializeCommand()
        {
            this.saveInformationCommand = new RelayCommand(SaveInformationExecute);
            this.editInformationCommand = new RelayCommand(EditInformationExecute);
            this.takePhotoCommand = new RelayCommand(TakePhotoExecute);
        }

        private void EditInformationExecute()
        {
            ViewState = ScreenStates.Editing;
        }

        private void SaveInformationExecute()
        {
            if (string.IsNullOrWhiteSpace(Information.FirstName))
                VisitorValidations.FirstNameValidationFailed = true;

            if (string.IsNullOrWhiteSpace(Information.LastName))
                VisitorValidations.LastNameValidationFailed = true;

            if (string.IsNullOrWhiteSpace(Information.Company))
                VisitorValidations.CompanyValidationFailed = true;

            if (string.IsNullOrWhiteSpace(Information.Email))
                VisitorValidations.EmailValidationFailed = true;

            if (SaveInformationCanExecute())
            {
                this.storageService.SavePersonalInformation(Information);
                RaisePropertyChanged(() => Information);
                ViewState = ScreenStates.Viewing;
            }
        }

        private bool SaveInformationCanExecute()
        {
            return (!(VisitorValidations.FirstNameValidationFailed || VisitorValidations.LastNameValidationFailed ||
                    VisitorValidations.CompanyValidationFailed || VisitorValidations.EmailValidationFailed));
        }

        private async void TakePhotoExecute()
        {
            var result = await this.photoService.GetPhoto();

            if (result != null)
            {
                Information.VisitorPictures = new List<VisitorPicture>();
                Information.VisitorPictures.Add(new VisitorPicture() { Content = result, PictureType = PictureType.Big});
                RaisePropertyChanged(() => Information);
            }
        }
    }
}