// <copyright file="AudioItem.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AudioItem.cs                     
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

    /// <summary>
    /// A class that represents an Audio item.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
    public class AudioItem : MediaItem
    {
        /// <summary>
        /// Gets or sets the duration of the Audio.
        /// </summary>
        /// <value>The duration of the audio.</value>
        [DataMember]
        public double? Duration { get; set; }

        [DataMember]
        public bool IsStereo { get; set; }

        [DataMember]
        public string ArchiveURL { get; set; }
    }
}