// <copyright file="DropPayload.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DropPayload.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.DragDrop
{
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// Payload for drag and drop.
    /// </summary>
    public class DropPayload
    {
        /// <summary>
        /// Gets or sets the dragged item.
        /// </summary>
        /// <value>The dragged item.</value>
        public object DraggedItem { get; set; }

        /// <summary>
        /// Gets or sets the relative position of the dragged item.
        /// </summary>
        /// <value>The relative position.</value>
        public Point RelativePosition { get; set; }

        /// <summary>
        /// Gets or sets the mouse event args.
        /// </summary>
        /// <value>The mouse event args.</value>
        public MouseEventArgs MouseEventArgs { get; set; }

        /// <summary>
        /// Gets or sets the drop data.
        /// </summary>
        /// <value>The drop data.</value>
        public object DropData { get; set; }

        /// <summary>
        /// Gets or sets the custom data.
        /// </summary>
        /// <value>The custom data.</value>
        public object CustomData { get; set; }
    }
}