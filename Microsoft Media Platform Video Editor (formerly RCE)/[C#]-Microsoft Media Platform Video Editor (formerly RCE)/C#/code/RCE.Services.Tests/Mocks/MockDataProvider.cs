// <copyright file="MockDataProvider.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockDataProvider.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services.Tests.Mocks
{
    using System;
    using Contracts;

    public class MockDataProvider : IDataProvider
    {
        /// <summary>
        /// Gets or sets a value indicating whether or not an exception should be thrown;
        /// </summary>
        public bool ThrowException { get; set; }

        public Project LoadProjectResult { get; set; }

        public bool LoadProjectCalled { get; set; }

        public MediaBin LoadMediaBinResult { get; set; }

        public bool LoadMediaBinCalled { get; set; }

        public Container LoadLibraryResult { get; set; }

        public bool SaveProjectResult { get; set; }

        public bool SaveProjectCalled { get; set; }

        public bool CreateIdCalled { get; set; }

        public Uri CreateIdResult { get; set; }

        public bool GetProjectsByUserCalled { get; set; }

        public ProjectCollection GetProjectsByUserResult { get; set; }

        public bool LoadTitleTemplatesCalled { get; set; }

        public TitleTemplateCollection LoadTitleTemplatesResult { get; set; }

        public bool DeleteProjectCalled { get; set; }

        public bool DeleteProjectResult { get; set; }

        public TitleTemplateCollection LoadTitleTemplates()
        {
            this.LoadTitleTemplatesCalled = true;

            if (this.ThrowException)
            {
                throw new Exception("I was told to throw an exception, so I am.");
            }

            return this.LoadTitleTemplatesResult;
        }

        /// <summary>
        /// Loads a project from the repository returning back the details
        /// </summary>
        /// <param name="site">The <see cref="Uri"/> of the project site.</param>
        /// <returns>The <see cref="Project"/> that exists at the specified <see cref="Uri"/>.</returns>
        public virtual Project LoadProject(Uri site)
        {
            this.LoadProjectCalled = true;

            if (this.ThrowException)
            {
                throw new Exception("I was told to throw an exception, so I am.");
            }

            return this.LoadProjectResult;
        }

        public virtual bool SaveProject(Project project)
        {
            this.SaveProjectCalled = true;

            if (this.ThrowException)
            {
                throw new Exception("I was told to throw an exception, so I am.");
            }

            return this.SaveProjectResult;
        }

        public MediaBin LoadMediaBin(Uri project)
        {
            this.LoadMediaBinCalled = true;

            if (this.ThrowException)
            {
                throw new Exception("I was told to throw an exception, so I am.");
            }

            return this.LoadMediaBinResult;
        }

        /// <summary>
        /// Get the projects collection of the given user.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <returns>A <see cref="ProjectCollection"/> with all the projects of the user.</returns>
        public ProjectCollection GetProjectsByUser(string userName)
        {
            this.GetProjectsByUserCalled = true;

            if (this.ThrowException)
            {
                throw new Exception("I was told to throw an exception, so I am.");
            }

            return this.GetProjectsByUserResult;
        }

        /// <summary>
        /// Deletes the project.
        /// </summary>
        /// <param name="site">The <see cref="Uri"/> of the project site.</param>
        public bool DeleteProject(Uri site)
        {
            this.DeleteProjectCalled = true;

            if (this.ThrowException)
            {
                throw new Exception("I was told to throw an exception, so I am.");
            }

            return this.DeleteProjectResult;
        }
    }
}
