// <copyright file="MockOverlaysDisplayController.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockOverlaysDisplayController.cs                     
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
    using System.Windows;
    using System.Windows.Controls;
    using Microsoft.SilverlightMediaFramework.Plugins;
    using Microsoft.SilverlightMediaFramework.Plugins.Primitives;
    using RCE.Overlays.Infrastructure.Models;
    using RCE.Overlays.Infrastructure.UI;

    public class MockOverlaysDisplayController : IOverlaysDisplayController
    {
        public Canvas OverlaysContainer { get; set; }

        public void ShowOverlay(Overlay o)
        {
        }

        public void ShowStaticOverlay(Overlay o)
        {
            throw new NotImplementedException();
        }

        public void HideOverlay(Overlay o)
        {
        }

        public void OnPlayerSizeChangedHandler(object sender, SizeChangedEventArgs e)
        {
        }

        public void OnPlayerStateChangedHandler(IMediaPlugin mediaPlugin, MediaPluginState mediaPluginState)
        {
        }

        public void PluginSeekCompleted(IMediaPlugin obj, bool sucess)
        {
        }
    }
}
