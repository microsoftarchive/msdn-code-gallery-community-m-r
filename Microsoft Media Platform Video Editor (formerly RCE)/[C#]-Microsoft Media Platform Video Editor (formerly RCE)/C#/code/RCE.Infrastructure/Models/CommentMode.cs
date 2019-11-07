// <copyright file="CommentMode.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentMode.cs                     
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
    /// Identifies the severity of the information being logged.
    /// </summary>
    public enum CommentMode
    {
        /// <summary>
        /// Indicates that event generated on the SubClip window
        /// </summary>
        SubClip,

        /// <summary>
        /// Indicates that event generated on the Timeline window
        /// </summary>
        Timeline
    }
}