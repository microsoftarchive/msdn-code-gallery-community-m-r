// <copyright file="MockEventAggregator.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockEventAggregator.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Comment.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Practices.Composite.Events;

    public class MockEventAggregator : IEventAggregator
    {
        private readonly Dictionary<Type, object> events = new Dictionary<Type, object>();

        public TEventType GetEvent<TEventType>() where TEventType : EventBase
        {
            return (TEventType)this.events[typeof(TEventType)];
        }

        public void AddMapping<TEventType>(TEventType mockEvent)
        {
            this.events.Add(typeof(TEventType), mockEvent);
        }
    }
}