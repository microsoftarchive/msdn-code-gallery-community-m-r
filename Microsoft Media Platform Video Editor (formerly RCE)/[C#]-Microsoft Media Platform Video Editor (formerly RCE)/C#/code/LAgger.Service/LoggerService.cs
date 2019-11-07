// <copyright file="LoggerService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: LoggerService.cs                     
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
    using System.Configuration;
    using System.Globalization;
    using System.ServiceModel.Activation;
    using Diag = System.Diagnostics;
    using EntLib = Microsoft.Practices.EnterpriseLibrary.Logging;
    
    /// <summary>
    /// An implementation of the <see cref="ILoggerService"/> that finally interacts with the Enterprise Library Logging Block.
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class LoggerService : ILoggerService
    {
        /// <summary>
        /// Collect Entry objects for backend processing.
        /// </summary>
        /// <param name="entries">The <see cref="LogEntry"/> object for backend processing.</param>
        public virtual void LogEntries(Entry[] entries)
        {
            if (entries != null && entries.Length > 0)
            {
                foreach (var checkEntry in entries)
                {
                    if (checkEntry != null)
                    {
                        var logEntry = checkEntry as LogEntry;
                        var traceEntry = checkEntry as TraceEntry;
                        if (logEntry != null)
                        {
                            this.WriteEntry(logEntry);
                        }
                        else if (traceEntry != null)
                        {
                            this.WriteEntry(traceEntry);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Load configuration stettings for LAgger.
        /// </summary>
        /// <returns>The <see cref="LoggingConfiguration"/> is used to load configuration settings for LAgger.</returns>
        public virtual LoggingConfiguration DistributeConfiguration()
        {
            var config = new LoggingConfiguration();
            config.BatchSize = Convert.ToInt32(ConfigurationManager.AppSettings["BatchSize"], CultureInfo.InvariantCulture);
            config.TracingEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["TracingEnabled"], CultureInfo.InvariantCulture);
            config.LogLevel = Convert.ToInt32(ConfigurationManager.AppSettings["LogLevel"], CultureInfo.InvariantCulture);
            return config;
        }

        /// <summary>
        /// Writes a log entry using enterpise library logging block.
        /// </summary>
        /// <param name="logEntry">The <see cref="LogEntry"/> that is to be written.</param>
        protected virtual void WriteEntry(LogEntry logEntry)
        {
            var entliblogEntry = new EntLib.LogEntry();
            entliblogEntry.Message = logEntry.Message;
            entliblogEntry.Priority = logEntry.Priority;
            entliblogEntry.Severity = (System.Diagnostics.TraceEventType)Enum.Parse(typeof(System.Diagnostics.TraceEventType), Enum.GetName(typeof(TraceEventType), logEntry.Severity));
            entliblogEntry.TimeStamp = logEntry.Timestamp;
            entliblogEntry.ManagedThreadName = logEntry.ThreadName;
            entliblogEntry.Title = logEntry.Title;
            entliblogEntry.Categories.Add(logEntry.Category);
            EntLib.Logger.Write(entliblogEntry);
        }

        /// <summary>
        /// Writes a trace entry using enterpise library logging block.
        /// </summary>
        /// <param name="traceEntry">The <see cref="TraceEntry"/> that is to be written.</param>
        protected virtual void WriteEntry(TraceEntry traceEntry)
        {
            var entlibTraceEntry = new EntLib.LogEntry();
            entlibTraceEntry.Message = traceEntry.Method + " " + traceEntry.Duration;
            entlibTraceEntry.Title = traceEntry.Operation;
            entlibTraceEntry.Categories.Add("Trace");
            EntLib.Logger.Write(entlibTraceEntry);
        }
    }
}