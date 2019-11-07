// <copyright file="ILibraryViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ILibraryViewPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Library
{
    using System.Collections.Generic;
    using System.ComponentModel;

    using Microsoft.Practices.Composite.Presentation.Commands;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    /// <summary>
    /// Presentation model for the Library view.
    /// </summary>
    public interface ILibraryViewPresentationModel : IHeaderInfoProvider<string>, INotifyPropertyChanged, IKeyboardAware
    {
        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>The <see cref="LibraryView"/>.</value>
        ILibraryView View { get; set; }

        /// <summary>
        /// Gets or sets the assets.
        /// </summary>
        /// <value>The assets.</value>
        List<Asset> Assets { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether videos are visible or not.
        /// </summary>
        /// <value>A <seealso cref="bool"/> value.<c>true</c> if  videos are visible; otherwise, <c>false</c>.</value>
        bool ShowVideos { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether audios are visible or not.
        /// </summary>
        /// <value>A <seealso cref="bool"/> value.<c>true</c> if  audios are visible; otherwise, <c>false</c>.</value>
        bool ShowAudio { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether pictures are visible or not.
        /// </summary>
        /// <value>A <seealso cref="bool"/> value.<c>true</c> if  pictures are visible; otherwise, <c>false</c>.</value>
        bool ShowImages { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Help window is open.
        /// </summary>
        /// <value>
        /// A <seealso cref="bool"/> value.<c>true</c> if help window is open; otherwise, <c>false</c>.
        /// </value>
        bool IsHelpWindowOpen { get; set; }

        /// <summary>
        /// Gets or sets the scale value of the slider.
        /// </summary>
        /// <value>The scale.</value>
        double Scale { get; set; }

        /// <summary>
        /// Gets the header icon on.
        /// </summary>
        /// <value>The header icon on.</value>
        string HeaderIconOn { get; }

        /// <summary>
        /// Gets a value indicating whether current view is active.
        /// </summary>
        /// <value>A <seealso cref="bool"/> value.<c>true</c> if current view is active; otherwise, <c>false</c>.</value>
        bool IsActive { get; }

        /// <summary>
        /// Gets the header icon off.
        /// </summary>
        /// <value>The header icon off.</value>
        string HeaderIconOff { get; }

        /// <summary>
        /// Gets the play selected asset command.
        /// </summary>
        /// <value>The play selected asset command.</value>
        DelegateCommand<object> PlaySelectedAssetCommand { get; }

        /// <summary>
        /// Gets the command for adding an item to media bin.
        /// </summary>
        /// <value>The add item command.</value>
        DelegateCommand<object> AddItemCommand { get; }

        /// <summary>
        /// Gets the command to open/hide the help window.
        /// </summary>
        /// <value>The help button command.</value>
        DelegateCommand<string> HelpButtonCommand { get; }

        /// <summary>
        /// Gets up command to move to the up folder.
        /// </summary>
        /// <value>Up arrow command.</value>
        DelegateCommand<string> UpArrowCommand { get; }

        /// <summary>
        /// Called when any asset is added to MediaBin.
        /// </summary>
        /// <param name="asset">The asset.</param>
        void OnAddAsset(Asset asset);

        /// <summary>
        /// Called when double clicked on any asset.
        /// </summary>
        /// <param name="asset">The asset.</param>
        void OnAssetSelected(Asset asset);

        /// <summary>
        /// Shows the metadata of the current selected timeline element.
        /// </summary>
        /// <param name="timelineElement">The current timeline element.</param>
        void ShowMetadata(TimelineElement timelineElement);
    }
}