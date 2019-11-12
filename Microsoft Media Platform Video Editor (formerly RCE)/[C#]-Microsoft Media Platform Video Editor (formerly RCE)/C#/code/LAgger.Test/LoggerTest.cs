// <copyright file="LoggerTest.cs" company="Microsoft Corporation">
//     Copyright 2008 (c) Microsoft Corporation. ALL RIGHTS RESERVED.
// </copyright>

namespace LAgger.Test
{
    using System;
    using LAgger;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    /// <summary>
    /// A test class for ensuring that <see cref="Logger"/> works correctly.
    /// </summary>
    [TestClass]
    public class LoggerTest
    {
        /// <summary>
        /// This method check the instance type of Logger Object,
        /// and compare the expected output with log entry data.
        /// </summary>
        [TestMethod]
        public void Logger_Write_LogEntry()
        {
            LogEntry expected = new LogEntry();
            MockLoggerManager manager = new MockLoggerManager();
            LoggerManager.Start(manager, new Uri("http://test"));
            
            Logger.Write(expected);

            Assert.AreEqual(expected, manager.Entry);
            Assert.IsInstanceOfType(manager.Entry, typeof(LogEntry));
        }
        
        /// <summary>
        /// This method is used to check the Logger Object with message.
        /// </summary>
        [TestMethod]
        public void Logger_Write_Message()
        {
            LogEntry expected = new LogEntry();
            expected.Message = "asdf";
            MockLoggerManager manager = new MockLoggerManager();
            LoggerManager.Start(manager, new Uri("http://test"));
            
            Logger.Write(expected);

            LogEntry actual = manager.Entry as LogEntry;
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Message, actual.Message);
        }
        
        /// <summary>
        /// This method is used to check the Logger Object with message and priority.
        /// </summary>
        [TestMethod]
        public void Logger_Write_Message_Priority()
        {
            LogEntry expected = new LogEntry();
            expected.Message = "asdf";
            expected.Priority = 1;
            MockLoggerManager manager = new MockLoggerManager();
            LoggerManager.Start(manager, new Uri("http://test"));
            
            Logger.Write(expected);

            LogEntry actual = manager.Entry as LogEntry;
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.Priority, actual.Priority);
        }
 
        /// <summary>
        /// This method is used to check the Logger Object with message, prioriy and eventId.
        /// </summary>
        [TestMethod]
        public void Logger_Write_Message_Priority_EventID()
        {
            LogEntry expected = new LogEntry();
             
            expected.Message = "asdf";
            expected.Priority = 1;
            expected.EventId = 1;

            MockLoggerManager manager = new MockLoggerManager();
            LoggerManager.Start(manager, new Uri("http://test"));
            
            Logger.Write(expected);

            LogEntry actual = manager.Entry as LogEntry;
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.Priority, actual.Priority);
            Assert.AreEqual(expected.EventId, actual.EventId);
         }
        
        /// <summary>
        /// This method is used to check the Logger Object with message, prioriy, eventId and severity.
        /// </summary>
        [TestMethod]
        public void Logger_Write_Message_Priority_EventID_Severity()
        {
            LogEntry expected = new LogEntry();

            expected.Message = "asdf";
            expected.Priority = 1;
            expected.EventId = 1;
            expected.Severity = TraceEventType.Information;
            
            MockLoggerManager manager = new MockLoggerManager();
            LoggerManager.Start(manager, new Uri("http://test"));
            
            Logger.Write(expected);

            LogEntry actual = manager.Entry as LogEntry;
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.Priority, actual.Priority);
            Assert.AreEqual(expected.EventId, actual.EventId);
            Assert.AreEqual(expected.Severity, actual.Severity);
        }
        
        /// <summary>
        /// This method is used to check the Logger Object with message, prioriy, eventId, severity and title.
        /// </summary>
        [TestMethod]
        public void Logger_Write_Message_Priority_EventID_Severity_Title()
        {
            LogEntry expected = new LogEntry();

            expected.Message = "asdf";
            expected.Priority = 1;
            expected.EventId = 1;
            expected.Severity = TraceEventType.Information;
            expected.Title = "Title";

            MockLoggerManager manager = new MockLoggerManager();
            LoggerManager.Start(manager, new Uri("http://test"));
            
            Logger.Write(expected);

            LogEntry actual = manager.Entry as LogEntry;
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Message, actual.Message);
            Assert.AreEqual(expected.Priority, actual.Priority);
            Assert.AreEqual(expected.EventId, actual.EventId);
            Assert.AreEqual(expected.Severity, actual.Severity);
            Assert.AreEqual(expected.Title, actual.Title);
        }
    }
}