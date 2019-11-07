// <copyright file="IDataServiceFacade.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IDataServiceFacade.cs                     
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
    using System.Collections.Generic;

    using RCE.Infrastructure.Models;

    /// <summary>
    /// Data service interface which interacts with the server to get the data.
    /// </summary>
    public interface IDataServiceFacade
    {
        /// <summary>
        /// Occurs when [load media bin asset completed].
        /// </summary>
        event EventHandler<DataEventArgs<MediaBin>> LoadMediaBinAssetCompleted;

        /// <summary>
        /// Occurs when [load project completed].
        /// </summary>
        event EventHandler<DataEventArgs<Project>> LoadProjectCompleted;

        /// <summary>
        /// Occurs when [save project completed].
        /// </summary>
        event EventHandler<DataEventArgs<bool>> SaveProjectCompleted;

        /// <summary>
        /// Occurs when [load title templates completed].
        /// </summary>
        event EventHandler<DataEventArgs<List<TitleTemplate>>> LoadTitleTemplatesCompleted;

        /// <summary>
        /// The handler that invokes when GetProjectsByUser method completed.
        /// </summary>
        event EventHandler<DataEventArgs<List<Project>>> GetProjectsByUserCompleted;

        /// <summary>
        /// The handler that invokes when DeleteProject method completed.
        /// </summary>
        event EventHandler<DataEventArgs<bool>> DeleteProjectCompleted;

        /// <summary>
        /// Loads the media bin assets asynchronously.
        /// </summary>
        /// <param name="containerUri">The container URI.</param>
        void LoadMediaBinAssetsAsync(Uri containerUri);

        /// <summary>
        /// Loads the project asynchronously.
        /// </summary>
        /// <param name="projectUri">The project URI.</param>
        void LoadProjectAsync(Uri projectUri);

        /// <summary>
        /// Saves the project asynchronously.
        /// </summary>
        /// <param name="project">The project.</param>
        void SaveProjectAsync(Project project);

        /// <summary>
        /// Loads the title templates asynchronously.
        /// </summary>
        void LoadTitleTemplatesAsync();

        /// <summary>
        /// Get the projects of the given user.
        /// </summary>
        /// <param name="userName">The user name.</param>
        void GetProjectsByUserAsync(string userName);

        /// <summary>
        /// Deletes the project.
        /// </summary>
        /// <param name="site">The Uri of the project.</param>
        void DeleteProject(Uri site);
    }
}
