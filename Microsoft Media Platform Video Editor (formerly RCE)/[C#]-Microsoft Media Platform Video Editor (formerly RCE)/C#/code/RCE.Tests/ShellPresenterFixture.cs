// <copyright file="ShellPresenterFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ShellPresenterFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Tests
{
    using Infrastructure;
    using Infrastructure.Events;
    using Infrastructure.Models;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Events;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    /// <summary>
    /// Test class for <see cref="ShellPresenter"/>.
    /// </summary>
    [TestClass]
    public class ShellPresenterFixture
    {
        /// <summary>
        /// Mock for <see cref="IEventAggregator"/>.
        /// </summary>
        private MockEventAggregator eventAggregator;

        /// <summary>
        /// Mock for <see cref="keyMappingEvent"/>.
        /// </summary>
        private MockKeyMappingEvent keyMappingEvent;

        /// <summary>
        /// Mock for <see cref="FullScreenEvent"/>.
        /// </summary>
        private MockFullScreenEvent fullScreenEvent;

        /// <summary>
        /// Mock for <see cref="StatusEvent"/>.
        /// </summary>
        private MockStatusEvent statusEvent;

        /// <summary>
        /// Mock for <see cref="saveProjectEvent"/>.
        /// </summary>
        private MockSaveProjectEvent saveProjectEvent;

        /// <summary>
        /// Mock for <see cref="Shell"/>.
        /// </summary>
        private MockShell shell;

        /// <summary>
        /// Initilize the test class.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.shell = new MockShell();
            this.keyMappingEvent = new MockKeyMappingEvent();
            this.fullScreenEvent = new MockFullScreenEvent();
            this.statusEvent = new MockStatusEvent();
            this.saveProjectEvent = new MockSaveProjectEvent();
            this.eventAggregator = new MockEventAggregator();
            
            this.eventAggregator.AddMapping<KeyMappingEvent>(this.keyMappingEvent);
            this.eventAggregator.AddMapping<FullScreenEvent>(this.fullScreenEvent);
            this.eventAggregator.AddMapping<StatusEvent>(this.statusEvent);
            this.eventAggregator.AddMapping<SaveProjectEvent>(this.saveProjectEvent);
        }

        /// <summary>
        /// Tests that <see cref="ShellPresenter"/> initilizes the shell view.
        /// </summary>
        [TestMethod]
        public void CanInitPresenter()
        {
            var presenter = this.CreatePresenter();

            Assert.AreEqual(this.shell, presenter.Shell);
        }

        /// <summary>
        /// Should set presentation model into view.
        /// </summary>
        [TestMethod]
        public void ShouldSetPresentationModelIntoView()
        {
            var presentationModel = this.CreatePresenter();

            Assert.AreSame(presentationModel, this.shell.Model);
        }

        /// <summary>
        /// Tests if <see cref="keyMappingEvent"/> is published when it is invoked.
        /// </summary>
        [TestMethod]
        public void ShouldPublishEventWhenInvokingkeyMappingEvent()
        {
            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.keyMappingEvent.PublishCalled);

            this.shell.InvokeKeyMappingActionEvent(KeyMappingAction.Toggle);

            Assert.IsTrue(this.keyMappingEvent.PublishArgumentPayload == KeyMappingAction.Toggle);
        }

        /// <summary>
        /// Tests if the <see cref="Shell"/> ToggleFullScreen is called when 
        /// <see cref="FullScreenEvent"/> is publishsed.
        /// </summary>
        [TestMethod]
        public void ShouldCallToggleFullScreenWithFullScreenEventSubscription()
        {
            Assert.IsNull(this.fullScreenEvent.SubscribeArgumentAction);
            Assert.IsNull(this.fullScreenEvent.SubscribeArgumentFilter);

            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.shell.ToggleFullScreenCalled);

            Assert.IsNotNull(this.fullScreenEvent.SubscribeArgumentAction);
            Assert.IsNull(this.fullScreenEvent.SubscribeArgumentFilter);
            Assert.AreEqual(ThreadOption.PublisherThread, this.fullScreenEvent.SubscribeArgumentThreadOption);

            this.fullScreenEvent.SubscribeArgumentAction(FullScreenMode.Player);

            Assert.IsTrue(this.shell.ToggleFullScreenCalled);
        }

        /// <summary>
        /// Tests if <see cref="ShellPresenter"/> publishes the <see cref="SaveProjectEvent"/>
        /// when SaveProject event is triggered.
        /// </summary>
        [TestMethod]
        public void ShouldCallSaveProjectWhenSaveProjectEventIsTriggered()
        {
            var presenter = this.CreatePresenter();

            Assert.IsFalse(this.saveProjectEvent.PublishCalled);

            this.shell.InvokeSaveProjectEvent();

            Assert.IsTrue(this.saveProjectEvent.PublishCalled);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when ProjectName is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenProjectNameIsUpdated()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresenter();
            presentationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            presentationModel.ProjectName = "text";

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("ProjectName", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Creates the presenter.
        /// </summary>
        /// <returns>The <see cref="ShellPresenter"/>.</returns>
        private ShellPresenter CreatePresenter()
        {
           return new ShellPresenter(this.shell, this.eventAggregator);
        }
    }
}
