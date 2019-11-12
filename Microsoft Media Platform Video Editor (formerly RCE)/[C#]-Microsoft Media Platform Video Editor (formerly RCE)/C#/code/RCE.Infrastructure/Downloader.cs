// <copyright file="Downloader.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Downloader.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure
{
    using System;
    using System.Net;
    using Models;

    using RCE.Infrastructure.Services;

    using DownloadStringCompletedEventArgs = RCE.Infrastructure.Models.DownloadStringCompletedEventArgs;

    /// <summary>
    /// Downloader claas to download any item from the given <see cref="Uri"/>.
    /// </summary>
    public class Downloader
    {
        /// <summary>
        /// The <seealso cref="ILogger"/> to log the exception.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Downloader"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public Downloader(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Occurs when [download string completed].
        /// </summary>
        public event EventHandler<DownloadStringCompletedEventArgs> DownloadStringCompleted;

        /// <summary>
        /// Occurs when [open read async completed].
        /// </summary>
        public event EventHandler<OpenReadCompletedEventArgs> OpenReadCompleted;

        /// <summary>
        /// Downloads the string asynchronously.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="userState">State of the user.</param>
        public virtual void DownloadStringAsync(Uri address, object userState)
        {
            WebClient client = new WebClient();
            client.DownloadStringCompleted += (args, e) => this.OnDownloadStringCompleted(e);
            client.DownloadStringAsync(address, userState);
        }

        /// <summary>
        /// Downloads the string asynchronously.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="userState">State of the user.</param>
        public virtual void OpenReadAsync(Uri address, object userState)
        {
            WebClient client = new WebClient();
            client.OpenReadCompleted += (args, e) => this.OnOpenReadCompleted(e);
            client.OpenReadAsync(address, userState);
        }

        /// <summary>
        /// Raises the DownloadStringCompleted event.
        /// </summary>
        /// <param name="e">The <see cref="System.Net.DownloadStringCompletedEventArgs"/> instance containing the event data.</param>
        protected void OnDownloadStringCompleted(System.Net.DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                string message = UtilityHelper.FormatExceptionMessage(e.Error);

                this.logger.Log(message, "Downloader", 1, 0, TraceEventType.Error);
                return;
            }

            this.OnDownloadStringCompleted(new DownloadStringCompletedEventArgs(e));
        }

        /// <summary>
        /// Raises the DownloadStringCompleted event.
        /// </summary>
        /// <param name="e">The <see cref="DownloadStringCompletedEventArgs"/> instance containing the event data.</param>
        protected void OnDownloadStringCompleted(DownloadStringCompletedEventArgs e)
        {
            EventHandler<DownloadStringCompletedEventArgs> completed = this.DownloadStringCompleted;
            if (completed != null)
            {
                completed(this, e);
            }
        }

        /// <summary>
        /// Raises the OpenReadCompletedEventArgs event.
        /// </summary>
        /// <param name="e">The <see cref="OpenReadCompletedEventArgs"/> instance containing the event data.</param>
        private void OnOpenReadCompleted(OpenReadCompletedEventArgs e)
        {
            EventHandler<OpenReadCompletedEventArgs> completed = this.OpenReadCompleted;
            if (completed != null)
            {
                completed(this, e);
            }
        }
    }
}
