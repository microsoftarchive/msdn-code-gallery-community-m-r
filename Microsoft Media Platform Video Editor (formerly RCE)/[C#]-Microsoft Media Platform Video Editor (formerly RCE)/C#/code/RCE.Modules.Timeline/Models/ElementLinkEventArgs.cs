// <copyright file="ElementLinkEventArgs.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ElementLinkEventArgs.cs                     
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
    using RCE.Infrastructure.Models;

    /// <summary>
    /// Event args that contains an element used to show/hide links.
    /// </summary>
    public class ElementLinkEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElementLinkEventArgs"/> class.
        /// </summary>
        /// <param name="element">The timeline element.</param>
        public ElementLinkEventArgs(TimelineElement element)
        {
            this.Element = element;
        }

        /// <summary>
        /// Gets the timeline element.
        /// </summary>
        /// <value>The timeline element.</value>
        public TimelineElement Element { get; private set; }
    }
}
