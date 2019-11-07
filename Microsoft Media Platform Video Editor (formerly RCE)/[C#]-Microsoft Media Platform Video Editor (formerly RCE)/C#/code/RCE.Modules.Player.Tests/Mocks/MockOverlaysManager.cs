// <copyright file="MockOverlaysManager.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockOverlaysManager.cs                     
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
    using Microsoft.SilverlightMediaFramework.Plugins;
    using RCE.Overlays.Infrastructure.Manager;
    using RCE.Overlays.Infrastructure.Models;

    public class MockOverlaysManager : IOverlaysManager
    {
        public event Action<Overlay> OverlayBeginReached;

        public event Action<Overlay> OverlayEndReached;

        public event Action<Overlay> OverlayBeginSeeked;

        public void SetAdaptivePlugin(IAdaptiveMediaPlugin plugin)
        {
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
