// <copyright file="ITimelineBarElementModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ITimelineBarElementModel.cs                     
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
    /// Common model interface for the timeline bar elements.
    /// </summary>
    public interface ITimelineBarElementModel
    {
        /// <summary>
        /// Occurs when the timeline bar element is updated.
        /// </summary>
        event EventHandler<EventArgs> TimelineBarElementUpdated;

        /// <summary>
        /// Ocurrs when the timeline bar element is being deleted.
        /// </summary>
        event EventHandler<EventArgs> Deleting;

        /// <summary>
        /// Gets the edit box.
        /// </summary>
        /// <value>The edit box associated with the timeline bar element.</value>
        object EditBox { get; }

        /// <summary>
        /// Gets the preview associated with the timeline bar element.
        /// </summary>
        /// <value>The preview associated with the timeline bar element.</value>
        object Preview { get; }

        /// <summary>
        /// Gets the position of the timeline bar element.
        /// </summary>
        /// <value>The current position of the timeline bar element.</value>
        double Position { get; }

        /// <summary>
        /// Gets the display box.
        /// </summary>
        /// <value>The display box associated with the timeline bar element.</value>
        object DisplayBox { get; }

        /// <summary>
        /// Sets the position of the timeline bar element.
        /// </summary>
        /// <param name="position">The new position.</param>
        void SetPosition(TimeSpan position);

        /// <summary>
        /// Refreshes the preview associated with the timeline bar element.
        /// </summary>
        /// <param name="refreshedWidth">The refreshed width.</param>
        void RefreshPreview(double refreshedWidth);

        /// <summary>
        /// Sets the element being added to the timeline bar.
        /// </summary>
        /// <param name="value">The element being added.</param>
        /// <param name="mode"></param>
        void SetElement(object value, CommentMode mode);

        T GetElement<T>() where T : class;
    }
}