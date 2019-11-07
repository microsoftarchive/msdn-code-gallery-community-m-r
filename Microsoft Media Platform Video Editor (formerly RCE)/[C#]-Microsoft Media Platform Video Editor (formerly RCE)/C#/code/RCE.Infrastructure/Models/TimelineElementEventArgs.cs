// <copyright file="TimelineElementEventArgs.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimelineElementEventArgs.cs                     
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
    /// Even args when any element is edited in the time line module.
    /// </summary>
    public class TimelineElementEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineElementEventArgs"/> class.
        /// </summary>
        /// <param name="timelineElement">The timeline element.</param>
        public TimelineElementEventArgs(TimelineElement timelineElement)
        {
            this.Element = timelineElement;
        }

        /// <summary>
        /// Gets the element.
        /// </summary>
        /// <value>The element.</value>
        public TimelineElement Element { get; private set; }
    }
}