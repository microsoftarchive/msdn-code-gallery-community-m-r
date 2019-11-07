// <copyright file="TimelineBarRegistry.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimelineBarRegistry.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services
{
    using System;
    using System.Collections.Generic;
    using Infrastructure;
    using Infrastructure.Models;

    using RCE.Infrastructure.Services;

    /// <summary>
    /// Registry used to register timeline bar elements.
    /// </summary>
    public class TimelineBarRegistry : ITimelineBarRegistry
    {
        /// <summary>
        /// Contains the registered elements.
        /// </summary>
        private readonly IDictionary<string, Func<ITimelineBarElementModel>> registry;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineBarRegistry"/> class.
        /// </summary>
        public TimelineBarRegistry()
        {
            this.registry = new Dictionary<string, Func<ITimelineBarElementModel>>();
        }

        /// <summary>
        /// Registers a Func that returns a <see cref="ITimelineBarElementModel"/>.
        /// </summary>
        /// <param name="key">The key used to identify the registration.</param>
        /// <param name="value">The func associated with the key.</param>
        public void RegisterTimelineBarElement(string key, Func<ITimelineBarElementModel> value)
        {
            this.registry.Add(key, value);
        }

        /// <summary>
        /// Gets a <see cref="ITimelineBarElementModel"/> associated with a key.
        /// </summary>
        /// <param name="key">The key used to get the registration value.</param>
        /// <returns>A <see cref="ITimelineBarElementModel"/> associated with the key.</returns>
        public ITimelineBarElementModel GetTimelineBarElement(string key)
        {
            if (this.registry.ContainsKey(key))
            {
                return this.registry[key].Invoke();
            }

            return null;
        }

        /// <summary>
        /// Gets a lists of the registered keys.
        /// </summary>
        /// <returns>The lists of registered keys.</returns>
        public IList<string> GetTimelineBarElementKeys()
        {
            return new List<string>(this.registry.Keys);
        }
    }
}