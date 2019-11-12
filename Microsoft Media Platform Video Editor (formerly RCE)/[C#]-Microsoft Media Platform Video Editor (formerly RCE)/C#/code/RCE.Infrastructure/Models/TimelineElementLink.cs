// <copyright file="TimelineElementLink.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimelineElementLink.cs                     
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
    /// Specifies the properties of the link between two <see cref="TimelineElement"/>.
    /// </summary>
    public class TimelineElementLink
    {
        /// <summary>
        /// The unique identifier for the <see cref="TimelineElementLink"/>.
        /// </summary>
        private readonly Guid elementId;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineElementLink"/> class.
        /// </summary>
        /// <param name="elementId">The element id.</param>
        public TimelineElementLink(Guid elementId)
        {
            if (elementId == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException("elementId", "Invalid elementId");
            }

            this.elementId = elementId;
        }

        /// <summary>
        /// Gets the element id.
        /// </summary>
        /// <value>The element id.</value>
        public Guid ElementId
        {
            get
            {
                return this.elementId;
            }
        }

        /// <summary>
        /// Gets the next element id.
        /// </summary>
        /// <value>The next element id.</value>
        public Guid NextElementId { get; internal set; }

        /// <summary>
        /// Gets the previous element id.
        /// </summary>
        /// <value>The previous element id.</value>
        public Guid PreviousElementId { get; internal set; }
    }
}
