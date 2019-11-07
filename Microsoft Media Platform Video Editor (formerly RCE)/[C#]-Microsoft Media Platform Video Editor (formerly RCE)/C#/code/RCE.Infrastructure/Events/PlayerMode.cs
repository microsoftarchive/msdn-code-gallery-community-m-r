// <copyright file="PlayerMode.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PlayerMode.cs                     
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
    /// <summary>
    /// To inidicate which module is playing in player module.
    /// </summary>
    public enum PlayerMode
    {
        /// <summary>
        /// If MediaBin module is playing.
        /// </summary>
        MediaBin,

        /// <summary>
        /// If Media Library module is playing.
        /// </summary>
        MediaLibrary,

        /// <summary>
        /// If Timeline module is playing.
        /// </summary>
        Timeline,

        /// <summary>
        /// If Comment module is playing.
        /// </summary>
        Comment
    }
}