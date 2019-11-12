// <copyright file="MockCommentUpdatedEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockCommentUpdatedEvent.cs                     
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
    using Events;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Events;

    public class MockCommentUpdatedEvent : CommentUpdatedEvent
    {
        public Action<RCE.Infrastructure.Models.Comment> SubscribeArgumentAction { get; set; }

        public Predicate<RCE.Infrastructure.Models.Comment> SubscribeArgumentFilter { get; set; }

        public ThreadOption SubscribeArgumentThreadOption { get; set; }

        public bool PublishCalled { get; set; }

        public RCE.Infrastructure.Models.Comment PublishArgumentPayload { get; set; }

        public override SubscriptionToken Subscribe(Action<RCE.Infrastructure.Models.Comment> action, ThreadOption threadOption, bool keepSubscriberReferenceAlive, Predicate<RCE.Infrastructure.Models.Comment> filter)
        {
            this.SubscribeArgumentAction = action;
            this.SubscribeArgumentFilter = filter;
            this.SubscribeArgumentThreadOption = threadOption;
            return null;
        }

        public override void Publish(RCE.Infrastructure.Models.Comment payload)
        {
            this.PublishCalled = true;
            this.PublishArgumentPayload = payload;
        }
    }
}