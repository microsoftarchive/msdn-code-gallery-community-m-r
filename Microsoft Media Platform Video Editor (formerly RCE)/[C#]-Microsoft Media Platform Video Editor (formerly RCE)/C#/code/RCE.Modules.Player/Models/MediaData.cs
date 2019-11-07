// <copyright file="MediaData.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MediaData.cs                     
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
    using System.IO;

    using Infrastructure.Events;
    using Infrastructure.Models;

    using Microsoft.SilverlightMediaFramework.Plugins;

    /// <summary>
    /// Base class for <see cref="ImageMediaData"/>, <see cref="TitleMediaData"/> and 
    /// <see cref="PlayableMediaData"/>. It is the definition of the methods which are being used 
    /// to play the asset in the player.
    /// </summary>
    public class MediaData : IDisposable
    {
        /// <summary>
        /// Handler for Buffer start for the media element.
        /// </summary>
        public event EventHandler BufferStart;

        /// <summary>
        /// Handler for Buffer end for the media element.
        /// </summary>
        public event EventHandler BufferEnd;

        /// <summary>
        /// Occurs when [download start].
        /// </summary>
        public event EventHandler<AssetDownloadProgressEventArgs> DownloadProgressChanged;

        /// <summary>
        /// Gets or sets start position of the asset of the <see cref="MediaData"/>.
        /// </summary>
        /// <value>Start position from where <see cref="MediaData"/> will start playing.</value>
        public virtual TimeSpan In { get; set; }

        /// <summary>
        /// Gets or sets the stop position of the asset of the <see cref="MediaData"/>.
        /// </summary>
        /// <value>End position from where <see cref="MediaData"/> will stop playing.</value>
        public virtual TimeSpan Out { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MediaData"/> is playing.
        /// </summary>
        /// <value><c>True</c> if playing; otherwise, <c>false</c>.</value>
        public virtual bool Playing { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="MediaData"/> is visible.
        /// </summary>
        /// <value>Value would be <c>true</c> if [currently showing]; otherwise, <c>false</c>.</value>
        public virtual bool CurrentlyShowing { get; set; }

        /// <summary>
        /// Gets or sets the position of the <see cref="MediaData"/>.
        /// </summary>
        /// <value>The position.</value>
        public virtual TimeSpan Position { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the mediadata is muted.
        /// </summary>
        /// <value>Value would be <c>true</c> if the <see cref="MediaData"/> is muted; otherwise, <c>false</c>.</value>
        public virtual bool IsMuted { get; set; }

        /// <summary>
        /// Gets the control corresponding to the <see cref="MediaData"/> which
        /// is used to play the <see cref="MediaData"/>.
        /// </summary>
        /// <value>The user control.</value>
        public virtual object Media { get; private set; }

        /// <summary>
        /// Whether the current media data has media or not. Useful to mark a plug-in's stream as deprecated.
        /// </summary>
        /// <value>True if media data has media, otherwise false.</value>
        public virtual bool HasMedia { get; set; }

        /// <summary>
        /// Gets the timeline element for the <see cref="MediaData"/>.
        /// </summary>
        /// <value>The timeline element.</value>
        public virtual TimelineElement TimelineElement { get; private set; }

        public virtual IMediaPlugin MediaPlugin { get; private set; }

        /// <summary>
        /// Plays this <see cref="MediaData"/>.
        /// </summary>
        public virtual void Play()
        {
        }

        /// <summary>
        /// Pauses this <see cref="MediaData"/>.
        /// </summary>
        public virtual void Pause()
        {
        }

        /// <summary>
        /// Stops this <see cref="MediaData"/>.
        /// </summary>
        public virtual void Stop()
        {
        }

        /// <summary>
        /// Hides this <see cref="MediaData"/>.
        /// </summary>
        public virtual void Hide()
        {
            this.Pause();
            this.CurrentlyShowing = false;
        }

        /// <summary>
        /// Shows this <see cref="MediaData"/>.
        /// </summary>
        public virtual void Show()
        {
            this.CurrentlyShowing = true;
        }

        public virtual void FastRewind()
        {
        }

        public virtual void FastForward()
        {
        }

        public virtual void SetStreamSource(Stream manifestStream)
        {
        }

        public virtual void SetSource(Uri source)
        {
        }

        public virtual void Dispose()
        {
        }

        /// <summary>
        /// Triggers BufferStart event.
        /// </summary>
        protected void OnBufferStart()
        {
            EventHandler bufferStart = this.BufferStart;
            if (bufferStart != null)
            {
                bufferStart(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Triggers BufferEnd event.
        /// </summary>
        protected void OnBufferEnd()
        {
            EventHandler bufferEnd = this.BufferEnd;
            if (bufferEnd != null)
            {
                bufferEnd(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Called when [download start].
        /// </summary>
        /// <param name="progress">The progress.</param>
        /// <param name="offset">The offset.</param>
        protected void OnDownloadProgressChanged(double progress, double offset)
        {
            EventHandler<AssetDownloadProgressEventArgs> downloadProgressChanged = this.DownloadProgressChanged;
            if (downloadProgressChanged != null)
            {
                downloadProgressChanged(this, new AssetDownloadProgressEventArgs(this.TimelineElement, progress, offset));
            }
        }
    }
}
