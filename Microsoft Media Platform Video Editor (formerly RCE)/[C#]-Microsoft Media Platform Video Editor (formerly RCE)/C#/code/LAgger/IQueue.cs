// <copyright file="IQueue.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IQueue.cs                     
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
    /// Defines the interface to use for LAgger to store items in prior to sending to the backend service.
    /// </summary>
    public interface IQueue
    {
        /// <summary>
        /// Raised when an item has been Enqueued to the <see cref="IQueue"/>.
        /// </summary>
        event EventHandler<EntryEnqueuedEventArgs> EntryEnqueued;

        /// <summary>
        /// Gets the number of <see cref="Entry"/> objects that are in the <see cref="IQueue"/>.
        /// </summary>
        /// <value>An <seealso cref="int"/> value that trepesents the number of objects that are in the <see cref="IQueue"/>.</value>
        int Count { get; }

        /// <summary>
        /// Adds a new item to the <see cref="IQueue"/>.
        /// </summary>
        /// <param name="entry">The <see cref="Entry"/> to add to the <see cref="IQueue"/>.</param>
        void Enqueue(Entry entry);

        /// <summary>
        /// Returns and removes a <see cref="LogEntry"/> from the beginning of the <see cref="IQueue"/>.
        /// </summary>
        /// <returns>The <see cref="Entry"/> that is removed from the beginning of the <see cref="IQueue"/>.</returns>
        Entry Dequeue();

        /// <summary>
        /// Returns the <see cref="Entry"/> at the beginning of the <see cref="IQueue"/> without removing it.
        /// </summary>
        /// <returns>The <see cref="Entry"/> at the beginning of the <see cref="IQueue"/>.</returns>
        Entry Peek();
    }
}
