// <copyright file="TrackType.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TrackType.cs                     
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
    /// <summary>
    /// Defines the range of track types that are available.
    /// </summary>
    public enum TrackType
    {
        /// <summary>
        /// The track is undefined.
        /// </summary>
        Undefined,

        /// <summary>
        /// The track is an Video track.
        /// </summary>
        Visual,

        /// <summary>
        /// The track is an Audio track.
        /// </summary>
        Audio,

        /// <summary>
        /// The track is an Title track.
        /// </summary>
        Overlay
    }
}