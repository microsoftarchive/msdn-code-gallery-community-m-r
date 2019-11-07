// <copyright file="ILogEntryCollection.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ILogEntryCollection.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure
{
    using System;

    using System.Collections.Generic;

    using RCE.Infrastructure.Models;

    public interface ILogEntryCollection : IDisposable
    {
        event EventHandler<DataEventArgs<EventData>> EventDataAdded;

        event EventHandler<DataEventArgs<EventData>> EventDataRemoved;

        IEnumerable<EventData> Items { get; }

        IList<string> LogStreams { get; }

        bool UseAllLogStreams { get; set; }

        void SetLogEntriesSource(object source);
    }
}
