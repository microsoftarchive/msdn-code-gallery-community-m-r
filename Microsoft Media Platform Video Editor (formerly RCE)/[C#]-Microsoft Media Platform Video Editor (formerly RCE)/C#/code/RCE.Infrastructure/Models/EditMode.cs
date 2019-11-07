// <copyright file="EditMode.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: EditMode.cs                     
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
    /// Specifies the edit mode in timeline.
    /// </summary>
    public enum EditMode
    {
        /// <summary>
        /// Do not edit the neighbour elements in the timeline if it is selected and the elements are attached.
        /// </summary>
        Gap = 0,

        /// <summary>
        /// Edits the neighbour elements in the timeline if it is selected and the elements are attached.
        /// </summary>
        Ripple = 1
    }
}
