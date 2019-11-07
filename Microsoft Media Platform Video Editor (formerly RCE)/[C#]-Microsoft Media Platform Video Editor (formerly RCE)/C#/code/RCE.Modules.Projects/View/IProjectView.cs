// <copyright file="IProjectView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IProjectView.cs                     
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
    /// <summary>
    /// Interface that definces the <see cref="ProjectView"/>.
    /// </summary>
    public interface IProjectView
    {
        /// <summary>
        /// Gets or sets the <see cref="IProjectViewPresenter"/> presentation model of the view.
        /// </summary>
        /// <value>A <see also="IProjectViewPresenter"/> that represents the presentation model of the view.</value>
        IProjectViewPresenter Model { get; set; }
    }
}
