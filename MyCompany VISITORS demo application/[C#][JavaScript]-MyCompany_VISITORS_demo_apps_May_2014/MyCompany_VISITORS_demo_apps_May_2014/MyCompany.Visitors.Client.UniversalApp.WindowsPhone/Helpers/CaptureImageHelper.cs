using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Media.Capture;
using Windows.Media.Devices;
using Windows.Media.MediaProperties;
using Windows.Storage;

namespace MyCompany.Visitors.Client.UniversalApp.WindowsPhone.Helpers
{
    public class CaptureImageHelper
    {
        private MediaCapture mediaCapture;
        private ImageEncodingProperties imgEncodingProperties;

        public async Task<MediaCapture> Initialize()
        {
            
            var cameraDeviceInfos = (await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture)).Where(d => d.IsEnabled && d.EnclosureLocation != null);

            var frontCamDeviceInfo = cameraDeviceInfos.FirstOrDefault(d => d.EnclosureLocation.Panel == Panel.Front);

            var mediaCaptureInitialization  = new MediaCaptureInitializationSettings
            {
                PhotoCaptureSource = PhotoCaptureSource.Auto,
                AudioDeviceId = string.Empty,
                VideoDeviceId = frontCamDeviceInfo.Id
            };
            
            mediaCapture = new MediaCapture();
            await mediaCapture.InitializeAsync(mediaCaptureInitialization);
            mediaCapture.VideoDeviceController.PrimaryUse = CaptureUse.Photo;
            mediaCapture.SetPreviewRotation(VideoRotation.Clockwise270Degrees);

            // Create photo encoding properties as JPEG and set the size that should be used for photo capturing
            imgEncodingProperties = ImageEncodingProperties.CreateJpeg();
            //imgEncodingProperties.Width = 640;
            //imgEncodingProperties.Height = 480;

            return mediaCapture;
        }

        public async Task StartPreview()
        {
            // Start Preview stream
            await mediaCapture.StartPreviewAsync();
        }

        public async Task StopPreview()
        {
            // Stop Preview stream
            await mediaCapture.StopPreviewAsync();
        }

        public async Task<StorageFile> CapturePhoto(string desiredName = "visit_photo.jpg")
        {
            // Create new unique file in the pictures library and capture photo into it
            var photoStorageFile = await KnownFolders.PicturesLibrary.CreateFileAsync(desiredName, CreationCollisionOption.ReplaceExisting);
            await mediaCapture.CapturePhotoToStorageFileAsync(imgEncodingProperties, photoStorageFile);
            return photoStorageFile;
        }

        public void Dispose()
        {
            if (mediaCapture != null)
            {
                mediaCapture.Dispose();
                mediaCapture = null;
            }
        }
    }
}
