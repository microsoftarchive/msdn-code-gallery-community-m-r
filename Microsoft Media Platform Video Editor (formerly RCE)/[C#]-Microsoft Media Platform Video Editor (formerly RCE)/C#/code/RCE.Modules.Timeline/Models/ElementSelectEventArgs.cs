// <copyright file="ElementSelectEventArgs.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ElementSelectEventArgs.cs                     
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
    /// Event args that contains element selection data.
    /// </summary>
    public class ElementSelectEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the selected element.
        /// </summary>
        /// <value>The selected element.</value>
        public TimelineElement Element { get; set; }

        /// <summary>
        /// Gets or sets the position of the selected element. 
        /// </summary>
        /// <value>The position of the current selected element.</value>
        public TimeCode Position { get; set; }
    }
}
