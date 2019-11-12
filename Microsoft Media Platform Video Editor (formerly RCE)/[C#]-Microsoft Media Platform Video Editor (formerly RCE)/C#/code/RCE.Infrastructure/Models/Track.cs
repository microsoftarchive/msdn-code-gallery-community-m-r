// <copyright file="Track.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: Track.cs                     
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
    using System.Linq;

    /// <summary>
    /// Collection of elemetns in the timeline(Visual/Audio/Title).
    /// </summary>
    public class Track : Audit
    {
        private double volume;

        private double balance;

        private bool isMuted;

        /// <summary>
        /// Initializes a new instance of the <see cref="Track"/> class.
        /// </summary>
        public Track()
        {
            this.Id = Guid.NewGuid();
            this.Shots = new List<TimelineElement>();
            this.Created = DateTime.Now;
            this.Modified = DateTime.Now;
            this.GlobalSettingsEnabled = true;
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The unique identifier for the <see cref="Track"/>.</value>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the shots.
        /// </summary>
        /// <value>The collection of <see cref="TimelineElement"/>.</value>
        public List<TimelineElement> Shots { get; private set; }

        /// <summary>
        /// Gets or sets the track number.
        /// </summary>
        /// <value>The track number.</value>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the type of the track.
        /// </summary>
        /// <value>The type of the track.</value>
        public TrackType TrackType { get; set; }

        /// <summary>
        /// Gets or sets the provider URI.
        /// </summary>
        /// <value>The provider URI.</value>
        public Uri ProviderUri { get; set; }

        /// <summary>
        /// Gets or sets balance.
        /// </summary>
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

        /// <summary>
        /// Gets or sets volume.
        /// </summary>
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
        /// Gets or sets muted state.
        /// </summary>
        public bool IsMuted
        {
            get
            {
                return this.isMuted;
            }

            set
            {
                this.isMuted = value;
                this.OnPropertyChanged("IsMuted");
            }
        }

        public bool GlobalSettingsEnabled { get; private set; }

        public int ChannelNumber { get; private set; }

        /// <summary>
        /// Gets the copy of the shots in <see cref="Track"/> which can use used 
        /// for ctrl + z functionality.
        /// </summary>
        /// <returns>The list of <see cref="TimelineElement"/>.</returns>
        public IList<TimelineElement> GetMemento()
        {
            IList<TimelineElement> memento = new List<TimelineElement>();

            foreach (TimelineElement element in this.Shots)
            {
                memento.Add(element.GetMemento());
            }

            return memento;
        }

        /// <summary>
        /// Sets the memento.
        /// </summary>
        /// <param name="memento">The list of <see cref="TimelineElement"/>.</param>
        public void SetMemento(IList<TimelineElement> memento)
        {
            foreach (TimelineElement element in this.Shots)
            {
                TimelineElement mementoElement = memento.Where(x => x.Id == element.Id).FirstOrDefault();

                if (mementoElement != null)
                {
                    element.SetMemento(mementoElement);
                }
            }
        }

        public void ApplyChannelConvention()
        {
            this.GlobalSettingsEnabled = false;

            this.Balance = this.Number % 2 == 0 ? 1 : -1;
            this.ChannelNumber = this.Number % 2 == 0 ? 2 : 1;
        }

        /// <summary>
        /// Returns true if the track has gaps
        /// </summary>
        /// <value>True if the track has at least one gap.</value>
        public bool HasGaps()
        {
            const double Epsilon = 0.000000000001d;

            var shots = this.Shots;
            for (var i = 0; i < shots.Count; i++)
            {
                double gap;
                var currentElement = shots[i];
                if (i > 0)
                {
                    var previousElement = shots[i - 1];
                    gap = currentElement.Position.TotalSeconds - (previousElement.Position.TotalSeconds + previousElement.Duration.TotalSeconds);
                }
                else
                {
                    gap = currentElement.Position.TotalSeconds;
                }

                if (Math.Abs(gap) > Epsilon)
                {
                    return true;
                }
            }

            return false;
        }
    }
}