// <copyright file="CacheItem.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CacheItem.cs                     
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

    /// <summary>
    /// Object that represents a item cached.
    /// </summary>
    public class CacheItem
    {
        /// <summary>
        /// Gets or sets Date of a cached Item.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the content of a cached Item.
        /// </summary>
        public object CachedValue { get; set; }

        public bool AvoidDeserialization { get; set; }
    }
}
