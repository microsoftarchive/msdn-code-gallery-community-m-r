// <copyright file="SmoothStreamingVideoItem.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SmoothStreamingVideoItem.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// A class that represents a smooth streaming video item.
    /// </summary>
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
#if !SILVERLIGHT
    [Serializable]
#endif
    public class SmoothStreamingVideoItem : VideoItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmoothStreamingVideoItem"/> class.
        /// </summary>
        public SmoothStreamingVideoItem()
        {
            this.DataStreams = new List<string>();
            this.ExternalManifests = new List<Uri>();
            this.AudioStreams = new List<AudioStreamInfo>();
        }

        /// <summary>
        /// Gets or sets the start position of the Video.
        /// </summary>
        /// <value>The start position of the video.</value>
        [DataMember]
        public double StartPosition { get; set; }

        /// <summary>
        /// Gets or sets the data streams of the Video.
        /// </summary>
        /// <value>The data streams of the video.</value>
        [DataMember]
        public List<string> DataStreams { get; set; }

        [DataMember]
        public List<AudioStreamInfo> AudioStreams { get; set; }

        [DataMember]
        public List<string> VideoStreams { get; set; }

        /// <summary>
        /// Gets or sets the external manifests of the Video.
        /// </summary>
        /// <value>The external manifests of the video.</value>
        [DataMember]
        public List<Uri> ExternalManifests { get; set; }

        [DataMember]
        public string AudioStreamName { get; set; }

        [DataMember]
        public string VodUrl { get; set; }
    }
}