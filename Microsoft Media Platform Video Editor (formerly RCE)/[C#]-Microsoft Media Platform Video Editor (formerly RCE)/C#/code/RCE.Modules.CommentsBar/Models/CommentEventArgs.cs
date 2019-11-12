// <copyright file="CommentEventArgs.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentEventArgs.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.CommentsBar
{
    using System;
    using Infrastructure.Models;

    /// <summary>
    /// Event argument to handle the action on any comment.
    /// </summary>
    public class CommentEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the action on comment.
        /// </summary>
        /// <value>The <see cref="CommentAction"/>.</value>
        public CommentAction Action { get; set; }

        /// <summary>
        /// Gets or sets the original comment that got updated.
        /// </summary>
        /// <value>The original comment.</value>
        public Comment OriginalComment { get; set; }

        /// <summary>
        /// Gets or sets the new value of the comment text.
        /// </summary>
        /// <value>The updated comment.</value>
        public string TextUpdated { get; set; }

        /// <summary>
        /// Gets or sets the updated mark in value.
        /// </summary>
        /// <value>The updated mark in value.</value>
        public double MarkInUpdated { get; set; }

        /// <summary>
        /// Gets or sets the updated mark out value.
        /// </summary>
        /// <value>The updated mark out.</value>
        public double MarkOutUpdated { get; set; }
    }
}
