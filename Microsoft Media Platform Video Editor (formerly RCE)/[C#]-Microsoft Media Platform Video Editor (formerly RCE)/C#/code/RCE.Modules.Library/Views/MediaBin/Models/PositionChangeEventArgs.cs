// <copyright file="PositionChangeEventArgs.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PositionChangeEventArgs.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.MediaBin
{
    using System;
    using SMPTETimecode;

    /// <summary>
    /// Event args that contains position changes.
    /// </summary>
    public class PositionChangeEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the new position.
        /// </summary>
        /// <value>A timecode that represents the new position.</value>
        public TimeCode NewPosition { get; set; }
    }
}