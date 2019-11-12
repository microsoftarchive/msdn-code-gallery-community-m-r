// <copyright file="IDataService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IDataService.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Contracts
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Web;

    /// <summary>
    /// Interface that defines the operations that are available to the RCE.
    /// </summary>
    [ServiceContract(Namespace = "http://schemas.microsoft.com/rce/")]
    public interface IDataService
    {
        /// <summary>
        /// Loads the MediaBin <see cref="Container" /> with a <see cref="ItemCollection" /> that contains the items in the media bin.
        /// </summary>
        /// <param name="mediaBinUri">The <see cref="Uri"/> of the media bin to load.</param>
        /// <returns>A <see cref="Container"/> with the media elements for the project.</returns>
        [OperationContract]
        MediaBin LoadMediaBin(Uri mediaBinUri);

        /// <summary>
        /// Loads a <see cref="TitleTemplate"/> from the repository.
        /// </summary>
        /// <returns>The <see cref="TitleTemplateCollection"/> of the titles template that were loaded.</returns>
        [OperationContract]
        TitleTemplateCollection LoadTitleTemplates();

        /// <summary>
        /// Loads a project from the repository returning back the details.
        /// </summary>
        /// <param name="site">The <see cref="Uri"/> of the project site.</param>
        /// <returns>The <see cref="Project"/> that exists at the specified <see cref="Uri"/>.</returns>
        [OperationContract]
        Project LoadProject(Uri site);
        
        /// <summary>
        /// Saves a project into the repository.
        /// </summary>
        /// <param name="project">The project to be saved.</param>
        /// <returns>true, indicates that the project was saved. false, that the save failed.</returns>
        [OperationContract]
        bool SaveProject(Project project);

        /// <summary>
        /// Get the projects collection of the given user.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <returns>A <see cref="ProjectCollection"/> with all the projects of the user.</returns>
        [OperationContract]
        ProjectCollection GetProjectsByUser(string userName);

        /// <summary>
        /// Gets a project from the repository returning back the details.
        /// </summary>
        /// <param name="projectId">The project id of the project.</param>
        /// <returns>The <see cref="Project"/> that exists at the specified <see cref="Uri"/>.</returns>
        [OperationContract]
        [WebGet(UriTemplate = "/Projects?id={projectId}")]
        Project GetProject(string projectId);

        /// <summary>
        /// Deletes the project.
        /// </summary>
        /// <param name="site">The <see cref="Uri"/> of the project site.</param>
        /// <returns>True if successful else false.</returns>
        [OperationContract]
        bool DeleteProject(Uri site);
    }
}
