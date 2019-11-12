using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office365.SharePoint;
using System.IO;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.ApplicationModel.Resources;
using Microsoft.Office365.SharePoint.Extensions;

namespace MyShuttle.Client.UniversalApp.ConnectedServices
{
    public class InvoiceService
    {
        internal async Task<StorageFile> GetFile(int id)
        {
            File file = await ConnectedService.GetInvoiceAsync(id);

            if (file == null)
            {
                return null;
            }

            var streamFile = await DownloadFileAsync(file);
            
            return streamFile;
        }

        async Task<StorageFile> DownloadFileAsync(File file)
        {
            StorageFolder temporaryFolder = ApplicationData.Current.TemporaryFolder;

            StorageFile invoice =
                await temporaryFolder.CreateFileAsync(file.Name, 
                Windows.Storage.CreationCollisionOption.ReplaceExisting);

            var stream = await file.DownloadAsync();                

            using (var reader = new DataReader(stream.AsInputStream()))
            {
                await reader.LoadAsync((uint)stream.Length);
                var buffer = new byte[(int)stream.Length];
                reader.ReadBytes(buffer);
                await FileIO.WriteBytesAsync(invoice, buffer);
            }

            return invoice;
        }

    }
}