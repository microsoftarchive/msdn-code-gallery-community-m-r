// <copyright file="ICommentsBarPresenter.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ICommentsBarPresenter.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.CommentsBar
{
    using System.Collections.Generic;
    using Microsoft.Practices.Composite.Presentation.Commands;

    /// <summary>
    /// Interface that defines a settings view presentation model.
    /// </summary>
    public interface ICommentsBarPresenter
    {
        /// <summary>
        /// Gets the command being executed when an option is being selected.
        /// </summary>
        /// <value>The command being executed when an option is selected.</value>
        DelegateCommand<string> OptionSelectedCommand { get; }

        /// <summary>
        /// Gets or sets the <see cref="ICommentsBarView"/> presentation model of the view.
        /// </summary>
        /// <value>A <see also="ICommentsBarView"/> that represents the presentation model of the view.</value>
        ICommentsBarView View { get; set; }

        /// <summary>
        /// Gets the list of available options to show in the timeline bar.
        /// </summary>
        /// <value>The list of available options.</value>
        IList<string> Options { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the options menu is visible or not.
        /// </summary>
        /// <value>A true if the options menu is visible;otherwise false.</value>
        bool IsOptionMenuVisible { get; set; }
    }
}