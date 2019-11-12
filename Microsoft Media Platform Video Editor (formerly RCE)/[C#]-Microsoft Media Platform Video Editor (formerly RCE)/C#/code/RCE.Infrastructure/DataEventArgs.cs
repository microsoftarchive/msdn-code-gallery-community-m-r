// <copyright file="DataEventArgs.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DataEventArgs.cs                     
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
    using System.Runtime.Serialization;

    /// <summary>
    /// Generic class to transfer given data in the <see cref="EventArgs"/>.
    /// </summary>
    /// <typeparam name="T">The <see cref="object"/>.</typeparam>
    public class DataEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the DataEventArgs class.
        /// </summary>
        /// <param name="data">The <see cref="T"/> instance.</param>
        public DataEventArgs(T data)
            : this(data, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DataEventArgs class.
        /// </summary>
        /// <param name="error">The error ocurred.</param>
        public DataEventArgs(Exception error)
            : this(default(T), error)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DataEventArgs class.
        /// </summary>
        /// <param name="data">The <see cref="T"/> instance.</param>
        /// <param name="error">The error ocurred.</param>
        public DataEventArgs(T data, Exception error)
        {
            this.Data = data;
            this.Error = error;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The <typeparam name="T">object</typeparam>.</value>
        public T Data { get; private set; }

        /// <summary>
        /// Gets the exception ocurred.
        /// </summary>
        /// <value>The <seealso cref="Exception"/>.</value>
        public Exception Error { get; private set; }
    }
}