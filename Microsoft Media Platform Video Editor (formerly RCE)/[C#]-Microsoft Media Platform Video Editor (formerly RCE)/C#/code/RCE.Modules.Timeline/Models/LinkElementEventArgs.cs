// <copyright file="LinkElementEventArgs.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: LinkElementEventArgs.cs                     
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

    /// <summary>
    /// Event args that contains linking data.
    /// </summary>
    public class LinkElementEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkElementEventArgs"/> class.
        /// </summary>
        /// <param name="element">The timeline element.</param>
        /// <param name="linkPosition">The link position.</param>
        public LinkElementEventArgs(TimelineElement element, LinkPosition linkPosition)
        {
            this.Element = element;
            this.LinkPosition = linkPosition;
        }

        /// <summary>
        /// Gets the timeline element being linked.
        /// </summary>
        /// <value>The timeline element that is going to be linked.</value>
        public TimelineElement Element { get; private set; }

        /// <summary>
        /// Gets the position of the link.
        /// </summary>
        /// <value>The link position being used to link the element.</value>
        public LinkPosition LinkPosition { get; private set; }
    }
}