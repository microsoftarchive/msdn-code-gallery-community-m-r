// <copyright file="MockSmpteTimecodeChangedEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockSmpteTimecodeChangedEvent.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Player.Tests.Mocks
{
    using System;
    using Infrastructure.Events;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Events;
    using SMPTETimecode;

    public class MockSmpteTimecodeChangedEvent : SmpteTimeCodeChangedEvent
    {
        public Action<SmpteFrameRate> SubscribeArgumentAction { get; set; }

        public Predicate<SmpteFrameRate> SubscribeArgumentFilter { get; set; }

        public ThreadOption SubscribeArgumentThreadOption { get; set; }

        public override SubscriptionToken Subscribe(Action<SmpteFrameRate> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<SmpteFrameRate> filter)
        {
            this.SubscribeArgumentAction = action;
            this.SubscribeArgumentFilter = filter;
            this.SubscribeArgumentThreadOption = threadOption;
            return null;
        }
    }
}