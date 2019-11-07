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

namespace RCE.Modules.MediaBin.Tests.Mocks
{
    using System;
    using Infrastructure.Events;
    using Infrastructure.Models;

    public class MockDeleteMediaBinAssetEvent : DeleteMediaBinAssetEvent
    {
        public bool PublishCalled { get; set; }

        public Asset PublishArgumentPayload { get; set; }

        public EventHandler PublishCalledEvent { get; set; }

        public override void Publish(Asset payload)
        {
            this.PublishCalled = true;
            this.PublishArgumentPayload = payload;
        }
    }
}