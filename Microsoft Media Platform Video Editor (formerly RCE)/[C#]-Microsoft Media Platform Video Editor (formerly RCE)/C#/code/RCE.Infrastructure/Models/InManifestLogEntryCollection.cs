// <copyright file="InManifestLogEntryCollection.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: InManifestLogEntryCollection.cs                     
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
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Linq;

    using Microsoft.Practices.ServiceLocation;
    using Microsoft.SilverlightMediaFramework.Plugins;
    using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

    using RCE.Infrastructure.Services;

    public class InManifestLogEntryCollection : BaseLogEntryCollection
    {
        private readonly IConfigurationService configurationService;

        private readonly string markerEntryElementName;

        /// <summary>
        /// The smooth streaming media element.
        /// </summary>
        private IRCESmoothStreamingMediaPlugin rceMediaPlugin;

        /// <summary>
        /// Indicates whether the collection was disposed or not.
        /// </summary>
        private bool disposed;

        public InManifestLogEntryCollection(IConfigurationService configurationService)
            : base(
            ServiceLocator.Current.GetInstance<IEventDataParser<EventData>>(),
            ServiceLocator.Current.GetInstance<IEventDataParser<EventOffset>>())
        {
            this.configurationService = configurationService;
            this.markerEntryElementName = this.configurationService.GetParameterValue("LogEntryElementName");
        }

        public override void SetLogEntriesSource(object source)
        {
            var plugin = source as IRCESmoothStreamingMediaPlugin;

            if (plugin != null)
            {
                if (this.rceMediaPlugin != null)
                {
                    this.rceMediaPlugin.ManifestReady -= this.OnManifestReady;
                    this.rceMediaPlugin.StreamSelected -= this.OnStreamSelected;
                    this.rceMediaPlugin.DownloadStreamDataCompleted -= this.OnDownloadStreamDataCompleted;
                }

                this.rceMediaPlugin = plugin;
                this.rceMediaPlugin.ChunkDownloadStrategy = ChunkDownloadStrategy.AggressiveFromStart;
                this.rceMediaPlugin.ManifestReady += this.OnManifestReady;
                this.rceMediaPlugin.StreamSelected += this.OnStreamSelected;
                this.rceMediaPlugin.DownloadStreamDataCompleted += this.OnDownloadStreamDataCompleted;
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
                if (this.rceMediaPlugin != null)
                {
                    this.rceMediaPlugin.ManifestReady -= this.OnManifestReady;
                    this.rceMediaPlugin.StreamSelected -= this.OnStreamSelected;
                    this.rceMediaPlugin.DownloadStreamDataCompleted -= this.OnDownloadStreamDataCompleted;
                }

                this.disposed = true;
            }
        }

        private void OnManifestReady(IAdaptiveMediaPlugin obj)
        {
            if (this.rceMediaPlugin != null && this.rceMediaPlugin.CurrentSegment != null)
            {
                // Get currently selected streams
                List<IMediaStream> currentlySelectedStreams =
                    this.rceMediaPlugin.CurrentSegment.SelectedStreams.ToList();

                var selectedStreams = this.UseAllLogStreams
                                          ? this.rceMediaPlugin.CurrentSegment.AvailableStreams.Where(
                                              x => x.IsSparseStream).ToList()
                                          : this.rceMediaPlugin.CurrentSegment.AvailableStreams.Where(
                                              i =>
                                              this.LogStreams.Contains(i.Name) && !currentlySelectedStreams.Contains(i))
                                                .ToList();

                if (selectedStreams.Count > 0)
                {
                    selectedStreams.ForEach(currentlySelectedStreams.Add);
                    this.rceMediaPlugin.SetSegmentSelectedStreams(this.rceMediaPlugin.CurrentSegment, currentlySelectedStreams);
                }
            }
        }

        private void OnDownloadStreamDataCompleted(IAdaptiveMediaPlugin mediaPlugin, IMediaTrack track, IStreamDownloadResult result)
        {
            try
            {
                if (result != null && result.Stream != null)
                {
                    var data = new byte[result.Stream.Length];
                    result.Stream.Read(data, 0, data.Length);
                    result.Stream.Flush();

                    this.ParseTimelineEvent(data);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void OnStreamSelected(IAdaptiveMediaPlugin mediaPlugin, IMediaStream stream)
        {
            try
            {
                if ((stream.Type == StreamType.Binary || stream.Type == StreamType.Text) &&
                    stream.AvailableTracks.Count() > 0)
                {
                    IMediaTrack track = stream.AvailableTracks.First();
                    this.rceMediaPlugin.DownloadStreamData(track);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Parses the <paramref name="data"/>. Extracts the event data.
        /// </summary>
        /// <param name="data">The timeline data being parsed.</param>
        private void ParseTimelineEvent(byte[] data)
        {
            string eventData = System.Text.Encoding.UTF8.GetString(data, 0, data.Length);

            if (!string.IsNullOrEmpty(eventData))
            {
                XElement element = XElement.Parse(eventData);

                if (element.Name == this.markerEntryElementName)
                {
                    ParseMarkerEntry(element);
                }
            }
        }
    }
}
