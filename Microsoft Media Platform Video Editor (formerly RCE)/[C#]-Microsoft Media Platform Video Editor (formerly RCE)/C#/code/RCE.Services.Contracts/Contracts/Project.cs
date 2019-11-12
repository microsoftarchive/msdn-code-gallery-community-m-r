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

namespace RCE.Services.Contracts
{
    using System;
    using System.Runtime.Serialization;
    using Output;

    /// <summary>
    /// A class that defines the structure of a project.
    /// </summary>
    [DataContract(Namespace = "http://schemas.microsoft.com/rce/")]
    [KnownType(typeof(OutputMetadata))]
#if !SILVERLIGHT
    [Serializable]
#endif
    public class Project : Container
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Project"/> class with the default values.
        /// </summary>
        public Project()
        {
            this.Comments = new CommentCollection();
            this.Sequences = new SequenceCollection();
            this.Transitions = new TransitionCollection();
            this.Titles = new TitleCollection();
        }

        /// <summary>
        /// Gets or sets the highlight Id of the project.
        /// </summary>
        /// <value>The highlight Id of the project.</value>
        [DataMember]
        public Guid HighlightId { get; set; }

        /// <summary>
        /// Gets or sets the duration of the project.
        /// </summary>
        /// <value>The duration of the project.</value>
        [DataMember]
        public double? Duration { get; set; }

        /// <summary>
        /// Gets or sets the Id of the MediaBin.
        /// </summary>
        /// <value>The media bin associated with the project.</value>
        [DataMember]
        public MediaBin MediaBin { get; set; }

        /// <summary>
        /// Gets or sets the resolution of the project.
        /// </summary>
        /// <value>The resolution of the project.</value>
        [DataMember]
        public string Resolution { get; set; }

        /// <summary>
        /// Gets or sets the frame rate of the project.
        /// </summary>
        /// <value>The frame rate of the project.</value>
        [DataMember]
        public string SmpteFrameRate { get; set; }

        /// <summary>
        /// Gets or sets the auto save interval of the project.
        /// </summary>
        /// <value>The interval of the project used to auto save.</value>
        [DataMember]
        public decimal? AutoSaveInterval { get; set; }

        /// <summary>
        /// Gets or sets the start TimeCode of the project.
        /// </summary>
        /// <value>The start TimeCode of the project.</value>
        [DataMember]
        public double? StartTimeCode { get; set; }

        /// <summary>
        /// Gets or sets a value that indicating whether the edit mode of the project is ripple mode or not.
        /// </summary>
        /// <value>A true if the edit mode of the project is RippleMode;otherwise false.</value>
        [DataMember]
        public bool? RippleMode { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail of the project.
        /// </summary>
        /// <value>The Thumbnail of the project.</value>
        [DataMember]
        public string ProjectThumbnail { get; set; }
        
        /// <summary>
        /// Gets or sets the <see cref="Sequence"/> that are in the project.
        /// </summary>
        /// <value>The collection of sequences.</value>
        [DataMember]
        public SequenceCollection Sequences { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="TransitionCollection"/> for the project.
        /// </summary>
        /// <value>The collection of transitions of the project.</value>
        [DataMember]
        public TransitionCollection Transitions { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="Comment"/>s that are associated to the project.
        /// </summary>
        /// <value>The collection of comments of the project.</value>
        [DataMember]
        public CommentCollection Comments { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="Titles"/>s that are associated to the project.
        /// </summary>
        /// <value>The collection of titles of the project.</value>
        [DataMember]
        public TitleCollection Titles { get; set; }

        /// <summary>
        /// Gets or sets the Project metadata.
        /// </summary>
        /// <value>The metadata associated with the project.</value>
        [DataMember]
        public object Metadata { get; set; }
    }
}