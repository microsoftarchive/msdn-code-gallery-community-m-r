// <copyright file="ISettingsViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ISettingsViewPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Settings
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Media;
    using Infrastructure;
    using Microsoft.Practices.Composite.Presentation.Commands;

    using RCE.Infrastructure.Services;

    /// <summary>
    /// Interface that defines a settings view presentation model.
    /// </summary>
    public interface ISettingsViewPresentationModel : IHeaderInfoProvider<string>, INotifyPropertyChanged, IKeyboardAware
    {
        /// <summary>
        /// Gets or sets the <see cref="ISettingsView"/> of the presentation model.
        /// </summary>
        /// <value>A <see also="ISeetingsView"/> that represents the current view of the presentation model.</value>
        ISettingsView View { get; set; }

        /// <summary>
        /// Gets a collection of Smpte timecode values.
        /// </summary>
        /// <value>A <see also="IList{T}"/> of <see cref="string"/> with the Smpte timecode values.</value>
        IList<string> SmpteTimeCodeValues { get; }

        /// <summary>
        /// Gets a collection of Edit mode values.
        /// </summary>
        /// <value>A <see also="IList{T}"/> of <see cref="string"/> with the edit mode values.</value>
        IList<string> EditModeValues { get; }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>The name of the project.</value>
        string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the 4:3 aspect ratio is selected.
        /// </summary>
        /// <value>A <seealso cref="bool"/> that represents if the 4:3 aspect ratio is selected. true, indicates that the 4:3 aspect ratio is selected. false, that is not selected.</value>
        bool IsAspectRatio43Selected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the 16:9 aspect ratio is selected.
        /// </summary>
        /// <value>A <seealso cref="bool"/> that represents if the 16:9 aspect ratio is selected. true, indicates that the 16:9 aspect ratio is selected. false, that is not selected.</value>
        bool IsAspectRatio169Selected { get; set; }

        /// <summary>
        /// Gets or sets the selected Smpte timecode.
        /// </summary>
        /// <value>An <seealso cref="string" /> that represents the selected Smpte timecode.</value>
        string SelectedSmpteTimeCode { get; set; }

        /// <summary>
        /// Gets or sets the selected start timecode.
        /// </summary>
        /// <value>An <seealso cref="string" /> that represents the selected start timecode.</value>
        string SelectedStartTimeCode { get; set; }

        /// <summary>
        /// Gets or sets the selected edit mode.
        /// </summary>
        /// <value>An <seealso cref="string" /> that represents the selected edit mode.</value>
        string SelectedEditMode { get; set; }

        /// <summary>
        /// Gets or sets the project thumbnail.
        /// </summary>
        /// <value>An ImageSource that represents the project thumbnail.</value>
        ImageSource ThumbImage { get; set; }

        /// <summary>
        /// Gets the command used to delete the current thumbnail.
        /// </summary>
        /// <value>The command to delete the current thumbnail.</value>
        DelegateCommand<object> DeleteThumbnailCommand { get; }

        /// <summary>
        /// Gets the command used to pick a new thumbnail.
        /// </summary>
        /// <value>The command to pick a new thumbnail.</value>
        DelegateCommand<object> PickThumbnailCommand { get; }

        /// <summary>
        /// Gets or sets the selected auto save time interval used to save the project periodically.
        /// </summary>
        /// <value>An <seealso cref="int" /> that represents the selected auto save time interval.</value>
        int SelectedAutoSaveTimeInterval { get; set; }

        /// <summary>
        /// Gets the header icon (on status).
        /// </summary>
        /// <value>An <seealso cref="string" /> that represents the header icon on resource.</value>
        string HeaderIconOn { get; }

        /// <summary>
        /// Gets the header icon (off status).
        /// </summary>
        /// <value>An <seealso cref="string" /> that represents the header icon off resource.</value>
        string HeaderIconOff { get; }

        /// <summary>
        /// Increases quota.
        /// </summary>
        DelegateCommand<object> IncreaseStorageQuotaCommand { get; }

        /// <summary>
        /// Clear Storage.
        /// </summary>
        DelegateCommand<object> ClearStorageCommand { get; }

        /// <summary>
        /// Set Keyboard Default.
        /// </summary>
        DelegateCommand<object> ApplyKeyboardMappingCommand { get; }

        /// <summary>
        /// Save the current project.
        /// </summary>
        void SaveProject();
    }
}