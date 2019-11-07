// <copyright file="MockAddAssetEvent.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockAddAssetEvent.cs                     
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
    using Infrastructure.Events;
    using Infrastructure.Models;

    public class MockAddAssetEvent : AddAssetEvent
    {
        public bool PublishCalled { get; set; }

        public Asset Asset { get; set; }

        public override void Publish(Asset asset)
        {
            this.PublishCalled = true;
            this.Asset = asset;
        }
    }
}