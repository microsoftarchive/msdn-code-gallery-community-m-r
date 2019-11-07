
namespace MyShuttle.WebJob.Services.SharePoint
{
    using Microsoft.SharePoint.Client;
    using System;
    using System.Configuration;
    using System.IO;

    public static class SharePointUploader
    {
        static MsOnlineClaimsHelper helper = null;

        public static void UploadFile(byte[] content, string fileName)
        {
            string webUrl = ConfigurationManager.AppSettings["SharePoint::webUrl"];
            string userName = ConfigurationManager.AppSettings["SharePoint::userName"];
            string password = ConfigurationManager.AppSettings["SharePoint::password"];

            helper = new MsOnlineClaimsHelper(userName, password, webUrl);

            using (var context = new ClientContext(webUrl))
            {
                context.ExecutingWebRequest += Context_ExecutingWebRequest;
                using (var stream = new MemoryStream(content))
                {
                    var list = context.Web.Lists.GetByTitle("Documents");
                    context.Load(list.RootFolder);
                    context.ExecuteQuery();
                    var fileUrl = String.Format("{0}/Invoices/{1}", list.RootFolder.ServerRelativeUrl, fileName);

                    Microsoft.SharePoint.Client.File.SaveBinaryDirect(context, fileUrl, stream, true);
                }
            }
        }

        private static void Context_ExecutingWebRequest(object sender, WebRequestEventArgs e)
        {
            e.WebRequestExecutor.WebRequest.CookieContainer = helper.CookieContainer;
        }

    }
}
