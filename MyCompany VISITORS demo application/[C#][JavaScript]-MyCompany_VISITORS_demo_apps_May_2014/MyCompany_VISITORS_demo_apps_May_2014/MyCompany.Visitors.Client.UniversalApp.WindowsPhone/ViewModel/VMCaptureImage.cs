using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Enumeration;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Media.Devices;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace MyCompany.Visitors.Client.UniversalApp.WindowsPhone.ViewModel
{
    public class VMCaptureImage : ViewModelBase
    {

        private MediaCapture mediaCapture;
        private ImageEncodingProperties imgEncodingProperties;
        private StorageFile capturedImage;

        private RelayCommand startCameraCommand;        
        private RelayCommand captureImageCommand;

        private bool isCaptureEnabled = true;
        private bool isPreviewing;

        /// <summary>
        /// Storaged File to Crop.
        /// </summary>
        public StorageFile CapturedImage
        {
            get { return this.capturedImage; }
            set
            {
                this.capturedImage = value;
                RaisePropertyChanged(() => CapturedImage);
            }
        }

        /// <summary>
        /// Controls if capture button is enabled
        /// </summary>
        public bool IsCaptureEnabled
        {
            get { return isCaptureEnabled; }
            set 
            {
                isCaptureEnabled = value;
                RaisePropertyChanged(() => IsCaptureEnabled);
            }
        }

        public VMCaptureImage()
        {
            InitializeCommands();            
        }

        /// <summary>
        /// Start camera command.
        /// </summary>
        public ICommand StartCameraCommand
        {
            get { return this.startCameraCommand; }
        }

        /// <summary>
        /// Confirm command.
        /// </summary>
        public ICommand CaptureImageCommand
        {
            get { return this.captureImageCommand; }
        }

        public async Task<MediaCapture> Initialize()
        {
            var cameraDeviceInfos = (await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture)).Where(d => d.IsEnabled && d.EnclosureLocation != null);

            var frontCamDeviceInfo = cameraDeviceInfos.FirstOrDefault(d => d.EnclosureLocation.Panel == Panel.Front);

            var mediaCaptureInitialization = new MediaCaptureInitializationSettings
            {
                PhotoCaptureSource = PhotoCaptureSource.Photo,
                AudioDeviceId = string.Empty,
                VideoDeviceId = frontCamDeviceInfo.Id,                 
            };

            mediaCapture = new MediaCapture();
            await mediaCapture.InitializeAsync(mediaCaptureInitialization);
            mediaCapture.VideoDeviceController.PrimaryUse = CaptureUse.Photo;           

            mediaCapture.SetPreviewRotation(VideoRotation.Clockwise270Degrees);
            mediaCapture.SetRecordRotation(VideoRotation.Clockwise270Degrees);

            // Create photo encoding properties as JPEG and set the size that should be used for photo capturing
            imgEncodingProperties = ImageEncodingProperties.CreateJpeg();
            imgEncodingProperties.Height = 480;
            imgEncodingProperties.Width = 640;


            return mediaCapture;
        }

        public async void StartPreview()
        {   
            // Start Preview stream
            if (mediaCapture == null)
            {
                await Initialize();
            }
            if (!isPreviewing) 
            {
                await mediaCapture.StartPreviewAsync();
                isPreviewing = true;
            }
        }

        public async void CaptureImage()
        {
            IsCaptureEnabled = false;

            // Create new unique file in the pictures library and capture photo into it
            string desiredName = "visit_photo.jpg";
            var photoStorageFile = await KnownFolders.PicturesLibrary.CreateFileAsync(desiredName, CreationCollisionOption.ReplaceExisting);            
            await mediaCapture.CapturePhotoToStorageFileAsync(imgEncodingProperties, photoStorageFile);

            if (photoStorageFile != null)
            {
                using (IRandomAccessStream sourceStream = await photoStorageFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(sourceStream);

                    using (InMemoryRandomAccessStream outputStream = new InMemoryRandomAccessStream())
                    {
                        BitmapEncoder encoder = await BitmapEncoder.CreateForTranscodingAsync(outputStream, decoder);
                    
                        encoder.BitmapTransform.Rotation = BitmapRotation.Clockwise270Degrees;

                        await encoder.FlushAsync();
                        outputStream.Seek(0);
                        sourceStream.Seek(0);

                        sourceStream.Size = 0;
                        await RandomAccessStream.CopyAsync(outputStream, sourceStream);
                        sourceStream.Dispose();
                        outputStream.Dispose();
                    }
                }
            }
            
            CapturedImage = photoStorageFile;

            IsCaptureEnabled = true;
        }

        public async Task Dispose()
        {
            if (mediaCapture != null)
            {
                if (isPreviewing) 
                {
                    await mediaCapture.StopPreviewAsync();
                    isPreviewing = false;
                }
                
                mediaCapture.Dispose();
                mediaCapture = null;
            }
        }

        private void InitializeCommands()
        {
            this.startCameraCommand = new RelayCommand(StartPreview);            
            this.captureImageCommand = new RelayCommand(CaptureImage);            
        }

    }
}

