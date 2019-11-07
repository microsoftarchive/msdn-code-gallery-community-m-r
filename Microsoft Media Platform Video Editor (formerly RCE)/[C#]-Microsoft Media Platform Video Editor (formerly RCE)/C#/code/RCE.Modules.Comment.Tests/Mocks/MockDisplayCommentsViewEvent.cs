// <copyright file="MockDisplayCommentsViewEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockDisplayCommentsViewEvent.cs                     
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

    public class MockDisplayCommentsViewEvent : DisplayCommentsViewEvent
    {
        public Action<object> SubscribeAction { get; set; }

        public override Microsoft.Practices.Composite.Events.SubscriptionToken Subscribe(
            System.Action<object> action,
            Microsoft.Practices.Composite.Presentation.Events.ThreadOption threadOption,
            bool keepSubscriberReferenceAlive, 
            System.Predicate<object> filter)
        {
            this.SubscribeAction = action;
            return null;
        }
    }
}
