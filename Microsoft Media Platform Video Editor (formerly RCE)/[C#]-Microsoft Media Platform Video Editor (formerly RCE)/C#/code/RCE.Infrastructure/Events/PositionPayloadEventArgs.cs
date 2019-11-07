// <copyright file="PositionPayloadEventArgs.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PositionPayloadEventArgs.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Events
{
    using System;
    using RCE.Infrastructure.Models;

    /// <summary>
    /// Payload for <see cref="PositionDoubleClickedEvent"/> and <see cref="PlayheadMovedEvent"/>.
    /// </summary>
    public class PositionPayloadEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PositionPayloadEventArgs"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public PositionPayloadEventArgs(TimeSpan position, CommentMode source)
        {
            this.Position = position;
            this.Source = source;
        }

        public PositionPayloadEventArgs(TimeSpan position)
        {
            this.Position = position;
        }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>The position.</value>
        public TimeSpan Position { get; private set; }

        public CommentMode Source { get; set; }
    }
}
