// <copyright file="SmpteFrameRate.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SmpteFrameRate.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Contracts
{
    using System;
    using System.Runtime.Serialization;

#if !SILVERLIGHT
    [Serializable]
#endif
    /// <summary>
    /// SMPTE Frame Rates enumeration. Use this enumeration with the Timecode struct to set the framerate for the Timecode.
    /// </summary>
    /// <remarks> 
    /// Framerates supported by the Timecode class include, 23.98 IVTC Film Sync, 24fps Film Sync, 25fps PAL, 29.97 drop frame,
    /// 29.97 Non drop, and 30fps.
    /// </remarks>
    public enum SmpteFrameRate
    {
        /// <summary>
        /// SMPTE 23.98 frame rate. Also known as Film Sync.
        /// </summary>
        Smpte2398 = 0,

        /// <summary>
        /// SMPTE 24 fps frame rate.
        /// </summary>
        Smpte24 = 1,

        /// <summary>
        /// SMPTE 25 fps frame rate. Also known as PAL.
        /// </summary>
        Smpte25 = 2,

        /// <summary>
        /// SMPTE 29.97 fps Drop Frame timecode. Used in the NTSC television system.
        /// </summary>
        Smpte2997Drop = 3,

        /// <summary>
        /// SMPTE 29.97 fps Non Drop Fram timecode. Used in the NTSC television system.
        /// </summary>
        Smpte2997NonDrop = 4,

        /// <summary>
        /// SMPTE 30 fps frame rate.
        /// </summary>
        Smpte30 = 5,

        /// <summary>
        /// UnKnown Value.
        /// </summary>
        Unknown = -1
    }
}