// <copyright file="MockRefreshElementsEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockRefreshElementsEvent.cs                     
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

    public class MockRefreshElementsEvent : RefreshElementsEvent
    {
        public bool PublishCalled { get; set; }

        public RefreshElementsEventArgs PublishArgumentPayload { get; set; }

        public EventHandler PublishCalledEvent { get; set; }

        public override void Publish(RefreshElementsEventArgs payload)
        {
            this.PublishCalled = true;
            this.PublishArgumentPayload = payload;
        }
    }
}