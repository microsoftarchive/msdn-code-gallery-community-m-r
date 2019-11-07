// <copyright file="BaseCommentsBarPresenter.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: BaseCommentsBarPresenter.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.CommentsBar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Commands;
    using Microsoft.Practices.Composite.Presentation.Events;

    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using RCE.Services.Contracts;

    using Comment = RCE.Infrastructure.Models.Comment;
    using Contract = RCE.Services.Contracts;
    using SMPTETimecode;

    /// <summary>
    /// Provides an abstract implementation for <see cref="ICommentsBarPresenter"/>.
    /// </summary>
    public abstract class BaseCommentsBarPresenter : BaseModel, ICommentsBarPresenter
    {
        /// <summary>
        /// The <see cref="IEventAggregator"/> to subscribe to application events.
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// The <see cref="ITimelineBarRegistry"/> used to retrieve the registered timeline bar elements.
        /// </summary>
        private readonly ITimelineBarRegistry timelineBarRegistry;

        /// <summary>
        /// The list of the current timeline bar elements.
        /// </summary>
        private readonly IList<ITimelineBarElementModel> timelineBarElements;

        private readonly ISequenceRegistry sequenceRegistry;

        private double refreshPreviewWidth;

        private IList<string> options;

        private bool isOptionMenuVisible;

        private TimeSpan lastKnownPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCommentsBarPresenter"/> class.
        /// </summary>
        /// <param name="view">Instance of the <see cref="ICommentsBarView"/> interface.</param>
        /// <param name="eventAggregator">Instance of the <see cref="IEventAggregator"/> interface.</param>
        /// <param name="sequenceRegistry">Instance of the <see cref="ISequenceRegistry"/> interface.</param>
        /// /// <param name="timelineBarRegistry">Instance of the <see cref="ITimelineBarRegistry"/> interface.</param>
        protected BaseCommentsBarPresenter(ICommentsBarView view, IEventAggregator eventAggregator, ISequenceRegistry sequenceRegistry, ITimelineBarRegistry timelineBarRegistry)
        {
            this.eventAggregator = eventAggregator;
            this.sequenceRegistry = sequenceRegistry;
            this.sequenceRegistry.CurrentSequenceChanged += this.HandleCurrentSequenceChanged;
            

            this.timelineBarRegistry = timelineBarRegistry;
            this.View = view;

            if (this.sequenceRegistry.CurrentSequenceModel != null)
            {
                this.sequenceRegistry.CurrentSequenceModel.PropertyChanged += this.OnCurrentSequenceOnPropertyChanged;
                this.View.SetDuration(this.sequenceRegistry.CurrentSequenceModel.Duration);
            }

            this.timelineBarElements = new List<ITimelineBarElementModel>();

            this.Options = new List<string>();
            this.OptionSelectedCommand = new DelegateCommand<string>(this.AddSelectedOption);
            this.CloseCommand = new DelegateCommand<object>(this.CloseOptionsMenu);

            this.eventAggregator.GetEvent<PositionDoubleClickedEvent>().Subscribe(this.AddPreview, ThreadOption.PublisherThread, true, this.FilterPositionDoubleClickedEvent);
            this.eventAggregator.GetEvent<RefreshElementsEvent>().Subscribe(this.RefreshPreviews, ThreadOption.PublisherThread, true, this.FilterRefreshElements);
            this.eventAggregator.GetEvent<AddPreviewEvent>().Subscribe(
                this.AddPreview, ThreadOption.UIThread, true, this.FilterAddPreviewEvent);
            this.eventAggregator.GetEvent<RemovePreviewEvent>().Subscribe(this.RemovePreview, ThreadOption.UIThread, true, this.FilterRemovePreviewEvent);
            this.eventAggregator.GetEvent<DeleteAllPreviewsEvent>().Subscribe(this.DeleteAllPreviews, ThreadOption.PublisherThread, true, this.FilterDeleteAllPreviewsEvent);

            this.View.Model = this;
        }

        /// <summary>
        /// Gets or sets the <see cref="ICommentsBarView"/> presentation model of the view.
        /// </summary>
        /// <value>
        /// A <see also="ICommentsBarView"/> that represents the presentation model of the view.
        /// </value>
        public ICommentsBarView View { get; set; }

        /// <summary>
        /// Gets the command being executed when an option is being selected.
        /// </summary>
        /// <value>The command being executed when an option is selected.</value>
        public DelegateCommand<string> OptionSelectedCommand { get; private set; }

        /// <summary>
        /// Gets the command being executed when closing the menu option.
        /// </summary>
        /// <value>The command being executed when closing the menu option.</value>
        public DelegateCommand<object> CloseCommand { get; private set; }

        /// <summary>
        /// Gets the list of available options to show in the timeline bar.
        /// </summary>
        /// <value>The list of available options.</value>
        public IList<string> Options
        {
            get
            {
                return this.options;
            }

            private set
            {
                this.options = value;
                this.OnPropertyChanged("Options");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the options menu is visible or not.
        /// </summary>
        /// <value>A true if the options menu is visible;otherwise false.</value>
        public bool IsOptionMenuVisible
        {
            get
            {
                return this.isOptionMenuVisible;
            }

            set
            {
                this.isOptionMenuVisible = value;
                this.OnPropertyChanged("IsOptionMenuVisible");
            }
        }

        protected abstract CommentMode Mode { get; }

        protected abstract bool FilterAddPreviewEvent(AddPreviewPayload payload);

        protected abstract bool FilterRemovePreviewEvent(RemovePreviewPayload payload);

        protected abstract bool FilterRefreshElements(RefreshElementsEventArgs payload);

        protected abstract bool FilterPositionDoubleClickedEvent(PositionPayloadEventArgs payload);

        protected abstract bool FilterDeleteAllPreviewsEvent(DeleteAllPreviewsPayload payload);
        
        protected abstract bool ShouldRemovePreviewsWhenSequenceChanges();

        protected abstract void SequenceDurationChanged(TimeCode newDuration);

        private void AddSelectedOption(string option)
        {
            this.View.CloseOptions();
            this.AddPreview(option, this.lastKnownPosition);
        }

        private void CloseOptionsMenu(object obj)
        {
            this.View.CloseOptions();
        }

        private void AddPreview(PositionPayloadEventArgs payload)
        {
            this.lastKnownPosition = payload.Position;
            this.View.CloseOptions();

            IList<string> availableElements = this.timelineBarRegistry.GetTimelineBarElementKeys().Where(k => !k.Equals("PlayByPlay")).ToList();

            if (availableElements.Count == 0)
            {
                return;
            }

            if (availableElements.Count == 1)
            {
                this.AddPreview(availableElements.First(), payload.Position);
            }
            else
            {
                this.Options = availableElements;
                this.View.ShowOptions(payload.Position.TotalSeconds);
                this.IsOptionMenuVisible = true;
            }
        }

        private void AddPreview(AddPreviewPayload payload)
        {
            ITimelineBarElementModel model = this.timelineBarRegistry.GetTimelineBarElement(payload.RegistryKey);

            if (model != null)
            {
                model.SetElement(payload.Value, this.Mode);
                this.HookModel(model);
            }
        }

        private void DeleteAllPreviews(DeleteAllPreviewsPayload payload)
        {
            if (this.ShouldDeleteAllPreviews(payload))
            {
                this.RemoveAllPreviews();
            }
            else
            {
                this.RemoveSpecificPlayByPlayMarkers(payload.ItemsToErase);
            }
        }

        private void RemoveSpecificPlayByPlayMarkers(IEnumerable<PlayByPlay> playByPlayList)
        {
            var col = this.timelineBarElements.Where(tbe =>
            {
                if (tbe.GetElement<PlayByPlay>() == null)
                {
                    return false;
                }

                var id = tbe.GetElement<PlayByPlay>().TimelineId;
                var idList = playByPlayList.Select(pbp => pbp.TimelineId);
                return idList.Contains(id);
            });

            int i = col.Count();

            while (i != 0) 
            {
                var model = col.ElementAt(0);
                this.RemovePreview(model);
                if (this.Mode == CommentMode.Timeline)
                {
                    this.sequenceRegistry.CurrentSequence.Markers.Remove(model.GetElement<PlayByPlay>());
                }

                this.timelineBarElements.Remove(model);
                i--;
            }
        }

        private void RemoveAllPreviews()
        {
            this.View.RemoveAllPreviews();
            this.timelineBarElements.Clear();
        }

        private bool ShouldDeleteAllPreviews(DeleteAllPreviewsPayload payload)
        {
            return payload.ItemsToErase == null || payload.ItemsToErase.Count() == 0;
        }

        private void AddPreview(string key, TimeSpan position)
        {
            ITimelineBarElementModel model = this.timelineBarRegistry.GetTimelineBarElement(key);

            if (model != null)
            {
                model.SetPosition(position);
                this.HookModel(model);
            }
        }

        private void AddPreview(string key, object element)
        {
            ITimelineBarElementModel model = this.timelineBarRegistry.GetTimelineBarElement(key);

            if (model != null)
            {
                this.HookModel(model);
                model.SetElement(element, CommentMode.Timeline);
            }
        }

        private void HookModel(ITimelineBarElementModel model)
        {
            if (model != null)
            {
                model.RefreshPreview(this.refreshPreviewWidth);
                model.Deleting += this.RemovePreview;
                model.TimelineBarElementUpdated += this.UpdatePreview;

                this.timelineBarElements.Add(model);
                this.View.AddPreview(model.Preview, model.Position, model.EditBox, model.DisplayBox);
            }
        }

        private void RemovePreview(object sender, EventArgs e)
        {
            var model = sender as ITimelineBarElementModel;

            if (model != null)
            {
                RemovePreview(model);
                this.timelineBarElements.Remove(model);
            }
        }

        private void RemovePreview(RemovePreviewPayload payload)
        {
            this.RemoveSpecificPlayByPlayMarkers(this.timelineBarElements.Select(
                tbe =>
                    {
                        PlayByPlay pbp = tbe.GetElement<PlayByPlay>();
                        if (pbp == null)
                        {
                            return null;
                        }

                        return pbp;
                    }).Where(pbp => pbp != null && pbp.EventId == payload.EventId));
        }

        private void RemovePreview(ITimelineBarElementModel model)
        {
            this.View.RemovePreview(model.Preview, model.EditBox);
            model.Deleting -= this.RemovePreview;
            model.TimelineBarElementUpdated -= this.UpdatePreview;
        }

        private void UpdatePreview(object sender, EventArgs e)
        {
            var model = sender as ITimelineBarElementModel;

            if (model != null)
            {
                this.View.UpdatePreview(model.Preview, model.Position, model.EditBox, model.DisplayBox);
            }
        }

        /// <summary>
        /// Refreshes the comments when timeline zoom In/Out happen.
        /// </summary>
        /// <param name="payload">The <see cref="RCE.Infrastructure.Events.RefreshElementsEventArgs"/> instance containing the event data.</param>
        private void RefreshPreviews(RefreshElementsEventArgs payload)
        {
            if (payload.RefreshedWidth.HasValue)
            {
                this.refreshPreviewWidth = payload.RefreshedWidth.Value;
            }
            
            this.View.RefreshPreviews(this.refreshPreviewWidth);

            foreach (ITimelineBarElementModel model in this.timelineBarElements)
            {
                model.RefreshPreview(this.refreshPreviewWidth);
            }
        }

        private void HandleCurrentSequenceChanged(object sender, Infrastructure.DataEventArgs<ISequenceModel> a)
        {
            this.View.SetDuration(this.sequenceRegistry.CurrentSequenceModel.Duration);

            if (a.Data != null && this.ShouldRemovePreviewsWhenSequenceChanges())
            {
                a.Data.PropertyChanged -= this.OnCurrentSequenceOnPropertyChanged;

                foreach (ITimelineBarElementModel model in this.timelineBarElements)
                {
                    this.RemovePreview(model);
                }

                this.timelineBarElements.Clear();
            }

            this.sequenceRegistry.CurrentSequenceModel.PropertyChanged += this.OnCurrentSequenceOnPropertyChanged;

            // collection is modified
            var comments = new List<Comment>(this.sequenceRegistry.CurrentSequence.CommentElements);
            
            foreach (var comment in comments)
            {
                this.AddPreview("Comment", comment);
            }

            var ads = new List<Contract.AdOpportunity>(this.sequenceRegistry.CurrentSequence.AdOpportunities);

            foreach (var adOpportunity in ads)
            {
                this.AddPreview("Ad", adOpportunity);
            }

            var markers = new List<Contract.Marker>(this.sequenceRegistry.CurrentSequence.Markers);

            foreach (var marker in markers)
            {
                this.AddPreview(marker is PlayByPlay ? "PlayByPlay" : "Marker", marker);
            }
        }

        protected void OnCurrentSequenceOnPropertyChanged(object s, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Duration")
            {
                this.SequenceDurationChanged(this.sequenceRegistry.CurrentSequenceModel.Duration);
            }
        }
    }
}