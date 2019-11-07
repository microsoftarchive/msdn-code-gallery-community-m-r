// <copyright file="IProjectService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IProjectService.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Services
{
    using System;

    using RCE.Infrastructure.Models;

    /// <summary>
    /// Interface definition for project service which have all the methods, deals with the project.
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Occurs when [project retrieved].
        /// </summary>
        event EventHandler ProjectRetrieved;

        /// <summary>
        /// Occurs when the project retrieving fails.
        /// </summary>
        event EventHandler ProjectError;

        /// <summary>
        /// Occurs when the save of the project started.
        /// </summary>
        event EventHandler ProjectSaving;

        /// <summary>
        /// Occures when the saving of the project ended.
        /// </summary>
        event EventHandler<DataEventArgs<bool>> ProjectSaved;

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The <see cref="ProjectState"/>.</value>
        ProjectState State { get; }

        /// <summary>
        /// Gets the current project.
        /// </summary>
        /// <returns>The <see cref="Project"/>.</returns>
        Project GetCurrentProject();

        /// <summary>
        /// Saves the project.
        /// </summary>
        void SaveProject();

        Sequence CreateTimeline();
    }
}
