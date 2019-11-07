// <copyright file="MockTimelineBarRegistry.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockTimelineBarRegistry.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Markers.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using Infrastructure;
    using Infrastructure.Models;

    using RCE.Infrastructure.Services;

    public class MockTimelineBarRegistry : ITimelineBarRegistry
    {
        public bool RegisterTimelineBarElementCalled { get; set; }

        public string RegisterTimelineBarElementKeyArgument { get; set; }

        public Func<ITimelineBarElementModel> RegisterTimelineBarElementValueArgument { get; set; }

        public void RegisterTimelineBarElement(string key, Func<ITimelineBarElementModel> value)
        {
            this.RegisterTimelineBarElementCalled = true;
            this.RegisterTimelineBarElementKeyArgument = key;
            this.RegisterTimelineBarElementValueArgument = value;
        }

        public ITimelineBarElementModel GetTimelineBarElement(string key)
        {
            throw new NotImplementedException();
        }

        public IList<string> GetTimelineBarElementKeys()
        {
            throw new NotImplementedException();
        }
    }
}