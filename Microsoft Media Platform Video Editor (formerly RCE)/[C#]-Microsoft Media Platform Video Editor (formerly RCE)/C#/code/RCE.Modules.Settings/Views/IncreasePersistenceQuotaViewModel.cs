// <copyright file="IncreasePersistenceQuotaViewModel.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: IncreasePersistenceQuotaViewModel.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Settings.Views
{
    using System;

    using Microsoft.Practices.Composite.Presentation.Commands;

    using RCE.Infrastructure.Services;

    public class IncreasePersistenceQuotaViewModel : IIncreasePersistenceQuotaViewModel
    {
        private const long MinimumRequiredQuota = 500 * 1024 * 1024;
        
        private readonly IIncreasePersistenceQuotaDialog increaseQuotaDialog;

        private readonly IPersistenceService persistenceService;

        public IncreasePersistenceQuotaViewModel(IIncreasePersistenceQuotaDialog increaseQuotaDialog, IPersistenceService persistenceService)
        {
            this.increaseQuotaDialog = increaseQuotaDialog;
            this.persistenceService = persistenceService;
           
            if (this.persistenceService.Quota < MinimumRequiredQuota)
            {
                this.IncreaseStorageQuotaCommand = new DelegateCommand<object>(this.IncreaseStorageQuota);
                this.CloseCommand = new DelegateCommand<object>(this.CloseDialog);
                this.increaseQuotaDialog.ShowDialog();
                this.increaseQuotaDialog.SetViewModel(this);    
            }
        }

        public DelegateCommand<object> IncreaseStorageQuotaCommand { get; set; }

        public DelegateCommand<object> CloseCommand { get; set; }

        public string QuotaMB
        {
            get
            {
                return string.Format("{0} MB", this.persistenceService.Quota / (1024 * 1024));
            }
        }

        private void IncreaseStorageQuota(object obj)
        {
            this.persistenceService.IncreaseQuota(MinimumRequiredQuota - this.persistenceService.Quota);
            this.CloseDialog(null);
        }

        private void CloseDialog(object obj)
        {
            this.increaseQuotaDialog.CloseDialog();
        }
    }
}
