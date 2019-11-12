// <copyright file="LoggerManagerTest.cs" company="Microsoft Corporation">
//     Copyright 2008 (c) Microsoft Corporation. ALL RIGHTS RESERVED.
// </copyright>

namespace LAgger.Test
{
    using System;
    using LAgger;
    using Microsoft.Silverlight.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    /// <summary>
    /// Test class for ensuring that <see cref="LoggerManager"/> works correctly.
    /// </summary>
    [TestClass]
    public class LoggerManagerTest : PresentationTest
    {
        /// <summary>
        /// Tests the write operation LoggerManager class with a valid LogEntry class.
        /// </summary>
        [TestMethod]
        public void LoggerManager_Write_ValidLogEntry()
        {
            var expected = new LogEntry
            {
                Title = "test",
                EventId = 1,
                Priority = 1,
                Severity = TraceEventType.Error,
                Message = "testing"
            };
            var manager = new MockLogManager { UnfilteredQueue = new MockQueue() };
            LoggerManager.Start(manager, new Uri("http://test"));
          
            Assert.IsNotNull(manager);
            ((ILoggerManager)manager).Write(expected);
            
            Assert.IsNotNull(manager.UnfilteredQueue);
            Assert.AreEqual(1, manager.UnfilteredQueue.Count);
            var actual = manager.UnfilteredQueue.Peek();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests the write operation with a null entry to verify a null operation exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoggerManager_Write_NullEntry()
        {
            LoggerManager.Start(new MockLogManager(), new Uri("http://test"));
            LoggerManager.Manager.Write(null);
        }

        /// <summary>
        /// Tests the write operation with an invalid Entry.Id value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void LoggerManager_Write_EmptyEntryId()
        {
            LoggerManager.Start(new MockLogManager(), new Uri("http://test"));
            var entry = new LogEntry();
            entry.Id = Guid.Empty;
            LoggerManager.Manager.Write(entry);
        }

        /// <summary>
        /// Tests the UnfilteredQueue property to ensure that it can be set succesfully.
        /// </summary>
        [TestMethod]
        public void LoggerManager_UnfilteredQueue_SetOnce()
        {
            var manager = new MockLogManager();
            manager.UnfilteredQueue = new MockQueue();
        }

        /// <summary>
        /// Tests the UnfilteredQueue property to ensure that it can be set succesfully.
        /// </summary>
        [TestMethod]
        public void LoggerManager_UnfilteredQueue_SetTwice()
        {
            var manager = new MockLogManager();
            manager.UnfilteredQueue = new MockQueue();
            manager.UnfilteredQueue = new MockQueue();
        }

        /// <summary>
        /// Tests the UnfilteredQueue property to ensure that it can be set succesfully.
        /// </summary>
        [TestMethod]
        public void LoggerManager_UnfilteredQueue_SetOnceThenNull()
        {
            var manager = new MockLogManager();
            manager.UnfilteredQueue = new MockQueue();
            manager.UnfilteredQueue = null;
        }

        /// <summary>
        /// Checks the default initialization of <see cref="LoggerManager.Start()"/>.
        /// </summary>
        [TestMethod]
        public void LoggerManager_Start_NoParameters()
        {
            LoggerManager.Start(new MockLogManager(), new Uri("http://test"));
            Assert.IsTrue(LoggerManager.Started);
            Assert.IsNotNull(LoggerManager.Manager);
            Assert.IsInstanceOfType(LoggerManager.Manager, typeof(LoggerManager));
        }

        /// <summary>
        /// Checks to make sure that the <see cref="ILoggerManager.Initialize()"/>. method is called.
        /// </summary>
        [TestMethod]
        public void LoggerManager_Start_CheckInitialize()
        {
            var manager = new MockLoggerManager();
            LoggerManager.Start(manager, new Uri("http://test"));
            Assert.IsTrue(manager.Initialized, "Initialized was not called.");
        }

        /// <summary>
        /// Check to make sure that expected entries are filtered correctly.
        /// </summary>
        [TestMethod]
        public void FilterEntryTest_FilterEntries()
        {
            var expected = new LogEntry
                               {
                                   Title = "test",
                                   EventId = 1,
                                   Priority = 1,
                                   Severity = TraceEventType.Error,
                                   Message = "testing"
                               };

            var manager = new MockLogManager();
            manager.UnfilteredQueue = new MockQueue();
            manager.FilteredQueue = new MockQueue();
            LoggerManager.Start(manager, new Uri("http://test"));
            Assert.IsNotNull(manager);
            Assert.IsNotNull(manager.UnfilteredQueue);
            Assert.IsNotNull(manager.FilteredQueue);

            ((ILoggerManager)manager).Write(expected);

            Assert.IsTrue(manager.FilterCalled, "FilterEntries was not called.");

            var actual = manager.FilteredQueue.Peek();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests to make sure that filter entries doesn't break with one items in queue.
        /// </summary>
        [TestMethod]
        public void LoggerManager_FilterEntries_OneEntries()
        {
            var manager = new MockLogManager();
            var unfilteredQueue = new MemoryQueue();
            var expected = new LogEntry
            {
                Title = "test",
                EventId = 1,
                Priority = 1,
                Severity = TraceEventType.Error,
                Message = "testing"
            };
            unfilteredQueue.Enqueue(expected);    

            manager.UnfilteredQueue = unfilteredQueue;
            manager.FilteredQueue = new MockQueue();
            manager.Initialize(new Uri("http://test"));
            manager.ExecuteFilter();
            Assert.IsTrue(manager.FilterCalled, "Filter was not called.");
            Assert.AreEqual(1, manager.FilteredQueue.Count, "Filters were not executed correctly.");
        }

        /// <summary>
        /// Tests to make sure that filter entries doesn't break with multiple items in queue.
        /// </summary>
        [TestMethod]
        public void LoggerManager_FilterEntries_ThreeEntries()
        {
            var manager = new MockLogManager();
            manager.ExecutePublishFilter = false;
            var unfilteredQueue = new MemoryQueue();
            unfilteredQueue.Enqueue(new LogEntry
                               {
                                   Title = "test",
                                   EventId = 1,
                                   Priority = 1,
                                   Severity = TraceEventType.Error,
                                   Message = "testing"
                               });
            unfilteredQueue.Enqueue(new LogEntry
            {
                Title = "test",
                EventId = 1,
                Priority = 1,
                Severity = TraceEventType.Error,
                Message = "testing"
            });
            unfilteredQueue.Enqueue(new LogEntry
            {
                Title = "test",
                EventId = 1,
                Priority = 1,
                Severity = TraceEventType.Error,
                Message = "testing"
            });

            manager.UnfilteredQueue = unfilteredQueue;
            var filteredQueue = new MemoryQueue();
            manager.FilteredQueue = filteredQueue;
            manager.Initialize(new Uri("http://test"));
            manager.ExecuteFilter();
            Assert.IsTrue(manager.FilterCalled, "Filter was not called.");
            Assert.AreEqual(3, manager.FilteredQueue.Count, "Filters were not executed correctly.");
        }

        /// <summary>
        /// Tests to make sure that filter entries doesn't break with zero items in queue.
        /// </summary>
        [TestMethod]
        public void LoggerManager_FilterEntries_ZeroEntries()
        {
            var manager = new MockLogManager();
            manager.UnfilteredQueue = new MockQueue();
            manager.FilteredQueue = new MockQueue();

            manager.ExecuteFilter();
            Assert.IsTrue(manager.FilterCalled, "Filter was not called.");
        }

        /// <summary>
        /// Test to make sure that entry objects have been sent correctly to LoggerProxy.
        /// </summary>
        [TestMethod]
        public void LoggerManager_PublishEntries()
        {
            var manager = new MockLogManager();
            var filteredQueue = new MemoryQueue();
            filteredQueue.Enqueue(new LogEntry
            {
                Title = "test",
                EventId = 1,
                Priority = 1,
                Severity = TraceEventType.Error,
                Message = "testing"
            });
            filteredQueue.Enqueue(new LogEntry
            {
                Title = "test",
                EventId = 1,
                Priority = 1,
                Severity = TraceEventType.Error,
                Message = "testing"
            });
            manager.FilteredQueue = filteredQueue;
            manager.PublishFilteredEntries();
            Assert.AreEqual(0, manager.FilteredQueue.Count, "Expected zero enteries in the filtered queue");
        }
    }
}
