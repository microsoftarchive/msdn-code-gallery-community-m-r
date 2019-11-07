// <copyright file="MockLogEntryCollection.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockLogEntryCollection.cs                     
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
    using System.Collections.Generic;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;

    public class MockLogEntryCollection : ILogEntryCollection
    {
        private readonly List<EventData> items;

        public MockLogEntryCollection()
        {
            this.items = new List<EventData>();
        }

        public event EventHandler<DataEventArgs<EventData>> EventDataAdded;

        public event EventHandler<DataEventArgs<EventData>> EventDataRemoved;

        public List<EventData> ItemCollection 
        {
            get
            {
                return this.items;
            }
        }

        public IEnumerable<EventData> Items
        {
            get
            {
                return this.items;
            }
        }

        public IList<string> LogStreams
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool UseAllLogStreams
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void SetLogEntriesSource(object source)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public void InvokeEventDataRemoved(EventData e)
        {
            EventHandler<DataEventArgs<EventData>> handler = this.EventDataRemoved;
            if (handler != null)
            {
                handler(this, new DataEventArgs<EventData>(e));
            }
        }

        public void InvokeEventDataAdded(EventData e)
        {
            EventHandler<DataEventArgs<EventData>> handler = this.EventDataAdded;
            if (handler != null)
            {
                handler(this, new DataEventArgs<EventData>(e));
            }
        }
    }
}
