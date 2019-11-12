// <copyright file="IPurgeStrategy.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IPurgeStrategy.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provide success to common members of purge strategies. 
    /// </summary>
    public interface IPurgeStrategy
    {
        event EventHandler<DataEventArgs<IEnumerable<string>>> ItemsPurged;
 
        /// <summary>
        /// Purges a provided cache.
        /// </summary>
        /// <param name="cache">
        /// The cache to purge.
        /// </param>
        void Purge(ICache cache);
    }
}
