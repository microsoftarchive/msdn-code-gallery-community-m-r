// <copyright file="IncreasePersistenceQuotaViewModelFixture.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IncreasePersistenceQuotaViewModelFixture.cs                     
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
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using RCE.Modules.Settings.Tests.Mocks;
    using RCE.Modules.Settings.Views;

    [TestClass]
    public class IncreasePersistenceQuotaViewModelFixture                        
    {
        private MockPersistenceService persistenceService;

        private MockIncreasePersistenceQuotaDialog increaseQuotaDialog;

        [TestInitialize]
        public void TestInitialize()
        {
            this.persistenceService = new MockPersistenceService();
            this.increaseQuotaDialog = new MockIncreasePersistenceQuotaDialog();
        }

        [TestMethod]
        public void ShouldShowDialogIfQuotaIsLessThan500MB()
        {
            // 100 MB
            long bytes = 100 * 1024 * 1024;

            this.persistenceService.Quota = bytes;

            Assert.IsFalse(this.increaseQuotaDialog.ShowWasCalled);

            var viewModel = this.CreateViewModel();

            Assert.IsTrue(this.increaseQuotaDialog.ShowWasCalled);
            Assert.AreSame(viewModel, this.increaseQuotaDialog.ViewModel);
        }

        [TestMethod]
        public void ShouldNotShowDialogIfQutaIsMoreThan500MB()
        {
            // 600 MB
            long bytes = 600 * 1024 * 1024;

            this.persistenceService.Quota = bytes;

            var viewModel = this.CreateViewModel();

            Assert.IsFalse(this.increaseQuotaDialog.ShowWasCalled);
            Assert.AreNotSame(viewModel, this.increaseQuotaDialog.ViewModel);
        }

        [TestMethod]
        public void ShouldNotShowDialogIfQuotaIsEqualTo500MB()
        {
            // 500 MB
            long bytes = 500 * 1024 * 1024;

            this.persistenceService.Quota = bytes;

            var viewModel = this.CreateViewModel();

            Assert.IsFalse(this.increaseQuotaDialog.ShowWasCalled);
            Assert.AreNotSame(viewModel, this.increaseQuotaDialog.ViewModel);
        }

        [TestMethod]
        public void ShouldCallToIncreaseQuotaWithDiffernceTo500MBWhenIncreaseStorageQuotaCommandIsExecuted()
        {
            long megabyte = 1024 * 1024;

            this.persistenceService.Quota = 100 * megabyte;

            var viewModel = this.CreateViewModel();

            Assert.IsFalse(this.persistenceService.IncreaseQuotaCalled);
            
            viewModel.IncreaseStorageQuotaCommand.Execute(null);
            
            Assert.IsTrue(this.persistenceService.IncreaseQuotaCalled);
            Assert.AreEqual(400 * megabyte, this.persistenceService.QuotaToIncrease);
        }

        [TestMethod]
        public void ShouldCloseDialogWhenIncreaseStorageQuotaCommandIsExecuted()
        {
            var viewModel = this.CreateViewModel();

            Assert.IsFalse(this.increaseQuotaDialog.CloseWasCalled);

            viewModel.IncreaseStorageQuotaCommand.Execute(null);

            Assert.IsTrue(this.increaseQuotaDialog.CloseWasCalled);
        }

        [TestMethod]
        public void ShouldCloseDialogWhenCloseCommandIsExecuted()
        {
            var viewModel = this.CreateViewModel();

            Assert.IsFalse(this.increaseQuotaDialog.CloseWasCalled);

            viewModel.CloseCommand.Execute(null);

            Assert.IsTrue(this.increaseQuotaDialog.CloseWasCalled);
        }

        [TestMethod]
        public void ShouldReturnMessageWithAmountOfMBAndMBCharactersWhenQuota()
        {
            this.persistenceService.Quota = 450 * 1024 * 1024;
            
            var viewModel = this.CreateViewModel();

            Assert.AreEqual("450 MB", viewModel.QuotaMB);
        }

        public IncreasePersistenceQuotaViewModel CreateViewModel()
        {
            return new IncreasePersistenceQuotaViewModel(this.increaseQuotaDialog, this.persistenceService);
        }
    }
}