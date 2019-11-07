// <copyright file="MockDisplayMarkerBrowserWindowEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockDisplayMarkerBrowserWindowEvent.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Comment.Tests.Mocks
{
    using System;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;

    public class MockDisplayMarkerBrowserWindowEvent : DisplayMarkerBrowserWindowEvent
    {
        // public override Microsoft.Practices.Composite.Events.SubscriptionToken Subscribe
        //    (System.Action<object> action,
        //    Microsoft.Practices.Composite.Presentation.Events.ThreadOption threadOption,
        //    bool keepSubscriberReferenceAlive,
        //    System.Predicate<object> filter)
        // {
        //    this.SubscribeAction = action;
        //    return null;
        // }
        public Action<SelectedMarkersBrowserTab> SubscribeAction { get; set; }

        public override Microsoft.Practices.Composite.Events.SubscriptionToken Subscribe(
            Action<Infrastructure.Models.SelectedMarkersBrowserTab> action,
            Microsoft.Practices.Composite.Presentation.Events.ThreadOption threadOption, 
            bool keepSubscriberReferenceAlive, 
            Predicate<Infrastructure.Models.SelectedMarkersBrowserTab> filter)
        {
            this.SubscribeAction = action;
            return null;
        }
    }
}
