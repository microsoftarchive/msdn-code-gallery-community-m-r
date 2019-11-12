// <copyright file="MockAddAssetToTimelineEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockAddAssetToTimelineEvent.cs                     
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
    using RCE.Infrastructure.Models;

    /// <summary>
    /// This event can be used to add a asset in the timeline.
    /// </summary>
    public class MockAddAssetToTimelineEvent : AddAssetToTimelineEvent
    {
        public Action<Asset> SubscribeArgumentAction { get; set; }

        public ThreadOption SubscribeArgumentThreadOption { get; set; }

        public Predicate<Asset> SubscribeArgumentFilter { get; set; }

        /// <summary>
        /// Override the subscribe method of the composite event.
        /// </summary>
        /// <param name="action">Action to be called.</param>
        /// <param name="threadOption">Thred to be used to execute the action.</param>
        /// <param name="keepSubscriberReferenceAlive">If it is to be garbage collected.</param>
        /// <returns></returns>
        public override SubscriptionToken Subscribe(Action<Asset> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<Asset> filter)
        {
            this.SubscribeArgumentAction = action;
            this.SubscribeArgumentFilter = filter;
            this.SubscribeArgumentThreadOption = threadOption;
            return null;
        }
    }
}
