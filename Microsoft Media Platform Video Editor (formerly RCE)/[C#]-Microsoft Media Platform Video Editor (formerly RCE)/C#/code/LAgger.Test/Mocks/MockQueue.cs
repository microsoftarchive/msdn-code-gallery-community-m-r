// <copyright file="MockQueue.cs" company="Microsoft Corporation">
//     Copyright 2008 (c) Microsoft Corporation. ALL RIGHTS RESERVED.
// </copyright>

namespace LAgger.Test.Mocks
{
    using System;

    /// <summary>
    /// Mock classed used to decouple the LogManager from the queues.
    /// </summary>
    public class MockQueue : IQueue
    {
        private Entry currentItem;

        /// <summary>
        /// Raised when an item has been Enqueued to the <see cref="IQueue"/>.
        /// </summary>
        public event EventHandler<EntryEnqueuedEventArgs> EntryEnqueued;

        /// <summary>
        /// Gets the number of <see cref="Entry"/> objects that are in the <see cref="IQueue"/>.
        /// </summary>
        public int Count
        {
            get
            {
                if (this.currentItem == null)
                {
                    return 0;
                }

                return 1;
            }
        }

        /// <summary>
        /// Adds a new item to the <see cref="IQueue"/>.
        /// </summary>
        /// <param name="entry">The <see cref="Entry"/> to add to the <see cref="IQueue"/>.</param>
        public void Enqueue(Entry entry)
        {
            this.currentItem = entry;
            this.EntryEnqueued(this, new EntryEnqueuedEventArgs { Entry = entry });
        }

        /// <summary>
        /// Returns and removes a <see cref="LogEntry"/> from the beginning of the <see cref="IQueue"/>.
        /// </summary>
        /// <returns>The <see cref="Entry"/> that is removed from the beginning of the <see cref="IQueue"/>.</returns>
        public Entry Dequeue()
        {
            var result = this.currentItem;
            this.currentItem = null;
            return result;
        }

        /// <summary>
        /// Returns the <see cref="Entry"/> at the beginning of the <see cref="IQueue"/> without removing it.
        /// </summary>
        /// <returns>The <see cref="Entry"/> at the beginning of the <see cref="IQueue"/></returns>
        public Entry Peek()
        {
            return this.currentItem;
        }
    }
}