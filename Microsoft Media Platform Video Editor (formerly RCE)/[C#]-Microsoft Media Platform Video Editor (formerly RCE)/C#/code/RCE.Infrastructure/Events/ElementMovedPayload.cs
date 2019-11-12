// <copyright file="ElementMovedPayload.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ElementMovedPayload.cs                     
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
    using Models;
    using SMPTETimecode;

    /// <summary>
    /// Payload for <see cref="ElementMovedEvent"/>.
    /// </summary>
    public class ElementMovedPayload
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElementMovedPayload"/> class.
        /// </summary>
        /// <param name="element">The <see cref="TimelineElement"/>.</param>
        /// <param name="positionType">Type of the position.</param>
        /// <param name="oldPosition">The old position.</param>
        /// <param name="newPosition">The new position.</param>
        public ElementMovedPayload(TimelineElement element, ElementPositionType positionType, TimeCode oldPosition, TimeCode newPosition)
        {
            this.Element = element;
            this.PositionType = positionType;
            this.OldPosition = oldPosition;
            this.NewPosition = newPosition;
        }

        /// <summary>
        /// Gets the type of the position.
        /// </summary>
        /// <value>The type of the position.</value>
        public ElementPositionType PositionType { get; private set; }

        /// <summary>
        /// Gets the <see cref="TimelineElement"/>.
        /// </summary>
        /// <value>The <see cref="TimelineElement"/>.</value>
        public TimelineElement Element { get; private set; }

        /// <summary>
        /// Gets the old position.
        /// </summary>
        /// <value>The old position.</value>
        public TimeCode OldPosition { get; private set; }

        /// <summary>
        /// Gets the new position.
        /// </summary>
        /// <value>The new position.</value>
        public TimeCode NewPosition { get; private set; }
    }
}