// <copyright file="ITimelinePresenter.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ITimelinePresenter.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Timeline
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using Infrastructure.DragDrop;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Presentation.Commands;

    using RCE.Infrastructure.Services;

    using SMPTETimecode;

    /// <summary>
    /// Interface that defines a <see cref="ITimelinePresenter"/> presenter.
    /// </summary>
    public interface ITimelinePresenter : INotifyPropertyChanged, IKeyboardAware
    {
        /// <summary>
        /// Gets the <see cref="ITimelineView"/> of the presenter.
        /// </summary>
        /// <value>A <see also="ITimelineView"/> that represents the current view of the presenter.</value>
        ITimelineView View { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the timeline is in snap mode or not.
        /// </summary>
        /// <value>A true if the timeline is in snap mode;otherwise false.</value>
        bool IsInSnapMode { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the timeline is in ripple mode or not.
        /// </summary>
        /// <value>A true if the timeline is in ripple mode;otherwise false.</value>
        bool IsInRippleMode { get; set; }

        TimeCode TimelineDuration { get; }

        /// <summary>
        /// Gets the command to add audio tracks.
        /// </summary>
        /// <value>The delegate command used to add audio tracks.</value>
        DelegateCommand<object> AddAudioTrackCommand { get; }

        /// <summary>
        /// Gets the command to remove audio tracks.
        /// </summary>
        /// <value>The delegate command used to remove audio tracks.</value>
        DelegateCommand<object> RemoveAudioTrackCommand { get; }

        /// <summary>
        /// Gets the command executed on drop elements.
        /// </summary>
        /// <value>The delegate command used to drop the elements.</value>
        DelegateCommand<DropPayload> DropCommand { get; }

        /// <summary>
        /// Gets the command to move frame backward and forward.
        /// </summary>
        /// <value>The move frame command.</value>
        DelegateCommand<object> MoveFrameCommand { get; }

        /// <summary>
        /// Gets the command to move to the next clip.
        /// </summary>
        /// <value>The command to move to the next clip.</value>
        DelegateCommand<object> MoveNextClipCommand { get; }

        /// <summary>
        /// Gets the command to move to the previous clip.
        /// </summary>
        /// <value>The command to move to the previous clip.</value>
        DelegateCommand<object> MovePreviousClipCommand { get; }

        bool IsTimelineLocked { get; set; }

        /// <summary>
        /// Gets the list of available audio tracks.
        /// </summary>
        /// <value>The list of available audio tracks.</value>
        ObservableCollection<Track> AudioTracks { get; }

        ObservableCollection<Track> VideoTracks { get; }

        DelegateCommand<object> LockCommand { get; set; }

        void AlignSelectedElementsToPlayheadPosition();
    }
}