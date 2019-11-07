// <copyright file="MockHideMetadataEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockHideMetadataEvent.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Metadata.Tests.Mocks
{
    using System;
    using Infrastructure.Events;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Events;

    public class MockHideMetadataEvent : HideMetadataEvent
    {
        public bool SubscribeCalled { get; set; }

        public Action<object> SubscribeArgumentAction { get; set; }

        public Predicate<object> SubscribeArgumentFilter { get; set; }

        public ThreadOption SubscribeArgumentThreadOption { get; set; }

        public override SubscriptionToken Subscribe(Action<object> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<object> filter)
        {
            this.SubscribeCalled = true;
            this.SubscribeArgumentAction = action;
            this.SubscribeArgumentThreadOption = threadOption;

            return null;
        }
    }
}