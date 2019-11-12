using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace PhotoUploader
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var cameraCaptureTask = new CameraCaptureTask();
            cameraCaptureTask.Completed += cameraCaptureTask_Completed;
            cameraCaptureTask.Show();
        }

        private async void cameraCaptureTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                await UploadPhoto(e.ChosenPhoto);
            }
        }

        private async Task<Uri> GetUploadUriAsync()
        {
            var request = (HttpWebRequest)WebRequest.Create("http://build2014demo.azurewebsites.net/Home/GetRandomBlobUploadUrl?randomguid=" + Guid.NewGuid().ToString());
            using (var response = (HttpWebResponse)(await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null)))
            {
                using (var body = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(body))
                    {
                        return new Uri(await reader.ReadToEndAsync());
                    }
                }
            }
        }

        private async Task UploadPhoto(Stream stream)
        {
            var uri = await GetUploadUriAsync();
            var blob = new CloudBlockBlob(uri);
            await blob.UploadFromStreamAsync(stream);
        }
    }
}