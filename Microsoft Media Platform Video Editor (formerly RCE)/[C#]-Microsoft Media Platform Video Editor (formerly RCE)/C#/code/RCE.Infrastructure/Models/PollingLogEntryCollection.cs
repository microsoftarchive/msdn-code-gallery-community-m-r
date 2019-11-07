// <copyright file="PollingLogEntryCollection.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PollingLogEntryCollection.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Models
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Threading;
    using System.Xml.Linq;

    using Microsoft.Practices.ServiceLocation;

    using RCE.Infrastructure.Services;

    public class PollingLogEntryCollection : BaseLogEntryCollection
    {
        private const double DefaultPollingIntervalInSeconds = 10;
        
        private readonly IConfigurationService configurationService;

        private readonly Uri markersUrl;

        private readonly DispatcherTimer pollingTimer;

        private readonly DownloaderManager downloadManager;

        private readonly string markerEntryElementName;

        /// <summary>
        /// Indicates whether the collection was disposed or not.
        /// </summary>
        private bool disposed;

        public PollingLogEntryCollection(IEventDataParser<EventData> eventDataParser, IEventDataParser<EventOffset> eventDataOffsetParser, IConfigurationService configurationService)
            : base(eventDataParser, eventDataOffsetParser)
        {
            this.configurationService = configurationService;
            this.markerEntryElementName = this.configurationService.GetParameterValue("LogEntryElementName");
            double? pollingIntervalInSeconds =
                this.configurationService.GetParameterValueAsDouble("LogEntryPollingIntervalInSeconds");
            this.pollingTimer = new DispatcherTimer();
            this.pollingTimer.Interval = TimeSpan.FromSeconds(pollingIntervalInSeconds.HasValue ? pollingIntervalInSeconds.Value : DefaultPollingIntervalInSeconds);
            this.pollingTimer.Tick += this.PollingTimerTick;
            this.pollingTimer.Start();
            this.downloadManager = new DownloaderManager();
            this.downloadManager.DownloadCompleted += this.DownloadCompleted;

            string url = this.configurationService.GetParameterValue("LogEntrySourceUrl");

            if (!string.IsNullOrEmpty(url))
            {
                this.markersUrl = new Uri(url);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Indicates if Dispose is being called from the Dispose method.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.disposed)
            {
                this.downloadManager.DownloadCompleted -= this.DownloadCompleted;
                this.disposed = true;
            }
        }

        private void PollingTimerTick(object sender, EventArgs e)
        {
            this.pollingTimer.Stop();
            this.downloadManager.DownloadManifestAsync(this.markersUrl, true);
        }

        private void DownloadCompleted(object sender, DownloaderManagerEventArgs e)
        {
            Stream markersStream = e.Stream;

            XDocument document = XDocument.Load(markersStream);

            Deployment.Current.Dispatcher.BeginInvoke(
                () =>
                    {
                        document.Descendants(this.markerEntryElementName).ForEach(this.ParseMarkerEntry);
                        this.pollingTimer.Start();
                    });
        }
    }
}