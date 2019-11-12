// <copyright file="ScrubShiftType.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ScrubShiftType.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Models
{
    /// <summary>
    /// Enum of Scrb move type.
    /// </summary>
    public enum ScrubShiftType
    {
        /// <summary>
        /// In Scrub is moving.
        /// </summary>
        In = 0,

        /// <summary>
        /// Out Scrub is moving.
        /// </summary>
        Out = 1,

        /// <summary>
        /// SpanBar is moving.
        /// </summary>
        SpanBar = 3,

        /// <summary>
        /// PlayHead is moving.
        /// </summary>
        PlayHead = 4
    }
}
