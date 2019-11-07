// <copyright file="InkComment.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: InkComment.cs                     
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
    using System.Windows.Ink;

    /// <summary>
    /// Specifies the properties of the ink comment.
    /// </summary>
    public class InkComment : Comment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InkComment"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the comment.</param>
        public InkComment(Guid id) : base(id)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InkComment"/> class.
        /// </summary>
        public InkComment() 
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the ink comment strokes.
        /// </summary>
        /// <value>The ink comment strokes.</value>
        public StrokeCollection InkCommentStrokes { get; set; }
    }
}
