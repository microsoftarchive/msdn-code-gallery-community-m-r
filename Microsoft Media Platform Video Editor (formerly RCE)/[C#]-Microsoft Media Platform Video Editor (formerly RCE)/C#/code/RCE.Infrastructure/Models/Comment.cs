// <copyright file="Comment.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Comment.cs                     
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
    /// Specifies the properties of the comment.
    /// </summary>
    public class Comment : Audit
    {
        /// <summary>
        /// Specifies the the text of the comment.
        /// </summary>
        private string text;

        /// <summary>
        /// Specifies the Mark in time of the comment.
        /// </summary>
        private double? markIn;

        /// <summary>
        /// Specifies the Mark out time of the comment.
        /// </summary>
        private double? markOut;

        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the comment.</param>
        public Comment(Guid id)
        {
            this.CommentId = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        public Comment()
        {
            this.CommentId = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the type of the comment.
        /// </summary>
        /// <value>The type of the comment.</value>
        public CommentType CommentType { get; set; }

        /// <summary>
        /// Gets the comment id.
        /// </summary>
        /// <value>The unique identifier for the comment.</value>
        public Guid CommentId { get; private set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The comment's text.</value>
        public string Text 
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
                this.OnPropertyChanged("Text");
            }
        }

        /// <summary>
        /// Gets or sets the mark in time.
        /// </summary>
        /// <value>The mark in time in seconds.</value>
        public double? MarkIn
        {
            get
            {
                return this.markIn;
            }

            set
            {
                this.markIn = value;
                this.OnPropertyChanged("MarkIn");
            }
        }

        /// <summary>
        /// Gets or sets the provider URI.
        /// </summary>
        /// <value>The provider URI.</value>
        public Uri ProviderUri { get; set; }

        /// <summary>
        /// Gets or sets the mark out time.
        /// </summary>
        /// <value>The mark out in seconds.</value>
        public double? MarkOut
        {
            get
            {
                return this.markOut;
            }

            set
            {
                this.markOut = value;
                this.OnPropertyChanged("MarkOut");
            }
        }
    }
}
