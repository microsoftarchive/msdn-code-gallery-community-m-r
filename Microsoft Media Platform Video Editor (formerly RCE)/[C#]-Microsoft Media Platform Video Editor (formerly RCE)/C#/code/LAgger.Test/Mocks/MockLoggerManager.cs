// <copyright file="MockLoggerManager.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockLoggerManager.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace LAgger.Test
{
    using System;
    using LAgger;

    /// <summary>
    /// A class used to get around testing ILoggerManager.
    /// </summary>
    public class MockLoggerManager : ILoggerManager
    {
        /// <summary>
        /// Gets or sets the value of the entry that was written to in Write.
        /// </summary>
        public Entry Entry
        {
            get; private set;
        }

        /// <summary>
        /// Indicates if Initialize was called.
        /// </summary>
        public bool Initialized
        {
            get; set;
        }

        public void Initialize(Uri uri)
        {
            this.Initialized = true;
        }

        /// <summary>
        /// Implements the write. This will expose the entry object as a public property.
        /// </summary>
        /// <param name="entry"></param>
        public void Write(Entry entry)
        {
            this.Entry = entry;
        }
    }
}
