// <copyright file="MockFullScreenEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockFullScreenEvent.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Tests.Mocks
{
    using System;
    using Infrastructure;
    using Infrastructure.Events;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Events;

    public class MockFullScreenEvent : FullScreenEvent
    {
        public Action<FullScreenMode> SubscribeArgumentAction { get; set; }

        public Predicate<FullScreenMode> SubscribeArgumentFilter { get; set; }

        public ThreadOption SubscribeArgumentThreadOption { get; set; }

        public override SubscriptionToken Subscribe(Action<FullScreenMode> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<FullScreenMode> filter)
        {
            base.Subscribe(action, threadOption, keepSubscriberReferenceAlive, filter); 
            this.SubscribeArgumentAction = action;
            this.SubscribeArgumentFilter = filter;
            this.SubscribeArgumentThreadOption = threadOption;
            return null;
        }
    }
}