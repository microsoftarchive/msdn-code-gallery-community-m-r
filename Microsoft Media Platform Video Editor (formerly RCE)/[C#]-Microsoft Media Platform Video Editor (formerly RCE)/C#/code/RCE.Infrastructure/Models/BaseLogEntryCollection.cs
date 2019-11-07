// <copyright file="BaseLogEntryCollection.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: BaseLogEntryCollection.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Linq;

    using RCE.Infrastructure.Services;

    public abstract class BaseLogEntryCollection : ILogEntryCollection
    {
        /// <summary>
        /// Contains the lists of log streams to parse.
        /// </summary>
        private readonly List<string> logStreams;

        private readonly IEventDataParser<EventData> eventDataParser;

        private readonly IEventDataParser<EventOffset> eventOffsetParser;

        private readonly IDictionary<Guid, LogEntry> logEntries;

        private TimeSpan currentEventOffset;

        protected BaseLogEntryCollection(IEventDataParser<EventData> eventDataParser, IEventDataParser<EventOffset> eventOffsetParser)
        {
            this.eventDataParser = eventDataParser;
            this.eventOffsetParser = eventOffsetParser;
            this.logStreams = new List<string>();
            this.logEntries = new Dictionary<Guid, LogEntry>();
        }

        public event EventHandler<DataEventArgs<EventData>> EventDataAdded;

        public event EventHandler<DataEventArgs<EventData>> EventDataRemoved;

        public IEnumerable<EventData> Items
        {
            get
            {
                var items = new List<EventData>();
                this.logEntries.Values.Select(le => le.DataEntries).ForEach(items.AddRange);
                return items;
            }
        }

        public IList<string> LogStreams
        {
            get
            {
                return this.logStreams;
            }
        }

        public bool UseAllLogStreams { get; set; }

        protected IDictionary<Guid, LogEntry> LogEntries 
        {
            get
            {
                return this.logEntries;
            }
        }

        protected IEventDataParser<EventData> EventDataParser
        {
            get
            {
                return this.eventDataParser;
            }
        }

        public virtual void SetLogEntriesSource(object source)
        {
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">Indicates if Dispose is being called from the Dispose method.</param>
        protected abstract void Dispose(bool disposing);

        protected void RemoveEvent(XElement element, Guid targetId)
        {
            foreach (var child in element.Elements())
            {
                EventOffset eventOffset = this.eventOffsetParser.ParseEventData(child);

                if (eventOffset != null)
                {
                    var offset = TimeSpan.FromSeconds(eventOffset.Offset);
                    this.Items.ForEach(ed => ed.Time.Add(offset));
                    this.currentEventOffset = TimeSpan.Zero;
                }
            }

            int initialCount = 0;
            int finalCount = 0;
            this.logEntries.Values.ForEach(edl => initialCount += edl.DataEntries.Count);

            if (targetId != Guid.Empty)
            {
                if (this.logEntries.ContainsKey(targetId))
                {
                    if (this.logEntries[targetId].Offset != TimeSpan.Zero)
                    {
                        this.Items.ForEach(ed => ed.Time.Subtract(this.logEntries[targetId].Offset));
                        this.currentEventOffset = TimeSpan.Zero;
                    }
                    else
                    {
                        foreach (var entry in this.logEntries[targetId].DataEntries.ToList())
                        {
                            this.logEntries[targetId].DataEntries.Remove(entry);
                            this.InvokeEventDataRemoved(entry);
                        }
                    }
                }

                this.logEntries.Values.ForEach(edl => finalCount += edl.DataEntries.Count);
                if (initialCount == finalCount)
                {
                    foreach (LogEntry logEntry in this.logEntries.Values)
                    {
                        if (logEntry.TargetId == targetId && this.logEntries.ContainsKey(targetId))
                        {
                            if (this.logEntries[targetId].Offset != TimeSpan.Zero)
                            {
                                this.Items.ForEach(ed => ed.Time.Subtract(this.logEntries[targetId].Offset));
                                this.currentEventOffset = TimeSpan.Zero;
                            }
                            else
                            {
                                foreach (var entry in this.logEntries[targetId].DataEntries.ToList())
                                {
                                    this.logEntries[targetId].DataEntries.Remove(entry);
                                    this.InvokeEventDataRemoved(entry);
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void AddEvent(XElement element, Guid id)
        {
            foreach (XElement child in element.Elements())
            {
                EventOffset eventOffset = this.eventOffsetParser.ParseEventData(element);

                if (eventOffset != null)
                {
                    this.currentEventOffset = TimeSpan.FromSeconds(eventOffset.Offset);
                    this.Items.ForEach(ed => ed.Time.Add(this.currentEventOffset));
                    this.logEntries[id].Offset = this.currentEventOffset;
                }
                else
                {
                    EventData eventData = this.eventDataParser.ParseEventData(child);

                    this.logEntries[id].DataEntries.Add(eventData);
                    this.InvokeEventDataAdded(eventData);
                }
            }
        }

        protected virtual void ParseMarkerEntry(XElement element)
        {
            Guid id = element.Attribute("Id").GetValueAsGuid();

            if (id != Guid.Empty && !this.LogEntries.ContainsKey(id))
            {
                Guid targetId = element.Attribute("TargetId").GetValueAsGuid();
                this.LogEntries[id] = new LogEntry(targetId);
                string action = element.Attribute("Action").GetValue().ToUpper(CultureInfo.InvariantCulture);

                switch (action)
                {
                    case "ADD":
                        this.AddEvent(element, id);
                        break;

                    case "REMOVE":
                        this.RemoveEvent(element, targetId);
                        break;

                    case "REPLACE":
                        this.RemoveEvent(element, targetId);
                        this.AddEvent(element, id);
                        break;
                }
            }
        }

        protected void InvokeEventDataAdded(EventData data)
        {
            EventHandler<DataEventArgs<EventData>> handler = this.EventDataAdded;
            if (handler != null)
            {
                handler(this, new DataEventArgs<EventData>(data));
            }
        }

        private void InvokeEventDataRemoved(EventData data)
        {
            EventHandler<DataEventArgs<EventData>> handler = this.EventDataRemoved;
            if (handler != null)
            {
                handler(this, new DataEventArgs<EventData>(data));
            }
        }

        protected class LogEntry
        {
            public LogEntry(Guid targetId)
            {
                this.TargetId = targetId;
                this.DataEntries = new List<EventData>();
            }

            public List<EventData> DataEntries { get; private set; }

            public Guid TargetId { get; private set; }

            public TimeSpan Offset { get; set; }
        }
    }
}
