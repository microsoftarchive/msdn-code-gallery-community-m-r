// <copyright file="FullScreenModeEventArgs.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: FullScreenModeEventArgs.cs                     
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
    using System;

    /// <summary>
    /// Even arguments for full screen event.
    /// </summary>
    public class FullScreenModeEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FullScreenModeEventArgs"/> class.
        /// </summary>
        /// <param name="mode">The <see cref="FullScreenMode"/>.</param>
        public FullScreenModeEventArgs(FullScreenMode mode)
        {
            this.Mode = mode;
        }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The <see cref="FullScreenMode"/>.</value>
        public FullScreenMode Mode { get; set; }
    }
}