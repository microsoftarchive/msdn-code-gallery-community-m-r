// <copyright file="AudioAssetInOut.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AudioAssetInOut.cs                     
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

    public class AudioAssetInOut : AudioAsset, ISubClipAsset
    {
        private double inPosition;

        private double outPosition;

        public AudioAssetInOut(AudioAsset audioAsset)
        {
            this.AudioAsset = audioAsset;
            this.Id = Guid.NewGuid();
            this.CMSId = audioAsset.CMSId;
            this.AzureId = audioAsset.AzureId;
            this.ArchiveURL = audioAsset.ArchiveURL;
            this.ProviderUri = audioAsset.ProviderUri;
            this.Title = audioAsset.Title;
            this.Source = audioAsset.Source;
            this.ResourceType = audioAsset.ResourceType;
            this.Metadata = audioAsset.Metadata;
        }

        /// <summary>
        /// Gets or sets the value of InPosition in seconds from the begining of the audio.
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
        /// Gets or sets the value of OutPosition in seconds from the begining of the audio.
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
        /// Gets the audio asset associated with the Audio In Out asset.
        /// </summary>
        /// <value>The audio asset associated with the Audio In Out asset.</value>
        public AudioAsset AudioAsset { get; private set; }

        public double SubClipDuration
        {
            get
            {
                return this.OutPosition - this.InPosition;
            }
        }
    }
}
