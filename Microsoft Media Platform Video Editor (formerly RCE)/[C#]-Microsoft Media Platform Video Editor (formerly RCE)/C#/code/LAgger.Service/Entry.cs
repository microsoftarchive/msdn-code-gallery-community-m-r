// <copyright file="Entry.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Entry.cs                     
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
    using System.Xml.Serialization;

    /// <summary>
    /// An abstract class used as a base for LAgger to pass messages.
    /// </summary>
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/LAgger/")]
    [KnownType(typeof(LogEntry))]
    [KnownType(typeof(TraceEntry))]
    [XmlInclude(typeof(LogEntry))]
    [XmlInclude(typeof(TraceEntry))]
    public abstract class Entry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entry"/> class with default values.
        /// </summary>
        protected Entry()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets a unique identifer for an <see cref="Entry"/>.
        /// </summary>
        /// <value>An unique identifier.</value>
        [DataMember]
        public Guid Id
        {
            get; set;
        }
    }
}
