// <copyright file="ElementPositionChangeEventArgs.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ElementPositionChangeEventArgs.cs                     
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
    using System;
    using Infrastructure.Models;
    using SMPTETimecode;

    /// <summary>
    /// Event args that contains element position changes.
    /// </summary>
    public class ElementPositionChangeEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the position type of the change.
        /// </summary>
        /// <value>The <seealso cref="ElementPositionType"/> position type.</value>
        public ElementPositionType PositionType { get; set; }

        /// <summary>
        /// Gets or sets the new position of the element.
        /// </summary>
        /// <value>A timecode that represents the new position of the element.</value>
        public TimeCode NewPosition { get; set; }
    }
}
