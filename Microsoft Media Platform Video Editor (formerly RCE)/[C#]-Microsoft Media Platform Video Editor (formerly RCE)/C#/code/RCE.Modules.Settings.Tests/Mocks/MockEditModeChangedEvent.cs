// <copyright file="MockEditModeChangedEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockEditModeChangedEvent.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Settings.Tests.Mocks
{
    using System;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Events;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;

    public class MockEditModeChangedEvent : EditModeChangedEvent
    {
        public Action<EditMode> SubscribeArgumentAction { get; set; }

        public Predicate<EditMode> SubscribeArgumentFilter { get; set; }

        public ThreadOption SubscribeArgumentThreadOption { get; set; }

        public override SubscriptionToken Subscribe(Action<EditMode> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<EditMode> filter)
        {
            this.SubscribeArgumentAction = action;
            this.SubscribeArgumentFilter = filter;
            this.SubscribeArgumentThreadOption = threadOption;
            return null;
        }
    }
}
