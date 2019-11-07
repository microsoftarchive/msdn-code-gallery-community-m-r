// <copyright file="IProjectViewPresenter.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IProjectViewPresenter.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Projects
{
    using System.Collections.ObjectModel;
    using Infrastructure;
    using Microsoft.Practices.Composite.Presentation.Commands;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    /// <summary>
    /// Interface that defines a <see cref="IProjectViewPresenter"/> presenter.
    /// </summary>
    public interface IProjectViewPresenter : IHeaderInfoProvider<string>, IKeyboardAware 
    {
        /// <summary>
        /// Gets or sets the <see cref="IProjectView"/> of the presenter.
        /// </summary>
        /// <value>A <see also="IProjectView"/> that represents the current view of the presenter.</value>
        IProjectView View { get; set; }

        /// <summary>
        /// Gets the header info.
        /// </summary>
        /// <value>The header info.</value>
        string HeaderIconOn { get; }

        /// <summary>
        /// Gets the Header Icon.
        /// </summary>
        /// <value>The header icon name.</value>
        string HeaderIconOff { get; }

        /// <summary>
        /// Gets or sets the Projects of the presenter.
        /// </summary>
        /// <value>A List of projects for the given user.</value>
        ObservableCollection<Project> Projects { get; set; }

        /// <summary>
        /// Gets the command that deletes the selected Project.
        /// </summary>
        /// <value>The <see cref="DelegateCommand{T}"/>.</value>
        DelegateCommand<object> DeleteCommand { get; }
    }
}
