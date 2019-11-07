// <copyright file="EncoderSettingsPresentationModelFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: EncoderSettingsPresentationModelFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.EncoderOutput.Tests.Views
{
    using EncoderOutput.Views;
    using Infrastructure;
    using Infrastructure.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocks;

    using RCE.Infrastructure.Services;

    using Services;

    [TestClass]
    public class EncoderSettingsPresentationModelFixture
    {
        /// <summary>
        /// Mock for <see cref="IEncoderSettingsView"/>.
        /// </summary>
        private MockEncoderSettingsView view;

        /// <summary>
        /// Mock for <see cref="IProjectService"/>.
        /// </summary>
        private MockProjectService projectService;

        /// <summary>
        /// Mock for <see cref="IOutputServiceFacade"/>.
        /// </summary>
        private MockOutputServiceFacade outputServiceFacade;

        /// <summary>
        /// Initializes the data in test class.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.view = new MockEncoderSettingsView();
            this.projectService = new MockProjectService();
            this.outputServiceFacade = new MockOutputServiceFacade();
        }

        /// <summary>
        /// Tests if <see cref="EncoderSettingsPresentationModel"/>
        /// is initilizing the <see cref="EncoderSettingsView"/>.
        /// </summary>
        [TestMethod]
        public void CanInitPresentationModel()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.AreEqual(this.view, presentationModel.View);
        }

        /// <summary>
        /// Test if the presentation model is set into view.
        /// </summary>
        [TestMethod]
        public void ShouldSetPresentationModelIntoView()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.AreSame(presentationModel, this.view.Model);
        }

        /// <summary>
        /// Tests if the constructor initialize the Metadata.
        /// </summary>
        [TestMethod]
        public void ShouldInitializeSettings()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.IsNotNull(presentationModel.Metadata);
        }

        /// <summary>
        /// Tests if the HeaderInfo property returns the expected value.
        /// </summary>
        [TestMethod]
        public void ShouldReturnHeaderInfoResource()
        {
            var presentationModel = this.CreatePresentationModel();

            var result = presentationModel.HeaderInfo;

            Assert.AreEqual(Resources.Resources.HeaderInfo, result);
        }

        /// <summary>
        /// Tests if the contructor populates the ResizeModeOptions collection.
        /// </summary>
        [TestMethod]
        public void ShouldPopulateResizeModeOptions()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.AreNotEqual(0, presentationModel.ResizeModeOptions.Count);
        }

        /// <summary>
        /// Tests if the contructor populates the AspectRatio collection.
        /// </summary>
        [TestMethod]
        public void ShouldPopulateAspectRatioOptions()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.AreNotEqual(0, presentationModel.AspectRatioOptions.Count);
        }

        /// <summary>
        /// Tests if the contructor populates the FrameRate collection.
        /// </summary>
        [TestMethod]
        public void ShouldPopulateFrameRateOptions()
        {
            var presentationModel = this.CreatePresentationModel();

            Assert.AreNotEqual(0, presentationModel.FrameRateOptions.Count);
        }

        /// <summary>
        /// Tests if the  output generator service is being called
        /// when the GenerateOutputAsync command is executed.
        /// </summary>
        [TestMethod]
        public void ShouldCallToGenerateOutputWhenExecutingGenerateOutputCommand()
        {
            var presentationModel = this.CreatePresentationModel();

            this.projectService.GetCurrentProjectReturnValue = new Project { Name = "Test" };

            Assert.IsFalse(this.outputServiceFacade.GenerateOutputCalled);

            presentationModel.GenerateOutputCommand.Execute(null);

            Assert.IsTrue(this.outputServiceFacade.GenerateOutputCalled);
           
            Assert.AreEqual(this.projectService.GetCurrentProjectReturnValue, this.outputServiceFacade.GenerateOutputArgument);
        }

        /// <summary>
        /// Tests if the  output generator service is being called
        /// when the GenerateOutputAsync command is executed.
        /// </summary>
        [TestMethod]
        public void ShouldCallToGenerateCompositeStreamManifestWhenExecutingGenerateOutputCommandAndIsCsmOutputSelected()
        {
            var presentationModel = this.CreatePresentationModel();

            this.projectService.GetCurrentProjectReturnValue = new Project { Name = "Test" };

            Assert.IsFalse(this.outputServiceFacade.GenerateCompositeStreamManifestCalled);

            presentationModel.IsCsmOutput = true;
            presentationModel.PbpDataStreamName = "PBP-Test";
            presentationModel.AdsDataStreamName = "AD-Test";
            presentationModel.GenerateOutputCommand.Execute(null);

            Assert.IsTrue(this.outputServiceFacade.GenerateCompositeStreamManifestCalled);

            Assert.AreEqual(this.projectService.GetCurrentProjectReturnValue, this.outputServiceFacade.GenerateCompositeStreamManifestProjectArgument);
            Assert.AreEqual(presentationModel.PbpDataStreamName, this.outputServiceFacade.GenerateCompositeStreamManifestPbpArgument);
            Assert.AreEqual(presentationModel.AdsDataStreamName, this.outputServiceFacade.GenerateCompositeStreamManifestAdsArgument);
        }

        /// <summary>
        /// Tests if the project metadata is being set
        /// when the GenerateOutputAsync command is executed.
        /// </summary>
        [TestMethod]
        public void ShouldSetProjectMetadataWhenExecutingGenerateOutputCommand()
        {
            var presentationModel = this.CreatePresentationModel();

            this.projectService.GetCurrentProjectReturnValue = new Project { Name = "Test" };

            Assert.IsNull(this.projectService.GetCurrentProjectReturnValue.Metadata);

            presentationModel.GenerateOutputCommand.Execute(null);

            Assert.IsNotNull(this.projectService.GetCurrentProjectReturnValue.Metadata);
        }

        /// <summary>
        /// Test if the ShowProgressBar method is being caleed when generation starts.
        /// </summary>
        [TestMethod]
        public void ShouldCallShowProgressBarOnViewWhenExecutingGenerateOutputCommand()
        {
            var presentationModel = this.CreatePresentationModel();

            this.projectService.GetCurrentProjectReturnValue = new Project { Name = "Test" };

            this.view.ShowProgressBarCalled = false;

            presentationModel.GenerateOutputCommand.Execute(null);

            Assert.IsTrue(this.view.ShowProgressBarCalled);
        }

        /// <summary>
        /// Tests if the HideProgressBar method is being called when generation is completed.
        /// </summary>
        [TestMethod]
        public void ShouldCallHideProgressBarOnViewWhenGenerateIsCompleted()
        {
            var presentationModel = this.CreatePresentationModel();
                        
            this.projectService.GetCurrentProjectReturnValue = new Project { Name = "Test" };

            this.view.HideProgressBarCalled = false;

            presentationModel.GenerateOutputCommand.Execute(null);

            this.outputServiceFacade.InvokeGenerateOuputCompleted(true);

            Assert.IsTrue(this.view.HideProgressBarCalled);
        }

        /// <summary>
        /// Creates the presentation model.
        /// </summary>
        /// <returns>The <see cref="IEncoderSettingsPresentationModel"/>.</returns>
        private IEncoderSettingsPresentationModel CreatePresentationModel()
        {
            return new EncoderSettingsPresentationModel(this.view, this.projectService, this.outputServiceFacade, new MockConfigurationService());
        }
    }
}
