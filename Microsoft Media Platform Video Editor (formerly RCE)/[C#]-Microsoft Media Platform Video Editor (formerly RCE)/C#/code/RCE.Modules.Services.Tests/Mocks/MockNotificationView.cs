// <copyright file="MockNotificationView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockShell.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services.Tests.Mocks
{
    using Modules.Services.Views;

    public class MockNotificationView : INotificationView
    {
        public bool ShowProgressBarCalled { get; set; }

        public bool HideProgressBarCalled { get; set; }

        public void ShowProgressBar()
        {
            this.ShowProgressBarCalled = true;
        }

        public void HideProgressBar()
        {
            this.HideProgressBarCalled = true;
        }
    }
}