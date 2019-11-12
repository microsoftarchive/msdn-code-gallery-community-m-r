// <copyright file="ILoggerManager.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ILoggerManager.cs                     
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

    /// <summary>
    /// Interface used to define what type of logger manager to use.
    /// </summary>
    public interface ILoggerManager
    {
        /// <summary>
        /// Performs any initialization necessary for the <see cref="ILoggerManager"/>.
        /// </summary>
        void Initialize(Uri loggerServiceUri);

        /// <summary>
        /// Persists the information for the details of the entry.
        /// </summary>
        /// <param name="entry">The information to be stored.</param>
        void Write(Entry entry);
    }
}
