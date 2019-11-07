// <copyright file="DataService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DataService.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services
{
    using System;
    using System.ServiceModel.Activation;
    using LAgger;
    using RCE.Services.Contracts;
    using TraceEventType = LAgger.TraceEventType;

    /// <summary>
    /// Provides the implementation for <see cref="IDataService"/> that will connect to the registered data provider to load the data.
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DataService : IDataService
    {
        /// <summary>
        /// The <see cref="ILoggerService"/>.
        /// </summary>
        private readonly ILoggerService loggerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataService"/> class with the specified data provider.
        /// </summary>
        /// <param name="provider">The data provider.</param>
        /// <param name="loggerService">The logger.</param>
        public DataService(IDataProvider provider, ILoggerService loggerService)
        {
            this.Provider = provider;
            this.loggerService = loggerService;
        }

        /// <summary>
        /// Gets the <see cref="IDataProvider"/> that is used for the implementation.
        /// </summary>
        /// <value>The data provider to retrieve data.</value>
        public IDataProvider Provider { get; private set; }

        /// <summary>
        /// Loads a <see cref="TitleTemplate"/> from the repository.
        /// </summary>
        /// <returns>The <see cref="TitleTemplateCollection"/> of the titles template that were loaded.</returns>
        public virtual TitleTemplateCollection LoadTitleTemplates()
        {
            try
            {
                return this.Provider.LoadTitleTemplates();
            }
            catch (Exception ex)
            {
                this.Log(ex);

                throw;
            }
        }

        /// <summary>
        /// Loads the MediaBin <see cref="Container" /> with a <see cref="ItemCollection" /> that contains the items in the media bin.
        /// </summary>
        /// <param name="mediaBinUri">The <see cref="Uri"/> of the media bin to load.</param>
        /// <returns>A <see cref="Container"/> with the media elements for the project.</returns>
        public virtual MediaBin LoadMediaBin(Uri mediaBinUri)
        {
            try
            {
                return this.Provider.LoadMediaBin(mediaBinUri);
            }
            catch (Exception ex)
            {
                this.Log(ex);

                throw;
            }
        }

        /// <summary>
        /// Loads a project from the repository returning back the details.
        /// </summary>
        /// <param name="site">The <see cref="Uri"/> of the project site.</param>
        /// <returns>The <see cref="Project"/> that exists at the specified <see cref="Uri"/>.</returns>
        public virtual Project LoadProject(Uri site)
        {
            try
            {
                return this.Provider.LoadProject(site);
            }
            catch (Exception ex)
            {
                this.Log(ex);

                throw;
            }
        }

        /// <summary>
        /// Saves a project into the repository.
        /// </summary>
        /// <param name="project">The project to be saved.</param>
        /// <returns>true, indicates that the project was saved. false, that the save failed.</returns>
        public virtual bool SaveProject(Project project)
        {
            try
            {
                return this.Provider.SaveProject(project);
            }
            catch (Exception ex)
            {
                this.Log(ex);

                throw;
            }
        }

        /// <summary>
        /// Get the projects collection of the given user.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <returns>A <see cref="ProjectCollection"/> with all the projects of the user.</returns>
        public ProjectCollection GetProjectsByUser(string userName)
        {
            try
            {
                return this.Provider.GetProjectsByUser(userName);
            }
            catch (Exception ex)
            {
                this.Log(ex);

                throw;
            }
        }

        /// <summary>
        /// Loads a project from the repository returning back the details.
        /// </summary>
        /// <param name="projectId">The projectId of the project site.</param>
        /// <returns>The <see cref="Project"/> that exists at the specified <see cref="Uri"/>.</returns>
        public virtual Project GetProject(string projectId)
        {
            try
            {
                if (Uri.IsWellFormedUriString(projectId, UriKind.Absolute))
                {
                    return this.LoadProject(new Uri(projectId));
                }

                return null;
            }
            catch (Exception ex)
            {
                this.Log(ex);

                throw;
            }
        }

        /// <summary>
        /// Deletes the project.
        /// </summary>
        /// <param name="site">The <see cref="Uri"/> of the project site.</param>
        /// <returns>True if successfull else false.</returns>
        public bool DeleteProject(Uri site)
        {
            try
            {
                return this.Provider.DeleteProject(site);
            }
            catch (Exception ex)
            {
                this.Log(ex);

                throw;
            }
        }

        /// <summary>
        /// Logs the specified ex.
        /// </summary>
        /// <param name="ex">The Exception.</param>
        private void Log(Exception ex)
        {
            string message = UtilityHelper.FormatExceptionMessage(ex);

            LogEntry entry = new LogEntry(message, "Data Service", 1, 0, TraceEventType.Error, "Error in Data Service");

            this.loggerService.LogEntries(new[] { entry });
        }
    }
}
