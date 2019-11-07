// <copyright file="MockLoggerService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockLoggerService.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace LAgger.Service.Test.Mocks
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class MockLoggerService : LoggerService
    {
        public bool MixEntryCollection
        {
            get { return this.TraceEntryCollection && this.LogEntryCollection; }
        }

        public bool TraceEntryCollection { get; set; }

        public bool LogEntryCollection { get; set; }

        public int LogCount { get; set; }

        public int TraceCount { get; set; }

        public int TotalCount
        {
            get { return this.LogCount + this.TraceCount; }
        }

        public bool ValidateEntry(LogEntry logEntry)
        {
            var expected = (System.Diagnostics.TraceEventType)Enum.Parse(typeof(System.Diagnostics.TraceEventType), Enum.GetName(typeof(TraceEventType), logEntry.Severity));

            if (expected != null)
            {
                return true;
            }

            return false;
        }

        protected override void WriteEntry(LogEntry entry)
        {
            this.LogEntryCollection = true;
            Assert.IsInstanceOfType(entry, typeof(LogEntry), "Expected Log Entry Instance");
            this.LogCount++;
        }

        protected override void WriteEntry(TraceEntry entry)
        {
            Assert.IsInstanceOfType(entry, typeof(TraceEntry), "Expected Trace Entry Instance");
            this.TraceEntryCollection = true;
            this.TraceCount++;
        }
    }
}
