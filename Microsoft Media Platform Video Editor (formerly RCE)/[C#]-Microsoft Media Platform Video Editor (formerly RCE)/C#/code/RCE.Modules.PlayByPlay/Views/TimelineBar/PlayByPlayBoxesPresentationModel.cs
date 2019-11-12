// <copyright file="PlayByPlayBoxesPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: PlayByPlayBoxesPresentationModel.cs                     
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

    public class PlayByPlayBoxesPresentationModel : BaseModel, IPlayByPlayBoxesPresentationModel
    {
        private readonly IPlayByPlayViewPreview preview;

        private readonly ISequenceRegistry sequenceRegistry;

        private readonly IEventAggregator eventAggregator;

        private double time;

        private string text;

        private RCE.Services.Contracts.PlayByPlay playByPlay;

        public PlayByPlayBoxesPresentationModel(IPlayByPlayDisplayBox displayView, IPlayByPlayEditBox editView, IPlayByPlayViewPreview preview, ISequenceRegistry sequenceRegistry, IEventAggregator eventAggregator)
        {
            this.DisplayView = displayView;
            this.EditView = editView;

            this.preview = preview;
            this.sequenceRegistry = sequenceRegistry;
            this.eventAggregator = eventAggregator;

            this.CloseCommand = new DelegateCommand<object>(this.Close);
            this.SaveCommand = new DelegateCommand<object>(this.Save, this.CanSave);
            this.DeleteCommand = new DelegateCommand<object>(this.Delete);

            this.playByPlay = new RCE.Services.Contracts.PlayByPlay(0);

            this.DisplayView.Model = this;
            this.EditView.Model = this;

            this.preview.Model = this;
        }

        public event EventHandler<EventArgs> TimelineBarElementUpdated;

        public event EventHandler<EventArgs> Deleting;

        public IPlayByPlayDisplayBox DisplayView
        {
            get;
            set;
        }

        public IList<string> TemplateTypes
        {
            get;
            set;
        }

        public IPlayByPlayEditBox EditView
        {
            get;
            set;
        }

        public DelegateCommand<object> CloseCommand
        {
            get;
            set;
        }

        public DelegateCommand<object> SaveCommand { get; private set; }

        public DelegateCommand<object> DeleteCommand { get; private set; }

        public string SelectedTemplateType
        {
            get;
            set;
        }

        public object EditBox
        {
            get { return this.EditView; }
        }

        public object Preview
        {
            get { return this.preview; }
        }

        public double Position
        {
            get { return this.Time; }
        }

        public object DisplayBox
        {
            get
            {
                return this.DisplayView;
            }
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
            var newPlayByPlay = value as RCE.Services.Contracts.PlayByPlay;
            
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

                this.playByPlay.PropertyChanged += this.PlayByPlayPropertyChanged;

                this.SetPosition(TimeSpan.FromTicks(this.playByPlay.Time));
                this.DisplayView.Close();
                this.EditView.Close();
            }
        }

        public void ShowDisplayBox()
        {
            this.Time = TimeSpan.FromTicks(this.playByPlay.Time).TotalSeconds;
            this.Text = this.playByPlay.Text;
            this.DisplayView.Show();
        }

        public void ShowEditBox()
        {
            if (this.CommentMode == CommentMode.Timeline)
            {
                this.Time = TimeSpan.FromTicks(this.playByPlay.Time).TotalSeconds;
                this.Text = this.playByPlay.Text;
                this.EditView.Show();
            }
        }

        public void CloseDisplayView()
        {
            this.DisplayView.Close();
        }

        private bool CanSave(object arg)
        {
            try
            {
                this.ValidateTime(this.Time);
                return true;
            }
            catch (InputValidationException)
            {
                return false;
            }
        }

        private void Save(object obj)
        {
            this.playByPlay.Time = TimeSpan.FromSeconds(this.Time).Ticks;
            this.playByPlay.Text = this.Text;

            this.OnTimelineBarElementUpdated();
            this.EditView.Close();
        }

        private void Delete(object obj)
        {
            this.sequenceRegistry.CurrentSequence.Markers.Remove(this.playByPlay);
            this.EditView.Close();
            this.OnDeleting();
        }

        private void Close(object obj)
        {
            if (this.CommentMode == CommentMode.SubClip)
            {
                this.DisplayView.Close();    
            }
            else
            {
                this.EditView.Close();
            }
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

        private void OnDeleting()
        {
            EventHandler<EventArgs> handler = this.Deleting;
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

        private void PlayByPlayPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Time")
            {
                this.time = TimeSpan.FromTicks(this.playByPlay.Time).TotalSeconds;
            }
        }
    }
}
