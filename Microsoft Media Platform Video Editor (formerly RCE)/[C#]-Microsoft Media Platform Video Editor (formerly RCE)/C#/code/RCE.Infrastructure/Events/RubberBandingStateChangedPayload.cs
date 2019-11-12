// <copyright file="RubberBandingStateChangedPayload.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MetadaSelectedPayload.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Events
{
    public class RubberBandingStateChangedPayload
    {
        public RubberBandingStateChangedPayload(bool isEnabled, double trackVolume)
        {
            this.IsEnabled = isEnabled;
            this.TrackVolume = trackVolume;
        }

        public bool IsEnabled
        {
            get;
            set;
        }

        public double TrackVolume
        {
            get;
            set;
        }
    }
}