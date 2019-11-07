// <copyright file="MockPlayerEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockPlayerEvent.cs                     
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
    using Infrastructure.Events;

    public class MockPlayerEvent : PlayerEvent
    {
        public bool PublishCalled { get; set; }

        public PlayerEventPayload PublishArgumentPayload { get; set; }

        public EventHandler PublishCalledEvent { get; set; }

        public override void Publish(PlayerEventPayload payload)
        {
            this.PublishCalled = true;
            this.PublishArgumentPayload = payload;
        }
    }
}