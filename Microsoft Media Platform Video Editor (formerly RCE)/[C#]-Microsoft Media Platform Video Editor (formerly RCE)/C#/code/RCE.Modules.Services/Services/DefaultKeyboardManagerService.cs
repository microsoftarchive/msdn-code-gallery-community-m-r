// <copyright file="DefaultKeyboardManagerService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DefaultKeyboardManagerService.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services
{
    using System.Windows.Input;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Services;

    /// <summary>
    /// Default Implementation.
    /// </summary>
    public class DefaultKeyboardManagerService : KeyboardManagerService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultKeyboardManagerService"/> class.
        /// </summary>
        public DefaultKeyboardManagerService()
        {
            this.Configure();
        }

        /// <summary>
        /// Configure a standard map to test the solution.
        /// </summary>
        private void Configure()
        {
            // Shell
            this.Map(Key.F9, ModifierKeys.None, KeyboardAction.TogglePlay, KeyboardActionContext.Shell);
            this.Map(Key.F12, ModifierKeys.None, KeyboardAction.PlayTimeline, KeyboardActionContext.Shell);
            this.Map(Key.Escape, ModifierKeys.None, KeyboardAction.PausePlayer, KeyboardActionContext.Shell);
            this.Map(Key.S, ModifierKeys.Control, KeyboardAction.Save, KeyboardActionContext.Shell);

            // TimelineControl
            this.Map(Key.Add, ModifierKeys.None, KeyboardAction.ZoomIn, KeyboardActionContext.TimelineControl);
            this.Map(Key.Subtract, ModifierKeys.None, KeyboardAction.ZoomOut, KeyboardActionContext.TimelineControl);

            // MediaBin
            this.Map(Key.D2, ModifierKeys.Control, KeyboardAction.ActivateModel, KeyboardActionContext.MediaBin);
            this.Map(Key.Delete, ModifierKeys.None, KeyboardAction.DeleteAsset, KeyboardActionContext.MediaBin);
            this.Map(Key.X, ModifierKeys.Control, KeyboardAction.CutAsset, KeyboardActionContext.MediaBin);
            this.Map(Key.V, ModifierKeys.Control, KeyboardAction.PasteAsset, KeyboardActionContext.MediaBin);
            this.Map(Key.A, ModifierKeys.None, KeyboardAction.AddAsset, KeyboardActionContext.MediaBin);
            this.Map(Key.Enter, ModifierKeys.None, KeyboardAction.Search, KeyboardActionContext.MediaBin);

            // Metadata
            this.Map(Key.Enter, ModifierKeys.None, KeyboardAction.Search, KeyboardActionContext.Metadata);

            // VideoPreviewTimeline
            this.Map(Key.I, ModifierKeys.None, KeyboardAction.SetMarkIn, KeyboardActionContext.SubClip);
            this.Map(Key.O, ModifierKeys.None, KeyboardAction.SetMarkOut, KeyboardActionContext.SubClip);
            this.Map(Key.Enter, ModifierKeys.None, KeyboardAction.CustomAction, KeyboardActionContext.SubClip);

            // Library
            this.Map(Key.D1, ModifierKeys.Control, KeyboardAction.ActivateModel, KeyboardActionContext.Library);

            // Player
            this.Map(Key.H, ModifierKeys.None, KeyboardAction.Rewind, KeyboardActionContext.Player);
            this.Map(Key.L, ModifierKeys.None, KeyboardAction.Forward, KeyboardActionContext.Player);
            this.Map(Key.Home, ModifierKeys.None, KeyboardAction.MoveToStart, KeyboardActionContext.Player);
            this.Map(Key.End, ModifierKeys.None, KeyboardAction.MoveToEnd, KeyboardActionContext.Player);
            this.Map(Key.Q, ModifierKeys.None, KeyboardAction.LoopPlayback, KeyboardActionContext.Player);
            this.Map(Key.C, ModifierKeys.None, KeyboardAction.AddTimelineElement, KeyboardActionContext.Player);
            this.Map(Key.F, ModifierKeys.None, KeyboardAction.FullScreen, KeyboardActionContext.Player);
            this.Map(Key.Enter, ModifierKeys.None, KeyboardAction.FullScreen, KeyboardActionContext.Player);
            this.Map(Key.M, ModifierKeys.None, KeyboardAction.Metadata, KeyboardActionContext.Player);
            this.Map(Key.Z, ModifierKeys.None, KeyboardAction.Mute, KeyboardActionContext.Player);

            // Timeline
            this.Map(Key.S, ModifierKeys.None, KeyboardAction.Split, KeyboardActionContext.Timeline);
            this.Map(Key.Space, ModifierKeys.None, KeyboardAction.TogglePlay, KeyboardActionContext.Timeline);
            this.Map(Key.Delete, ModifierKeys.None, KeyboardAction.Delete, KeyboardActionContext.Timeline);
            this.Map(Key.Back, ModifierKeys.None, KeyboardAction.Delete, KeyboardActionContext.Timeline);
            this.Map(Key.Z, ModifierKeys.Control, KeyboardAction.Undo, KeyboardActionContext.Timeline);
            this.Map(Key.Y, ModifierKeys.Control, KeyboardAction.Redo, KeyboardActionContext.Timeline);
            this.Map(Key.F, ModifierKeys.None, KeyboardAction.ToggleEdit, KeyboardActionContext.Timeline);
            this.Map(Key.Up, ModifierKeys.None, KeyboardAction.ZoomIn, KeyboardActionContext.Timeline);
            this.Map(Key.Down, ModifierKeys.None, KeyboardAction.ZoomOut, KeyboardActionContext.Timeline);
            this.Map(Key.I, ModifierKeys.None, KeyboardAction.TrimElementIn, KeyboardActionContext.Timeline);
            this.Map(Key.O, ModifierKeys.None, KeyboardAction.TrimElementOut, KeyboardActionContext.Timeline);
            this.Map(Key.T, ModifierKeys.None, KeyboardAction.PickThumbnail, KeyboardActionContext.Timeline);
            this.Map(Key.J, ModifierKeys.None, KeyboardAction.PreviousClip, KeyboardActionContext.Timeline);
            this.Map(Key.K, ModifierKeys.None, KeyboardAction.NextClip, KeyboardActionContext.Timeline);

            // Comment
            this.Map(Key.Enter, ModifierKeys.None, KeyboardAction.Search, KeyboardActionContext.Comment);
            this.Map(Key.D3, ModifierKeys.Control, KeyboardAction.ActivateModel, KeyboardActionContext.Comment);

            // CommentEdit
            this.Map(Key.Enter, ModifierKeys.None, KeyboardAction.Save, KeyboardActionContext.CommentEdit);

            // Settings
            this.Map(Key.D7, ModifierKeys.Control, KeyboardAction.ActivateModel, KeyboardActionContext.Settings);

            // Titles
            this.Map(Key.D4, ModifierKeys.Control, KeyboardAction.ActivateModel, KeyboardActionContext.Titles);
            this.Map(Key.A, ModifierKeys.None, KeyboardAction.AddTitle, KeyboardActionContext.Titles);

            // Project
            this.Map(Key.D6, ModifierKeys.Control, KeyboardAction.ActivateModel, KeyboardActionContext.Projects);

            // Search
            this.Map(Key.Enter, ModifierKeys.None, KeyboardAction.Search, KeyboardActionContext.Search);
        }
    }
}
