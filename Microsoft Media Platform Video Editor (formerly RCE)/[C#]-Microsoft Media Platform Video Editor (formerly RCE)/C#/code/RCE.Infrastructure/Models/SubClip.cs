// <copyright file="SubClip.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SubClip.cs                     
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
    using System.Collections.Generic;

    using SMPTETimecode;

    /// <summary>
    /// Specifies the In and Out scrub position of the <see cref="VideoAsset"/>.
    /// </summary>
    public class SubClip : VideoAsset
    {
        /// <summary>
        /// The in position of the scrubbed <see cref="VideoAsset"/>.
        /// </summary>
        private double inPosition = -1;

        /// <summary>
        /// The out position of the scrubbed <see cref="VideoAsset"/>.
        /// </summary>
        private double outPosition = -1;

        private AudioStream previewAudioStream;

        private string previewVideoCamera;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubClip"/> class.
        /// </summary>
        /// <param name="videoAsset">The video asset.</param>
        public SubClip(VideoAsset videoAsset)
        {
            this.VideoAsset = videoAsset;
            this.Id = videoAsset.Id;
            this.ProviderUri = videoAsset.ProviderUri;
            this.Title = videoAsset.Title;
            this.Source = videoAsset.Source;
            this.Height = videoAsset.Height;
            this.Width = videoAsset.Width;
            this.ResourceType = videoAsset.ResourceType;
            this.ThumbnailSource = videoAsset.ThumbnailSource;
            this.Metadata = videoAsset.Metadata;
            this.SequenceAudioStreams = new List<AudioStream>();
        }

        public string SequenceVideoCamera { get; set; }

        public IList<AudioStream> SequenceAudioStreams { get; set; }

        public AudioStream PreviewAudioStream
        {
            get
            {
                return this.previewAudioStream;
            }

            set
            {
                this.previewAudioStream = value;
                this.OnPropertyChanged("PreviewAudioStream");
            }
        }

        public string PreviewVideoCamera
        {
            get
            {
                return this.previewVideoCamera;
            }

            set
            {
                this.previewVideoCamera = value;
                this.OnPropertyChanged("PreviewVideoCamera");
            }
        }

        /// <summary>
        /// Gets the video asset associated with the Video In Out asset.
        /// </summary>
        /// <value>The video asset associated with the Video In Out asset.</value>
        public VideoAsset VideoAsset { get; private set; }

        /// <summary>
        /// Gets or sets the value of InPosition in seconds from the begining of the video.
        /// </summary>
        /// <value>InPosition value in second.</value>
        public double InPosition
        {
            get
            {
                return this.inPosition;
            }

            set
            {
                this.inPosition = value;
                this.OnPropertyChanged("SubClipDuration");
            }
        }

        /// <summary>
        /// Gets or sets the value of OutPosition in seconds from the begining of the video.
        /// </summary>
        /// <value>OutPosition value in second.</value>
        public double OutPosition
        {
            get
            {
                return this.outPosition;
            }

            set
            {
                this.outPosition = value;
                this.OnPropertyChanged("SubClipDuration");
            }
        }

        /// <summary>
        /// Gets or sets the duration of the Video.
        /// </summary>
        /// <value>The duration of the video.</value>
        public override TimeCode Duration
        {
            get
            {
                return this.VideoAsset.Duration;
            }

            set
            {
                this.VideoAsset.Duration = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="SmpteFrameRate"/> of the Video.
        /// </summary>
        /// <value>The frame rate of the video.</value>
        public override SmpteFrameRate FrameRate
        {
            get
            {
                return this.VideoAsset.FrameRate;
            }

            set
            {
                this.VideoAsset.FrameRate = value;
            }
        }

        public TimeCode SubClipDuration
        {
            get
            {
                return TimeCode.FromAbsoluteTime(this.OutPosition, this.VideoAsset.FrameRate) - TimeCode.FromAbsoluteTime(this.InPosition, this.VideoAsset.FrameRate);
            }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return "VideoInOut";
        }
    }
}
