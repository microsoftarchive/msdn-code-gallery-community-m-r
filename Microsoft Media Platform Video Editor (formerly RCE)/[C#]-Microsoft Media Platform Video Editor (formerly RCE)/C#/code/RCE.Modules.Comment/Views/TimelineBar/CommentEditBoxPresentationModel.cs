// <copyright file="CommentEditBoxPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentEditBoxPresentationModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Comment
{
    using System;
    using System.Collections.Specialized;
    using Events;
    using Infrastructure;
    using Infrastructure.Events;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Commands;
    using Microsoft.Practices.Composite.Presentation.Events;

    using RCE.Infrastructure.Services;

    using SMPTETimecode;

    public class CommentEditBoxPresentationModel : BaseModel, ICommentEditBoxPresentationModel
    {
        private readonly ICommentViewPreview preview;

        private readonly IEventAggregator eventAggregator;

        private readonly IConfigurationService configurationService;

        /// <summary>
        /// Default duration of the comment.
        /// </summary>
        private const int DefaultCommentDuration = 60;

        /// <summary>
        /// The current comment.
        /// </summary>
        private Comment comment;

        private double markIn;

        private double markOut;

        private string text;

        private ISequenceRegistry sequenceRegistry;

        public CommentEditBoxPresentationModel(ICommentEditBox view, ICommentViewPreview preview, IEventAggregator eventAggregator, ISequenceRegistry sequenceRegistry, IConfigurationService configurationService)
        {
            this.View = view;
            this.preview = preview;
            this.eventAggregator = eventAggregator;
            this.configurationService = configurationService;
            this.sequenceRegistry = sequenceRegistry;
            this.sequenceRegistry.CurrentSequence.CommentElements.CollectionChanged += this.CommentElements_CollectionChanged;
            this.preview.SetTimelineDuration(this.sequenceRegistry.CurrentSequenceModel.Duration);

            this.CloseCommand = new DelegateCommand<object>(this.Close);
            this.SaveCommand = new DelegateCommand<string>(this.Save, this.CanSave);
            this.DeleteCommand = new DelegateCommand<object>(this.Delete);
            this.PlayCommand = new DelegateCommand<object>(this.PlayComment);
            this.KeyboardActionCommand = new DelegateCommand<Tuple<KeyboardAction, object>>(this.ExecuteKeyboardAction, this.CanExecuteKeyboardAction);

            this.comment = this.CreateComment();
            this.sequenceRegistry.CurrentSequence.AddComment(this.comment);

            this.eventAggregator.GetEvent<CommentUpdatedEvent>().Subscribe(x => this.UpdateComment(), ThreadOption.PublisherThread, true, x => this.comment == x);

            this.View.Model = this;
            this.preview.Model = this;
        }

        public event EventHandler<EventArgs> TimelineBarElementUpdated;

        public event EventHandler<EventArgs> Deleting;

        public ICommentEditBox View { get; private set; }

        public DelegateCommand<Tuple<KeyboardAction, object>> KeyboardActionCommand { get; private set; }

        public KeyboardActionContext ActionContext
        {
            get
            {
                return KeyboardActionContext.CommentEdit;
            }
        }

        public object Preview
        {
            get { return this.preview; }
        }

        public object EditBox
        {
            get { return this.View; }
        }

        public DelegateCommand<object> CloseCommand { get; private set; }

        public DelegateCommand<string> SaveCommand { get; private set; }

        public DelegateCommand<object> DeleteCommand { get; private set; }

        public DelegateCommand<object> PlayCommand { get; private set; }

        public double MarkIn
        {
            get
            {
                return this.markIn;
            }

            set
            {
                this.markIn = value;
                this.OnPropertyChanged("MarkIn");
                this.SaveCommand.RaiseCanExecuteChanged();
                ValidateTime(this.MarkIn);
                ValidateRangeTime(this.MarkIn, this.MarkOut);
            }
        }

        public double MarkOut
        {
            get
            {
                return this.markOut;
            }

            set
            {
                this.markOut = value;
                this.OnPropertyChanged("MarkOut");
                this.SaveCommand.RaiseCanExecuteChanged();
                ValidateTime(this.MarkOut);
                ValidateRangeTime(this.MarkIn, this.MarkOut);
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

        public double Position
        {
            get { return this.MarkIn; }
        }

        public object DisplayBox
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Refreshes the comments when timeline zoom In/Out happen.
        /// </summary>
        /// <param name="refreshedWidth">The refreshed width</param>
        public void RefreshPreview(double refreshedWidth)
        {
            this.preview.RefreshPreview(refreshedWidth);
            this.OnTimelineBarElementUpdated();
        }

        public void SetElement(object value, CommentMode mode)
        {
            Comment newComment = value as Comment;

            if (newComment != null)
            {
                this.sequenceRegistry.CurrentSequence.CommentElements.CollectionChanged -= this.CommentElements_CollectionChanged;
                this.sequenceRegistry.CurrentSequence.CommentElements.Remove(this.comment);
                this.sequenceRegistry.CurrentSequence.CommentElements.CollectionChanged += this.CommentElements_CollectionChanged;
                this.comment = newComment;
                this.Text = this.comment.Text;
                this.SetPosition(TimeSpan.FromSeconds(this.comment.MarkIn.GetValueOrDefault()));
                this.View.Close();
            }
        }

        public void SetPosition(TimeSpan position)
        {
            double commentDuration = this.comment.MarkOut.GetValueOrDefault() - this.comment.MarkIn.GetValueOrDefault();

            this.comment.MarkIn = position.TotalSeconds;
            this.comment.MarkOut = position.TotalSeconds + commentDuration;

            this.MarkOut = this.comment.MarkOut.GetValueOrDefault();
            this.MarkIn = this.comment.MarkIn.GetValueOrDefault();

            this.OnTimelineBarElementUpdated();
            this.UpdateCommentDuration();
        }

        public T GetElement<T>() where T : class
        {
            return this.comment as T;
        }

        public void ShowEditBox()
        {
            this.MarkOut = this.comment.MarkOut.GetValueOrDefault();
            this.MarkIn = this.comment.MarkIn.GetValueOrDefault();
            this.Text = this.comment.Text;

            this.View.Show();
        }

        private static void ValidateTime(double time)
        {
            if (double.IsNaN(time) || double.IsInfinity(time) || time < 0)
            {
                throw new InputValidationException("Value is not valid.");
            }
        }

        private static void ValidateRangeTime(double markIn, double markOut)
        {
            if (markIn > markOut)
            {
                throw new InputValidationException("Value is not valid.");
            }
        }

        private void UpdateCommentDuration()
        {
            TimeCode durationTimeCode = TimeCode.FromSeconds(this.comment.MarkOut.GetValueOrDefault() - this.comment.MarkIn.GetValueOrDefault(), this.sequenceRegistry.CurrentSequenceModel.Duration.FrameRate);

            this.preview.UpdateCommentDuration(durationTimeCode);
        }

        private bool CanExecuteKeyboardAction(Tuple<KeyboardAction, object> arg)
        {
            return this.SaveCommand.CanExecute(string.Empty);
        }

        private void ExecuteKeyboardAction(Tuple<KeyboardAction, object> parameter)
        {
            switch (parameter.Item1)
            {
                case KeyboardAction.Save:
                    this.Save(parameter.Item2.ToString());
                    break;
            }
        }

        private bool CanSave(string parameter)
        {
            try
            {
                ValidateTime(this.MarkIn);
                ValidateTime(this.MarkOut);
                ValidateRangeTime(this.MarkIn, this.MarkOut);

                return true;
            }
            catch (InputValidationException)
            {
                return false;
            }
        }

        private void Save(string parameter)
        {
            if (this.CanSave(parameter))
            {
                this.comment.MarkIn = this.MarkIn;
                this.comment.MarkOut = this.MarkOut;
                this.comment.Text = parameter;

                this.OnTimelineBarElementUpdated();
                this.UpdateCommentDuration();
                this.View.Close();
            }
        }

        private void Delete(object obj)
        {
            this.sequenceRegistry.CurrentSequence.CommentElements.Remove(this.comment);
        }

        private void Close(object obj)
        {
            this.View.Close();
        }

        /// <summary>
        /// Plays the comment.
        /// </summary>
        private void PlayComment(object obj)
        {
            this.eventAggregator.GetEvent<PlayCommentEvent>().Publish(this.comment);
            this.View.Close();
        }

        private void UpdateComment()
        {
            this.MarkOut = this.comment.MarkOut.GetValueOrDefault();
            this.MarkIn = this.comment.MarkIn.GetValueOrDefault();
            this.OnTimelineBarElementUpdated();
            this.UpdateCommentDuration();
        }

        private void OnDeleting()
        {
            EventHandler<EventArgs> handler = this.Deleting;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
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

        private Comment CreateComment()
        {
            return new Comment
                       {
                           CommentType = CommentType.Timeline,
                           Text = string.Empty,
                           MarkIn = 0,
                           MarkOut = DefaultCommentDuration,
                           Created = DateTime.Now,
                           Creator = this.configurationService.GetUserName()
                       };
        }

        /// <summary>
        /// Handles the CollectionChanged event of the CommentElements collection.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void CommentElements_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                Comment deletedComment = e.OldItems[0] as Comment;

                if (deletedComment != null && deletedComment == this.comment)
                {
                    this.sequenceRegistry.CurrentSequence.CommentElements.CollectionChanged -= this.CommentElements_CollectionChanged;

                    this.View.Close();

                    this.OnDeleting();
                }
            }
        }
    }
}
