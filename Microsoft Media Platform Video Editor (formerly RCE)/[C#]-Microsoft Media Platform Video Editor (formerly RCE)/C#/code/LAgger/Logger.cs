// <copyright file="Logger.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Logger.cs                     
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
    /// <summary>
    /// A class used for writing logging information.
    /// </summary>
    public static class Logger 
    {
        /// <summary>
        /// Tracks what to use for the default priority if it has not been specified. Default value is 1.
        /// </summary>
        private const int DEFAULTPRIORITY = 1;

        /// <summary>
        /// Identifies the default severity to use. Default it is Info.
        /// </summary>
        private const TraceEventType DEFAULTSEVERITY = TraceEventType.Information;

        /// <summary>
        /// Identifies the default event id to use. Default value is 1.
        /// </summary>
        private const int DEFAULTEVENTID = 1;

        /// <summary>
        /// Identifies the default title to use. Default value is "Title".
        /// </summary>
        private const string DEFAULTTITLE = "Title";

        /// <summary>
        /// Identifies the default category to use. Default value is "General".
        /// </summary>
        private const string DEFAULTCATEGORY = "General";

        /// <summary>
        /// Writes the values to the logging store.
        /// </summary>
        /// <param name="message">The message that is being logged.</param>
        public static void Write(object message)
        {
           Logger.Write(message, DEFAULTCATEGORY, DEFAULTPRIORITY, DEFAULTEVENTID, DEFAULTSEVERITY, DEFAULTTITLE);
        }

        /// <summary>
        /// Writes the values to the logging store.
        /// </summary>
        /// <param name="message">The message that is being logged.</param>
        /// <param name="category">Category name used to route the log entry.</param>
        public static void Write(object message, string category)
        {
            Logger.Write(message, category, DEFAULTPRIORITY, DEFAULTEVENTID, DEFAULTSEVERITY, DEFAULTTITLE);
        }

        /// <summary>
        /// Writes the values to the logging store.
        /// </summary>
        /// <param name="message">The message that is being logged.</param>
        /// <param name="severity">Identifies the severity of the entry.</param>
        public static void Write(object message, TraceEventType severity)
        {
            Logger.Write(message, DEFAULTCATEGORY, DEFAULTPRIORITY, DEFAULTEVENTID, severity, DEFAULTTITLE);
        }

        /// <summary>
        /// Writes the values to the logging store.
        /// </summary>
        /// <param name="message">The message that is being logged.</param>
        /// <param name="category">Category name used to route the log entry.</param>
        /// <param name="priority">Indicates the priority of the log entry.</param>
        public static void Write(object message, string category, int priority)
        {
            Logger.Write(message, category, priority, DEFAULTEVENTID, DEFAULTSEVERITY, DEFAULTTITLE);
        }

        /// <summary>
        /// Writes the values to the logging store.
        /// </summary>
        /// <param name="message">The message that is being logged.</param>
        /// <param name="category">Category name used to route the log entry.</param>
        /// <param name="priority">Indicates the priority of the log entry.</param>
        /// <param name="eventId">The event id to use to identify the event.</param>
        public static void Write(object message, string category, int priority, int eventId)
        {
            Logger.Write(message, category, priority, eventId, DEFAULTSEVERITY, DEFAULTTITLE);
        }

        /// <summary>
        /// Writes the values to the logging store.
        /// </summary>
        /// <param name="message">The message that is being logged.</param>
        /// <param name="category">Category name used to route the log entry.</param>
        /// <param name="priority">Indicates the priority of the log entry.</param>
        /// <param name="eventId">The event id to use to identify the event.</param>
        /// <param name="severity">Identifies the severity of the entry.</param>
        public static void Write(object message, string category, int priority, int eventId, TraceEventType severity)
        {
            Logger.Write(message, category, priority, eventId, severity, DEFAULTTITLE);
        }

        /// <summary>
        /// Writes the values to the logging store.
        /// </summary>
        /// <param name="message">The message that is being logged.</param>
        /// <param name="category">Category name used to route the log entry.</param>
        /// <param name="priority">Indicates the priority of the log entry.</param>
        /// <param name="eventId">The event id to use to identify the event.</param>
        /// <param name="severity">Identifies the severity of the entry.</param>
        /// <param name="title">The title of the entry.</param>
        public static void Write(object message, string category, int priority, int eventId, TraceEventType severity, string title)
        {
            LogEntry log = new LogEntry();
            log.Message = message.ToString();
            log.Category = category;
            log.Priority = priority;
            log.EventId = eventId;
            log.Severity = severity;
            log.Title = title;
            Logger.Write(log);
        }

        /// <summary>
        /// Writes a new <see cref="LogEntry"/> to the configured logging store.
        /// </summary>
        /// <param name="log">The <see cref="LogEntry"/> that is being logged.</param>
        public static void Write(LogEntry log)
        {
           if (LoggerManager.Manager != null)
           {
               LoggerManager.Manager.Write(log);
           }     
        }
    }
}
