// <copyright file="TransitionsMetadataViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: TransitionsMetadataViewPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Metadata.Views
{
    using System.ComponentModel;

    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Events;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;

    public class TransitionsMetadataViewPresentationModel : BaseModel, ITransitionsMetadataViewPresentationModel
    {
        private readonly IEventAggregator eventAggregator;

        private double fadeInDuration = -1;

        private double fadeOutDuration = -1;

        private TimelineElement currentTimelineElement;

        public TransitionsMetadataViewPresentationModel(ITransitionsMetadataView view, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            this.eventAggregator.GetEvent<ShowMetadataEvent>().Subscribe(this.ShowMetadata, ThreadOption.PublisherThread, true);
            this.eventAggregator.GetEvent<HideMetadataEvent>().Subscribe(this.HideMetadata, true);

            this.FadeInDuration = 0;
            this.FadeOutDuration = 0;

            this.View = view;
            this.View.Model = this;
        }
        
        public ITransitionsMetadataView View { get; set; }

        public double FadeInDuration
        {
            get
            {
                return this.fadeInDuration;
            }

            set
            {
                if (this.fadeInDuration != value)
                {
                    this.fadeInDuration = value;
                    this.OnPropertyChanged("FadeInDuration");
                    this.NotifyTransitionChange();
                }
            }
        }

        public double FadeOutDuration
        {
            get
            {
                return this.fadeOutDuration;
            }

            set
            {
                if (this.fadeOutDuration != value)
                {
                    this.fadeOutDuration = value;
                    this.OnPropertyChanged("FadeOutDuration");
                    this.NotifyTransitionChange();
                }
            }
        }

        private void ShowMetadata(TimelineElement timelineElement)
        {
            if (this.currentTimelineElement != null)
            {
                this.currentTimelineElement.PropertyChanged -= this.OnCurrentTimelineElementPropertyChanged;
            }

            this.currentTimelineElement = timelineElement;
            this.currentTimelineElement.PropertyChanged += this.OnCurrentTimelineElementPropertyChanged;

            bool notifyTransitionChange = false;

            this.fadeInDuration = timelineElement.InTransition.Duration;
            this.fadeOutDuration = timelineElement.OutTransition.Duration;

            this.OnPropertyChanged("FadeOutDuration");
            this.OnPropertyChanged("FadeInDuration");
        }

        private void HideMetadata(object obj)
        {
            this.fadeInDuration = 0;
            this.OnPropertyChanged("FadeInDuration");

            this.fadeOutDuration = 0;
            this.OnPropertyChanged("FadeOutDuration");
        }

        private void OnCurrentTimelineElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "InPosition" || e.PropertyName == "OutPosition")
            {
                if (this.FadeInDuration > this.currentTimelineElement.Duration.TotalSeconds)
                {
                    this.FadeInDuration = this.currentTimelineElement.Duration.TotalSeconds;
                }

                if (this.FadeOutDuration > this.currentTimelineElement.Duration.TotalSeconds)
                {
                    this.FadeOutDuration = this.currentTimelineElement.Duration.TotalSeconds;
                }

                this.NotifyTransitionChange();
            }
        }

        private void NotifyTransitionChange()
        {
            if (this.currentTimelineElement != null)
            {
                this.currentTimelineElement.InTransition.Duration = this.FadeInDuration;
                this.currentTimelineElement.OutTransition.Duration = this.FadeOutDuration;

                this.eventAggregator.GetEvent<OperationExecutedInTimelineEvent>().Publish(null);
            }
        }
    }
}
