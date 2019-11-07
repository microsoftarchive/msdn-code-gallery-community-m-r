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

namespace RCE.Modules.CommentsBar.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using Infrastructure;
    using Infrastructure.Models;

    using RCE.Infrastructure.Services;

    public class MockTimelineBarRegistry : ITimelineBarRegistry
    {
        public IList<string> GetTimelineBarElementKeysReturnValue { get; set; }

        public Func<string, ITimelineBarElementModel> GetTimelineBarElementFunction { get; set; }

        public void RegisterTimelineBarElement(string key, Func<ITimelineBarElementModel> value)
        {
            throw new NotImplementedException();
        }

        public ITimelineBarElementModel GetTimelineBarElement(string key)
        {
            return this.GetTimelineBarElementFunction(key);
        }

        public IList<string> GetTimelineBarElementKeys()
        {
            return this.GetTimelineBarElementKeysReturnValue;
        }
    }
}