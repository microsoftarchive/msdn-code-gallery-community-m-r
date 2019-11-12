// <copyright file="MemoryQueue.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MemoryQueue.cs                     
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
    using System.Collections.Generic;

    /// <summary>
    /// An implementation of the <see cref="IQueue"/> that persists the queue in memory only.
    /// </summary>
    public class MemoryQueue : IQueue
    {
        /// <summary>
        /// Contains the private queue that is used to back the <see cref="MemoryQueue"/>.
        /// </summary>
        private readonly Queue<Entry> queue = new Queue<Entry>();

        /// <summary>
        /// Raised when an item has been Enqueued to the <see cref="IQueue"/>.
        /// </summary>
        public event EventHandler<EntryEnqueuedEventArgs> EntryEnqueued;

        /// <summary>
        /// Gets the number of <see cref="Entry"/> objects that are in the <see cref="IQueue"/>.
        /// </summary>
        /// <value>Contains the number of objectes that are in the queue.</value>
        public int Count
        {
            get { return this.queue.Count; }
        }

        /// <summary>
        /// Adds a new item to the <see cref="IQueue"/>.
        /// </summary>
        /// <param name="entry">The <see cref="Entry"/> to add to the <see cref="IQueue"/>.</param>
        public void Enqueue(Entry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException("entry");
            }

            this.queue.Enqueue(entry);

            var entryEnqueued = this.EntryEnqueued;
            if (entryEnqueued != null)
            {
                entryEnqueued(this, new EntryEnqueuedEventArgs { Entry = entry });
            }
        }

        /// <summary>
        /// Returns and removes a <see cref="LogEntry"/> from the beginning of the <see cref="IQueue"/>.
        /// </summary>
        /// <returns>The <see cref="Entry"/> that is removed from the beginning of the <see cref="IQueue"/>.</returns>
        public Entry Dequeue()
        {
            lock (this.queue)
            {
                if (this.queue.Count == 0)
                {
                    return null;
                }
                
                return this.queue.Dequeue();
            }
        }

        /// <summary>
        /// Returns the <see cref="Entry"/> at the beginning of the <see cref="IQueue"/> without removing it.
        /// </summary>
        /// <returns>The <see cref="Entry"/> at the beginning of the <see cref="IQueue"/>.</returns>
        public Entry Peek()
        {
            lock (this.queue)
            {
                if (this.queue.Count == 0)
                {
                    return null;
                }
                
                return this.queue.Peek();
            }
        }
    }
}
