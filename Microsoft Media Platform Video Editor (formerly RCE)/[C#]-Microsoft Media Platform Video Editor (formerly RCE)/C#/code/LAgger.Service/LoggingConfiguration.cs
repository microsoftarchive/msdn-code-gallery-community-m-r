// <copyright file="LoggingConfiguration.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: LoggingConfiguration.cs                     
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
    using System.Runtime.Serialization;

    /// <summary>
    /// The LoggingConfiguration class is used to load config settings for LAgger.
    /// </summary>
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/2.1/LAgger/")]
    public class LoggingConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingConfiguration"/> class.
        /// </summary>
        public LoggingConfiguration()
        {
        }

        /// <summary>
        /// Gets or sets <see cref="LogLevel"/> for client application.
        /// </summary>
        /// <value>Contains the log level for the client applicaiton.</value>
        [DataMember]
        public int LogLevel
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets <see cref="BatchSize"/> for queue.
        /// </summary>
        /// <value>Contains the batch size used in the queue.</value>
        [DataMember]
        public int BatchSize
        {
            get;
            set;
        }

        /// <summary>
        ///  Gets or sets a value indicating whether or not the tracing is enabled.
        /// </summary>
        /// <value>Contains true if the tracing is enable;otherwise false.</value>
        [DataMember]
        public bool TracingEnabled
        {
            get;
            set;
        }
    }
}
