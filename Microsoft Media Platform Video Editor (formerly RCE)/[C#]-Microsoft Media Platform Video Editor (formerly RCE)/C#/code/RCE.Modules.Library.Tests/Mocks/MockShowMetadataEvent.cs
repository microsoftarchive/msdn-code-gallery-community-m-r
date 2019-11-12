// <copyright file="MockShowMetadataEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockShowMetadataEvent.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Library.Tests.Mocks
{
    using System;
    using Infrastructure.Events;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Events;

    public class MockShowMetadataEvent : ShowMetadataEvent
    {
        public bool PublishCalled { get; set; }

        public TimelineElement Payload { get; set; }

        public Action<TimelineElement> SubscribeArgumentAction { get; set; }

        public Predicate<TimelineElement> SubscribeArgumentFilter { get; set; }

        public ThreadOption SubscribeArgumentThreadOption { get; set; }

        public override SubscriptionToken Subscribe(Action<TimelineElement> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<TimelineElement> filter)
        {
            this.SubscribeArgumentAction = action;
            this.SubscribeArgumentFilter = filter;
            this.SubscribeArgumentThreadOption = threadOption;

            return null;
        }

        public override void Publish(TimelineElement payload)
        {
            this.PublishCalled = true;
            this.Payload = payload;
        }
    }
}