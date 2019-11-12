// <copyright file="TimelineElement.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TimelineElement.cs                     
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
    using System.Windows;

    using SMPTETimecode;

    /// <summary>
    /// Specifies the properties of the elements in the timeline module.
    /// </summary>
    public class TimelineElement : Audit
    {
        /// <summary>
        /// In position of the <see cref="TimelineElement"/> relative to the asset duration.
        /// </summary>
        private TimeCode inPosition;

        /// <summary>
        /// Out position of the <see cref="TimelineElement"/> relative to the asset duration.
        /// </summary>
        private TimeCode outPosition;

        /// <summary>
        /// Volume of the timeline element.
        /// </summary>
        private double volume;

        private double balance;

        private List<Point> volumeNodeCollection;
        
        private string selectedAudioStream;

        private string selectedVideoStream;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineElement"/> class.
        /// </summary>
        public TimelineElement()
        {
            this.Id = Guid.NewGuid();
            this.Comments = new ObservableCollection<Comment>();
            this.InTransition = new Transition(TransitionType.Fade);
            this.OutTransition = new Transition(TransitionType.Fade);
            this.VolumeNodeCollection = new List<Point>();
            this.Volume = 0.5;
            this.Created = DateTime.Now;
            this.Modified = DateTime.Now;
        }

        /// <summary>
        /// Gets the unique identifier for the <see cref="TimelineElement"/>.
        /// </summary>
        /// <value>The unique identifier for the <see cref="TimelineElement"/>.</value>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets the asset.
        /// </summary>
        /// <value>The <see cref="Asset"/>.</value>
        public Asset Asset { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The <see cref="TimeCode"/>.</value>
        public TimeCode Position { get; set; }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public ObservableCollection<Comment> Comments { get; private set; }

        /// <summary>
        /// Gets the in transition.
        /// </summary>
        /// <value>The in transition.</value>
        public Transition InTransition { get; private set; }

        /// <summary>
        /// Gets the out transition.
        /// </summary>
        /// <value>The out transition.</value>
        public Transition OutTransition { get; private set; }

        /// <summary>
        /// Gets or sets the In position of the <see cref="TimelineElement"/> relative to the asset duration.
        /// </summary>
        /// <value>The in position.</value>
        public TimeCode InPosition
        {
            get
            {
                return this.inPosition;
            }

            set
            {
                this.inPosition = value;
                this.OnPropertyChanged("InPosition");
            }
        }

        /// <summary>
        /// Gets or sets the Out position of the <see cref="TimelineElement"/> relative to the asset duration.
        /// </summary>
        /// <value>The out position.</value>
        public TimeCode OutPosition
        {
            get
            {
                return this.outPosition;
            }

            set
            {
                this.outPosition = value;
                this.OnPropertyChanged("OutPosition");
            }
        }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <value>The duration.</value>
        public TimeCode Duration
        {
            get
            {
                return TimeCode.FromAbsoluteTime(this.OutPosition.TotalSeconds - this.InPosition.TotalSeconds, this.Position.FrameRate);
            }
        }

        /// <summary>
        /// Gets or sets the provider URI.
        /// </summary>
        /// <value>The provider URI.</value>
        public Uri ProviderUri { get; set; }

        // TODO: Replace this with Anchor properties instead of using the values

        /// <summary>
        /// Gets or sets the track anchor URI.
        /// </summary>
        /// <value>The track anchor URI.</value>
        public Uri TrackAnchorUri { get; set; }

        // TODO: Replace this with Anchor properties instead of using the values

        /// <summary>
        /// Gets or sets the source anchor URI.
        /// </summary>
        /// <value>The source anchor URI.</value>
        public Uri SourceAnchorUri { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>The volume.</value>
        public double Volume
        {
            get
            {
                return this.volume;
            }

            set
            {
                this.volume = value;
                this.OnPropertyChanged("Volume");
            }
        }

        /// <summary>
        /// Gets or sets the volume node collection.
        /// </summary>
        /// <value>The list of volume nodes.</value>
        public List<Point> VolumeNodeCollection
        {
            get
            {
                return this.volumeNodeCollection;
            }

            set
            {
                this.volumeNodeCollection = value;
                this.IsRubberbandingEnabled = true;
            }
        }

        public bool IsRubberbandingEnabled { get; set; }

        public double Balance
        {
            get
            {
                return this.balance;
            }

            set
            {
                this.balance = value;
                this.OnPropertyChanged("Balance");
            }
        }

        public string SelectedVideoStream
        {
            get
            {
                return this.selectedVideoStream;
            }

            set
            {
                this.selectedVideoStream = value;
                this.OnPropertyChanged("SelectedVideoStream");
            }
        }

        public string SelectedAudioStream
        {
            get
            {
                return this.selectedAudioStream;
            }

            set
            {
                this.selectedAudioStream = value;
                this.OnPropertyChanged("SelectedAudioStream");
            }
        }

        /// <summary>
        /// Gets or sets the Media Plugin.
        /// </summary>
        /// <value>The <see cref="IRCESmoothStreamingMediaPlugin"/>.</value>
        public IRCESmoothStreamingMediaPlugin MediaPlugin { get; set; }

        /// <summary>
        /// Adds the comments.
        /// </summary>
        /// <param name="comments">The comments.</param>
        public void AddComments(ObservableCollection<Comment> comments)
        {
            this.Comments = comments;
        }

        /// <summary>
        /// Gets the memento.
        /// </summary>
        /// <returns>The <see cref="TimelineElement"/>.</returns>
        public TimelineElement GetMemento()
        {
            return new TimelineElement
                {
                    Id = this.Id,
                    InPosition = this.InPosition,
                    OutPosition = this.OutPosition,
                    Position = this.Position,
                    VolumeNodeCollection = this.VolumeNodeCollection,
                    Volume = this.Volume,
                    Balance = this.Balance
                };
        }

        /// <summary>
        /// Sets the memento.
        /// </summary>
        /// <param name="element">The <see cref="TimelineElement"/>.</param>
        public void SetMemento(TimelineElement element)
        {
            this.InPosition = element.inPosition;
            this.OutPosition = element.OutPosition;
            this.Position = element.Position;
            this.VolumeNodeCollection = element.VolumeNodeCollection;
            this.Volume = element.Volume;
            this.Balance = element.Balance;
        }
    }
}
