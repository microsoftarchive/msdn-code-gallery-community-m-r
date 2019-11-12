// <copyright file="Track.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Track.cs                     
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
    /// A class that describes a track in the sequence.
    /// </summary>
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
#if !SILVERLIGHT
    [Serializable]
#endif
    public class Track : Container
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Track"/> class.
        /// </summary>
        public Track()
        {
            this.Shots = new ShotCollection();
        }

        /// <summary>
        /// Gets or sets the track number.
        /// </summary>
        /// <value>The track number.</value>
        [DataMember]
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the track type.
        /// </summary>
        /// <value>The track type.</value>
        [DataMember]
        public string TrackType { get; set; }

        /// <summary>
        /// Gets or sets the track volume.
        /// </summary>
        /// <value>The track volume.</value>
        [DataMember]
        public double Volume { get; set; }

        [DataMember]
        public double Balance { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ShotCollection"/> for this track.
        /// </summary>
        /// <value>The collection of shots of the track.</value>
        [DataMember]
        public ShotCollection Shots { get; set; }
    }
}