// <copyright file="MockAspectRatioChangedEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockAspectRatioChangedEvent.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Settings.Tests.Mocks
{
    using Infrastructure.Events;
    using Infrastructure.Models;

    public class MockAspectRatioChangedEvent : AspectRatioChangedEvent
    {
        public bool PublishCalled { get; set; }

        public AspectRatio PublishArgumentPayload { get; set; }

        public override void Publish(AspectRatio payload)
        {
            this.PublishCalled = true;
            this.PublishArgumentPayload = payload;
        }
    }
}