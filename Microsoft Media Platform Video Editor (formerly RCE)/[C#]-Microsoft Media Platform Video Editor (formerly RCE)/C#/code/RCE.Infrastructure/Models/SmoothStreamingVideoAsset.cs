// <copyright file="SmoothStreamingVideoAsset.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SmoothStreamingVideoAsset.cs                     
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
    using System.Linq;

    using RCE.Services.Contracts;

    /// <summary>
    /// A class that represents a smooth streaming video asset.
    /// </summary>
    public class SmoothStreamingVideoAsset : VideoAsset, IAdaptiveAsset
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmoothStreamingVideoAsset"/> class.
        /// </summary>
        public SmoothStreamingVideoAsset()
        {
            this.DataStreams = new List<string>();
            this.ExternalManifests = new List<Uri>();
            this.AudioStreams = new List<AudioStream>();
            this.VideoStreams = new List<string>();
            this.PlayByPlayMarkers = new List<PlayByPlay>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmoothStreamingVideoAsset"/> class.
        /// </summary>
        /// <param name="id">The id of the asset.</param>
        public SmoothStreamingVideoAsset(Guid id)
            : base(id)
        {
            this.DataStreams = new List<string>();
            this.ExternalManifests = new List<Uri>();
        }

        /// <summary>
        /// Gets or sets the start position of the Video.
        /// </summary>
        /// <value>The start position of the video.</value>
        public double StartPosition { get; set; }

        /// <summary>
        /// Gets or sets the data streams of the Video.
        /// </summary>
        /// <value>The data streams of the video.</value>
        public List<string> DataStreams { get; set; }

        /// <summary>
        /// Gets or sets the external manifests of the Video.
        /// </summary>
        /// <value>The external manifests of the video.</value>
        public List<Uri> ExternalManifests { get; set; }

        public List<AudioStream> AudioStreams { get; set; }

        public List<string> VideoStreams { get; set; }

        public List<PlayByPlay> PlayByPlayMarkers { get; set; }
        
        public double DurationInSeconds
        {
            get { return this.Duration.TotalSeconds; }
        }

        public Uri VodUri { get; set; }
        
        public SmoothStreamingAudioAsset GetSmoothStreamingAudioAsset(string audioStreamName)
        {
            var audioStream = this.AudioStreams.FirstOrDefault(a => a.Name == audioStreamName);
            if (audioStream == null)
            {
                if (string.IsNullOrEmpty(audioStreamName))
                {
                    audioStream = this.AudioStreams.FirstOrDefault();
                }
                else
                {
                    throw new ArgumentException(string.Format("Invalid audio stream name: {0}", audioStream));
                }
            }

            SmoothStreamingAudioAsset audioAsset = new SmoothStreamingAudioAsset();
            audioAsset.IsStereo = audioStream.IsStereo;
            audioAsset.AudioStreamName = audioStream.Name;
            audioAsset.DurationInSeconds = this.Duration.TotalSeconds;
            audioAsset.ResourceType = this.ResourceType;
            audioAsset.ProviderUri = this.ProviderUri;
            audioAsset.Source = this.Source;
            audioAsset.Title = string.IsNullOrEmpty(audioStream.Name) ? this.Title : string.Format("{0} ({1})", this.Title, audioStream.Name);
            audioAsset.StartPosition = this.StartPosition;
            audioAsset.AzureId = this.AzureId;
            audioAsset.ArchiveURL = this.ArchiveURL;
            audioAsset.CMSId = this.CMSId;
            return audioAsset;
        }
    }
}
