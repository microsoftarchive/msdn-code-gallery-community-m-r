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

namespace RCE.Services.Tests.Mocks
{
    using LAgger;

    public class MockLoggerService : ILoggerService
    {
        public bool LogEntriesCalled { get; set; }

        public Entry[] LogEntriesArgument { get; set; }

        public void LogEntries(Entry[] entries)
        {
            this.LogEntriesCalled = true;
            this.LogEntriesArgument = entries;
        }

        public LoggingConfiguration DistributeConfiguration()
        {
            throw new System.NotImplementedException();
        }
    }
}
