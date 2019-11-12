// <copyright file="SqlDataProviderTranslatorFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SqlDataProviderTranslatorFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Data.Sql.Tests.Translators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RCE.Data.Sql.Translators;
    using RCE.Services.Contracts;
    using SqlComment = RCE.Data.Sql.Comment;
    using SqlContainer = RCE.Data.Sql.Container;
    using SqlItem = RCE.Data.Sql.Item;
    using SqlProject = RCE.Data.Sql.Project;
    using SqlResource = RCE.Data.Sql.Resource;
    using SqlShot = RCE.Data.Sql.Shot;
    using SqlTitle = RCE.Data.Sql.Title;
    using SqlTitleTemplate = RCE.Data.Sql.TitleTemplate;
    using SqlTrack = RCE.Data.Sql.Track;

    /// <summary>
    /// A class for testing the <see cref="SqlDataProviderTranslator"/>.
    /// </summary>
    [TestClass]
    public class SqlDataProviderTranslatorFixture
    {
        /// <summary>
        /// Tests that a shot is converted to a sql shot.
        /// </summary>
        [TestMethod]
        public void ShouldConvertShotToSqlShot()
        {
            var shot = SqlHelper.CreateShot();
            var sqlShot = new SqlShot();

            var item = SqlHelper.CreateSqlItem();
            item.Id = 1;
            item.Resources.First().Id = 1;

            var items = new List<SqlItem> { item };

            SqlDataProviderTranslator.ConvertToSqlShot(shot, sqlShot, items);

            SqlHelper.AssertShot(shot, sqlShot);
        }

        /// <summary>
        /// Tests that a comment is converted to a sql comment.
        /// </summary>
        [TestMethod]
        public void ShouldConvertCommentToSqlComment()
        {
            var comment = SqlHelper.CreateComment();
            var sqlComment = new SqlComment();

            SqlDataProviderTranslator.ConvertToSqlComment(comment, sqlComment);

            SqlHelper.AssertComment(comment, sqlComment);
        }

        /// <summary>
        /// Tests that an ink comment is converted to a sql comment.
        /// </summary>
        [TestMethod]
        public void ShouldConvertInkCommentToSqlComment()
        {
            var comment = SqlHelper.CreateInkComment();
            var sqlComment = new SqlComment();

            SqlDataProviderTranslator.ConvertToSqlComment(comment, sqlComment);

            SqlHelper.AssertComment(comment, sqlComment);
        }

        /// <summary>
        /// Tests that a track is converted to a sql track.
        /// </summary>
        [TestMethod]
        public void ShouldConvertTrackToSqlTrack()
        {
            var track = SqlHelper.CreateTrack();
            var sqlTrack = new SqlTrack();

            SqlDataProviderTranslator.ConvertToSqlTrack(track, sqlTrack);

            SqlHelper.AssertTrack(track, sqlTrack);
        }

        /// <summary>
        /// Tests that a title is converted to a sql title.
        /// </summary>
        [TestMethod]
        public void ShouldConvertTitleToSqlTitle()
        {
            var title = SqlHelper.CreateTitle();
            var sqlTitle = new SqlTitle();

            SqlDataProviderTranslator.ConvertToSqlTitle(title, sqlTitle);

            SqlHelper.AssertTitle(title, sqlTitle);
        }

        /// <summary>
        /// Tests that a project is converted to a sql project.
        /// </summary>
        [TestMethod]
        public void ShouldConvertProjectToSqlProject()
        {
            var project = SqlHelper.CreateProject();
            var sqlProject = new SqlProject();

            SqlDataProviderTranslator.ConvertToSqlProject(project, sqlProject);

            SqlHelper.AssertProject(project, sqlProject);
        }

        /// <summary>
        /// Tests that a media bin is converted to a sql container.
        /// </summary>
        [TestMethod]
        public void ShouldConvertMediaBinToSqlContainer()
        {
            var mediaBin = SqlHelper.CreateMediaBin();
            var sqlMediaBin = new SqlContainer();

            var item = SqlHelper.CreateSqlItem();
            item.Id = 1;
            item.Resources.First().Id = 1;

            var items = new List<SqlItem> { item };

            var result = SqlDataProviderTranslator.ConvertToSqlMediaBin(mediaBin, sqlMediaBin, items);

            SqlHelper.AssertContainer(mediaBin, result);
        }

        /// <summary>
        /// Tests that a list of sql title templates is converted to a title template collection.
        /// </summary>
        [TestMethod]
        public void ShouldConvertListOfSqlTitleTemplatesToTitleCollection()
        {
            var sqlTitleTemplates = new List<SqlTitleTemplate>();
            
            sqlTitleTemplates.Add(SqlHelper.CreateSqlTitleTemplate());
            sqlTitleTemplates.Add(SqlHelper.CreateSqlTitleTemplate());

            var titleTemplates = SqlDataProviderTranslator.ConvertToTitleTemplates(sqlTitleTemplates);

            SqlHelper.AssertTitleTemplate(titleTemplates[0], sqlTitleTemplates[0]);
            SqlHelper.AssertTitleTemplate(titleTemplates[1], sqlTitleTemplates[1]);
        }

        /// <summary>
        /// Tests that a sql project is converted to a project.
        /// </summary>
        [TestMethod]
        public void ShouldConvertSqlProjectToProject()
        {
            var sqlProject = SqlHelper.CreateSqlProject();
            
            var project = SqlDataProviderTranslator.ConvertToProject(sqlProject, null);

            SqlHelper.AssertProject(sqlProject, project);
        }

        /// <summary>
        /// Tests that a list of sql projects is converted to a project collection.
        /// </summary>
        [TestMethod]
        public void ShouldConvertListOfSqlProjectsToProjectCollection()
        {
            var sqlProjects = new List<SqlProject>();

            sqlProjects.Add(SqlHelper.CreateSqlProject());
            sqlProjects.Add(SqlHelper.CreateSqlProject());

            var projects = SqlDataProviderTranslator.ConvertToProjects(sqlProjects, null);

            SqlHelper.AssertProject(sqlProjects[0], projects[0]);
            SqlHelper.AssertProject(sqlProjects[1], projects[1]);
        }

        /// <summary>
        /// Tests that a sql container is converted to a container.
        /// </summary>
        [TestMethod]
        public void ShouldConvertSqlContainerToContainer()
        {
            var sqlContainer = SqlHelper.CreateSqlContainer();

            var container = SqlDataProviderTranslator.ConvertToContainer(sqlContainer, null);

            SqlHelper.AssertContainer(sqlContainer, container);
        }

        /// <summary>
        /// Tests that a sql container is converted to a container.
        /// </summary>
        [TestMethod]
        public void ShouldConvertSqlContainerToContainerAndLimitTheItemsToTheValuePassed()
        {
            var maxNumberOfItems = 1;
            var sqlContainer = SqlHelper.CreateSqlContainer();

            var container = SqlDataProviderTranslator.ConvertToContainer(sqlContainer, null, maxNumberOfItems, null);

            SqlHelper.AssertContainer(sqlContainer, container, maxNumberOfItems);
        }

        /// <summary>
        /// Tests that a sql container is converted to a media bin.
        /// </summary>
        [TestMethod]
        public void ShouldConvertSqlContainerToAMediaBin()
        {
            var sqlContainer = SqlHelper.CreateSqlContainer();

            var mediaBin = SqlDataProviderTranslator.ConvertToMediaBin(sqlContainer, null);

            SqlHelper.AssertContainer(sqlContainer, mediaBin);
        }
    }
}
