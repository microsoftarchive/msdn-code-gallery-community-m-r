namespace MyCompany.Visitors.Client.UniversalApp.WindowsStore.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using MyCompany.Visitors.Client.UniversalApp.Model;
    using MyCompany.Visitors.Client.UniversalApp.Model.Messages;
    using MyCompany.Visitors.Client.UniversalApp.Services.Messages;
    using MyCompany.Visitors.Client.UniversalApp.Services.Navigation;
    using MyCompany.Visitors.Client.UniversalApp.Services.Storage;
    using MyCompany.Visitors.Client.UniversalApp.ViewModel.Base;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Windows.ApplicationModel.Resources;
    using Windows.Foundation;
    using Windows.Media.Capture;
    using Windows.Storage;
    using Windows.UI.Core;
    using System.Threading.Tasks;
    using MyCompany.Visitors.Client.UniversalApp.Settings;

    /// <summary>
    /// New Visitor page ViewModel.
    /// </summary>
    public class VMNewVisitorPage : VMBase
    {
        private readonly INavigationService navService;
        private readonly IMyCompanyClient clientService;
        private readonly IMessageService messageService;
        private readonly IStorageService storageService;

        private RelayCommand navigateBackCommand;
        private RelayCommand sendNewVisitorCommand;
        private RelayCommand takePhotoCommand;
        private RelayCommand confirmNavigateBackCommand;
        private RelayCommand remainAtPageCommand;

        private Visitor visitor;
        private ICollection<VisitorPicture> visitorPictures;

        private bool isVisibleCrop;
        private bool areEnabledButtoms;
        private bool isConfirmationPopupOpened;
        private bool isAppBarOpened;
        private StorageFile imageToCrop;
        private StorageFile imageCropped;

        private Validations visitorValidations;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public VMNewVisitorPage(INavigationService navService, IMyCompanyClient clientService, IMessageService messageService, IStorageService storageService)
        {
            this.navService = navService;
            this.clientService = clientService;
            this.messageService = messageService;
            this.storageService = storageService;
            InitializeCommands();
        }

        /// <summary>
        /// Navigate Back Command.
        /// </summary>
        public ICommand NavigateBackCommand
        {
            get { return this.navigateBackCommand; }
        }

        /// <summary>
        /// Send new Visitor Command.
        /// </summary>
        public ICommand SendNewVisitorCommand
        {
            get { return this.sendNewVisitorCommand; }
        }

        /// <summary>
        /// Take Photo Command.
        /// </summary>
        public ICommand TakePhotoCommand
        {
            get { return this.takePhotoCommand; }
        }

        /// <summary>
        /// Confirm navigate back comman
        /// </summary>
        public ICommand ConfirmNavigateBackCommand
        {
            get { return this.confirmNavigateBackCommand; }
        }

        /// <summary>
        /// Remain at page command
        /// </summary>
        public ICommand RemainAtPageCommand
        {
            get { return this.remainAtPageCommand; }
        }

        /// <summary>
        /// Visitor.
        /// </summary>
        public Visitor Visitor
        {
            get { return this.visitor; }
            set
            {
                this.visitor = value;
                RaisePropertyChanged(() => Visitor);
            }
        }

        /// <summary>
        /// Visitor Pictures.
        /// </summary>
        public ICollection<VisitorPicture> VisitorPictures
        {
            get { return this.visitorPictures; }
            set
            {
                this.visitorPictures = value;
                RaisePropertyChanged(() => VisitorPictures);
            }
        }

        /// <summary>
        /// Visitor Validations.
        /// </summary>
        public Validations VisitorValidations
        {
            get { return this.visitorValidations; }
            set
            {
                this.visitorValidations = value;
                RaisePropertyChanged(() => VisitorValidations);
            }
        }

        /// <summary>
        /// Crop control Visibility.
        /// </summary>
        public bool IsVisibleCrop
        {
            get { return this.isVisibleCrop; }
            set
            {
                this.isVisibleCrop = value;
                RaisePropertyChanged(() => IsVisibleCrop);
            }
        }

        /// <summary>
        /// Enabled buttoms.
        /// </summary>
        public bool AreEnabledButtoms
        {
            get { return this.areEnabledButtoms; }
            set
            {
                this.areEnabledButtoms = value;
                RaisePropertyChanged(() => AreEnabledButtoms);
            }
        }

        /// <summary>
        /// Image to Crop.
        /// </summary>
        public StorageFile ImageToCrop
        {
            get { return this.imageToCrop; }
            set
            {
                this.imageToCrop = value;
                if(this.imageToCrop != null)
                {
                    // Add the first picture.
                    AddPicture(PictureType.Big);
                    RaisePropertyChanged(() => ImageToCrop);
                    CropImage();
                }
            }
        }        

        /// <summary>
        /// Cropped Image.
        /// </summary>
        public StorageFile ImageCropped
        {
            get { return this.imageCropped; }
            set
            {
                this.imageCropped = value;
                if(this.imageCropped != null)
                {
                    // Add the second picture cropped.
                    AddPicture(PictureType.Small);
                    // Hide the crop control.
                    IsVisibleCrop = false;
                }
            }
        }

        /// <summary>
        /// Text for the Add/Change image button
        /// </summary>
        public string ImageButtonText
        {
            get
            {
                var loader = new ResourceLoader();
                if (this.visitorPictures == null || !this.visitorPictures.Any())
                {
                    return loader.GetString("Add_Image");
                }

                return loader.GetString("ChangeImage");
            }
        }

        /// <summary>
        /// Open or close the popup
        /// </summary>
        public bool IsConfirmationPopupOpened
        {
            get
            {
                return this.isConfirmationPopupOpened;
            }
            set
            {
                this.isConfirmationPopupOpened = value;
                RaisePropertyChanged(() => IsConfirmationPopupOpened);
            }
        }

        /// <summary>
        /// Open or close the app bar
        /// </summary>
        public bool IsAppBarOpened
        {
            get
            {
                return this.isAppBarOpened;
            }
            set
            {
                this.isAppBarOpened = value;
                RaisePropertyChanged(() => IsAppBarOpened);
            }
        }

        /// <summary>
        /// Initialize data method.
        /// </summary>
        public void InitializeData()
        {
            Visitor = new Visitor();
            VisitorPictures = new Collection<VisitorPicture>();
            ImageToCrop = null;
            VisitorValidations = new Validations();
            RaisePropertyChanged(() => ImageButtonText);
        }

        /// <summary>
        /// Set the image to crop from the visitor picture.
        /// </summary>
        public async void SetImageToCrop()
        {
            if ((visitor.VisitorPictures != null) && (visitor.VisitorPictures.Any()) && (visitor.VisitorPictures.First().Content != null))
            {
                ImageToCrop = await this.storageService.ByteToFile(Visitor.VisitorPictures.First().Content, "imageFromNFC");
            }  
        }

        /// <summary>
        /// This method remove the pictures storaged.
        /// </summary>
        public async void RemovePicturesStoraged()
        {
            if (this.imageCropped != null)
            {
                await this.imageCropped.DeleteAsync();
                this.imageCropped = null;
            }
            if (this.imageToCrop != null)
            {
                await this.imageToCrop.DeleteAsync();
                this.imageToCrop = null;
            }
        }

        private bool SendNewVisitorCanExecute()
        {
                return (!(VisitorValidations.FirstNameValidationFailed || VisitorValidations.LastNameValidationFailed ||
                        VisitorValidations.CompanyValidationFailed || VisitorValidations.EmailValidationFailed || VisitorValidations.CorrectEmailValidationFailed));
        }
        private void InitializeCommands()
        {
            this.navigateBackCommand = new RelayCommand(NavigateBackExecute);
            this.sendNewVisitorCommand = new RelayCommand(SendNewVisitorExecute);
            this.takePhotoCommand = new RelayCommand(TakePhotoExecute);
            this.confirmNavigateBackCommand = new RelayCommand(ConfirmNavigateBackExecute);
            this.remainAtPageCommand = new RelayCommand(RemainAtPageExecute);
        }

        private void ConfirmNavigateBackExecute()
        {
            IsConfirmationPopupOpened = false;
            IsAppBarOpened = false;
            this.navService.GoBack();
        }

        private void RemainAtPageExecute()
        {
            IsAppBarOpened = true;
            IsConfirmationPopupOpened = false;
        }

        private void NavigateBackExecute()
        {
            IsConfirmationPopupOpened = true;
        }

        private async void SendNewVisitorExecute()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.visitor.FirstName))
                    VisitorValidations.FirstNameValidationFailed = true;

                if (string.IsNullOrWhiteSpace(this.visitor.LastName))
                    VisitorValidations.LastNameValidationFailed = true;

                if (string.IsNullOrWhiteSpace(this.visitor.Company))
                    VisitorValidations.CompanyValidationFailed = true;

                if (string.IsNullOrWhiteSpace(this.visitor.Email))
                    VisitorValidations.EmailValidationFailed = true;

                if (SendNewVisitorCanExecute())
                {
                    AreEnabledButtoms = false;
                    this.visitor.VisitorPictures = this.visitorPictures;
                    int visitorId = await clientService.VisitorService.Add(this.visitor);
                    this.navService.NavigateToNewVisitPageFromNewVisitorPage(visitorId);
                }
            }
            catch(Exception)
            {
                var loader = new ResourceLoader();
                this.messageService.ShowMessage(loader.GetString("ErrorOcurred"), loader.GetString("Error"));
            }
        }
        private async void TakePhotoExecute()
        {
            var dialog = new CameraCaptureUI();
            var aspectRatio = new Size(10, 13.5);
            dialog.PhotoSettings.CroppedAspectRatio = aspectRatio;

            StorageFile file = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (file != null)
            {
                await file.RenameAsync("imageToCrop.jpg", NameCollisionOption.ReplaceExisting);
                ImageToCrop = file;
            }
        }

        private void CropImage()
        {
            if (!AppSettings.HideSmallImageCrop)
            {
                IsVisibleCrop = true;
            }
            else
            {
                var crop = CreateAutomaticallyCroppedFile();
                crop.ContinueWith(task =>
                {
                    StorageFile croppedFile = task.Result;
                    ImageCropped = croppedFile;
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private async Task<StorageFile> CreateAutomaticallyCroppedFile()
        {
            StorageFile originalFile = this.imageToCrop;

            var folder = await StorageFolder.GetFolderFromPathAsync(originalFile.Path.Substring(0, originalFile.Path.LastIndexOf(@"\")));
            StorageFile croppedFile = await originalFile.CopyAsync(folder, string.Format("imageCropped.jpg"), NameCollisionOption.ReplaceExisting);

            return croppedFile;
        }

        private async void AddPicture(PictureType pictureType)
        {
            VisitorPicture picture = null;
            // If picture type is big, creates a new Visitor Picture using the image to crop attribute.
            if((pictureType == PictureType.Big)&&(this.imageToCrop != null))
            {
                picture = new VisitorPicture
                              {
                                  Content = await storageService.FileToByte(this.imageToCrop),
                                  PictureType = pictureType
                              };
                // If there are already two pictures in the collection.
                if(this.visitorPictures.Count == 2)
                {
                    this.visitorPictures = new Collection<VisitorPicture>(){ picture };
                }
                // If there isn't any picture in the collection yet.
                else
                {
                    this.visitorPictures.Add(picture);
                }
            }
            // If picture type is small, creates a new Visitor Picture using the image croppped attribute.
            else if ((pictureType == PictureType.Small) && ((this.imageCropped != null)))
            {
                await App.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    picture = new VisitorPicture
                    {  
                        Content = await storageService.FileToByte(this.imageCropped),
                        PictureType = pictureType
                    };
                    this.visitorPictures.Add(picture);
                });
            }

            RaisePropertyChanged(() => VisitorPictures);
            RaisePropertyChanged(() => ImageButtonText);
        }
    }
}
