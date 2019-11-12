// <copyright file="IPlayerView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IPlayerView.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Player
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Modules.Player.Models;

    using SMPTETimecode;
    using Comment = RCE.Infrastructure.Models.Comment;

    /// <summary>
    /// Interface definition for the <see cref="IPlayerView"/> control.
    /// </summary>
    public interface IPlayerView
    {
        /// <summary>
        /// Occurs when [full screen changed].
        /// </summary>
        event EventHandler<FullScreenModeEventArgs> FullScreenChanged;

        /// <summary>
        /// Occurs when [play clicked].
        /// </summary>
        event EventHandler PlayClicked;

        /// <summary>
        /// Occurs when [pause clicked].
        /// </summary>
        event EventHandler PauseClicked;

        /// <summary>
        /// Occurs when [rewind started].
        /// </summary>
        event EventHandler FrameRewindStarted;

        /// <summary>
        /// Occurs when [rewind ended].
        /// </summary>
        event EventHandler FrameRewindEnded;

        /// <summary>
        /// Occurs when [forward started].
        /// </summary>
        event EventHandler FrameForwardStarted;

        /// <summary>
        /// Occurs when [forward ended].
        /// </summary>
        event EventHandler FrameForwardEnded;

        /// <summary>
        /// Handles the PickThumbnail click event.
        /// </summary>
        event EventHandler PickThumbnailClicked;
       
        /// <summary>
        /// Handles the slow motion click event.
        /// </summary>
        event EventHandler SlowMotionClicked;

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        IPlayerViewPresenter Model { get; set; }

        Canvas OverlaysContainer { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is muted.
        /// </summary>
        /// <value>It should be <c>true</c> if this instance is muted; otherwise, <c>false</c>.</value>
        bool IsMuted { get; set; }

        /// <summary>
        /// Enables the Play button on the value of isEnabled.
        /// </summary>
        void EnablePlayButton(bool isEnabled);

        /// <summary>
        /// Toggles between play/pause.
        /// </summary>
        void TogglePlay();

        /// <summary>
        /// Sets the source of media element of image.
        /// </summary>
        /// <param name="asset">The asset.</param>
        void SetSource(Asset asset);

        /// <summary>
        /// Sets the current smpte frame rate.
        /// </summary>
        /// <param name="frameRate">The frame rate.</param>
        void SetCurrentSmpteFrameRate(SmpteFrameRate frameRate);

        /// <summary>
        /// Adds the element to the current control.
        /// </summary>
        /// <param name="mediaData">The media data.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        void AddElement(MediaData mediaData, int width, int height);

        /// <summary>
        /// Removes the element from the current control.
        /// </summary>
        /// <param name="mediaData">The media data.</param>
        void RemoveElement(MediaData mediaData);

        /// <summary>
        /// Sets the aspect ratio.
        /// </summary>
        /// <param name="selectedAspectRatio">The <see cref="AspectRatio"/>.</param>
        void SetAspectRatio(AspectRatio selectedAspectRatio);

        /// <summary>
        /// Sets the text of the time control to the current time.
        /// </summary>
        /// <param name="position">The position.</param>
        void SetCurrentTime(TimeSpan position);

        /// <summary>
        /// Sets the position of the media to the start of the media.
        /// </summary>
        void MoveToStart();

        /// <summary>
        /// Sets the position of the media to the end of the media.
        /// </summary>
        void MoveToEnd();

        /// <summary>
        /// Starts the rewind forward.
        /// </summary>
        /// <param name="skipDirection">The skip direction.</param>
        void StartFrameRewindForward(int skipDirection);

        /// <summary>
        /// Ends the rewind forward.
        /// </summary>
        void EndFrameRewindForward();

        /// <summary>
        /// Toggles the play/pause visibility.
        /// </summary>
        /// <param name="playing">True if the player is playing.</param>
        void TogglePlayVisibility(bool playing);

        /// <summary>
        /// Toggles the loop playback of the media.
        /// </summary>
        void ToggleLoopPlayback();

        /// <summary>
        /// Stops this current playing media.
        /// </summary>
        void Stop();

        /// <summary>
        /// Shows the comments.
        /// </summary>
        /// <param name="comments">The comments.</param>
        void ShowComments(List<Comment> comments);

        /// <summary>
        /// Hides the comment.
        /// </summary>
        void HideComments();

        /// <summary>
        /// Starts the animation of buffering in the player.
        /// </summary>
        void StartBuffer();

        /// <summary>
        /// End the animation of buffering in the player.
        /// </summary>
        void EndBuffer();

        /// <summary>
        /// Hides the image control if the Audio/Video asset is selected for playing.
        /// </summary>
        void HidePreviewImage();

        /// <summary>
        /// Pauses the player.
        /// </summary>
        void PausePlayer();

        /// <summary>
        /// Picks a thumnbnail.
        /// </summary>
        /// <param name="mediaData">The media data of the current element.</param>
        void PickThumbnail(MediaData mediaData, ThumbnailType thumbnailType);

        /// <summary>
        /// Toggles the motion.
        /// </summary>
        /// <param name="isChecked"></param>
        void ToggleSlowMotion(bool isChecked);

        void ShowErrorMessage(bool isVisible);

        void ShowSequenceHasGapErrorMessage(bool isVisible);

        void HandleKeyboardAction(KeyboardAction keyboardAction);

        void RemoveAllElements();

        void AddXamlElement(string xaml, IDictionary<string, string> properties, double positionX, double positionY, double height, double width);

        void RemoveOverlayPreviews();

        void RemovePlaybackOverlays();

        void AddOverlaysSupport();
    }
}
