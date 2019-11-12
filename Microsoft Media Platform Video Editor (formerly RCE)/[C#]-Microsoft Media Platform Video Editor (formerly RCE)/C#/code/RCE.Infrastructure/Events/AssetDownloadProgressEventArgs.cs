// <copyright file="AssetDownloadProgressEventArgs.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: AssetDownloadProgressEventArgs.cs                     
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
    /// Payload for <see cref="DownloadProgressChangedEvent"/>.
    /// </summary>
    public class AssetDownloadProgressEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetDownloadProgressEventArgs"/> class.
        /// </summary>
        /// <param name="element">The <see cref="TimelineElement"/>.</param>
        /// <param name="progress">The progress.</param>
        /// /// <param name="offset">The offset.</param>
        public AssetDownloadProgressEventArgs(TimelineElement element, double progress, double offset)
        {
            this.Element = element;
            this.Progress = progress;
            this.Offset = offset;
        }

        /// <summary>
        /// Gets the element.
        /// </summary>
        /// <value>The element.</value>
        public TimelineElement Element { get; private set; }

        /// <summary>
        /// Gets the progress.
        /// </summary>
        /// <value>The progress.</value>
        public double Progress { get; private set; }

        /// <summary>
        /// Gets the offset.
        /// </summary>
        /// <value>The offset.</value>
        public double Offset { get; private set; }
    }
}
