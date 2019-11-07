// <copyright file="MockEncoderSettingsView.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: MockTitlesView.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.EncoderOutput.Tests.Mocks
{
    using EncoderOutput.Views;

    public class MockEncoderSettingsView : IEncoderSettingsView
    {
        public bool ShowProgressBarCalled { get; set; }

        public bool HideProgressBarCalled { get; set; }

        public IEncoderSettingsPresentationModel Model { get; set; }

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