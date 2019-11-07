// <copyright file="DataProviderFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DataProviderFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Data.Sql.Tests
{
    using System;
    using System.Configuration;
    using System.Data.Objects.DataClasses;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Transactions;
    using Microsoft.SqlServer.Management.Common;
    using Microsoft.SqlServer.Management.Smo;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using RCE.Services.Contracts;
    using Comment = RCE.Data.Sql.Comment;
    using Container = RCE.Data.Sql.Container;
    using Item = RCE.Data.Sql.Item;
    using Project = RCE.Data.Sql.Project;
    using Resource = RCE.Data.Sql.Resource;
    using Shot = RCE.Data.Sql.Shot;
    using Title = RCE.Data.Sql.Title;
    using TitleTemplate = RCE.Data.Sql.TitleTemplate;
    using Track = RCE.Data.Sql.Track;

    /// <summary>
    /// A class for testing the <see cref="DataProvider"/>.
    /// </summary>
    [TestClass]
    public class DataProviderFixture
    {
        private const string IdUrlTemplate = "http://rce.litwareinc.com/id/{0}";

        /// <summary>
        /// The mocked logger service.
        /// </summary>
        private MockLoggerService loggerService;
 
        /// <summary>
        /// Initializes resources need it by the tests.
        /// </summary>
        /// <param name="context">The context.</param>
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            FileInfo file = new FileInfo(@".\Data\CreateDatabase.sql");
            string createDbScript = file.OpenText().ReadToEnd();
            createDbScript = createDbScript.Replace("RoughCutEditor", "RoughCutEditorTest");
            ExecuteDbScript(createDbScript);
        }

        /// <summary>
        /// Clean up the resources created.
        /// </summary>
        [ClassCleanup]
        public static void CleanUp()
        {
            string dropDbScript =
                @"IF EXISTS (SELECT * FROM [sys].[databases] WHERE [name] = N'RoughCutEditorTest')
BEGIN
	ALTER DATABASE RoughCutEditorTest SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE RoughCutEditorTest;
END;";
            ExecuteDbScript(dropDbScript);
        }

        /// <summary>
        /// Initializes the resources need it by the tests.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.loggerService = new MockLoggerService();
        }

        /// <summary>
        /// Tests that the library is being loaded.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@".\Data", @".\Data")]
        public void ShouldLoadLibrary()
        {
            var context = new RoughCutEditorEntities();

            using (TransactionScope transactionScope = new TransactionScope())
            {
                var libraryId = CreateId(typeof(Services.Contracts.Container).Name);
                var dataProvider = new DataProvider(this.loggerService, null, libraryId);

                Container container = Container.CreateContainer(1);
                container.Title = "Test";

                Container childContainer = Container.CreateContainer(2);
                childContainer.Title = "ChildContainer";

                Item item = SqlHelper.CreateSqlVideoItem();
                item.Id = 1;
                item.Resources.First().Id = 1;
                item.Resources.First().VideoFormat.Id = 1;

                Item childItem = SqlHelper.CreateSqlVideoItem();
                childItem.Id = 2;
                childItem.Resources.First().Id = 2;
                childItem.Resources.First().VideoFormat.Id = 2;

                container.Items.Add(item);
                childContainer.Items.Add(childItem);

                container.Containers.Add(childContainer);
                context.AddToContainer(container);

                context.SaveChanges();

                var library = dataProvider.LoadLibrary(1000);

                Assert.AreEqual(container.Title, library.Title);
                Assert.AreEqual(1, library.Items.Count);
                Assert.AreEqual(1, library.Containers.Count);
                Assert.AreEqual(childContainer.Title, library.Containers[0].Title);
                Assert.AreEqual(0, library.Containers[0].Items.Count);
                Assert.AreEqual(string.Format(IdUrlTemplate, item.Id), library.Items[0].Id.ToString());
                Assert.AreEqual(item.Title, library.Items[0].Title);
                Assert.AreEqual(item.Description, library.Items[0].Description);
                Assert.IsInstanceOfType(library.Items[0], typeof(VideoItem));
                Assert.AreEqual(1, library.Items[0].Resources.Count);
                Assert.AreEqual(string.Format(IdUrlTemplate, item.Resources.First().Id), library.Items[0].Resources[0].Id.ToString());
                Assert.AreEqual(item.Resources.First().Ref, library.Items[0].Resources[0].Ref);
                Assert.AreEqual(item.Resources.First().ResourceType, library.Items[0].Resources[0].ResourceType);
                Assert.AreEqual(item.Resources.First().VideoFormat.Duration, ((VideoItem)library.Items[0]).Duration);
                Assert.AreEqual(item.Resources.First().VideoFormat.FrameRate, ((VideoItem)library.Items[0]).FrameRate.ToString());
                Assert.AreEqual(item.Resources.First().VideoFormat.ResolutionX, ((VideoItem)library.Items[0]).Width);
                Assert.AreEqual(item.Resources.First().VideoFormat.ResolutionY, ((VideoItem)library.Items[0]).Height);
            }
        }

        /// <summary>
        /// Tests that the library is being loaded.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@".\Data", @".\Data")]
        public void ShouldLoadLibraryById()
        {
            var context = new RoughCutEditorEntities();

            using (TransactionScope transactionScope = new TransactionScope())
            {
                var libraryId = CreateId(typeof(Services.Contracts.Container).Name);
                var dataProvider = new DataProvider(this.loggerService, null, libraryId);

                Container container = Container.CreateContainer(1);
                container.Title = "Test";

                var childLibraryId = CreateId(typeof(Services.Contracts.Container).Name);

                Container childContainer = Container.CreateContainer(2);
                childContainer.Title = "ChildContainer";

                Item item = SqlHelper.CreateSqlVideoItem();
                item.Id = 1;
                item.Resources.First().Id = 1;
                item.Resources.First().VideoFormat.Id = 1;

                Item childItem = SqlHelper.CreateSqlVideoItem();
                childItem.Id = 2;
                childItem.Resources.First().Id = 2;
                childItem.Resources.First().VideoFormat.Id = 2;

                container.Items.Add(item);
                childContainer.Items.Add(childItem);

                container.Containers.Add(childContainer);
                context.AddToContainer(container);

                context.SaveChanges();

                var library = dataProvider.LoadLibraryById(childLibraryId, 1000);

                Assert.AreEqual(container.Title, library.Title);
                Assert.AreEqual(1, library.Items.Count);
                Assert.AreEqual(1, library.Containers.Count);
                Assert.AreEqual(string.Format(IdUrlTemplate, item.Id), library.Items[0].Id.ToString());
                Assert.AreEqual(childItem.Title, library.Items[0].Title);
                Assert.AreEqual(childItem.Description, library.Items[0].Description);
                Assert.IsInstanceOfType(library.Items[0], typeof(VideoItem));
                Assert.AreEqual(1, library.Items[0].Resources.Count);
                Assert.AreEqual(string.Format(IdUrlTemplate, item.Resources.First().Id), library.Items[0].Resources[0].Id.ToString());
                Assert.AreEqual(childItem.Resources.First().Ref, library.Items[0].Resources[0].Ref);
                Assert.AreEqual(childItem.Resources.First().ResourceType, library.Items[0].Resources[0].ResourceType);
                Assert.AreEqual(childItem.Resources.First().VideoFormat.Duration, ((VideoItem)library.Items[0]).Duration);
                Assert.AreEqual(childItem.Resources.First().VideoFormat.FrameRate, ((VideoItem)library.Items[0]).FrameRate.ToString());
                Assert.AreEqual(childItem.Resources.First().VideoFormat.ResolutionX, ((VideoItem)library.Items[0]).Width);
                Assert.AreEqual(childItem.Resources.First().VideoFormat.ResolutionY, ((VideoItem)library.Items[0]).Height);
            }
        }

        /// <summary>
        /// Tests that the media bin is being loaded.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@".\Data", @".\Data")]
        public void ShouldLoadMediaBin()
        {
            var context = new RoughCutEditorEntities();

            using (TransactionScope transactionScope = new TransactionScope())
            {
                var libraryId = CreateId(typeof(Services.Contracts.Container).Name);
                var dataProvider = new DataProvider(this.loggerService, null, libraryId);

                Project project = Project.CreateProject(1, "creator", DateTime.Now);

                Item item = SqlHelper.CreateSqlVideoItem();
                item.Id = 1;
                item.Resources.First().Id = 1;
                item.Resources.First().VideoFormat.Id = 1;
                Item childItem = SqlHelper.CreateSqlVideoItem();
                childItem.Id = 2;
                childItem.Resources.First().Id = 2;
                childItem.Resources.First().VideoFormat.Id = 2;

                context.AddToItem(item);
                context.AddToItem(childItem);

                context.SaveChanges();

                Container mediaBin = Container.CreateContainer(1);
                mediaBin.Items.Add(item);

                Container childContainer = Container.CreateContainer(2);
                childContainer.Items.Add(childItem);
                mediaBin.Containers.Add(childContainer);

                context.AddToContainer(mediaBin);

                context.SaveChanges();

                var mediaBinContainer = dataProvider.LoadMediaBin(new Uri(string.Format(IdUrlTemplate, mediaBin.Id)));

                Assert.AreEqual(1, mediaBinContainer.Items.Count);
                Assert.AreEqual(1, mediaBinContainer.Containers.Count);
                Assert.AreEqual(0, mediaBinContainer.Containers[0].Items.Count);
                Assert.AreEqual(string.Format(IdUrlTemplate, item.Id), mediaBinContainer.Items[0].Id.ToString());
                Assert.AreEqual(item.Title, mediaBinContainer.Items[0].Title);
                Assert.AreEqual(item.Description, mediaBinContainer.Items[0].Description);
                Assert.IsInstanceOfType(mediaBinContainer.Items[0], typeof(VideoItem));
                Assert.AreEqual(1, mediaBinContainer.Items[0].Resources.Count);
                Assert.AreEqual(string.Format(IdUrlTemplate, item.Resources.First().Id), mediaBinContainer.Items[0].Resources[0].Id.ToString());
                Assert.AreEqual(item.Resources.First().Ref, mediaBinContainer.Items[0].Resources[0].Ref);
                Assert.AreEqual(item.Resources.First().VideoFormat.Duration, ((VideoItem)mediaBinContainer.Items[0]).Duration);
                Assert.AreEqual(item.Resources.First().VideoFormat.FrameRate, ((VideoItem)mediaBinContainer.Items[0]).FrameRate.ToString());
                Assert.AreEqual(item.Resources.First().VideoFormat.ResolutionX, ((VideoItem)mediaBinContainer.Items[0]).Width);
                Assert.AreEqual(item.Resources.First().VideoFormat.ResolutionY, ((VideoItem)mediaBinContainer.Items[0]).Height);
            }
        }

        /// <summary>
        /// Tests that the title templates are being loaded.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@".\Data", @".\Data")]
        public void ShouldLoadTitleTemplates()
        {
            var context = new RoughCutEditorEntities();

            using (TransactionScope transactionScope = new TransactionScope())
            {
                var libraryId = CreateId(typeof(Services.Contracts.Container).Name);
                var dataProvider = new DataProvider(this.loggerService, null, libraryId);

                TitleTemplate titleTemplate = SqlHelper.CreateSqlTitleTemplate();
                context.AddToTitleTemplate(titleTemplate);

                context.SaveChanges();

                var titleTemplates = dataProvider.LoadTitleTemplates();

                Assert.AreEqual(1, titleTemplates.Count);
                Assert.AreEqual(string.Format(IdUrlTemplate, titleTemplate.Id), titleTemplates[0].Id.ToString());
                Assert.AreEqual(titleTemplate.TemplateName, titleTemplates[0].TemplateName);
            }
        }

        /// <summary>
        /// Tests that the project of an user are being loaded.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@".\Data", @".\Data")]
        public void ShouldRetrieveProjectsByUser()
        {
            var context = new RoughCutEditorEntities();

            using (TransactionScope transactionScope = new TransactionScope())
            {
                var libraryId = CreateId(typeof(Services.Contracts.Container).Name);
                var dataProvider = new DataProvider(this.loggerService, null, libraryId);

                Project project1 = Project.CreateProject(1, "creator", DateTime.Now);
                project1.Duration = 100;
                project1.Resolution = "HD";
                project1.AutoSaveInterval = 100;
                project1.StartTimeCode = 3600;
                project1.SmpteFrameRate = "Smpte2997NonDrop";
                project1.RippleMode = true;

                Container mediaBin1 = Container.CreateContainer(1);

                project1.MediaBin = mediaBin1;

                Project project2 = Project.CreateProject(2, "notthesamecreator", DateTime.Now);
                project2.Duration = 100;
                project2.Resolution = "HD";
                project2.AutoSaveInterval = 100;
                project2.StartTimeCode = 3600;
                project2.SmpteFrameRate = "Smpte2997NonDrop";
                project2.RippleMode = true;

                Container mediaBin2 = Container.CreateContainer(2);

                project2.MediaBin = mediaBin2;

                context.AddToProject(project1);
                context.AddToProject(project2);
                context.SaveChanges();

                var projects = dataProvider.GetProjectsByUser("creator");

                Assert.AreEqual(1, projects.Count());
                Assert.AreEqual(project1.Duration, projects[0].Duration);
                Assert.AreEqual(project1.Resolution, projects[0].Resolution);
                Assert.AreEqual(project1.AutoSaveInterval, projects[0].AutoSaveInterval);
                Assert.AreEqual(project1.StartTimeCode, projects[0].StartTimeCode);
                Assert.AreEqual(project1.SmpteFrameRate, projects[0].SmpteFrameRate);
                Assert.AreEqual(project1.RippleMode, projects[0].RippleMode);
                Assert.AreEqual(project1.Creator, projects[0].Creator);
            }
        }

        /// <summary>
        /// Tests that a project is being loaded.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@".\Data", @".\Data")]
        public void ShouldLoadProject()
        {
            var context = new RoughCutEditorEntities();

            using (TransactionScope transactionScope = new TransactionScope())
            {
                var libraryId = CreateId(typeof(Services.Contracts.Container).Name);
                var dataProvider = new DataProvider(this.loggerService, null, libraryId);

                Item item = Item.CreateItem(1, "title", "Video");
                item.Description = "description";
                item.Resources = new EntityCollection<Resource>();
                Resource resource = Resource.CreateResource(1, "\test\test.wmv", "Master");
                resource.VideoFormat = VideoFormat.CreateVideoFormat(1);
                resource.VideoFormat.Duration = 10;
                resource.VideoFormat.FrameRate = "Smpte25";
                resource.VideoFormat.ResolutionX = 200;
                resource.VideoFormat.ResolutionY = 500;
                item.Resources.Add(resource);

                context.AddToItem(item);

                TitleTemplate titleTemplate = TitleTemplate.CreateTitleTemplate(1, "TemplateName");

                context.AddToTitleTemplate(titleTemplate);

                context.SaveChanges();

                Project project = Project.CreateProject(1, "creator", DateTime.Now);
                project.Duration = 100;
                project.Resolution = "HD";
                project.AutoSaveInterval = 100;
                project.StartTimeCode = 3600;
                project.SmpteFrameRate = "Smpte2997NonDrop";
                project.RippleMode = true;

                Container mediaBin = Container.CreateContainer(1);
                mediaBin.Items.Add(item);

                Container container = Container.CreateContainer(2);
                container.Items.Add(item);
                mediaBin.Containers.Add(container);

                project.MediaBin = mediaBin;

                Comment comment = Comment.CreateComment(3, "Text", "Timeline", "user", DateTime.Today);
                comment.MarkIn = 1200;
                comment.MarkOut = 1500;

                Comment shotComment = Comment.CreateComment(4, "Text", "Shot", "user", DateTime.Today);
                shotComment.MarkIn = 170;
                shotComment.MarkOut = 180;

                project.Comments.Add(comment);
                project.Comments.Add(shotComment);

                Track track = Track.CreateTrack(1, "Video");

                Shot shot = Shot.CreateShot(1);
                shot.Item = item;
                shot.ItemMarkIn = 150;
                shot.ItemMarkOut = 200;
                shot.TrackMarkIn = 500;
                shot.TrackMarkOut = 0;
                shot.Volume = 5;
                shot.Comments.Add(shotComment);

                track.Shots = new EntityCollection<Shot>();
                track.Shots.Add(shot);
                
                project.Tracks.Add(track);

                Title title = Title.CreateTitle(1, 600, 100, "Main", "Sub");
                title.TitleTemplate = titleTemplate;

                project.Titles.Add(title);

                context.AddToProject(project);

                context.SaveChanges();

                var retrievedProject = dataProvider.LoadProject(new Uri(string.Format(IdUrlTemplate, project.Id)));

                Assert.AreEqual(project.Duration, retrievedProject.Duration);
                Assert.AreEqual(project.Resolution, retrievedProject.Resolution);
                Assert.AreEqual(project.AutoSaveInterval, retrievedProject.AutoSaveInterval);
                Assert.AreEqual(project.StartTimeCode, retrievedProject.StartTimeCode);
                Assert.AreEqual(project.SmpteFrameRate, retrievedProject.SmpteFrameRate);
                Assert.AreEqual(project.RippleMode, retrievedProject.RippleMode);
                Assert.AreEqual(project.Creator, retrievedProject.Creator);
                Assert.IsNotNull(retrievedProject.MediaBin);
                Assert.AreEqual(string.Format(IdUrlTemplate, mediaBin.Id), retrievedProject.MediaBin.Id.ToString());
                Assert.AreEqual(1, retrievedProject.MediaBin.Items.Count);
                Assert.AreEqual(1, retrievedProject.MediaBin.Containers.Count);
                Assert.AreEqual(0, retrievedProject.MediaBin.Containers[0].Items.Count);
                Assert.AreEqual(1, retrievedProject.Sequences[0].Tracks.Count);
                Assert.AreEqual(string.Format(IdUrlTemplate, track.Id), retrievedProject.Sequences[0].Tracks[0].Id.ToString());
                Assert.AreEqual(track.TrackType, retrievedProject.Sequences[0].Tracks[0].TrackType);
                Assert.AreEqual(track.Shots.Count, retrievedProject.Sequences[0].Tracks[0].Shots.Count);
                Assert.AreEqual(string.Format(IdUrlTemplate, shot.Id), retrievedProject.Sequences[0].Tracks[0].Shots[0].Id.ToString());
                Assert.AreEqual(shot.TrackMarkIn, retrievedProject.Sequences[0].Tracks[0].Shots[0].TrackAnchor.MarkIn);
                Assert.AreEqual(shot.TrackMarkOut, retrievedProject.Sequences[0].Tracks[0].Shots[0].TrackAnchor.MarkOut);
                Assert.AreEqual(shot.ItemMarkIn, retrievedProject.Sequences[0].Tracks[0].Shots[0].SourceAnchor.MarkIn);
                Assert.AreEqual(shot.ItemMarkOut, retrievedProject.Sequences[0].Tracks[0].Shots[0].SourceAnchor.MarkOut);
                Assert.AreEqual(shot.Volume, retrievedProject.Sequences[0].Tracks[0].Shots[0].Volume);
                Assert.AreEqual(1, retrievedProject.Sequences[0].Tracks[0].Shots[0].Comments.Count);
                Assert.AreEqual(string.Format(IdUrlTemplate, shotComment.Id), retrievedProject.Sequences[0].Tracks[0].Shots[0].Comments[0].Id.ToString());
                Assert.AreEqual(shotComment.Creator, retrievedProject.Sequences[0].Tracks[0].Shots[0].Comments[0].Creator);
                Assert.AreEqual(shotComment.Created, retrievedProject.Sequences[0].Tracks[0].Shots[0].Comments[0].Created);
                Assert.AreEqual(shotComment.Text, retrievedProject.Sequences[0].Tracks[0].Shots[0].Comments[0].Text);
                Assert.AreEqual(shotComment.CommentType, retrievedProject.Sequences[0].Tracks[0].Shots[0].Comments[0].Type);
                Assert.AreEqual(shotComment.MarkIn, retrievedProject.Sequences[0].Tracks[0].Shots[0].Comments[0].MarkIn);
                Assert.AreEqual(shotComment.MarkOut, retrievedProject.Sequences[0].Tracks[0].Shots[0].Comments[0].MarkOut);
                Assert.AreEqual(2, retrievedProject.Comments.Count);
                Assert.AreEqual(string.Format(IdUrlTemplate, comment.Id), retrievedProject.Comments[1].Id.ToString());
                Assert.AreEqual(comment.Created, retrievedProject.Comments[1].Created);
                Assert.AreEqual(comment.Creator, retrievedProject.Comments[1].Creator);
                Assert.AreEqual(comment.Text, retrievedProject.Comments[1].Text);
                Assert.AreEqual(comment.CommentType, retrievedProject.Comments[1].Type);
                Assert.AreEqual(comment.MarkIn, retrievedProject.Comments[1].MarkIn);
                Assert.AreEqual(comment.MarkOut, retrievedProject.Comments[1].MarkOut);
                Assert.AreEqual(string.Format(IdUrlTemplate, shotComment.Id), retrievedProject.Comments[0].Id.ToString());
                Assert.AreEqual(shotComment.Created, retrievedProject.Comments[0].Created);
                Assert.AreEqual(shotComment.Creator, retrievedProject.Comments[0].Creator);
                Assert.AreEqual(shotComment.Text, retrievedProject.Comments[0].Text);
                Assert.AreEqual(shotComment.CommentType, retrievedProject.Comments[0].Type);
                Assert.AreEqual(shotComment.MarkIn, retrievedProject.Comments[0].MarkIn);
                Assert.AreEqual(shotComment.MarkOut, retrievedProject.Comments[0].MarkOut);
                Assert.AreEqual(string.Format(IdUrlTemplate, item.Id), retrievedProject.Sequences[0].Tracks[0].Shots[0].Source.Id.ToString());
                Assert.AreEqual(item.Title, retrievedProject.Sequences[0].Tracks[0].Shots[0].Source.Title);
                Assert.AreEqual(item.Description, retrievedProject.Sequences[0].Tracks[0].Shots[0].Source.Description);
                Assert.IsInstanceOfType(retrievedProject.Sequences[0].Tracks[0].Shots[0].Source, typeof(VideoItem));
                Assert.AreEqual(string.Format(IdUrlTemplate, item.Resources.First().Id), retrievedProject.Sequences[0].Tracks[0].Shots[0].Source.Resources[0].Id.ToString());
                Assert.AreEqual(item.Resources.First().Ref, retrievedProject.Sequences[0].Tracks[0].Shots[0].Source.Resources[0].Ref);
                Assert.AreEqual(item.Resources.First().VideoFormat.Duration, ((VideoItem)retrievedProject.Sequences[0].Tracks[0].Shots[0].Source).Duration);
                Assert.AreEqual(item.Resources.First().VideoFormat.FrameRate, ((VideoItem)retrievedProject.Sequences[0].Tracks[0].Shots[0].Source).FrameRate.ToString());
                Assert.AreEqual(item.Resources.First().VideoFormat.ResolutionX, ((VideoItem)retrievedProject.Sequences[0].Tracks[0].Shots[0].Source).Width);
                Assert.AreEqual(item.Resources.First().VideoFormat.ResolutionY, ((VideoItem)retrievedProject.Sequences[0].Tracks[0].Shots[0].Source).Height);
                Assert.AreEqual(1, retrievedProject.Titles.Count);
                Assert.AreEqual(string.Format(IdUrlTemplate, title.Id), retrievedProject.Titles[0].Id.ToString());
                Assert.AreEqual(title.MainText, retrievedProject.Titles[0].TextBlockCollection[0].Text);
                Assert.AreEqual(title.SubText, retrievedProject.Titles[0].TextBlockCollection[1].Text);
                Assert.AreEqual(title.TrackMarkIn, retrievedProject.Titles[0].TrackAnchor.MarkIn);
                Assert.AreEqual(title.TrackMarkOut, retrievedProject.Titles[0].TrackAnchor.MarkOut);
                Assert.AreEqual(string.Format(IdUrlTemplate, title.TitleTemplate.Id), retrievedProject.Titles[0].TitleTemplate.Id.ToString());
                Assert.AreEqual(title.TitleTemplate.TemplateName, retrievedProject.Titles[0].TitleTemplate.TemplateName);
            }
        }

        /// <summary>
        /// Tests that a project is being saved.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@".\Data", @".\Data")]
        public void ShouldSaveProject()
        {
            var context = new RoughCutEditorEntities();

            using (TransactionScope transactionScope = new TransactionScope())
            {
                var libraryId = CreateId(typeof(Services.Contracts.Container).Name);
                var dataProvider = new DataProvider(this.loggerService, null, libraryId);

                Item item = Item.CreateItem(1, "title", "Video");
                item.Description = "description";
                item.Resources = new EntityCollection<Resource>();
                Resource resource = Resource.CreateResource(1, "\test\test.wmv", "Master");
                resource.VideoFormat = VideoFormat.CreateVideoFormat(1);
                resource.VideoFormat.Duration = 10;
                resource.VideoFormat.FrameRate = "Smpte25";
                resource.VideoFormat.ResolutionX = 200;
                resource.VideoFormat.ResolutionY = 500;
                item.Resources.Add(resource);

                context.AddToItem(item);

                TitleTemplate titleTemplate = TitleTemplate.CreateTitleTemplate(1, "TestTemplateName");

                context.AddToTitleTemplate(titleTemplate);

                context.SaveChanges();

                Services.Contracts.VideoItem videoItem = new Services.Contracts.VideoItem();
                videoItem.Id = new Uri(string.Format(IdUrlTemplate, 1));
                videoItem.Title = "title";
                videoItem.Description = "description";

                Services.Contracts.Resource videoResource = new Services.Contracts.Resource();
                videoResource.Id = new Uri(string.Format(IdUrlTemplate, 1));
                videoResource.Ref = "\test\test.wmv";

                videoItem.Resources.Add(videoResource);

                Services.Contracts.Project project = new Services.Contracts.Project();
                project.Id = new Uri(string.Format(IdUrlTemplate, 1));
                project.Duration = 100;
                project.Resolution = "HD";
                project.AutoSaveInterval = 100;
                project.StartTimeCode = 3600;
                project.SmpteFrameRate = "Smpte2997NonDrop";
                project.RippleMode = true;
                project.Creator = "creator";
                project.Created = DateTime.Now;

                Services.Contracts.MediaBin mediaBin = new Services.Contracts.MediaBin();
                mediaBin.Id = new Uri(string.Format(IdUrlTemplate, 1));
                mediaBin.Items.Add(videoItem);

                Services.Contracts.Container container = new Services.Contracts.Container();
                container.Id = new Uri(string.Format(IdUrlTemplate, 2));
                container.Title = "Test";
                mediaBin.Containers.Add(container);

                project.MediaBin = mediaBin;

                Services.Contracts.Track track = new Services.Contracts.Track();
                track.Id = new Uri(string.Format(IdUrlTemplate, 1));
                track.TrackType = "Video";

                Services.Contracts.Comment shotComment = new Services.Contracts.Comment();
                shotComment.Id = new Uri(string.Format(IdUrlTemplate, 1));
                shotComment.Creator = "user";
                shotComment.Created = DateTime.Today;
                shotComment.Type = "Shot";
                shotComment.Text = "Text";
                shotComment.MarkIn = 170;
                shotComment.MarkOut = 180;

                Services.Contracts.Shot shot = new Services.Contracts.Shot();
                shot.Id = new Uri(string.Format(IdUrlTemplate, 1));
                shot.Source = videoItem;
                shot.SourceAnchor = new Anchor { MarkIn = 150, MarkOut = 200 };
                shot.TrackAnchor = new Anchor { MarkIn = 500, MarkOut = 0 };
                shot.Volume = 50;

                shot.Comments.Add(shotComment);

                track.Shots.Add(shot);

                Services.Contracts.Sequence sequence = new Services.Contracts.Sequence();
                sequence.Id = new Guid();

                project.Sequences.Add(sequence);

                project.Sequences[0].Tracks.Add(track);

                Services.Contracts.Comment comment = new Services.Contracts.Comment();
                comment.Id = new Uri(string.Format(IdUrlTemplate, 2));
                comment.Creator = "user";
                comment.Created = DateTime.Today;
                comment.Type = "Timeline";
                comment.Text = "Text";
                comment.MarkIn = 1200;
                comment.MarkOut = 1500;

                project.Comments.Add(comment);
                project.Comments.Add(shotComment);

                Services.Contracts.Title title = new Services.Contracts.Title();
                title.Id = new Uri(string.Format(IdUrlTemplate, 1));
                title.TrackAnchor = new Anchor { MarkIn = 600, MarkOut = 100 };
                title.TextBlockCollection.Add(new TextBlock { Text = "Main" });
                title.TextBlockCollection.Add(new TextBlock { Text = "Sub" });
                title.TitleTemplate = new Services.Contracts.TitleTemplate { Id = new Uri(string.Format(IdUrlTemplate, titleTemplate.Id)), TemplateName = titleTemplate.TemplateName };

                project.Titles.Add(title);

                var result = dataProvider.SaveProject(project);

                Assert.IsTrue(result);

                Project retrievedProject = context.Project.Include("Tracks")
                                                   .Include("Tracks.Shots")
                                                   .Include("Tracks.Shots.Comments")
                                                   .Include("Tracks.Shots.Item")
                                                   .Include("Tracks.Shots.Item.Resources")
                                                   .Include("Tracks.Shots.Item.Resources.VideoFormat")
                                                   .Include("Tracks.Shots.Item.Resources.AudioFormat")
                                                   .Include("Tracks.Shots.Item.Resources.ImageFormat")
                                                   .Include("Comments")
                                                   .Include("MediaBin")
                                                   .Include("MediaBin.Containers")
                                                   .Include("Titles")
                                                   .Include("Titles.TitleTemplate")
                                                   .Where(x => x.Id == 1).FirstOrDefault();

                Assert.IsNotNull(retrievedProject);
                Assert.AreEqual(project.Duration, retrievedProject.Duration);
                Assert.AreEqual(project.Resolution, retrievedProject.Resolution);
                Assert.AreEqual(project.Resolution, retrievedProject.Resolution);
                Assert.AreEqual(project.AutoSaveInterval, retrievedProject.AutoSaveInterval);
                Assert.AreEqual(project.StartTimeCode, retrievedProject.StartTimeCode);
                Assert.AreEqual(project.SmpteFrameRate, retrievedProject.SmpteFrameRate);
                Assert.AreEqual(project.RippleMode, retrievedProject.RippleMode);
                Assert.AreEqual(project.Creator, retrievedProject.Creator);
                Assert.IsNotNull(retrievedProject.MediaBin);
                Assert.AreEqual(mediaBin.Id.ToString(), string.Format(IdUrlTemplate, retrievedProject.MediaBin.Id));
                Assert.AreEqual(0, retrievedProject.MediaBin.Items.Count);
                Assert.AreEqual(1, retrievedProject.MediaBin.Containers.Count);
                Assert.AreEqual(1, retrievedProject.Tracks.Count);
                Assert.AreEqual(track.Id.ToString(), string.Format(IdUrlTemplate, retrievedProject.Tracks.First().Id));
                Assert.AreEqual(track.TrackType, retrievedProject.Tracks.First().TrackType);
                Assert.AreEqual(track.Shots.Count, retrievedProject.Tracks.First().Shots.Count);
                Assert.AreEqual(shot.Id.ToString(), string.Format(IdUrlTemplate, retrievedProject.Tracks.First().Shots.First().Id));
                Assert.AreEqual(shot.TrackAnchor.MarkIn, retrievedProject.Tracks.First().Shots.First().TrackMarkIn);
                Assert.AreEqual(shot.TrackAnchor.MarkOut, retrievedProject.Tracks.First().Shots.First().TrackMarkOut);
                Assert.AreEqual(shot.SourceAnchor.MarkIn, retrievedProject.Tracks.First().Shots.First().ItemMarkIn);
                Assert.AreEqual(shot.Volume, retrievedProject.Tracks.First().Shots.First().Volume);
                Assert.AreEqual(1, retrievedProject.Tracks.First().Shots.First().Comments.Count);
                Assert.AreEqual(shotComment.Id.ToString(), string.Format(IdUrlTemplate, retrievedProject.Tracks.First().Shots.First().Comments.First().Id));
                Assert.AreEqual(shotComment.Text, retrievedProject.Tracks.First().Shots.First().Comments.First().Text);
                Assert.AreEqual(shotComment.Creator, retrievedProject.Tracks.First().Shots.First().Comments.First().Creator);
                Assert.AreEqual(shotComment.Created, retrievedProject.Tracks.First().Shots.First().Comments.First().Created);
                Assert.AreEqual(shotComment.Type, retrievedProject.Tracks.First().Shots.First().Comments.First().CommentType);
                Assert.AreEqual(shotComment.MarkIn, retrievedProject.Tracks.First().Shots.First().Comments.First().MarkIn);
                Assert.AreEqual(shotComment.MarkOut, retrievedProject.Tracks.First().Shots.First().Comments.First().MarkOut);
                Assert.AreEqual(videoItem.Id.ToString(), string.Format(IdUrlTemplate, retrievedProject.Tracks.First().Shots.First().Item.Id));
                Assert.AreEqual(videoItem.Title, retrievedProject.Tracks.First().Shots.First().Item.Title);
                Assert.AreEqual(videoItem.Description, retrievedProject.Tracks.First().Shots.First().Item.Description);
                Assert.AreEqual("video", retrievedProject.Tracks.First().Shots.First().Item.ItemType.ToLowerInvariant());
                Assert.AreEqual(videoItem.Resources[0].Id.ToString(), string.Format(IdUrlTemplate, retrievedProject.Tracks.First().Shots.First().Item.Resources.First().Id));
                Assert.AreEqual(videoItem.Resources[0].Ref, retrievedProject.Tracks.First().Shots.First().Item.Resources.First().Ref);
                Assert.AreEqual(item, retrievedProject.Tracks.First().Shots.First().Item);
                Assert.AreEqual(2, retrievedProject.Comments.Count);
                Assert.AreEqual(comment.Id.ToString(), string.Format(IdUrlTemplate, retrievedProject.Comments.ElementAt(1).Id));
                Assert.AreEqual(comment.Text, retrievedProject.Comments.ElementAt(1).Text);
                Assert.AreEqual(comment.Creator, retrievedProject.Comments.ElementAt(1).Creator);
                Assert.AreEqual(comment.Created, retrievedProject.Comments.ElementAt(1).Created);
                Assert.AreEqual(comment.Type, retrievedProject.Comments.ElementAt(1).CommentType);
                Assert.AreEqual(comment.MarkIn, retrievedProject.Comments.ElementAt(1).MarkIn);
                Assert.AreEqual(comment.MarkOut, retrievedProject.Comments.ElementAt(1).MarkOut);
                Assert.AreEqual(shotComment.Id.ToString(), string.Format(IdUrlTemplate, retrievedProject.Comments.ElementAt(0).Id));
                Assert.AreEqual(shotComment.Text, retrievedProject.Comments.ElementAt(0).Text);
                Assert.AreEqual(shotComment.Creator, retrievedProject.Comments.ElementAt(0).Creator);
                Assert.AreEqual(shotComment.Created, retrievedProject.Comments.ElementAt(0).Created);
                Assert.AreEqual(shotComment.Type, retrievedProject.Comments.ElementAt(0).CommentType);
                Assert.AreEqual(shotComment.MarkIn, retrievedProject.Comments.ElementAt(0).MarkIn);
                Assert.AreEqual(shotComment.MarkOut, retrievedProject.Comments.ElementAt(0).MarkOut);
                Assert.AreEqual(1, retrievedProject.Titles.Count);
                Assert.AreEqual(title.Id.ToString(), string.Format(IdUrlTemplate, retrievedProject.Titles.ElementAt(0).Id));
                Assert.AreEqual(title.TextBlockCollection[0].Text, retrievedProject.Titles.ElementAt(0).MainText);
                Assert.AreEqual(title.TextBlockCollection[1].Text, retrievedProject.Titles.ElementAt(0).SubText);
                Assert.AreEqual(title.TrackAnchor.MarkIn, retrievedProject.Titles.ElementAt(0).TrackMarkIn);
                Assert.AreEqual(title.TrackAnchor.MarkOut, retrievedProject.Titles.ElementAt(0).TrackMarkOut);
                Assert.AreEqual(title.TitleTemplate.Id.ToString(), string.Format(IdUrlTemplate, retrievedProject.Titles.ElementAt(0).TitleTemplate.Id));
                Assert.AreEqual(title.TitleTemplate.TemplateName, retrievedProject.Titles.ElementAt(0).TitleTemplate.TemplateName);
            }
        }

        /// <summary>
        /// Tests that the exceptions are being logged.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@".\Data", @".\Data")]
        public void ShouldLogException()
        {
            var libraryId = CreateId(typeof(Services.Contracts.Container).Name);
            var dataProvider = new DataProvider(this.loggerService, null, libraryId);

            Assert.IsFalse(this.loggerService.LogEntriesCalled);

            dataProvider.SaveProject(null);

            Assert.IsTrue(this.loggerService.LogEntriesCalled);
        }

        /// <summary>
        /// Tests that the library is being loaded limiting items by the MaxNumberOfItems parameter given.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@".\Data", @".\Data")]
        public void ShouldLimitResultsWhenLoadingLibraryWithMaxNumberOfItems()
        {
            var context = new RoughCutEditorEntities();

            using (TransactionScope transactionScope = new TransactionScope())
            {
                var libraryId = CreateId(typeof(Services.Contracts.Container).Name);
                var dataProvider = new DataProvider(this.loggerService, null, libraryId);

                Container container = Container.CreateContainer(1);
                container.Title = "Test";

                Item item1 = SqlHelper.CreateSqlVideoItem();
                item1.Id = 1;
                item1.Resources.First().Id = 1;
                item1.Resources.First().VideoFormat.Id = 1;
                Item item2 = SqlHelper.CreateSqlAudioItem();
                item2.Id = 2;
                item2.Resources.First().Id = 2;
                item2.Resources.First().AudioFormat.Id = 2;
                Item item3 = SqlHelper.CreateSqlImageItem();
                item3.Id = 3;
                item3.Resources.First().Id = 3;
                item3.Resources.First().ImageFormat.Id = 3;

                container.Items.Add(item1);
                container.Items.Add(item2);
                container.Items.Add(item3);

                context.AddToContainer(container);

                context.SaveChanges();

                var maxNumberOfItems = 2;

                var library = dataProvider.LoadLibrary(maxNumberOfItems);

                Assert.AreEqual(maxNumberOfItems, library.Items.Count);
            }
        }

        /// <summary>
        /// Tests that the library is being loaded filter the results with the given parameter.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@".\Data", @".\Data")]
        public void ShouldFilterResultsWhenLoadingLibraryWithFilter()
        {
            var context = new RoughCutEditorEntities();

            using (TransactionScope transactionScope = new TransactionScope())
            {
                var libraryId = CreateId(typeof(Services.Contracts.Container).Name);
                var dataProvider = new DataProvider(this.loggerService, null, libraryId);

                Container container = Container.CreateContainer(1);
                container.Title = "Test";

                Item item1 = SqlHelper.CreateSqlVideoItem();
                item1.Title = "FilterItem";
                item1.Id = 1;
                item1.Resources.First().Id = 1;
                item1.Resources.First().VideoFormat.Id = 1;
                Item item2 = SqlHelper.CreateSqlAudioItem();
                item2.Title = "Test";
                item2.Id = 2;
                item2.Resources.First().Id = 2;
                item2.Resources.First().AudioFormat.Id = 2;
                Item item3 = SqlHelper.CreateSqlImageItem();
                item3.Title = "Test2";
                item3.Id = 3;
                item3.Resources.First().Id = 3;
                item3.Resources.First().ImageFormat.Id = 3;

                container.Items.Add(item1);
                container.Items.Add(item2);
                container.Items.Add(item3);

                context.AddToContainer(container);

                context.SaveChanges();

                var filter = "Filter";

                var library = dataProvider.LoadLibrary(filter, 0);

                Assert.AreEqual(1, library.Items.Count);
            }
        }

        /// <summary>
        /// Tests that a project is deleted.
        /// </summary>
        [TestMethod]
        [DeploymentItem(@".\Data", @".\Data")]
        public void ShouldDeleteProject()
        {
            var context = new RoughCutEditorEntities();

            using (TransactionScope transactionScope = new TransactionScope())
            {
                var libraryId = CreateId(typeof(Services.Contracts.Container).Name);
                var dataProvider = new DataProvider(this.loggerService, null, libraryId);

                Item item = Item.CreateItem(1, "title", "Video");
                item.Description = "description";
                item.Resources = new EntityCollection<Resource>();
                Resource resource = Resource.CreateResource(1, "\test\test.wmv", "Master");
                resource.VideoFormat = VideoFormat.CreateVideoFormat(1);
                resource.VideoFormat.Duration = 10;
                resource.VideoFormat.FrameRate = "Smpte25";
                resource.VideoFormat.ResolutionX = 200;
                resource.VideoFormat.ResolutionY = 500;
                item.Resources.Add(resource);

                context.AddToItem(item);

                TitleTemplate titleTemplate = TitleTemplate.CreateTitleTemplate(1, "TemplateName");

                context.AddToTitleTemplate(titleTemplate);

                context.SaveChanges();

                Project project = Project.CreateProject(1, "creator", DateTime.Now);
                project.Duration = 100;
                project.Resolution = "HD";
                project.AutoSaveInterval = 100;
                project.StartTimeCode = 3600;
                project.SmpteFrameRate = "Smpte2997NonDrop";
                project.RippleMode = true;

                Container mediaBin = Container.CreateContainer(1);
                mediaBin.Items.Add(item);

                Container container = Container.CreateContainer(2);
                mediaBin.Containers.Add(container);

                project.MediaBin = mediaBin;

                Comment comment = Comment.CreateComment(1, "Text", "Timeline", "user", DateTime.Today);
                comment.MarkIn = 1200;
                comment.MarkOut = 1500;

                Comment shotComment = Comment.CreateComment(2, "Text", "Shot", "user", DateTime.Today);
                shotComment.MarkIn = 170;
                shotComment.MarkOut = 180;

                project.Comments.Add(comment);
                project.Comments.Add(shotComment);

                Track track = Track.CreateTrack(1, "Video");

                Shot shot = Shot.CreateShot(1);
                shot.Item = item;
                shot.ItemMarkIn = 150;
                shot.ItemMarkOut = 200;
                shot.TrackMarkIn = 500;
                shot.TrackMarkOut = 0;
                shot.Volume = 5;
                shot.Comments.Add(shotComment);

                track.Shots = new EntityCollection<Shot>();
                track.Shots.Add(shot);

                project.Tracks.Add(track);

                Title title = Title.CreateTitle(1, 600, 100, "Main", "Sub");
                title.TitleTemplate = titleTemplate;

                project.Titles.Add(title);

                context.AddToProject(project);

                context.SaveChanges();

                var retrievedProject = dataProvider.LoadProject(new Uri(string.Format("http://temp/{0}", project.Id)));

                bool result = dataProvider.DeleteProject(new Uri(string.Format("http://temp/{0}", project.Id)));
                var retrievedDeletedProject = dataProvider.LoadProject(new Uri(string.Format("http://temp/{0}", project.Id)));

                Assert.IsTrue(result);
                Assert.IsNull(retrievedDeletedProject.Id);
            }
        }

        /// <summary>
        /// Executes the given script against a database.
        /// </summary>
        /// <param name="script">The script being executed.</param>
        private static void ExecuteDbScript(string script)
        {
            try
            {
                ConnectionStringSettings sqlConfiguration = ConfigurationManager.ConnectionStrings["TestSqlServer"];
                using (SqlConnection connection = new SqlConnection(sqlConfiguration.ConnectionString))
                {
                    Server server = new Server(new ServerConnection(connection));
                    server.ConnectionContext.ExecuteNonQuery(script);
                }
            }
            catch
            {
                throw;
            }
        }

        private static Uri CreateId(string name)
        {
            return new Uri(string.Format("http://test/{0}/{1}", name, 1));
        }
    }
}
