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

namespace RCE.Modules.Overlays.Tests.Mocks
{
    using System;
    using System.Collections.Generic;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;

    public class MockAssetsAvailableEvent : AssetsAvailableEvent
    {
        private Action<DataEventArgs<List<Asset>>> subscribeAction;

        public override Microsoft.Practices.Composite.Events.SubscriptionToken Subscribe(
            System.Action<Infrastructure.DataEventArgs<List<Infrastructure.Models.Asset>>> action,
            Microsoft.Practices.Composite.Presentation.Events.ThreadOption threadOption,
            bool keepSubscriberReferenceAlive,
            System.Predicate<Infrastructure.DataEventArgs<List<Infrastructure.Models.Asset>>> filter)
        {
            this.subscribeAction = action;
            return base.Subscribe(action, threadOption, keepSubscriberReferenceAlive, filter);
        }

        public override void Publish(DataEventArgs<List<Asset>> payload)
        {
            this.subscribeAction(payload);
        }
    }
}
