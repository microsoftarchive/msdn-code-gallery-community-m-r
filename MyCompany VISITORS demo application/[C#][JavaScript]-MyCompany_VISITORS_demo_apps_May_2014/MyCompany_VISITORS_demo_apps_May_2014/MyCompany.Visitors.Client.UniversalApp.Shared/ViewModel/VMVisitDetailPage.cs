namespace MyCompany.Visitors.Client.UniversalApp.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using MyCompany.Visitors.Client.DocumentResponse;
    using MyCompany.Visitors.Client.UniversalApp.Converters;
    using MyCompany.Visitors.Client.UniversalApp.Model;
    using MyCompany.Visitors.Client.UniversalApp.Model.Messages;
    using MyCompany.Visitors.Client.UniversalApp.Services.Messages;
    using MyCompany.Visitors.Client.UniversalApp.Services.Navigation;
    using MyCompany.Visitors.Client.UniversalApp.Services.SampleData;
    using MyCompany.Visitors.Client.UniversalApp.Services.Storage;
    using MyCompany.Visitors.Client.UniversalApp.Services.Tiles;    
    using MyCompany.Visitors.Client.UniversalApp.ViewModel.Base;
    using MyCompany.Visitors.Client.UniversalApp.Settings;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Windows.ApplicationModel.Resources;
    using Windows.Foundation;
    using Windows.Media.Capture;    
    using Windows.Storage;
    using Windows.UI.Core;
    using Windows.UI.Popups;
    using Windows.UI.StartScreen;
    using Windows.UI.Xaml.Media.Imaging;
    using MyCompany.Visitors.Client.UniversalApp.Services.Dispatcher;
    using System.IO;
    using Windows.Storage.Streams;
    using Windows.Media.MediaProperties;    
    

    /// <summary>
    /// Visits details viewmodel.
    /// </summary>
    public class VMVisitDetailPage : VMBase
    {
        private const string PINLOGO_PATH = "ms-appx:///Assets/PinLogo.png";
        private const string PINLOGOWIDE_PATH = "ms-appx:///Assets/PinLogoWide.png";
        private const string VISIT = "Visit_";

        private readonly INavigationService navService;
        private readonly IMyCompanyClient clientService;
        private readonly IMessageService messageService;
        private readonly ITilesService tilesService;
        private readonly ISampleDataService sampleDataService;
        private readonly IStorageService storageService;
        private readonly IDispatcherService dispatcherService;

        private RelayCommand navigateBackCommand;
        private RelayCommand<Rect> pinVisitCommand;
        private RelayCommand validateVisitCommand;
        private RelayCommand takePhotoCommand;

        private bool validateButtonIsEnabled;
        private bool navHistoryWasEmpty;
        private VisitItem visitItem;
        private ICollection<VisitorPicture> visitorPictures = new Collection<VisitorPicture>();        
        
        private StorageFile imageToCrop;
        private StorageFile imageCropped;
        private bool isImageControlVisible;    

        /// <summary>
        /// Default constructor.
        /// </summary>
        public VMVisitDetailPage(
            INavigationService navService, 
            IMyCompanyClient clientService,
            IMessageService messageService, 
            ITilesService tilesService, 
            ISampleDataService sampleDataService, 
            IStorageService storageService,
            IDispatcherService dispatcherService)
        {
            this.navService = navService;
            this.clientService = clientService;
            this.messageService = messageService;
            this.tilesService = tilesService;
            this.sampleDataService = sampleDataService;
            this.storageService = storageService;
            this.dispatcherService = dispatcherService;

            InitializeCommands();
            if (base.IsInDesignMode)
            {
                InitializeMockedData();
            }
        }

        /// <summary>
        /// This property will be true if navigation history is empty.
        /// </summary>
        public bool NavHistoryWasEmpty
        {
            get { return this.navHistoryWasEmpty; }
            set
            {
                this.navHistoryWasEmpty = value;
                InitializeCommands();
            }
        }

        /// <summary>
        /// Validate button enabled.
        /// </summary>
        public bool ValidateButtonIsEnabled
        {
            get { return this.validateButtonIsEnabled; }
            set
            {
                this.validateButtonIsEnabled = value;
                RaisePropertyChanged(() => ValidateButtonIsEnabled);
            }
        }

        /// <summary>
        /// Visit Item.
        /// </summary>
        public VisitItem Visit
        {
            get { return this.visitItem; }
            set
            {
                this.visitItem = value;
                base.RaisePropertyChanged(() => Visit);
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
                if (this.imageToCrop != null)
                {
                    // Add the first picture.
                    AddPicture(PictureType.Big);
                    RaisePropertyChanged(() => ImageToCrop);
                    // Set the crop control visible.
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
                if (this.imageCropped != null)
                {
                    // Add the second picture cropped.
                    AddPicture(PictureType.Small);
                    // Hide the crop control.
                    IsImageControlVisible = false;
                }
            }
        }

        /// <summary>
        /// Crop control Visibility.
        /// </summary>
        public bool IsImageControlVisible
        {
            get { return this.isImageControlVisible; }
            set
            {
                this.isImageControlVisible = value;
                RaisePropertyChanged(() => IsImageControlVisible);
            }
        }

        /// <summary>
        /// Take Photo Command.
        /// </summary>
        public ICommand TakePhotoCommand
        {
            get { return this.takePhotoCommand; }
        }


        /// <summary>
        /// Navigate Back Command.
        /// </summary>
        public ICommand NavigateBackCommand
        {
            get { return this.navigateBackCommand; }
        }

        /// <summary>
        /// Pin the Visit Command.
        /// </summary>
        public ICommand PinVisitCommand
        {
            get { return this.pinVisitCommand; }
        }

        /// <summary>
        /// Validate the Visit Command.
        /// </summary>
        public ICommand ValidateVisitCommand
        {
            get { return this.validateVisitCommand; }
        }

        
        /// <summary>
        /// Initialize Data Function.
        /// </summary>
        public async Task InitializeData(int visitId = 0)
        {
            IsBusy = true;
            Visit result = await this.clientService.VisitService.Get(visitId, PictureType.All);

            if (result == null)
            {
                var loader = new ResourceLoader();
                messageService.ShowMessage(loader.GetString("ElementNotFound"), loader.GetString("Error"));
            }
            else
            {
                Visit = new VisitItem(result, 0);
                ValidateButtonIsEnabled = !this.visitItem.HasArrived;
            }

            IsBusy = false;
        }

        /// <summary>
        /// Initialize Mocked Data Function.
        /// </summary>
        private void InitializeMockedData()
        {
            Visit visitMocked = (this.sampleDataService.GetVisits(1)).First();
            VisitItem visitItem = new VisitItem(visitMocked, 0);
            Visit = visitItem;
        }

        private void InitializeCommands()
        {
            this.navigateBackCommand = new RelayCommand(NavigateBackExecute);
            this.pinVisitCommand = new RelayCommand<Rect>(PinVisitCommandExecute);
            this.validateVisitCommand = new RelayCommand(ValidateVisitExecute);
            this.takePhotoCommand = new RelayCommand(TakePhotoExecute);

        }

        private void NavigateBackExecute()
        {
            if (this.navHistoryWasEmpty)
            {
                this.navService.NavigateToMainPage();
            }
            else
            {
                this.navService.GoBack();
            }
        }

        private void ValidateVisitExecute()
        {
            this.clientService.VisitService.UpdateStatus(this.visitItem.VisitId, VisitStatus.Arrived);
            ValidateButtonIsEnabled = false;
        }

        private async void PinVisitCommandExecute(Rect rect)
        {
            var pictureUri = new Uri(PINLOGO_PATH);
            var pictureWideUri = new Uri(PINLOGOWIDE_PATH);

            string tileActivationArguments = string.Format("{0}{1}", VISIT, visitItem.VisitId);

            var loader = new ResourceLoader();
            var displayName = string.Format("{0}. {1} {2}", loader.GetString("AppName"), this.Visit.VisitorName, Visit.VisitFormattedDate);

            // Windows 8.1
            var tile = new SecondaryTile(visitItem.VisitId.ToString(), displayName, tileActivationArguments, pictureUri, TileSize.Wide310x150);
            tile.VisualElements.ForegroundText = ForegroundText.Light;
            tile.VisualElements.Wide310x150Logo = pictureWideUri;
            tile.VisualElements.ShowNameOnSquare150x150Logo = true;
            tile.VisualElements.ShowNameOnWide310x150Logo = true;

            await tile.RequestCreateForSelectionAsync(rect, Placement.Above);
            this.tilesService.UpdatePinTile(this.visitItem, tile);
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
            if ((pictureType == PictureType.Big) && (this.imageToCrop != null))
            {
                this.visitorPictures = new Collection<VisitorPicture>() {};

                picture = new VisitorPicture
              {
                  Content = await storageService.FileToByte(this.imageToCrop),
                  PictureType = pictureType
              };

                this.visitorPictures.Add(picture);
            }


            // If picture type is small, creates a new Visitor Picture using the image croppped attribute.
            else if ((pictureType == PictureType.Small) && ((this.imageCropped != null)))
            {
                await dispatcherService.InvokeUI( async () =>
                {
                    picture = new VisitorPicture
                    {
                        Content = await storageService.FileToByte(this.imageCropped),
                        PictureType = pictureType
                    };
                    this.visitorPictures.Add(picture);
                    IsBusy = true;


                    var newVisitorPictures = this.visitorPictures.ToList();

                    foreach (var visitorPicture in newVisitorPictures)
                    {
                        visitorPicture.VisitorId = visitItem.VisitorId;
                    }

                    await this.clientService.VisitorPictureService.AddOrUpdatePictures(visitorPictures);

                    var message = new VisitorPicturesChanged(this.visitorPictures);
                    this.MessengerInstance.Send<VisitorPicturesChanged>(message);

                    this.Visit.ChangePhotos(this.visitorPictures);
                    base.RaisePropertyChanged(() => Visit);
                    IsBusy = false;
                });
            }

        }

#if WINDOWS_APP
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
                IsImageControlVisible = true;
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
#endif

#if WINDOWS_PHONE_APP
        private void TakePhotoExecute()
        {
            IsImageControlVisible = true;
        }

        private void CropImage()
        {
            IsImageControlVisible = false;
            var crop = CreateAutomaticallyCroppedFile();
            crop.ContinueWith(task =>
            {
                StorageFile croppedFile = task.Result;
                ImageCropped = croppedFile;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
#endif

    }
}
