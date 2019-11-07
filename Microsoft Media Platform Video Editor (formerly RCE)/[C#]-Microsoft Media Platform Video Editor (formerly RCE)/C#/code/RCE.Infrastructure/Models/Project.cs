// <copyright file="Project.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Project.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using RCE.Services.Contracts;
    using SMPTETimecode;

    /// <summary>
    /// A class that defines the structure of a project.
    /// </summary>
    public class Project : Audit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class.
        /// </summary>
        public Project()
        {
            this.Id = Guid.NewGuid();
            this.MediaBin = new MediaBin();
            this.Comments = new ObservableCollection<Comment>();
            this.Timelines = new ObservableCollection<Sequence>();
        }

        /// <summary>
        /// Gets or sets the id of the project.
        /// </summary>
        /// <value>The id of the current project.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>The name of the project.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the collection of <see cref="Track"/>s that are associated to the project.
        /// </summary>
        /// <value>The list of tracks of the project.</value>
        public ObservableCollection<Sequence> Timelines { get; private set; }

        /// <summary>
        /// Gets the collection of <see cref="Comment"/>s that are associated to the project.
        /// </summary>
        /// <value>The collection of comments of the project.</value>
        public ObservableCollection<Comment> Comments { get; private set; }

        /// <summary>
        /// Gets or sets the MediaBin of the project.
        /// </summary>
        /// <value>The media bin of the project.</value>
        public MediaBin MediaBin { get; set; }

        /// <summary>
        /// Gets or sets the duration of the project.
        /// </summary>
        /// <value>The duration of the project.</value>
        public double? Duration { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail of the project.
        /// </summary>
        /// <value>The Thumbnail of the project.</value>
        public string ProjectThumbnail { get; set; }

        /// <summary>
        /// Gets or sets the auto save interval of the project.
        /// </summary>
        /// <value>The interval used to save the project.</value>
        public decimal AutoSaveInterval { get; set; }

        /// <summary>
        /// Gets or sets the start timecode of the project.
        /// </summary>
        /// <value>The start time code of the project.</value>
        public TimeCode StartTimeCode { get; set; }

        /// <summary>
        /// Gets or sets the frame rate of the project.
        /// </summary>
        /// <value>The frame rate of the project.</value>
        public SmpteFrameRate SmpteFrameRate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the edit mode of the project is ripple mode or not.
        /// </summary>
        /// <value>A true if the edit mode is Ripple Mode;otherwise false.</value>
        public bool RippleMode { get; set; }

        /// <summary>
        /// Gets or sets the resolution of the project.
        /// </summary>
        /// <value>The resolution of the project.</value>
        public string Resolution { get; set; }

        /// <summary>
        /// Gets or sets the ProviderUri that identifies the project on the server.
        /// </summary>
        /// <value>The uri that identifies the project on the server.</value>
        public Uri ProviderUri { get; set; }

        /// <summary>
        /// Gets or sets the Project metadata.
        /// </summary>
        /// <value>The metadata associated with the project.</value>
        public object Metadata { get; set; }

        /// <summary>
        /// Adds a collection of comments to the project.
        /// </summary>
        /// <param name="comments">The collection of comments being added.</param>
        public void AddComments(ObservableCollection<Comment> comments)
        {
            this.Comments = comments;
        }

        /// <summary>
        /// Adds a list of track to the project.
        /// </summary>
        /// <param name="sequence">The timeline being added.</param>
        public void AddTimeline(Sequence sequence)
        {
            this.Timelines.Add(sequence);
        }

        /// <summary>
        /// Calculates the duration of the project from tracks and set the duration property.
        /// </summary>
        public void SetProjectDuration()
        {
            double naturalDuration = 0;
            TimelineElement lastElement;
            
            // TODO: Refactor this to remove duration.
            foreach (Track track in this.Timelines.First().Tracks)
            {
                if (track == null || track.Shots == null || track.Shots.Count == 0)
                {
                    continue;
                }

                lastElement = track.Shots.Where(x => x.Position == track.Shots.Max(y => y.Position)).SingleOrDefault();
                if (lastElement != null)
                {
                    double duration = lastElement.Position.TotalSeconds + lastElement.OutPosition.TotalSeconds -
                                      lastElement.InPosition.TotalSeconds;

                    naturalDuration = Math.Max(naturalDuration, duration);
                }
            }

            this.Duration = naturalDuration;
        }
    }
}
