// <copyright file="LogEntry.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: LogEntry.cs                     
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
    using System.Runtime.Serialization;

    /// <summary>
    /// Is a class used to represent logging data for LAgger.
    /// </summary>
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/2.1/LAgger/")]
    public class LogEntry : Entry
    {        
        /// <summary>
        /// Backing field for the <see cref="AppDomainName"/>.
        /// </summary>
        private string appDomainName;

        /// <summary>
        /// Backing field for the <see cref="ThreadName"/>.
        /// </summary>
        private string threadName;

        /// <summary>
        /// Indicates if the <see cref="Timestamp"/> has been initialized.
        /// </summary>
        private bool timestampInitialized;

        /// <summary>
        /// Indicates if the <see cref="AppDomainName"/> has been initialized.
        /// </summary>
        private bool appDomainNameInitialized;

        /// <summary>
        /// Indicates if the <see cref="ThreadName"/> has been initialized.
        /// </summary>
        private bool threadNameInitialized;

        /// <summary>
        /// Backing field for the <see cref="Timestamp"/>.
        /// </summary>
        private DateTime timestamp = DateTime.MaxValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntry"/> class.
        /// </summary>
        public LogEntry()
        {
            // do no additional work in this constructor
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntry"/> class with a full set of constructor parameters.
        /// </summary>
        /// <param name="message"><see cref="Message"/> of the <see cref="LogEntry"/>.</param>
        /// <param name="category"><see cref="Category"/> of the <see cref="LogEntry"/>.</param>
        /// <param name="priority"><see cref="Priority"/> of the <see cref="LogEntry"/>.</param>
        /// <param name="eventId"><see cref="EventId"/> of the <see cref="LogEntry"/>.</param>
        /// <param name="severity"><see cref="Severity"/> of the <see cref="LogEntry"/>.</param>
        /// <param name="title"><see cref="Title"/> of the <see cref="LogEntry"/>.</param>
        public LogEntry(object message, string category, int priority, int eventId, TraceEventType severity, string title)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            this.Message = message.ToString();
            this.Category = category;
            this.Priority = priority;
            this.EventId = eventId;
            this.Severity = severity;
            this.Title = title;
        }
        
        /// <summary>
        /// Gets or sets the message body to log.  Value from ToString() method from message object.
        /// </summary>
        /// <value>The nmessage body to log entry.</value>
        [DataMember]
        public string Message
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the importance of the log message.  Only messages whose priority is between the minimum and maximum priorities (inclusive)
        /// will be processed.
        /// </summary>
        /// <value>The importance of the log entry.</value>
        [DataMember]
        public int Priority
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the severity as a <see cref="TraceEventType"/> enumeration. (Error, Debug, Info, Warn).
        /// </summary>
        /// <value>The severity of the log entry.</value>
        [DataMember]
        public TraceEventType Severity
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the title of the log entry message.
        /// </summary>
        /// <value>The title of the log entry.</value>
        [DataMember]
        public string Title
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets event number or identifier.
        /// </summary>
        /// <value>The event number of the log entry.</value>
        [DataMember]
        public int EventId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets category name used to route the log entry.
        /// </summary>
        /// <value>Contains the category name used to route the log entry.</value>
        [DataMember]
        public string Category
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the name of the thread.
        /// </summary>
        /// <value>Contains the name of the thread.</value>
        [DataMember]
        public string ThreadName
        {
            get
            {
                if (!this.threadNameInitialized)
                {
                    this.threadName = System.Threading.Thread.CurrentThread.Name;
                }
                
               return this.threadName; 
            }

            set 
            {
                this.threadName = value;
                this.threadNameInitialized = true;
            }
        }
        
        /// <summary>
        /// Gets or sets the date and time of the log entry message.
        /// </summary>
        /// <value>Contains the datetime used in the log entry.</value>
        [DataMember]
        public DateTime Timestamp
        {
            get
            {
                if (!this.timestampInitialized)
                {
                    this.timestamp = DateTime.UtcNow;
                }

                return this.timestamp;
            }

            set
            {
                this.timestamp = value;
                this.timestampInitialized = true;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="AppDomain"/> name in which the program is running.
        /// </summary>
        /// <value>Contains the AppDomain name in which the program is running.</value>
        [DataMember]
        public string AppDomainName
        {
            get
            {
                if (!this.appDomainNameInitialized)
                {
                    this.appDomainName = AppDomain.CurrentDomain.FriendlyName;
                }

                return this.appDomainName;
            }

            set
            {
                this.appDomainName = value;
                this.appDomainNameInitialized = true;
            }
        }

        /// <summary>
        /// Creates a new <see cref="LogEntry"/> that is a copy of the current instance.
        /// </summary>
        /// <returns>A copy of the current <see cref="LogEntry"/>.</returns>
        public object Clone()
        {
            LogEntry result = new LogEntry();

            result.Message = this.Message;
            result.EventId = this.EventId;
            result.Title = this.Title;
            result.Severity = this.Severity;
            result.Priority = this.Priority;
            result.Category = this.Category;

            result.Timestamp = this.Timestamp;
            result.AppDomainName = this.AppDomainName;
           
            return result;
        }
    }
}
