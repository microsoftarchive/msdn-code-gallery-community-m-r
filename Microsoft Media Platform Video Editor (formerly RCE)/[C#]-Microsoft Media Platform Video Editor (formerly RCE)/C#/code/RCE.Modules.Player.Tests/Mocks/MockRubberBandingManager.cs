// <copyright file="MockRubberBandingManager.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockRubberBandingManager.cs                     
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

    using RCE.Plugins.RubberBanding.Manager;

    public class MockRubberBandingManager : IRubberBandingManager
    {
        public bool Enabled
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Func<bool> Suspended
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public double VolumeLevel { get; set; }

        public void SetAdaptivePlugin(IAdaptiveMediaPlugin plugin)
        {
        }

        public void Reset()
        {
        }

        public double CalculateVolumeFor(ulong currentPosition)
        {
            throw new NotImplementedException();
        }
    }
}
