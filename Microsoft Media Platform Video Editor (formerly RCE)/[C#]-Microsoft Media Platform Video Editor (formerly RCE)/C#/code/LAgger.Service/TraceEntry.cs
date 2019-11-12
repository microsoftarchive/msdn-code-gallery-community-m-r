// <copyright file="TraceEntry.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TraceEntry.cs                     
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
    /// A class that represents data from a trace.
    /// </summary>
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/2.1/LAgger/")]
    public class TraceEntry : Entry
    {
        /// <summary>
        /// Gets or sets the ID used to correlate a single execution path between multiple trace events.
        /// </summary>
        /// <value>Contians the ID used to correlate a single execution path between multiple trace events.</value>
        [DataMember]
        public Guid ActivityId
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the operation that the trace is being performed on.
        /// </summary>
        /// <value>Contains the operation being performed by the trace.</value>
        [DataMember]
        public string Operation
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the name of the method that the trace was called from.
        /// </summary>
        /// <value>Contains the name of the method that the trace was called from.</value>
        [DataMember]
        public string Method
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the duration that the trace was run.
        /// </summary>
        /// <value>Contains the duration that the trace was run.</value>
        [DataMember]
        public TimeSpan Duration
        {
            get; set;
        }
    }
}
