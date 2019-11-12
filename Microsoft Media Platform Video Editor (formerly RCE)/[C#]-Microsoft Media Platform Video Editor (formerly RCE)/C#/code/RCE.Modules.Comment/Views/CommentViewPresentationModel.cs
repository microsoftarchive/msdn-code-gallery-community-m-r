// <copyright file="CommentViewPresentationModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentViewPresentationModel.cs                     
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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Ink;
    using Events;
    using Infrastructure;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Commands;
    using Microsoft.Practices.Composite.Presentation.Events;
    using Microsoft.Practices.Composite.Regions;
    using Models;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Services;
    using RCE.Infrastructure.Windows;
    using SMPTETimecode;
    
    /// <summary>
    /// Presentation model for the comment view.
    /// </summary>
    public class CommentViewPresentationModel : BaseModel, ICommentViewPresentationModel, IWindowMetadataProvider
    {
        /// <summary>
        /// The default duration of the comment.
        /// </summary>
        private const int DefaultCommentDuration = 60;

        /// <summary>
        /// The duration of the comment.
        /// </summary>
        private readonly int commentDuration;

        /// <summary>
        /// The <seealso cref="IEventAggregator"/> instance used to publish and subscribe to events.
        /// </summary>
        private readonly IEventAggregator eventAggregator;

        /// <summary>
        /// The <seealso cref="IConfigurationService"/> instance used to retrieve the configuration settings.
        /// </summary>
        private readonly IConfigurationService configurationService;

        /// <summary>
        /// The <seealso cref="IProjectService"/> instance used to save the current project.
        /// </summary>
        private readonly IProjectService projectService;
        
        /// <summary>
        /// The <see cref="IRegionManager"/> to activate the view.
        /// </summary>
        private readonly IRegionManager regionManager;

        /// <summary>
        /// The <see cref="IThumbnailService"/> service to retrieve thumbnails.
        /// </summary>
        private readonly IThumbnailService thumbnailService;

        /// <summary>
        /// Command used to search comments.
        /// </summary>
        private readonly DelegateCommand<string> searchCommand;

        /// <summary>
        /// Command used to delete a comment.
        /// </summary>
        private readonly DelegateCommand<object> deleteCommand;

        /// <summary>
        /// Command used to save a comment.
        /// </summary>
        private readonly DelegateCommand<Guid> saveCommand;
        
        /// <summary>
        /// Command used to edit a comment.
        /// </summary>
        private readonly DelegateCommand<object> editCommand;
        
        /// <summary>
        /// Command used to play a comment. 
        /// </summary>
        private readonly DelegateCommand<object> playCommentCommand;

        private readonly ISequenceRegistry sequenceRegistry;

        /// <summary>
        /// Command used to cancel the edit of a comment.
        /// </summary>
        private readonly DelegateCommand<string> cancelCommand;
        
        /// <summary>
        /// Maintains the current comments of the project.
        /// </summary>
        private List<Comment> currentComments;

        /// <summary>
        /// The comments being shown on the view.
        /// </summary>
        private ObservableCollection<Comment> comments;

        /// <summary>
        /// Maintains the current comment being edited.
        /// </summary>
        private Comment currentComment;

        /// <summary>
        /// Maintains the stroke points of an ink comment.
        /// </summary>
        private StrokeCollection inkCommentStrokes;

        /// <summary>
        /// Flag used to determine if the view is in edit mode or not.
        /// </summary>
        private bool editMode;
        
        /// <summary>
        /// Used to maintain the frame image location. 
        /// </summary>
        private string frameImage;

        /// <summary>
        /// Used to maintain the comment text.
        /// </summary>
        private string text;

        /// <summary>
        /// Used to maintain the selected comment type.
        /// </summary>
        private string selectedCommentType;

        /// <summary>
        /// Flag to show the global comments.
        /// </summary>
        private bool showGlobalComments;

        /// <summary>
        /// Flag to show the timeline comments.
        /// </summary>
        private bool showTimelineComments;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentViewPresentationModel"/> class.
        /// </summary>
        /// <param name="view">The <see cref="ICommentView"/>.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="projectService">The project service.</param>
        /// <param name="sequenceRegistry">The seqeunce registry.</param>
        /// <param name="regionManager">The <see cref="IRegionManager"/> to activate the comment view.</param>
        /// <param name="thumbnailService">The <see cref="IThumbnailService"/> used to retrieve thumbnails.</param>
        public CommentViewPresentationModel(
            ICommentView view,
            IEventAggregator eventAggregator,
            IConfigurationService configurationService,
            IProjectService projectService,
            ISequenceRegistry sequenceRegistry,
            IRegionManager regionManager,
            IThumbnailService thumbnailService)
        {
            this.eventAggregator = eventAggregator;
            this.configurationService = configurationService;
            this.projectService = projectService;
            this.sequenceRegistry = sequenceRegistry;
            this.regionManager = regionManager;
            this.thumbnailService = thumbnailService;
            this.View = view;
            this.currentComment = new Comment(Guid.Empty);
            this.searchCommand = new DelegateCommand<string>(this.Search);
            this.deleteCommand = new DelegateCommand<object>(this.Delete);
            this.saveCommand = new DelegateCommand<Guid>(this.Save);
            this.cancelCommand = new DelegateCommand<string>(this.Cancel);
            this.editCommand = new DelegateCommand<object>(this.Edit);
            this.playCommentCommand = new DelegateCommand<object>(this.PlayComment);
            this.KeyboardActionCommand = new DelegateCommand<Tuple<KeyboardAction, object>>(this.ExecuteKeyboardAction, this.CanExecuteKeyboardAction);
            this.EditMode = false;
            this.InkCommentStrokes = new StrokeCollection();
            this.ShowGlobalComments = true;
            this.ShowTimelineComments = true;

            this.commentDuration = this.configurationService.GetCommentDuration() ?? DefaultCommentDuration;

            this.eventAggregator.GetEvent<ElementMovedEvent>().Subscribe(this.MoveComments, true);

            this.eventAggregator.GetEvent<DisplayMarkerBrowserWindowEvent>().Subscribe(this.DisplayView, ThreadOption.PublisherThread, true, this.FilterDisplayMarkerBrowserWindowEvent);

            this.eventAggregator.GetEvent<ResetWindowsEvent>().Subscribe(this.ResetWindow);
            this.CommentsTypes = this.GetCommentTypes();

            this.PropertyChanged += this.CommentViewPresentationModel_PropertyChanged;

            if (sequenceRegistry.CurrentSequenceModel != null)
            {
                this.sequenceRegistry.CurrentSequence.CommentElements.CollectionChanged += this.CommentElements_CollectionChanged;
                this.sequenceRegistry.CurrentSequenceModel.ElementRemoved += this.OnCurrentSequenceOnElementRemoved;
            }

            this.sequenceRegistry.CurrentSequenceChanged += this.HandleCurrentSequenceChanged;

            this.LoadComments();

            this.View.Model = this;
        }

        public event EventHandler<Infrastructure.DataEventArgs<object>> TitleUpdated;

        public event EventHandler<Infrastructure.DataEventArgs<object>> ResetPositionRaised;

        public VerticalWindowPosition VerticalPosition
        {
            get
            {
                return VerticalWindowPosition.Center;
            }
        }

        public HorizontalWindowPosition HorizontalPosition
        {
            get
            {
                return HorizontalWindowPosition.Center;
            }
        }

        public object Title
        {
            get
            {
                return "Comments";
            }
        }

        public ResizeDirection ResizeDirection
        {
            get
            {
                return Infrastructure.Windows.ResizeDirection.Both;
            }
        }

        public Size Size
        {
            get
            {
                return System.Windows.Size.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>The <see cref="ICommentView"/>.</value>
        public ICommentView View { get; set; }

        /// <summary>
        /// Gets the header info.
        /// </summary>
        /// <value>A value that represents the header info.</value>
        public string HeaderInfo
        {
            get { return Resources.Resources.HeaderInfo; }
        }

        /// <summary>
        /// Gets the header icon (on status).
        /// </summary>
        /// <value>An <seealso cref="string" /> that represents the header icon on resource.</value>
        public string HeaderIconOn
        {
            get { return Resources.Resources.HeaderIconOn; }
        }

        /// <summary>
        /// Gets the header icon (off status).
        /// </summary>
        /// <value>An <seealso cref="string" /> that represents the header icon off resource.</value>
        public string HeaderIconOff
        {
            get { return Resources.Resources.HeaderIconOff; }
        }

        /// <summary>
        /// Gets or sets the comments types.
        /// </summary>
        /// <value>The comments types.</value>
        public IList<string> CommentsTypes { get; set; }

        /// <summary>
        /// Gets or sets the currently selected comment type.
        /// </summary>
        /// <value>The type of the selected comment.</value>
        public string SelectedCommentType
        {
            get
            {
                return this.selectedCommentType;
            }

            set
            {
                this.selectedCommentType = value;
                this.OnPropertyChanged("SelectedCommentType");
            }
        }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>The comments.</value>
        public ObservableCollection<Comment> Comments
        {
            get
            {
                return this.comments;
            }

            set
            {
                this.comments = value;
                this.OnPropertyChanged("Comments");
            }
        }

        /// <summary>
        /// Gets or sets the current comment.
        /// </summary>
        /// <value>The current comment.</value>
        public Comment CurrentComment
        {
            get
            {
                return this.currentComment;
            }

            set
            {
                this.currentComment = value;
                if (value != null)
                {
                    this.OnPropertyChanged("CurrentComment");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the current mode is edit.
        /// </summary>
        /// <value>A <seealso cref="bool"/> that represents current mode is edit. true, indicates current mode is edit. false, that is not.</value>
        public bool EditMode
        {
            get
            {
                return this.editMode;
            }

            set
            {
                this.editMode = value;
                this.OnPropertyChanged("EditMode");
            }
        }

        /// <summary>
        /// Gets or sets the ink comment strokes.
        /// </summary>
        /// <value>The ink comment strokes.</value>
        public StrokeCollection InkCommentStrokes
        {
            get
            {
                return this.inkCommentStrokes;
            }

            set
            {
                this.inkCommentStrokes = value;
                this.OnPropertyChanged("InkCommentStrokes");
            }
        }

        /// <summary>
        /// Gets or sets the frame image.
        /// </summary>
        /// <value>The frame image.</value>
        public string FrameImage
        {
            get
            {
                return this.frameImage;
            }

            set
            {
                this.frameImage = value;
                this.OnPropertyChanged("FrameImage");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the global comments should be showed in the comments view.
        /// </summary>
        /// <value>A <seealso cref="bool"/> that represents if the global comments should be show. true, indicates that the global comments is showed. false, that is not showed.</value>
        public bool ShowGlobalComments
        {
            get
            {
                return this.showGlobalComments;
            }

            set
            {
                this.showGlobalComments = value;
                this.OnPropertyChanged("ShowGlobalComments");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the timeline comments should be showed in the comments view.
        /// </summary>
        /// <value>A <seealso cref="bool"/> that represents if the timeline comments should be show. true, indicates that the global comments is showed. false, that is not showed.</value>
        public bool ShowTimelineComments
        {
            get
            {
                return this.showTimelineComments;
            }

            set
            {
                this.showTimelineComments = value;
                this.OnPropertyChanged("ShowTimelineComments");
            }
        }

        /// <summary>
        /// Gets the search command.
        /// </summary>
        /// <value>The search command.</value>
        public DelegateCommand<string> SearchCommand
        {
            get { return this.searchCommand; }
        }

        /// <summary>
        /// Gets the command that deletes the selected comment.
        /// </summary>
        /// <value>The <see cref="DelegateCommand{T}"/>.</value>
        public DelegateCommand<object> DeleteCommand
        {
            get { return this.deleteCommand; }
        }

        /// <summary>
        /// Gets the command that save the comment.
        /// </summary>
        /// <value>The <see cref="DelegateCommand{T}"/>.</value>
        public DelegateCommand<Guid> SaveCommand
        {
            get { return this.saveCommand; }
        }

        /// <summary>
        /// Gets the command that edit the current selected comment.
        /// </summary>
        /// <value>The <see cref="DelegateCommand{T}"/>.</value>
        public DelegateCommand<object> EditCommand
        {
            get { return this.editCommand; }
        }

        /// <summary>
        /// Gets the command that plays the current selected command.
        /// </summary>
        /// <value>The <see cref="DelegateCommand{T}"/>.</value>
        public DelegateCommand<object> PlayCommentCommand
        {
            get { return this.playCommentCommand; }
        }

        /// <summary>
        /// Gets the command that save the comment.
        /// </summary>
        /// <value>The <see cref="DelegateCommand{T}"/>.</value>
        public DelegateCommand<string> CancelCommand
        {
            get { return this.cancelCommand; }
        }

        public DelegateCommand<Tuple<KeyboardAction, object>> KeyboardActionCommand { get; private set; }

        public KeyboardActionContext ActionContext
        {
            get
            {
                return KeyboardActionContext.Comment;
            }
        }

        /// <summary>
        /// Gets or sets the text of the current comment.
        /// </summary>
        /// <value>An <seealso cref="string"/> that represents the text of the current comment.</value>
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
        
        /// <summary>
        /// Gets a value indicating whether the Comment is the active view.
        /// </summary>
        /// <value>True if Comment is active else false.</value>
        private bool IsActive
        {
            get
            {
                return this.regionManager.Regions[RegionNames.MainRegion].ActiveViews.Where(x => x == this.View).SingleOrDefault() != null;
            }
        }

        /// <summary>
        /// Plays the current comment.
        /// </summary>
        public void PlayComment()
        {
            if (this.CurrentComment != null)
            {
                this.eventAggregator.GetEvent<PlayCommentEvent>().Publish(this.CurrentComment);
            }
        }

        public void DisplayView(SelectedMarkersBrowserTab selectedMarkersBrowserTab)
        {
            this.regionManager.Regions[RegionNames.MarkerBrowserRegion].Activate(this.View);
        }

        public void ResetWindow(object obj)
        {
            EventHandler<Infrastructure.DataEventArgs<object>> handler = this.ResetPositionRaised;

            if (handler != null)
            {
                handler.Invoke(this, new Infrastructure.DataEventArgs<object>(this.View));
            }
        }

        /// <summary>
        /// Gets the comment types.
        /// </summary>
        /// <returns>The list of comment types.</returns>
        private IList<string> GetCommentTypes()
        {
            IList<string> commentTypes = new List<string>();

            foreach (string commentType in this.configurationService.GetCommentTypes())
            {
                commentTypes.Add(string.Format(CultureInfo.InvariantCulture, Resources.Resources.CommentTypePattern, commentType));
            }

            return commentTypes;
        }

        /// <summary>
        /// Handles the CollectionChanged event of the CommentElements collection.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void CommentElements_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Comment comment;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null && e.NewItems.Count > 0)
                    {
                        comment = e.NewItems[0] as Comment;
                        if (comment != null && !this.Comments.Contains(comment))
                        {
                            this.comments.Add(comment);
                            this.currentComments.Add(comment);
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null && e.OldItems.Count > 0)
                    {
                        comment = e.OldItems[0] as Comment;
                        if (comment != null && this.Comments.Contains(comment))
                        {
                            if (comment == this.currentComment)
                            {
                                this.currentComment = new Comment(Guid.Empty);
                            }

                            this.Comments.Remove(comment);
                            this.currentComments.Remove(comment);
                        }

                        this.RemoveCommentAssociationFromElement(comment);
                    }

                    break;
            }
        }

        /// <summary>
        /// Removes the comment association from <see cref="TimelineElement"/>.
        /// </summary>
        /// <param name="comment">The comment.</param>
        private void RemoveCommentAssociationFromElement(Comment comment)
        {
            if (comment.CommentType == CommentType.Ink || comment.CommentType == CommentType.Shot)
            {
                Track track = this.sequenceRegistry.CurrentSequenceModel.Tracks.Where(x => x.TrackType == TrackType.Visual).FirstOrDefault();

                if (track != null)
                {
                    TimelineElement element = track.Shots.Where(x => x.Comments.Contains(comment)).FirstOrDefault();

                    if (element != null)
                    {
                        element.Comments.Remove(comment);
                    }
                }
            }
        }

        /// <summary>
        /// Searches the filter string data.
        /// </summary>
        /// <param name="filter">The filter.</param>
        private void Search(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                ObservableCollection<Comment> commentsCollection = new ObservableCollection<Comment>();
                this.currentComments.ForEach(commentsCollection.Add);

                this.Comments = commentsCollection;
            }
            else
            {
                filter = filter.ToUpper(CultureInfo.InvariantCulture);

                List<Comment> results =
                    this.currentComments.Where(x => x.Text.ToUpper(CultureInfo.InvariantCulture).Contains(filter) ||
                                             x.Creator.ToUpper(CultureInfo.InvariantCulture).Contains(filter)).ToList();

                this.Comments.Clear();
                results.ForEach(x => this.Comments.Add(x));
            }
        }

        /// <summary>
        /// Deletes the comments with the specified comment id.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        private void Delete(object commentId)
        {
            if (commentId is Guid)
            {
                Comment comment = this.currentComments.Where(x => x.CommentId == (Guid)commentId).SingleOrDefault();

                if (comment != null)
                {
                    this.DeleteComment(comment);
                }

                this.EditMode = false;
                this.SelectedCommentType = null;
            }
        }

        /// <summary>
        /// Saves the ink comment.
        /// </summary>
        private void SaveInkComment()
        {
            TimelineElement element = this.GetElementAtCurrentPosition();

            if (element == null)
            {
                this.EditMode = false;
                this.SelectedCommentType = null;
                this.View.ShowErrorMessage(string.Format(CultureInfo.InvariantCulture, Resources.Resources.NoVisualAssetAssociated, Resources.Resources.InkComment));
            }
            else
            {
                double markIn = this.sequenceRegistry.CurrentSequenceModel.CurrentPosition.TotalSeconds;
                double markOut = element.Position.TotalSeconds + element.Duration.TotalSeconds;

                StrokeCollection strokes = new StrokeCollection();

                if (this.InkCommentStrokes != null)
                {
                    foreach (Stroke stroke in this.InkCommentStrokes)
                    {
                        strokes.Add(stroke);
                    }
                }

                this.CurrentComment = new InkComment
                                          {
                                              CommentType = this.CurrentComment == null ? CommentType.Ink : this.CurrentComment.CommentType,
                                              Text = this.Text ?? string.Empty,
                                              Creator = this.configurationService.GetUserName(),
                                              Created = DateTime.Now,
                                              Modified = DateTime.Now,
                                              MarkIn = markIn,
                                              MarkOut = markOut,
                                              InkCommentStrokes = strokes
                                          };

                element.Comments.Add(this.CurrentComment);
                this.AddComment(this.CurrentComment);
            }
        }

        /// <summary>
        /// Saves the shot comment.
        /// </summary>
        private void SaveShotComment()
        {
            TimelineElement element = this.GetElementAtCurrentPosition();

            if (element == null)
            {
                this.EditMode = false;
                this.SelectedCommentType = null;
                this.View.ShowErrorMessage(string.Format(CultureInfo.InvariantCulture, Resources.Resources.NoVisualAssetAssociated, Resources.Resources.ShotComment));
            }
            else
            {
                double markIn = this.sequenceRegistry.CurrentSequenceModel.CurrentPosition.TotalSeconds;
                double markOut = element.Position.TotalSeconds + element.Duration.TotalSeconds;

                this.CurrentComment = new Comment
                {
                    CommentType = this.CurrentComment == null ? CommentType.Shot : this.CurrentComment.CommentType,
                    Text = this.Text ?? string.Empty,
                    Creator = this.configurationService.GetUserName(),
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    MarkIn = markIn,
                    MarkOut = markOut
                };

                element.Comments.Add(this.CurrentComment);
                this.AddComment(this.CurrentComment);
            }
        }

        /// <summary>
        /// Saves the comment with the specified comment id.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        private void Save(Guid commentId)
        {
            Comment comment = this.currentComments.SingleOrDefault(x => x.CommentId == commentId);

            if (comment != null)
            {
                this.UpdateComment(comment);
            }
            else if (commentId == Guid.Empty)
            {
                if (this.CurrentComment.CommentType == CommentType.Ink)
                {
                    this.SaveInkComment();   
                }
                else if (this.CurrentComment.CommentType == CommentType.Shot)
                {
                    this.SaveShotComment();
                }
                else
                {
                    this.CurrentComment = new Comment
                    {
                        CommentType = this.CurrentComment == null ? CommentType.Global : this.CurrentComment.CommentType,
                        Text = this.Text ?? string.Empty,
                        Creator = this.configurationService.GetUserName(),
                        Created = DateTime.Now,
                        Modified = DateTime.Now
                    };

                    if (this.CurrentComment.CommentType == CommentType.Timeline)
                    {
                        this.CurrentComment.MarkIn = this.sequenceRegistry.CurrentSequenceModel.CurrentPosition.TotalSeconds;
                        this.CurrentComment.MarkOut = this.CurrentComment.MarkIn + this.commentDuration; 
                    }

                    this.AddComment(this.CurrentComment);
                }
            }

            this.Reset();
        }

        /// <summary>
        /// Update an existing comment.
        /// </summary>
        /// <param name="comment">The comment being updated.</param>
        private void UpdateComment(Comment comment)
        {
            InkComment inkComment = comment as InkComment;

            if (inkComment != null)
            {
                var strokes = new StrokeCollection();

                if (this.InkCommentStrokes != null)
                {
                    foreach (Stroke stroke in this.InkCommentStrokes)
                    {
                        strokes.Add(stroke);
                    }
                }

                inkComment.Text = this.Text ?? string.Empty;
                inkComment.Modified = DateTime.Now;
                inkComment.InkCommentStrokes = strokes;
            }
            else
            {
                comment.Text = this.Text ?? string.Empty;
                comment.Modified = DateTime.Now;
            }
        }

        /// <summary>
        /// Adds the comment to the comment collection.
        /// </summary>
        /// <param name="comment">The comment.</param>
        private void AddComment(Comment comment)
        {
            this.Comments.Add(comment);
            this.currentComments.Add(comment);

            if (comment.CommentType != CommentType.Global)
            {
                this.sequenceRegistry.CurrentSequence.AddComment(this.currentComment);
                this.eventAggregator.GetEvent<AddPreviewEvent>().Publish(new AddPreviewPayload("Comment", this.currentComment, CommentMode.Timeline));
            }

            this.FilterComments();
        }

        /// <summary>
        /// Deletes the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        private void DeleteComment(Comment comment)
        {
            this.Comments.Remove(comment);
            this.currentComments.Remove(comment);

            if (comment.CommentType != CommentType.Global)
            {
                this.sequenceRegistry.CurrentSequence.CommentElements.Remove(comment);
            }
        }

        /// <summary>
        /// Edits the comment with the specified id.
        /// </summary>
        /// <param name="id">The id of the comment.</param>
        private void Edit(object id)
        {
            if (id is Guid)
            {
                Guid commentId = (Guid)id;

                Comment comment = this.currentComments.SingleOrDefault(x => x.CommentId == commentId);

                if (comment != null)
                {
                    this.CurrentComment = comment;
                    this.Text = comment.Text;
                    this.EditMode = true;

                    InkComment inkComment = comment as InkComment;

                    if (inkComment != null)
                    {
                        var strokes = new StrokeCollection();

                        foreach (var stroke in inkComment.InkCommentStrokes)
                        {
                            strokes.Add(stroke);
                        }

                        TimelineElement element = this.GetElementAssociatedToComment(inkComment);

                        if (element != null)
                        {
                            TimeCode markInTimeCode = TimeCode.FromSeconds(inkComment.MarkIn.GetValueOrDefault(), this.sequenceRegistry.CurrentSequenceModel.Duration.FrameRate);

                            TimeCode currentFramePosition = (markInTimeCode - element.Position) + element.InPosition;

                            this.FrameImage = this.thumbnailService.GetThumbnailSource(element.Asset, currentFramePosition);
                        }

                        this.InkCommentStrokes = strokes;
                        this.View.SetInkEditingMode(InkEditingMode.Ink);
                    }
                }
            }
        }

        /// <summary>
        /// Plays the comment with the specified id.
        /// </summary>
        /// <param name="id">The id of the comment.</param>
        private void PlayComment(object id)
        {
            if (id is Guid)
            {
                Guid commentId = (Guid)id;

                Comment comment = this.currentComments.Where(x => x.CommentId == commentId).SingleOrDefault();

                if (comment != null)
                {
                    this.eventAggregator.GetEvent<PlayCommentEvent>().Publish(comment);
                }
            }
        }

        /// <summary>
        /// Cancels the edit of the comment with the given id.
        /// </summary>
        /// <param name="id">The id of the comment.</param>
        private void Cancel(string id)
        {
            this.Reset();
        }

        /// <summary>
        /// Resets the settings in order to be able to create new comments.
        /// </summary>
        private void Reset()
        {
            this.EditMode = false;
            this.InkCommentStrokes = null;
            this.Text = string.Empty;
            this.SelectedCommentType = null;
            this.View.ClearInkComment();
        }

        /// <summary>
        /// Adds the global comment.
        /// </summary>
        private void AddGlobalComment()
        {
            this.CurrentComment = new Comment(Guid.Empty)
                                      {
                                          Creator = this.configurationService.GetUserName(), 
                                          CommentType = CommentType.Global,
                                          MarkIn = null,
                                          MarkOut = null,
                                          Modified = DateTime.Now
                                      };

            this.Text = this.CurrentComment.Text;
            this.EditMode = true;
        }

        /// <summary>
        /// Adds the playhead comment.
        /// </summary>
        private void AddPlayheadComment()
        {
            this.CurrentComment = new Comment(Guid.Empty) 
                                    { 
                                        Creator = this.configurationService.GetUserName(), 
                                        CommentType = CommentType.Timeline,
                                        Modified = DateTime.Now
                                    };

            this.Text = this.CurrentComment.Text;
            this.EditMode = true;
        }

        /// <summary>
        /// Adds the shot comment.
        /// </summary>
        private void AddShotComment()
        {
            TimelineElement element = this.GetElementAtCurrentPosition();

            if (element == null)
            {
                this.EditMode = false;
                this.View.ShowErrorMessage(string.Format(CultureInfo.InvariantCulture, Resources.Resources.NoVisualAssetAssociated, Resources.Resources.ShotComment));
                this.SelectedCommentType = null;
            }
            else
            {
                this.CurrentComment = new Comment(Guid.Empty)
                                          {
                                              Creator = this.configurationService.GetUserName(),
                                              CommentType = CommentType.Shot,
                                              Modified = DateTime.Now
                                          };
                this.Text = this.CurrentComment.Text;
                this.EditMode = true;
            }
        }

        /// <summary>
        /// Adds the ink comment.
        /// </summary>
        private void AddInkComment()
        {
            TimelineElement element = this.GetElementAtCurrentPosition();

            if (element == null)
            {
                this.EditMode = false;
                this.SelectedCommentType = null;
                this.View.ShowErrorMessage(string.Format(CultureInfo.InvariantCulture, Resources.Resources.NoVisualAssetAssociated, Resources.Resources.InkComment));
            }
            else
            {
                TimeCode currentFramePosition = (this.sequenceRegistry.CurrentSequenceModel.CurrentPosition - element.Position) + element.InPosition;
                this.FrameImage = this.thumbnailService.GetThumbnailSource(element.Asset, currentFramePosition);

                this.CurrentComment = new InkComment(Guid.Empty)
                                    { 
                                        Creator = this.configurationService.GetUserName(), 
                                        CommentType = CommentType.Ink,
                                        Modified = DateTime.Now
                                    };

                this.Text = this.CurrentComment.Text;
                this.EditMode = true;
            }
        }

        /// <summary>
        /// Get the element associated to the Ink/Shot comment.
        /// </summary>
        /// <param name="comment">The comment associated to an element.</param>
        /// <returns>The element associated or null.</returns>
        private TimelineElement GetElementAssociatedToComment(Comment comment)
        {
            if (comment.CommentType == CommentType.Ink || comment.CommentType == CommentType.Shot)
            {
                Track track = this.sequenceRegistry.CurrentSequenceModel.Tracks.Where(x => x.TrackType == TrackType.Visual).FirstOrDefault();

                if (track != null)
                {
                    TimelineElement element = track.Shots.Where(x => x.Comments.Contains(comment)).FirstOrDefault();

                    return element;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the element at current position from the timeline.
        /// </summary>
        /// <returns>The <see cref="TimelineElement"/>.</returns>
        private TimelineElement GetElementAtCurrentPosition()
        {
            TimelineElement element;
            Track visualTrack = this.sequenceRegistry.CurrentSequenceModel.Tracks.Where(x => x.TrackType == TrackType.Visual).FirstOrDefault();

            if (visualTrack != null)
            {
                element = this.sequenceRegistry.CurrentSequenceModel.GetElementAtPosition(this.sequenceRegistry.CurrentSequenceModel.CurrentPosition, visualTrack, null);

                if (element != null && element.Asset is VideoAsset)
                {
                    return element;
                }
            }

            return null;
        }

        /// <summary>
        /// Loads the comments.
        /// </summary>
        private void LoadComments()
        {
            if (this.projectService.State != ProjectState.Retrieved)
            {
                this.projectService.ProjectRetrieved += (sender, e) => this.LoadComments(this.projectService.GetCurrentProject());
            }
            else
            {
                this.LoadComments(this.projectService.GetCurrentProject());
            }
        }

        /// <summary>
        /// Loads the comments.
        /// </summary>
        /// <param name="project">The current project.</param>
        private void LoadComments(Project project)
        {
            if (project != null)
            {
                ObservableCollection<Comment> projectComments = project.Comments;

                this.Comments = projectComments;
                
                this.currentComments = new List<Comment>(projectComments);

                foreach (Comment comment in this.currentComments)
                {
                    if (comment.CommentType != CommentType.Global)
                    {
                        this.sequenceRegistry.CurrentSequence.AddComment(comment);
                    }
                }
            }
        }

        /// <summary>
        /// Filters the comments.
        /// </summary>
        private void FilterComments()
        {
            List<Comment> filteredComments = new List<Comment>();
            
            if (this.ShowGlobalComments)
            {
                filteredComments.AddRange(this.currentComments.Where(x => x.CommentType == CommentType.Global));
            }
            
            if (this.ShowTimelineComments)
            {
                filteredComments.AddRange(this.currentComments.Where(x => x.CommentType != CommentType.Global));
            }

            this.Comments.Clear();
            filteredComments.ForEach(x => this.Comments.Add(x));
         }

        /// <summary>
        /// Handles the PropertyChanged event of the CommentViewPresentationModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void CommentViewPresentationModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SelectedCommentType":
                    if (this.SelectedCommentType != null && this.SelectedCommentType.Contains("Global"))
                    {
                        this.AddGlobalComment();
                    }
                    else if (this.SelectedCommentType != null && this.SelectedCommentType.Contains("Playhead"))
                    {
                        this.AddPlayheadComment();
                    }
                    else if (this.SelectedCommentType != null && this.SelectedCommentType.Contains("Ink"))
                    {
                        this.AddInkComment();
                    }
                    else if (this.SelectedCommentType != null && this.SelectedCommentType.Contains("Shot"))
                    {
                        this.AddShotComment();
                    }

                    break;
                case "ShowGlobalComments":
                case "ShowTimelineComments":
                    if (e.PropertyName.Equals("ShowGlobalComments") || e.PropertyName.Equals("ShowTimelineComments"))
                    {
                        this.FilterComments();
                    }

                    break;
            }
        }

        /// <summary>
        /// Moves the comments.
        /// </summary>
        /// <param name="payload">The payload.</param>
        private void MoveComments(ElementMovedPayload payload)
        {
            this.MoveCommentsAssociatedToElement(payload.Element, payload.PositionType, payload.OldPosition, payload.NewPosition);
        }

        /// <summary>
        /// Moves the comments associated to given <see cref="TimelineElement"/>.
        /// </summary>
        /// <param name="element">The <see cref="SequenceModel"/>.</param>
        /// <param name="positionType">Type of the position.</param>
        /// <param name="oldPosition">The old position.</param>
        /// <param name="newPosition">The new position.</param>
        private void MoveCommentsAssociatedToElement(TimelineElement element, ElementPositionType positionType, TimeCode oldPosition, TimeCode newPosition)
        {
            if (element.Asset != null && element.Asset is VideoAsset)
            {
                foreach (Comment comment in element.Comments)
                {
                    if (newPosition > oldPosition)
                    {
                        if (positionType != ElementPositionType.OutPosition)
                        {
                            comment.MarkIn += newPosition.TotalSeconds - oldPosition.TotalSeconds;
                        }

                        if (positionType != ElementPositionType.InPosition)
                        {
                            comment.MarkOut += newPosition.TotalSeconds - oldPosition.TotalSeconds;
                        }
                    }
                    else if (newPosition < oldPosition)
                    {
                        if (positionType != ElementPositionType.OutPosition)
                        {
                            comment.MarkIn -= oldPosition.TotalSeconds - newPosition.TotalSeconds;
                        }

                        if (positionType != ElementPositionType.InPosition)
                        {
                            comment.MarkOut -= oldPosition.TotalSeconds - newPosition.TotalSeconds;
                        }
                    }

                    this.eventAggregator.GetEvent<CommentUpdatedEvent>().Publish(comment);
                }
            }
        }

        /// <summary>
        /// Removes the comments associated to <see cref="TimelineElement"/>.
        /// </summary>
        /// <param name="element">The element.</param>
        private void RemoveCommentsAssociatedToElement(TimelineElement element)
        {
            if (element.Asset != null && element.Asset is VideoAsset)
            {
                element.Comments.ToList().ForEach(this.DeleteComment);
                element.Comments.Clear();
            }
        }

        private bool CanExecuteKeyboardAction(Tuple<KeyboardAction, object> tuple)
        {
            return this.IsActive || tuple.Item1 == KeyboardAction.ActivateModel;
        }

        private void ExecuteKeyboardAction(Tuple<KeyboardAction, object> parameter)
        {
            switch (parameter.Item1)
            {
                case KeyboardAction.ActivateModel:
                    this.Activate();
                    break;

                case KeyboardAction.Search:
                    if (parameter.Item2 != null)
                    {
                        this.Search(parameter.Item2.ToString());
                    }

                    break;
            }
        }

        /// <summary>
        /// Activates this media Comment view.
        /// </summary>
        private void Activate()
        {
            this.regionManager.Regions[RegionNames.MarkerBrowserRegion].Activate(this.View);
        }

        private void HandleCurrentSequenceChanged(object o, Infrastructure.DataEventArgs<ISequenceModel> eventArgs)
        {
            if (eventArgs != null && eventArgs.Data != null)
            {
                this.sequenceRegistry.CurrentSequence.CommentElements.CollectionChanged -= this.CommentElements_CollectionChanged;
                eventArgs.Data.ElementRemoved -= this.OnCurrentSequenceOnElementRemoved;

                if (this.comments == null)
                {
                    this.comments = new ObservableCollection<Comment>();
                }
                else
                {
                    this.comments.Clear();
                }

                foreach (var comment in this.sequenceRegistry.CurrentSequence.CommentElements)
                {
                    this.comments.Add(comment);
                }
            }

            this.sequenceRegistry.CurrentSequence.CommentElements.CollectionChanged += this.CommentElements_CollectionChanged;
            this.sequenceRegistry.CurrentSequenceModel.ElementRemoved += this.OnCurrentSequenceOnElementRemoved;
        }

        private void OnCurrentSequenceOnElementRemoved(object sender, TimelineElementEventArgs e)
        {
            this.RemoveCommentsAssociatedToElement(e.Element);
        }

        private bool FilterDisplayMarkerBrowserWindowEvent(SelectedMarkersBrowserTab selectedTab)
        {
            return selectedTab == SelectedMarkersBrowserTab.Comments;
        }
    }
}