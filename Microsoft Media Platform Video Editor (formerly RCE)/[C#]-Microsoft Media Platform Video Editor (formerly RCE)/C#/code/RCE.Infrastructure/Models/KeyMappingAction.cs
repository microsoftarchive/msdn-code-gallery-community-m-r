// <copyright file="KeyMappingAction.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: KeyMappingAction.cs                     
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
    /// Enum used for keyboards mapping.
    /// </summary>
    public enum KeyMappingAction
    {
        /// <summary>
        /// If the user press the shortcut to toggle the player between Play/Pause.
        /// </summary>
        Toggle,

        /// <summary>
        /// If the user press the shortcut to play the timeline.
        /// </summary>
        PlayTimeline,

        /// <summary>
        /// If the user press the shortcut to pause the player.
        /// </summary>
        PausePlayer
    }
}