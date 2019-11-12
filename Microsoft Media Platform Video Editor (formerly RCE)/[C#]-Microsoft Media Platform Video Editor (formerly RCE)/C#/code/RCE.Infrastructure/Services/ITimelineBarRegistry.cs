// <copyright file="ITimelineBarRegistry.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ITimelineBarRegistry.cs                     
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

    using RCE.Infrastructure.Models;

    /// <summary>
    /// Registry used to register timeline bar elements.
    /// </summary>
    public interface ITimelineBarRegistry
    {
        /// <summary>
        /// Registers a Func that returns a <see cref="ITimelineBarElementModel"/>.
        /// </summary>
        /// <param name="key">The key used to identify the registration.</param>
        /// <param name="value">The func associated with the key.</param>
        void RegisterTimelineBarElement(string key, Func<ITimelineBarElementModel> value);

        /// <summary>
        /// Gets a <see cref="ITimelineBarElementModel"/> associated with a key.
        /// </summary>
        /// <param name="key">The key used to get the registration value.</param>
        /// <returns>A <see cref="ITimelineBarElementModel"/> associated with the key.</returns>
        ITimelineBarElementModel GetTimelineBarElement(string key);

        /// <summary>
        /// Gets a lists of the registered keys.
        /// </summary>
        /// <returns>The lists of registered keys.</returns>
        IList<string> GetTimelineBarElementKeys();
    }
}