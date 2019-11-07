// <copyright file="LoggerFacade.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: LoggerFacade.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services
{
    using System;
    using System.ServiceModel;
    using Infrastructure;
    using LAgger;

    using RCE.Infrastructure.Services;

    using TraceEventType = Infrastructure.Models.TraceEventType;

    /// <summary>
    /// Provides an implementation of the <see cref="ILogger"/> class.
    /// </summary>
    public class LoggerFacade : ILogger
    {
        /// <summary>
        /// Logs the values to the logging store.
        /// </summary>
        /// <param name="message">The message that is being logged.</param>
        public void Log(string message)
        {
            Logger.Write(message);
        }

        /// <summary>
        /// Logs the values to the logging store.
        /// </summary>
        /// <param name="message">The message that is being logged.</param>
        /// <param name="category">Category name used to route the log entry.</param>
        public void Log(string message, string category)
        {
            Logger.Write(message, category);
        }

        /// <summary>
        /// Logs the values to the logging store.
        /// </summary>
        /// <param name="message">The message that is being logged.</param>
        /// <param name="severity">Identifies the severity of the entry.</param>
        public void Log(string message, TraceEventType severity)
        {
            Logger.Write(message, (LAgger.TraceEventType)severity);
        }

        /// <summary>
        /// Logs the values to the logging store.
        /// </summary>
        /// <param name="message">The message that is being logged.</param>
        /// <param name="category">Category name used to route the log entry.</param>
        /// <param name="priority">Indicates the priority of the log entry.</param>
        public void Log(string message, string category, int priority)
        {
            Logger.Write(message, category, priority);
        }

        /// <summary>
        /// Logs the values to the logging store.
        /// </summary>
        /// <param name="message">The message that is being logged.</param>
        /// <param name="category">Category name used to route the log entry.</param>
        /// <param name="priority">Indicates the priority of the log entry.</param>
        /// <param name="eventId">The event id to use to identify the event.</param>
        public void Log(string message, string category, int priority, int eventId)
        {
            Logger.Write(message, category, priority, eventId);
        }

        /// <summary>
        /// Logs the values to the logging store.
        /// </summary>
        /// <param name="message">The message that is being logged.</param>
        /// <param name="category">Category name used to route the log entry.</param>
        /// <param name="priority">Indicates the priority of the log entry.</param>
        /// <param name="eventId">The event id to use to identify the event.</param>
        /// <param name="severity">Identifies the severity of the entry.</param>
        public void Log(string message, string category, int priority, int eventId, TraceEventType severity)
        {
            Logger.Write(message, category, priority, eventId, (LAgger.TraceEventType)severity);
        }

        /// <summary>
        /// Logs the values to the logging store.
        /// </summary>
        /// <param name="message">The message that is being logged.</param>
        /// <param name="category">Category name used to route the log entry.</param>
        /// <param name="priority">Indicates the priority of the log entry.</param>
        /// <param name="eventId">The event id to use to identify the event.</param>
        /// <param name="severity">Identifies the severity of the entry.</param>
        /// <param name="title">The title of the entry.</param>
        public void Log(string message, string category, int priority, int eventId, TraceEventType severity, string title)
        {
            Logger.Write(message, category, priority, eventId, (LAgger.TraceEventType)severity, title);
        }

        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="category">Category name used to route the log entry.</param>
        /// <param name="ex">The instance of exception.</param>
        public void Log(string category, Exception ex)
        {
            FaultException<ExceptionDetail> fault = ex as FaultException<ExceptionDetail>;

            string message;

            if (fault != null)
            {
                message = UtilityHelper.FormatExceptionDetailMessage(fault.Detail.Message, fault.Detail.StackTrace);
            }
            else
            {
                message = UtilityHelper.FormatExceptionMessage(ex);
            }

            this.Log(message, category, 1, 0, TraceEventType.Error);
        }
    }
}