// <copyright file="ScrubPositionChangeEventArgs.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ScrubPositionChangeEventArgs.cs                     
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
    using System;

    /// <summary>
    /// Event argument for Shift operation.
    /// </summary>
    public class ScrubPositionChangeEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScrubPositionChangeEventArgs"/> class.
        /// </summary>
        /// <param name="shiftType">Type of the shift.</param>
        public ScrubPositionChangeEventArgs(ScrubShiftType shiftType)
        {
            this.ShiftType = shiftType;
        }

        /// <summary>
        /// Gets or sets the type of the shift.
        /// </summary>
        /// <value>The type of the shift.</value>
        public ScrubShiftType ShiftType { get; set; }
    }
}
