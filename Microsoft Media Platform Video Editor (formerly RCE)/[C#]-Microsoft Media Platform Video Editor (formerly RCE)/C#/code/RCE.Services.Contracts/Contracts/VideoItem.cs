// <copyright file="VideoItem.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: VideoItem.cs                     
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
    using System.Runtime.Serialization;
    using SMPTETimecode;

    /// <summary>
    /// A class that represents a video item.
    /// </summary>
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
#if !SILVERLIGHT
    [Serializable]
#endif
    public class VideoItem : MediaItem
    {
        /// <summary>
        /// Gets or sets the duration of the Video.
        /// </summary>
        /// <value>The duration of the video.</value>
        [DataMember]
        public double? Duration { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SmpteFrameRate"/> of the Video.
        /// </summary>
        /// <value>The frame rate of the video.</value>
        [DataMember]
        public SmpteFrameRate FrameRate { get; set; }

        /// <summary>
        /// Gets or sets the height of the Video.
        /// </summary>
        /// <value>The height of the video.</value>
        [DataMember]
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the width of the Video.
        /// </summary>
        /// <value>The width of the video.</value>
        [DataMember]
        public int? Width { get; set; }

        /// <summary>
        /// Gets or sets the source of the Video Thumbnail.
        /// </summary>
        /// <value>The source of the thumbnail.</value>
        [DataMember]
        public string ThumbnailSource { get; set; }

        [DataMember]
        public bool IsStereo { get; set; }

        [DataMember]
        public string ArchiveURL { get; set; }
    }
}
