// <copyright file="MenuButtonViewModelFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MenuButtonViewModelFixture.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure.Tests.ViewModels
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Infrastructure.Menu;
    using RCE.Infrastructure.Tests.Mocks;

    [TestClass]
    public class MenuButtonViewModelFixture
    {
        private MockMenuButtonView menuButtonView;

        private MockRegionManager regionManager;

        private object viewToDisplay;

        private MockRegion mainRegion;

        [TestInitialize]
        public void TestInitialize()
        {
            this.menuButtonView = new MockMenuButtonView();
            this.regionManager = new MockRegionManager();

            this.mainRegion = new MockRegion { Name = "MainRegion" };

            this.regionManager.Regions.Add(this.mainRegion);
            this.viewToDisplay = new object();
        }
        
        [TestMethod]
        public void ShouldViewPropertyPassedThroughConstructor()
        {
            MenuButtonViewModel subClipMenuButtonViewModel = this.CreateViewModel();

            Assert.AreEqual(this.menuButtonView, subClipMenuButtonViewModel.View);
        }

        [TestMethod]
        public void ShouldSetSelfAsViewModel()
        {
            MenuButtonViewModel subClipMenuButtonViewModel = this.CreateViewModel();

            Assert.AreSame(subClipMenuButtonViewModel, this.menuButtonView.ViewModelArgument);
        }

        [TestMethod]
        public void ShouldAddViewToMainRegionIfViewIsNotActiveAndClickCommandIsExecuted()
        {
            MenuButtonViewModel subClipMenuButtonViewModel = this.CreateViewModel();

            subClipMenuButtonViewModel.IsViewActive = false;
            subClipMenuButtonViewModel.ViewToDisplay = this.viewToDisplay;

            Assert.AreEqual(0, this.mainRegion.AddedViews.Count);

            subClipMenuButtonViewModel.ClickCommand.Execute(null);

            Assert.AreEqual(1, this.mainRegion.AddedViews.Count);
            Assert.AreEqual(this.viewToDisplay, this.mainRegion.AddedViews[0]);
            Assert.IsTrue(subClipMenuButtonViewModel.IsViewActive);
        }

        [TestMethod]
        public void ShouldRemoveViewFromMainRegionIfViewIsActiveAndClickCommandIsExecuted()
        {
            MenuButtonViewModel subClipMenuButtonViewModel = this.CreateViewModel();

            subClipMenuButtonViewModel.IsViewActive = true;
            subClipMenuButtonViewModel.ViewToDisplay = this.viewToDisplay;

            this.mainRegion.Add(this.viewToDisplay);

            Assert.AreEqual(1, this.mainRegion.AddedViews.Count);

            subClipMenuButtonViewModel.ClickCommand.Execute(null);

            Assert.AreEqual(0, this.mainRegion.AddedViews.Count);
            Assert.IsFalse(subClipMenuButtonViewModel.IsViewActive);
        }

        [TestMethod]
        public void ShouldRaiseTextPropertyChangedWhenTextChanges()
        {
            MenuButtonViewModel subClipMenuButtonViewModel = this.CreateViewModel();

            bool called = false;
            subClipMenuButtonViewModel.PropertyChanged += (s, a) =>
                {
                    if (a.PropertyName == "Text")
                    {
                        called = true;
                    }
                };

            Assert.IsFalse(called);

            subClipMenuButtonViewModel.Text = "New text";

            Assert.IsTrue(called);
        }

        [TestMethod]
        public void ShouldRaiseIsViewActivePropertyChangedWhenIsViewActiveChanges()
        {
            MenuButtonViewModel subClipMenuButtonViewModel = this.CreateViewModel();

            bool called = false;
            subClipMenuButtonViewModel.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == "IsViewActive")
                {
                    called = true;
                }
            };

            Assert.IsFalse(called);

            subClipMenuButtonViewModel.IsViewActive = true;

            Assert.IsTrue(called);
        }

        private MenuButtonViewModel CreateViewModel()
        {
            return new MenuButtonViewModel(this.menuButtonView, this.regionManager);
        }
    }
}