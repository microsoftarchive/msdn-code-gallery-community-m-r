// <copyright file="MockTransitionsManager.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockTransitionsManager.cs                     
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
    using RCE.Transitions.Infrastructure.Managers;

    public class MockTransitionsManager : ITransitionsManager
    {
        public Func<ulong, double> CalculateVolumeFor
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

        public bool IsAudioOnly
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

        public bool IsAudioTransitionRunning
        {
            get { throw new NotImplementedException(); }
        }

        public double VolumeLevel
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

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void SetAdaptivePlugin(IAdaptiveMediaPlugin plugin)
        {
        }
    }
}
