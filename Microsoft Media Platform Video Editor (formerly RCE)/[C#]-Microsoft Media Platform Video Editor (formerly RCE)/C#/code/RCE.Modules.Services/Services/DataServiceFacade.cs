// <copyright file="DataServiceFacade.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DataServiceFacade.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Windows.Browser;
    using DataService;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Translators;
    using RCE.Infrastructure.Services;
    using MediaBin = RCE.Infrastructure.Models.MediaBin;
    using Project = RCE.Infrastructure.Models.Project;
    using TitleTemplate = RCE.Infrastructure.Models.TitleTemplate;

    /// <summary>
    /// Interacts with server to get the data.
    /// </summary>
    public class DataServiceFacade : IDataServiceFacade
    {
        /// <summary>
        /// The <see cref="ILogger"/> to log the exceptions.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// The service address.
        /// </summary>
        private readonly Uri serviceAddress;

        /// <summary>
        /// True if the service sent a save project request and waiting for the response.
        /// </summary>
        private bool isSaving;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataServiceFacade"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="configurationService">The configuration service.</param>
        public DataServiceFacade(ILogger logger, IConfigurationService configurationService)
        {
            this.logger = logger;
            string serviceUriString = configurationService.GetParameterValue("DataServiceUrl");

            this.serviceAddress = new Uri(serviceUriString, UriKind.Absolute);
        }

        /// <summary>
        /// Occurs when [load media bin asset completed].
        /// </summary>
        public event EventHandler<DataEventArgs<MediaBin>> LoadMediaBinAssetCompleted;

        /// <summary>
        /// Occurs when [load project completed].
        /// </summary>
        public event EventHandler<DataEventArgs<Project>> LoadProjectCompleted;

        /// <summary>
        /// Occurs when [save project completed].
        /// </summary>
        public event EventHandler<DataEventArgs<bool>> SaveProjectCompleted;

        /// <summary>
        /// Occurs when [load title templates completed].
        /// </summary>
        public event EventHandler<DataEventArgs<List<TitleTemplate>>> LoadTitleTemplatesCompleted;

        /// <summary>
        /// The handler that invokes when GetProjectsByUser method completed.
        /// </summary>
        public event EventHandler<DataEventArgs<List<Project>>> GetProjectsByUserCompleted;

        /// <summary>
        /// The handler that invokes when DeleteProject method completed.
        /// </summary>
        public event EventHandler<DataEventArgs<bool>> DeleteProjectCompleted;

        /// <summary>
        /// Loads the media bin assets asynchronously.
        /// </summary>
        /// <param name="containerUri">The container URI.</param>
        public void LoadMediaBinAssetsAsync(Uri containerUri)
        {
            DataServiceClient client = this.CreateDataServiceClient();

            client.LoadMediaBinCompleted += (sender, args) =>
                                                {
                                                    if (args.Error != null)
                                                    {
                                                        client.Abort();
                                                        this.logger.Log(this.GetType().Name, args.Error);

                                                        if (args.Error.GetType() == typeof(Exception))
                                                        {
                                                            throw args.Error;
                                                        }

                                                        return;
                                                    }

                                                    try
                                                    {
                                                        MediaBin mediaBin = DataServiceTranslator.ConvertToMediaBin(args.Result);
                                                        this.OnLoadMediaBinAssetsCompleted(new DataEventArgs<MediaBin>(mediaBin));
                                                    }
                                                    catch (Exception e)
                                                    {
                                                        client.Abort();
                                                        this.logger.Log(this.GetType().Name, e);
                                                        throw;
                                                    }
                                                };

            client.LoadMediaBinAsync(containerUri);
        }

        /// <summary>
        /// Loads the project asynchronously.
        /// </summary>
        /// <param name="projectUri">The project URI.</param>
        public void LoadProjectAsync(Uri projectUri)
        {
            this.OnLoadProjectCompleted(new DataEventArgs<Project>(null, null));

            if (projectUri == null)
            {
                return;
            }

            DataServiceClient client = this.CreateDataServiceClient();

            client.LoadProjectCompleted += (sender, args) =>
                                               {
                                                   if (args.Error != null)
                                                   {
                                                       client.Abort();
                                                       this.logger.Log(this.GetType().Name, args.Error);

                                                       if (args.Error.GetType() == typeof(Exception))
                                                       {
                                                           throw args.Error;
                                                       }

                                                       return;
                                                   }

                                                   try
                                                   {
                                                       Project project = DataServiceTranslator.ConvertToProject(args.Result);
                                                       this.OnLoadProjectCompleted(new DataEventArgs<Project>(project));
                                                   }
                                                   catch (Exception e)
                                                   {
                                                       client.Abort();
                                                       this.logger.Log(this.GetType().Name, e);
                                                       throw;
                                                   }
                                               };
           
            client.LoadProjectAsync(projectUri);
        }

        /// <summary>
        /// Saves the project asynchronously.
        /// </summary>
        /// <param name="project">The project.</param>
        public void SaveProjectAsync(Project project)
        {
            if (!this.isSaving)
            {
                this.isSaving = true;

                DataServiceClient client = this.CreateDataServiceClient();

                RCE.Services.Contracts.Project dataProject = DataServiceTranslator.ConvertToDataServiceProject(project);

                string projectId = null;
                if (HtmlPage.Document.QueryString.TryGetValue("projectId", out projectId))
                {
                    dataProject.HighlightId = Guid.Parse(projectId);
                }

                client.SaveProjectCompleted += (sender, args) =>
                {
                    this.isSaving = false;

                    if (args.Error != null)
                    {
                        client.Abort();
                        this.logger.Log(this.GetType().Name, args.Error);

                        if (args.Error.GetType() == typeof(Exception))
                        {
                            throw args.Error;
                        }

                        return;
                    }

                    this.OnSaveProjectCompleted(new DataEventArgs<bool>(args.Result));
                };

                client.SaveProjectAsync(dataProject);
            }
        }

        /// <summary>
        /// Loads the title templates asynchronously.
        /// </summary>
        public void LoadTitleTemplatesAsync()
        {
            DataServiceClient client = this.CreateDataServiceClient();

            client.LoadTitleTemplatesCompleted += (sender, args) =>
                                                      {
                                                          if (args.Error != null)
                                                          {
                                                              client.Abort();
                                                              this.logger.Log(this.GetType().Name, args.Error);

                                                              if (args.Error.GetType() == typeof(Exception))
                                                              {
                                                                  throw args.Error;
                                                              }

                                                              return;
                                                          }

                                                          try
                                                          {
                                                              List<TitleTemplate> titleTemplates = DataServiceTranslator.ConvertToTitleTemplates(args.Result);
                                                              this.OnLoadTitleTemplatesCompleted(new DataEventArgs<List<TitleTemplate>>(titleTemplates));
                                                          }
                                                          catch (Exception e)
                                                          {
                                                              client.Abort();
                                                              this.logger.Log(this.GetType().Name, e);
                                                              throw;
                                                          }
                                                      };

            client.LoadTitleTemplatesAsync();
        }

        /// <summary>
        /// Get the list of projects for the given user.
        /// </summary>
        /// <param name="userName">The username.</param>
        public void GetProjectsByUserAsync(string userName)
        {
            DataServiceClient client = this.CreateDataServiceClient();

            client.GetProjectsByUserCompleted += (sender, args) =>
                                                     {
                                                         if (args.Error != null)
                                                         {
                                                             client.Abort();
                                                             this.logger.Log(this.GetType().Name, args.Error);

                                                             if (args.Error.GetType() == typeof(Exception))
                                                             {
                                                                 throw args.Error;
                                                             }

                                                             return;
                                                         }

                                                         try
                                                         {
                                                             List<Project> projects = DataServiceTranslator.ConvertToProjects(args.Result);
                                                             this.OnGetProjectsByUserCompleted(new DataEventArgs<List<Project>>(projects));
                                                         }
                                                         catch (Exception e)
                                                         {
                                                             client.Abort();
                                                             this.logger.Log(this.GetType().Name, e);
                                                             throw;
                                                         }
                                                     };

            client.GetProjectsByUserAsync(userName);
        }

        /// <summary>
        /// Deletes the project.
        /// </summary>
        /// <param name="site">The uri of the poject.</param>
        public void DeleteProject(Uri site)
        {
            DataServiceClient client = this.CreateDataServiceClient();

            client.DeleteProjectCompleted += (sender, args) =>
                                                 {
                                                     if (args.Error != null)
                                                     {
                                                         client.Abort();
                                                         this.logger.Log(this.GetType().Name, args.Error);

                                                         if (args.Error.GetType() == typeof(Exception))
                                                         {
                                                             throw args.Error;
                                                         }

                                                         return;
                                                     }

                                                     this.OnDeleteProjectCompleted(new DataEventArgs<bool>(args.Result));
                                                 };

            client.DeleteProjectAsync(site);
        }

        /// <summary>
        /// Called when [load MediaBin assets completed].
        /// </summary>
        /// <param name="e">The instance of event arguments.</param>
        private void OnLoadMediaBinAssetsCompleted(DataEventArgs<MediaBin> e)
        {
            EventHandler<DataEventArgs<MediaBin>> loadMediaBinAssetsCompletedHandler = this.LoadMediaBinAssetCompleted;
            if (loadMediaBinAssetsCompletedHandler != null)
            {
                loadMediaBinAssetsCompletedHandler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="LoadProjectCompleted"/> event.
        /// </summary>
        /// <param name="e">The instance containing the event data.</param>
        private void OnLoadProjectCompleted(DataEventArgs<Project> e)
        {
            EventHandler<DataEventArgs<Project>> loadProjectCompletedHandler = this.LoadProjectCompleted;
            if (loadProjectCompletedHandler != null)
            {
                loadProjectCompletedHandler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="SaveProjectCompleted"/> event.
        /// </summary>
        /// <param name="e">The instance containing the event data.</param>
        private void OnSaveProjectCompleted(DataEventArgs<bool> e)
        {
            EventHandler<DataEventArgs<bool>> saveProjectCompletedHandler = this.SaveProjectCompleted;
            if (saveProjectCompletedHandler != null)
            {
                saveProjectCompletedHandler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="DeleteProjectCompleted"/> event.
        /// </summary>
        /// <param name="e">The instance containing the event data.</param>
        private void OnDeleteProjectCompleted(DataEventArgs<bool> e)
        {
            EventHandler<DataEventArgs<bool>> deleteProjectCompletedHandler = this.DeleteProjectCompleted;
            if (deleteProjectCompletedHandler != null)
            {
                deleteProjectCompletedHandler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="LoadTitleTemplatesCompleted"/> event.
        /// </summary>
        /// <param name="e">The instance containing the event data.</param>
        private void OnLoadTitleTemplatesCompleted(DataEventArgs<List<TitleTemplate>> e)
        {
            EventHandler<DataEventArgs<List<TitleTemplate>>> loadTitleTemplatesCompletedHandler = this.LoadTitleTemplatesCompleted;
            if (loadTitleTemplatesCompletedHandler != null)
            {
                loadTitleTemplatesCompletedHandler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="GetProjectsByUserCompleted"/> event.
        /// </summary>
        /// <param name="e">The instance containing the event data.</param>
        private void OnGetProjectsByUserCompleted(DataEventArgs<List<Project>> e)
        {
            EventHandler<DataEventArgs<List<Project>>> getProjectsByUserHandler = this.GetProjectsByUserCompleted;
            if (getProjectsByUserHandler != null)
            {
                getProjectsByUserHandler(this, e);
            }
        }

        /// <summary>
        /// Creates the dataservice client.
        /// </summary>
        /// <returns>The data service client.</returns>
        private DataServiceClient CreateDataServiceClient()
        {
            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.None) 
            { 
                Name = "DataServiceBinding",
                MaxReceivedMessageSize = 2147483647,
                MaxBufferSize = 2147483647,
            };

            EndpointAddress endpointAddress = new EndpointAddress(this.serviceAddress);

            return new DataServiceClient(binding, endpointAddress);
        }
    }
}