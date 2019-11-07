using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Threading.Tasks;
using Microsoft.Office365.SharePoint;
using Microsoft.Office365.OAuth;
using System.IO;

namespace MyShuttle.Client.iOS.Services
{
    public class InvoiceService
    {
        const string SharePointResourceId = "https://your_server.sharepoint.com/";
        const string SharePointServiceRoot = "https://your_server.sharepoint.com/_api/";

        internal async static Task<string> DownloadInvoiceAsync(int employeeId, UIViewController context)
        {
            var myFiles = await GetDefaultDocumentFiles(context);

            var invoiceFile = myFiles.FirstOrDefault(file => 
                file.Name.Equals(String.Format("invoice_{0}.pdf", employeeId)));

            var actualInvoiceFile = invoiceFile as Microsoft.Office365.SharePoint.File;

            if (actualInvoiceFile == null)
            {
                return null;
            }

            var invoiceStream = await actualInvoiceFile.DownloadAsync();
            var invoicePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                invoiceFile.Name);

            using (var invoiceFileStream = System.IO.File.Create(invoicePath))
            {
                await invoiceStream.CopyToAsync(invoiceFileStream);
            }

            return invoicePath;
        }

        public static async Task<IEnumerable<IFileSystemItem>> GetDefaultDocumentFiles(UIViewController context)
        {
            var client = await EnsureClientCreated(context);

            // Obtain files in default SharePoint folder
            var filesResults = await client.Files.ExecuteAsync();
            var files = filesResults.CurrentPage.OrderBy(e => e.Name);
            return files;
        }

        public static async Task<SharePointClient> EnsureClientCreated(UIViewController context)
        {
            Authenticator authenticator = new Authenticator(context);
            var authInfo = await authenticator.AuthenticateAsync(SharePointResourceId, ServiceIdentifierKind.Resource);

            // Create the SharePoint client proxy:
            return new SharePointClient(new Uri(SharePointServiceRoot), authInfo.GetAccessToken);
        }

        public static void SignOut(UIViewController context)
        {
            new Authenticator(context).ClearCache();
        }
    }
}