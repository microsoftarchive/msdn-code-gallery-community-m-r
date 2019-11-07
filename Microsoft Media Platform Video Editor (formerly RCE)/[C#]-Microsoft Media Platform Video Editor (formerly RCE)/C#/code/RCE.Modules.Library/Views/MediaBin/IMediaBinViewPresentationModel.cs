// <copyright file="IMediaBinViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IMediaBinViewPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.MediaBin
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using Infrastructure;
    using Infrastructure.DragDrop;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Presentation.Commands;

    using RCE.Infrastructure.Services;

    /// <summary>
    /// Presention Model Interface for the Media Bin module.
    /// </summary>
    public interface IMediaBinViewPresentationModel : IHeaderInfoProvider<string>, INotifyPropertyChanged, IKeyboardAware
    {
        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>The S<see cref="IMediaBinView"/>.</value>
        IMediaBinView View { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show videos].
        /// </summary>
        /// <value>A <seealso cref="bool"/> value.<c>true</c> if [show videos]; otherwise, <c>false</c>.</value>
        bool ShowVideos { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show audio].
        /// </summary>
        /// <value>A <seealso cref="bool"/> value.<c>true</c> if [show audio]; otherwise, <c>false</c>.</value>
        bool ShowAudio { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show images].
        /// </summary>
        /// <value>A <seealso cref="bool"/> value.<c>true</c> if [show images]; otherwise, <c>false</c>.</value>
        bool ShowImages { get; set; }

        /// <summary>
        /// Gets a value indicating whether MediaBin view is active or not.
        /// </summary>
        /// <value>A <seealso cref="bool"/> value.<c>true</c> if this MediaBin is active; otherwise, <c>false</c>.</value>
        bool IsActive { get; }

        /// <summary>
        /// Gets or sets the scale value of the slider.
        /// </summary>
        /// <value>The scale.</value>
        double Scale { get; set; }

        /// <summary>
        /// Gets or sets the folder title.
        /// </summary>
        /// <value>The folder title.</value>
        string FolderTitle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance of help window is open.
        /// </summary>
        /// <value>
        /// A <seealso cref="bool"/> value.<c>true</c> if this instance is help window open; otherwise, <c>false</c>.
        /// </value>
        bool IsHelpWindowOpen { get; set; }

        /// <summary>
        /// Gets the header icon on.
        /// </summary>
        /// <value>The header icon on.</value>
        string HeaderIconOn { get; }

        /// <summary>
        /// Gets the header icon off.
        /// </summary>
        /// <value>The header icon off.</value>
        string HeaderIconOff { get; }

        /// <summary>
        /// Gets the search command.
        /// </summary>
        /// <value>The search command.</value>
        DelegateCommand<string> SearchCommand { get; }

        /// <summary>
        /// Gets the play selected asset command.
        /// </summary>
        /// <value>The play selected asset command.</value>
        DelegateCommand<object> PlaySelectedAssetCommand { get; }

        /// <summary>
        /// Gets the shift slider scale command.
        /// </summary>
        /// <value>The shift slider scale command.</value>
        DelegateCommand<string> ShiftSliderScaleCommand { get; }

        /// <summary>
        /// Gets the add folder command.
        /// </summary>
        /// <value>The add folder command.</value>
        DelegateCommand<string> AddFolderCommand { get; }

        /// <summary>
        /// Gets up arrow command.
        /// </summary>
        /// <value>Up arrow command.</value>
        DelegateCommand<string> UpArrowCommand { get; }

        /// <summary>
        /// Gets the help button command.
        /// </summary>
        /// <value>The help button command.</value>
        DelegateCommand<string> HelpButtonCommand { get; }

        /// <summary>
        /// Gets the delete asset button command.
        /// </summary>
        /// <value>The delete asset command.</value>
        DelegateCommand<string> DeleteAssetCommand { get; }

        /// <summary>
        /// Gets the command executed on drop elements.
        /// </summary>
        /// <value>The delegate command used to drop the elements.</value>
        DelegateCommand<DropPayload> DropCommand { get; }

        /// <summary>
        /// Gets or sets the assets.
        /// </summary>
        /// <value>The assets.</value>
        ObservableCollection<Asset> Assets { get; set; }

        /// <summary>
        /// Gets or sets the selected asset in thumbnail view or list view.
        /// </summary>
        /// <value>The selected asset.</value>
        Asset SelectedAsset { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether thumbnail view is visible or 
        /// list view is visible.
        /// </summary>
        /// <value>The value would be <c>true</c> if thumbnail view is visible; otherwise, <c>false</c>.</value>
        bool IsThumbChecked { get; set; }

        /// <summary>
        /// Called when [asset selected].
        /// </summary>
        /// <param name="asset">The asset.</param>
        void OnAssetSelected(Asset asset);

        /// <summary>
        /// Gets the media bin assets provider Uris.
        /// </summary>
        /// <returns>Returns the array of Provider Uris of the assets in MediaBin.</returns>
        string[] GetMediaBin();

        /// <summary>
        /// Adds the asset to timeline.
        /// </summary>
        /// <param name="asset">The asset.</param>
        void AddAssetToTimeline(Asset asset);

        /// <summary>
        /// Shows the metadata of the current selected timeline element.
        /// </summary>
        /// <param name="timelineElement">The timeline element.</param>
        void ShowMetadata(TimelineElement timelineElement);

        /// <summary>
        /// Deletes the current selected asset.
        /// </summary>
        void DeleteCurrentAsset();
    }
}
