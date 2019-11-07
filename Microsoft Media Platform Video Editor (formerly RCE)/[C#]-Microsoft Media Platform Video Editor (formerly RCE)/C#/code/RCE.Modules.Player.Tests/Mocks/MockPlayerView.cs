// <copyright file="MockPlayerView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockPlayerView.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Player.Tests.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;

    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Modules.Player.Models;
    using SMPTETimecode;
    using Comment = RCE.Infrastructure.Models.Comment;

    public class MockPlayerView : IPlayerView
    {
        private Canvas overlaysContainer = new Canvas();

        public event EventHandler<FullScreenModeEventArgs> FullScreenChanged;

        public event EventHandler PlayClicked;

        public event EventHandler PauseClicked;

        public event EventHandler FrameRewindStarted;

        public event EventHandler FrameRewindEnded;

        public event EventHandler FrameForwardStarted;

        public event EventHandler FrameForwardEnded;

        public event EventHandler PickThumbnailClicked;

        public event EventHandler SlowMotionClicked;

        public IPlayerViewPresenter Model { get; set; }

        public Uri SetSourceArgument { get; set; }

        public bool SetSourceCalled { get; set; }

        public bool StopCalled { get; set; }

        public bool StartBufferCalled { get; set; }

        public bool HidePreviewImageCalled { get; set; }

        public bool PausePlayerCalled { get; set; }
        
        public bool EndBufferCalled { get; set; }

        public SmpteFrameRate SetCurrentSmpteFrameRateArgument { get; set; }

        public bool SetCurrentSmpteFrameRateCalled { get; set; }

        public bool SetAspectRatioCalled { get; set; }

        public Canvas OverlaysContainer
        {
            get
            {
                return this.overlaysContainer;
            }
        }

        public bool IsMuted { get; set; }

        public AspectRatio SetCurrentAspectRatio { get; set; }

        public bool AddElementCalled { get; set; }

        public MediaData AddElementArgument { get; set; }

        public bool RemoveElementCalled { get; set; }

        public bool TogglePlayCalled { get; set; }

        public bool SetCurrentTimeCalled { get; set; }

        public bool DownloadProgressChangedCalled { get; set; }

        public TimeSpan SetCurrentTimeArgument { get; set; }

        public bool MoveToStartCalled { get; set; }

        public bool MoveToEndCalled { get; set; }

        public bool StartRewindForwardCalled { get; set; }

        public bool EndRewindForwardCalled { get; set; }

        public bool TogglePlayVisibilityCalled { get; set; }

        public bool ToggleLoopPlayBackCalled { get; set; }

        public bool ShowCommentsCalled { get; set; }
        
        public bool HideCommentsCalled { get; set; }

        public IList<Comment> Comments { get; set; }

        public bool PickThumbnailCalled { get; set; }

        public bool ToggleSlowMotionCalled { get; set; }

        public bool ShowSequenceHasGapErrorMessageVisible { get; set; }

        public void TogglePlay()
        {
            this.TogglePlayCalled = true;
        }

        public void SetSource(Asset asset)
        {
            this.SetSourceCalled = true;
            this.SetSourceArgument = asset.Source;
        }

        public void SetCurrentSmpteFrameRate(SmpteFrameRate frameRate)
        {
            this.SetCurrentSmpteFrameRateCalled = true;
            this.SetCurrentSmpteFrameRateArgument = frameRate;
        }

        public void AddElement(MediaData videoMediaData, int width, int height)
        {
            this.AddElementCalled = true;
            this.AddElementArgument = videoMediaData;
        }

        public void RemoveElement(MediaData mediaData)
        {
            this.RemoveElementCalled = true;
        }

        public void InvokeFullScreenChanged(FullScreenMode mode)
        {
            EventHandler<FullScreenModeEventArgs> fullScreenChangedHandler = this.FullScreenChanged;
            if (fullScreenChangedHandler != null)
            {
                fullScreenChangedHandler(this, new FullScreenModeEventArgs(mode));
            }
        }

        public void InvokePlayClicked()
        {
            EventHandler playClickedHandler = this.PlayClicked;
            if (playClickedHandler != null)
            {
                playClickedHandler(this, EventArgs.Empty);
            }
        }

        public void InvokeRewindStarted()
        {
            EventHandler rewindStartedHandler = this.FrameRewindStarted;
            if (rewindStartedHandler != null)
            {
                rewindStartedHandler(this, EventArgs.Empty);
            }
        }

        public void InvokeRewindEnded()
        {
            EventHandler rewindEndedHandler = this.FrameRewindEnded;
            if (rewindEndedHandler != null)
            {
                rewindEndedHandler(this, EventArgs.Empty);
            }
        }

        public void InvokeForwardStarted()
        {
            EventHandler forwardStartedHandler = this.FrameForwardStarted;
            if (forwardStartedHandler != null)
            {
                forwardStartedHandler(this, EventArgs.Empty);
            }
        }

        public void InvokeForwardEnded()
        {
            EventHandler forwardEndedHandler = this.FrameForwardEnded;
            if (forwardEndedHandler != null)
            {
                forwardEndedHandler(this, EventArgs.Empty);
            }
        }

        public void InvokePauseClicked()
        {
            EventHandler pauseClickedHandler = this.PauseClicked;
            if (pauseClickedHandler != null)
            {
                pauseClickedHandler(this, EventArgs.Empty);
            }
        }

        public void SetAspectRatio(AspectRatio selectedAspectRatio)
        {
            this.SetAspectRatioCalled = true;
            this.SetCurrentAspectRatio = selectedAspectRatio;
        }

        public void SetCurrentTime(TimeSpan position)
        {
            this.SetCurrentTimeCalled = true;
            this.SetCurrentTimeArgument = position;
        }

        public void MoveToStart()
        {
            this.MoveToStartCalled = true;
        }

        public void MoveToEnd()
        {
            this.MoveToEndCalled = true;
        }

        public void StartFrameRewindForward(int skipDirection)
        {
            this.StartRewindForwardCalled = true;
        }

        public void EndFrameRewindForward()
        {
            this.EndRewindForwardCalled = true;
        }

        public void TogglePlayVisibility(bool playing)
        {
            this.TogglePlayVisibilityCalled = true;
        }

        public void ToggleLoopPlayback()
        {
            this.ToggleLoopPlayBackCalled = true;
        }

        public void Stop()
        {
            this.StopCalled = true;
        }

        public void ShowComments(List<Comment> comments)
        {
            this.Comments = comments;
            this.ShowCommentsCalled = true;
        }

        public void HideComments()
        {
            this.HideCommentsCalled = true;
        }

        public void StartBuffer()
        {
            this.StartBufferCalled = true;
        }

        public void EndBuffer()
        {
            this.EndBufferCalled = true;
        }

        public void HidePreviewImage()
        {
            this.HidePreviewImageCalled = true;
        }

        public void PausePlayer()
        {
            this.PausePlayerCalled = true;
        }

        public void PickThumbnail(MediaData mediaData, ThumbnailType thumbnailType)
        {
            this.PickThumbnailCalled = true;
        }

        public void ToggleSlowMotion(bool isChecked)
        {
            this.ToggleSlowMotionCalled = true;
        }

        public void ToggleSlowMotion(MediaData mediaData)
        {
            this.ToggleSlowMotionCalled = true;
        }

        public void ShowSequenceHasGapErrorMessage(bool isVisible)
        {
            this.ShowSequenceHasGapErrorMessageVisible = isVisible;
        }

        public void HandleKeyboardAction(KeyboardAction keyboardAction)
        {
        }

        public void RemoveAllElements()
        {
        }

        public void InvokePickThumbnailClicked()
        {
            EventHandler handler = this.PickThumbnailClicked;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public void InvokeSlowMotionClicked()
        {
            EventHandler handler = this.SlowMotionClicked;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public void EnablePlayButton(bool isEnabled)
        {
        }

        public void AddXamlElement(string xaml, IDictionary<string, string> properties, double positionX, double positionY, double height, double width)
        {
            throw new NotImplementedException();
        }

        public void RemoveOverlayPreviews()
        {
        }

        public void RemovePlaybackOverlays()
        {
        }

        public void AddOverlaysSupport()
        {
        }

        public void ShowErrorMessage(bool isVisible)
        {
        }
    }
}