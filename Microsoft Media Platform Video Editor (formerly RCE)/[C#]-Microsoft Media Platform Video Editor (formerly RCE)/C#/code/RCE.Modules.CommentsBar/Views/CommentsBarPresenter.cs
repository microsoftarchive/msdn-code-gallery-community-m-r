// <copyright file="CommentsBarPresenter.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentsBarPresenter.cs                     
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
    using Infrastructure;
    using Infrastructure.Events;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Commands;

    using RCE.Infrastructure.Services;

    /// <summary>
    /// Provides the implementation for <see cref="ICommentsBarPresenter"/>.
    /// </summary>
    public class CommentsBarPresenter : BaseModel, ICommentsBarPresenter
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

        private double refreshPreviewWidth;

        private IList<string> options;

        private bool isOptionMenuVisible;

        private TimeSpan lastKnownPosition;

        private ISequenceRegistry sequenceRegistry;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsBarPresenter"/> class.
        /// </summary>
        /// <param name="view">Instance of the <see cref="ICommentsBarView"/> interface.</param>
        /// <param name="eventAggregator">Instance of the <see cref="IEventAggregator"/> interface.</param>
        /// <param name="timelineModel">Instance of the <see cref="ISequenceModel"/> interface.</param>
        public CommentsBarPresenter(ICommentsBarView view, IEventAggregator eventAggregator, ISequenceRegistry sequenceRegistry, ITimelineBarRegistry timelineBarRegistry)
        {
            this.eventAggregator = eventAggregator;
            this.sequenceRegistry = sequenceRegistry;
            this.sequenceRegistry.CurrentSequenceChanged += this.HandleCurrentSequenceChanged;
            this.sequenceRegistry.CurrentSequenceModel.PropertyChanged += this.OnCurrentSequenceOnPropertyChanged;

            this.timelineBarRegistry = timelineBarRegistry;
            this.View = view;

            this.View.SetDuration(this.sequenceRegistry.CurrentSequenceModel.Duration);
            this.timelineBarElements = new List<ITimelineBarElementModel>();

            this.Options = new List<string>();
            this.OptionSelectedCommand = new DelegateCommand<string>(this.AddSelectedOption);
            this.CloseCommand = new DelegateCommand<object>(this.CloseOptionsMenu);
            

            this.eventAggregator.GetEvent<PositionDoubleClickedEvent>().Subscribe(this.AddPreview, true);
            this.eventAggregator.GetEvent<RefreshElementsEvent>().Subscribe(this.RefreshPreviews, true);
            this.eventAggregator.GetEvent<AddPreviewEvent>().Subscribe(this.AddPreview, true);

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

        private void AddSelectedOption(string option)
        {
            this.IsOptionMenuVisible = false;
            this.AddPreview(option, this.lastKnownPosition);
        }

        private void CloseOptionsMenu(object obj)
        {
            this.IsOptionMenuVisible = false;
        }

        private void AddPreview(PositionPayloadEventArgs payload)
        {
            this.lastKnownPosition = payload.Position;
            this.IsOptionMenuVisible = false;

            IList<string> availableElements = this.timelineBarRegistry.GetTimelineBarElementKeys();

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
                this.HookModel(model);
                model.SetElement(payload.Value);
            }
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
                model.SetElement(element);
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
                this.View.AddPreview(model.Preview, model.Position, model.EditBox);
            }
        }

        private void RemovePreview(object sender, EventArgs e)
        {
            ITimelineBarElementModel model = sender as ITimelineBarElementModel;

            if (model != null)
            {
                RemovePreview(model);
                this.timelineBarElements.Remove(model);
            }
        }

        private void RemovePreview(ITimelineBarElementModel model)
        {
            this.View.RemovePreview(model.Preview, model.EditBox);
            model.Deleting -= this.RemovePreview;
            model.TimelineBarElementUpdated -= this.UpdatePreview;
        }

        private void UpdatePreview(object sender, EventArgs e)
        {
            ITimelineBarElementModel model = sender as ITimelineBarElementModel;

            if (model != null)
            {
                this.View.UpdatePreview(model.Preview, model.Position, model.EditBox);
            }
        }

        /// <summary>
        /// Refreshes the comments when timeline zoom In/Out happen.
        /// </summary>
        /// <param name="payload">The <see cref="RCE.Infrastructure.Events.RefreshElementsEventArgs"/> instance containing the event data.</param>
        private void RefreshPreviews(RefreshElementsEventArgs payload)
        {
            this.refreshPreviewWidth = payload.RefreshedWidth;

            this.View.RefreshPreviews(this.refreshPreviewWidth);

            foreach (ITimelineBarElementModel model in this.timelineBarElements)
            {
                model.RefreshPreview(this.refreshPreviewWidth);
            }
        }


        private void HandleCurrentSequenceChanged(object sender, Infrastructure.DataEventArgs<ISequenceModel> a)
        {
            if (a.Data != null)
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
            List<Comment> comments = new List<Comment>(this.sequenceRegistry.CurrentSequenceModel.CommentElements);
            
            foreach (Comment comment in comments)
            {
                this.AddPreview("Comment", comment);
            }

            this.View.SetDuration(this.sequenceRegistry.CurrentSequenceModel.Duration);
        }

        private void OnCurrentSequenceOnPropertyChanged(object s, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Duration")
            {
                this.View.SetDuration(this.sequenceRegistry.CurrentSequenceModel.Duration);
            }
        }
    }
}