//----------------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Microsoft Corporation">
//  Copyright 2011 Microsoft Corporation
// </copyright>
// Licensed under the MICROSOFT LIMITED PUBLIC LICENSE version 1.1 (the "License"); 
// You may not use this file except in compliance with the License. 
//---------------------------------------------------------------------------------------------------------------------------
namespace BlobUploader.Html5.Web.Controllers
{
    using System;
    using System.Configuration;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using BlobUploader.Html5.Web.Infrastructure;
    using BlobUploader.Html5.Web.Models;
    using BlobUploader.Html5.Web.Properties;
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;

    /// <summary>
    /// Controller for BlobUpload control.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Default Action for Home Controller.
        /// </summary>
        /// <returns>Default view for Home Controller</returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Prepares the meta data for file to be uploaded.
        /// </summary>
        /// <param name="blocksCount">Count of blocks to be uploaded.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fileSize">Size of the file.</param>
        /// <returns>True in JSON format to the view.</returns>
        [HttpPost]
        public ActionResult PrepareMetadata(int blocksCount, string fileName, long fileSize)
        {
            var container = CloudStorageAccount.Parse(ConfigurationManager.AppSettings[Constants.ConfigurationSectionKey]).CreateCloudBlobClient().GetContainerReference(Constants.ContainerName);
            container.CreateIfNotExist();
            var fileToUpload = new FileUploadModel()
                {
                    BlockCount = blocksCount,
                    FileName = fileName,
                    FileSize = fileSize,
                    BlockBlob = container.GetBlockBlobReference(fileName),
                    StartTime = DateTime.Now,
                    IsUploadCompleted = false,
                    UploadStatusMessage = string.Empty
                };
            Session.Add(Constants.FileAttributesSession, fileToUpload);
            return Json(true);
        }

        /// <summary>
        /// Uploads each block of file to be uploaded and puts block list in the end.
        /// </summary>
        /// <param name="id">The id of block to upload.</param>
        /// <returns>
        /// JSON message specifying status of operation.
        /// </returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UploadBlock(int id)
        {
            var request = Request.Files["Slice"];
            byte[] chunk = new byte[request.ContentLength];
            request.InputStream.Read(chunk, 0, Convert.ToInt32(request.ContentLength));
            if (Session[Constants.FileAttributesSession] != null)
            {
                var model = (FileUploadModel)Session[Constants.FileAttributesSession];
                using (var chunkStream = new MemoryStream(chunk))
                {
                    var blockId = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0:D4}", id)));
                    try
                    {
                        model.BlockBlob.PutBlock(blockId, chunkStream, null, new BlobRequestOptions() { RetryPolicy = RetryPolicies.Retry(3, TimeSpan.FromSeconds(10)) });
                    }
                    catch (StorageException e)
                    {
                        Session.Clear();
                        model.IsUploadCompleted = true;
                        model.UploadStatusMessage = string.Format(CultureInfo.CurrentCulture, Resources.FailedToUploadFileMessage, e.Message);
                        return Json(new { error = true, isLastBlock = false, message = model.UploadStatusMessage });
                    }
                }

                if (id == model.BlockCount)
                {
                    model.IsUploadCompleted = true;
                    bool errorInOperation = false;
                    try
                    {
                        var blockList = Enumerable.Range(1, (int)model.BlockCount).ToList<int>().ConvertAll(rangeElement => Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0:D4}", rangeElement))));
                        model.BlockBlob.PutBlockList(blockList);
                        var duration = DateTime.Now - model.StartTime;
                        float fileSizeInKb = model.FileSize / Constants.BytesPerKb;
                        string fileSizeMessage = fileSizeInKb > Constants.BytesPerKb ?
                            string.Concat((fileSizeInKb / Constants.BytesPerKb).ToString(CultureInfo.CurrentCulture), " MB") :
                            string.Concat(fileSizeInKb.ToString(CultureInfo.CurrentCulture), " KB");
                        model.UploadStatusMessage = string.Format(CultureInfo.CurrentCulture, Resources.FileUploadedMessage, fileSizeMessage, duration.TotalSeconds);
                    }
                    catch (StorageException e)
                    {
                        model.UploadStatusMessage = string.Format(CultureInfo.CurrentCulture, Resources.FailedToUploadFileMessage, e.Message);
                        errorInOperation = true;
                    }
                    finally
                    {
                        Session.Clear();
                    }

                    return Json(new { error = errorInOperation, isLastBlock = model.IsUploadCompleted, message = model.UploadStatusMessage });
                }
            }
            else
            {
                return Json(new { error = true, isLastBlock = false, message = string.Format(CultureInfo.CurrentCulture, Resources.FailedToUploadFileMessage, Resources.SessonExpired) });
            }

            return Json(new { error = false, isLastBlock = false, message = string.Empty });
        }
    }
}
