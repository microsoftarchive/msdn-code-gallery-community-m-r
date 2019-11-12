// <copyright file="CommentCollection.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentCollection.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Contracts
{
    using System;
    using System.Collections.ObjectModel;

    /// <summary>
    /// A class used to contain a collection of <see cref="Comment"/>s.
    /// </summary>
    public class CommentCollection : ObservableCollection<Comment>
    {
        /// <summary>
        /// Adds a new comment to the collection.
        /// </summary>
        /// <param name="id">The item that is being annotated.</param>
        /// <param name="text">The text of the comment.</param>
        /// <param name="markIn">The mark in point of the comment.</param>
        /// <param name="markOut">The mark out point of the comment.</param>
        public void Add(Uri id, string text, double markIn, double markOut)
        {
            Comment comment = new Comment { Id = id, MarkIn = markIn, MarkOut = markOut, Text = text };
            this.Add(comment);
        }
    }
}