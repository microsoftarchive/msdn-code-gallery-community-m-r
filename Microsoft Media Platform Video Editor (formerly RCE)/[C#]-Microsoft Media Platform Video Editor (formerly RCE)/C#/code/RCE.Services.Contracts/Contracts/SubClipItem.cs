// <copyright file="SubClipItem.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SubClipItem.cs                     
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

#if !SILVERLIGHT
    [Serializable]
#endif
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
    public class SubClipItem : MediaItem
    {
        public SubClipItem()
        {
            this.SequenceAudioStreams = new List<AudioStreamInfo>();
        }

        [DataMember]
        public double InPosition { get; set; }

        [DataMember]
        public double OutPosition { get; set; }

        [DataMember]
        public IList<AudioStreamInfo> SequenceAudioStreams { get; set; }

        [DataMember]
        public string SequenceVideoStream { get; set; }

        [DataMember]
        public Item RelatedItem { get; set; }
    }
}
