// <copyright file="PlayByPlayDisplayBoxPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PlayByPlayDisplayBoxPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.PlayByPlay.Views.TimelineBar
{
    using System;
    using System.Collections.Generic;
    using Infrastructure.Events;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Commands;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;

    public class PlayByPlayDisplayBoxPresentationModel : BaseModel, IPlayByPlayDisplayBoxPresentationModel
    {
        private readonly IPlayByPlayViewPreview preview;

        private readonly ISequenceRegistry sequenceRegistry;

        private readonly IEventAggregator eventAggregator;

        private double time;

        private string text;

        private PlayByPlay playByPlay;

        public PlayByPlayDisplayBoxPresentationModel(IPlayByPlayDisplayBox view, IPlayByPlayViewPreview preview, ISequenceRegistry sequenceRegistry, IEventAggregator eventAggregator)
        {
            this.View = view;
            this.preview = preview;
            this.sequenceRegistry = sequenceRegistry;
            this.eventAggregator = eventAggregator;
            this.CloseCommand = new DelegateCommand<object>(this.Close);

            this.playByPlay = new PlayByPlay(0);

            this.View.Model = this;
            this.preview.Model = this;
        }

        public event EventHandler<EventArgs> TimelineBarElementUpdated;

        public event EventHandler<EventArgs> Deleting;

        public IPlayByPlayDisplayBox View
        {
            get;
            set;
        }

        public IList<string> TemplateTypes
        {
            get;
            set;
        }

        public DelegateCommand<object> CloseCommand
        {
            get;
            set;
        }

        public string SelectedTemplateType
        {
            get;
            set;
        }

        public object EditBox
        {
            get { return this.View; }
        }

        public object Preview
        {
            get { return this.preview; }
        }

        public double Position
        {
            get { return this.Time; }
        }

        public double Time
        {
            get
            {
                return this.time;
            }

            set
            {
                this.time = value;
                this.OnPropertyChanged("Time");
                this.ValidateTime(value);
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
                this.OnPropertyChanged("Text");
            }
        }

        protected CommentMode CommentMode { get; set; }

        public T GetElement<T>() where T : class
        {
            return this.playByPlay as T;
        }

        public void RaisePreviewClickedEvent()
        {
            this.eventAggregator.GetEvent<MetadataEventSelected>().Publish(this.GetPreviewClickEventPayload());
        }

        public void SetPosition(TimeSpan position)
        {
            this.playByPlay.Time = position.Ticks;
            this.Time = position.TotalSeconds;

            this.OnTimelineBarElementUpdated();
        }

        /// <summary>
        /// Refreshes the comments when timeline zoom In/Out happen.
        /// </summary>
        /// <param name="refreshedWidth">The refreshed width.</param>
        public void RefreshPreview(double refreshedWidth)
        {
            this.OnTimelineBarElementUpdated();
        }

        public void SetElement(object value, CommentMode mode) 
        {
            var newPlayByPlay = value as PlayByPlay;
            
            if (newPlayByPlay != null)
            {
                this.CommentMode = mode;

                if (mode == CommentMode.Timeline)
                {
                    this.sequenceRegistry.CurrentSequence.Markers.Remove(this.playByPlay);
                }
                
                this.playByPlay = newPlayByPlay;
                if (mode == CommentMode.Timeline)
                {
                    if (!this.sequenceRegistry.CurrentSequence.Markers.Contains(this.playByPlay))
                    {
                        this.sequenceRegistry.CurrentSequence.Markers.Add(this.playByPlay);
                    }
                }

                this.SetPosition(TimeSpan.FromTicks(this.playByPlay.Time));
                this.View.Close();
            }
        }

        public void ShowEditBox()
        {
            this.Time = TimeSpan.FromTicks(this.playByPlay.TimeWithOffset).TotalSeconds;
            this.Text = this.playByPlay.Text;
            this.View.Show();
        }

        private void Close(object obj)
        {
            this.View.Close();
        }

        private void ValidateTime(double timeToValidate)
        {
            if (double.IsNaN(timeToValidate) || double.IsInfinity(timeToValidate) || timeToValidate < 0)
            {
                throw new InputValidationException("Position is not valid.");
            }
        }

        private void OnTimelineBarElementUpdated()
        {
            EventHandler<EventArgs> handler = this.TimelineBarElementUpdated;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private MetadaSelectedPayload GetPreviewClickEventPayload()
        {
            var eventData = new EventData(this.playByPlay.ID.ToString(), TimeSpan.FromTicks(this.playByPlay.Time), this.playByPlay.Text, false);
            return new MetadaSelectedPayload(eventData, this.CommentMode);
        }
    }
}
