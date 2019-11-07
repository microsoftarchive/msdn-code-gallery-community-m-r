// <copyright file="DownloaderManager.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DownloaderManager.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Services
{
    using System;
    using System.IO;
    using System.Net;

    using RCE.Infrastructure.Models;

    public class DownloaderManager
    {
        public event EventHandler<DownloaderManagerEventArgs> DownloadCompleted;

        public virtual void DownloadManifestAsync(Uri manifestUri, bool forceNewDownload, CookieContainer cookies)
        {
            if (forceNewDownload)
            {
                string separator = "&";
                if (manifestUri.AbsoluteUri.IndexOf("?", StringComparison.OrdinalIgnoreCase) < 0)
                {
                    separator = "?";
                }

                string newUriString = string.Concat(manifestUri.AbsoluteUri, separator, "ignore=", Guid.NewGuid());
                manifestUri = new Uri(newUriString);
            }

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(manifestUri);

            if (cookies != null)
            {
                request.CookieContainer = cookies;
            }

            request.BeginGetResponse(this.DownloadManifestCallback, request);
        }

        public virtual void DownloadManifestAsync(Uri manifestUri, bool forceNewDownload)
        {
            this.DownloadManifestAsync(manifestUri, forceNewDownload, null);
        }

        private void DownloadManifestCallback(IAsyncResult asyncResult)
        {
            HttpWebRequest request = (HttpWebRequest)asyncResult.AsyncState;
            WebResponse response = request.EndGetResponse(asyncResult);

            Stream stream = response.GetResponseStream();

            int indexOfIgnore = request.RequestUri.AbsoluteUri.IndexOf("?ignore");
            indexOfIgnore = indexOfIgnore != -1 ? indexOfIgnore : request.RequestUri.AbsoluteUri.IndexOf("&ignore");

            string originalUri;

            // passing absolute path to remove "?ignore=" in case new download was forced
            if (indexOfIgnore != -1)
            {
                originalUri = request.RequestUri.AbsoluteUri.Remove(
                    indexOfIgnore, request.RequestUri.AbsoluteUri.Length - indexOfIgnore);
            }
            else
            {
                originalUri = request.RequestUri.AbsoluteUri;
            }

            this.OnDownloadManifestCompleted(stream, new Uri(originalUri));
        }

        private void OnDownloadManifestCompleted(Stream stream, Uri uri)
        {
            EventHandler<DownloaderManagerEventArgs> handler = this.DownloadCompleted;
            if (handler != null)
            {
                handler(this, new DownloaderManagerEventArgs(stream, uri));
            }
        }
    }
}