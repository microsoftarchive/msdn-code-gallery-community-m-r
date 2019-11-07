// <copyright file="PlayByPlay.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PlayByPlay.cs                     
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
    using System.ComponentModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines the PlayByPlay class.
    /// </summary>
#if !SILVERLIGHT
    [Serializable]
#endif
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
    public class PlayByPlay : Marker
    {
        private readonly long offset;

        public PlayByPlay(long offset)
        {
            this.offset = offset;
        }

        public long TimeWithOffset
        {
            get
            {
                return this.Time + this.offset;
            }
        }

        public string EventId { get; set; }

        [DataMember]
        public Guid TimelineId { get; set; }
    }
}
