// <copyright file="MemoryQueueTest.cs" company="Microsoft Corporation">
//     Copyright 2008 (c) Microsoft Corporation. ALL RIGHTS RESERVED.
// </copyright>

namespace LAgger.Test
{
    using System;
    using LAgger;
    using Microsoft.Silverlight.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Services;

    /// <summary>
    /// Test class for ensuring that <see cref="MemoryQueue"/> works correctly.
    /// </summary>
    [TestClass]
    public class MemoryQueueTest : PresentationTest
    {
        /// <summary>
        /// This method is used to ensure Peek returns null when there are no entries.
        /// </summary>
        [TestMethod]
        public void MemoryQueue_Peek_NoItems()
        {
            var queue = new MemoryQueue();
            Assert.AreEqual(0, queue.Count, "Expected zero Entries in the queue.");
            var entry = queue.Peek();
            Assert.IsNull(entry, "Peek did not return back null when it had zero entries.");
        }

        /// <summary>
        /// This method tests to make sure that a Peek can be performed with 1 item in the queue.
        /// </summary>
        [TestMethod]
        public void MemoryQueue_Peek_OneItem()
        {
            var expected = new LogEntry();
            var queue = new MemoryQueue();
            queue.Enqueue(expected);
            Assert.AreEqual(1, queue.Count, "Expected one Entries in the queue.");
            var actual = queue.Peek();
            Assert.AreEqual(expected.Id, actual.Id, "The Id of the enqueued item did not match the Id of the Peeked item.");
            Assert.AreEqual(expected, actual, "A different object was returned from Peek than what was inserted.");
        }

        /// <summary>
        /// This method tests to make sure that a peek returns the correct item when there are three items in the queue.
        /// </summary>
        [TestMethod]
        public void MemoryQueue_Peek_ThreeItems()
        {
            var expected = new LogEntry();
            var queue = new MemoryQueue();
            queue.Enqueue(expected);
            queue.Enqueue(new LogEntry());
            queue.Enqueue(new LogEntry());
            Assert.AreEqual(3, queue.Count, "Expected three Entries in the queue.");
            var actual = queue.Peek();
            Assert.AreEqual(expected.Id, actual.Id, "The Id of the enqueued item did not match the Id of the Peeked item.");
            Assert.AreEqual(expected, actual, "A different object was returned from Peek than what was inserted.");
        }

        /// <summary>
        /// This method tests to make sure that a dequeue returns null when there are no entires in the queue.
        /// </summary>
        [TestMethod]
        public void MemoryQueue_Dequeue_NoItem()
        {
            var queue = new MemoryQueue();
            Assert.AreEqual(0, queue.Count, "Expected zero Entries in the queue.");
            var entry = queue.Dequeue();
            Assert.IsNull(entry, "Dequeue did not return back null when it had zero entries.");
        }

        /// <summary>
        /// This method tests to make sure that a dequeue returns the correct item when there is one item in the queue.
        /// </summary>
        [TestMethod]
        public void MemoryQueue_Dequeue_OneItem()
        {
            var expected = new LogEntry();
            var queue = new MemoryQueue();
            queue.Enqueue(expected);
            var actual = queue.Dequeue();
            Assert.AreEqual(0, queue.Count, "Expected zero entries in the queue");
            Assert.AreEqual(expected.Id, actual.Id, "The Id of the enqueued item did not match the Id of the dequeued item.");
            Assert.AreEqual(expected, actual, "A different object was returned from dequeue than what was inserted.");
        }

        /// <summary>
        /// This method tests to make sure that a dequeue returns the correct item when there three items in the queue.
        /// </summary>
        [TestMethod]
        public void MemoryQueue_Dequeue_ThreeItem()
        {
            var expected = new LogEntry();
            var queue = new MemoryQueue();
            queue.Enqueue(expected);
            queue.Enqueue(new LogEntry());
            queue.Enqueue(new LogEntry());
            var actual = queue.Dequeue();
            Assert.AreEqual(2, queue.Count, "Expected two entries in the queue");
            Assert.AreEqual(expected.Id, actual.Id, "The Id of the enqueued item did not match the Id of the dequeued item.");
            Assert.AreEqual(expected, actual, "A different object was returned from dequeue than what was inserted.");
        }

        /// <summary>
        /// This method tests to make sure that a enqueue adds null object in the queue when there is zero item in the queue.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MemoryQueue_Enqueue_NullItem()
        {
            var queue = new MemoryQueue();
            queue.Enqueue(null);
        }

        /// <summary>
        /// This method tests to make sure that a enqueue adds the item in the queue when there is zero item in the queue.
        /// </summary>
        [TestMethod]
        public void MemoryQueue_Enqueue_OneItem()
        {
            var queue = new MemoryQueue();
            queue.Enqueue(new LogEntry());
            Assert.AreEqual(1, queue.Count, "Expected one entry in the queue");
        }

        /// <summary>
        /// This method tests to make sure that an EntryEnqueuedEvent is raised when item is enqueued.
        /// </summary>
        [TestMethod]
        public void EntryEnqueuedRaiseEvent()
        {
            bool eventRaised = false;
            var expected = new LogEntry();
            var queue = new MemoryQueue();
            queue.EntryEnqueued +=
                delegate(object sender, EntryEnqueuedEventArgs e)
                {
                    Assert.AreEqual(1, queue.Count, "Expected one entry in the queue");
                    Assert.AreEqual(expected, e.Entry, "Expected correct object should be enqueued");
                    eventRaised = true;
                };
            queue.Enqueue(expected);
            Assert.IsTrue(eventRaised, "Event was not raised");
        }
    }
}
