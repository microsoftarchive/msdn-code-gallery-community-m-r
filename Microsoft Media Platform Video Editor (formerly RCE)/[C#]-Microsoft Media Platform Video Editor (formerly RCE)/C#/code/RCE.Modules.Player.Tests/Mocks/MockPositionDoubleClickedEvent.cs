// <copyright file="MockPositionDoubleClickedEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockPositionDoubleClickedEvent.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Player.Tests.Mocks
{
    using System;
    using RCE.Infrastructure.Events;

    public class MockPositionDoubleClickedEvent : PositionDoubleClickedEvent
    {
        public bool PublishCalled { get; set; }

        public PositionPayloadEventArgs PositionPayloadEventArgs { get; set; }

        public override void Publish(PositionPayloadEventArgs args)
        {
            this.PublishCalled = true;
            this.PositionPayloadEventArgs = args;
        }
    }
}
