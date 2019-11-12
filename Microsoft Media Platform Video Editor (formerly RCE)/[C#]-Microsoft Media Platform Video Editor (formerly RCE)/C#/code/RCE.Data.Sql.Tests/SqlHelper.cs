// <copyright file="SqlHelper.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SqlHelper.cs                     
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
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RCE.Services.Contracts;
    using SMPTETimecode;
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
    /// A class with helper methods used in different tests.
    /// </summary>
    public sealed class SqlHelper
    {
        private const string IdUrlTemplate = "http://rce.litwareinc.com/id/{0}";

        /// <summary>
        /// Prevents a default instance of the <see cref="SqlHelper"/> class from being created.
        /// </summary>
        private SqlHelper()
        {
        }

        /// <summary>
        /// Asserts that the <paramref name="sqlShot"/> contains equivalent values from the <paramref name="shot"/>. 
        /// </summary>
        /// <param name="shot">The shot with expected values.</param>
        /// <param name="sqlShot">The shot with actual values.</param>
        public static void AssertShot(Shot shot, SqlShot sqlShot)
        {
            Assert.AreEqual(shot.Volume, sqlShot.Volume);

            Assert.AreEqual(shot.TrackAnchor.MarkIn, sqlShot.TrackMarkIn);
            Assert.AreEqual(shot.TrackAnchor.MarkOut, sqlShot.TrackMarkOut);
            Assert.AreEqual(shot.SourceAnchor.MarkIn, sqlShot.ItemMarkIn);
            Assert.AreEqual(shot.SourceAnchor.MarkOut, sqlShot.ItemMarkOut);

            AssertItem(shot.Source, sqlShot.Item);
        }

        /// <summary>
        /// Asserts that the <paramref name="sqlComment"/> contains equivalent values from the <paramref name="comment"/>.
        /// </summary>
        /// <param name="comment">The comment with expected values.</param>
        /// <param name="sqlComment">The comment with actual values.</param>
        public static void AssertComment(Comment comment, SqlComment sqlComment)
        {
            Assert.AreEqual(comment.Creator, sqlComment.Creator);
            Assert.AreEqual(comment.Created, sqlComment.Created);
            Assert.AreEqual(comment.MarkIn, sqlComment.MarkIn);
            Assert.AreEqual(comment.MarkOut, sqlComment.MarkOut);
            Assert.AreEqual(comment.Text, sqlComment.Text);
            Assert.AreEqual(comment.Type, sqlComment.CommentType);

            InkComment inkComment = comment as InkComment;

            if (inkComment != null)
            {
                Assert.AreEqual(inkComment.Strokes, sqlComment.Strokes);
            }
        }

        /// <summary>
        /// Asserts that the <paramref name="sqlTrack"/> contains equivalent values from the <paramref name="track"/>.
        /// </summary>
        /// <param name="track">The track with expected values.</param>
        /// <param name="sqlTrack">The track with actual values.</param>
        public static void AssertTrack(Track track, SqlTrack sqlTrack)
        {
            Assert.AreEqual(track.TrackType, sqlTrack.TrackType);
        }

        /// <summary>
        /// Asserts that the <paramref name="sqlTitle"/> contains equivalent values from the <paramref name="title"/>.
        /// </summary>
        /// <param name="title">The title with expected values.</param>
        /// <param name="sqlTitle">The title with actual values.</param>
        public static void AssertTitle(Title title, SqlTitle sqlTitle)
        {
            Assert.AreEqual(title.TrackAnchor.MarkIn, sqlTitle.TrackMarkIn);
            Assert.AreEqual(title.TrackAnchor.MarkOut, sqlTitle.TrackMarkOut);
            Assert.AreEqual(title.TextBlockCollection[0].Text, sqlTitle.MainText);
            Assert.AreEqual(title.TextBlockCollection[0].Text, sqlTitle.SubText);
        }

        /// <summary>
        /// Asserts that the <paramref name="sqlProject"/> contains equivalent values from the <paramref name="project"/>.
        /// </summary>
        /// <param name="project">The project with expected values.</param>
        /// <param name="sqlProject">The project with actual values.</param>
        public static void AssertProject(Project project, SqlProject sqlProject)
        {
            Assert.AreEqual(project.Title, sqlProject.Name);
            Assert.AreEqual(project.Created, sqlProject.Created);
            Assert.AreEqual(project.Creator, sqlProject.Creator);
            Assert.AreEqual(project.Duration, sqlProject.Duration);
            Assert.AreEqual(project.AutoSaveInterval, sqlProject.AutoSaveInterval);
            Assert.AreEqual(project.StartTimeCode, sqlProject.StartTimeCode);
            Assert.AreEqual(project.SmpteFrameRate, sqlProject.SmpteFrameRate);
            Assert.AreEqual(project.RippleMode, sqlProject.RippleMode);
            Assert.AreEqual(project.Resolution, sqlProject.Resolution);
        }

        /// <summary>
        /// Asserts that the <paramref name="sqlTitleTemplate"/> contains equivalent values from the <paramref name="titleTemplate"/>.
        /// </summary>
        /// <param name="titleTemplate">The title template with expected values.</param>
        /// <param name="sqlTitleTemplate">The title template with actual values.</param>
        public static void AssertTitleTemplate(TitleTemplate titleTemplate, SqlTitleTemplate sqlTitleTemplate)
        {
            Assert.AreEqual(titleTemplate.Id.ToString(), string.Format(IdUrlTemplate, sqlTitleTemplate.Id));
            Assert.AreEqual(titleTemplate.TemplateName, sqlTitleTemplate.TemplateName);
        }

        /// <summary>
        /// Asserts that the <paramref name="sqlContainer"/> contains equivalent values from the <paramref name="container"/>.
        /// </summary>
        /// <param name="container">The container with expected values.</param>
        /// <param name="sqlContainer">The container with actual values.</param>
        public static void AssertContainer(Container container, SqlContainer sqlContainer)
        {
            Assert.AreEqual(container.Title, sqlContainer.Title);
            Assert.AreEqual(container.Items.Count, sqlContainer.Items.Count);
        }

        /// <summary>
        /// Asserts that the <paramref name="track"/> contains equivalent values from the <paramref name="sqlTrack"/>.
        /// </summary>
        /// <param name="sqlTrack">The track with expected values.</param>
        /// <param name="track">The track with actual values.</param>
        public static void AssertTrack(SqlTrack sqlTrack, Track track)
        {
            Assert.AreEqual(string.Format(IdUrlTemplate, sqlTrack.Id), track.Id.ToString());
            Assert.AreEqual(sqlTrack.TrackType, track.TrackType);

            AssertShot(sqlTrack.Shots.First(), track.Shots[0]);
        }

        /// <summary>
        /// Asserts that the <paramref name="shot"/> contains equivalent values from the <paramref name="sqlShot"/>.
        /// </summary>
        /// <param name="sqlShot">The shot with expected values.</param>
        /// <param name="shot">The shot with actual values.</param>
        public static void AssertShot(SqlShot sqlShot, Shot shot)
        {
            Assert.AreEqual(string.Format(IdUrlTemplate, sqlShot.Id), shot.Id.ToString());
            Assert.AreEqual(sqlShot.Volume, shot.Volume);

            Assert.AreEqual(sqlShot.TrackMarkIn, shot.TrackAnchor.MarkIn);
            Assert.AreEqual(sqlShot.TrackMarkOut, shot.TrackAnchor.MarkOut);
            Assert.AreEqual(sqlShot.ItemMarkIn, shot.SourceAnchor.MarkIn);
            Assert.AreEqual(sqlShot.ItemMarkOut, shot.SourceAnchor.MarkOut);

            AssertItem(sqlShot.Item, shot.Source);

            AssertComment(sqlShot.Comments.First(), shot.Comments[0]);
        }

        /// <summary>
        /// Asserts that the <paramref name="comment"/> contains equivalent values from the <paramref name="sqlComment"/>.
        /// </summary>
        /// <param name="sqlComment">The comment with expected values.</param>
        /// <param name="comment">The comment with actual values.</param>
        public static void AssertComment(SqlComment sqlComment, Comment comment)
        {
            Assert.AreEqual(string.Format(IdUrlTemplate, sqlComment.Id), comment.Id.ToString());
            Assert.AreEqual(sqlComment.Creator, comment.Creator);
            Assert.AreEqual(sqlComment.Created, comment.Created);
            Assert.AreEqual(sqlComment.MarkIn, comment.MarkIn);
            Assert.AreEqual(sqlComment.MarkOut, comment.MarkOut);
            Assert.AreEqual(sqlComment.Text, comment.Text);
            Assert.AreEqual(sqlComment.CommentType, comment.Type);
        }

        /// <summary>
        /// Asserts that the <paramref name="title"/> contains equivalent values from the <paramref name="sqlTitle"/>.
        /// </summary>
        /// <param name="sqlTitle">The title with expected values.</param>
        /// <param name="title">The title with actual values.</param>
        public static void AssertTitle(SqlTitle sqlTitle, Title title)
        {
            Assert.AreEqual(string.Format(IdUrlTemplate, sqlTitle.Id), title.Id.ToString());
            Assert.AreEqual(sqlTitle.TrackMarkIn, title.TrackAnchor.MarkIn);
            Assert.AreEqual(sqlTitle.TrackMarkOut, title.TrackAnchor.MarkOut);
            Assert.AreEqual(sqlTitle.MainText, title.TextBlockCollection[0].Text);
            Assert.AreEqual(sqlTitle.SubText, title.TextBlockCollection[1].Text);

            AssertTitleTemplate(title.TitleTemplate, sqlTitle.TitleTemplate);
        }

        /// <summary>
        /// Asserts that the <paramref name="project"/> contains equivalent values from the <paramref name="sqlProject"/>.
        /// </summary>
        /// <param name="sqlProject">The project with expected values.</param>
        /// <param name="project">The project with actual values.</param>
        public static void AssertProject(SqlProject sqlProject, Project project)
        {
            Assert.AreEqual(string.Format(IdUrlTemplate, sqlProject.Id), project.Id.ToString());
            Assert.AreEqual(sqlProject.Name, project.Title);
            Assert.AreEqual(sqlProject.Created, project.Created);
            Assert.AreEqual(sqlProject.Creator, project.Creator);

            Assert.AreEqual(sqlProject.Duration, project.Duration);
            Assert.AreEqual(sqlProject.AutoSaveInterval, project.AutoSaveInterval);
            Assert.AreEqual(sqlProject.StartTimeCode, project.StartTimeCode);
            Assert.AreEqual(sqlProject.SmpteFrameRate, project.SmpteFrameRate);
            Assert.AreEqual(sqlProject.RippleMode, project.RippleMode);

            Assert.AreEqual(string.Format(IdUrlTemplate, sqlProject.MediaBin.Id), project.MediaBin.Id.ToString());
            Assert.AreEqual(sqlProject.Resolution, project.Resolution);

            Assert.AreEqual(sqlProject.Tracks.Count, project.Sequences[0].Tracks.Count());
            Assert.AreEqual(sqlProject.Comments.Count, project.Comments.Count());
            Assert.AreEqual(sqlProject.Titles.Count, project.Titles.Count());
            AssertTrack(sqlProject.Tracks.First(), project.Sequences[0].Tracks[0]);
            AssertTrack(sqlProject.Tracks.ElementAt(1), project.Sequences[0].Tracks[1]);
            AssertComment(sqlProject.Comments.First(), project.Comments[0]);
            AssertTitle(sqlProject.Titles.First(), project.Titles[0]);
        }

        /// <summary>
        /// Asserts that the <paramref name="container"/> contains equivalent values from the <paramref name="sqlContainer"/>.
        /// </summary>
        /// <param name="sqlContainer">The container with expected values.</param>
        /// <param name="container">The container with actual values.</param>
        public static void AssertContainer(SqlContainer sqlContainer, Container container)
        {
            AssertContainer(sqlContainer, container, sqlContainer.Items.Count);
        }

        /// <summary>
        /// Asserts that the <paramref name="container"/> contains equivalent values from the <paramref name="sqlContainer"/>.
        /// </summary>
        /// <param name="sqlContainer">The container with expected values.</param>
        /// <param name="container">The container with actual values.</param>
        /// <param name="maxNumberOfItems">The max number of items to retrieve.</param>
        public static void AssertContainer(SqlContainer sqlContainer, Container container, int maxNumberOfItems)
        {
            Assert.AreEqual(sqlContainer.Title, container.Title);
            Assert.AreEqual(maxNumberOfItems, container.Items.Count);
            AssertItem(sqlContainer.Items.First(), container.Items[0]);
            Assert.AreEqual(sqlContainer.Containers.Count, container.Containers.Count);
            AssertChildContainer(sqlContainer.Containers.First(), container.Containers[0]);
        }

        /// <summary>
        /// Creates a track for testing.
        /// </summary>
        /// <returns>A track with values.</returns>
        public static Track CreateTrack()
        {
            var track = new Track();
            track.Id = CreateUri();
            track.Number = 0;
            track.TrackType = TrackType.Visual.ToString();
            track.Volume = 100;
            track.Creator = "Creator";
            track.Created = new DateTime(2009, 1, 1);
            track.Shots.Add(CreateShot());

            return track;
        }

        /// <summary>
        /// Creates a shot for testing.
        /// </summary>
        /// <returns>A shot with values.</returns>
        public static Shot CreateShot()
        {
            var shot = new Shot();
            shot.Id = CreateUri();
            shot.Title = "Title";
            shot.Description = "Description";
            shot.Creator = "Creator";
            shot.Created = new DateTime(2009, 1, 1);
            shot.Source = CreateMediaItem();
            shot.SourceAnchor = CreateAnchor();
            shot.TrackAnchor = CreateAnchor();
            shot.Volume = (decimal)0.7;
            shot.Comments.Add(CreateComment());

            return shot;
        }

        /// <summary>
        /// Creates a comment for testing.
        /// </summary>
        /// <returns>A comment with values.</returns>
        public static Comment CreateComment()
        {
            var comment = new Comment();
            comment.Id = CreateUri();
            comment.Type = "Global";
            comment.MarkIn = 5;
            comment.MarkOut = 7.6;
            comment.Text = "Text";
            comment.Creator = "Creator";
            comment.Created = new DateTime(2009, 1, 1);

            return comment;
        }

        /// <summary>
        /// Creates a ink comment for testing.
        /// </summary>
        /// <returns>A ink comment with values.</returns>
        public static InkComment CreateInkComment()
        {
            string strokes = @"<?xml version=""1.0"" encoding=""utf-16"" ?> <StrokeCollection xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""> <Stroke> <Stroke.DrawingAttributes> <DrawingAttributes Width=""200"" Height=""300"" Color=""#01020304"" OutlineColor=""#02020304""/> </Stroke.DrawingAttributes> <Stroke.StylusPoints> <StylusPointCollection> <StylusPoint X=""100"" Y=""40""/> </StylusPointCollection> </Stroke.StylusPoints> </Stroke> </StrokeCollection>";

            var comment = new InkComment();
            comment.Id = CreateUri();
            comment.Type = "Ink";
            comment.MarkIn = 5;
            comment.MarkOut = 7.6;
            comment.Text = "Text";
            comment.Creator = "Creator";
            comment.Created = new DateTime(2009, 1, 1);
            comment.Strokes = strokes;

            return comment;
        }

        /// <summary>
        /// Creates a title for testing.
        /// </summary>
        /// <returns>A title with values.</returns>
        public static Title CreateTitle()
        {
            var title = new Title();
            title.Id = CreateUri();
            title.Creator = "Creator";
            title.Created = new DateTime(2009, 1, 1);
            title.TrackAnchor = CreateAnchor();
            title.TitleTemplate = CreateTitleTemplate();
            title.TextBlockCollection.Add(CreateTextBlock());
            title.TextBlockCollection.Add(CreateTextBlock());

            return title;
        }

        /// <summary>
        /// Creates a project for testing.
        /// </summary>
        /// <returns>A project with values.</returns>
        public static Project CreateProject()
        {
            var project = new Project();
            project.Id = CreateUri();
            project.Title = "MyProject";
            project.Creator = "Creator";
            project.Created = new DateTime(2009, 1, 1);
            project.Description = "Description";
            project.AutoSaveInterval = 10;
            project.Duration = 60;
            project.RippleMode = false;
            project.SmpteFrameRate = SmpteFrameRate.Smpte2997NonDrop.ToString();
            project.StartTimeCode = 60;
            project.MediaBin = new MediaBin { Id = CreateUri() };
            project.Resolution = "1280x800";
            project.Title = "Title";
            project.Sequences.Add(new Sequence());
            project.Sequences[0].Tracks.Add(CreateTrack());
            project.Titles.Add(CreateTitle());
            project.Comments.Add(CreateComment());
            project.Comments.Add(CreateInkComment());

            return project;
        }

        /// <summary>
        /// Creates a media bin for testing.
        /// </summary>
        /// <returns>A media bin with values.</returns>
        public static MediaBin CreateMediaBin()
        {
            var mediaBin = new MediaBin();
            mediaBin.Id = CreateUri();
            mediaBin.Title = "Title";
            mediaBin.Creator = "Creator";
            mediaBin.Created = new DateTime(2009, 1, 1);
            mediaBin.Items.Add(CreateMediaItem());
            mediaBin.Containers.Add(CreateContainer());

            return mediaBin;
        }

        /// <summary>
        /// Creates a sql project for testing.
        /// </summary>
        /// <returns>A sql project with values.</returns>
        public static SqlProject CreateSqlProject()
        {
            var project = new SqlProject();
            project.Id = 1;
            project.Name = "MyProject";
            project.Creator = "Creator";
            project.Created = new DateTime(2009, 1, 1);
            project.AutoSaveInterval = 10;
            project.Duration = 60;
            project.RippleMode = false;
            project.SmpteFrameRate = SmpteFrameRate.Smpte2997NonDrop.ToString();
            project.StartTimeCode = 60;
            project.MediaBin = new SqlContainer { Id = 1 };
            project.Resolution = "1280x800";
            project.Tracks.Add(CreateSqlTrack(TrackType.Visual.ToString(), CreateSqlShot(CreateSqlVideoItem())));
            project.Tracks.Add(CreateSqlTrack(TrackType.Audio.ToString(), CreateSqlShot(CreateSqlAudioItem())));
            project.Titles.Add(CreateSqlTitle());
            project.Comments.Add(CreateSqlComment());
            project.Comments.Add(CreateSqlInkComment());

            return project;
        }

        /// <summary>
        /// Creates a sql title template for testing.
        /// </summary>
        /// <returns>A sql title template with values.</returns>
        public static SqlTitleTemplate CreateSqlTitleTemplate()
        {
            var titleTemplate = new SqlTitleTemplate();
            titleTemplate.Id = 1;
            titleTemplate.TemplateName = "Spinner";

            return titleTemplate;
        }

        /// <summary>
        /// Creates a sql item for testing.
        /// </summary>
        /// <returns>A sql item with values.</returns>
        public static SqlItem CreateSqlItem()
        {
            var item = new SqlItem();
            item.Id = 1;
            item.Title = "Title";
            item.Description = "Description";
            item.Resources.Add(CreateSqlResource());

            return item;
        }

        /// <summary>
        /// Creates a sql container for testing.
        /// </summary>
        /// <returns>A sql container with values.</returns>
        public static SqlContainer CreateSqlContainer()
        {
            var container = new SqlContainer();
            container.Id = 1;
            container.Title = "Title";
            container.Items.Add(CreateSqlImageItem());
            container.Items.Add(CreateSqlVideoItem());
            container.Items.Add(CreateSqlAudioItem());
            container.Containers.Add(CreateSqlChildContainer());

            return container;
        }

        /// <summary>
        /// Creates a sql video item for testing.
        /// </summary>
        /// <returns>A sql video item with values.</returns>
        public static SqlItem CreateSqlVideoItem()
        {
            var item = CreateSqlItem();
            item.ItemType = "Video";

            var resource = item.Resources.First();
            resource.Id = 1;

            resource.VideoFormat = new VideoFormat();
            resource.VideoFormat.Id = 1;
            resource.VideoFormat.ResolutionX = 100;
            resource.VideoFormat.ResolutionY = 200;
            resource.VideoFormat.Duration = 200;
            resource.VideoFormat.FrameRate = SmpteFrameRate.Smpte2997Drop.ToString();

            return item;
        }

        /// <summary>
        /// Creates a sql audio item for testing.
        /// </summary>
        /// <returns>A sql audio item with values.</returns>
        public static SqlItem CreateSqlAudioItem()
        {
            var item = CreateSqlItem();
            item.ItemType = "Audio";

            var resource = item.Resources.First();

            resource.AudioFormat = new AudioFormat();
            resource.AudioFormat.Id = 1;
            resource.AudioFormat.Duration = 200;

            return item;
        }

        /// <summary>
        /// Creates a sql image item for testing.
        /// </summary>
        /// <returns>A sql image item with values.</returns>
        public static SqlItem CreateSqlImageItem()
        {
            var item = CreateSqlItem();
            item.ItemType = "Image";

            var resource = item.Resources.First();

            resource.ImageFormat = new ImageFormat();
            resource.ImageFormat.Id = 1;
            resource.ImageFormat.ResolutionX = 100;
            resource.ImageFormat.ResolutionY = 200;

            return item;
        }

        /// <summary>
        /// Creates an anchor for testing.
        /// </summary>
        /// <returns>An anchor with values.</returns>
        private static Anchor CreateAnchor()
        {
            var anchor = new Anchor();
            anchor.Id = CreateUri();
            anchor.MarkIn = 5;
            anchor.MarkOut = 6.907;
            anchor.Creator = "Creator";
            anchor.Created = new DateTime(2009, 1, 1);

            return anchor;
        }

        /// <summary>
        /// Creates a media item form testing.
        /// </summary>
        /// <returns>A media item with values.</returns>
        private static MediaItem CreateMediaItem()
        {
            var mediaItem = new MediaItem();
            mediaItem.Id = CreateUri();
            mediaItem.Title = "Title";
            mediaItem.Description = "Description";
            mediaItem.Resources.Add(CreateResource());
            mediaItem.Creator = "Creator";
            mediaItem.Created = new DateTime(2009, 1, 1);

            return mediaItem;
        }

        /// <summary>
        /// Creates a resource for testing.
        /// </summary>
        /// <returns>A resource with values.</returns>
        private static Resource CreateResource()
        {
            var resource = new Resource();
            resource.Id = CreateUri();
            resource.Ref = "ref";
            resource.ResourceType = "Master";
            resource.MimeType = "mimeType";
            resource.Creator = "Creator";
            resource.Created = new DateTime(2009, 1, 1);

            return resource;
        }

        /// <summary>
        /// Asserts that the <paramref name="sqlItem"/> contains equivalent values from the <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The container with expected values.</param>
        /// <param name="sqlItem">The container with actual values.</param>
        private static void AssertItem(Item item, SqlItem sqlItem)
        {
            Assert.AreEqual(item.Id.ToString(), string.Format(IdUrlTemplate, sqlItem.Id));
            Assert.AreEqual(item.Title, sqlItem.Title);
            Assert.AreEqual(item.Description, sqlItem.Description);
            AssertResource(item.Resources[0], sqlItem.Resources.First());
        }

        /// <summary>
        /// Asserts that the <paramref name="sqlResource"/> contains equivalent values from the <paramref name="resource"/>.
        /// </summary>
        /// <param name="resource">The resource with expected values.</param>
        /// <param name="sqlResource">The resource with actual values.</param>
        private static void AssertResource(Resource resource, SqlResource sqlResource)
        {
            Assert.AreEqual(resource.Id.ToString(), string.Format(IdUrlTemplate, sqlResource.Id));
            Assert.AreEqual(resource.Ref, sqlResource.Ref);
            Assert.AreEqual(resource.ResourceType, sqlResource.ResourceType);
        }

        /// <summary>
        /// Asserts that the <paramref name="container"/> contains equivalent values from the <paramref name="sqlContainer"/>.
        /// </summary>
        /// <param name="sqlContainer">The container with expected values.</param>
        /// <param name="container">The container with actual values.</param>
        private static void AssertChildContainer(SqlContainer sqlContainer, Container container)
        {
            Assert.AreEqual(sqlContainer.Title, container.Title);
            Assert.AreEqual(sqlContainer.Items.Count, container.Items.Count);
            AssertItem(sqlContainer.Items.First(), container.Items[0]);
            Assert.AreEqual(sqlContainer.Containers.Count, container.Containers.Count);
        }

        /// <summary>
        /// Creates a container for testing.
        /// </summary>
        /// <returns>A container with values.</returns>
        private static Container CreateContainer()
        {
            var container = new Container();
            container.Id = CreateUri();
            container.Title = "Title";
            container.Creator = "Creator";
            container.Created = new DateTime(2009, 1, 1);
            container.Items.Add(CreateMediaItem());

            return container;
        }

        /// <summary>
        /// Creates a title template for testing.
        /// </summary>
        /// <returns>A title template with values.</returns>
        private static TitleTemplate CreateTitleTemplate()
        {
            var titleTemplate = new TitleTemplate();
            titleTemplate.Id = CreateUri();
            titleTemplate.TemplateName = "Spinner";
            titleTemplate.Creator = "Creator";
            titleTemplate.Created = new DateTime(2009, 1, 1);

            return titleTemplate;
        }

        /// <summary>
        /// Creates a text block for testing.
        /// </summary>
        /// <returns>A textblock with values.</returns>
        private static TextBlock CreateTextBlock()
        {
            var textBlock = new TextBlock();
            textBlock.Id = CreateUri();
            textBlock.Text = "text";
            textBlock.Creator = "Creator";
            textBlock.Created = new DateTime(2009, 1, 1);

            return textBlock;
        }

        /// <summary>
        /// Asserts that the <paramref name="item"/> contains equivalent values from the <paramref name="sqlItem"/>.
        /// </summary>
        /// <param name="sqlItem">The item with expected values.</param>
        /// <param name="item">The item with actual values.</param>
        private static void AssertItem(SqlItem sqlItem, Item item)
        {
            Assert.AreEqual(string.Format(IdUrlTemplate, sqlItem.Id), item.Id.ToString());
            Assert.AreEqual(sqlItem.Title, item.Title);
            Assert.AreEqual(sqlItem.Description, item.Description);

            AssertItem(sqlItem, item as VideoItem);
            AssertItem(sqlItem, item as AudioItem);
            AssertItem(sqlItem, item as ImageItem);
        }

        /// <summary>
        /// Asserts that the <paramref name="item"/> contains equivalent values from the <paramref name="sqlItem"/>.
        /// </summary>
        /// <param name="sqlItem">The item with expected values.</param>
        /// <param name="item">The item with actual values.</param>
        private static void AssertItem(SqlItem sqlItem, VideoItem item)
        {
            if (item != null)
            {
                var resource = sqlItem.Resources.First();

                Assert.AreEqual(resource.VideoFormat.ResolutionX, item.Width);
                Assert.AreEqual(resource.VideoFormat.ResolutionY, item.Height);
                Assert.AreEqual(resource.VideoFormat.Duration, item.Duration);
                Assert.AreEqual(resource.VideoFormat.FrameRate, item.FrameRate.ToString());
            }
        }

        /// <summary>
        /// Asserts that the <paramref name="item"/> contains equivalent values from the <paramref name="sqlItem"/>.
        /// </summary>
        /// <param name="sqlItem">The item with expected values.</param>
        /// <param name="item">The item with actual values.</param>
        private static void AssertItem(SqlItem sqlItem, ImageItem item)
        {
            if (item != null)
            {
                var resource = sqlItem.Resources.First();

                Assert.AreEqual(resource.ImageFormat.ResolutionX, item.Width);
                Assert.AreEqual(resource.ImageFormat.ResolutionY, item.Height);
            }
        }

        /// <summary>
        /// Asserts that the <paramref name="item"/> contains equivalent values from the <paramref name="sqlItem"/>.
        /// </summary>
        /// <param name="sqlItem">The item with expected values.</param>
        /// <param name="item">The item with actual values.</param>
        private static void AssertItem(SqlItem sqlItem, AudioItem item)
        {
            if (item != null)
            {
                var resource = sqlItem.Resources.First();

                Assert.AreEqual(resource.AudioFormat.Duration, item.Duration);
            }
        }

        /// <summary>
        /// Creates a sql resource for testing.
        /// </summary>
        /// <returns>A sql resource with values.</returns>
        private static SqlResource CreateSqlResource()
        {
            var resource = new SqlResource();
            resource.Id = 1;
            resource.Ref = "ref";
            resource.ResourceType = "Master";

            return resource;
        }

        /// <summary>
        /// Creates a sql track for testing.
        /// </summary>
        /// <param name="trackType">The track type of the track being created.</param>
        /// <param name="shot">The shot being added to the track being created.</param>
        /// <returns>A sql track with values.</returns>
        private static SqlTrack CreateSqlTrack(string trackType, SqlShot shot)
        {
            var track = new SqlTrack();
            track.Id = 1;
            track.TrackType = trackType;
            track.Shots.Add(shot);

            return track;
        }

        /// <summary>
        /// Creates a sql shot for testing.
        /// </summary>
        /// <param name="item">The sql item associated to the shot being created.</param>
        /// <returns>A sql shot with values.</returns>
        private static SqlShot CreateSqlShot(SqlItem item)
        {
            var shot = new SqlShot();
            shot.Id = 1;
            shot.Item = item;
            shot.ItemMarkIn = 10;
            shot.ItemMarkOut = 20.5;
            shot.TrackMarkIn = 60;
            shot.TrackMarkOut = 200;
            shot.Volume = (decimal)0.7;
            shot.Comments.Add(CreateSqlComment());

            return shot;
        }

        /// <summary>
        /// Creates a sql comment for testing.
        /// </summary>
        /// <returns>A sql comment with values.</returns>
        private static SqlComment CreateSqlComment()
        {
            var comment = new SqlComment();
            comment.Id = 1;
            comment.CommentType = "Global";
            comment.MarkIn = 5;
            comment.MarkOut = 7.6;
            comment.Text = "Text";
            comment.Creator = "Creator";
            comment.Created = new DateTime(2009, 1, 1);

            return comment;
        }

        /// <summary>
        /// Creates a sql ink comment for testing.
        /// </summary>
        /// <returns>A sql comment with values.</returns>
        private static SqlComment CreateSqlInkComment()
        {
            string strokes = @"<?xml version=""1.0"" encoding=""utf-16"" ?> <StrokeCollection xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""> <Stroke> <Stroke.DrawingAttributes> <DrawingAttributes Width=""200"" Height=""300"" Color=""#01020304"" OutlineColor=""#02020304""/> </Stroke.DrawingAttributes> <Stroke.StylusPoints> <StylusPointCollection> <StylusPoint X=""100"" Y=""40""/> </StylusPointCollection> </Stroke.StylusPoints> </Stroke> </StrokeCollection>";

            var comment = CreateSqlComment();

            comment.Strokes = strokes;

            return comment;
        }

        /// <summary>
        /// Creates a sql title for testing.
        /// </summary>
        /// <returns>A sql title with values.</returns>
        private static SqlTitle CreateSqlTitle()
        {
            var title = new SqlTitle();
            title.Id = 1;
            title.TitleTemplate = CreateSqlTitleTemplate();
            title.MainText = "MainText";
            title.SubText = "SubText";

            return title;
        }

        /// <summary>
        /// Creates a sql container for testing.
        /// </summary>
        /// <returns>A sql container with values.</returns>
        private static SqlContainer CreateSqlChildContainer()
        {
            var container = new SqlContainer();
            container.Id = 1;
            container.Title = "Title";
            container.Items.Add(CreateSqlVideoItem());

            return container;
        }

        /// <summary>
        /// Creates a random uri for testing.
        /// </summary>
        /// <returns>The random uri.</returns>
        private static Uri CreateUri()
        {
            var uriString = string.Format(IdUrlTemplate, 1);

            return new Uri(uriString);
        }
    }
}
