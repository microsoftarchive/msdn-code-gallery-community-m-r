// <copyright file="Sequence.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Sequence.cs                     
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
    using System.Windows.Media;
    using RCE.Services.Contracts;

    public class Sequence : BaseModel
    {
        private string name;

        private string encodedThumbnail;

        private ImageSource thumbnail;

        public Sequence()
        {
            this.Tracks = new List<Track>();
            this.Id = Guid.NewGuid();
            this.Name = this.Id.ToString();
            this.AdOpportunities = new ObservableCollection<AdOpportunity>();
            this.CommentElements = new ObservableCollection<Comment>();
            this.Markers = new ObservableCollection<Marker>();
        }

        /// <summary>
        /// Gets the collection of <see cref="Comment"/> of the sequence.
        /// </summary>
        /// <value>The collection of comments.</value>
        public ObservableCollection<Comment> CommentElements { get; private set; }

        /// <summary>
        /// Gets the collection of <see cref="AdOpportunities"/> of the sequence.
        /// </summary>
        /// <value>The collection of ads.</value>
        public ObservableCollection<AdOpportunity> AdOpportunities { get; private set; }

        /// <summary>
        /// Gets the collection of <see cref="Markers"/> of the sequence.
        /// </summary>
        /// <value>The collection of markers.</value>
        public ObservableCollection<Marker> Markers { get; private set; }

        public Guid Id { get; set; }

        public ImageSource Thumbnail
        {
            get
            {
                return this.thumbnail;
            }

            set 
            { 
                this.thumbnail = value;
                this.OnPropertyChanged("Thumbnail");
            }
        }

        public string EncodedThumbnail
        {
            get
            {
                return this.encodedThumbnail;
            }

            set
            {
                this.encodedThumbnail = value;
                this.OnPropertyChanged("EncodedThumbnail");
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.OnPropertyChanged("Name");
            }
        }

        public IList<Track> Tracks { get; set; }

        /// <summary>
        /// Adds the given <paramref name="comment"/> to the comment collection.
        /// </summary>
        /// <param name="comment">The comment being added.</param>
        public void AddComment(Comment comment)
        {
            Comment prevElement = null;
            foreach (var entry in this.CommentElements)
            {
                if (entry.MarkIn < comment.MarkIn)
                {
                    prevElement = entry;
                }
            }

            if (prevElement == null)
            {
                this.CommentElements.Insert(0, comment);
            }
            else
            {
                this.CommentElements.Insert(this.CommentElements.IndexOf(prevElement) + 1, comment);
            }
        }
    }
}
