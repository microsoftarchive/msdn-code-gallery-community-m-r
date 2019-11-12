namespace MyCompany.Visitors.Client.UniversalApp.WindowsStore.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;    
    using System;
    using System.Windows.Input;
    using Windows.Graphics.Imaging;
    using Windows.Storage;
    using Windows.Storage.Streams;
    using MyCompany.Visitors.Client.UniversalApp.Services.Storage;    

    /// <summary>
    /// Crop Picture control ViewModel.
    /// </summary>
    public class VMCropPicture : ViewModelBase
    {
        private readonly IStorageService storageService;

        private byte[] originalImage;
        private StorageFile imageToCrop;
        private StorageFile imageCropped;
        private RelayCommand confirmCommand;
        private bool isEnabledConfirmButtom;

        private double selectedLeft, selectedTop, selectedWidth, selectedHeight;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public VMCropPicture(IStorageService storageService)
        {
            this.storageService = storageService;
            InitializeCommands();
        }

        /// <summary>
        /// Confirm command.
        /// </summary>
        public ICommand ConfirmCommand
        {
            get { return this.confirmCommand; }
        }

        /// <summary>
        /// Original image.
        /// </summary>
        public byte[] OriginalImage
        {
            get { return this.originalImage; }
            set
            {
                this.originalImage = value;
                RaisePropertyChanged(() => OriginalImage);
            }
        }

        /// <summary>
        /// Storaged File to Crop.
        /// </summary>
        public StorageFile ImageToCrop
        {
            get { return this.imageToCrop; }
            set
            {
                this.imageToCrop = value;
                LoadImage();
            }
        }

        /// <summary>
        /// Storaged File Cropped.
        /// </summary>
        public StorageFile ImageCropped
        {
            get { return this.imageCropped; }
            set
            {
                this.imageCropped = value;
                if(this.imageCropped != null)
                {
                    RaisePropertyChanged(() => ImageCropped);
                }
            }
        }

        /// <summary>
        /// Is enabled confirm button.
        /// </summary>
        public bool IsEnabledConfirmButtom
        {
            get { return this.isEnabledConfirmButtom; }
            set
            {
                this.isEnabledConfirmButtom = value;
                RaisePropertyChanged(() => IsEnabledConfirmButtom);
            }
        }

        /// <summary>
        /// X coordinate.
        /// </summary>
        public double SelectedLeft
        {
            get { return this.selectedLeft; }
            set
            {
                this.selectedLeft = value;
                RaisePropertyChanged(() => SelectedLeft);
            }
        }

        /// <summary>
        /// Y coordinate.
        /// </summary>
        public double SelectedTop
        {
            get { return this.selectedTop; }
            set
            {
                this.selectedTop = value;
                RaisePropertyChanged(() => SelectedTop);
            }
        }

        /// <summary>
        /// Width of selected region.
        /// </summary>
        public double SelectedWidth
        {
            get { return this.selectedWidth; }
            set
            {
                this.selectedWidth = value;
                RaisePropertyChanged(() => SelectedWidth);
            }
        }

        /// <summary>
        /// Height of selected region.
        /// </summary>
        public double SelectedHeight
        {
            get { return this.selectedHeight; }
            set
            {
                this.selectedHeight = value;
                RaisePropertyChanged(() => SelectedHeight);
            }
        }

        private void InitializeCommands()
        {
            this.confirmCommand = new RelayCommand(CropPicture);
        }

        private async void LoadImage()
        {
            OriginalImage = await this.storageService.FileToByte(this.imageToCrop);
        }

        private async void CropPicture()
        {
            IsEnabledConfirmButtom = false;

            StorageFile originalFile = this.imageToCrop;
            
            var folder = await StorageFolder.GetFolderFromPathAsync(originalFile.Path.Substring(0, originalFile.Path.LastIndexOf(@"\")));
            StorageFile croppedFile = await originalFile.CopyAsync(folder,string.Format("imageCropped.jpg"),NameCollisionOption.ReplaceExisting);

            using (IRandomAccessStream sourceStream = await originalFile.OpenAsync(FileAccessMode.Read))
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(sourceStream);

                using (var outputStream = new InMemoryRandomAccessStream())
                {
                    var encoder = await BitmapEncoder.CreateForTranscodingAsync(outputStream, decoder);

                    encoder.BitmapTransform.Bounds = new BitmapBounds()
                                                         {
                                                             X = (uint) Math.Floor(this.selectedLeft),
                                                             Y = (uint) Math.Floor(this.selectedTop),
                                                             Height = (uint) Math.Floor(this.selectedHeight),
                                                             Width = (uint) Math.Floor(this.selectedWidth)
                                                         };

                    await encoder.FlushAsync();

                    using (var stream = await croppedFile.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        await RandomAccessStream.CopyAndCloseAsync(outputStream.GetInputStreamAt(0), stream.GetOutputStreamAt(0));
                    }
                }
            }
            this.imageToCrop = null;
            ResetSelection();
            OriginalImage = null;
            ImageCropped = croppedFile;
        }

        private void ResetSelection()
        {
            this.selectedLeft = 0.0;
            this.selectedTop = 0.0;
            this.selectedHeight = 0.0;
            this.selectedWidth = 0.0;
        }

        /// <summary>
        /// Initialize data method.
        /// </summary>
        public void InitializeData()
        {
            ImageCropped = null;
            OriginalImage = null;
            ResetSelection();
        }
    }
}
