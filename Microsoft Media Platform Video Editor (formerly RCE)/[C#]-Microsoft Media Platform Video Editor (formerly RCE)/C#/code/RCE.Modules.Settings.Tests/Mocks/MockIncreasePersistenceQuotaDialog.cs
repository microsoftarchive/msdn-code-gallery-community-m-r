// <copyright file="MockIncreasePersistenceQuotaDialog.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockIncreasePersistenceQuotaDialog.cs                    
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Settings.Tests.Mocks
{
    using RCE.Modules.Settings.Views;

    public class MockIncreasePersistenceQuotaDialog : IIncreasePersistenceQuotaDialog
    {
        public bool ShowWasCalled { get; set; }

        public object ViewModel { get; set; }

        public bool CloseWasCalled { get; set; }

        public void ShowDialog()
        {
            this.ShowWasCalled = true;
        }

        public void SetViewModel(object increasePersistenceQuotaViewModel)
        {
            this.ViewModel = increasePersistenceQuotaViewModel;
        }

        public void CloseDialog()
        {
            this.CloseWasCalled = true;
        }
    }
}
