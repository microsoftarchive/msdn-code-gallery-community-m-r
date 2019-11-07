// <copyright file="VideoAsset.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: VideoAsset.cs                     
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

    using SMPTETimecode;

    /// <summary>
    /// A class that represents a video asset.
    /// </summary>
    public class VideoAsset : Asset
    {
        /// <summary>
        /// The video asset duration.
        /// </summary>
        private TimeCode duration;

        private bool isStereo;

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoAsset"/> class.
        /// </summary>
        public VideoAsset()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoAsset"/> class.
        /// </summary>
        /// <param name="id">The id of the asset.</param>
        public VideoAsset(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the duration of the Video.
        /// </summary>
        /// <value>The duration of the video.</value>
        public virtual TimeCode Duration
        {
            get
            {
                return this.duration;
            }

            set 
            { 
                this.duration = value;
                this.OnPropertyChanged("Duration");
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="SmpteFrameRate"/> of the Video.
        /// </summary>
        /// <value>The frame rate of the video.</value>
        public virtual SmpteFrameRate FrameRate { get; set; }

        /// <summary>
        /// Gets or sets the height of the Video.
        /// </summary>
        /// <value>The height of the video.</value>
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the width of the Video.
        /// </summary>
        /// <value>The width of the video.</value>
        public int? Width { get; set; }

        /// <summary>
        /// Gets or sets the source of the Video Thumbnail.
        /// </summary>
        /// <value>The source of the thumbnail.</value>
        public string ThumbnailSource { get; set; }

        public bool IsStereo
        {
            get
            {
                return this.isStereo;
            }

            set
            {
                this.isStereo = value;
                this.OnPropertyChanged("IsStereo");
            }
        }

        public override string Summary
        {
            get
            {
                return string.Format("{0} ({1})", this.Title, this.Duration);
            }
        }

        public string ArchiveURL { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
        /// </summary>
        /// <returns> A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
        public override string ToString()
        {
            return "Video";
        }

        public AudioAsset ConvertToAudioAsset()
        {
            AudioAsset audioAsset = new AudioAsset();
            audioAsset.IsStereo = this.IsStereo;
            audioAsset.DurationInSeconds = this.Duration.TotalSeconds;
            audioAsset.ResourceType = this.ResourceType;
            audioAsset.ProviderUri = this.ProviderUri;
            audioAsset.Source = this.Source;
            audioAsset.Title = this.Title;

            return audioAsset;
        }
    }
}