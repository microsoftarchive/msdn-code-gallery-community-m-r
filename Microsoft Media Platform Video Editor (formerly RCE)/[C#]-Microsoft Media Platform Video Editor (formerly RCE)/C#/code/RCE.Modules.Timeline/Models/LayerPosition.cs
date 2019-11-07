// <copyright file="LayerPosition.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: LayerPosition.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline
{
    using Infrastructure.Models;
    using SMPTETimecode;

    /// <summary>
    /// Defines a layer position.
    /// </summary>
    public class LayerPosition
    {
        /// <summary>
        /// Gets or sets the <seealso cref="TrackType"/>.
        /// </summary>
        /// <value>The layer type.</value>
        public TrackType LayerType { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        public TimeCode Position { get; set; }

        /// <summary>
        /// Gets or sets the track.
        /// </summary>
        /// <value>The track associated if any.</value>
        public Track Track { get; set; }
    }
}
