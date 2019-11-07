// <copyright file="SettingsViewPresentationModelFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SettingsViewPresentationModelFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Settings.Tests.Views
{
    using System;
    using System.Windows.Media.Imaging;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Regions;
    using Microsoft.Silverlight.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Events;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using RCE.Modules.Settings.Tests.Mocks;

    using SMPTETimecode;

    /// <summary>
    /// Test class for <see cref="SettingsViewPresentationModel"/>.
    /// </summary>
    [TestClass]
    public class SettingsViewPresentationModelFixture : SilverlightTest
    {
        /// <summary>
        /// Mock for <see cref="SettingsView"/>.
        /// </summary>
        private MockSettingsView view;

        /// <summary>
        /// Mock for <see cref="IEventAggregator"/>.
        /// </summary>
        private MockEventAggregator eventAggregator;

        /// <summary>
        /// Mock for <see cref="AspectRatioChangedEvent"/>.
        /// </summary>
        private MockAspectRatioChangedEvent aspectRatioChangedEvent;

        /// <summary>
        /// Mock for <see cref="SmpteTimeCodeChangedEvent"/>.
        /// </summary>
        private MockSmtpeTimeCodeChangedEvent smpteTimeCodeChangedEvent;

        /// <summary>
        /// Mock for <see cref="StartTimeCodeChangedEvent"/>.
        /// </summary>
        private MockStartTimeCodeChangedEvent startTimeCodeChangedEvent;

        /// <summary>
        /// Mock for <see cref="EditModeChangedEvent"/>.
        /// </summary>
        private MockEditModeChangedEvent editModeChangedEvent;

        /// <summary>
        /// Mock for <see cref="PickThumbnailEvent"/>.
        /// </summary>
        private MockPickThumbnailEvent pickThumbnailEvent;

        /// <summary>
        /// Mock for <see cref="thumbnailEvent"/>.
        /// </summary>
        private MockThumbnailEvent thumbnailEvent;

        /// <summary>
        /// Mock for <see cref="IProjectService"/>.
        /// </summary>
        private MockProjectService projectService;

        private MockConfigurationService configurationService;

        /// <summary>
        /// Mock for <see cref="IRegionManager"/>.
        /// </summary>
        private MockRegionManager regionManager;

        /// <summary>
        /// Mock for <see cref="IRegion"/>.
        /// </summary>
        private MockRegion mainRegion;

        private MockUserSettingsService userSettingsService;

        private MockPersistenceService persistenceService;

        private MockResetWindowsEvent resetWindowsEvent;

        /// <summary>
        /// Initializes the data.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.view = new MockSettingsView();
            this.projectService = new MockProjectService();
            this.configurationService = new MockConfigurationService();

            this.projectService.GetCurrentProjectReturnValue.AutoSaveInterval = 30;
            this.aspectRatioChangedEvent = new MockAspectRatioChangedEvent();
            this.smpteTimeCodeChangedEvent = new MockSmtpeTimeCodeChangedEvent();
            this.startTimeCodeChangedEvent = new MockStartTimeCodeChangedEvent();
            this.editModeChangedEvent = new MockEditModeChangedEvent();
            this.pickThumbnailEvent = new MockPickThumbnailEvent();
            this.resetWindowsEvent = new MockResetWindowsEvent();
            this.thumbnailEvent = new MockThumbnailEvent();
            this.regionManager = new MockRegionManager();
            this.mainRegion = new MockRegion { Name = RegionNames.MainRegion };
            this.userSettingsService = new MockUserSettingsService();
            this.persistenceService = new MockPersistenceService();

            this.regionManager.Regions.Add(this.mainRegion);

            this.eventAggregator = new MockEventAggregator();
            this.eventAggregator.AddMapping<AspectRatioChangedEvent>(this.aspectRatioChangedEvent);
            this.eventAggregator.AddMapping<SmpteTimeCodeChangedEvent>(this.smpteTimeCodeChangedEvent);
            this.eventAggregator.AddMapping<StartTimeCodeChangedEvent>(this.startTimeCodeChangedEvent);
            this.eventAggregator.AddMapping<EditModeChangedEvent>(this.editModeChangedEvent);
            this.eventAggregator.AddMapping<PickThumbnailEvent>(this.pickThumbnailEvent);
            this.eventAggregator.AddMapping<ThumbnailEvent>(this.thumbnailEvent);
            this.eventAggregator.AddMapping<ResetWindowsEvent>(this.resetWindowsEvent);
        }

        /// <summary>
        /// Tests if the view is initiated.
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
        /// Should be on 16x9 aspect ratio by default.
        /// </summary>
        [TestMethod]
        public void ShouldBeOn16x9AspectRatioByDefault()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.IsTrue(presentationModel.IsAspectRatio169Selected);
        }

        /// <summary>
        /// Should not be on 4x3 aspect ratio by default.
        /// </summary>
        [TestMethod]
        public void ShouldNotBeOn4x3AspectRatioByDefault()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(presentationModel.IsAspectRatio43Selected);
        }

        [TestMethod]
        public void ShouldCallLoadSettings()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.IsTrue(this.userSettingsService.LoadSettingsCalled);
        }

        /// <summary>
        /// Should use the project settings for the auto save time interval.
        /// </summary>
        [TestMethod]
        public void ShouldUseProjectSettingsForTheSetTheSelectedAutoSaveTimeInterval()
        {
            this.projectService.GetCurrentProjectReturnValue.AutoSaveInterval = 17;

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(17, presentationModel.SelectedAutoSaveTimeInterval);
        }

        /// <summary>
        /// Should use project settings for the edit mode.
        /// </summary>
        [TestMethod]
        public void ShouldUseProjectSettingsForTheSetTheSelectedEditMode()
        {
            this.projectService.GetCurrentProjectReturnValue.RippleMode = true;

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual("Ripple Mode", presentationModel.SelectedEditMode);
        }

        /// <summary>
        /// Should use project settings for the selected smpte time code.
        /// </summary>
        [TestMethod]
        public void ShouldUseProjectSettingsForTheSetTheSelectedSmpteTimeCode()
        {
            this.projectService.GetCurrentProjectReturnValue.SmpteFrameRate = SmpteFrameRate.Smpte2997NonDrop;

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual("SMPTE Non-Drop", presentationModel.SelectedSmpteTimeCode);
        }

        /// <summary>
        /// Should use project settings for the selected start time code.
        /// </summary>
        [TestMethod]
        public void ShouldUseProjectSettingsForTheSetTheSelectedStartTimeCode()
        {
            this.projectService.GetCurrentProjectReturnValue.StartTimeCode = TimeCode.FromSeconds(3603.62, SmpteFrameRate.Smpte2997NonDrop);

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual("01:00:00:00", presentationModel.SelectedStartTimeCode);
        }

        /// <summary>
        /// Should convert to correct string from Smpte2997Drop frame rate.
        /// </summary>
        [TestMethod]
        public void ShouldConvertToCorrectStringTheSmpte2997DropFrameRate()
        {
            this.projectService.GetCurrentProjectReturnValue.SmpteFrameRate = SmpteFrameRate.Smpte2997Drop;

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual("SMPTE Drop", presentationModel.SelectedSmpteTimeCode);
        }

        /// <summary>
        /// Should convert to correct string from the smpte30 frame rate.
        /// </summary>
        [TestMethod]
        public void ShouldConvertToCorrectStringTheSmpte30FrameRate()
        {
            this.projectService.GetCurrentProjectReturnValue.SmpteFrameRate = SmpteFrameRate.Smpte30;

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual("SMPTE 30", presentationModel.SelectedSmpteTimeCode);
        }

        /// <summary>
        /// Should convert to correct string from the smpte25 frame rate.
        /// </summary>
        [TestMethod]
        public void ShouldConvertToCorrectStringTheSmpte25FrameRate()
        {
            this.projectService.GetCurrentProjectReturnValue.SmpteFrameRate = SmpteFrameRate.Smpte25;

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual("SMPTE EBU", presentationModel.SelectedSmpteTimeCode);
        }

        /// <summary>
        /// Should convert to correct string from smpte24 frame rate.
        /// </summary>
        [TestMethod]
        public void ShouldConvertToCorrectStringTheSmpte24FrameRate()
        {
            this.projectService.GetCurrentProjectReturnValue.SmpteFrameRate = SmpteFrameRate.Smpte24;

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual("SMPTE Film Sync", presentationModel.SelectedSmpteTimeCode);
        }

        /// <summary>
        /// Should convert to correct string from smpte2398 frame rate.
        /// </summary>
        [TestMethod]
        public void ShouldConvertToCorrectStringTheSmpte2398FrameRate()
        {
            this.projectService.GetCurrentProjectReturnValue.SmpteFrameRate = SmpteFrameRate.Smpte2398;

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual("SMPTE Film Sync IVTC", presentationModel.SelectedSmpteTimeCode);
        }

        /// <summary>
        /// Should convert to correct string from unknown frame rate.
        /// </summary>
        [TestMethod]
        public void ShouldConvertToCorrectStringTheUnknownFrameRate()
        {
            this.projectService.GetCurrentProjectReturnValue.SmpteFrameRate = SmpteFrameRate.Unknown;

            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual("Seconds", presentationModel.SelectedSmpteTimeCode);
        }

        /// <summary>
        /// Should set settings values based on project settings when invoking the project retrieved event.
        /// </summary>
        [TestMethod]
        public void ShouldSetSettingsValuesBasedOnProjectSettingsWhenInvokingTheProjectRetrievedEvent()
        {
            this.projectService.GetCurrentProjectReturnValue.AutoSaveInterval = 29;
            this.projectService.GetCurrentProjectReturnValue.RippleMode = true;
            this.projectService.GetCurrentProjectReturnValue.SmpteFrameRate = SmpteFrameRate.Smpte24;
            this.projectService.GetCurrentProjectReturnValue.StartTimeCode = TimeCode.FromAbsoluteTime(3603.62, SmpteFrameRate.Smpte24);

            this.projectService.State = ProjectState.Retrieving;

            var presentationModel = this.CreatePresentationModel();

            Assert.AreNotEqual(29, presentationModel.SelectedAutoSaveTimeInterval);
            Assert.AreNotEqual("SMPTE Film Sync", presentationModel.SelectedSmpteTimeCode);
            Assert.AreNotEqual("Ripple Mode", presentationModel.SelectedEditMode);
            Assert.AreNotEqual("01:00:03:14", presentationModel.SelectedStartTimeCode);

            this.projectService.InvokeProjectRetrieved();

            Assert.AreEqual(29, presentationModel.SelectedAutoSaveTimeInterval);
            Assert.AreEqual("SMPTE Film Sync", presentationModel.SelectedSmpteTimeCode);
            Assert.AreEqual("Ripple Mode", presentationModel.SelectedEditMode);
            Assert.AreEqual("01:00:03:14", presentationModel.SelectedStartTimeCode);
        }

        /// <summary>
        /// Should call to Save after the auto save interval time passed.
        /// </summary>
        [TestMethod]
        [Asynchronous]
        public void ShouldCallToSaveAfterTheAutoSaveIntervalTimePassed()
        {
            // interval = 1 minute. Let the test run, it is not failing.
            this.projectService.GetCurrentProjectReturnValue.AutoSaveInterval = 0.01m;

            var presentationModel = this.CreatePresentationModel();

            EnqueueConditional(() => this.projectService.SaveProjectCalled);
            EnqueueConditional(() => this.userSettingsService.SaveSettingsCalled);
            EnqueueTestComplete();
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when IsAspectRatio43IsSelected is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenIsAspectRatio43IsSelectedIsUpdated()
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

            presentationModel.IsAspectRatio43Selected = true;

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("IsAspectRatio43Selected", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when IsAspectRatio169Selected is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenIsAspectRatio169IsSelectedIsUpdated()
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

            presentationModel.IsAspectRatio169Selected = true;

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("IsAspectRatio169Selected", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when SelectedSmpteTimeCode is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenSelectedSmpteTimecodeIsUpdated()
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

            presentationModel.SelectedSmpteTimeCode = "SMPTE Drop";

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("SelectedSmpteTimeCode", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when SelectedStartTimeCode is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenSelectedStartTimecodeIsUpdated()
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

            presentationModel.SelectedStartTimeCode = "01:00:00:20";

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("SelectedStartTimeCode", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when SelectedAutoSaveTimeInterval is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenAutoSaveTimeIntervalIsUpdated()
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

            presentationModel.SelectedAutoSaveTimeInterval = 20;

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("SelectedAutoSaveTimeInterval", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when IsAspectRatio43IsSelected is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenSelectedEditModeIsUpdated()
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

            presentationModel.SelectedEditMode = "Ripple Mode";

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("SelectedEditMode", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Should publish aspect ratio changed event when updating IsAspectRatio43Selected.
        /// </summary>
        [TestMethod]
        public void ShouldPublishAspectRatioChangedEventWhenUpdatingIsAspectRatio43Selected()
        {
            var presentationModel = this.CreatePresentationModel();

            this.aspectRatioChangedEvent.PublishCalled = false;

            presentationModel.IsAspectRatio43Selected = true;

            Assert.IsTrue(this.aspectRatioChangedEvent.PublishCalled);
            Assert.AreEqual(AspectRatio.Square, this.aspectRatioChangedEvent.PublishArgumentPayload);
        }

        /// <summary>
        /// Should publish aspect ratio changed event when updating IsAspectRatio169Selected.
        /// </summary>
        [TestMethod]
        public void ShouldPublishAspectRatioChangedEventWhenUpdatingIsAspectRatio169Selected()
        {
            var presentationModel = this.CreatePresentationModel();

            this.aspectRatioChangedEvent.PublishCalled = false;

            presentationModel.IsAspectRatio169Selected = true;

            Assert.IsTrue(this.aspectRatioChangedEvent.PublishCalled);
            Assert.AreEqual(AspectRatio.Wide, this.aspectRatioChangedEvent.PublishArgumentPayload);
        }

        /// <summary>
        /// Should throw exception if hour part of time code is not valid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InputValidationException))]
        public void ShouldThrowExceptionIfHourPartOfTimeCodeIsNotValid()
        {
            ISettingsViewPresentationModel presentationModel = this.CreatePresentationModel();
            presentationModel.SelectedStartTimeCode = "ab:23:34;34";
        }

        /// <summary>
        /// Should not throw exception and set selected start time code if time code is valid.
        /// </summary>
        [TestMethod]
        public void ShouldNotThrowExceptionAndSetSelectedStartTimeCodeStIfTimeCodeIsValid()
        {
            ISettingsViewPresentationModel presentationModel = this.CreatePresentationModel();
            presentationModel.SelectedStartTimeCode = "00:01:01;00";

            Assert.IsTrue(presentationModel.SelectedStartTimeCode == "00:01:01;00");
        }

        /// <summary>
        /// Should throw exception if minute part of time code is not valid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InputValidationException))]
        public void ShouldThrowExceptionIfMinutePartOfTimeCodeIsNotValid()
        {
            ISettingsViewPresentationModel presentationModel = this.CreatePresentationModel();
            presentationModel.SelectedStartTimeCode = "12:80:34;24";
        }

        /// <summary>
        /// Should throw exception if second part of time code is not valid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InputValidationException))]
        public void ShouldThrowExceptionIfSecondPartOfTimeCodeIsNotValid()
        {
            ISettingsViewPresentationModel presentationModel = this.CreatePresentationModel();
            presentationModel.SelectedStartTimeCode = "12:23:80;23";
        }

        /// <summary>
        /// Should throw exception if the start time code is greater than the DefaultTimelineDuration.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InputValidationException))]
        public void ShouldThrowExceptionIfStartTimeCodeIsGreaterThanDefaultTimelineDuration()
        {
            ISettingsViewPresentationModel presentationModel = this.CreatePresentationModel();
            presentationModel.SelectedStartTimeCode = "02:59:32;25";
        }

        /// <summary>
        /// Should throw exception if frame part of time code is not valid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InputValidationException))]
        public void ShouldThrowExceptionIfFramePartOfTimeCodeIsNotValid()
        {
            ISettingsViewPresentationModel presentationModel = this.CreatePresentationModel();
            presentationModel.SelectedStartTimeCode = "12:23:34;34";
        }

        /// <summary>
        /// Should throw exception if time code is not valid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InputValidationException))]
        public void ShouldThrowExceptionIfTimeCodeIsNotValid()
        {
            ISettingsViewPresentationModel presentationModel = this.CreatePresentationModel();
            presentationModel.SelectedStartTimeCode = "ab:34;34";
        }

        /// <summary>
        /// Should subscribe to edit mode changed event.
        /// </summary>
        [TestMethod]
        public void ShouldSubscribeToEditModeChangedEvent()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.IsNotNull(this.editModeChangedEvent.SubscribeArgumentAction);
            Assert.IsNotNull(this.editModeChangedEvent.SubscribeArgumentThreadOption);
        }

        /// <summary>
        /// Should change the edit mode if it is not same when edit mode changed event is raised.
        /// </summary>
        [TestMethod]
        public void ShouldChangeTheEditModeIfItIsNotSameWhenEditModeChangedEventIsRaised()
        {
            var presentationModel = this.CreatePresentationModel();
            presentationModel.SelectedEditMode = "Gap Mode";
            this.editModeChangedEvent.SubscribeArgumentAction.Invoke(EditMode.Ripple);

            Assert.IsTrue(presentationModel.SelectedEditMode == "Ripple Mode");
        }

        /// <summary>
        /// Should change the edit mode if it is same when edit mode changed event is raised.
        /// </summary>
        [TestMethod]
        public void ShouldNotChangeTheEditModeIfItIsSameWhenEditModeChangedEventIsRaised()
        {
            var propertyChangedRaised = false;
            string propertyChangedEventArgsArgument = null;

            var presentationModel = this.CreatePresentationModel();
            presentationModel.SelectedEditMode = "Gap Mode";
            presentationModel.PropertyChanged += (sender, e) =>
            {
                propertyChangedRaised = true;
                propertyChangedEventArgsArgument = e.PropertyName;
            };

            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);

            this.editModeChangedEvent.SubscribeArgumentAction.Invoke(EditMode.Gap);
            
            Assert.IsTrue(presentationModel.SelectedEditMode == "Gap Mode");
            Assert.IsFalse(propertyChangedRaised);
            Assert.IsNull(propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Tests if the HeaderInfo property returns the expected value.
        /// </summary>
        [TestMethod]
        public void ShouldReturnHeaderInfoResource()
        {
            var presenter = this.CreatePresentationModel();

            var result = presenter.HeaderInfo;

            Assert.AreEqual("Settings", result);
        }

        /// <summary>
        /// Tests if the HeaderIconOff property returns the expected value.
        /// </summary>
        [TestMethod]
        public void ShouldReturnHeaderIconOffResource()
        {
            var presenter = this.CreatePresentationModel();

            var result = presenter.HeaderIconOff;

            Assert.AreEqual("/RCE.Modules.Settings;component/images/icon_off.png", result);
        }

        /// <summary>
        /// Tests if the HeaderIconOn property returns the expected value.
        /// </summary>
        [TestMethod]
        public void ShouldReturnHeaderIconOnResource()
        {
            var presenter = this.CreatePresentationModel();

            var result = presenter.HeaderIconOn;

            Assert.AreEqual("/RCE.Modules.Settings;component/images/icon_on.png", result);
        }

        /// <summary>
        /// Tests if the Activate of Region is called when Activate method is called.
        /// </summary>
        [TestMethod]
        public void ShouldActivateTheViewIfKeyboardMappingIsExecuted()
        {
            this.mainRegion.SelectedItem = null;
            var presentationModel = this.CreatePresentationModel();

            presentationModel.KeyboardActionCommand.Execute(new Tuple<KeyboardAction, object>(KeyboardAction.ActivateModel, null));

            Assert.AreSame(this.view, this.mainRegion.SelectedItem);
        }

        /// <summary>
        /// Should raise the OnPropertyChanged event when project name is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenProjectNameIsUpdated()
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

            presentationModel.ProjectName = "test";

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("ProjectName", propertyChangedEventArgsArgument);
        }

        /// <summary>
        /// Shoulud set the project name in project service when ProjectName property is updated.
        /// </summary>
        [TestMethod]
        public void ShouldSetTheProjectNameInProjectServiceWhenProjectNamePropertyIsUpdated()
        {
            this.projectService.GetCurrentProject().Name = "test";
            var presentationModel = this.CreatePresentationModel();

            presentationModel.ProjectName = "TestProject";

            Assert.AreSame(this.projectService.GetCurrentProject().Name, "TestProject");
        }

        /// <summary>
        /// Should throw exception if project name is more than 100 characters.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InputValidationException))]
        public void ShouldThrowExceptionIfProjectNameIsMoreThan100Characters()
        {
            this.projectService.GetCurrentProject().Name = "test";
            var presentationModel = this.CreatePresentationModel();

            presentationModel.ProjectName = "01234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567891";
        }

        /// <summary>
        /// Should raise OnPropertyChanged event when ThumbImage is updated.
        /// </summary>
        [TestMethod]
        public void ShouldRaiseOnPropertyChangedEventWhenThumbImageIsUpdated()
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

            presentationModel.ThumbImage = new BitmapImage();

            Assert.IsTrue(propertyChangedRaised);
            Assert.AreEqual("ThumbImage", propertyChangedEventArgsArgument);
        }

        [TestMethod]
        public void ShouldNotCanExecuteDeleteThumbnailCommandIsProjectThumbnailIsEmpty()
        {
            this.projectService.GetCurrentProject().ProjectThumbnail = null;

            var presentationModel = this.CreatePresentationModel();

            var result = presentationModel.DeleteThumbnailCommand.CanExecute(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ShouldClearProjectThumbnailOnCurrentProjectWhenExecutingDeleteThumbnailCommand()
        {
            this.projectService.GetCurrentProject().ProjectThumbnail = "xx1x23x45xx5dxasX4/54X31Xxxxxx";
            
            var presentationModel = this.CreatePresentationModel();

            Assert.IsNotNull(this.projectService.GetCurrentProject().ProjectThumbnail);

            presentationModel.DeleteThumbnailCommand.Execute(null);

            Assert.IsNull(this.projectService.GetCurrentProject().ProjectThumbnail);
        }

        /// <summary>
        /// Tests that the ThumbnailEvent event is being published when the SetThumbnail method is being called.
        /// </summary>
        [TestMethod]
        public void ShouldPublishPickThumbnailEventWhenExecutingPickThumbnailCommand()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.pickThumbnailEvent.PublishCalled);

            presentationModel.PickThumbnailCommand.Execute(null);

            Assert.IsTrue(this.pickThumbnailEvent.PublishCalled);
        }

        [TestMethod]
        public void ShouldCallToIncreaseQuotaWhenIncreaseStorageQuotaCommandIsExecuted()
        {
            this.persistenceService.Quota = 1;
            this.persistenceService.AvailableFreeSpace = 1;
            this.persistenceService.UsedSize = 0;

            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.persistenceService.IncreaseQuotaCalled);

            presentationModel.IncreaseStorageQuotaCommand.Execute(null);

            Assert.IsTrue(this.persistenceService.IncreaseQuotaCalled);
        }

        [TestMethod]
        public void ShouldCallToRemoveStorageWhenClearStorageCommandIsExecuted()
        {
            this.persistenceService.Quota = 1;
            this.persistenceService.AvailableFreeSpace = 1;
            this.persistenceService.UsedSize = 0;

            var presentationModel = this.CreatePresentationModel();

            Assert.IsFalse(this.persistenceService.RemoveStorageCalled);

            presentationModel.ClearStorageCommand.Execute(null);

            Assert.IsTrue(this.persistenceService.RemoveStorageCalled);
        }

        /// <summary>
        /// Creates the presentation model.
        /// </summary>
        /// <returns>The <see cref="ISettingsViewPresentationModel"/>.</returns>
        private ISettingsViewPresentationModel CreatePresentationModel()
        {
            return new SettingsViewPresentationModel(this.view, this.eventAggregator, this.projectService, this.regionManager, this.userSettingsService, this.persistenceService, this.configurationService);
        }
    }
}
