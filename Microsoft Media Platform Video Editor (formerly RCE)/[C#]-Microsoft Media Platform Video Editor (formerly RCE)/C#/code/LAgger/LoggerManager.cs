// <copyright file="LoggerManager.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: LoggerManager.cs                     
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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.ServiceModel;
    using Services;
   
    /// <summary>
    /// Class used for managing the logging for Silverlight.
    /// </summary>
    public class LoggerManager : ILoggerManager
    {
        /// <summary>
        /// Used for holding onto a lock to keep state in sync.
        /// </summary>
        private static readonly object staticLockObject = new object();

        /// <summary>
        /// Used for taking locks at the class level.
        /// </summary>
        private readonly object lockObject = new object();

        /// <summary>
        /// Tracks if LoggerManager has been initialized.
        /// </summary>
        private static bool started;

        /// <summary>
        /// Tracks the <see cref="ILoggerManager"/> that is being used.
        /// </summary>
        private static ILoggerManager manager;

        /// <summary>
        /// The user state will be used to uniquely identify the entry collection.
        /// </summary>
        private static Guid userState;

        /// <summary>
        /// The queue that will hold the <see cref="Entry"/> objects that have not been filtered.
        /// </summary>
        private IQueue unfilteredQueue;

        /// <summary>
        /// The queue that will hold the <see cref="Entry"/> objects that have been filtered.
        /// </summary>
        private IQueue filteredQueue;

        /// <summary>
        /// The dictionary that will hold the <see cref="Entry"/> objects for recovery.
        /// </summary>
        private Dictionary<Guid, ObservableCollection<Entry>> entryCollector = new Dictionary<Guid, ObservableCollection<Entry>>();

        /// <summary>
        /// The WCF client to use for communicating to the <see cref="ILoggerService"/> WCF service.
        /// </summary>
        private LoggerServiceClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerManager"/> class. This class can only be created inside of LAgger.
        /// </summary>
        protected LoggerManager()
        {
            // for now there is nothing that is happening in the constructor
        }

        /// <summary>
        /// Gets a value indicating whether or not the LoggerManager has been started.
        /// </summary>
        /// <value>True if the LoggerManager is started;otherwise false.</value>
        public static bool Started
        {
            get { return started; }
        }

        /// <summary>
        /// Gets the <see cref="ILoggerManager"/> instance that is associated to the <see cref="LoggerManager"/> class.
        /// </summary>
        /// <value>The <see cref="ILoggerManager"/> instance that is associated to the <see cref="LoggerManager"/> class.</value>
        public static ILoggerManager Manager
        {
            get
            {
                // if (!started)
                // {
                //    Start();
                // }
                return manager;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="IQueue"/> that is used to contain filtered <see cref="LogEntry"/> objects.
        /// </summary>
        /// <value>The <see cref="IQueue"/> that is used to contain filtered <see cref="LogEntry"/> objects.</value>
        public IQueue FilteredQueue
        {
            get
            {
                return this.filteredQueue;
            }

            set
            {
                if (this.filteredQueue != value && this.filteredQueue != null)
                {
                    this.filteredQueue.EntryEnqueued -= this.FilteredEnqueued;
                }

                this.filteredQueue = value;

                if (this.filteredQueue != null)
                {
                    this.filteredQueue.EntryEnqueued += this.FilteredEnqueued;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="IQueue"/> that is used to contain unfiltered <see cref="LogEntry"/> objects.
        /// </summary>
        /// <value>The <see cref="IQueue"/> that is used to contain unfiltered <see cref="LogEntry"/> objects.</value>
        public IQueue UnfilteredQueue
        {
            get
            {
                return this.unfilteredQueue;
            }

            set
            {
                if (this.unfilteredQueue != value && this.unfilteredQueue != null)
                {
                    this.unfilteredQueue.EntryEnqueued -= this.UnfilteredEnqueued;
                }

                this.unfilteredQueue = value;

                if (this.unfilteredQueue != null)
                {
                    this.UnfilteredQueue.EntryEnqueued += this.UnfilteredEnqueued;
                }
            }
        }

        /// <summary>
        /// Gets or sets the loggingConfiguration for deciding what information to log.
        /// </summary>
        /// <value>Contains the logging configuration used to decide what information log.</value>
        protected LoggingConfiguration Configuration
        {
            get;
            set;
        }

        /// <summary>
        /// Performs an initialization of the LoggerManager with the default values.
        /// </summary>
        public static void Start(Uri loggerServiceUri)
        {
            Start(new LoggerManager { UnfilteredQueue = new MemoryQueue(), FilteredQueue = new MemoryQueue() }, loggerServiceUri);
        }

        /// <summary>
        /// Performs an intiailization of <see cref="LoggerManager"/> using the specified manager.
        /// </summary>
        /// <param name="loggerManager">Identifies the <see cref="ILoggerManager"/> to use.</param>
        public static void Start(ILoggerManager loggerManager, Uri loggerServiceUri)
        {
            lock (staticLockObject)
            {
                manager = loggerManager;
                manager.Initialize(loggerServiceUri);
                started = true;
            }
        }

        /// <summary>
        /// This method will perform the necessary filtering and transmission to the service for logging messages.
        /// </summary>
        /// <param name="entry">The data that is being logged.</param>
        public void Write(Entry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException("entry");
            }

            if (entry.Id == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException("entry", "Entry.Id must be a unique guid.");
            }

            this.UnfilteredQueue.Enqueue(entry);
        }

        /// <summary>
        /// Performs intialization steps on the <see cref="ILoggerManager"/>.
        /// </summary>
        public void Initialize(Uri loggerServiceUri)
        {
            try
            {
                this.LoadConfiguration(loggerServiceUri);
            }
            catch (FaultException ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// Filters through the current <see cref="Entry"/> objects in the <see cref="UnfilteredQueue"/>.
        /// </summary>
        protected virtual void FilterEntries()
        {
            if (this.Configuration != null && this.unfilteredQueue != null && this.filteredQueue != null)
            {
                while (this.unfilteredQueue.Count > 0)
                {
                    var entry = this.unfilteredQueue.Dequeue();
                    if (entry != null)
                    {
                        var logEntry = entry as LogEntry;
                        var traceEntry = entry as TraceEntry;

                        if (logEntry != null)
                        {
                            if ((int)logEntry.Severity <= this.Configuration.LogLevel)
                            {
                                this.filteredQueue.Enqueue(logEntry);
                            }
                        }
                        else if (traceEntry != null)
                        {
                            if (this.Configuration.TracingEnabled)
                            {
                                this.filteredQueue.Enqueue(traceEntry);
                            }
                        }
                        else
                        {
                            throw new NotSupportedException();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Loads up the loggingConfiguration from the remote web service.
        /// </summary>
        protected virtual void LoadConfiguration(Uri loggerServiceUri)
        {
            lock (this.lockObject)
            {
                try
                {
                    BasicHttpSecurityMode securityMode = loggerServiceUri.Scheme.ToUpper(CultureInfo.InvariantCulture) == "HTTPS" ? BasicHttpSecurityMode.Transport : BasicHttpSecurityMode.None;

                    BasicHttpBinding binding = new BasicHttpBinding(securityMode)
                                                   {
                                                       MaxBufferSize = 2147483647,
                                                       MaxReceivedMessageSize = 2147483647
                                                   };

                    this.client = new LoggerServiceClient(binding, new EndpointAddress(loggerServiceUri));
                    this.client.DistributeConfigurationCompleted += this.ConfigurationLoaded;
                    this.client.LogEntriesCompleted += this.CollectEnteries;
                    this.client.DistributeConfigurationAsync();
                }
                catch (InvalidOperationException)
                {
                    // unable to load the client configuration.
                    // this error will be ignored and no messages will be sent out.
                    // instead they will get stored in the queue and sent once a config is loaded.
                }
            }
        }

        /// <summary>
        /// Sends the entry objects to the LoggerProxy.
        /// </summary>
        protected virtual void PublishEntries()
        {
            if (this.Configuration != null && this.filteredQueue.Count >= this.Configuration.BatchSize)
            {
                lock (this.lockObject)
                {
                    var entries = new ObservableCollection<Entry>();
                    
                    while (this.filteredQueue.Count > 0)
                    {
                        var entry = this.filteredQueue.Dequeue();
                        entries.Add(entry);
                    }
                    
                    userState = Guid.NewGuid();
                    this.entryCollector.Add(userState, entries);
                    this.SendEntries(entries);
                }
            }
        }

        /// <summary>
        /// Sends a batch of entries to the WCF client.
        /// </summary>
        /// <param name="entries">The <see cref="Entry"/> objects to be sent to the WCF client.</param>
        protected virtual void SendEntries(ObservableCollection<Entry> entries)
        {
            if (this.client == null)
            {
                foreach (var entry in entries)
                {
                    this.filteredQueue.Enqueue(entry);
                }
            }
            else
            {
                this.client.LogEntriesAsync(entries, userState);
            }
        }

        /// <summary>
        /// Handles the event that is raised when the enteries are collected from the <see cref="ILoggerService"/>.
        /// </summary>
        /// <param name="sender">The originator of the event.</param>
        /// <param name="args">Arguments passed from the event.</param>
        private void CollectEnteries(object sender, AsyncCompletedEventArgs args)
        {
            if (!args.Cancelled)
            {
                if (args.Error == null)
                {
                    if (this.entryCollector.Count > 0)
                    {
                        if (this.entryCollector.ContainsKey((Guid)args.UserState))
                        {
                            this.entryCollector.Remove((Guid)args.UserState);
                        }
                    }
                }
                else
                {
                    var entries = this.entryCollector[(Guid)args.UserState];
                        
                    foreach (Entry entry in entries)
                    {
                        this.filteredQueue.Enqueue(entry);
                    }

                    this.entryCollector.Remove((Guid)args.UserState);
                }
            }
        }

        /// <summary>
        /// Handles the event that is raised when the loggingConfiguration is loaded from the <see cref="ILoggerService"/>.
        /// </summary>
        /// <param name="sender">The originator of the event.</param>
        /// <param name="args">Arguments passed from the event.</param>
        private void ConfigurationLoaded(object sender, DistributeConfigurationCompletedEventArgs args)
        {
            if (!args.Cancelled)
            {
                if (args.Error == null)
                {
                    lock (this.lockObject)
                    {
                        this.Configuration = args.Result;
                    }
                }
                else
                {
                    throw new ConfigurationLoadException("Failed to load the loggingConfiguration.", args.Error);
                }
            }
        }

        /// <summary>
        /// Used for handling events when an item has been enqueued in the <see cref="UnfilteredQueue"/>.
        /// </summary>
        /// <param name="sender">The originator of the event.</param>
        /// <param name="args">Arguments passed from the event.</param>
        private void UnfilteredEnqueued(object sender, EntryEnqueuedEventArgs args)
        {
            this.FilterEntries();
        }

        /// <summary>
        /// Used for handling events when an item has been enqueued in the <see cref="FilteredQueue"/>.
        /// </summary>
        /// <param name="sender">The originator of the event.</param>
        /// <param name="args">The arguments being passed from the event.</param>
        private void FilteredEnqueued(object sender, EntryEnqueuedEventArgs args)
        {
            this.PublishEntries();
        }
    }
}
