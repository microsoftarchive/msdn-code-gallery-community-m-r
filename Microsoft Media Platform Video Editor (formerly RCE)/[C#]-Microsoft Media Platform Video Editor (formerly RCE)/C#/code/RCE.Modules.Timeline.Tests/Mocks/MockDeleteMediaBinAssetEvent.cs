// <copyright file="MockDeleteMediaBinAssetEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockDeleteMediaBinAssetEvent.cs                     
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
    using Infrastructure.Events;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Events;

    public class MockDeleteMediaBinAssetEvent : DeleteMediaBinAssetEvent
    {
        public Action<Asset> SubscribeArgumentAction { get; set; }

        public Predicate<Asset> SubscribeArgumentFilter { get; set; }

        public ThreadOption SubscribeArgumentThreadOption { get; set; }

        public override SubscriptionToken Subscribe(Action<Asset> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<Asset> filter)
        {
            this.SubscribeArgumentAction = action;
            this.SubscribeArgumentFilter = filter;
            this.SubscribeArgumentThreadOption = threadOption;

            return null;
        }
    }
}