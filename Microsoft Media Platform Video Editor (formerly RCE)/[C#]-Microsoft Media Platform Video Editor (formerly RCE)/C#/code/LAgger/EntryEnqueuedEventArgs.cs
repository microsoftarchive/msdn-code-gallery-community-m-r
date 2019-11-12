// <copyright file="EntryEnqueuedEventArgs.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: EntryEnqueuedEventArgs.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace LAgger
{
    using System;
    using Services;

    /// <summary>
    /// A class used to contain the event arguments for the <see cref="IQueue.EntryEnqueued"/> event.
    /// </summary>
    public class EntryEnqueuedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the item that is being enqueued.
        /// </summary>
        /// <value>The <see cref="Entry"/> that is being enqueued.</value>
        public Entry Entry
        {
            get;
            set;
        }
    }
}
