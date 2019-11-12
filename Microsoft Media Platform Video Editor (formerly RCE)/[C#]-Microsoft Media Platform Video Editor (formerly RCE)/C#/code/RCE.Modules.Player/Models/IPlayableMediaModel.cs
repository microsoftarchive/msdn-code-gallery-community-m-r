// <copyright file="IPlayableMediaModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IPlayableMediaModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Player.Models
{
    using System;
    using RCE.Infrastructure.Events;

    public interface IPlayableMediaModel
    {
        /// <summary>
        /// Occurs when [finished playing].
        /// </summary>
        event EventHandler FinishedPlaying;

        /// <summary>
        /// Occurs when media element position changes.
        /// </summary>
        event EventHandler<PositionPayloadEventArgs> PositionUpdated;

        /// <summary>
        /// Gets or sets the position of the model.
        /// </summary>
        /// <value>The position.</value>
        TimeSpan Position { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is playing.
        /// </summary>
        /// <value>
        /// It shold be <c>true</c> if this instance is playing; otherwise, <c>false</c>.
        /// </value>
        bool IsPlaying { get; }

        /// <summary>
        /// Gets the duration of the current model.
        /// </summary>
        /// <value>The duration.</value>
        TimeSpan Duration { get; }

        /// <summary>
        /// Sets a value indicating whether media model is muted.
        /// </summary>
        /// <value>True if the model is to be in muted; otherwise false.</value>
        bool Mute { set; }

        void FastForward();

        void FastRewind();

        /// <summary>
        /// Plays this model.
        /// </summary>
        void Play();

        /// <summary>
        /// Pauses this model.
        /// </summary>
        void Pause();
    }
}
