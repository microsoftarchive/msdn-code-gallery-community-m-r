// <copyright file="MockAssetsAvailableEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockAssetsAvailableEvent.cs                     
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
    using System.Collections.Generic;
    using Infrastructure.Events;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Events;

    public class MockAssetsAvailableEvent : AssetsAvailableEvent
    {
        public Action<Infrastructure.DataEventArgs<List<Asset>>> SubscribeArgumentAction { get; set; }

        public Predicate<Infrastructure.DataEventArgs<List<Asset>>> SubscribeArgumentFilter { get; set; }

        public ThreadOption SubscribeArgumentThreadOption { get; set; }

        public override SubscriptionToken Subscribe(Action<Infrastructure.DataEventArgs<List<Asset>>> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<Infrastructure.DataEventArgs<List<Asset>>> filter)
        {
            base.Subscribe(action, threadOption, keepSubscriberReferenceAlive, filter);
            this.SubscribeArgumentAction = action;
            this.SubscribeArgumentFilter = filter;
            this.SubscribeArgumentThreadOption = threadOption;
            return null;
        }
    }
}