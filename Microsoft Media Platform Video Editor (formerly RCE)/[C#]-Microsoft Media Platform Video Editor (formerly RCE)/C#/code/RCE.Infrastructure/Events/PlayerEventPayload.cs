// <copyright file="PlayerEventPayload.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PlayerEventPayload.cs                     
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
    using Models;

    /// <summary>
    /// Payload for <see cref="PlayerEvent"/>.
    /// </summary>
    public class PlayerEventPayload
    {
        /// <summary>
        /// Gets or sets the asset.
        /// </summary>
        /// <value>The asset.</value>
        public Asset Asset { get; set; }

        /// <summary>
        /// Gets or sets the player mode.
        /// </summary>
        /// <value>The <see cref="PlayerMode"/>.</value>
        public PlayerMode PlayerMode { get; set; }
    }
}
