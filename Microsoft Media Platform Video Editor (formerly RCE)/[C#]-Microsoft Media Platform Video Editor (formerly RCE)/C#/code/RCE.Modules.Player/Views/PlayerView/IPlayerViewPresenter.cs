// <copyright file="IPlayerViewPresenter.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IPlayerViewPresenter.cs                     
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
    using System.Windows.Media.Imaging;

    using Microsoft.Practices.Composite.Presentation.Commands;

    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    /// <summary>
    /// Interface for the player view presenter.
    /// </summary>
    public interface IPlayerViewPresenter : IKeyboardAware
    {
        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>The <see cref="IPlayerView"/>.</value>
        IPlayerView View { get; set; }

        /// <summary>
        /// Gets or sets the player mode.
        /// </summary>
        /// <value>The <see cref="PlayerMode"/>.</value>
        PlayerMode PlayerMode { get; set; }

        /// <summary>
        /// Gets the command executed on fast rewind.
        /// </summary>
        /// <value>The delegate command used to start/stop fast rewind.</value>
        DelegateCommand<object> FastRewindCommand { get; }

        /// <summary>
        /// Gets the command executed on fast forward.
        /// </summary>
        /// <value>The delegate command used to start/stop fast forward.</value>
        DelegateCommand<object> FastForwardCommand { get; }

        DelegateCommand<object> MoveToStartCommand { get; }

        DelegateCommand<object> MoveToEndCommand { get; }

        DelegateCommand<object> MediaRepeatCommand { get; }

        DelegateCommand<object> AddTimelineElementCommand { get; }

        DelegateCommand<object> MuteCommand { get; }

        /// <summary>
        /// Publishes the ThumbnailEvent.
        /// </summary>
        /// <param name="bitmap">The bitmap being published.</param>
        void SetThumbnail(WriteableBitmap bitmap, ThumbnailType thumbnailType);
    }
}