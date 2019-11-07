// <copyright file="MockStartTimeCodeChangedEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockStartTimeCodeChangedEvent.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline.Tests.Mocks
{
    using System;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Events;
    using RCE.Infrastructure.Events;
    using SMPTETimecode;

    public class MockStartTimeCodeChangedEvent : StartTimeCodeChangedEvent
    {
        public Action<TimeCode> SubscribeArgumentAction { get; set; }

        public Predicate<TimeCode> SubscribeArgumentFilter { get; set; }

        public ThreadOption SubscribeArgumentThreadOption { get; set; }

        public override SubscriptionToken Subscribe(Action<TimeCode> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<TimeCode> filter)
        {
            this.SubscribeArgumentAction = action;
            this.SubscribeArgumentFilter = filter;
            this.SubscribeArgumentThreadOption = threadOption;
            return null;
        }
    }
}
