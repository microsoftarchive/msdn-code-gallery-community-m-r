// <copyright file="DataServiceTranslator.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DataServiceTranslator.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Translators
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Xml;
    using Models;
    using RCE.Services.Contracts;

    using SMPTETimecode;
    using Comment = RCE.Services.Contracts.Comment;
    using InkComment = RCE.Services.Contracts.InkComment;
    using MediaBin = RCE.Services.Contracts.MediaBin;
    using Project = RCE.Services.Contracts.Project;
    using Sequence = RCE.Infrastructure.Models.Sequence;
    using TitleTemplate = RCE.Services.Contracts.TitleTemplate;
    using Track = RCE.Services.Contracts.Track;
    using TransitionType = RCE.Services.Contracts.TransitionType;

    /// <summary>
    /// Handle translations between the client and the server contracts.
    /// </summary>
    public static class DataServiceTranslator
    {
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
        /// Constant used for Anchor.
        /// </summary>
        private const string Anchors = "Anchors";

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
        /// Mapping used to create ids.
        /// </summary>
        private static readonly Dictionary<string, string> mappings = new Dictionary<string, string>
                           {
                               { typeof(RCE.Services.Contracts.Project).Name, Projects },
                               { typeof(RCE.Services.Contracts.MediaBin).Name, MediaBins },
                               { typeof(RCE.Services.Contracts.Comment).Name, Annotations },
                               { typeof(RCE.Services.Contracts.Track).Name, Tracks },
                               { typeof(RCE.Services.Contracts.Shot).Name, Shots },
                               { typeof(RCE.Services.Contracts.Anchor).Name, Anchors },
                               { typeof(RCE.Services.Contracts.Container).Name, Containers },
                               { typeof(RCE.Services.Contracts.TitleTemplate).Name, TitleTemplates },
                               { typeof(RCE.Services.Contracts.Title).Name, Titles }
                           };

        /// <summary>
        /// Identifies the default content network prefix to be used to build the assets uri.
        /// </summary>
        private static string contentNetworkPrefix = "http://rcecdn/";

        /// <summary>
        /// Gets or sets the content network prefix.
        /// </summary>
        /// <value>A <seealso cref="string"/> that represents the current content network prefix.</value>
        public static string ContentNetworkPrefix
        {
            get
            {
                return contentNetworkPrefix;
            }

            set
            {
                contentNetworkPrefix = value;
            }
        }

        /// <summary>
        /// Translate a <see cref="Container"/> into a <seealso cref="List{T}"/> of <see cref="Asset"/>s.
        /// </summary>
        /// <param name="container">The <see cref="Container"/>being translated.</param>
        /// <returns>A <seealso cref="List{T}"/> of <see cref="Asset"/>s.</returns>
        public static List<Asset> ConvertToAssets(Container container)
        {
            List<Asset> assets = new List<Asset>();

            if (container != null && container.Items != null)
            {
                foreach (Item item in container.Items)
                {
                    if (item != null)
                    {
                        Asset asset = ConvertToAsset(item);

                        if (asset != null)
                        {
                            assets.Add(asset);
                        }
                    }
                }

                if (container.Containers != null)
                {
                    foreach (Container childContainer in container.Containers)
                    {
                        if (childContainer != null)
                        {
                            FolderAsset folderAsset = ConvertToFolderAsset(childContainer);

                            if (folderAsset != null)
                            {
                                IList<Asset> childAssets = ConvertToAssets(childContainer);

                                foreach (Asset asset in childAssets)
                                {
                                    FolderAsset folder = asset as FolderAsset;

                                    if (folder != null)
                                    {
                                        folder.ParentFolder = folderAsset;
                                    }

                                    folderAsset.Assets.Add(asset);
                                }

                                assets.Add(folderAsset);
                            }
                        }
                    }
                }
            }

            return assets;
        }

        /// <summary>
        /// Translate a <see cref="Container"/> into a <see cref="RCE.Infrastructure.Models.MediaBin"/>.
        /// </summary>
        /// <param name="mediaBinContainer">The <see cref="Container"/>being translated.</param>
        /// <returns>A <see cref="RCE.Infrastructure.Models.MediaBin"/>.</returns>
        public static RCE.Infrastructure.Models.MediaBin ConvertToMediaBin(Container mediaBinContainer)
        {
            RCE.Infrastructure.Models.MediaBin mediaBin = new RCE.Infrastructure.Models.MediaBin();

            if (mediaBinContainer != null)
            {
                mediaBin.ProviderUri = mediaBinContainer.Id;
                mediaBin.Created = mediaBinContainer.Created;
                mediaBin.Creator = mediaBinContainer.Creator;
                mediaBin.Modified = mediaBinContainer.Modified;
                mediaBin.ModifiedBy = mediaBinContainer.ModifiedBy;

                // mediaBin.Title = mediaBinContainer.Title;
                // mediaBin.Description = mediaBinContainer.Description;
                if (mediaBinContainer.Items != null)
                {
                    foreach (Item item in mediaBinContainer.Items)
                    {
                        if (item != null)
                        {
                            var subClipItem = item as SubClipItem;
                            Asset asset;
                            
                            if (subClipItem != null)
                            {
                                asset = ConvertToVideoAssetInOut(subClipItem);
                            }
                            else
                            {
                                asset = ConvertToAsset(item);
                            }

                            if (asset != null)
                            {
                                mediaBin.Assets.Add(asset);
                            }
                        }
                    }
                }

                if (mediaBinContainer.Containers != null)
                {
                    foreach (Container container in mediaBinContainer.Containers)
                    {
                        FolderAsset folderAsset = ConvertToFolderAsset(container);
                        IList<Asset> assets = ConvertToAssets(container);

                        foreach (Asset asset in assets)
                        {
                            folderAsset.Assets.Add(asset);
                        }

                        mediaBin.Assets.Add(folderAsset);
                    }
                }
            }

            return mediaBin;
        }

        /// <summary>
        /// Translate a <see cref="Services.Contracts.Project"/> into a <see cref="RCE.Infrastructure.Models.Project"/>.
        /// </summary>
        /// <param name="projectContainer">The <see cref="Services.Contracts.Project"/>being translated.</param>
        /// <returns>A <see cref="RCE.Infrastructure.Models.Project"/>.</returns>
        public static RCE.Infrastructure.Models.Project ConvertToProject(Project projectContainer)
        {
            RCE.Infrastructure.Models.Project project = null;

            if (projectContainer != null)
            {
                project = new RCE.Infrastructure.Models.Project
                              {
                                  Name = projectContainer.Title,
                                  ProviderUri = projectContainer.Id,
                                  Creator = projectContainer.Creator,
                                  Created = projectContainer.Created,
                                  ModifiedBy = projectContainer.ModifiedBy,
                                  Modified = projectContainer.Modified,
                                  MediaBin = ConvertToMediaBin(projectContainer.MediaBin),
                                  Resolution = projectContainer.Resolution,
                                  Duration = projectContainer.Duration,
                                  RippleMode = projectContainer.RippleMode.GetValueOrDefault(),
                                  ProjectThumbnail = projectContainer.ProjectThumbnail,
                                  AutoSaveInterval = projectContainer.AutoSaveInterval.GetValueOrDefault() == 0 ? 15 : projectContainer.AutoSaveInterval.GetValueOrDefault(),
                                  Metadata = projectContainer.Metadata,
                              };

                SmpteFrameRate frameRate = SmpteFrameRate.Smpte2997NonDrop;

                if (projectContainer.SmpteFrameRate != null && Enum.IsDefined(typeof(SmpteFrameRate), projectContainer.SmpteFrameRate))
                {
                    frameRate = (SmpteFrameRate)Enum.Parse(typeof(SmpteFrameRate), projectContainer.SmpteFrameRate, false);
                }

                project.SmpteFrameRate = frameRate;
                project.StartTimeCode = TimeCode.FromAbsoluteTime(projectContainer.StartTimeCode.GetValueOrDefault(), frameRate);

                project.AddComments(ConvertToComments(projectContainer.Comments));

                foreach (var sequence in projectContainer.Sequences)
                {
                    var timeline = new Sequence
                                       {
                                           Tracks = ConvertToTimeline(sequence.Tracks, project.Comments, frameRate),
                                           Id = sequence.Id,
                                           Name = sequence.Name,
                                           EncodedThumbnail = sequence.SequenceThumbnail,
                                       };

                    if (sequence.CommentsCollection == null)
                    {
                        sequence.CommentsCollection = new CommentCollection();
                    }

                    sequence.AdOpportunities.ForEach(ao => timeline.AdOpportunities.Add(ao));
                    sequence.MarkerCollection.ForEach(m => timeline.Markers.Add(m));
                    sequence.CommentsCollection.ForEach(c => timeline.CommentElements.Add(ConvertToComment(c)));

                    project.AddTimeline(timeline);
                }

                if (projectContainer.Titles.Count > 0)
                {
                    IDictionary<Guid, RCE.Infrastructure.Models.Track> tracksByTimeline = ConvertToTitleAssets(projectContainer.Titles, frameRate);

                    foreach (var kvp in tracksByTimeline)
                    {
                        Guid timelineId = kvp.Key;
                        Models.Track track = kvp.Value;
                        project.Timelines.First(t => t.Id == timelineId).Tracks.Add(track);
                    }
                }
            }

            return project;
        }

        /// <summary>
        /// Translated the DataService Project collection to the Project collection.
        /// </summary>
        /// <param name="dataServiceProjects">The Collection of <see cref="Project"/>.</param>
        /// <returns>The collection of <see cref="RCE.Infrastructure.Models.Project"/>.</returns>
        public static List<RCE.Infrastructure.Models.Project> ConvertToProjects(ObservableCollection<Project> dataServiceProjects)
        {
            if (dataServiceProjects == null)
            {
                return null;
            }

            List<RCE.Infrastructure.Models.Project> projects = new List<RCE.Infrastructure.Models.Project>();

            foreach (Project dataServiceProject in dataServiceProjects)
            {
                if (dataServiceProject != null)
                {
                    RCE.Infrastructure.Models.Project project = ConvertToProject(dataServiceProject);

                    if (project != null)
                    {
                        projects.Add(project);
                    }
                }
            }

            return projects;
        }

        /// <summary>
        /// Translate a <see cref="RCE.Infrastructure.Models.Project"/> into a <see cref="Project"/>.
        /// </summary>
        /// <param name="project">The <see cref="RCE.Infrastructure.Models.Project"/>being translated.</param>
        /// <returns>A <see cref="Project"/>.</returns>
        public static Project ConvertToDataServiceProject(RCE.Infrastructure.Models.Project project)
        {
            CreateIds(project);

            Project dataProject = null;

            if (project != null)
            {
                dataProject = new Project
                                  {
                                      Id = project.ProviderUri,
                                      Title = project.Name,
                                      Creator = project.Creator,
                                      Created = project.Created,
                                      Modified = DateTime.Now,
                                      ModifiedBy = project.ModifiedBy,
                                      MediaBin = ConvertToDataServiceMediaBin(project.MediaBin),
                                      Resolution = project.Resolution,
                                      Duration = project.Duration,
                                      AutoSaveInterval = project.AutoSaveInterval,
                                      StartTimeCode = project.StartTimeCode.TotalSeconds,
                                      SmpteFrameRate = project.SmpteFrameRate.ToString(),
                                      RippleMode = project.RippleMode,
                                      ProjectThumbnail = project.ProjectThumbnail,
                                      Comments = new CommentCollection(),
                                      Sequences = new SequenceCollection(),
                                      Titles = new TitleCollection(),
                                      Metadata = project.Metadata,
                                  };

                List<Comment> comments = ConvertToDataServiceComments(project.Comments);

                comments.ForEach(comment => dataProject.Comments.Add(comment));

                foreach (var timeline in project.Timelines)
                {
                    var sequence = new RCE.Services.Contracts.Sequence
                                       {
                                           Id = timeline.Id,
                                           Name = timeline.Name,
                                           SequenceThumbnail = timeline.EncodedThumbnail
                                       };
                    dataProject.Sequences.Add(sequence);
                    List<Track> tracks = ConvertToDataServiceTracks(timeline.Tracks, dataProject.Comments);
                    tracks.ForEach(track => sequence.Tracks.Add(track));
                    timeline.AdOpportunities.ForEach(ao => sequence.AdOpportunities.Add(ao));
                    timeline.Markers.ForEach(m => sequence.MarkerCollection.Add(m));
                    timeline.CommentElements.ForEach(c => sequence.CommentsCollection.Add(ConvertToDataServiceComment(c)));
                }
            }

            return dataProject;
        }

        /// <summary>
        /// Converts the <see cref="TitleTemplate"/> <see cref="ObservableCollection{T}"/> to <see cref="List{TitleTemplate}"/>.
        /// </summary>
        /// <param name="templates">The templates.</param>
        /// <returns>The collection of <see cref="RCE.Infrastructure.Models.TitleTemplate"/>.</returns>
        public static List<RCE.Infrastructure.Models.TitleTemplate> ConvertToTitleTemplates(ObservableCollection<TitleTemplate> templates)
        {
            List<RCE.Infrastructure.Models.TitleTemplate> titleTemplates = new List<RCE.Infrastructure.Models.TitleTemplate>();

            if (templates != null)
            {
                foreach (TitleTemplate template in templates)
                {
                    if (template != null)
                    {
                        RCE.Infrastructure.Models.TitleTemplate titleTemplate = ConvertToTitleTemplate(template);

                        if (titleTemplate != null)
                        {
                            titleTemplates.Add(titleTemplate);
                        }
                    }
                }
            }

            return titleTemplates;
        }

        /// <summary>
        /// Converts the <see cref="Title"/> <see cref="ObservableCollection{T}"/> to <see cref="List{TitleAsset}"/>.
        /// </summary>
        /// <param name="titles">The titles.</param>
        /// <returns>The collection of <see cref="TitleAsset"/>.</returns>
        public static List<TitleAsset> ConvertToTitles(ObservableCollection<Title> titles)
        {
            List<TitleAsset> titleAssets = new List<TitleAsset>();

            foreach (Title title in titles)
            {
                if (title != null)
                {
                    TitleAsset titleAsset = ConvertToTitleAsset(title);

                    if (titleAsset != null)
                    {
                        titleAssets.Add(titleAsset);
                    }
                }
            }

            return titleAssets;
        }

        /// <summary>
        /// Converts the <see cref="Title"/> to <see cref="TitleAsset"/>.
        /// </summary>
        /// <param name="title">The <see cref="Title"/>.</param>
        /// <returns>The <see cref="TitleAsset"/>.</returns>
        private static TitleAsset ConvertToTitleAsset(Title title)
        {
            TitleAsset titleAsset = new TitleAsset
                                        {
                                            ProviderUri = title.Id,
                                            TitleTemplate = ConvertToTitleTemplate(title.TitleTemplate),
                                        };

            if (title.TextBlockCollection.Count > 1)
            {
                titleAsset.MainText = title.TextBlockCollection[0].Text;
                titleAsset.SubText = title.TextBlockCollection[1].Text;
            }

            return titleAsset;
        }

        /// <summary>
        /// Converts the <see cref="Services.Contracts.TitleTemplate"/> to <see cref="RCE.Infrastructure.Models.TitleTemplate"/>.
        /// </summary>
        /// <param name="titleTemplate">The <see cref="Services.Contracts.TitleTemplate"/>.</param>
        /// <returns>The <see cref="RCE.Infrastructure.Models.TitleTemplate"/>.</returns>
        private static RCE.Infrastructure.Models.TitleTemplate ConvertToTitleTemplate(TitleTemplate titleTemplate)
        {
            RCE.Infrastructure.Models.TitleTemplate template = new RCE.Infrastructure.Models.TitleTemplate
                                                                   {
                                                                       ProviderUri = titleTemplate.Id,
                                                                       Title = titleTemplate.TemplateName
                                                                   };

            return template;
        }

        /// <summary>
        /// Converts the <see cref="Services.Contracts.Comment"/> collection to <see cref="RCE.Infrastructure.Models.Comment"/> <see cref="ObservableCollection{T}"/>.
        /// </summary>
        /// <param name="collection">The collection of <see cref="Services.Contracts.Comment"/>.</param>
        /// <returns>The collection of <see cref="RCE.Infrastructure.Models.Comment"/>.</returns>
        private static ObservableCollection<RCE.Infrastructure.Models.Comment> ConvertToComments(IEnumerable<Comment> collection)
        {
            ObservableCollection<RCE.Infrastructure.Models.Comment> comments = new ObservableCollection<RCE.Infrastructure.Models.Comment>();
            if (collection != null)
            {
                foreach (Comment commentItem in collection)
                {
                    if (commentItem != null)
                    {
                        RCE.Infrastructure.Models.Comment comment = ConvertToComment(commentItem);
                        if (comment != null)
                        {
                            comments.Add(comment);
                        }
                    }
                }
            }

            return comments;
        }

        /// <summary>
        /// Converts the <see cref="Title"/> collection to <see cref="RCE.Infrastructure.Models.Track"/>.
        /// </summary>
        /// <param name="collection">The collection of <see cref="Title"/>.</param>
        /// <param name="frameRate">The <see cref="SmpteFrameRate"/>.</param>
        /// <returns>The <see cref="RCE.Infrastructure.Models.Track"/>.</returns>
        private static Dictionary<Guid, RCE.Infrastructure.Models.Track> ConvertToTitleAssets(IEnumerable<Title> collection, SmpteFrameRate frameRate)
        {
            Dictionary<Guid, RCE.Infrastructure.Models.Track> tracksByTimeline = new Dictionary<Guid, Models.Track>();

            if (collection != null)
            {
                foreach (Title titleItem in collection)
                {
                    if (titleItem != null)
                    {
                        RCE.Infrastructure.Models.Track titleTrack;

                        if (tracksByTimeline.ContainsKey(titleItem.SequenceId))
                        {
                            titleTrack = tracksByTimeline[titleItem.SequenceId];
                        }
                        else
                        {
                            titleTrack = new RCE.Infrastructure.Models.Track { TrackType = RCE.Infrastructure.Models.TrackType.Overlay };
                            tracksByTimeline[titleItem.SequenceId] = titleTrack;
                        }

                        TitleAsset title = ConvertToTitleAsset(titleItem);
                        if (title != null)
                        {
                            TimelineElement element = new TimelineElement();
                            element.Asset = title;
                            element.TrackAnchorUri = titleItem.TrackAnchor.Id;
                            element.Position = TimeCode.FromAbsoluteTime(titleItem.TrackAnchor.MarkIn.GetValueOrDefault(), frameRate);
                            element.InPosition = TimeCode.FromAbsoluteTime(0, frameRate);
                            element.OutPosition = TimeCode.FromAbsoluteTime(titleItem.TrackAnchor.MarkOut.GetValueOrDefault(), frameRate);

                            titleTrack.Shots.Add(element);
                        }
                    }
                }
            }

            return tracksByTimeline;
        }

        /// <summary>
        /// Converts the <see cref="RCE.Infrastructure.Models.MediaBin"/> to <see cref="Services.Contracts.MediaBin"/>.
        /// </summary>
        /// <param name="mediaBin">The <see cref="RCE.Infrastructure.Models.MediaBin"/>.</param>
        /// <returns>The <see cref="Services.Contracts.MediaBin"/>.</returns>
        private static MediaBin ConvertToDataServiceMediaBin(RCE.Infrastructure.Models.MediaBin mediaBin)
        {
            MediaBin dataMediaBin = new MediaBin
                                        {
                                            Id = mediaBin.ProviderUri,
                                            Creator = mediaBin.Creator,
                                            Created = mediaBin.Created,
                                            ModifiedBy = mediaBin.ModifiedBy,
                                            Modified = DateTime.Now,
                                            Items = new ItemCollection(),
                                            Containers = new ContainerCollection(),
                                        };

            foreach (Asset asset in mediaBin.Assets)
            {
                FolderAsset folderAsset = asset as FolderAsset;

                if (folderAsset == null)
                {
                    ////dataMediaBin.Items.Add(ConvertToDataServiceItem(asset));
                    if (asset is ISubClipAsset)
                    {
                        dataMediaBin.Items.Add(ConvertToDataServiceSubClipItem(asset));
                    }
                    else
                    {
                        dataMediaBin.Items.Add(ConvertToDataServiceItem(asset));
                    }
                }
                else
                {
                    dataMediaBin.Containers.Add(ConvertToDataServiceContainer(folderAsset));
                }
            }

            return dataMediaBin;
        }

        private static Item ConvertToDataServiceSubClipItem(Asset asset)
        {
            VideoAssetInOut inOutAsset = (VideoAssetInOut)asset;

             SubClipItem subClipItem = new SubClipItem()
                {
                    InPosition = inOutAsset.InPosition,
                    OutPosition = inOutAsset.OutPosition,
                    SequenceVideoStream = inOutAsset.SequenceVideoCamera,
                    Id = inOutAsset.ProviderUri,
                    CMSId = inOutAsset.CMSId,
                    AzureId = inOutAsset.AzureId,
                    Created = inOutAsset.Created,
                    Creator = inOutAsset.Creator,
                    Modified = DateTime.Now,
                    ModifiedBy = inOutAsset.ModifiedBy,
                    Resources = new ResourceCollection { new Resource { Ref = inOutAsset.Source.ToString(), ResourceType = inOutAsset.ResourceType.ToString() } },
                };

            foreach (var audioStream in inOutAsset.SequenceAudioStreams)
            {
                subClipItem.SequenceAudioStreams.Add(
                    new AudioStreamInfo { IsStereo = audioStream.IsStereo, Name = audioStream.Name });
            }

            subClipItem.Title = inOutAsset.Title;
            subClipItem.RelatedItem = ConvertToDataServiceItem(inOutAsset.VideoAsset);

            return subClipItem;
        }

        /// <summary>
        /// Converts the <see cref="FolderAsset"/> to <see cref="Container"/>.
        /// </summary>
        /// <param name="folderAsset">The <see cref="FolderAsset"/>.</param>
        /// <returns>The <see cref="Container"/>.</returns>
        private static Container ConvertToDataServiceContainer(FolderAsset folderAsset)
        {
            var container = new Container
                                {
                                    Id = folderAsset.ProviderUri,
                                    Title = folderAsset.Title,
                                    Items = new ItemCollection(),
                                    Modified = DateTime.Now,
                                    Containers = new ContainerCollection(),
                                };

            foreach (Asset asset in folderAsset.Assets)
            {
                FolderAsset folder = asset as FolderAsset;

                if (folder == null)
                {
                    container.Items.Add(ConvertToDataServiceItem(asset));
                }
                else
                {
                    container.Containers.Add(ConvertToDataServiceContainer(folder));
                }
            }

            return container;
        }

        /// <summary>
        /// Converts the <see cref="RCE.Infrastructure.Models.Track"/> collection to <see cref="List{Track}"/>.
        /// </summary>
        /// <param name="timeline">The collection of <see cref="Track"/>.</param>
        /// <param name="comments">The collection of <see cref="Comment"/>.</param>
        /// <returns>The collection of <see cref="RCE.Services.Contracts.Track"/>.</returns>
        private static List<Track> ConvertToDataServiceTracks(IEnumerable<RCE.Infrastructure.Models.Track> timeline, IEnumerable<Comment> comments)
        {
            List<Track> dataComments = new List<Track>();

            foreach (Models.Track track in timeline)
            {
                Track dataTrack = new Track
                                        {
                                            Id = track.ProviderUri,
                                            Number = track.Number,
                                            TrackType = track.TrackType.ToString(),
                                            Created = track.Created,
                                            Creator = track.Creator,
                                            ModifiedBy = track.ModifiedBy,
                                            Modified = DateTime.Now,
                                            Shots = new ShotCollection(),
                                            Balance = track.Balance,
                                            Volume = track.Volume,
                                        };

                foreach (TimelineElement element in track.Shots)
                {
                    dataTrack.Shots.Add(ConvertToDataServiceShot(element, comments));
                }

                dataComments.Add(dataTrack);
            }

            return dataComments;
        }

        /// <summary>
        /// Converts to data service shot.
        /// </summary>
        /// <param name="element">The <see cref="TimelineElement"/>.</param>
        /// <param name="comments">The <see cref="RCE.Services.Contracts.Comment"/> collection.</param>
        /// <returns>The <see cref="Shot"/> value.</returns>
        private static Shot ConvertToDataServiceShot(TimelineElement element, IEnumerable<Comment> comments)
        {
            Shot shot = new Shot
                {
                    Id = element.ProviderUri,
                    Source = element.Asset is ISubClipAsset ? ConvertToDataServiceSubClipItem(element.Asset) : ConvertToDataServiceItem(element.Asset),
                    Title = element.Asset.Title,
                    CMSId = element.Asset.CMSId,
                    AzureId = element.Asset.AzureId,
                    Volume = (decimal)(double.IsNaN(element.Volume) ? 1 : element.Volume * 100),
                    Created = element.Created,
                    Creator = element.Creator,
                    Modified = DateTime.Now,
                    ModifiedBy = element.ModifiedBy,

                    // TODO: Fix this for the semantic data provider
                    SourceAnchor =
                        new Anchor
                            {
                                Id = element.SourceAnchorUri,
                                Created = DateTime.Now,
                                Modified = DateTime.Now,
                                MarkIn = element.InPosition.TotalSeconds,
                                MarkOut = element.OutPosition.TotalSeconds
                            },
                    TrackAnchor =
                        new Anchor
                            {
                                Id = element.TrackAnchorUri,
                                Created = DateTime.Now,
                                Modified = DateTime.Now,
                                MarkIn = element.Position.TotalSeconds,
                                MarkOut = 0
                            },
                    Comments = new CommentCollection(),
                    VolumeNodeCollection = ConvertToVolumeLevels(element.VolumeNodeCollection, element.Duration.FrameRate),
                    Balance = element.Balance,
                    SequenceAudioStream = element.SelectedAudioStream,
                    SequenceVideoStream = element.SelectedVideoStream
                };
            
            // should use real logic eventually, when there are more transition types
            shot.InTransition.Duration = element.InTransition.Duration;
            shot.InTransition.Type = element.InTransition.Type == Models.TransitionType.Fade
                                          ? RCE.Services.Contracts.TransitionType.Fade
                                          : RCE.Services.Contracts.TransitionType.None;

            shot.OutTransition.Duration = element.OutTransition.Duration;
            shot.OutTransition.Type = element.OutTransition.Type == Models.TransitionType.Fade
                                          ? RCE.Services.Contracts.TransitionType.Fade
                                          : RCE.Services.Contracts.TransitionType.None;

            foreach (RCE.Infrastructure.Models.Comment comment in element.Comments)
            {
                Comment dataComment = comments.Where(x => x.Id == comment.ProviderUri).FirstOrDefault();

                if (dataComment != null)
                {
                    shot.Comments.Add(dataComment);
                }
            }

            return shot;
        }

        /// <summary>
        /// Converts the <see cref="Asset"/> to <see cref="Item"/>.
        /// </summary>
        /// <param name="asset">The <see cref="Asset"/>.</param>
        /// <returns>The <see cref="Item"/> value.</returns>
        private static Item ConvertToDataServiceItem(Asset asset)
        {
            VideoAsset videoAsset = asset as VideoAsset;
            ImageAsset imageAsset = asset as ImageAsset;
            AudioAsset audioAsset = asset as AudioAsset;
            OverlayAsset overlayAsset = asset as OverlayAsset;

            SmoothStreamingVideoAsset smoothStreamingVideoAsset = asset as SmoothStreamingVideoAsset;
            SmoothStreamingAudioAsset smoothStreamingAudioAsset = asset as SmoothStreamingAudioAsset;

            if (smoothStreamingVideoAsset != null)
            {
                var item = new SmoothStreamingVideoItem
                    {
                        Id = smoothStreamingVideoAsset.ProviderUri,
                        Created = smoothStreamingVideoAsset.Created,
                        Creator = smoothStreamingVideoAsset.Creator,
                        Modified = DateTime.Now,
                        ModifiedBy = smoothStreamingVideoAsset.ModifiedBy,

                        // Description =  videoAsset.Description,
                        Title = smoothStreamingVideoAsset.Title,
                        Duration = smoothStreamingVideoAsset.Duration.TotalSeconds,
                        FrameRate =
                            (SmpteFrameRate)
                            Enum.Parse(typeof(SmpteFrameRate), smoothStreamingVideoAsset.FrameRate.ToString(), true),
                        Height = smoothStreamingVideoAsset.Height,
                        Width = smoothStreamingVideoAsset.Width,
                        Resources =
                            new ResourceCollection
                                {
                                    new Resource
                                        {
                                            Ref = smoothStreamingVideoAsset.Source.ToString(),
                                            ResourceType = smoothStreamingVideoAsset.ResourceType.ToString()
                                        }
                                },
                        ThumbnailSource = smoothStreamingVideoAsset.ThumbnailSource,
                        StartPosition = smoothStreamingVideoAsset.StartPosition,
                        DataStreams = smoothStreamingVideoAsset.DataStreams,
                        ExternalManifests = smoothStreamingVideoAsset.ExternalManifests,

                        // Metadata = videoAsset.Metadata
                        IsStereo = smoothStreamingVideoAsset.IsStereo,
                        ArchiveURL = smoothStreamingVideoAsset.ArchiveURL,
                        CMSId = smoothStreamingVideoAsset.CMSId,
                        VodUrl =
                            smoothStreamingVideoAsset.VodUri != null
                                ? smoothStreamingVideoAsset.VodUri.OriginalString
                                : string.Empty,
                        AzureId = smoothStreamingVideoAsset.AzureId
                };

                smoothStreamingVideoAsset.AudioStreams.ForEach(
                    stream => item.AudioStreams.Add(new AudioStreamInfo { Name = stream.Name, IsStereo = stream.IsStereo }));

                return item;
            }

            if (smoothStreamingAudioAsset != null)
            {
                return new SmoothStreamingAudioItem
                {
                    Id = smoothStreamingAudioAsset.ProviderUri,
                    Created = smoothStreamingAudioAsset.Created,
                    Creator = smoothStreamingAudioAsset.Creator,
                    Modified = DateTime.Now,
                    ModifiedBy = smoothStreamingAudioAsset.ModifiedBy,

                    // Description =  audioAsset.Description,
                    Title = smoothStreamingAudioAsset.Title,
                    Duration = smoothStreamingAudioAsset.DurationInSeconds,
                    Resources = new ResourceCollection { new Resource { Ref = smoothStreamingAudioAsset.Source.ToString(), ResourceType = smoothStreamingAudioAsset.ResourceType.ToString() } },
                    IsStereo = smoothStreamingAudioAsset.IsStereo,
                    AudioStreamName = smoothStreamingAudioAsset.AudioStreamName,
                    ArchiveURL = smoothStreamingAudioAsset.ArchiveURL,
                    CMSId = smoothStreamingAudioAsset.CMSId,
                    AzureId = smoothStreamingAudioAsset.AzureId
                };
            }

            if (videoAsset != null)
            {
                return new VideoItem
                           {
                               Id = videoAsset.ProviderUri,
                               Created = videoAsset.Created,
                               Creator = videoAsset.Creator,
                               Modified = DateTime.Now,
                               ModifiedBy = videoAsset.ModifiedBy,

                               // Description =  videoAsset.Description,
                               Title = videoAsset.Title,
                               Duration = videoAsset.Duration.TotalSeconds,
                               FrameRate = (SmpteFrameRate)Enum.Parse(typeof(SmpteFrameRate), videoAsset.FrameRate.ToString(), true),
                               Height = videoAsset.Height,
                               Width = videoAsset.Width,
                               Resources = new ResourceCollection { new Resource { Ref = videoAsset.Source.ToString(), ResourceType = videoAsset.ResourceType.ToString() } },
                               ThumbnailSource = videoAsset.ThumbnailSource,
                               IsStereo = videoAsset.IsStereo,
                               ArchiveURL = videoAsset.ArchiveURL,
                               CMSId = videoAsset.CMSId,
                               AzureId = videoAsset.AzureId

                               // Metadata = videoAsset.Metadata
                           };
            }

            if (imageAsset != null)
            {
                return new ImageItem
                           {
                               Id = imageAsset.ProviderUri,
                               Created = imageAsset.Created,
                               Creator = imageAsset.Creator,
                               Modified = DateTime.Now,
                               ModifiedBy = imageAsset.ModifiedBy,

                               // Description =  imageAsset.Description,
                               Title = imageAsset.Title,
                               Height = imageAsset.Height,
                               Width = imageAsset.Width,
                               Resources = new ResourceCollection { new Resource { Ref = imageAsset.Source.ToString(), ResourceType = imageAsset.ResourceType.ToString() } },
                               CMSId = imageAsset.CMSId,
                               AzureId = imageAsset.AzureId

                               // Metadata = imageAsset.Metadata
                           };
            }

            if (audioAsset != null)
            {
                return new AudioItem
                           {
                               Id = audioAsset.ProviderUri,
                               Created = audioAsset.Created,
                               Creator = audioAsset.Creator,
                               Modified = DateTime.Now,
                               ModifiedBy = audioAsset.ModifiedBy,

                               // Description =  audioAsset.Description,
                               Title = audioAsset.Title,
                               Duration = audioAsset.DurationInSeconds,
                               Resources = new ResourceCollection { new Resource { Ref = audioAsset.Source.ToString(), ResourceType = audioAsset.ResourceType.ToString() } },
                               IsStereo = audioAsset.IsStereo,
                               ArchiveURL = audioAsset.ArchiveURL,
                               CMSId = audioAsset.CMSId,
                               AzureId = audioAsset.AzureId

                               // Metadata = audioAsset.Metadata
                           };
            }

            if (overlayAsset != null)
            {
                var item = new OverlayItem
                               {
                                   Id = overlayAsset.ProviderUri,
                                   Title = overlayAsset.Title,
                                   Created = overlayAsset.Created,
                                   Creator = overlayAsset.Creator,
                                   Modified = overlayAsset.Modified,
                                   ModifiedBy = overlayAsset.ModifiedBy,
                                   Metadata = overlayAsset.Metadata,
                                   X = overlayAsset.PositionX,
                                   Y = overlayAsset.PositionY,
                                   Width = overlayAsset.Width,
                                   Height = overlayAsset.Height,
                                   XamlTemplate = overlayAsset.XamlResource,
                                   Duration = overlayAsset.DurationInSeconds,
                                   CMSId = overlayAsset.CMSId,
                                   AzureId = overlayAsset.AzureId
                               };

                return item;
            }

            return new MediaItem { Id = asset.ProviderUri };
        }

        /// <summary>
        /// Converts the <see cref="TimelineElement"/> to data service title.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The <see cref="Title"/>.</returns>
        private static Title ConvertToDataServiceTitle(TimelineElement element)
        {
            Title title = ConvertToDataServiceTitle(element.Asset);

            title.TrackAnchor = new Anchor
                                    {
                                        Id = element.TrackAnchorUri,
                                        Modified = DateTime.Now,
                                        MarkIn = element.Position.TotalSeconds,
                                        MarkOut = element.OutPosition.TotalSeconds,
                                    };

            return title;
        }

        /// <summary>
        /// Converts the <see cref="Asset"/>to <see cref="Title"/>.
        /// </summary>
        /// <param name="asset">The <see cref="Asset"/>.</param>
        /// <returns>The <see cref="Title"/>.</returns>
        private static Title ConvertToDataServiceTitle(Asset asset)
        {
            TitleAsset titleAsset = asset as TitleAsset;

            if (titleAsset != null)
            {
                Title title = new Title
                                  {
                                      Id = titleAsset.ProviderUri,
                                      TitleTemplate = ConvertToDataServiceTitleTemplate(titleAsset.TitleTemplate),
                                  };

                title.TextBlockCollection = new TextBlockCollection();

                TextBlock mainTextBlock = new TextBlock
                                              {
                                                  Text = titleAsset.MainText
                                              };

                TextBlock subTextBlock = new TextBlock
                                             {
                                                 Text = titleAsset.SubText
                                             };

                title.TextBlockCollection.Add(mainTextBlock);
                title.TextBlockCollection.Add(subTextBlock);

                return title;
            }

            return null;
        }

        /// <summary>
        /// Converts the <see cref="RCE.Infrastructure.Models.TitleTemplate"/> to <see cref="TitleTemplate"/>.
        /// </summary>
        /// <param name="template">The <see cref="RCE.Infrastructure.Models.TitleTemplate"/>.</param>
        /// <returns>The <see cref="TitleTemplate"/>.</returns>
        private static TitleTemplate ConvertToDataServiceTitleTemplate(RCE.Infrastructure.Models.TitleTemplate template)
        {
            return new TitleTemplate
                       {
                           Id = template.ProviderUri,
                           Modified = DateTime.Now,
                           TemplateName = template.Title
                       };
        }

        /// <summary>
        /// Converts the <see cref="RCE.Infrastructure.Models.Comment"/> collection to <see cref="Comment"/> collection.
        /// </summary>
        /// <param name="comments">The comments.</param>
        /// <returns>The collection of <see cref="Comment"/>.</returns>
        private static List<Comment> ConvertToDataServiceComments(IEnumerable<RCE.Infrastructure.Models.Comment> comments)
        {
            List<Comment> dataComments = new List<Comment>();

            foreach (RCE.Infrastructure.Models.Comment comment in comments)
            {
                dataComments.Add(ConvertToDataServiceComment(comment));
            }

            return dataComments;
        }

        /// <summary>
        /// Converts the <see cref="RCE.Infrastructure.Models.Track"/> to <see cref="Title"/> list.
        /// </summary>
        /// <param name="titleTrack">The title track.</param>
        /// <returns>The collection of <see cref="Title"/>.</returns>
        private static List<Title> ConvertToDataServiceTitles(RCE.Infrastructure.Models.Track titleTrack)
        {
            List<Title> dataTitles = new List<Title>();

            foreach (TimelineElement element in titleTrack.Shots)
            {
                Title title = ConvertToDataServiceTitle(element);

                if (title != null)
                {
                    dataTitles.Add(title);
                }
            }

            return dataTitles;
        }

        /// <summary>
        /// Converts the <see cref="Comment"/> to <see cref="RCE.Services.Contracts.Comment"/>.
        /// </summary>
        /// <param name="comment">The <see cref="Comment"/>.</param>
        /// <returns>The <see cref="RCE.Services.Contracts.Comment"/>.</returns>
        private static Comment ConvertToDataServiceComment(RCE.Infrastructure.Models.Comment comment)
        {
            Comment dataComment;
            RCE.Infrastructure.Models.InkComment inkComment = comment as RCE.Infrastructure.Models.InkComment;

            if (inkComment != null)
            {
                string strokes = ConvertStrokesToXaml(inkComment);
                dataComment = new InkComment { Strokes = strokes };
            }
            else
            {
                dataComment = new Comment();
            }

            dataComment.Id = comment.ProviderUri;
            dataComment.Text = comment.Text;
            dataComment.Type = comment.CommentType.ToString();
            dataComment.Creator = comment.Creator;
            dataComment.Created = comment.Created;
            dataComment.Modified = DateTime.Now;
            dataComment.Modified = comment.Modified;
            dataComment.MarkIn = comment.MarkIn;
            dataComment.MarkOut = comment.MarkOut;

            return dataComment;
        }

        /// <summary>
        /// Converts to <see cref="IList{Track}"/>.
        /// </summary>
        /// <param name="timeline">The timeline.</param>
        /// <param name="comments">The comments.</param>
        /// <param name="frameRate">The frame rate.</param>
        /// <returns>The collection of <see cref="RCE.Infrastructure.Models.Track"/>.</returns>
        private static IList<RCE.Infrastructure.Models.Track> ConvertToTimeline(IEnumerable<Track> timeline, IEnumerable<RCE.Infrastructure.Models.Comment> comments, SmpteFrameRate frameRate)
        {
            IList<RCE.Infrastructure.Models.Track> tracks = new List<RCE.Infrastructure.Models.Track>();

            foreach (Track track in timeline)
            {
                if (track != null)
                {
                    Models.Track newTrack = new Models.Track
                                                                   {
                                                                       ProviderUri = track.Id,
                                                                       Created = track.Created,
                                                                       Creator = track.Creator,
                                                                       ModifiedBy = track.ModifiedBy,
                                                                       Modified = track.Modified,
                                                                       Number = track.Number,
                                                                       Balance = track.Balance,
                                                                       Volume = track.Volume,
                                                                   };

                    if (Enum.IsDefined(typeof(RCE.Infrastructure.Models.TrackType), track.TrackType))
                    {
                        newTrack.TrackType = (RCE.Infrastructure.Models.TrackType)Enum.Parse(typeof(RCE.Infrastructure.Models.TrackType), track.TrackType, true);
                    }

                    foreach (Shot shot in track.Shots)
                    {
                        if (shot != null)
                        {
                            TimelineElement timelineElement = ConvertToShot(shot, comments, frameRate);

                            if (timelineElement != null)
                            {
                                newTrack.Shots.Add(timelineElement);
                            }
                        }
                    }

                    tracks.Add(newTrack);
                }
            }

            return tracks;
        }

        /// <summary>
        /// Converts the <see cref="Shot"/> to <see cref="TimelineElement"/>.
        /// </summary>
        /// <param name="shot">The <see cref="Shot"/> value.</param>
        /// <param name="comments">The comments.</param>
        /// <param name="frameRate">The frame rate.</param>
        /// <returns>The <see cref="TimelineElement"/>.</returns>
        private static TimelineElement ConvertToShot(Shot shot, IEnumerable<RCE.Infrastructure.Models.Comment> comments, SmpteFrameRate frameRate)
        {
            TimelineElement timelineElement = new TimelineElement
                                                  {
                                                      ProviderUri = shot.Id,
                                                      Volume = (double)shot.Volume / 100,
                                                      Asset = ConvertToAsset(shot.Source),
                                                      Created = shot.Created,
                                                      Creator = shot.Creator,
                                                      Modified = shot.Modified,
                                                      ModifiedBy = shot.ModifiedBy,
                                                  };

            timelineElement.Position = shot.TrackAnchor.MarkIn.HasValue ? TimeCode.FromAbsoluteTime(shot.TrackAnchor.MarkIn.Value, frameRate) : TimeCode.FromAbsoluteTime(0, frameRate);
            timelineElement.InPosition = shot.SourceAnchor.MarkIn.HasValue ? TimeCode.FromAbsoluteTime(shot.SourceAnchor.MarkIn.Value, frameRate) : TimeCode.FromAbsoluteTime(0, frameRate);
            timelineElement.OutPosition = shot.SourceAnchor.MarkOut.HasValue ? TimeCode.FromAbsoluteTime(shot.SourceAnchor.MarkOut.Value, frameRate) : TimeCode.FromAbsoluteTime(0, frameRate);

            timelineElement.VolumeNodeCollection = ConvertToPoints(shot.VolumeNodeCollection, timelineElement.Duration.FrameRate);
            timelineElement.Balance = shot.Balance;

            // TODO: replace this with anchor models
            timelineElement.TrackAnchorUri = shot.TrackAnchor.Id;
            timelineElement.SourceAnchorUri = shot.SourceAnchor.Id;
            timelineElement.SelectedAudioStream = shot.SequenceAudioStream;
            timelineElement.SelectedVideoStream = shot.SequenceVideoStream;

            timelineElement.InTransition.Duration = shot.InTransition.Duration;
            timelineElement.OutTransition.Duration = shot.OutTransition.Duration;
            timelineElement.InTransition.Type = shot.InTransition.Type == TransitionType.Fade ? Models.TransitionType.Fade : Models.TransitionType.None;
            timelineElement.OutTransition.Type = shot.OutTransition.Type == TransitionType.Fade ? Models.TransitionType.Fade : Models.TransitionType.None;

            foreach (Comment dataComment in shot.Comments)
            {
                RCE.Infrastructure.Models.Comment comment = comments.Where(x => x.ProviderUri == dataComment.Id).FirstOrDefault();

                if (comment != null)
                {
                    timelineElement.Comments.Add(comment);
                }
            }

            return timelineElement;
        }

        private static List<VolumeLevelNode> ConvertToVolumeLevels(IList<Point> points, SmpteFrameRate frameRate)
        {
            List<VolumeLevelNode> volumeLevelNodes = new List<VolumeLevelNode>();
            foreach (Point point in points)
            {
                double volume = 1 - point.Y;
                volumeLevelNodes.Add(new VolumeLevelNode { Position = TimeCode.FromFrames((long)point.X, frameRate).TotalSeconds, Volume = volume });
            }

            return volumeLevelNodes;
        }

        private static List<Point> ConvertToPoints(IList<VolumeLevelNode> volumeLevelNodes, SmpteFrameRate frameRate)
        {
            List<Point> points = new List<Point>();
            foreach (VolumeLevelNode volumeLevelNode in volumeLevelNodes)
            {
                double y = 1 - volumeLevelNode.Volume;
                points.Add(new Point { X = TimeCode.FromSeconds(volumeLevelNode.Position, frameRate).TotalFrames, Y = y });
            }

            return points;
        }

        /// <summary>
        /// Converts the folder <see cref="Container"/> to <see cref="FolderAsset"/>.
        /// </summary>
        /// <param name="container">The folder container.</param>
        /// <returns>The <see cref="FolderAsset"/>.</returns>
        private static FolderAsset ConvertToFolderAsset(Container container)
        {
            return new FolderAsset
                       {
                           ProviderUri = container.Id,
                           Title = container.Title
                       };
        }

        /// <summary>
        /// Converts the <see cref="Item"/> to <see cref="Asset"/>.
        /// </summary>
        /// <param name="item">The instance of <see cref="Item"/>.</param>
        /// <returns>The instance of <see cref="Asset"/>.</returns>
        private static Asset ConvertToAsset(Item item)
        {
            SubClipItem subClipItem = item as SubClipItem;

            Item itemToCast = subClipItem != null ? subClipItem.RelatedItem : item;

            SmoothStreamingVideoItem smoothStreamingVideoItem = itemToCast as SmoothStreamingVideoItem;
            VideoItem videoItem = itemToCast as VideoItem;
            ImageItem imageItem = itemToCast as ImageItem;
            AudioItem audioItem = itemToCast as AudioItem;
            OverlayItem overlayItem = itemToCast as OverlayItem;

            SmoothStreamingAudioItem smoothStreamingAudioItem = itemToCast as SmoothStreamingAudioItem;

            if (smoothStreamingVideoItem != null)
            {
                return ConvertToAsset(smoothStreamingVideoItem);
            }

            if (smoothStreamingAudioItem != null)
            {
                return ConvertToAsset(smoothStreamingAudioItem);
            }

            if (videoItem != null)
            {
                Resource resource =
                    itemToCast.Resources.FirstOrDefault(x => x.ResourceType != null && Enum.IsDefined(typeof(ResourceType), x.ResourceType)
                        && !string.IsNullOrEmpty(x.Ref));

                if (resource != null && resource.Ref != null && (resource.ResourceType == "SmoothStream" || resource.ResourceType == "LiveSmoothStream"))
                {
                    Uri uri;

                    if (!Uri.TryCreate(resource.Ref, UriKind.Absolute, out uri))
                    {
                        string uriString = string.Concat(ContentNetworkPrefix, resource.Ref);
                        uri = new Uri(uriString, UriKind.RelativeOrAbsolute);
                    }

                    bool smoothResource = UtilityHelper.IsLiveAdaptiveStreaming(uri) || UtilityHelper.IsAdaptiveStreaming(uri);
                    
                    if (smoothResource)
                    {
                        return ConvertToSmoothStreamingVideoAsset(videoItem);
                    }
                }
                
                return ConvertToAsset(videoItem);
            }

            if (imageItem != null)
            {
                return ConvertToAsset(imageItem);
            }

            if (audioItem != null)
            {
                return ConvertToAsset(audioItem);
            }

            if (overlayItem != null)
            {
                return ConvertToAsset(overlayItem);
            }

            return null;
        }

        /// <summary>
        /// Converts the <see cref="SmoothStreamingVideoItem"/> to <see cref="SmoothStreamingVideoAsset"/>.
        /// </summary>
        /// <param name="item">The <see cref="SmoothStreamingVideoItem"/>.</param>
        /// <returns>The <see cref="SmoothStreamingVideoAsset"/>.</returns>
        private static SmoothStreamingVideoAsset ConvertToAsset(SmoothStreamingVideoItem item)
        {
            VideoAsset videoAsset = ConvertToAsset((VideoItem)item);

            SmoothStreamingVideoAsset result = new SmoothStreamingVideoAsset
                {
                    ProviderUri = videoAsset.ProviderUri,
                    Title = videoAsset.Title,
                    Width = videoAsset.Width,
                    Height = videoAsset.Height,
                    Created = videoAsset.Created,
                    Creator = videoAsset.Creator,
                    Modified = videoAsset.Modified,
                    ModifiedBy = videoAsset.ModifiedBy,
                    Metadata = videoAsset.Metadata,
                    ThumbnailSource = videoAsset.ThumbnailSource,
                    Duration = videoAsset.Duration,
                    FrameRate = videoAsset.FrameRate,
                    ResourceType = videoAsset.ResourceType,
                    Source = videoAsset.Source,
                    StartPosition = item.StartPosition,
                    DataStreams = item.DataStreams,
                    ExternalManifests = item.ExternalManifests,
                    IsStereo = item.IsStereo,
                    VideoStreams = item.VideoStreams,
                    ArchiveURL = item.ArchiveURL,
                    CMSId = item.CMSId,
                    VodUri = !string.IsNullOrEmpty(item.VodUrl) ? new Uri(item.VodUrl) : null,
                    AzureId = item.AzureId
                };

            item.AudioStreams.ForEach(info => result.AudioStreams.Add(new AudioStream(info.Name, info.IsStereo)));

            return result;
        }

        /// <summary>
        /// Converts the <see cref="VideoItem"/> to <see cref="SmoothStreamingVideoAsset"/>.
        /// </summary>
        /// <param name="item">The <see cref="SmoothStreamingVideoItem"/>.</param>
        /// <returns>The <see cref="SmoothStreamingVideoAsset"/>.</returns>
        private static SmoothStreamingVideoAsset ConvertToSmoothStreamingVideoAsset(VideoItem item)
        {
            VideoAsset videoAsset = ConvertToAsset(item);

            SmoothStreamingVideoAsset result = new SmoothStreamingVideoAsset
            {
                ProviderUri = videoAsset.ProviderUri,
                Title = videoAsset.Title,
                Width = videoAsset.Width,
                Height = videoAsset.Height,
                Created = videoAsset.Created,
                Creator = videoAsset.Creator,
                Modified = videoAsset.Modified,
                ModifiedBy = videoAsset.ModifiedBy,
                Metadata = videoAsset.Metadata,
                ThumbnailSource = videoAsset.ThumbnailSource,
                Duration = videoAsset.Duration,
                FrameRate = videoAsset.FrameRate,
                ResourceType = videoAsset.ResourceType,
                Source = videoAsset.Source,
                StartPosition = 0,
                DataStreams = new List<string>(),
                ExternalManifests = new List<Uri>(),
                IsStereo = videoAsset.IsStereo,
                CMSId = item.CMSId,
                AzureId = item.AzureId
            };

            return result;
        }

        /// <summary>
        /// Converts the <see cref="VideoItem"/> to <see cref="VideoAsset"/>.
        /// </summary>
        /// <param name="item">The <see cref="VideoItem"/>.</param>
        /// <returns>The <see cref="VideoAsset"/>.</returns>
        private static VideoAsset ConvertToAsset(VideoItem item)
        {
            VideoAsset result = new VideoAsset
                                    {
                                        ProviderUri = item.Id,
                                        Title = item.Title,
                                        Width = item.Width,
                                        Height = item.Height,
                                        Created = item.Created,
                                        Creator = item.Creator,
                                        Modified = item.Modified,
                                        ModifiedBy = item.ModifiedBy,
                                        Metadata = item.Metadata,
                                        ThumbnailSource = item.ThumbnailSource,
                                        IsStereo = item.IsStereo,
                                        ArchiveURL = item.ArchiveURL,
                                        CMSId = item.CMSId,
                                        AzureId = item.AzureId
                                    };

            SmpteFrameRate frameRate = (SmpteFrameRate)Enum.Parse(typeof(SmpteFrameRate), item.FrameRate.ToString(), false);

            if (item.Duration.HasValue)
            {
                result.Duration = TimeCode.FromSeconds(item.Duration.Value, frameRate);
            }

            Resource resource =
                item.Resources.FirstOrDefault(
                    x =>
                    x.ResourceType != null
                    && Enum.IsDefined(typeof(ResourceType), x.ResourceType)
                    && !string.IsNullOrEmpty(x.Ref));

            if (resource != null)
            {
                result.ResourceType = (ResourceType)Enum.Parse(typeof(ResourceType), resource.ResourceType, true);

                Uri uri;

                if (Uri.TryCreate(resource.Ref, UriKind.Absolute, out uri))
                {
                    result.Source = uri;
                }
                else
                {
                    string uriString = string.Concat(ContentNetworkPrefix, resource.Ref);
                    result.Source = new Uri(uriString, UriKind.RelativeOrAbsolute);
                }

                result.FrameRate = frameRate;
            }

            return result;
        }

        /// <summary>
        /// Converts the <see cref="AudioItem"/> to <see cref="AudioAsset"/>.
        /// </summary>
        /// <param name="item">The <see cref="AudioItem"/>.</param>
        /// <returns>The <see cref="AudioAsset"/>.</returns>
        private static AudioAsset ConvertToAsset(AudioItem item)
        {
            AudioAsset result = new AudioAsset
                                    {
                                        ProviderUri = item.Id,
                                        Title = item.Title,
                                        Created = item.Created,
                                        Creator = item.Creator,
                                        Modified = item.Modified,
                                        ModifiedBy = item.ModifiedBy,
                                        Metadata = item.Metadata,
                                        IsStereo = item.IsStereo,
                                        ArchiveURL = item.ArchiveURL,
                                        CMSId = item.CMSId,
                                        AzureId = item.AzureId
                                    };

            if (item.Duration.HasValue)
            {
                result.DurationInSeconds = item.Duration.GetValueOrDefault();
            }

            Resource resource = item.Resources.FirstOrDefault(
                x =>
                x.ResourceType != null
                && Enum.IsDefined(typeof(ResourceType), x.ResourceType)
                && !string.IsNullOrEmpty(x.Ref));

            if (resource != null)
            {
                result.ResourceType = (ResourceType)Enum.Parse(typeof(ResourceType), resource.ResourceType, true);

                Uri uri;

                if (Uri.TryCreate(resource.Ref, UriKind.Absolute, out uri))
                {
                    result.Source = uri;
                }
                else
                {
                    string uriString = string.Concat(ContentNetworkPrefix, resource.Ref);
                    result.Source = new Uri(uriString);
                }
            }

            return result;
        }

        private static OverlayAsset ConvertToAsset(OverlayItem item)
        {
            var result = new OverlayAsset
                             {
                                 ProviderUri = item.Id,
                                 XamlResource = item.XamlTemplate,
                                 Title = item.Title,
                                 Created = item.Created,
                                 Creator = item.Creator,
                                 Modified = item.Modified,
                                 ModifiedBy = item.ModifiedBy,
                                 Metadata = item.Metadata,
                                 PositionX = item.X,
                                 PositionY = item.Y,
                                 Width = item.Width,
                                 Height = item.Height,
                                 DurationInSeconds = item.Duration,
                                 CMSId = item.CMSId,
                                 AzureId = item.AzureId
                             };

            return result;
        }

        /// <summary>
        /// Converts the <see cref="SmoothStreamingAudioItem"/> to <see cref="SmoothStreamingAudioAsset"/>.
        /// </summary>
        /// <param name="item">The <see cref="SmoothStreamingAudioItem"/>.</param>
        /// <returns>The <see cref="SmoothStreamingAudioAsset"/>.</returns>
        private static SmoothStreamingAudioAsset ConvertToAsset(SmoothStreamingAudioItem item)
        {
            SmoothStreamingAudioAsset result = new SmoothStreamingAudioAsset
            {
                ProviderUri = item.Id,
                Title = item.Title,
                Created = item.Created,
                Creator = item.Creator,
                Modified = item.Modified,
                ModifiedBy = item.ModifiedBy,
                Metadata = item.Metadata,
                IsStereo = item.IsStereo,
                AudioStreamName = item.AudioStreamName,
                ArchiveURL = item.ArchiveURL,
                CMSId = item.CMSId,
                AzureId = item.AzureId
            };

            if (item.Duration.HasValue)
            {
                result.DurationInSeconds = item.Duration.GetValueOrDefault();
            }

            Resource resource = item.Resources.FirstOrDefault(
                x =>
                x.ResourceType != null
                && Enum.IsDefined(typeof(ResourceType), x.ResourceType)
                && !string.IsNullOrEmpty(x.Ref));

            if (resource != null)
            {
                result.ResourceType = (ResourceType)Enum.Parse(typeof(ResourceType), resource.ResourceType, true);

                Uri uri;

                if (Uri.TryCreate(resource.Ref, UriKind.Absolute, out uri))
                {
                    result.Source = uri;
                }
                else
                {
                    string uriString = string.Concat(ContentNetworkPrefix, resource.Ref);
                    result.Source = new Uri(uriString);
                }
            }

            return result;
        }

        /// <summary>
        /// Converts the <see cref="ImageItem"/> to <see cref="ImageAsset"/>.
        /// </summary>
        /// <param name="item">The <see cref="ImageItem"/>.</param>
        /// <returns>The <see cref="ImageAsset"/>.</returns>
        private static ImageAsset ConvertToAsset(ImageItem item)
        {
            ImageAsset result = new ImageAsset
                                    {
                                        ProviderUri = item.Id,
                                        Title = item.Title,
                                        Width = item.Width,
                                        Height = item.Height,
                                        Created = item.Created,
                                        Creator = item.Creator,
                                        Modified = item.Modified,
                                        ModifiedBy = item.ModifiedBy,
                                        Metadata = item.Metadata,
                                        CMSId = item.CMSId,
                                        AzureId = item.AzureId
                                    };

            Resource resource = item.Resources.FirstOrDefault(
                x =>
                x.ResourceType != null
                && Enum.IsDefined(typeof(ResourceType), x.ResourceType)
                && !string.IsNullOrEmpty(x.Ref));

            if (resource != null)
            {
                result.ResourceType = (ResourceType)Enum.Parse(typeof(ResourceType), resource.ResourceType, true);

                Uri uri;

                if (Uri.TryCreate(resource.Ref, UriKind.Absolute, out uri))
                {
                    result.Source = uri;
                }
                else
                {
                    string uriString = string.Concat(ContentNetworkPrefix, resource.Ref);
                    result.Source = new Uri(uriString);
                }
            }

            return result;
        }

        /// <summary>
        /// Converts the <see cref="Comment"/> to <see cref="RCE.Infrastructure.Models.Comment"/>.
        /// </summary>
        /// <param name="commentItem">The <see cref="Comment"/>.</param>
        /// <returns>The <see cref="RCE.Infrastructure.Models.Comment"/>.</returns>
        private static RCE.Infrastructure.Models.Comment ConvertToComment(Comment commentItem)
        {
            RCE.Infrastructure.Models.Comment comment;
            CommentType commentType = (CommentType)Enum.Parse(typeof(CommentType), commentItem.Type, true);
            InkComment inkComment = commentItem as InkComment;

            if (inkComment != null)
            {
                StrokeCollection strokes = ConvertXamlToStrokes(inkComment.Strokes);
                comment = new RCE.Infrastructure.Models.InkComment { InkCommentStrokes = strokes, Text = inkComment.Text };
            }
            else
            {
                comment = new RCE.Infrastructure.Models.Comment { Text = commentItem.Text };
            }

            comment.ProviderUri = commentItem.Id;
            comment.Creator = commentItem.Creator;
            comment.CommentType = commentType;
            comment.Created = commentItem.Created;
            comment.Modified = commentItem.Modified;
            comment.MarkIn = commentItem.MarkIn;
            comment.MarkOut = commentItem.MarkOut;

            return comment;
        }

        /// <summary>
        /// Converts the xaml to strokes collection.
        /// </summary>
        /// <param name="xaml">The xaml of stroke points.</param>
        /// <returns>The <see cref="StrokeCollection"/>.</returns>
        private static StrokeCollection ConvertXamlToStrokes(string xaml)
        {
            try
            {
                return XamlReader.Load(xaml) as StrokeCollection;
            }
            catch (ArgumentNullException)
            {
                return new StrokeCollection();
            }
            catch (InvalidOperationException)
            {
                return new StrokeCollection();
            }
        }

        /// <summary>
        /// Converts the strokes to xaml.
        /// </summary>
        /// <param name="comment">The <see cref="RCE.Infrastructure.Models.InkComment"/>.</param>
        /// <returns>The xaml of stroke collection.</returns>
        private static string ConvertStrokesToXaml(RCE.Infrastructure.Models.InkComment comment)
        {
            StringBuilder builder = new StringBuilder();

            XmlWriter writer = XmlWriter.Create(builder);

            if (writer != null)
            {
                writer.WriteStartElement("StrokeCollection", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");

                foreach (Stroke stroke in comment.InkCommentStrokes)
                {
                    writer.WriteStartElement("Stroke");
                    writer.WriteStartElement("Stroke.DrawingAttributes");
                    writer.WriteStartElement("DrawingAttributes");
                    writer.WriteAttributeString("Width", stroke.DrawingAttributes.Width.ToString(CultureInfo.InvariantCulture));
                    writer.WriteAttributeString("Height", stroke.DrawingAttributes.Height.ToString(CultureInfo.InvariantCulture));
                    writer.WriteAttributeString("Color", string.Format(CultureInfo.InvariantCulture, "#{0:X2}{1:X2}{2:X2}{3:X2}", stroke.DrawingAttributes.Color.A, stroke.DrawingAttributes.Color.R, stroke.DrawingAttributes.Color.G, stroke.DrawingAttributes.Color.B));
                    writer.WriteAttributeString("OutlineColor", string.Format(CultureInfo.InvariantCulture, "#{0:X2}{1:X2}{2:X2}{3:X2}", stroke.DrawingAttributes.OutlineColor.A, stroke.DrawingAttributes.OutlineColor.R, stroke.DrawingAttributes.OutlineColor.G, stroke.DrawingAttributes.OutlineColor.B));
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.WriteStartElement("Stroke.StylusPoints");
                    writer.WriteStartElement("StylusPointCollection");

                    foreach (StylusPoint sp in stroke.StylusPoints)
                    {
                        writer.WriteStartElement("StylusPoint");
                        writer.WriteAttributeString("X", sp.X.ToString(CultureInfo.InvariantCulture));
                        writer.WriteAttributeString("Y", sp.Y.ToString(CultureInfo.InvariantCulture));
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.Flush();
            }

            return builder.ToString();
        }

        /// <summary>
        /// Creates the ids.
        /// </summary>
        /// <param name="project">The project.</param>
        private static void CreateIds(RCE.Infrastructure.Models.Project project)
        {
            if (project.ProviderUri == null)
            {
                project.ProviderUri = CreateId(typeof(RCE.Services.Contracts.Project));
            }

            if (project.MediaBin.ProviderUri == null)
            {
                project.MediaBin.ProviderUri = CreateId(typeof(RCE.Services.Contracts.MediaBin));

                CreateFolderAssetsId(project.MediaBin.Assets);
            }

            foreach (RCE.Infrastructure.Models.Comment comment in project.Comments)
            {
                if (comment.ProviderUri == null)
                {
                    comment.ProviderUri = CreateId(typeof(RCE.Services.Contracts.Comment));
                }
            }

            foreach (Sequence timeline in project.Timelines)
            {
                foreach (RCE.Infrastructure.Models.Track track in timeline.Tracks)
                {
                    if (track.ProviderUri == null)
                    {
                        track.ProviderUri = CreateId(typeof(RCE.Services.Contracts.Track));
                    }

                    foreach (TimelineElement element in track.Shots)
                    {
                        if (element.ProviderUri == null)
                        {
                            element.ProviderUri = CreateId(typeof(RCE.Services.Contracts.Shot));

                            if (element.Asset is TitleAsset)
                            {
                                element.Asset.ProviderUri = CreateId(typeof(RCE.Services.Contracts.Title));
                            }
                        }

                        if (element.SourceAnchorUri == null)
                        {
                            element.SourceAnchorUri = CreateId(typeof(RCE.Services.Contracts.Anchor));
                        }

                        if (element.TrackAnchorUri == null)
                        {
                            element.TrackAnchorUri = CreateId(typeof(RCE.Services.Contracts.Anchor));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new id for the given <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type to create the id.</param>
        /// <returns>A new <seealso cref="Uri"/> that representes a new Id.</returns>
        private static Uri CreateId(Type type)
        {
            if (mappings.ContainsKey(type.Name))
            {
                string value = mappings[type.Name];
                return CreateUri(value);
            }

            throw new InvalidOperationException();
        }

        /// <summary>
        /// Creates the folder assets id.
        /// </summary>
        /// <param name="assets">The assets.</param>
        private static void CreateFolderAssetsId(IEnumerable<Asset> assets)
        {
            foreach (Asset asset in assets)
            {
                FolderAsset folderAsset = asset as FolderAsset;

                if (folderAsset != null)
                {
                    if (folderAsset.ProviderUri == null)
                    {
                        folderAsset.ProviderUri = CreateId(typeof(RCE.Services.Contracts.Container));
                    }

                    if (folderAsset.Assets.Count > 0)
                    {
                        CreateFolderAssetsId(folderAsset.Assets);
                    }
                }
            }
        }

        /// <summary>
        /// Creates the URI for the given item.
        /// </summary>
        /// <param name="item">The item name.</param>
        /// <returns><see cref="Uri"/> for the given item.</returns>
        private static Uri CreateUri(string item)
        {
            string uriString = string.Format(CultureInfo.InvariantCulture, "http://rce/samples/2.0/{0}/{1}", item, Guid.NewGuid().ToString("D"));
            return new Uri(uriString);
        }

        private static Asset ConvertToVideoAssetInOut(SubClipItem subClipItem)
        {
            Asset relatedAsset = ConvertToAsset(subClipItem.RelatedItem);
            var videoAssetInOut = new VideoAssetInOut((VideoAsset)relatedAsset)
            {
                CMSId = subClipItem.CMSId,
                AzureId = subClipItem.AzureId,
                Title = subClipItem.Title,
                InPosition = subClipItem.InPosition,
                OutPosition = subClipItem.OutPosition,
                SequenceVideoCamera = subClipItem.SequenceVideoStream,
            };

            foreach (var audioStreamInfo in subClipItem.SequenceAudioStreams)
            {
                videoAssetInOut.SequenceAudioStreams.Add(new AudioStream(audioStreamInfo.Name, audioStreamInfo.IsStereo));
            }

            return videoAssetInOut;
        }
    }
}