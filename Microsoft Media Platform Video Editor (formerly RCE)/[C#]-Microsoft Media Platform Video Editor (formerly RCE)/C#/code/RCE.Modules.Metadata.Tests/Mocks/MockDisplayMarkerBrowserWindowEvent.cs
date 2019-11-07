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

namespace RCE.Modules.Metadata.Tests.Mocks
{
    using RCE.Infrastructure.Events;

    public class MockDisplayMarkerBrowserWindowEvent : DisplayMarkerBrowserWindowEvent
    {
        public bool PublishCalled { get; set; }

        public override void Publish(Infrastructure.Models.SelectedMarkersBrowserTab payload)
        {
            this.PublishCalled = true;
        }
    }
}
