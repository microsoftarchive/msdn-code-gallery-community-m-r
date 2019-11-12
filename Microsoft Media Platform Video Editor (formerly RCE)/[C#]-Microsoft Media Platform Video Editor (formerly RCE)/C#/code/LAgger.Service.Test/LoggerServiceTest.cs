// <copyright file="LoggerServiceTest.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: LoggerServiceTest.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace LAgger.Service.Test
{
    using LAgger;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    /// <summary>
    /// A test class for ensuring that <see cref="LoggerService"/> works correctly.
    /// </summary>
    [TestClass]
    public class LoggerServiceTest
    {
        /// <summary>
        /// Test to make sure that configuration loaded correctly.
        /// </summary>
        [TestMethod]
        public void LoggerServiceTest_DistributeConfiguration()
        {
            var expected = new LoggingConfiguration();
            expected.BatchSize = 2;
            expected.TracingEnabled = true;
            expected.LogLevel = 2;
            LoggerService loggerService = new LoggerService();
            var actual = loggerService.DistributeConfiguration();
            Assert.AreEqual(expected.BatchSize, actual.BatchSize);
            Assert.AreEqual(expected.LogLevel, actual.LogLevel);
            Assert.AreEqual(expected.TracingEnabled, actual.TracingEnabled);
        }

        /// <summary>
        /// Test to make sure that there are null entires in the collection.
        /// </summary>
        [TestMethod]
        public void LoggerService_CollectEntries_null()
        {
            Entry[] entries = { new LogEntry(), new TraceEntry(), null };
            var actual = new MockLoggerService();
            actual.LogEntries(entries);
            Assert.AreEqual(1, actual.LogCount);
            Assert.AreEqual(1, actual.TraceCount);
            Assert.AreEqual(2, actual.TotalCount);
            Assert.IsTrue(actual.LogEntryCollection);
            Assert.IsTrue(actual.TraceEntryCollection);
            Assert.IsTrue(actual.MixEntryCollection);
        }

        /// <summary>
        /// Test to make sure that there is a single log entry in the collection. 
        /// </summary>
        [TestMethod]
        public void LoggerService_CollectEntries_Single_LogEntry()
        {
            Entry[] entries = { new LogEntry() };
            var actual = new MockLoggerService();
            actual.LogEntries(entries);
            Assert.AreEqual(1, actual.LogCount);
            Assert.IsTrue(actual.LogEntryCollection);
        }
        
        /// <summary>
        /// Test to make sure that there is a single trace entry in the collection.
        /// </summary>
        [TestMethod]
        public void LoggerService_CollectEntries_Single_TraceEntry()
        {
            Entry[] entries = { new TraceEntry() };
            var actual = new MockLoggerService();
            actual.LogEntries(entries);
            Assert.AreEqual(1, actual.TraceCount);
            Assert.IsTrue(actual.TraceEntryCollection);
        }

        /// <summary>
        /// Test to make sure that there are mix entries in the collection.
        /// </summary>
        [TestMethod]
        public void LoggerService_CollectEntries_Three_Mix()
        {
            Entry[] entries = { new LogEntry(), new LogEntry(), new TraceEntry() };
            var actual = new MockLoggerService();
            actual.LogEntries(entries);
            Assert.AreEqual(2, actual.LogCount);
            Assert.AreEqual(1, actual.TraceCount);
            Assert.AreEqual(3, actual.TotalCount);
            Assert.IsTrue(actual.MixEntryCollection);
        }

        /// <summary>
        /// Test to make sure that there are three trace entries in the collection. 
        /// </summary>
        [TestMethod]
        public void LoggerService_CollectEntries_Three_TraceEntry()
        {
            Entry[] entries = { new TraceEntry(), new TraceEntry(), new TraceEntry() };
            var actual = new MockLoggerService();
            actual.LogEntries(entries);
            Assert.AreEqual(0, actual.LogCount);
            Assert.AreEqual(3, actual.TraceCount);
            Assert.AreEqual(3, actual.TotalCount);
            Assert.IsTrue(actual.TraceEntryCollection);
        }

        /// <summary>
        /// Test to validate the log entry.
        /// </summary>
        [TestMethod]
        public void LoggerService_ValidateEntry()
        {
            LogEntry logEntry = new LogEntry("Test", "TestCategory", 1, 12, TraceEventType.Verbose, "Test Title");
            var actual = new MockLoggerService();
            Assert.IsTrue(actual.ValidateEntry(logEntry));
        }
    }
}
