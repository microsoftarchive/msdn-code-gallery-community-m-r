// <copyright file="SqlDataProviderTranslator.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SqlDataProviderTranslator.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Data.Sql.Translators
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using RCE.Services.Contracts;
    using SmoothStreamingManifestGenerator.Models;
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
    /// A class that translates sql entities to service contracts.
    /// </summary>
    public sealed class SqlDataProviderTranslator
    {
        // <summary>
        /// Constant used for Image Uri.
        /// </summary>
        private const string ImageItems = "ImageItems";

        /// <summary>
        /// Constant used for Video Uri.
        /// </summary>
        private const string VideoItems = "VideoItems";

        /// <summary>
        /// Constant used for Audio Uri.
        /// </summary>
        private const string AudioItems = "AudioItems";

        /// <summary>
        /// Constant used for Resource Uri.
        /// </summary>
        private const string Resources = "Resources";

        /// <summary>
        /// Constant used for Project Uri.
        /// </summary>
        private const string Projects = "Projects";

        /// <summary>
        /// Constant used for Annotations Uri.
        /// </summary>
        private const string Annotations = "Annotations";

        /// <summary>
        /// Constant used for MediaBin Uri.
        /// </summary>
        private const string MediaBins = "MediaBin";

        /// <summary>
        /// Constant used for Track Uri(Visual/Audio).
        /// </summary>
        private const string Tracks = "Tracks";

        /// <summary>
        /// Constant used for Shots.
        /// </summary>
        private const string Shots = "Shots";

        /// <summary>
        /// Constant used for the folder Container Uri.
        /// </summary>
        private const string Containers = "Containers";

        /// <summary>
        /// Constant used for the Title template Uri.
        /// </summary>
        private const string TitleTemplates = "TitleTemplates";

        /// <summary>
        /// Constant used for the Title Uri.
        /// </summary>
        private const string Titles = "Titles";

        /// <summary>
        /// The Uri template to build the Item Id.
        /// </summary>
        private const string ItemIdUriTemplate = "http://rce.litwareinc.com/id/{0}";

        /// <summary>
        /// Prevents a default instance of the SqlDataProviderTranslator class from being created.
        /// </summary>
        private SqlDataProviderTranslator()
        {
        }

        /// <summary>
        /// Converts a <see cref="SqlContainer"/> into a <seealso cref="Container"/>.
        /// </summary>
        /// <param name="sqlContainer">The <see cref="SqlContainer"/> being converted.</param>
        /// <returns>A new <see cref="Container"/> that contains equivalent values from the <paramref name="sqlContainer"/>.</returns>
        public static Container ConvertToContainer(SqlContainer sqlContainer, IMetadataLocator metadataLocator)
        {
            return ConvertToContainer(sqlContainer, null, 0, metadataLocator);
        }

        /// <summary>
        /// Converts a <see cref="SqlContainer"/> into a <seealso cref="Container"/>.
        /// </summary>
        /// <param name="sqlContainer">The <see cref="SqlContainer"/> being converted.</param>
        /// <param name="filter">The filter to apply to the items of the <paramref name="sqlContainer"/>.</param>
        /// <param name="maxNumberOfItems">The max number of items that the new container should contains.</param>
        /// <returns>A new <see cref="Container"/> that contains equivalent values from the <paramref name="sqlContainer"/>.</returns>
        public static Container ConvertToContainer(SqlContainer sqlContainer, string filter, int maxNumberOfItems, IMetadataLocator metadataLocator)
        {
            Container container = new Container
            {
                Id = CreateUri(sqlContainer.Id),
                Title = sqlContainer.Title
            };

            if (maxNumberOfItems > 0)
            {
                foreach (SqlItem item in sqlContainer.Items)
                {
                    if (container.Items.Count < maxNumberOfItems)
                    {
                        MediaItem mediaItem = ConvertToMediaItem(item, metadataLocator);

                        if (string.IsNullOrEmpty(filter))
                        {
                            container.Items.Add(mediaItem);
                        }
                        else
                        {
                            if (mediaItem.Title.Contains(filter))
                            {
                                container.Items.Add(mediaItem);
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (SqlItem item in sqlContainer.Items)
                {
                    MediaItem mediaItem = ConvertToMediaItem(item, metadataLocator);

                    if (string.IsNullOrEmpty(filter))
                    {
                        container.Items.Add(mediaItem);
                    }
                    else
                    {
                        if (mediaItem.Title.Contains(filter))
                        {
                            container.Items.Add(mediaItem);
                        }
                    }
                }
            }

            foreach (SqlContainer sqlChildContainer in sqlContainer.Containers)
            {
                Container childContainer = ConvertToContainer(sqlChildContainer, filter, maxNumberOfItems, metadataLocator);

                if (string.IsNullOrEmpty(filter))
                {
                    container.Containers.Add(childContainer);
                }
                else
                {
                    foreach (Item item in childContainer.Items)
                    {
                        container.Items.Add(item);
                    }
                }
            }

            return container;
        }

        /// <summary>
        /// Converts a <see cref="SqlProject"/> into a <see cref="Project"/>.
        /// </summary>
        /// <param name="sqlProject">The <see cref="Sql.Project"/> being converted.</param>
        /// <returns>A new <see cref="Project"/> that contains equivalent values from the <paramref name="sqlProject"/>.</returns>
        public static Project ConvertToProject(SqlProject sqlProject, IMetadataLocator metadataLocator)
        {
            Project project = new Project
                                  {
                                      Id = CreateUri(sqlProject.Id),
                                      Title = sqlProject.Name,
                                      Duration = sqlProject.Duration,
                                      Resolution = sqlProject.Resolution,
                                      AutoSaveInterval = sqlProject.AutoSaveInterval,
                                      RippleMode = sqlProject.RippleMode,
                                      SmpteFrameRate = sqlProject.SmpteFrameRate,
                                      StartTimeCode = sqlProject.StartTimeCode,
                                      Creator = sqlProject.Creator,
                                      Created = sqlProject.Created,
                                      MediaBin = ConvertToMediaBin(sqlProject.MediaBin, metadataLocator)
                                  };

            project.Sequences.Add(new Sequence());

            foreach (SqlComment comment in sqlProject.Comments)
            {
                project.Comments.Add(ConvertToComment(comment));
            }

            foreach (SqlTrack track in sqlProject.Tracks)
            {
                project.Sequences[0].Tracks.Add(ConvertToTrack(track, metadataLocator));
            }

            foreach (SqlTitle title in sqlProject.Titles)
            {
                project.Titles.Add(ConvertToTitle(title));
            }

            return project;
        }

        /// <summary>
        /// Converts the SQL result to media bin.
        /// </summary>
        /// <param name="sqlMediaBin">The SQL result of media bin.</param>
        /// <returns>The <see cref="MediaBin"/>.</returns>
        public static MediaBin ConvertToMediaBin(SqlContainer sqlMediaBin, IMetadataLocator metadataLocator)
        {
            MediaBin mediaBin = new MediaBin
                                    {
                                        Id = CreateUri(sqlMediaBin.Id),
                                        Title = sqlMediaBin.Title
                                    };

            foreach (SqlItem item in sqlMediaBin.Items)
            {
                mediaBin.Items.Add(ConvertToMediaItem(item, metadataLocator));
            }

            foreach (SqlContainer sqlChildContainer in sqlMediaBin.Containers)
            {
                Container childContainer = ConvertToContainer(sqlChildContainer, metadataLocator);

                mediaBin.Containers.Add(childContainer);
            }

            return mediaBin;
        }

        /// <summary>
        /// Converts a <see cref="Project"/> into a <see cref="Sql.Project"/>.
        /// </summary>
        /// <param name="project">The <see cref="Project"/> being converted.</param>
        /// <param name="sqlProject">The converted <see cref="Sql.Project"/> A new <see cref="Sql.Project"/> that contains equivalent values from the <paramref name="project"/>.</param>
        public static void ConvertToSqlProject(Project project, SqlProject sqlProject)
        {
            sqlProject.Name = project.Title;
            sqlProject.Duration = project.Duration;
            sqlProject.Resolution = project.Resolution;
            sqlProject.AutoSaveInterval = project.AutoSaveInterval;
            sqlProject.RippleMode = project.RippleMode;
            sqlProject.SmpteFrameRate = project.SmpteFrameRate;
            sqlProject.StartTimeCode = project.StartTimeCode;
            sqlProject.Created = project.Created;
            sqlProject.Creator = project.Creator;
        }

        /// <summary>
        /// Converts a <see cref="Shot"/> into a <see cref="Sql.Shot"/>.
        /// </summary>
        /// <param name="shot">The <see cref="Shot"/> being converted.</param>
        /// <param name="sqlShot">The converted <see cref="Sql.Shot"/> that contains equivalent values from the <paramref name="shot"/>.</param>
        /// <param name="items">The existent items to look for the items of the shot.</param>
        public static void ConvertToSqlShot(Shot shot, SqlShot sqlShot, IEnumerable<SqlItem> items)
        {
            sqlShot.Item = ConvertToSqlItem(shot.Source, items);
            sqlShot.TrackMarkIn = shot.TrackAnchor.MarkIn;
            sqlShot.TrackMarkOut = shot.TrackAnchor.MarkOut;
            sqlShot.ItemMarkIn = shot.SourceAnchor.MarkIn;
            sqlShot.ItemMarkOut = shot.SourceAnchor.MarkOut;
            sqlShot.Volume = shot.Volume;
        }

        /// <summary>
        /// Converts a <see cref="Comment"/> into a <see cref="Sql.Comment"/>.
        /// </summary>
        /// <param name="comment">The <see cref="Comment"/> being converted.</param>
        /// <param name="sqlComment">The converted <see cref="Sql.Comment"/> that contains equivalent values from the <paramref name="comment"/>.</param>
        public static void ConvertToSqlComment(Comment comment, SqlComment sqlComment)
        {
            sqlComment.Creator = comment.Creator;
            sqlComment.Created = comment.Created;
            sqlComment.CommentType = comment.Type;
            sqlComment.Text = comment.Text;
            sqlComment.MarkIn = comment.MarkIn;
            sqlComment.MarkOut = comment.MarkOut;

            InkComment inkComment = comment as InkComment;

            if (inkComment != null)
            {
                sqlComment.Strokes = inkComment.Strokes;
            }
        }

        /// <summary>
        /// Converts a <see cref="Container"/> into a <see cref="Sql.Container"/>.
        /// </summary>
        /// <param name="mediaBin">The <see cref="Container"/> being converted.</param>
        /// <param name="sqlMediaBin">The converted <see cref="Sql.Container"/> that contains equivalent values from the <paramref name="mediaBin"/>.</param>
        /// /// <param name="items">The existent items to look for the items of the container.</param>
        /// <returns>A new <see cref="Project"/> that contains equivalent values from the <paramref name="sqlMediaBin"/>.</returns>
        public static SqlContainer ConvertToSqlMediaBin(Container mediaBin, SqlContainer sqlMediaBin, IEnumerable<SqlItem> items)
        {
            sqlMediaBin.Title = mediaBin.Title;

            foreach (MediaItem item in mediaBin.Items)
            {
                sqlMediaBin.Items.Add(ConvertToSqlItem(item, items));
            }

            return sqlMediaBin;
        }

        /// <summary>
        /// Converts a <see cref="Track"/> into a <see cref="Sql.Track"/>.
        /// </summary>
        /// <param name="track">The <see cref="Track"/> being converted.</param>
        /// <param name="sqlTrack">The converted <see cref="Sql.Track"/> that contains equivalent values from the <paramref name="track"/>.</param>
        public static void ConvertToSqlTrack(Track track, SqlTrack sqlTrack)
        {
            sqlTrack.TrackType = track.TrackType;
        }

        /// <summary>
        /// Converts a <see cref="Title"/> into a <see cref="Sql.Title"/>.
        /// </summary>
        /// <param name="title">The <see cref="Title"/> being converted.</param>
        /// <param name="sqlTitle">The converted <see cref="Sql.Title"/> that contains equivalent values from the <paramref name="title"/>.</param>
        public static void ConvertToSqlTitle(Title title, SqlTitle sqlTitle)
        {
            sqlTitle.TrackMarkIn = title.TrackAnchor.MarkIn.GetValueOrDefault();
            sqlTitle.TrackMarkOut = title.TrackAnchor.MarkOut.GetValueOrDefault();
            sqlTitle.MainText = title.TextBlockCollection[0].Text;
            sqlTitle.SubText = title.TextBlockCollection[1].Text;
        }

        /// <summary>
        /// Converts the <see cref="List{SqlTitleTemplate}"/> to <see cref="TitleTemplateCollection"/>.
        /// </summary>
        /// <param name="templates">The <see cref="List{SqlTitleTemplate}"/> to be converted.</param>
        /// <returns>The converted <see cref="TitleTemplateCollection"/>.</returns>
        public static TitleTemplateCollection ConvertToTitleTemplates(List<SqlTitleTemplate> templates)
        {
            TitleTemplateCollection titleTemplates = new TitleTemplateCollection();

            foreach (SqlTitleTemplate titleTemplate in templates)
            {
                titleTemplates.Add(ConvertToTitleTemplate(titleTemplate));
            }

            return titleTemplates;
        }

        /// <summary>
        /// Converts the <see cref="List{SqlProject}"/> to <see cref="ProjectCollection"/>.
        /// </summary>
        /// <param name="projects">The projects.</param>
        /// <returns>The <see cref="ProjectCollection"/>.</returns>
        public static ProjectCollection ConvertToProjects(List<SqlProject> projects, IMetadataLocator metadataLocator)
        {
            ProjectCollection projectCollection = new ProjectCollection();

            foreach (SqlProject sqlProject in projects)
            {
                projectCollection.Add(ConvertToProject(sqlProject, metadataLocator));
            }

            return projectCollection;
        }

        /// <summary>
        /// Converts the <see cref="SqlTrack"/> to <see cref="Track"/>.
        /// </summary>
        /// <param name="sqlTrack">The <see cref="SqlTrack"/>.</param>
        /// <returns>The <see cref="Track"/>.</returns>
        private static Track ConvertToTrack(SqlTrack sqlTrack, IMetadataLocator metadataLocator)
        {
            Track track = new Track
                              {
                                  Id = CreateUri(sqlTrack.Id),
                                  TrackType = sqlTrack.TrackType
                              };

            foreach (SqlShot sqlShot in sqlTrack.Shots)
            {
                track.Shots.Add(ConvertToShot(sqlShot, metadataLocator));
            }

            return track;
        }

        /// <summary>
        /// Converts the <see cref="SqlShot"/> to <see cref="Shot"/>.
        /// </summary>
        /// <param name="sqlShot">The <see cref="SqlShot"/> to be converted.</param>
        /// <returns>The converted <see cref="Shot"/>.</returns>
        private static Shot ConvertToShot(SqlShot sqlShot, IMetadataLocator metadataLocator)
        {
            Shot shot = new Shot
            {
                Id = CreateUri(sqlShot.Id),
                Source = ConvertToMediaItem(sqlShot.Item, metadataLocator),
                TrackAnchor = new Anchor { MarkIn = sqlShot.TrackMarkIn, MarkOut = sqlShot.TrackMarkOut },
                SourceAnchor = new Anchor { MarkIn = sqlShot.ItemMarkIn, MarkOut = sqlShot.ItemMarkOut },
                Volume = sqlShot.Volume.GetValueOrDefault()
            };

            foreach (SqlComment comment in sqlShot.Comments)
            {
                shot.Comments.Add(ConvertToComment(comment));
            }

            return shot;
        }

        /// <summary>
        /// Converts the <see cref="SqlComment"/> to <see cref="Comment"/>.
        /// </summary>
        /// <param name="sqlComment">The <see cref="SqlComment"/>.</param>
        /// <returns>The <see cref="Comment"/>.</returns>
        private static Comment ConvertToComment(SqlComment sqlComment)
        {
            Comment comment;

            if (string.IsNullOrEmpty(sqlComment.Strokes))
            {
                comment = new Comment();
            }
            else
            {
                comment = new InkComment { Strokes = sqlComment.Strokes };
            }

            comment.Id = CreateUri(sqlComment.Id);
            comment.Created = sqlComment.Created;
            comment.Creator = sqlComment.Creator;
            comment.Text = sqlComment.Text;
            comment.Type = sqlComment.CommentType;
            comment.MarkIn = sqlComment.MarkIn;
            comment.MarkOut = sqlComment.MarkOut;

            return comment;
        }

        /// <summary>
        /// Converts the <see cref="SqlItem"/> to <see cref="MediaItem"/>.
        /// </summary>
        /// <param name="item">The <see cref="SqlItem"/>.</param>
        /// <returns>The <see cref="MediaItem"/>.</returns>
        private static MediaItem ConvertToMediaItem(SqlItem item, IMetadataLocator metadataLocator)
        {
            MediaItem mediaItem = new MediaItem();

            if (item.ItemType.ToUpperInvariant() == "video".ToUpperInvariant())
            {
                bool isAdaptiveStreaming = false;
                Uri thumbnailUri = null;
                IList<string> dataStreams = new List<string>();
                List<AudioStreamInfo> audioStreamsInformation = new List<AudioStreamInfo>();
                IList<string> videoStreamInformation = new List<string>();
                SmpteFrameRate smpteFrameRate = SmpteFrameRate.Smpte2997NonDrop;
                double duration = 0;
                double width = 0;
                double height = 0;

                if (item.Resources.Count() > 0)
                {
                    SqlResource sqlVideoResource = item.Resources
                        .Where(x => x.ResourceType.ToUpperInvariant().Contains("smoothstream".ToUpper(CultureInfo.InvariantCulture)))
                        .FirstOrDefault();
                    if (sqlVideoResource != null)
                    {
                        isAdaptiveStreaming = true;
                        Uri manifestUri = new Uri(sqlVideoResource.Ref);
                        Metadata metadata = metadataLocator.GetMetadata(manifestUri);

                        if (metadata != null)
                        {
                            IEnumerable<StreamInfo> audioStream = metadata.MetadataFields.SingleOrDefault(mf => mf.Name.Equals("AudioStreams", StringComparison.OrdinalIgnoreCase)).Value as IEnumerable<StreamInfo>;

                            if (audioStream != null)
                            {
                                foreach (StreamInfo streamInfo in audioStream)
                                {
                                    string name = null;

                                    if (streamInfo.Attributes.ContainsKey("Name"))
                                    {
                                        name = streamInfo.Attributes["Name"];
                                    }

                                    bool isStereo = true;

                                    if (streamInfo.QualityLevels.Count > 0 && streamInfo.QualityLevels.FirstOrDefault().Attributes.ContainsKey("Channels"))
                                    {
                                        isStereo = streamInfo.QualityLevels.FirstOrDefault().Attributes["Channels"] == "2";
                                    }

                                    audioStreamsInformation.Add(new AudioStreamInfo { IsStereo = isStereo, Name = name });
                                }
                            }

                            MetadataField videoStreamMetadataField = metadata.MetadataFields.SingleOrDefault(mf => mf.Name.Equals("VideoStreams", StringComparison.OrdinalIgnoreCase));

                            if (videoStreamMetadataField != null)
                            {
                                IList<string> videoStreams = videoStreamMetadataField.Value as IList<string>;

                                if (videoStreams != null)
                                {
                                    videoStreamInformation = videoStreams;
                                }
                            }
                        }
                    }
                    else if (item.Resources.Any(x => x.ResourceType.ToUpperInvariant().Contains("master".ToUpper(CultureInfo.InvariantCulture))))
                    {
                        sqlVideoResource = item.Resources.Where(x => x.ResourceType.ToUpperInvariant().Contains("master".ToUpper(CultureInfo.InvariantCulture))).First();
                    }
                    else
                    {
                        sqlVideoResource = item.Resources.First();
                    }
                    
                    if (sqlVideoResource.VideoFormat != null)
                    {
                        duration = sqlVideoResource.VideoFormat.Duration.GetValueOrDefault();
                        width = sqlVideoResource.VideoFormat.ResolutionX.GetValueOrDefault();
                        height = sqlVideoResource.VideoFormat.ResolutionY.GetValueOrDefault();

                        if (sqlVideoResource.VideoFormat.FrameRate == null || !Enum.TryParse(sqlVideoResource.VideoFormat.FrameRate, true, out smpteFrameRate))
                        {
                            smpteFrameRate = SmpteFrameRate.Smpte2997NonDrop;
                        }
                    }

                    SqlResource sqlThumbnailResource = item.Resources
                        .Where(x => x.ResourceType.Equals("Thumbnail", StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefault();
                    if (sqlThumbnailResource != null)
                    {
                        thumbnailUri = new Uri(sqlThumbnailResource.Ref);
                    }

                    SqlResource sqlDataStreamResource = item.Resources
                        .Where(x => x.ResourceType.Equals("DataStreams", StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefault();
                    if (sqlDataStreamResource != null)
                    {
                        dataStreams = string.IsNullOrWhiteSpace(sqlDataStreamResource.Ref)
                            ? dataStreams
                            : sqlDataStreamResource.Ref.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    }
                }

                mediaItem = CreateVideoItem(CreateUri(item.Id), isAdaptiveStreaming, dataStreams, thumbnailUri, duration, smpteFrameRate, width, height, audioStreamsInformation, videoStreamInformation);
            }
            else if (item.ItemType.ToUpperInvariant() == "audio".ToUpperInvariant())
            {
                AudioItem audioItem = new AudioItem { Id = CreateUri(item.Id) };

                if (item.Resources.Count() > 0)
                {
                    SqlResource sqlResource = item.Resources.Where(x => x.ResourceType.ToUpperInvariant().Contains("master".ToUpper(CultureInfo.InvariantCulture))).FirstOrDefault();

                    if (sqlResource == null)
                    {
                        sqlResource = item.Resources.First();
                    }

                    if (sqlResource.AudioFormat != null)
                    {
                        audioItem.Duration = sqlResource.AudioFormat.Duration;
                    }
                }

                mediaItem = audioItem;
            }
            else if (item.ItemType.ToUpperInvariant() == "IMAGE")
            {
                ImageItem imageItem = new ImageItem { Id = CreateUri(item.Id) };

                if (item.Resources.Count() > 0)
                {
                    SqlResource sqlResource = item.Resources.Where(x => x.ResourceType.ToUpperInvariant().Contains("master".ToUpper(CultureInfo.InvariantCulture))).FirstOrDefault();

                    if (sqlResource == null)
                    {
                        sqlResource = item.Resources.First();
                    }

                    if (sqlResource.ImageFormat != null)
                    {
                        imageItem.Width = sqlResource.ImageFormat.ResolutionX.GetValueOrDefault();
                        imageItem.Height = sqlResource.ImageFormat.ResolutionY.GetValueOrDefault();
                    }
                }

                mediaItem = imageItem;
            }

            mediaItem.Title = item.Title;
            mediaItem.Description = item.Description;

            foreach (SqlResource sqlResource in item.Resources.Where(
                r => !r.ResourceType.Equals("DataStreams", StringComparison.OrdinalIgnoreCase) && !r.ResourceType.Equals("Thumbnail", StringComparison.OrdinalIgnoreCase)))
            {
                mediaItem.Resources.Add(ConvertToResource(sqlResource));
            }

            return mediaItem;
        }

        private static VideoItem CreateVideoItem(Uri id, bool isAdaptiveStreaming, IList<string> dataStreams, Uri thumbnailUri, double? duration, SmpteFrameRate smpteFrameRate, double width, double height, IList<AudioStreamInfo> audioStreamsInformation, IList<string> videoStreamInformation)
        {
            VideoItem item;

            if (isAdaptiveStreaming)
            {
                SmoothStreamingVideoItem tempItem = new SmoothStreamingVideoItem();

                if (dataStreams != null && dataStreams.Count > 0)
                {
                    tempItem.DataStreams = (List<string>)dataStreams;
                }

                if (audioStreamsInformation != null && audioStreamsInformation.Count > 0)
                {
                    tempItem.AudioStreams = (List<AudioStreamInfo>)audioStreamsInformation;
                }

                if (videoStreamInformation != null && videoStreamInformation.Count > 0)
                {
                    tempItem.VideoStreams = (List<string>)videoStreamInformation;
                }

                item = tempItem;
            }
            else
            {
                item = new VideoItem();
            }

            item.Id = id;
            item.ThumbnailSource = (thumbnailUri != null) ? thumbnailUri.AbsoluteUri : null;
            item.Duration = duration;
            item.FrameRate = smpteFrameRate;
            item.Width = (int)width;
            item.Height = (int)height;

            return item;
        }

        /// <summary>
        /// Converts to <see cref="SqlResource"/> to <see cref="Resource"/>.
        /// </summary>
        /// <param name="resource">The <see cref="SqlResource"/>.</param>
        /// <returns>The <see cref="Resource"/>.</returns>
        private static Resource ConvertToResource(SqlResource resource)
        {
            return new Resource
            {
                Id = CreateUri(resource.Id),
                Ref = resource.Ref,
                ResourceType = resource.ResourceType
            };
        }

        /// <summary>
        /// Converts the <see cref="SqlTitle"/> to <see cref="Title"/>.
        /// </summary>
        /// <param name="title">The <see cref="SqlTitle"/>.</param>
        /// <returns>The <see cref="Title"/>.</returns>
        private static Title ConvertToTitle(SqlTitle title)
        {
            return new Title
                       {
                           Id = CreateUri(title.Id),
                           TrackAnchor = new Anchor { MarkIn = title.TrackMarkIn, MarkOut = title.TrackMarkOut },
                           TitleTemplate = ConvertToTitleTemplate(title.TitleTemplate),
                           TextBlockCollection =
                               {
                                   new TextBlock
                                       {
                                           Text = title.MainText
                                       },
                                    new TextBlock
                                       {
                                           Text = title.SubText
                                       }
                               }
                       };
        }

        /// <summary>
        /// Converts the <see cref="SqlTitleTemplate"/> to <see cref="TitleTemplate"/>.
        /// </summary>
        /// <param name="titleTemplate">The <see cref="SqlTitleTemplate"/>.</param>
        /// <returns>The <see cref="TitleTemplate"/>.</returns>
        private static TitleTemplate ConvertToTitleTemplate(SqlTitleTemplate titleTemplate)
        {
            return new TitleTemplate
                       {
                           Id = CreateUri(titleTemplate.Id),
                           TemplateName = titleTemplate.TemplateName
                       };
        }

        /// <summary>
        /// Returns the <see cref="SqlItem"/> corresponding to the given source in the list of SqlItems.
        /// </summary>
        /// <param name="source">The <see cref="Item"/> to be searched.</param>
        /// <param name="items">The list of SqlItem.</param>
        /// <returns>The <see cref="SqlItem"/> corresponding to the given Item.</returns>
        private static SqlItem ConvertToSqlItem(Item source, IEnumerable<SqlItem> items)
        {
            return items.Where(x => CreateUri(x.Id) == source.Id).Single();
        }

        private static Uri CreateUri(int id)
        {
            string uriString = string.Format(CultureInfo.InvariantCulture, ItemIdUriTemplate, id);
            return new Uri(uriString);
        }
    }
}
