// <copyright file="CommentViewPresentationModelFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: CommentViewPresentationModelFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Comment.Tests.Views
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Ink;
    using System.Windows.Input;
    using Events;
    using Infrastructure;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Presentation.Events;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Services;

    using Services.Contracts;
    using SMPTETimecode;
    using Comment = RCE.Infrastructure.Models.Comment;
    using InkComment = RCE.Infrastructure.Models.InkComment;
    using Sequence = RCE.Infrastructure.Models.Sequence;
    using Track = RCE.Infrastructure.Models.Track;
    using TrackType = RCE.Infrastructure.Models.TrackType;

    /// <summary>
    /// Test class for <see cref="CommentViewPresentationModel"/>.
    /// </summary>
    [TestClass]
    public class CommentViewPresentationModelFixture
    {
        /// <summary>
        /// Mock for <see cref="CommentView"/>.
        /// </summary>
        private MockCommentView view;

        /// <summary>
        /// Mock for <see cref="IEventAggregator"/>.
        /// </summary>
        private MockEventAggregator eventAggregator;

        /// <summary>
        /// Mock for <see cref="IConfigurationService"/>.
        /// </summary>
        private MockConfigurationService configurationService;

        /// <summary>
        /// Mock for <see cref="IProjectService"/>.
        /// </summary>
        private MockProjectService projectService;

        /// <summary>
        /// Mock for <see cref="IThumbnailService"/>.
        /// </summary>
        private MockThumbnailService thumbnailService;

        /// <summary>
        /// Mock for <see cref="SequenceModel"/>.
        /// </summary>
        private MockSequenceModel sequenceModel;

        /// <summary>
        /// Mock for <see cref="PlayCommentEvent"/>.
        /// </summary>
        private MockPlayCommentEvent playCommentEvent;

        /// <summary>
        /// Mock for <see cref="ElementMovedEvent"/>.
        /// </summary>
        private MockElementMovedEvent elementMovedEvent;

        /// <summary>
        /// Mock for <see cref="CommentUpdatedEvent"/>.
        /// </summary>
        private MockCommentUpdatedEvent commentUpdatedEvent;

        /// <summary>
        /// Mock for <see cref="AddPreviewEvent"/>.
        /// </summary>
        private MockAddPreviewEvent addPreviewEvent;

        /// <summary>
        /// Mock for <see cref="Microsoft.Practices.Composite.Regions.IRegionManager"/>.
        /// </summary>
        private MockRegionManager regionManager;

        /// <summary>
        /// Mock for <see cref="IRegion"/>.
        /// </summary>
        private MockRegion mainRegion;

        private MockSequenceRegistry sequenceRegistry;

        private MockDisplayCommentsViewEvent displayCommentsViewEvent;

        private MockResetWindowsEvent resetWindowsEvent;

        private MockDisplayMarkerBrowserWindowEvent displayMarkerBrowserEvent;

        private Sequence sequence;

        private MockRegion markerBrowserRegion;

        /// <summary>
        /// Innitialize the test class.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.view = new MockCommentView();
            this.regionManager = new MockRegionManager();
            this.eventAggregator = new MockEventAggregator();
            this.configurationService = new MockConfigurationService();
            this.projectService = new MockProjectService();
            this.thumbnailService = new MockThumbnailService();
            this.sequenceModel = new MockSequenceModel();
            this.playCommentEvent = new MockPlayCommentEvent();
            this.elementMovedEvent = new MockElementMovedEvent();
            this.commentUpdatedEvent = new MockCommentUpdatedEvent();
            this.addPreviewEvent = new MockAddPreviewEvent();
            this.resetWindowsEvent = new MockResetWindowsEvent();
            this.displayCommentsViewEvent = new MockDisplayCommentsViewEvent();
            this.displayMarkerBrowserEvent = new MockDisplayMarkerBrowserWindowEvent();
            this.mainRegion = new MockRegion();
            this.markerBrowserRegion = new MockRegion();
            this.sequenceRegistry = new MockSequenceRegistry();
            this.sequence = new Sequence();
            this.sequenceRegistry.CurrentSequence = this.sequence;

            this.sequenceRegistry.CurrentSequenceModel = this.sequenceModel;

            this.mainRegion.Name = RegionNames.MainRegion;
            this.regionManager.Regions.Add(this.mainRegion);

            this.markerBrowserRegion.Name = RegionNames.MarkerBrowserRegion;
            this.regionManager.Regions.Add(this.markerBrowserRegion);

            this.eventAggregator.AddMapping<PlayCommentEvent>(this.playCommentEvent);
            this.eventAggregator.AddMapping<ElementMovedEvent>(this.elementMovedEvent);
            this.eventAggregator.AddMapping<CommentUpdatedEvent>(this.commentUpdatedEvent);
            this.eventAggregator.AddMapping<AddPreviewEvent>(this.addPreviewEvent);
            this.eventAggregator.AddMapping<ResetWindowsEvent>(this.resetWindowsEvent);
            this.eventAggregator.AddMapping<DisplayMarkerBrowserWindowEvent>(this.displayMarkerBrowserEvent);
        }

        /// <summary>
        /// Tests if the view is getting initilaized on presetation model creation.
        /// </summary>
        [TestMethod]
        public void CanInitPresentationModel()
        {
            var presentationModel = this.CreatePresentationModel();
            Assert.AreEqual(this.view, presentationModel.View);
        }

        /// <summary>
        /// Should set the presentation model into view.
        /// </summary>
        [TestMethod]
        public void ShouldSetPresentationModelIntoView()
        {
            var presentationModel = this.CreatePresentationModel();
            Assert.AreSame(presentationModel, this.view.Model);
        }

        /// <summary>
        /// Should get the comments from the current project.
        /// </summary>
        [TestMethod]
        public void ShouldGetTheCommentsFromTheCurrentProject()
        {
            var comment = new Comment();

            Assert.IsFalse(this.projectService.GetCurrentProjectCalled);
            Assert.AreEqual(0, this.projectService.GetCurrentProject().Comments.Count);

            this.projectService.GetCurrentProject().Comments.Add(comment);

            var presentationModel = this.CreatePresentationModel();

            Assert.IsTrue(this.projectService.GetCurrentProjectCalled);
            Assert.AreEqual(1, presentationModel.Comments.Count);
            Assert.AreSame(comment, presentationModel.Comments[0]);
        }

        /// <summary>
        /// Shoulds call GetCommentsTypes of configuration service.
        /// </summary>
        [TestMethod]
        public void ShouldCallGetCommentsTypesOnTheConfigurationService()
        {
            this.configurationService.GetParameterValueReturnFunction = parameter => parameter == "CommentTypes" ? "Test" : null;

            Assert.IsFalse(this.configurationService.GetParameterValueCalled);

            var presentationModel = this.CreatePresentationModel();

            Assert.IsTrue(this.configurationService.GetParameterValueCalled);
            Assert.AreEqual(1, presentationModel.CommentsTypes.Count);
            StringAssert.Contains(presentationModel.CommentsTypes[0], "Test");
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when comments is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenCommentsIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presentationModel.Comments = new ObservableCollection<Infrastructure.Models.Comment>();

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("Comments", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when Text is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenTextIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presentationModel.Text = "text";

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("Text", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when EditMode is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenEditModeIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presentationModel.EditMode = true;

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("EditMode", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when currentcomment is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenCurrentCommentIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presentationModel.CurrentComment = new Comment();

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("CurrentComment", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when FrameIamge is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenProviderUriIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presentationModel.FrameImage = "http://www/microsoft.com";

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("FrameImage", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when ShowGlobalComments is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenShowGlobalCommentsIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presentationModel.ShowGlobalComments = false;

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("ShowGlobalComments", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when ShowTimelineComments is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenShowTimelineCommentsIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presentationModel.ShowTimelineComments = false;

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("ShowTimelineComments", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when InkCommentStrokes is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenInkCommentStrokesIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presentationModel.InkCommentStrokes = new StrokeCollection();

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("InkCommentStrokes", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when SelectedCommentType is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenSelectedCommentTypeIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presentationModel.SelectedCommentType = "Global";

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("SelectedCommentType", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should set edit mode to false when executing cancel command.
        /// </summary>
        [TestMethod]
        public void ShouldSetEditModeToFalseWhenExecutingCancelCommand()
        {
            var presentationModel = this.CreatePresentationModel();

            presentationModel.EditMode = true;

            Assert.IsTrue(presentationModel.EditMode);

            presentationModel.CancelCommand.Execute(null);

            Assert.IsFalse(presentationModel.EditMode);
        }

        /// <summary>
        /// Should call to clear ink comment when executing cancel command.
        /// </summary>
        [TestMethod]
        public void ShouldCallToClearInkCommentWhenExecutingCancelCommand()
        {
            var presentationModel = this.CreatePresentationModel();

            presentationModel.EditMode = true;

            Assert.IsFalse(this.view.ClearInkCommentCalled);

            presentationModel.CancelCommand.Execute(null);

            Assert.IsTrue(this.view.ClearInkCommentCalled);
        }

        /// <summary>
        /// Should delete comment and set edit mode to false when executing delete command.
        /// </summary>
        [TestMethod]
        public void ShouldDeleteCommentAndSetEditModeToFalseWhenExecutingDeleteCommand()
        {
            var comment = new Comment();

            this.projectService.GetCurrentProject().Comments.Add(comment);

            var presentationModel = this.CreatePresentationModel();
            
            presentationModel.EditMode = true;

            Assert.AreEqual(1, presentationModel.Comments.Count);
            Assert.IsTrue(presentationModel.EditMode);

            presentationModel.DeleteCommand.Execute(comment.CommentId);

            Assert.AreEqual(0, presentationModel.Comments.Count);
            Assert.IsFalse(presentationModel.EditMode);
        }

        /// <summary>
        /// Should update comment and set edit mode to false when executing save command.
        /// </summary>
        [TestMethod]
        public void ShouldUpdateCommentAndSetEditModeToFalseWhenExecutingSaveCommand()
        {
            var modified = new DateTime(2008, 11, 7);
            var text = "NewText";

            var comment = new Comment
                      {
                          Text = "Text", 
                          Modified = modified
                      };

            this.projectService.GetCurrentProject().Comments.Add(comment);

            var presentationModel = this.CreatePresentationModel();
            presentationModel.Text = text;

            presentationModel.EditMode = true;

            Assert.IsTrue(presentationModel.EditMode);

            presentationModel.SaveCommand.Execute(comment.CommentId);

            Assert.AreEqual(text, comment.Text);
            Assert.AreNotEqual(modified, comment.Modified);
        }

        /// <summary>
        /// Should  update comment when executing save command even if the current text is null.
        /// </summary>
        [TestMethod]
        public void ShouldUpdateCommentWhenExecutingSaveCommandEvenIfTheCurrentTextIsNull()
        {
            var modified = new DateTime(2008, 11, 7);
            var comment = new Comment
            {
                Text = "Text",
                Modified = modified
            };

            this.projectService.GetCurrentProject().Comments.Add(comment);

            var presentationModel = this.CreatePresentationModel();
            presentationModel.Text = null;

            presentationModel.SaveCommand.Execute(comment.CommentId);

            Assert.AreEqual(string.Empty, comment.Text);
            Assert.AreNotEqual(modified, comment.Modified);
        }

        /// <summary>
        /// Should update Ink comment and set edit mode to false when executing save command.
        /// </summary>
        [TestMethod]
        public void ShouldUpdateInkCommentAndSetEditModeToFalseWhenExecutingSaveCommand()
        {
            var modified = new DateTime(2008, 11, 7);
            var text = "NewText";

            var strokes = new StrokeCollection
                              {
                                  new Stroke(new StylusPointCollection
                                                 {
                                                     new StylusPoint(10, 10)
                                                 }),
                                  new Stroke(new StylusPointCollection
                                                 {
                                                     new StylusPoint(10, 10)
                                                 })
                              };

            var comment = new InkComment
            {
                Text = "Text",
                Modified = modified,
                InkCommentStrokes = new StrokeCollection
                              {
                                  new Stroke(new StylusPointCollection
                                                 {
                                                     new StylusPoint(20, 40)
                                                 })
                              }
            };

            this.projectService.GetCurrentProject().Comments.Add(comment);

            var presentationModel = this.CreatePresentationModel();
            presentationModel.Text = text;
            presentationModel.InkCommentStrokes = strokes;

            presentationModel.EditMode = true;

            Assert.IsTrue(presentationModel.EditMode);

            presentationModel.SaveCommand.Execute(comment.CommentId);

            Assert.AreEqual(text, comment.Text);
            Assert.AreEqual(2, comment.InkCommentStrokes.Count);
            Assert.AreNotEqual(modified, comment.Modified);
        }

        /// <summary>
        /// Should add comment and set edit mode to false when executing save command.
        /// </summary>
        [TestMethod]
        public void ShouldAddCommentAndSetEditModeToFalseWhenExecutingSaveCommand()
        {
            this.configurationService.GetParameterValueReturnFunction = parameter => parameter == "UserName" ? "test" : null;

            var text = "NewText";
            
            var presentationModel = this.CreatePresentationModel();

            presentationModel.Text = text;

            presentationModel.EditMode = true;

            Assert.IsTrue(presentationModel.EditMode);
            Assert.AreEqual(0, presentationModel.Comments.Count);

            presentationModel.SaveCommand.Execute(Guid.Empty);

            Assert.IsFalse(presentationModel.EditMode);
            Assert.AreEqual(1, presentationModel.Comments.Count);
            Assert.AreEqual(text, presentationModel.Comments[0].Text);
            Assert.AreEqual("test", presentationModel.Comments[0].Creator);
        }

        /// <summary>
        /// Should add shot comment and set edit mode to false when executing save command if comment type is shot.
        /// </summary>
        [TestMethod]
        public void ShouldAddShotCommentAndSetEditModeToFalseWhenExecutingSaveCommandIfCommentTypeIsShot()
        {
            this.configurationService.GetParameterValueReturnFunction = parameter => parameter == "UserName" ? "test" : null;

            var text = "NewText";

            var track = new Track { TrackType = TrackType.Visual };

            var element = new TimelineElement
            {
                Asset = new VideoAsset(),
                Position = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            this.sequenceModel.Tracks.Add(track);

            this.sequenceModel.GetElementAtPositionReturnFunction = () =>
            {
                return element;
            };

            var presentationModel = this.CreatePresentationModel();

            presentationModel.SelectedCommentType = "Shot";
            presentationModel.Text = text;
            presentationModel.EditMode = true;

            Assert.IsTrue(presentationModel.EditMode);
            Assert.AreEqual(0, presentationModel.Comments.Count);

            presentationModel.SaveCommand.Execute(Guid.Empty);

            Assert.IsFalse(presentationModel.EditMode);
            Assert.AreEqual(1, presentationModel.Comments.Count);
            Assert.AreEqual(text, presentationModel.Comments[0].Text);
            Assert.AreEqual(CommentType.Shot, presentationModel.Comments[0].CommentType);
            Assert.AreEqual(element.InPosition.TotalSeconds, presentationModel.Comments[0].MarkIn);
            Assert.AreEqual(element.Position.TotalSeconds + element.Duration.TotalSeconds, presentationModel.Comments[0].MarkOut);
            Assert.AreEqual("test", presentationModel.Comments[0].Creator);
        }

        /// <summary>
        /// Should show error message if when executing save command for A shot comment and there is no timeline element under current position.
        /// </summary>
        [TestMethod]
        public void ShouldShowErrorMessageIfWhenExecutingSaveCommandForAShotCommentThereIsNoTimelineElementUnderCurrentPosition()
        {
            this.configurationService.GetParameterValueReturnFunction = parameter => parameter == "UserName" ? "test" : null;

            var track = new Track { TrackType = TrackType.Visual };

            var returnElement = true;

            var element = new TimelineElement
            {
                Asset = new VideoAsset(),
                Position = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            this.sequenceModel.Tracks.Add(track);

            this.sequenceModel.GetElementAtPositionReturnFunction = () =>
            {
                return returnElement ? element : null;
            };

            var presentationModel = this.CreatePresentationModel();

            presentationModel.SelectedCommentType = "Shot";

            this.view.ShowErrorMessageCalled = false;

            returnElement = false;

            presentationModel.SaveCommand.Execute(Guid.Empty);

            Assert.IsTrue(this.view.ShowErrorMessageCalled);
            StringAssert.Contains(this.view.ShowErrorMessageArgument, "Shot");
        }

        /// <summary>
        /// Should add ink comment and set edit mode to false when executing save command if comment type is ink.
        /// </summary>
        [TestMethod]
        public void ShouldAddInkCommentAndSetEditModeToFalseWhenExecutingSaveCommandIfCommentTypeIsInk()
        {
            this.configurationService.GetParameterValueReturnFunction = parameter =>
            {
                switch (parameter)
                {
                    case "UserName":
                        return "test";

                    case "MediaServicesUrl":
                        return "http://test";

                    default:
                        return null;
                }
            };

            var track = new Track { TrackType = TrackType.Visual };

            var element = new TimelineElement
            {
                Asset = new VideoAsset(),
                Position = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            this.sequenceModel.Tracks.Add(track);

            this.sequenceModel.GetElementAtPositionReturnFunction = () =>
            {
                return element;
            };

            var strokes = new StrokeCollection
                              {
                                  new Stroke(new StylusPointCollection
                                                 {
                                                     new StylusPoint(10, 10)
                                                 })
                              };

            var text = "NewText";

            var presentationModel = this.CreatePresentationModel();

            presentationModel.SelectedCommentType = "Ink";
            presentationModel.InkCommentStrokes = strokes;
            presentationModel.EditMode = true;
            presentationModel.Text = text;

            Assert.IsTrue(presentationModel.EditMode);
            Assert.AreEqual(0, presentationModel.Comments.Count);

            presentationModel.SaveCommand.Execute(Guid.Empty);

            Assert.IsFalse(presentationModel.EditMode);
            Assert.IsNull(presentationModel.InkCommentStrokes);
            Assert.AreEqual(1, presentationModel.Comments.Count);
            Assert.AreEqual(text, presentationModel.Comments[0].Text);
            Assert.AreEqual(CommentType.Ink, presentationModel.Comments[0].CommentType);
            Assert.AreEqual(element.InPosition.TotalSeconds, presentationModel.Comments[0].MarkIn);
            Assert.AreEqual(element.Position.TotalSeconds + element.Duration.TotalSeconds, presentationModel.Comments[0].MarkOut);
            Assert.AreEqual(strokes.Count, ((InkComment)presentationModel.Comments[0]).InkCommentStrokes.Count);
            Assert.AreEqual(strokes[0], ((InkComment)presentationModel.Comments[0]).InkCommentStrokes[0]);
            Assert.AreEqual("test", presentationModel.Comments[0].Creator);
        }

        /// <summary>
        /// Should show error message when executing save command for an ink comment and there is no timeline element under current position.
        /// </summary>
        [TestMethod]
        public void ShouldShowErrorMessageIfWhenExecutingSaveCommandForAnInkCommentThereIsNoTimelineElementUnderCurrentPosition()
        {
            this.configurationService.GetParameterValueReturnFunction = parameter =>
            {
                switch (parameter)
                {
                    case "UserName":
                        return "test";

                    case "MediaServicesUrl":
                        return "http://test";

                    default:
                        return null;
                }
            };

            var track = new Track { TrackType = TrackType.Visual };

            var returnElement = true;

            var element = new TimelineElement
            {
                Asset = new VideoAsset(),
                Position = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            this.sequenceModel.Tracks.Add(track);

            this.sequenceModel.GetElementAtPositionReturnFunction = () =>
            {
                return returnElement ? element : null;
            };

            var presentationModel = this.CreatePresentationModel();

            presentationModel.SelectedCommentType = "Ink";

            this.view.ShowErrorMessageCalled = false;

            returnElement = false;

            presentationModel.SaveCommand.Execute(Guid.Empty);

            Assert.IsTrue(this.view.ShowErrorMessageCalled);
            StringAssert.Contains(this.view.ShowErrorMessageArgument, "Ink");
        }

        /// <summary>
        /// Should add comment to timeline model when executing save command and comment is not global.
        /// </summary>
        [TestMethod]
        public void ShouldAddCommentToTimelineModelWhenExecutingSaveCommandIfCommentIsNotGlobal()
        {
            this.configurationService.GetParameterValueReturnFunction = parameter => parameter == "UserName" ? "test" : null;

            var presentationModel = this.CreatePresentationModel();

            presentationModel.Text = "NewText";
            presentationModel.CurrentComment = new Comment(Guid.Empty) { CommentType = CommentType.Timeline };

            presentationModel.EditMode = true;

            Assert.AreEqual(0, this.sequence.CommentElements.Count);

            presentationModel.SaveCommand.Execute(Guid.Empty);

            Assert.AreEqual(1, this.sequence.CommentElements.Count);
        }

        /// <summary>
        /// Should not add comment to timeline model when executing save command and comment is global.
        /// </summary>
        [TestMethod]
        public void ShouldNotAddCommentToTimelineModelWhenExecutingSaveCommandIfCommentIsGlobal()
        {
            this.configurationService.GetParameterValueReturnFunction = parameter => parameter == "UserName" ? "test" : null;

            var presentationModel = this.CreatePresentationModel();

            presentationModel.Text = "NewText";
            presentationModel.CurrentComment = new Comment(Guid.Empty) { CommentType = CommentType.Global };

            presentationModel.EditMode = true;

            Assert.AreEqual(0, this.sequence.CommentElements.Count);

            presentationModel.SaveCommand.Execute(Guid.Empty);

            Assert.AreEqual(0, this.sequence.CommentElements.Count);
        }

        /// <summary>
        /// Should set current comment and set edit mode to true when executing edit command.
        /// </summary>
        [TestMethod]
        public void ShouldSetCurrentCommentAndSetEditModeToTrueWhenExecutingEditCommand()
        {
            var comment = new Comment();

            this.projectService.GetCurrentProject().Comments.Add(comment);

            var presentationModel = this.CreatePresentationModel();

            presentationModel.EditMode = false;

            Assert.IsFalse(presentationModel.EditMode);
            Assert.AreNotEqual(comment, presentationModel.CurrentComment);

            presentationModel.EditCommand.Execute(comment.CommentId);

            Assert.IsTrue(presentationModel.EditMode);
            Assert.IsNotNull(presentationModel.CurrentComment);
            Assert.AreEqual(comment, presentationModel.CurrentComment);
        }

        /// <summary>
        /// Should call set ink editing moode when executing edit command for an ink comment.
        /// </summary>
        [TestMethod]
        public void ShouldCallSetInkEditingModeWhenExecutingEditCommandForAnInkComment()
        {
            var comment = new InkComment() { InkCommentStrokes = new StrokeCollection() };

            this.projectService.GetCurrentProject().Comments.Add(comment);

            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.view.SetInkEditingModeCalled);

            presentationModel.EditCommand.Execute(comment.CommentId);

            Assert.IsTrue(this.view.SetInkEditingModeCalled);
        }

        /// <summary>
        /// Should set frame image when executing edit command for an ink comment.
        /// </summary>
        [TestMethod]
        public void ShouldSetFrameImageWhenExecutingEditCommandForAnInkComment()
        {
            this.configurationService.GetParameterValueReturnFunction = parameter => parameter == "MediaServicesUrl" ? "http://test/" : null;

            var track = new Track { TrackType = TrackType.Visual };

            var element = new TimelineElement
            {
                Asset = new VideoAsset { ProviderUri = new Uri("http://asset/1") },
                Position = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            var comment = new InkComment
                              {
                                  InkCommentStrokes = new StrokeCollection(),
                                  MarkIn = 200,
                                  CommentType = CommentType.Ink
                              };

            element.Comments.Add(comment);

            track.Shots.Add(element);

            this.sequenceModel.Tracks.Add(track);

            this.projectService.GetCurrentProject().Comments.Add(comment);

            this.thumbnailService.GetThumbnailSourceResult = "http://test/";

            var presentationModel = this.CreatePresentationModel();

            presentationModel.FrameImage = null;

            presentationModel.EditCommand.Execute(comment.CommentId);

            Assert.IsNotNull(presentationModel.FrameImage);
            StringAssert.Contains(presentationModel.FrameImage, "http://test/");
        }

        /// <summary>
        /// Should search by text when executing search command.
        /// </summary>
        [TestMethod]
        public void ShouldSearchByTextWhenExecutingSearchCommand()
        {
            var globalComment = new Comment(Guid.Empty) { Text = "Hello World", CommentType = CommentType.Global, Creator = string.Empty };
            var timelineComment = new Comment(Guid.Empty) { Text = string.Empty, CommentType = CommentType.Timeline, Creator = string.Empty };

            var comments = new ObservableCollection<Comment> { globalComment, timelineComment };

            this.projectService.GetCurrentProject().AddComments(comments);

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(2, presentationModel.Comments.Count);

            presentationModel.SearchCommand.Execute("Hello");

            Assert.AreEqual(1, presentationModel.Comments.Count);
        }

        /// <summary>
        /// Should search by creator when executing search command.
        /// </summary>
        [TestMethod]
        public void ShouldSearchByCreatorWhenExecutingSearchCommand()
        {
            var globalComment = new Comment(Guid.Empty) { Text = string.Empty, CommentType = CommentType.Global, Creator = "Creator" };
            var timelineComment = new Comment(Guid.Empty) { Text = string.Empty, CommentType = CommentType.Timeline, Creator = string.Empty };

            var comments = new ObservableCollection<Comment> { globalComment, timelineComment };

            this.projectService.GetCurrentProject().AddComments(comments);

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(2, presentationModel.Comments.Count);

            presentationModel.SearchCommand.Execute("Creator");

            Assert.AreEqual(1, presentationModel.Comments.Count);
        }

        /// <summary>
        /// Should cleanup search when executing search command if filter is empty.
        /// </summary>
        [TestMethod]
        public void ShouldCleanupSearchWhenExecutingSearchCommandIfFilterIsEmpty()
        {
            var globalComment = new Comment(Guid.Empty) { Text = string.Empty, CommentType = CommentType.Global, Creator = "Creator" };
            var timelineComment = new Comment(Guid.Empty) { Text = string.Empty, CommentType = CommentType.Timeline, Creator = string.Empty };

            var comments = new ObservableCollection<Comment> { globalComment, timelineComment };

            this.projectService.GetCurrentProject().AddComments(comments);

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(2, presentationModel.Comments.Count);

            presentationModel.SearchCommand.Execute("Creator");

            Assert.AreEqual(1, presentationModel.Comments.Count);

            presentationModel.SearchCommand.Execute(string.Empty);

            Assert.AreEqual(2, presentationModel.Comments.Count);
        }

        /// <summary>
        /// Should add the comment when comment is added in timeline model.
        /// </summary>
        [TestMethod]
        public void ShouldAddTheCommentWhenCommentIsAddedInTimelineModel()
        {
            var comment = new Comment(Guid.NewGuid());
            var presentationModel = this.CreatePresentationModel();
            this.sequenceRegistry.InvokeCurrentSequenceChanged(null);

            Assert.IsTrue(presentationModel.Comments.Count == 0);

            this.sequence.CommentElements.Add(comment);

            Assert.IsTrue(presentationModel.Comments.Count == 1);
            Assert.AreSame(presentationModel.Comments[0], comment);
        }

        /// <summary>
        /// Should remove the comment when comment is removed in timeline model.
        /// </summary>
        [TestMethod]
        public void ShouldRemoveTheCommentWhenCommentIsRemovedInTimelineModel()
        {
            var comment = new Comment(Guid.NewGuid());
            var presentationModel = this.CreatePresentationModel();
            this.sequenceRegistry.InvokeCurrentSequenceChanged(null);
            this.sequence.CommentElements.Add(comment);

            Assert.IsTrue(presentationModel.Comments.Count == 1);

            this.sequence.CommentElements.Remove(comment);

            Assert.IsTrue(presentationModel.Comments.Count == 0);
        }

        /// <summary>
        /// Should publish the play comment event when play comment is called.
        /// </summary>
        [TestMethod]
        public void ShouldPublishThePlayCommentEventWhenPlayCommentIsCalled()
        {
            var comment = new Comment(Guid.NewGuid());

            var presentationModel = this.CreatePresentationModel();
            
            presentationModel.CurrentComment = comment;

            this.playCommentEvent.PublishArgumentPayload = null;

            presentationModel.PlayComment();

            Assert.AreSame(this.playCommentEvent.PublishArgumentPayload, comment);
        }

        /// <summary>
        /// Should not publish the play comment event if current comment is null.
        /// </summary>
        [TestMethod]
        public void ShouldNotPublishThePlayCommentEventIfCurrentCommentIsNull()
        {
            var presentationModel = this.CreatePresentationModel();
            
            presentationModel.CurrentComment = null;

            this.playCommentEvent.PublishArgumentPayload = null;

            presentationModel.PlayComment();

            Assert.IsNull(this.playCommentEvent.PublishArgumentPayload);
        }

        /// <summary>
        /// Should show global comments when only show global comments is true.
        /// </summary>
        [TestMethod]
        public void ShouldShowGlobalCommentsWhenOnlyShowGlobalCommentsIsTrue()
        {
            var inkComment = new Comment(Guid.Empty) { CommentType = CommentType.Ink }; 
            var globalComment = new Comment(Guid.Empty) { CommentType = CommentType.Global };
            var timelineComment = new Comment(Guid.Empty) { CommentType = CommentType.Timeline };

            var comments = new ObservableCollection<Comment> { inkComment, globalComment, timelineComment };

            this.projectService.GetCurrentProject().AddComments(comments);

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(3, presentationModel.Comments.Count);

            presentationModel.ShowGlobalComments = true;
            presentationModel.ShowTimelineComments = false;

            Assert.AreEqual(1, presentationModel.Comments.Count);
            Assert.AreSame(globalComment, presentationModel.Comments[0]);
        }

        /// <summary>
        /// Should show timeline comments when only show timeline comments is true.
        /// </summary>
        [TestMethod]
        public void ShouldShowTimelineCommentsWhenOnlyShowTimelineCommentsIsTrue()
        {
            var globalComment = new Comment(Guid.Empty) { CommentType = CommentType.Global };
            var timelineComment = new Comment(Guid.Empty) { CommentType = CommentType.Timeline };

            var comments = new ObservableCollection<Comment> { globalComment, timelineComment };

            this.projectService.GetCurrentProject().AddComments(comments);

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(2, presentationModel.Comments.Count);
            
            presentationModel.ShowGlobalComments = false;
            presentationModel.ShowTimelineComments = true;

            Assert.AreEqual(1, presentationModel.Comments.Count);
            Assert.AreSame(timelineComment, presentationModel.Comments[0]);
        }

        /// <summary>
        /// Should show no comments when show timeline comments and show global comment is false.
        /// </summary>
        [TestMethod]
        public void ShouldShowNoCommentsWhenShowTimelineCommentsAndShowGlobalCommentIsFalse()
        {
            var globalComment = new Comment(Guid.Empty) { CommentType = CommentType.Global };
            var timelineComment = new Comment(Guid.Empty) { CommentType = CommentType.Timeline };

            var comments = new ObservableCollection<Comment> { globalComment, timelineComment };

            this.projectService.GetCurrentProject().AddComments(comments);

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(2, presentationModel.Comments.Count);

            presentationModel.ShowGlobalComments = false;
            presentationModel.ShowTimelineComments = false;

            Assert.AreEqual(0, presentationModel.Comments.Count);
        }

        /// <summary>
        /// Should show all comments when show timeline comments and show global comment is true.
        /// </summary>
        [TestMethod]
        public void ShouldShowAllCommentsWhenShowTimelineCommentsAndShowGlobalCommentIsTrue()
        {
            var globalComment = new Comment(Guid.Empty) { CommentType = CommentType.Global };
            var timelineComment = new Comment(Guid.Empty) { CommentType = CommentType.Timeline };

            var comments = new ObservableCollection<Comment> { globalComment, timelineComment };

            this.projectService.GetCurrentProject().AddComments(comments);

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(2, presentationModel.Comments.Count);

            presentationModel.ShowGlobalComments = true;
            presentationModel.ShowTimelineComments = true;

            Assert.AreEqual(2, presentationModel.Comments.Count);
        }

        /// <summary>
        /// Should create A global comment if selected comment type is global.
        /// </summary>
        [TestMethod]
        public void ShouldCreateAGlobalCommentIfSelectedCommentTypeIsGlobal()
        {
            var presentationModel = this.CreatePresentationModel();

            this.configurationService.GetParameterValueReturnFunction = parameter => parameter == "UserName" ? "test" : null;

            presentationModel.SelectedCommentType = "Global";

            Assert.AreEqual(presentationModel.CurrentComment.CommentType, CommentType.Global);
            Assert.IsNull(presentationModel.CurrentComment.MarkIn);
            Assert.IsNull(presentationModel.CurrentComment.MarkOut);
            Assert.AreEqual("test", presentationModel.CurrentComment.Creator);
        }

        /// <summary>
        /// Should create A playhead comment if selected comment type is playhead.
        /// </summary>
        [TestMethod]
        public void ShouldCreateAPlayheadCommentIfSelectedCommentTypeIsPlayhead()
        {
            var presentationModel = this.CreatePresentationModel();

            this.configurationService.GetParameterValueReturnFunction = parameter => parameter == "UserName" ? "test" : null;

            presentationModel.SelectedCommentType = "Playhead";

            Assert.AreEqual(Guid.Empty, presentationModel.CurrentComment.CommentId);
            Assert.AreEqual(CommentType.Timeline, presentationModel.CurrentComment.CommentType);
            Assert.AreEqual("test", presentationModel.CurrentComment.Creator);
        }

        /// <summary>
        /// Should create A shot comment if there A timeline element at current position and selected comment type is shot.
        /// </summary>
        [TestMethod]
        public void ShouldCreateAShotCommentIfThereATimelineElementAtCurrentPositionSelectedCommentTypeIsShot()
        {
            this.configurationService.GetParameterValueReturnFunction = parameter => parameter == "UserName" ? "test" : null;

            var track = new Track { TrackType = TrackType.Visual };

            var element = new TimelineElement
            {
                Asset = new VideoAsset(),
                Position = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            this.sequenceModel.Tracks.Add(track);

            this.sequenceModel.GetElementAtPositionReturnFunction = () =>
            {
                return element;
            };

            var presentationModel = this.CreatePresentationModel();

            this.view.ShowErrorMessageCalled = false;

            presentationModel.SelectedCommentType = "Shot";

            Assert.AreEqual(Guid.Empty, presentationModel.CurrentComment.CommentId);
            Assert.AreEqual(CommentType.Shot, presentationModel.CurrentComment.CommentType);
            Assert.AreEqual("test", presentationModel.CurrentComment.Creator);
        }

        /// <summary>
        /// Should create an ink comment if there A timeline element at current position and selected comment type is ink.
        /// </summary>
        [TestMethod]
        public void ShouldCreateAnInkCommentIfThereATimelineElementAtCurrentPositionAndSelectedCommentTypeIsInk()
        {
            this.configurationService.GetParameterValueReturnFunction = parameter =>
            {
                switch (parameter)
                {
                    case "UserName":
                        return "test";

                    case "MediaServicesUrl":
                        return "http://test";

                    default:
                        return null;
                }
            };

            var text = "NewText";

            var track = new Track { TrackType = TrackType.Visual };

            var element = new TimelineElement
            {
                Asset = new VideoAsset(),
                Position = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            this.sequenceModel.Tracks.Add(track);

            this.sequenceModel.GetElementAtPositionReturnFunction = () =>
            {
                return element;
            };

            var presentationModel = this.CreatePresentationModel();

            this.view.ShowErrorMessageCalled = false;

            presentationModel.Text = text;
            presentationModel.SelectedCommentType = "Ink";

            Assert.AreEqual(Guid.Empty, presentationModel.CurrentComment.CommentId);
            Assert.AreEqual(CommentType.Ink, presentationModel.CurrentComment.CommentType);
            Assert.AreEqual("test", presentationModel.CurrentComment.Creator);
        }

        /// <summary>
        /// Should set frame image when selected comment type is ink.
        /// </summary>
        [TestMethod]
        public void ShouldSetFrameImageWhenSelectedCommentTypeIsInk()
        {
            this.configurationService.GetParameterValueReturnFunction = parameter =>
            {
                switch (parameter)
                {
                    case "UserName":
                        return "test";

                    case "MediaServicesUrl":
                        return "http://test/";

                    default:
                        return null;
                }
            };

            var track = new Track { TrackType = TrackType.Visual };

            var element = new TimelineElement
            {
                Asset = new VideoAsset { ProviderUri = new Uri("http://asset/1") },
                Position = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            this.sequenceModel.Tracks.Add(track);

            this.sequenceModel.GetElementAtPositionReturnFunction = () =>
            {
                return element;
            };

            this.thumbnailService.GetThumbnailSourceResult = "http://test/";

            var presentationModel = this.CreatePresentationModel();

            presentationModel.FrameImage = null;
            presentationModel.SelectedCommentType = "Ink";

            Assert.IsNotNull(presentationModel.FrameImage);
            StringAssert.Contains(presentationModel.FrameImage, "http://test/");
        }

        /// <summary>
        /// Should create an ink comment if there is a timeline element at current position.
        /// </summary>
        [TestMethod]
        public void ShouldCreateAnInkCommentIfThereATimelineElementAtCurrentPosition()
        {
            this.configurationService.GetParameterValueReturnFunction = parameter =>
            {
                switch (parameter)
                {
                    case "UserName":
                        return "test";

                    case "MediaServicesUrl":
                        return "http://test/";

                    default:
                        return null;
                }
            };

            var text = "NewText";

            var track = new Track { TrackType = TrackType.Visual };

            var element = new TimelineElement
            {
                Asset = new VideoAsset(),
                Position = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                InPosition = TimeCode.FromAbsoluteTime(0, this.sequenceModel.Duration.FrameRate),
                OutPosition = TimeCode.FromAbsoluteTime(20, this.sequenceModel.Duration.FrameRate)
            };

            this.sequenceModel.Tracks.Add(track);

            this.sequenceModel.GetElementAtPositionReturnFunction = () =>
            {
                return element;
            };

            var presentationModel = this.CreatePresentationModel();

            this.view.ShowErrorMessageCalled = false;

            presentationModel.Text = text;

            presentationModel.SelectedCommentType = "Ink";

            Assert.AreEqual(Guid.Empty, presentationModel.CurrentComment.CommentId);
            Assert.AreEqual(CommentType.Ink, presentationModel.CurrentComment.CommentType);
            Assert.AreEqual("test", presentationModel.CurrentComment.Creator);
        }

        /// <summary>
        /// Should show error message when trying to create A shot comment and there is no timeline element at current position.
        /// </summary>
        [TestMethod]
        public void ShouldShowErrorMessageIfWhenTryingToCreateAShotCommentThereIsNoTimelineElementAtCurrentPosition()
        {
            var track = new Track { TrackType = TrackType.Visual };
            this.sequenceModel.Tracks.Add(track);

            var presentationModel = this.CreatePresentationModel();

            this.view.ShowErrorMessageCalled = false;

            presentationModel.SelectedCommentType = "Shot";

            Assert.IsTrue(this.view.ShowErrorMessageCalled);
            StringAssert.Contains(this.view.ShowErrorMessageArgument, "Shot");
        }

        /// <summary>
        /// Should show error message when trying to create an ink comment and there is no timeline element at current position.
        /// </summary>
        [TestMethod]
        public void ShouldShowErrorMessageIfWhenTryingToCreateAnInkCommentThereIsNoTimelineElementAtCurrentPosition()
        {
            var track = new Track { TrackType = TrackType.Visual };
            this.sequenceModel.Tracks.Add(track);

            var presentationModel = this.CreatePresentationModel();

            this.view.ShowErrorMessageCalled = false;

            presentationModel.SelectedCommentType = "Ink";

            Assert.IsTrue(this.view.ShowErrorMessageCalled);
            StringAssert.Contains(this.view.ShowErrorMessageArgument, "Ink");
        }

        /// <summary>
        /// Tests if the HeaderInfo property returns the expected value.
        /// </summary>
        [TestMethod]
        public void ShouldReturnHeaderInfoResource()
        {
            var presenter = this.CreatePresentationModel();

            var result = presenter.HeaderInfo;

            Assert.AreEqual("Comments", result);
        }

        /// <summary>
        /// Tests if the HeaderIconOff property returns the expected value.
        /// </summary>
        [TestMethod]
        public void ShouldReturnHeaderIconOffResource()
        {
            var presenter = this.CreatePresentationModel();

            var result = presenter.HeaderIconOff;

            Assert.AreEqual("/RCE.Modules.Comment;component/images/icon_off.png", result);
        }

        /// <summary>
        /// Tests if the HeaderIconOn property returns the expected value.
        /// </summary>
        [TestMethod]
        public void ShouldReturnHeaderIconOnResource()
        {
            var presenter = this.CreatePresentationModel();

            var result = presenter.HeaderIconOn;

            Assert.AreEqual("/RCE.Modules.Comment;component/images/icon_on.png", result);
        }

        /// <summary>
        /// Should publish play comment event when executing play comment command.
        /// </summary>
        [TestMethod]
        public void ShouldPublishPlayCommentEventWhenExecutingPlayCommentCommand()
        {
            var timelineComment = new Comment(Guid.Empty) { Text = string.Empty, CommentType = CommentType.Timeline, Creator = string.Empty };

            var comments = new ObservableCollection<Comment> { timelineComment };

            this.projectService.GetCurrentProject().AddComments(comments);

            var presentationModel = this.CreatePresentationModel();

            this.playCommentEvent.PublishCalled = false;

            presentationModel.PlayCommentCommand.Execute(timelineComment.CommentId);

            Assert.IsTrue(this.playCommentEvent.PublishCalled);
        }

        /// <summary>
        /// Should remove comment from timeline model when executing delete command if comment is not global.
        /// </summary>
        [TestMethod]
        public void ShouldRemoveCommentFromTimelineModelWhenExecutingDeleteCommandIfCommentIsNotGlobal()
        {
            var timelineComment = new Comment { Text = string.Empty, CommentType = CommentType.Timeline, Creator = string.Empty };

            var comments = new ObservableCollection<Comment> { timelineComment };

            this.projectService.GetCurrentProject().AddComments(comments);

            var presentationModel = this.CreatePresentationModel();
            this.sequenceRegistry.InvokeCurrentSequenceChanged(null);

            Assert.AreEqual(1, this.sequence.CommentElements.Count);

            presentationModel.DeleteCommand.Execute(timelineComment.CommentId);

            Assert.AreEqual(0, this.sequence.CommentElements.Count);
        }

        /// <summary>
        /// Should add comments to comments collection when invoking the project retrieved event.
        /// </summary>
        [TestMethod]
        public void ShouldAddCommentsToCommentsCollectionWhenInvokingTheProjectRetrievedEvent()
        {
            var globalComment = new Comment(Guid.Empty) { Text = "Hello World", CommentType = CommentType.Global, Creator = string.Empty };
            var timelineComment = new Comment(Guid.Empty) { Text = string.Empty, CommentType = CommentType.Timeline, Creator = string.Empty };

            var comments = new ObservableCollection<Comment> { globalComment, timelineComment };

            this.projectService.GetCurrentProject().AddComments(comments);

            this.projectService.State = ProjectState.Retrieving;

            var presentationModel = this.CreatePresentationModel();

            Assert.IsNull(presentationModel.Comments);

            this.projectService.InvokeProjectRetrieved();

            Assert.AreEqual(2, presentationModel.Comments.Count);
        }

        /// <summary>
        /// Should  add non global comments to timeline model comments collection when invoking the project retrieved event.
        /// </summary>
        [TestMethod]
        public void ShouldAddNonGlobalCommentsToTimelineModelCommentsCollectionWhenInvokingTheProjectRetrievedEvent()
        {
            var globalComment = new Comment(Guid.Empty) { Text = "Hello World", CommentType = CommentType.Global, Creator = string.Empty };
            var timelineComment = new Comment(Guid.Empty) { Text = string.Empty, CommentType = CommentType.Timeline, Creator = string.Empty };

            var comments = new ObservableCollection<Comment> { globalComment, timelineComment };

            this.projectService.GetCurrentProject().AddComments(comments);

            this.projectService.State = ProjectState.Retrieving;

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(0, this.sequence.CommentElements.Count);

            this.projectService.InvokeProjectRetrieved();

            Assert.AreEqual(1, this.sequence.CommentElements.Count);
        }

        /// <summary>
        /// Tests if the Activate of Region is called when Keyboard Action command is executed
        /// </summary>
        [TestMethod]
        public void ShouldActivateTheViewIfActivateIsCalled()
        {
            this.markerBrowserRegion.SelectedItem = null;
            var presentationModel = this.CreatePresentationModel();

            presentationModel.KeyboardActionCommand.Execute(Tuple.Create(KeyboardAction.ActivateModel, default(object)));

            Assert.AreSame(this.view, this.markerBrowserRegion.SelectedItem);
        }

        /// <summary>
        /// Should remove comment from the element when comment is removed from TimelineModel
        /// </summary>
        [TestMethod]
        public void ShouldRemoveCommentFromTheElementWhenCommentIsRemovedFromTimelineModel()
        {
            TimelineElement element = new TimelineElement { Asset = new VideoAsset() };
            Comment comment = new Comment { CommentType = CommentType.Shot };
            element.Comments.Add(comment);

            var track = new Track { TrackType = TrackType.Visual };
            track.Shots.Add(element);

            this.sequenceModel.Tracks.Add(track);

            this.sequence.CommentElements.Add(comment);

            var presentationModel = this.CreatePresentationModel();

            this.sequenceRegistry.InvokeCurrentSequenceChanged(null);

            Assert.AreEqual(1, element.Comments.Count);

            this.sequence.CommentElements.Remove(comment);

            Assert.AreEqual(0, element.Comments.Count);
        }

        /// <summary>
        /// Should not touch comments elements collection on timeline model when ElementRemoved event 
        /// is invoked and element has no comments associated.
        /// </summary>
        [TestMethod]
        public void ShouldNotTouchCommentsElementsCollectionInTheTimelineModelWhenElementRemovedEventInvokedIfElementHasNoCommentsAssociated()
        {
            TimelineElement element = new TimelineElement { Asset = new VideoAsset() };
            
            Comment comment = new Comment { CommentType = CommentType.Shot, };

            this.sequence.CommentElements.Add(comment);

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(1, this.sequence.CommentElements.Count);

            this.sequenceModel.InvokeElementRemoved(element);

            Assert.AreEqual(1, this.sequence.CommentElements.Count);
        }

        /// <summary>
        /// Should not touch comments elements collection on timeline model when ElementRemoved
        /// event invoked and element has comments associated and asset is not video asset.
        /// </summary>
        [TestMethod]
        public void ShouldNotTouchCommentsElementsCollectionInTheTimelineModelWhenElementRemovedEventInvokedIfElementHasCommentsAssociatedAndAssetIsNotVideoAsset()
        {
            TimelineElement element = new TimelineElement { Asset = new AudioAsset() };
            Comment comment = new Comment();
            element.Comments.Add(comment);

            this.sequence.CommentElements.Add(comment);

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(1, this.sequence.CommentElements.Count);

            this.sequenceModel.InvokeElementRemoved(element);

            Assert.AreEqual(1, this.sequence.CommentElements.Count);
        }

        /// <summary>
        /// Should remove comment in the timeline model when ElementRemoved event 
        /// invoked and element has comments associated and asset is video asset.
        /// </summary>
        [TestMethod]
        public void ShouldRemoveCommentInTheTimelineModelWhenElementRemovedEventInvokedIfElementHasCommentsAssociatedAndAssetIsVideoAsset()
        {
            TimelineElement element = new TimelineElement { Asset = new VideoAsset() };
            Comment comment = new Comment { CommentType = CommentType.Shot };
            element.Comments.Add(comment);

            this.sequence.CommentElements.Add(comment);

            var presentationModel = this.CreatePresentationModel();
            this.sequenceRegistry.InvokeCurrentSequenceChanged(null);

            Assert.AreEqual(1, this.sequence.CommentElements.Count);

            this.sequenceModel.InvokeElementRemoved(element);

            Assert.AreEqual(0, this.sequence.CommentElements.Count);
        }

        /// <summary>
        /// Should not publish CommentUpdated event with 
        /// ElementRemoved event subscription and element has no comments associated.
        /// </summary>
        [TestMethod]
        public void ShouldNotPublishCommentUpdatedEventWithElementMovedEventSubscriptionIfElementHasNoCommentsAssociated()
        {
            TimelineElement element = new TimelineElement();

            var oldPosition = TimeCode.FromSeconds(60d, SmpteFrameRate.Smpte2997NonDrop);
            var newPosition = TimeCode.FromSeconds(60d, SmpteFrameRate.Smpte2997NonDrop);
            var payload = new ElementMovedPayload(element, ElementPositionType.Position, oldPosition, newPosition);

            Assert.IsNull(this.elementMovedEvent.SubscribeArgumentAction);

            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.commentUpdatedEvent.PublishCalled);
            Assert.IsNull(this.commentUpdatedEvent.PublishArgumentPayload);

            Assert.IsNotNull(this.elementMovedEvent.SubscribeArgumentAction);
            Assert.AreEqual(ThreadOption.PublisherThread, this.elementMovedEvent.SubscribeArgumentThreadOption);

            this.elementMovedEvent.SubscribeArgumentAction(payload);

            Assert.IsFalse(this.commentUpdatedEvent.PublishCalled);
            Assert.IsNull(this.commentUpdatedEvent.PublishArgumentPayload);
        }

        /// <summary>
        /// Should not publish CommentUpdatedEvent with 
        /// ElementRemoved event subscription and element has comments associated and asset is not video asset.
        /// </summary>
        [TestMethod]
        public void ShouldNotPublishCommentUpdatedEventWithElementMovedEventSubscriptionIfElementHasCommentsAssociatedAndAssetIsNotVideoAsset()
        {
            TimelineElement element = new TimelineElement { Asset = new AudioAsset() };
            Comment comment = new Comment();
            element.Comments.Add(comment);

            this.sequence.CommentElements.Add(comment);

            var oldPosition = TimeCode.FromSeconds(60d, SmpteFrameRate.Smpte2997NonDrop);
            var newPosition = TimeCode.FromSeconds(60d, SmpteFrameRate.Smpte2997NonDrop);
            var payload = new ElementMovedPayload(element, ElementPositionType.Position, oldPosition, newPosition);

            Assert.IsNull(this.elementMovedEvent.SubscribeArgumentAction);

            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.commentUpdatedEvent.PublishCalled);
            Assert.IsNull(this.commentUpdatedEvent.PublishArgumentPayload);

            Assert.IsNotNull(this.elementMovedEvent.SubscribeArgumentAction);
            Assert.AreEqual(ThreadOption.PublisherThread, this.elementMovedEvent.SubscribeArgumentThreadOption);

            this.elementMovedEvent.SubscribeArgumentAction(payload);

            Assert.IsFalse(this.commentUpdatedEvent.PublishCalled);
            Assert.IsNull(this.commentUpdatedEvent.PublishArgumentPayload);
        }

        /// <summary>
        /// Should publish comment updated event with ElementMoved event
        /// subscription and element has comments associated and asset is video asset.
        /// </summary>
        [TestMethod]
        public void ShouldPublishCommentUpdatedEventWithElementMovedEventSubscriptionIfElementHasCommentsAssociatedAndAssetIsVideoAsset()
        {
            TimelineElement element = new TimelineElement { Asset = new VideoAsset() };
            Comment comment = new Comment { CommentType = CommentType.Shot };
            element.Comments.Add(comment);

            this.sequenceModel.CommentElements.Add(comment);

            var oldPosition = TimeCode.FromSeconds(60d, SmpteFrameRate.Smpte2997NonDrop);
            var newPosition = TimeCode.FromSeconds(60d, SmpteFrameRate.Smpte2997NonDrop);
            var payload = new ElementMovedPayload(element, ElementPositionType.Position, oldPosition, newPosition);

            Assert.IsNull(this.elementMovedEvent.SubscribeArgumentAction);

            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.commentUpdatedEvent.PublishCalled);
            Assert.IsNull(this.commentUpdatedEvent.PublishArgumentPayload);

            Assert.IsNotNull(this.elementMovedEvent.SubscribeArgumentAction);
            Assert.AreEqual(ThreadOption.PublisherThread, this.elementMovedEvent.SubscribeArgumentThreadOption);

            this.elementMovedEvent.SubscribeArgumentAction(payload);

            Assert.IsTrue(this.commentUpdatedEvent.PublishCalled);
            Assert.IsNotNull(this.commentUpdatedEvent.PublishArgumentPayload);
            Assert.AreEqual(comment, this.commentUpdatedEvent.PublishArgumentPayload);
        }

        /// <summary>
        /// Should increase mark in value when new position is greater than old position and 
        /// element position type is InPosition.
        /// </summary>
        [TestMethod]
        public void ShouldIncreaseMarkInValueWhenNewPositionIsGreaterThanOldPositionAndElementPositionTypeIsInPosition()
        {
            TimelineElement element = new TimelineElement { Asset = new VideoAsset() };
            Comment comment = new Comment
            {
                MarkIn = 200,
                MarkOut = 320,
                CommentType = CommentType.Shot
            };

            element.Comments.Add(comment);
            this.sequenceModel.CommentElements.Add(comment);

            var oldPosition = TimeCode.FromSeconds(60d, SmpteFrameRate.Smpte2997NonDrop);
            var newPosition = TimeCode.FromSeconds(120d, SmpteFrameRate.Smpte2997NonDrop);
            var payload = new ElementMovedPayload(element, ElementPositionType.InPosition, oldPosition, newPosition);

            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.commentUpdatedEvent.PublishCalled);
            Assert.IsNull(this.commentUpdatedEvent.PublishArgumentPayload);

            this.elementMovedEvent.SubscribeArgumentAction(payload);

            Assert.IsTrue(this.commentUpdatedEvent.PublishCalled);
            Assert.IsNotNull(this.commentUpdatedEvent.PublishArgumentPayload);
            Assert.AreEqual(comment, this.commentUpdatedEvent.PublishArgumentPayload);
            Assert.AreEqual(260, this.commentUpdatedEvent.PublishArgumentPayload.MarkIn);
            Assert.AreEqual(320, this.commentUpdatedEvent.PublishArgumentPayload.MarkOut);
        }

        /// <summary>
        /// Should decrease mark in value when old position is greater than new position and  
        /// element position type is InPosition.
        /// </summary>
        [TestMethod]
        public void ShouldDecreaseMarkInValueWhenOldPositionIsGreaterThanNewPositionAndElementPositionTypeIsInPosition()
        {
            TimelineElement element = new TimelineElement { Asset = new VideoAsset() };
            Comment comment = new Comment
            {
                MarkIn = 200,
                MarkOut = 320,
                CommentType = CommentType.Shot
            };

            element.Comments.Add(comment);

            this.sequenceModel.CommentElements.Add(comment);

            var oldPosition = TimeCode.FromSeconds(120d, SmpteFrameRate.Smpte2997NonDrop);
            var newPosition = TimeCode.FromSeconds(60d, SmpteFrameRate.Smpte2997NonDrop);
            var payload = new ElementMovedPayload(element, ElementPositionType.InPosition, oldPosition, newPosition);

            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.commentUpdatedEvent.PublishCalled);
            Assert.IsNull(this.commentUpdatedEvent.PublishArgumentPayload);

            this.elementMovedEvent.SubscribeArgumentAction(payload);

            Assert.IsTrue(this.commentUpdatedEvent.PublishCalled);
            Assert.IsNotNull(this.commentUpdatedEvent.PublishArgumentPayload);
            Assert.AreEqual(comment, this.commentUpdatedEvent.PublishArgumentPayload);
            Assert.AreEqual(140, this.commentUpdatedEvent.PublishArgumentPayload.MarkIn);
            Assert.AreEqual(320, this.commentUpdatedEvent.PublishArgumentPayload.MarkOut);
        }

        /// <summary>
        /// Should increase mark out value when new position is greater than old position and  
        /// element position type is OutPosition.
        /// </summary>
        [TestMethod]
        public void ShouldIncreaseMarkOutValueWhenNewPositionIsGreaterThanOldPositionAndElementPositionTypeIsOutPosition()
        {
            TimelineElement element = new TimelineElement { Asset = new VideoAsset() };
            Comment comment = new Comment
            {
                MarkIn = 200,
                MarkOut = 320,
                CommentType = CommentType.Shot
            };

            element.Comments.Add(comment);

            this.sequenceModel.CommentElements.Add(comment);

            var oldPosition = TimeCode.FromSeconds(60d, SmpteFrameRate.Smpte2997NonDrop);
            var newPosition = TimeCode.FromSeconds(120d, SmpteFrameRate.Smpte2997NonDrop);
            var payload = new ElementMovedPayload(element, ElementPositionType.OutPosition, oldPosition, newPosition);

            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.commentUpdatedEvent.PublishCalled);
            Assert.IsNull(this.commentUpdatedEvent.PublishArgumentPayload);

            this.elementMovedEvent.SubscribeArgumentAction(payload);

            Assert.IsTrue(this.commentUpdatedEvent.PublishCalled);
            Assert.IsNotNull(this.commentUpdatedEvent.PublishArgumentPayload);
            Assert.AreEqual(comment, this.commentUpdatedEvent.PublishArgumentPayload);
            Assert.AreEqual(200, this.commentUpdatedEvent.PublishArgumentPayload.MarkIn);
            Assert.AreEqual(380, this.commentUpdatedEvent.PublishArgumentPayload.MarkOut);
        }

        /// <summary>
        /// Should decrease mark out value when old position is greater 
        /// than new position and element position type is OutPosition.
        /// </summary>
        [TestMethod]
        public void ShouldDecreaseMarkOutValueWhenOldPositionIsGreaterThanNewPositionAndElementPositionTypeIsOutPosition()
        {
            TimelineElement element = new TimelineElement { Asset = new VideoAsset() };
            Comment comment = new Comment
            {
                MarkIn = 200,
                MarkOut = 320,
                CommentType = CommentType.Shot
            };

            element.Comments.Add(comment);

            this.sequenceModel.CommentElements.Add(comment);

            var oldPosition = TimeCode.FromSeconds(120d, SmpteFrameRate.Smpte2997NonDrop);
            var newPosition = TimeCode.FromSeconds(60d, SmpteFrameRate.Smpte2997NonDrop);
            var payload = new ElementMovedPayload(element, ElementPositionType.OutPosition, oldPosition, newPosition);

            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.commentUpdatedEvent.PublishCalled);
            Assert.IsNull(this.commentUpdatedEvent.PublishArgumentPayload);

            this.elementMovedEvent.SubscribeArgumentAction(payload);

            Assert.IsTrue(this.commentUpdatedEvent.PublishCalled);
            Assert.IsNotNull(this.commentUpdatedEvent.PublishArgumentPayload);
            Assert.AreEqual(comment, this.commentUpdatedEvent.PublishArgumentPayload);
            Assert.AreEqual(200, this.commentUpdatedEvent.PublishArgumentPayload.MarkIn);
            Assert.AreEqual(260, this.commentUpdatedEvent.PublishArgumentPayload.MarkOut);
        }

        /// <summary>
        /// Should increase mark in and mark out values when new position is greater 
        /// than old position and element position type is position.
        /// </summary>
        [TestMethod]
        public void ShouldIncreaseMarkInAndMarkOutValuesWhenNewPositionIsGreaterThanOldPositionAndElementPositionTypeIsPosition()
        {
            TimelineElement element = new TimelineElement { Asset = new VideoAsset() };
            Comment comment = new Comment
            {
                MarkIn = 200,
                MarkOut = 320,
                CommentType = CommentType.Shot
            };

            element.Comments.Add(comment);

            this.sequenceModel.CommentElements.Add(comment);

            var oldPosition = TimeCode.FromSeconds(60d, SmpteFrameRate.Smpte2997NonDrop);
            var newPosition = TimeCode.FromSeconds(120d, SmpteFrameRate.Smpte2997NonDrop);
            var payload = new ElementMovedPayload(element, ElementPositionType.Position, oldPosition, newPosition);

            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.commentUpdatedEvent.PublishCalled);
            Assert.IsNull(this.commentUpdatedEvent.PublishArgumentPayload);

            this.elementMovedEvent.SubscribeArgumentAction(payload);

            Assert.IsTrue(this.commentUpdatedEvent.PublishCalled);
            Assert.IsNotNull(this.commentUpdatedEvent.PublishArgumentPayload);
            Assert.AreEqual(comment, this.commentUpdatedEvent.PublishArgumentPayload);
            Assert.AreEqual(260, this.commentUpdatedEvent.PublishArgumentPayload.MarkIn);
            Assert.AreEqual(380, this.commentUpdatedEvent.PublishArgumentPayload.MarkOut);
        }

        /// <summary>
        /// Should decrease mark in and mark out values when old position is greater  
        /// than new position and element position type is position.
        /// </summary>
        [TestMethod]
        public void ShouldDecreaseMarkInAndMarkOutValuesWhenOldPositionIsGreaterThanNewPositionAndElementPositionTypeIsPosition()
        {
            TimelineElement element = new TimelineElement { Asset = new VideoAsset() };
            Comment comment = new Comment
            {
                MarkIn = 200,
                MarkOut = 320,
                CommentType = CommentType.Shot
            };
            element.Comments.Add(comment);

            this.sequenceModel.CommentElements.Add(comment);

            var oldPosition = TimeCode.FromSeconds(120d, SmpteFrameRate.Smpte2997NonDrop);
            var newPosition = TimeCode.FromSeconds(60d, SmpteFrameRate.Smpte2997NonDrop);
            var payload = new ElementMovedPayload(element, ElementPositionType.Position, oldPosition, newPosition);

            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.commentUpdatedEvent.PublishCalled);
            Assert.IsNull(this.commentUpdatedEvent.PublishArgumentPayload);

            this.elementMovedEvent.SubscribeArgumentAction(payload);

            Assert.IsTrue(this.commentUpdatedEvent.PublishCalled);
            Assert.IsNotNull(this.commentUpdatedEvent.PublishArgumentPayload);
            Assert.AreEqual(comment, this.commentUpdatedEvent.PublishArgumentPayload);
            Assert.AreEqual(140, this.commentUpdatedEvent.PublishArgumentPayload.MarkIn);
            Assert.AreEqual(260, this.commentUpdatedEvent.PublishArgumentPayload.MarkOut);
        }

        [TestMethod]
        public void ShouldAddViewToRegionWhenDisplayCommentsViewEventIsPublished()
        {
            this.CreatePresentationModel();
            
            Assert.IsNull(this.markerBrowserRegion.SelectedItem);
            
            this.displayMarkerBrowserEvent.SubscribeAction(SelectedMarkersBrowserTab.Comments);

            Assert.AreSame(this.markerBrowserRegion.SelectedItem, this.view);
        }

        /// <summary>
        /// Creates the presentation model.
        /// </summary>
        /// <returns>The <see cref="ICommentViewPresentationModel"/>.</returns>
        private ICommentViewPresentationModel CreatePresentationModel()
        {
            return new CommentViewPresentationModel(this.view, this.eventAggregator, this.configurationService, this.projectService, this.sequenceRegistry, this.regionManager, this.thumbnailService);
        }
    }
}
