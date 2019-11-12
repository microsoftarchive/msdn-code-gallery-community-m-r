// <copyright file="MockThumbnailEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockThumbnailEvent.cs                     
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
    using System.Windows.Media.Imaging;
    using Infrastructure.Events;

    public class MockThumbnailEvent : ThumbnailEvent
    {
        public bool PublishCalled { get; set; }

        public WriteableBitmap PublishArgumentPayload { get; set; }

        public EventHandler PublishCalledEvent { get; set; }

        public override void Publish(ThumbnailEventPayload payload)
        {
            this.PublishCalled = true;
            this.PublishArgumentPayload = payload.Bitmap;
        }
    }
}
