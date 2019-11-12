// <copyright file="DemoDataProvider.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: DemoDataProvider.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Data.Demo
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Services.Contracts;
    using SMPTETimecode;

    /// <summary>
    /// Provides the implementation for <see cref="IDataProvider"/> and <see cref="IAssetsDataProvider"/> that will retrieve data statically.
    /// </summary>
    public class DemoDataProvider : IDataProvider, IAssetsDataProvider
    {
        /// <summary>
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
        /// The container for library module.
        /// </summary>
        private readonly Container libraryContainer;

        /// <summary>
        /// To have the details of the project.
        /// </summary>
        private readonly Project sampleProject;

        /// <summary>
        /// To have all the details of the media bin for the current project.
        /// </summary>
        private readonly MediaBin sampleMediaBin;

        /// <summary>
        /// Collection of comments in the project.
        /// </summary>
        private readonly CommentCollection sampleComments;

        /// <summary>
        /// Collection of the titles.
        /// </summary>
        private readonly TitleCollection sampleTitles;

        /// <summary>
        /// Mediabin Uri.
        /// </summary>
        private readonly Uri sampleMediaBinUri;

        /// <summary>
        /// The collection of the tracks.
        /// </summary>
        private readonly TrackCollection sampleTracks;

        /// <summary>
        /// Collection of the Title templates.
        /// </summary>
        private readonly TitleTemplateCollection sampleTitleTemplates;

        /// <summary>
        /// The list of projects.
        /// </summary>
        private readonly ProjectCollection sampleProjects;

        /// <summary>
        /// Initializes a new instance of the <see cref="DemoDataProvider"/> class.
        /// </summary>
        public DemoDataProvider()
        {
            this.sampleMediaBinUri =
                new Uri("http://rce.litwareinc.com/samples/2.0/MediaBin/11111111-1111-1111-1111-111111111111");

            this.libraryContainer = new Container();

            // Adding sample videos;
            this.libraryContainer.Items.Add(
                CreateSmoothVideoItem(
                    CreateUri(VideoItems),
                    "Elephants Dream",
                    653.791678,
                    SmpteFrameRate.Smpte2997NonDrop,
                    "http://video3.smoothhd.com/ondemand/ElephantsDream.ism/Manifest",
                    1280,
                    720,
                    "SmoothStream"));

            this.libraryContainer.Items.Add(
                CreateImageItem(
                    "Francis & ESQL", "http://i.msdn.microsoft.com/bb969103.how-we-do-it(en-us,MSDN.10).png", 300, 200));

            this.libraryContainer.Items.Add(
                CreateSmoothVideoItem(
                    CreateUri(VideoItems),
                    "NBA",
                    408,
                    SmpteFrameRate.Smpte2997NonDrop,
                    "http://video3.smoothhd.com/ondemand/NBA.ism/Manifest",
                    1280,
                    720,
                    "SmoothStream"));
            this.libraryContainer.Items.Add(
                CreateImageItem(
                    "p&p Logo", "http://i.msdn.microsoft.com/ms998572.pandp-logo-txt-2009(en-us,MSDN.10).png", 250, 68));

            this.libraryContainer.Items.Add(
                CreateSmoothVideoItem(
                    CreateUri(VideoItems),
                    "Basic Curveball",
                    62.1621042,
                    SmpteFrameRate.Smpte2997Drop,
                    "http://video3.smoothhd.com/ondemand/eHow_Baseball.ism/Manifest",
                    1280,
                    720,
                    "SmoothStream"));
            this.libraryContainer.Items.Add(
                CreateAudioItem(
                    "Glenn Block on Prism",
                    2286,
                    "http://herdingcode.com/wp-content/uploads/HerdingCode-0011-Glenn-Block-Part-1.mp3"));

            this.libraryContainer.Items.Add(
                CreateSmoothVideoItem(
                    CreateUri(VideoItems),
                    "Big Buck Bunny",
                    596.458333,
                    SmpteFrameRate.Smpte2997NonDrop,
                    "http://video3.smoothhd.com/ondemand/Big%20Buck%20Bunny%20Adaptive.ism/Manifest",
                    1280,
                    720,
                    "SmoothStream"));

            this.libraryContainer.Items.Add(
                CreateImageItem(
                    "XBox Live",
                    "http://www.xbox.com/NR/rdonlyres/09842316-4F8D-46E5-A504-BE7AB207CE69/0/xbox_white_1280x1204.jpg",
                    1280,
                    1024));
            this.libraryContainer.Items.Add(
                CreateImageItem(
                    "PDC 2009 640x474", "http://blogs.southworks.net/srenzi/files/2009/11/image3.png", 640, 474));
            this.libraryContainer.Items.Add(
                CreateImageItem(
                    "Windows 63 x 63",
                    "http://i.microsoft.com/global/En/us/PublishingImages/SLWindowPane/Windows_T.png",
                    63,
                    63));

            // Adding sample comments
            this.sampleComments = new CommentCollection();
            this.sampleComments.Add(
                CreateComment(
                    "David", Properties.Resources.Comment1Text, "Global", DateTime.Today, null, null, DateTime.Today.AddDays(-15)));
            this.sampleComments.Add(
                CreateComment(
                    "Jay",
                    Properties.Resources.Comment2Text,
                    "Timeline",
                    new DateTime(2008, 2, 10),
                    1400,
                    1520,
                    DateTime.Today.AddDays(-15)));
            this.sampleComments.Add(
                CreateComment(
                    "David",
                    Properties.Resources.Comment3Text,
                    "Shot",
                    new DateTime(2008, 2, 10),
                    980,
                    1060,
                    DateTime.Today.AddDays(-15)));

            // Adding sample media bin
            this.sampleMediaBin = new MediaBin { Id = this.sampleMediaBinUri };
            this.sampleMediaBin.Items.Add(this.libraryContainer.Items[0]);
            this.sampleMediaBin.Items.Add(this.libraryContainer.Items[1]);
            this.sampleMediaBin.Items.Add(this.libraryContainer.Items[2]);

            // Adding sample tracks
            this.sampleTracks = new TrackCollection();
            this.sampleTracks.Add(CreateTrack(TrackType.Visual));

            this.sampleTracks[0].Shots.Add(
                CreateShot(
                    this.sampleMediaBin.Items[0],
                    180,
                    ((VideoItem)this.sampleMediaBin.Items[0]).Duration.Value - 180,
                    800,
                    100));
            this.sampleTracks[0].Shots.Add(
                CreateShot(
                    this.sampleMediaBin.Items[0],
                    0,
                    ((VideoItem)this.sampleMediaBin.Items[0]).Duration.Value - 60,
                    0,
                    65));
            this.sampleTracks[0].Shots[0].Comments.Add(this.sampleComments[2]);

            this.sampleTracks.Add(CreateTrack(TrackType.Audio));

            // this.sampleTracks[1].Shots.Add(CreateShot(this.sampleMediaBin.Items[1], 0, ((AudioItem)this.sampleMediaBin.Items[1]).Duration.Value, 60, 80));

            // Adding sample title templates
            this.sampleTitleTemplates = new TitleTemplateCollection();
            this.sampleTitleTemplates.Add(CreateTitleTemplate("FadeCenter"));
            this.sampleTitleTemplates.Add(CreateTitleTemplate("Spinner"));
            this.sampleTitleTemplates.Add(CreateTitleTemplate("ScrollingCenter"));
            this.sampleTitleTemplates.Add(CreateTitleTemplate("ZoomCenter"));

            // Adding sample titles
            this.sampleTitles = new TitleCollection();
            this.sampleTitles.Add(CreateTitle(this.sampleTitleTemplates[0], 1200, 600));
            this.sampleTitles.Add(CreateTitle(this.sampleTitleTemplates[0], 2200, 400));

            // Adding sample project
            this.sampleProject = new Project
                {
                    Id = new Uri("http://rce.litwareinc.com/samples/2.0/Projects/11111111-1111-1111-1111-111111111111"),
                    Title = "Sample Project",
                    Creator = @"RCE\ejadib",
                    Created = new DateTime(2009, 1, 1),
                    RippleMode = false,
                    AutoSaveInterval = 10,
                    SmpteFrameRate = SmpteFrameRate.Smpte2997NonDrop.ToString(),
                    StartTimeCode = 1802,
                    MediaBin = this.sampleMediaBin,
                    Comments = 
                               {
                                   this.sampleComments[0], this.sampleComments[1], this.sampleComments[2] 
                               },
                    Sequences =
                        new SequenceCollection 
                            {
                                new Sequence { Tracks = { this.sampleTracks[0], this.sampleTracks[1] } } 
                            },
                    Titles = 
                             {
                                 this.sampleTitles[0], this.sampleTitles[1] 
                             }
                };

            this.sampleProjects = new ProjectCollection();
            this.sampleProjects.Add(this.sampleProject);

            this.sampleProjects.Add(
                new Project
                    {
                        Id =
                            new Uri(
                            "http://rce.litwareinc.com/samples/2.0/Projects/CC8191B0-64FF-438b-89D1-C9464B310FB7"),
                        Title = "WithStartTimeCode1802",
                        Creator = @"RCE\ejadib",
                        Created = new DateTime(2009, 3, 1),
                        RippleMode = false,
                        AutoSaveInterval = 10,
                        SmpteFrameRate = SmpteFrameRate.Smpte25.ToString(),
                        StartTimeCode = 1802,
                        MediaBin = this.sampleMediaBin,
                        Comments = 
                            {
                                this.sampleComments[0], this.sampleComments[1], this.sampleComments[2] 
                            },
                        Sequences =
                       new SequenceCollection 
                            {
                                new Sequence { Tracks = { this.sampleTracks[0], this.sampleTracks[1] } } 
                            },
                        Duration = 200
                    });

            this.sampleProjects.Add(
                new Project
                    {
                        Id =
                            new Uri(
                            "http://rce.litwareinc.com/samples/2.0/Projects/0F20DC18-2164-4683-AB65-51715A5DD7D3"),
                        Title = "WithStartTimeCode600",
                        Creator = @"RCE\ejadib",
                        Created = new DateTime(2009, 2, 6),
                        RippleMode = false,
                        AutoSaveInterval = 10,
                        SmpteFrameRate = SmpteFrameRate.Smpte2997Drop.ToString(),
                        StartTimeCode = 600,
                        MediaBin = this.sampleMediaBin,
                        Comments = 
                            {
                                this.sampleComments[0], this.sampleComments[1], this.sampleComments[2] 
                            },
                        Sequences =
                            new SequenceCollection 
                            {
                                new Sequence { Tracks = { this.sampleTracks[0], this.sampleTracks[1] } } 
                            },
                        Duration = 400
                    });

            this.sampleProjects.Add(
                new Project
                    {
                        Id =
                            new Uri(
                            "http://rce.litwareinc.com/samples/2.0/Projects/0B05A8F0-1D3C-44bb-AD71-1387D2A93414"),
                        Title = "No Titles",
                        Creator = @"RCE\ejadib",
                        Created = new DateTime(2009, 3, 25),
                        RippleMode = true,
                        AutoSaveInterval = 10,
                        SmpteFrameRate = SmpteFrameRate.Smpte2997NonDrop.ToString(),
                        StartTimeCode = 0,
                        MediaBin = this.sampleMediaBin,
                        Comments = 
                            {
                                this.sampleComments[0], this.sampleComments[1], this.sampleComments[2] 
                            },
                        Sequences =
                            new SequenceCollection 
                            {
                                new Sequence { Tracks = { this.sampleTracks[0], this.sampleTracks[1] } } 
                            },
                        Duration = 600
                    });
        }

        /// <summary>
        /// Returns back given no. of items from the library.
        /// </summary>
        /// <param name="maxNumberOfItems">Maximum no. of records in the result.</param>
        /// <returns>A <see cref="Container"/> with the items.</returns>
        public virtual Container LoadLibrary(int maxNumberOfItems)
        {
            if (maxNumberOfItems > 0)
            {
                Container filteredContainer = new Container();

                this.libraryContainer.Containers.ToList().ForEach(filteredContainer.Containers.Add);
                this.libraryContainer.Items.Take(maxNumberOfItems).ToList().ForEach(filteredContainer.Items.Add);

                return filteredContainer;
            }
            else
            {
                if (this.libraryContainer.Id == null)
                {
                    this.libraryContainer.Id = new Uri("http://democontainer/");
                }

                return this.libraryContainer;
            }
        }

        /// <summary>
        /// Returns back all of the items that are contained in the library filtering them using the filter provided.
        /// </summary>
        /// <param name="filter">The filter used to get the items.</param>
        /// <param name="maxNumberOfItems">Maximum no. of records in the result.</param>
        /// <returns>A <see cref="Container"/> with the items.</returns>
        public Container LoadLibrary(string filter, int maxNumberOfItems)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                Container filteredContainer = new Container();

                if (maxNumberOfItems > 0)
                {
                    this.libraryContainer.Items.Where(
                        x => x.Title.StartsWith(filter, StringComparison.OrdinalIgnoreCase)).Take(maxNumberOfItems)
                        .ToList().ForEach(filteredContainer.Items.Add);

                    if (maxNumberOfItems > filteredContainer.Items.Count)
                    {
                        foreach (Container childContainer in this.libraryContainer.Containers)
                        {
                            childContainer.Items.Where(
                                x => x.Title.StartsWith(filter, StringComparison.OrdinalIgnoreCase)).Take(
                                maxNumberOfItems - filteredContainer.Items.Count)
                                .ToList().ForEach(filteredContainer.Items.Add);
                        }
                    }
                }
                else
                {
                    this.libraryContainer.Items.Where(
                        x => x.Title.StartsWith(filter, StringComparison.OrdinalIgnoreCase)).ToList().ForEach(
                        filteredContainer.Items.Add);

                    foreach (Container childContainer in this.libraryContainer.Containers)
                    {
                        childContainer.Items.Where(
                            x => x.Title.StartsWith(filter, StringComparison.OrdinalIgnoreCase)).ToList()
                            .ForEach(filteredContainer.Items.Add);
                    }
                }

                return filteredContainer;
            }

            return this.LoadLibrary(maxNumberOfItems);
        }

        /// <summary>
        /// Returns back given no. of items from the library.
        /// </summary>
        /// <param name="libraryId">The <see cref="Uri"/> of the container to load from the library.</param>
        /// <param name="maxNumberOfItems">Maximum no. of records in the result.</param>
        /// <returns>A <see cref="Container"/> with the items.</returns>
        public Container LoadLibraryById(Uri libraryId, int maxNumberOfItems)
        {
            // Add here the logic to retrieve an specific logic
            return new Container();
        }

        /// <summary>
        /// Returns back given no. of items from the library.
        /// </summary>
        /// <param name="libraryId">The <see cref="Uri"/> of the container to load from the library.</param>
        /// <param name="filter">The filter used to get the items.</param>
        /// <param name="maxNumberOfItems">Maximum no. of records in the result.</param>
        /// <returns>A <see cref="Container"/> with the items.</returns>
        public Container LoadLibraryById(Uri libraryId, string filter, int maxNumberOfItems)
        {
            // Add here the logic to retrieve an specific logic
            return new Container();
        }

        /// <summary>
        /// Loads the MediaBin <see cref="Container" /> with a <see cref="ItemCollection" /> that contains the items in the media bin.
        /// </summary>
        /// <param name="mediaBinUri">The <see cref="Uri"/> of the media bin to load.</param>
        /// <returns>A <see cref="Container"/> with the media elements for the project.</returns>
        public MediaBin LoadMediaBin(Uri mediaBinUri)
        {
            if (mediaBinUri == this.sampleMediaBinUri)
            {
                return this.sampleMediaBin;
            }

            return new MediaBin { Id = CreateUri(MediaBins) };
        }

        /// <summary>
        /// Loads a <see cref="TitleTemplate"/> from the repository.
        /// </summary>
        /// <returns>The <see cref="TitleTemplateCollection"/> of the titles template that were loaded.</returns>
        public TitleTemplateCollection LoadTitleTemplates()
        {
            return this.sampleTitleTemplates;
        }

        /// <summary>
        /// Loads a project from the repository returning back the details.
        /// </summary>
        /// <param name="site">The <see cref="Uri"/> of the project site.</param>
        /// <returns>The <see cref="Project"/> that exists at the specified <see cref="Uri"/>.</returns>
        public Project LoadProject(Uri site)
        {
            Project project = this.sampleProjects.Where(x => x.Id == site).FirstOrDefault();

            if (project != null)
            {
                return project;
            }

            return new Project { Id = CreateUri(Projects) };
        }

        /// <summary>
        /// Saves a project into the repository.
        /// </summary>
        /// <param name="project">The project to be saved.</param>
        /// <returns>true, indicates that the project was saved. false, that the save failed.</returns>
        public bool SaveProject(Project project)
        {
            if (this.sampleProjects.Any(x => x.Id == project.Id))
            {
                this.sampleProjects.Remove(this.sampleProjects.First(x => x.Id == project.Id));
            }

            this.sampleProjects.Add(project);

            return true;
        }

        /// <summary>
        /// Get the projects collection of the given user.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <returns>A <see cref="ProjectCollection"/> with all the projects of the user.</returns>
        public ProjectCollection GetProjectsByUser(string userName)
        {
            return this.sampleProjects;
        }

        /// <summary>
        /// Deletes the project.
        /// </summary>
        /// <param name="site">The <see cref="Uri"/> of the project site.</param>
        /// <returns>True if project have been deleted else false. </returns>
        public bool DeleteProject(Uri site)
        {
            Project project = this.sampleProjects.SingleOrDefault(x => x.Id == site);
            if (project != null)
            {
                this.sampleProjects.Remove(project);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Creates the URI for the given item.
        /// </summary>
        /// <param name="item">The item name.</param>
        /// <returns><see cref="Uri"/> for the given item.</returns>
        private static Uri CreateUri(string item)
        {
            string uriString = string.Format(CultureInfo.InvariantCulture, "http://rce.litwareinc.com/samples/2.0/{0}/{1}", item, Guid.NewGuid().ToString("D"));
            return new Uri(uriString);
        }

        /// <summary>
        /// Creates the image image.
        /// </summary>
        /// <param name="title">The title of the image.</param>
        /// <param name="reference">The url of the image.</param>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        /// <returns>The <see cref="ImageItem"/>.</returns>
        private static ImageItem CreateImageItem(string title, string reference, int width, int height)
        {
            return new ImageItem
            {
                Id = CreateUri(ImageItems),
                Title = title,
                Width = width,
                Height = height,
                Resources =
                               {
                                   new Resource
                                       {
                                           Id = CreateUri(Resources),
                                           ResourceType = "Master",
                                           Ref = reference
                                       }
                               }
            };
        }

        /// <summary>
        /// Creates the audio item.
        /// </summary>
        /// <param name="title">The title of the audio item.</param>
        /// <param name="duration">The duration of the audio.</param>
        /// <param name="reference">The url of the audio item.</param>
        /// <returns>The <see cref="AudioItem"/>.</returns>
        private static AudioItem CreateAudioItem(string title, double? duration, string reference)
        {
            return new AudioItem
            {
                Id = CreateUri(AudioItems),
                Title = title,
                Duration = duration,
                Resources =
                               {
                                   new Resource
                                       {
                                           Id = CreateUri(Resources),
                                           ResourceType = "Master",
                                           Ref = reference
                                       }
                               }
            };
        }

        /// <summary>
        /// Creates the video item.
        /// </summary>
        /// <param name="id">The <see cref="Uri"/> of the video.</param>
        /// <param name="title">The title of the video item.</param>
        /// <param name="duration">The duration of the video.</param>
        /// <param name="frameRate">The framerate of the video.</param>
        /// <param name="reference">The url of the audio item.</param>
        /// <param name="width">The width of the video.</param>
        /// <param name="height">The height of the video.</param>
        /// <param name="resourceType">The type of the resource.</param>
        /// <returns>The <see cref="VideoItem"/>.</returns>
        private static SmoothStreamingVideoItem CreateSmoothVideoItem(Uri id, string title, double? duration, SmpteFrameRate frameRate, string reference, int width, int height, string resourceType)
        {
            return new SmoothStreamingVideoItem
            {
                Id = id,
                Title = title,
                Duration = duration,
                FrameRate = frameRate,
                Width = width,
                Height = height,
                Resources =
                               {
                                   new Resource
                                       {
                                           Id = CreateUri(Resources),
                                           Ref = reference,
                                           ResourceType = resourceType,
                                       }
                               }
            };
        }

        /// <summary>
        /// Creates the comment.
        /// </summary>
        /// <param name="creator">The creator of the comment.</param>
        /// <param name="text">The comment's text.</param>
        /// <param name="commentType">Type of the comment.</param>
        /// <param name="created">The created date.</param>
        /// <param name="markIn">The mark in time.</param>
        /// <param name="markOut">The mark out time.</param>
        /// <param name="modified">The modified datetime.</param>
        /// <returns>The <see cref="Comment"/>.</returns>
        private static Comment CreateComment(string creator, string text, string commentType, DateTime created, double? markIn, double? markOut, DateTime modified)
        {
            return new Comment
            {
                Id = CreateUri(Annotations),
                Creator = creator,
                Text = text,
                Type = commentType,
                Created = created,
                MarkIn = markIn,
                MarkOut = markOut,
                Modified = modified
            };
        }

        /// <summary>
        /// Creates the track.
        /// </summary>
        /// <param name="trackType">Type of the track.</param>
        /// <returns>The <see cref="Track"/>.</returns>
        private static Track CreateTrack(TrackType trackType)
        {
            return new Track
            {
                Id = CreateUri(Tracks),
                TrackType = trackType.ToString()
            };
        }

        /// <summary>
        /// Creates the shot.
        /// </summary>
        /// <param name="item">The source of the shot.</param>
        /// <param name="markIn">The mark in time.</param>
        /// <param name="markOut">The mark out time.</param>
        /// <param name="position">The position in the timeline.</param>
        /// <param name="volume">The volume of the shot.</param>
        /// <returns>The Shot item.</returns>
        private static Shot CreateShot(Item item, double markIn, double markOut, double position, decimal volume)
        {
            return new Shot
            {
                Id = CreateUri(Shots),
                Title = item.Title,
                CMSId = item.CMSId,
                AzureId = item.AzureId,
                Source = item,
                SourceAnchor = new Anchor { MarkIn = markIn, MarkOut = markOut },
                TrackAnchor = new Anchor { MarkIn = position, MarkOut = position },
                Volume = volume,
            };
        }

        /// <summary>
        /// Creates the title template.
        /// </summary>
        /// <param name="name">The name of the title template.</param>
        /// <returns>The <see cref="TitleTemplate"/>.</returns>
        private static TitleTemplate CreateTitleTemplate(string name)
        {
            return new TitleTemplate
            {
                Id = CreateUri(TitleTemplates),
                TemplateName = name
            };
        }

        /// <summary>
        /// Creates the title.
        /// </summary>
        /// <param name="titleTemplate">The <see cref="TitleTemplate"/>.</param>
        /// <param name="position">The position of the title in the timeline.</param>
        /// <param name="markOut">The mark out time.</param>
        /// <returns>The <see cref="Title"/>.</returns>
        private static Title CreateTitle(TitleTemplate titleTemplate, double position, double markOut)
        {
            return new Title
            {
                Id = CreateUri(Titles),
                TitleTemplate = titleTemplate,
                TrackAnchor = new Anchor { MarkIn = position, MarkOut = markOut },
                TextBlockCollection =
                               {
                                   new TextBlock
                                       {
                                           Text = RCE.Data.Demo.Properties.Resources.TitleText
                                       },

                                   new TextBlock
                                       {
                                           Text = RCE.Data.Demo.Properties.Resources.SubtitleText
                                       }
                               }
            };
        }
    }
}
