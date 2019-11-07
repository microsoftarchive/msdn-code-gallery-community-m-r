// <copyright file="MockLoggerFacade.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockLoggerFacade.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.MediaBin.Tests.Mocks
{
    using System;
    using Infrastructure;
    using Infrastructure.Models;

    using RCE.Infrastructure.Services;

    /// <summary>
    /// Mock class used to test ILogger.
    /// </summary>
    public class MockLoggerFacade : ILogger 
    {
        /// <summary>
        /// Gets or sets the value for Message.
        /// </summary>
        public string LogMessageArgument { get; set; }

        /// <summary>
        /// Gets or sets the value for Category.
        /// </summary>
        public string LogCategoryArgument { get; set; }

        /// <summary>
        /// Gets or sets the value for Severity.
        /// </summary>
        public TraceEventType LogSeverityArgument { get; set; }

        /// <summary>
        /// Gets or sets the value for Priorty.
        /// </summary>
        public int LogPriorityArgument { get; set; }

        /// <summary>
        /// Gets or sets the value for Event ID.
        /// </summary>
        public int LogEventIdArgument { get; set; }

        /// <summary>
        /// Gets or sets the value for Title.
        /// </summary>
        public string LogTitleArgument { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Log method is called or not.
        /// </summary>
        public bool LogCalled { get; set; }

        /// <summary>
        /// Used to check log object with message.
        /// </summary>
        /// <param name="message">Message that is being logged.</param>
        public void Log(string message)
        {
            this.LogCalled = true;
            this.LogMessageArgument = message;
        }

        /// <summary>
        /// Used to check log object with message and category.
        /// </summary>
        /// <param name="message">Message that is being logged.</param>
        /// <param name="category">Category name used to route the log entry.</param>
        public void Log(string message, string category)
        {
            this.LogCalled = true;
            this.LogMessageArgument = message;
            this.LogCategoryArgument = category;
        }

        /// <summary>
        /// Used to check log object with message and severity.
        /// </summary>
        /// <param name="message">Message that is being logged.</param>
        /// <param name="severity">Identifies the severity of the entry.</param>
        public void Log(string message, TraceEventType severity)
        {
            this.LogCalled = true;
            this.LogMessageArgument = message;
            this.LogSeverityArgument = severity;
        }

        /// <summary>
        /// Used to check log object with message, category and priority.
        /// </summary>
        /// <param name="message">Message that is being logged.</param>
        /// <param name="category">Category name used to route the log entry.</param>
        /// <param name="priority">Indicates the priority of the log entry.</param>
        public void Log(string message, string category, int priority)
        {
            this.LogCalled = true;
            this.LogMessageArgument = message;
            this.LogCategoryArgument = category;
            this.LogPriorityArgument = priority;
        }

        /// <summary>
        /// Used to check log object with message, category, priority and event id.
        /// </summary>
        /// <param name="message">Message that is being logged.</param>
        /// <param name="category">Category name used to route the log entry.</param>
        /// <param name="priority">Indicates the priority of the log entry.</param>
        /// <param name="eventId">The event id to use to identify the event.</param>
        public void Log(string message, string category, int priority, int eventId)
        {
            this.LogCalled = true;
            this.LogMessageArgument = message;
            this.LogCategoryArgument = category;
            this.LogPriorityArgument = priority;
            this.LogEventIdArgument = eventId;
        }

        /// <summary>
        /// Used to check log object with message, category, priority, event id and severity.
        /// </summary>
        /// <param name="message">Message that is being logged.</param>
        /// <param name="category">Category name used to route the log entry.</param>
        /// <param name="priority">Indicates the priority of the log entry.</param>
        /// <param name="eventId">The event id to use to identify the event.</param>
        /// <param name="severity">Identifies the severity of the entry.</param>
        public void Log(string message, string category, int priority, int eventId, TraceEventType severity)
        {
            this.LogCalled = true;
            this.LogMessageArgument = message;
            this.LogCategoryArgument = category;
            this.LogPriorityArgument = priority;
            this.LogEventIdArgument = eventId;
            this.LogSeverityArgument = severity;
        }

        /// <summary>
        /// Used to check log object with message, category, priority, event id, severity and title.
        /// </summary>
        /// <param name="message">Message that is being logged.</param>
        /// <param name="category">Category name used to route the log entry.</param>
        /// <param name="priority">Indicates the priority of the log entry.</param>
        /// <param name="eventId">The event id to use to identify the event.</param>
        /// <param name="severity">Identifies the severity of the entry.</param>
        /// <param name="title">The title of the entry.</param>
        public void Log(string message, string category, int priority, int eventId, TraceEventType severity, string title)
        {
            this.LogCalled = true;
            this.LogMessageArgument = message;
            this.LogCategoryArgument = category;
            this.LogPriorityArgument = priority;
            this.LogEventIdArgument = eventId;
            this.LogSeverityArgument = severity;
            this.LogTitleArgument = title;
        }

        public void Log(string category, Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}
